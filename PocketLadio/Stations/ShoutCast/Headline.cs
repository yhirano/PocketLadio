#region ディレクティブを使用する

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using System.Diagnostics;
using PocketLadio.Stations;
using MiscPocketCompactLibrary.Net;
using MiscPocketCompactLibrary.Reflection;

#endregion

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// SHOUTcastのヘッドライン
    /// </summary>
    public class Headline : PocketLadio.Stations.IHeadline
    {
        /// <summary>
        /// ヘッドラインの種類
        /// </summary>
        private const string KIND_NAME = "SHOUTcast";

        /// <summary>
        /// ヘッドラインのID（ヘッドラインを識別するためのキー）
        /// </summary>
        private readonly string id;

        /// <summary>
        /// ヘッドラインの設定
        /// </summary>
        private UserSetting setting;

        /// <summary>
        /// 番組のリスト
        /// </summary>
        private Channel[] channels = new Channel[0];

        /// <summary>
        /// ヘッドラインを取得した時間
        /// </summary>
        private DateTime lastCheckTime = DateTime.MinValue;

        /// <summary>
        /// 番組の表示方法設定
        /// </summary>
        public string HeadlineViewType
        {
            get { return setting.HeadlineViewType; }
        }

        /// <summary>
        /// ソートの種類
        /// </summary>
        public enum SortKind
        {
            None, Title, Listener, ListenerTotal, BitRate
        }

        /// <summary>
        /// ソートの昇順・降順
        /// </summary>
        public enum SortScending
        {
            Ascending, Descending
        }

        #region HTML解析用正規表現

        /// <summary>
        /// HTML解析用正規表現。
        /// Path解析用。
        /// </summary>
        private readonly static Regex pathRegex =
            new Regex(@"<a\s+[^>]*href=""(.*playlist\.pls[^""]*)""[^>]*>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Rank解析用。
        /// </summary>
        private readonly static Regex rankRegex =
            new Regex(@"(\d+)</b>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Category解析用。
        /// </summary>
        private readonly static Regex categoryRegex =
            new Regex(@"^.*(\[.+?\])", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// ClusterUrl解析用。
        /// </summary>
        private readonly static Regex clusterUrlRegex =
            new Regex(@"<a\s+[^>]*href=""([^""]*)""[^>]*>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Title解析用。
        /// Willcom高速化サービス用。
        /// </summary>
        private readonly static Regex titleRegex =
            new Regex(@"(.+?)</a>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Listener解析用。
        /// </summary>
        private readonly static Regex listenerRegex =
            new Regex(@"(\d+)/(\d+)</font>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Playing解析用1。
        /// </summary>
        private readonly static Regex playingNowRegex =
            new Regex(@"Now Playing:</font>(.*)", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Playing解析用2。
        /// </summary>
        private readonly static Regex playingRegex =
            new Regex(@"\s*(.+?)</font.*$", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// BitRate解析用。
        /// Willcom高速化サービス用。
        /// </summary>
        private readonly static Regex bitRateRegex =
            new Regex(@"(\d+)</font>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Rankらしき行の解析用。
        /// </summary>
        private readonly static Regex maybeRankLineRegex =
            new Regex(@"^.*</b>", RegexOptions.None);

        #endregion

        /// <summary>
        /// 親放送局
        /// </summary>
        private readonly Station parentStation;

        /// <summary>
        /// 親放送局
        /// </summary>
        public virtual Station ParentStation
        {
            get { return parentStation; }
        }

        /// <summary>
        /// ヘッドラインのコンストラクタ
        /// </summary>
        /// <param name="id">ヘッドラインのID</param>
        /// <param name="parentStation">親放送局</param>
        public Headline(string id, Station parentStation)
        {
            this.id = id;
            this.parentStation = parentStation;
            setting = new UserSetting(this);

            try
            {
                setting.LoadSetting();
            }
            catch (XmlException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
        }

        /// <summary>
        /// 起動時の初期化メソッド。
        /// </summary>
        public static void StartUpInitialize()
        {
            ;
        }

        /// <summary>
        /// ヘッドラインのIDを返す
        /// </summary>
        /// <returns>ヘッドラインのID</returns>
        public virtual string GetId()
        {
            return id;
        }

        /// <summary>
        /// ヘッドラインの種類の名前を返す
        /// </summary>
        /// <returns>ヘッドラインの種類の名前</returns>
        public virtual string GetKindName()
        {
            return KIND_NAME;
        }

        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public virtual IChannel[] GetChannels()
        {
            return channels;
        }

        /// <summary>
        /// フィルタリングした番組の結果を返す
        /// </summary>
        /// <returns>フィルタリングした番組のリスト</returns>
        public virtual IChannel[] GetChannelsFiltered()
        {
            ArrayList alChannels = new ArrayList();

            #region 単語フィルタ処理

            // 単語フィルタが存在する場合
            if (setting.GetFilterWords().Length > 0)
            {
                foreach (IChannel channel in GetChannels())
                {
                    foreach (string filter in setting.GetFilterWords())
                    {
                        if (channel.GetFilteredWord().IndexOf(filter) != -1)
                        {
                            alChannels.Add(channel);
                            break;
                        }
                    }
                }
            }
            // 単語フィルタが存在しない場合
            else
            {
                alChannels.AddRange(GetChannels());
            }

            #endregion

            #region 最低ビットレートフィルタ処理

            ArrayList alDeleteChannels = new ArrayList();

            // 最低ビットレートフィルタが存在する場合
            if (setting.FilterAboveBitRateUse == true)
            {
                // 削除する番組のリストを作成
                foreach (Channel channel in alChannels)
                {
                    if (0 < channel.BitRate && channel.BitRate < setting.FilterAboveBitRate)
                    {
                        alDeleteChannels.Add(channel);
                    }
                }
                // 番組を削除
                foreach (Channel deleteChannel in alDeleteChannels)
                {
                    alChannels.Remove(deleteChannel);
                }
            }

            #endregion


            #region 最大ビットレートフィルタ処理

            alDeleteChannels.Clear();

            // 最大ビットレートフィルタが存在する場合
            if (setting.FilterBelowBitRateUse == true)
            {
                foreach (Channel channel in alChannels)
                {
                    if (channel.BitRate > setting.FilterBelowBitRate)
                    {
                        alDeleteChannels.Add(channel);
                    }
                }
                // 番組を削除
                foreach (Channel deleteChannel in alDeleteChannels)
                {
                    alChannels.Remove(deleteChannel);
                }
            }

            #endregion

            #region ソート処理

            if (setting.SortKind == SortKind.None)
            {
                ;
            }
            else if (setting.SortKind == SortKind.Title)
            {
                alChannels.Sort((IComparer)new ChannelTitleComparer());

            }
            else if (setting.SortKind == SortKind.Listener)
            {
                alChannels.Sort((IComparer)new ChannelListenerComparer());
            }
            else if (setting.SortKind == SortKind.ListenerTotal)
            {
                alChannels.Sort((IComparer)new ChannelListenerTotalComparer());
            }
            else if (setting.SortKind == SortKind.BitRate)
            {
                alChannels.Sort((IComparer)new ChannelBitRateComparer());
            }
            else
            {
                // ここに到達することはあり得ない
                Trace.Assert(false, "想定外の動作のため、終了します");
            }

            // 降順の場合
            if (setting.SortKind != SortKind.None && setting.SortScending == SortScending.Descending)
            {
                alChannels.Reverse();
            }

            #endregion

            return (IChannel[])alChannels.ToArray(typeof(IChannel));
        }

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        public virtual void FetchHeadline()
        {
            // 時刻をセットする
            lastCheckTime = DateTime.Now;

            WebStream st = null;
            StreamReader sr = null;

            try
            {
                // チャンネルのリスト
                ArrayList alChannels = new ArrayList();
                Channel channel = null;

                string searchWord = ((setting.SearchWord.Length != 0) ? "&s=" + setting.SearchWord : "");
                // 半角スペースと全角スペースを+に置き換える SHOUTcast上のURLでAND検索のスペースが+に置き換えられるため
                searchWord = searchWord.Replace(' ', '+').Replace("　", "+");

                string perView = ((setting.PerView.ToString().Length != 0) ? "&numresult=" + setting.PerView : "");
                Uri url = new Uri(PocketLadioInfo.ShoutcastUrl + "/?" + searchWord + perView);

                st = PocketLadioUtility.GetWebStream(url);

                sr = new StreamReader(st, Encoding.GetEncoding("Windows-1252"));
                string httpString = sr.ReadToEnd();

#if SHOUTCAST_HTTP_LOG
                // ShoutcastのHTTPのログを書き出す
                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(
                            AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.ShoutcastHttpLog,
                            false,
                            Encoding.GetEncoding("Windows-1252"));
                    sw.Write(httpString);
                }
                catch (IOException)
                {
                    throw;
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                    }
                }
#endif

                // タグの後に改行を入れる（Willcom高速化サービス対応のため）
                httpString = httpString.Replace(">", ">\n");

                string[] lines = httpString.Split('\n');

                #region HTML解析

                // 順位らしき行
                string maybeRankLine = string.Empty;

                // 1～指定行目まではHTMLを解析しない
                int analyzeHtmlFirstTo = setting.IgnoreHtmlAnalyzeFirstTo;
                // 指定行目から行末まではHTMLを解析しない
                int analyzeHtmlLast = lines.Length - setting.IgnoreHtmlAnalyzeEndFrom;

                // HTML解析
                for (int lineNumber = analyzeHtmlFirstTo; lineNumber < analyzeHtmlLast && lineNumber < lines.Length; ++lineNumber)
                {
                    /*** playlist.plsを検索 ***/
                    Match pathMatch = pathRegex.Match(lines[lineNumber]);

                    // playlist.plsが見つかった場合
                    if (pathMatch.Success)
                    {
                        channel = new Channel(this);

                        channel.Path = pathMatch.Groups[1].Value;

                        /*** Rankを検索 ***/
                        Match rankMatch = rankRegex.Match(maybeRankLine);

                        // Rankが見つかった場合
                        if (rankMatch.Success)
                        {
                            channel.Rank = rankMatch.Groups[1].Value;
                        }

                        /*** Categoryを検索 ***/
                        Match categoryMatch;

                        // Categoryが見つからない場合は行を読み飛ばして検索する
                        for (++lineNumber; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            categoryMatch = categoryRegex.Match(lines[lineNumber]);

                            // Categoryが見つかった場合
                            if (categoryMatch.Success)
                            {
                                channel.Category = categoryMatch.Groups[1].Value;
                                break;
                            }
                        }

                        /*** ClusterUrlを検索 ***/
                        Match clusterUrlMatch;

                        // ClusterUrlが見つからない場合は行を読み飛ばして検索する
                        for (; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            clusterUrlMatch = clusterUrlRegex.Match(lines[lineNumber]);

                            // Categoryが見つかった場合
                            if (clusterUrlMatch.Success)
                            {
                                try
                                {
                                    channel.ClusterUrl = new Uri(clusterUrlMatch.Groups[1].Value);
                                }
                                catch (UriFormatException)
                                {
                                    channel.ClusterUrl = null;
                                }
                                break;
                            }
                        }

                        /*** Titleを検索 ***/
                        Match titleMatch;

                        // Titleが見つからない場合は行を読み飛ばして検索する
                        for (; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            titleMatch = titleRegex.Match(lines[lineNumber]);

                            // Titleが見つかった場合
                            if (titleMatch.Success)
                            {
                                channel.Title = titleMatch.Groups[1].Value;
                                break;
                            }
                        }

                        /*** Listenerを検索 ***/
                        Match listenerMatch = listenerRegex.Match(lines[lineNumber]);
                        for (; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            listenerMatch = listenerRegex.Match(lines[lineNumber]);

                            if (listenerMatch.Success)
                            {
                                break;
                            }

                            // Now Playing:は存在しない場合があるのでリスナー数検出の中でチェックを行う
                            Match playingNowMatch = playingNowRegex.Match(lines[lineNumber]);
                            if (playingNowMatch.Success)
                            {
                                Match playingMatch = playingRegex.Match(playingNowMatch.Groups[1].Value);
                                if (playingMatch.Success)
                                {
                                    channel.Playing = playingMatch.Groups[1].Value;
                                }
                            }

                        }
                        try
                        {
                            channel.Listener = int.Parse(listenerMatch.Groups[1].Value);
                            channel.ListenerTotal = int.Parse(listenerMatch.Groups[2].Value);
                        }
                        catch (ArgumentException)
                        {
                            ;
                        }
                        catch (FormatException)
                        {
                            ;
                        }
                        catch (OverflowException)
                        {
                            ;
                        }

                        /*** Bitrateを検索 ***/
                        Match bitrateMatch;

                        // Bitrateが見つからない場合は行を読み飛ばして検索する
                        for (++lineNumber; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            bitrateMatch = bitRateRegex.Match(lines[lineNumber]);

                            // Bitrateが見つかった場合
                            if (bitrateMatch.Success)
                            {
                                try
                                {
                                    channel.BitRate = int.Parse(bitrateMatch.Groups[1].Value);
                                    break;
                                }
                                catch (ArgumentException)
                                {
                                    ;
                                }
                                catch (FormatException)
                                {
                                    ;
                                }
                                catch (OverflowException)
                                {
                                    ;
                                }
                            }
                        }
                        alChannels.Add(channel);
                    }

                    /*** Rankらしき行を保存する ***/
                    Match maybeRankLineMatch = maybeRankLineRegex.Match(lines[lineNumber]);

                    if (maybeRankLineMatch.Success)
                    {
                        maybeRankLine = lines[lineNumber];

                    }
                }

                channels = (Channel[])alChannels.ToArray(typeof(Channel));

                #endregion

            }
            catch (WebException)
            {
                throw;
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (UriFormatException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (SocketException)
            {
                throw;
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// ヘッドラインをネットから取得した時刻を返す。
        /// 未取得の場合はDateTime.MinValueを返す。
        /// </summary>
        /// <returns>ヘッドラインをネットから取得した時刻</returns>
        public virtual DateTime GetLastCheckTime()
        {
            return lastCheckTime;
        }

        /// <summary>
        /// ヘッドラインの設定フォームを表示する
        /// </summary>
        /// <returns>ヘッドラインの設定フォーム</returns>
        public virtual void ShowSettingForm()
        {
            SettingForm settingForm = new SettingForm(setting);
            settingForm.ShowDialog();
            settingForm.Dispose();
        }

        /// <summary>
        /// 設定を保存していたファイルを削除する
        /// </summary>
        public virtual void DeleteUserSettingFile()
        {
            setting.DeleteUserSettingFile();
        }

        #region ソート用比較クラス

        /// <summary>
        /// タイトルを比較
        /// </summary>
        class ChannelTitleComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Title.CompareTo(((Channel)object2).Title);
            }
        }

        /// <summary>
        /// リスナ数を比較
        /// </summary>
        class ChannelListenerComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Listener - ((Channel)object2).Listener;
            }
        }

        /// <summary>
        /// 述べリスナ数を比較
        /// </summary>
        class ChannelListenerTotalComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).ListenerTotal - ((Channel)object2).ListenerTotal;
            }
        }

        /// <summary>
        /// ビットレートを比較
        /// </summary>
        class ChannelBitRateComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).BitRate - ((Channel)object2).BitRate;
            }
        }

        #endregion
    }
}
