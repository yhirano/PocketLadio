#region ディレクティブを使用する

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using PocketLadio.Stations;
using MiscPocketCompactLibrary.Net;

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
        /// SHOUTcastのURL
        /// </summary>
        public Uri ShoutCastUrl
        {
            get { return UserSetting.ShoutcastUrl; }
        }

        #region HTML解析用正規表現

        /// <summary>
        /// HTML解析用正規表現。
        /// Path解析用。
        /// </summary>
        private readonly static Regex pathRegex = new Regex(
            @"<a\s+[^>]*href=""(.*playlist\.pls[^""]*)""[^>]*>",
            RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Rank解析用。
        /// </summary>
        private readonly static Regex rankRegex = new Regex(@"(\d+)</b>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Category解析用。
        /// </summary>
        private readonly static Regex categoryRegex = new Regex(@"^.*(\[.+?\])", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// ClusterUrl解析用。
        /// </summary>
        private readonly static Regex clusterUrlRegex = new Regex(
            @"<a\s+[^>]*href=""(.*[^""]*)""[^>]*>",
            RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Title解析用。
        /// </summary>
        private readonly static Regex titleRegex = new Regex(@"<a.*[^>]*>(.+?)</a>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Listener解析用。
        /// </summary>
        private readonly static Regex listenerRegex = new Regex(@"(\d+/\d+)</font>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Playing解析用1。
        /// </summary>
        private readonly static Regex playingNowRegex = new Regex(@"^.*Now Playing:</font>(.*)", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Playing解析用2。
        /// </summary>
        private readonly static Regex playingRegex = new Regex(@"\s*(.+?)</font.*$", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// BitRate解析用。
        /// </summary>
        private readonly static Regex bitRateRegex = new Regex(@"(\d+)</font>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Rankらしき行の解析用。
        /// </summary>
        private readonly static Regex maybeRankLineRegex = new Regex(@"^.*</b>", RegexOptions.None);

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
        /// 起動時の初期化メソッド。Max bit rate設定のハッシュの初期化を行う
        /// </summary>
        public static void StartUpInitialize()
        {
            StreamReader srMaxBitRate = null;
            StreamReader srPerView = null;

            try
            {
                #region ビットレート対応表の読み込み

                // 現在のコードを実行しているAssemblyを取得
                System.Reflection.Assembly thisAssembly
                    = System.Reflection.Assembly.GetExecutingAssembly();
                // 指定されたマニフェストリソースを読み込む
                srMaxBitRate =
                    new StreamReader(thisAssembly.GetManifestResourceStream(UserSetting.SHOUTCAST_MAX_BIT_RATE_SETTING_FILE),
                    Encoding.GetEncoding("shift-jis"));

                // 内容を読み込む
                string bitRateString = srMaxBitRate.ReadToEnd();

                string[] maxBitRateRawArray = bitRateString.Split('\n');

                foreach (string maxBitRateRaw in maxBitRateRawArray)
                {
                    if (maxBitRateRaw.Length != 0)
                    {
                        string[] maxBitRate = maxBitRateRaw.Split(',');
                        UserSetting.MaxBitRateTable.Add(maxBitRate[0], maxBitRate[1].Trim());
                    }
                }

                #endregion

                #region ヘッドライン取得数の設定可能値を読み込む
                // 指定されたマニフェストリソースを読み込む
                srPerView =
                    new StreamReader(thisAssembly.GetManifestResourceStream(UserSetting.SHOUTCAST_PER_VIEW_SETTING_FILE),
                    Encoding.GetEncoding("shift-jis"));

                // 内容を読み込む
                UserSetting.PerViewArray = srPerView.ReadToEnd().Split('\n');
                for (int count = 0; count < UserSetting.PerViewArray.Length; count++)
                {
                    UserSetting.PerViewArray[count] = UserSetting.PerViewArray[count].Trim();
                }

                #endregion
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally
            {
                if (srMaxBitRate != null)
                {
                    srMaxBitRate.Close();
                }
                if (srPerView != null)
                {
                    srPerView.Close();
                }
            }

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
        /// ヘッドラインをネットから取得する
        /// </summary>
        public virtual void FetchHeadline()
        {
            // 時刻をセットする
            lastCheckTime = DateTime.Now;

            Stream st = null;
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
                string maxBitRate = ((setting.MaxBitRate.Length != 0) ? "&bitrate=" + setting.MaxBitRate : "");
                Uri url = new Uri(ShoutCastUrl.ToString() + "?" + searchWord + perView + maxBitRate);

                st = PocketLadioUtility.GetWebStream(url);

                sr = new StreamReader(st, Encoding.GetEncoding("Windows-1252"));
                string httpString = sr.ReadToEnd();
                string[] lines = httpString.Split('\n');

                #region HTML解析

                // 順位らしき行
                string maybeRankLine = "";

                // 1～指定行目まではHTMLを解析しない
                int analyzeHtmlFirstTo = setting.IgnoreHtmlAnalyzeFirstTo;
                // 指定行目から行末まではHTMLを解析しない
                int analyzeHtmlLast = lines.Length - setting.IgnoreHtmlAnalyzeEndFrom;

                // HTML解析
                for (int lineNumber = analyzeHtmlFirstTo; lineNumber < analyzeHtmlLast; ++lineNumber)
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
                        channel.Listener = listenerMatch.Groups[1].Value;

                        /*** Bitrateを検索 ***/
                        Match bitrateMatch;

                        // Bitrateが見つからない場合は行を読み飛ばして検索する
                        for (++lineNumber; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            bitrateMatch = bitRateRegex.Match(lines[lineNumber]);

                            // Bitrateが見つかった場合
                            if (bitrateMatch.Success)
                            {
                                channel.BitRate = bitrateMatch.Groups[1].Value;
                                break;
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
    }
}
