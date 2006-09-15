using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using PocketLadio.Utility;
using PocketLadio.Stations;

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
            get { return UserSetting.SHOUTCAST_URL; }
        }

        #region HTML解析用正規表現

        /// <summary>
        /// HTML解析用正規表現。
        /// Path解析用。
        /// </summary>
        private readonly static Regex pathRegex = new Regex(
            @"<a\s+[^>]*href\s*=\s*(?:""(?<1>.*playlist\.pls[^""]*)""|(?<1>.*playlist\.pls[^\s>]+))[^>]*>(?<2>[^<]*)",
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
            @"<a\s+[^>]*href\s*=\s*(?:""(?<1>.*[^""]*)""|(?<1>.*[^\s>]+))[^>]*>(?<2>[^<]*)",
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
        private readonly static Regex listenerRegex = new Regex(@"\D*(\d+/\d+)</font>", RegexOptions.None);

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
        private readonly static Regex bitRateRegex = new Regex(@"\D(\d+)</font>", RegexOptions.None);

        /// HTML解析用正規表現。
        /// </summary>
        private readonly static Regex maybeRankLineRegex = new Regex(@"^.*</b>", RegexOptions.None);

        #endregion

        /// <summary>
        /// Max Bit Rate設定の設定表示と実際値を示すファイル
        /// </summary>
        private const string SHOUTCAST_MAX_BIT_RATE_SETTING_FILE
            = "PocketLadio.Resource.ShoutCastMaxBitRateSetting.txt";

        /// <summary>
        /// ヘッドラインのコンストラクタ
        /// </summary>
        /// <param name="ID">ヘッドラインのID</param>
        public Headline(string id)
        {
            this.id = id;
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
            StreamReader sr = null;

            try
            {
                // 現在のコードを実行しているAssemblyを取得
                System.Reflection.Assembly thisAssembly
                    = System.Reflection.Assembly.GetExecutingAssembly();
                // 指定されたマニフェストリソースを読み込む
                sr =
                    new StreamReader(thisAssembly.GetManifestResourceStream(SHOUTCAST_MAX_BIT_RATE_SETTING_FILE),
                    Encoding.GetEncoding("shift-jis"));
                // 内容を読み込む
                string bitRateString = sr.ReadToEnd();

                string[] maxBitRateRawArray = bitRateString.Split('\n');

                foreach (string maxBitRateRaw in maxBitRateRawArray)
                {
                    if (maxBitRateRaw.Length != 0)
                    {
                        string[] maxBitRate = maxBitRateRaw.Split(',');
                        UserSetting.MaxBitRateTable.Add(maxBitRate[0], maxBitRate[1].Trim());
                    }
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
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
        public virtual void WebGetHeadline()
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

                string searchWord = ((setting.SearchWord.Length != 0) ? "?s=" + setting.SearchWord : "");
                // 半角スペースと全角スペースを+に置き換える SHOUTcast上のURLでAND検索のスペースが+に置き換えられるため
                searchWord = searchWord.Replace(' ', '+').Replace("　", "+");

                string perView = ((setting.PerView.ToString().Length != 0) ? "&numresult=" + setting.PerView : "");
                string maxBitRate = ((setting.MaxBitRate.Length != 0) ? "&bitrate=" + setting.MaxBitRate : "");
                Uri url = new Uri(ShoutCastUrl.ToString() + searchWord + perView + maxBitRate);

                st = PocketLadioUtility.GetHttpStream(url);
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
                    Match pathMatch = pathRegex.Match(lines[lineNumber].Trim());

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
                            categoryMatch = categoryRegex.Match(lines[lineNumber].Trim());

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
                            clusterUrlMatch = clusterUrlRegex.Match(lines[lineNumber].Trim());

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
                            titleMatch = titleRegex.Match(lines[lineNumber].Trim());

                            // Titleが見つかった場合
                            if (titleMatch.Success)
                            {
                                channel.Title = titleMatch.Groups[1].Value;
                                break;
                            }
                        }

                        /*** Listenerを検索 ***/
                        Match listenerMatch = listenerRegex.Match(lines[lineNumber].Trim());
                        for (; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            listenerMatch = listenerRegex.Match(lines[lineNumber].Trim());
                            if (listenerMatch.Success)
                            {
                                break;
                            }

                            // Now Playing:は存在しない場合があるのでリスナー数検出の中でチェックを行う
                            Match playingNowMatch = playingNowRegex.Match(lines[lineNumber].Trim());
                            if (playingNowMatch.Success)
                            {
                                Match playingMatch = playingRegex.Match(playingNowMatch.Groups[1].Value.Trim());
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
                            bitrateMatch = bitRateRegex.Match(lines[lineNumber].Trim());

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
            settingForm settingForm = new settingForm(setting);
            settingForm.ShowDialog();
            settingForm.Dispose();
        }

        /// <summary>
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="channel">番組</param>
        /// <returns>番組の詳細フォーム</returns>
        public virtual void ShowPropertyForm(IChannel channel)
        {
            ChannelPropertyForm channelPropertyForm = new ChannelPropertyForm((Channel)channel);
            channelPropertyForm.ShowDialog();
            channelPropertyForm.Dispose();
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
