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
using System.Globalization;
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
        /// SHOUTcastのURL
        /// </summary>
        public const string SHOUTCAST_URL = "http://www.shoutcast.com/directory/search_results.jsp";

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
        private Channel[] _channels = new Channel[0];

        /// <summary>
        /// 番組のリスト
        /// </summary>
        private Channel[] channels
        {
            get { return _channels; }
            set
            {
                filtedChannelsCache = null;
                _channels = value;
            }
        }

        /// <summary>
        /// フィルター済み番組のキャッシュ
        /// </summary>
        private Channel[] filtedChannelsCache;

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
        public enum SortKinds
        {
            None, Title, Listener, BitRate
        }

        /// <summary>
        /// ソートの昇順・降順
        /// </summary>
        public enum SortScendings
        {
            Ascending, Descending
        }

        #region HTML解析用正規表現

        /// <summary>
        /// HTML解析用正規表現。
        /// 空白。
        /// </summary>
        private readonly static Regex emptyRegex =
            new Regex(@"^\s+$", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// 放送局。
        /// </summary>
        private readonly static Regex stationRegex =
            new Regex(@"<div\s+[^>]*id=""\d+""[^>]*>$", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Path解析用。
        /// </summary>
        private readonly static Regex pathRegex =
            new Regex(@"<a\s+[^>]*href=""(.*tunein-station\.pls[^""]*)""[^>]*>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Title解析用1。
        /// </summary>
        private readonly static Regex titleRegex1 =
            new Regex(@"Station:</span>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Title解析用2。
        /// </summary>
        private readonly static Regex titleRegex2 =
            new Regex(@"<a\s+[^>]*href=""([^""]*)""[^>]*>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Title解析用3。
        /// </summary>
        private readonly static Regex titleRegex3 =
            new Regex(@"^\s*(.+?)$", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Playing解析用1。
        /// </summary>
        private readonly static Regex playingNowRegex1 =
            new Regex(@"Now Playing:</span>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Playing解析用2。
        /// </summary>
        private readonly static Regex playingNowRegex2 =
            new Regex(@"^\s*(.+?)</span>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Genre解析用1。
        /// </summary>
        private readonly static Regex genreRegex1 =
            new Regex(@"Genre:</span>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Genre解析用2。
        /// </summary>
        private readonly static Regex genreRegex2 =
            new Regex(@"(.+?)</span>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Listener解析用1。
        /// </summary>
        private readonly static Regex listenerRegex1 =
            new Regex(@"Listeners:</span>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// Listener解析用2。
        /// </summary>
        private readonly static Regex listenerRegex2 =
            new Regex(@"([\d\,]+)</span>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// BitRate解析用1。
        /// </summary>
        private readonly static Regex bitRateRegex1 =
            new Regex(@"Bitrate:</span>", RegexOptions.None);

        /// <summary>
        /// HTML解析用正規表現。
        /// BitRate解析用2。
        /// </summary>
        private readonly static Regex bitRateRegex2 =
            new Regex(@"(\d+)</span>", RegexOptions.None);

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
            if (id == null)
            {
                throw new ArgumentNullException("HeadlineのIDにNullは指定できません");
            }
            if (id == string.Empty)
            {
                throw new ArgumentException("HeadlineのIDに空文字は指定できません");
            }
            if (parentStation == null)
            {
                throw new ArgumentNullException("Headlineの親放送局にNullは指定できません");
            }

            this.id = id;
            this.parentStation = parentStation;
            setting = new UserSetting(this);
            setting.LoadSetting();
            setting.FilterChanged += new EventHandler(setting_FilterChanged);
        }

        private void setting_FilterChanged(object sender, EventArgs e)
        {
            // フィルター条件が変わった場合は、フィルター番組キャッシュを空にする
            filtedChannelsCache = null;
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
            // フィルター結果キャッシュが空の場合、フィルター結果をキャッシュに格納
            if (filtedChannelsCache == null)
            {
                ArrayList alChannels = new ArrayList();

                #region 単語フィルター処理

                // 一致単語フィルター・除外フィルターが存在する場合
                if (setting.GetFilterMatchWords().Length > 0 && setting.GetFilterExclusionWords().Length > 0)
                {
                    foreach (IChannel channel in GetChannels())
                    {
                        if (IsMatchFilterMatchWords(channel) == true && IsMatchFilterExclusionWords(channel) == false)
                        {
                            alChannels.Add(channel);
                        }
                    }
                }
                // 一致単語フィルターのみが存在する場合
                else if (setting.GetFilterMatchWords().Length > 0 && setting.GetFilterExclusionWords().Length <= 0)
                {
                    foreach (IChannel channel in GetChannels())
                    {
                        if (IsMatchFilterMatchWords(channel) == true)
                        {
                            alChannels.Add(channel);
                        }
                    }
                }
                // 除外フィルターのみが存在する場合
                else if (setting.GetFilterMatchWords().Length <= 0 && setting.GetFilterExclusionWords().Length > 0)
                {
                    foreach (IChannel channel in GetChannels())
                    {
                        if (IsMatchFilterExclusionWords(channel) == false)
                        {
                            alChannels.Add(channel);
                        }
                    }
                }
                // 単語フィルターが存在しない場合
                else
                {
                    alChannels.AddRange(GetChannels());
                }

                #endregion

                #region 最低ビットレートフィルター処理

                ArrayList alDeleteChannels = new ArrayList();

                // 最低ビットレートフィルターが存在する場合
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

                #region 最大ビットレートフィルター処理

                alDeleteChannels.Clear();

                // 最大ビットレートフィルターが存在する場合
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

                if (setting.SortKind == SortKinds.None)
                {
                    ;
                }
                else if (setting.SortKind == SortKinds.Title)
                {
                    alChannels.Sort((IComparer)new ChannelTitleComparer());

                }
                else if (setting.SortKind == SortKinds.Listener)
                {
                    alChannels.Sort((IComparer)new ChannelListenerComparer());
                }
                else if (setting.SortKind == SortKinds.BitRate)
                {
                    alChannels.Sort((IComparer)new ChannelBitRateComparer());
                }
                else
                {
                    // ここに到達することはあり得ない
                    Trace.Assert(false, "想定外の動作のため、終了します");
                }

                // 降順の場合
                if (setting.SortKind != SortKinds.None && setting.SortScending == SortScendings.Descending)
                {
                    alChannels.Reverse();
                }

                #endregion

                // フィルター結果をキャッシュに格納
                filtedChannelsCache = (Channel[])alChannels.ToArray(typeof(Channel));
            }

            return filtedChannelsCache;
        }


        /// <summary>
        /// 番組が一致単語フィルターに合致するかを調べる
        /// </summary>
        /// <param name="channel">番組</param>
        /// <returns>番組が一致単語フィルターに合致したらtrue、それ以外はfalse</returns>
        private bool IsMatchFilterMatchWords(IChannel channel)
        {
            foreach (string filter in setting.GetFilterMatchWords())
            {
                foreach (string filted in channel.GetFilteredWords())
                {
                    if (filted.ToLower(CultureInfo.InvariantCulture).IndexOf(filter.ToLower(CultureInfo.InvariantCulture)) != -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 番組が除外単語フィルターに合致するかを調べる
        /// </summary>
        /// <param name="channel">番組</param>
        /// <returns>番組が除外単語フィルターに合致したらtrue、それ以外はfalse</returns>
        private bool IsMatchFilterExclusionWords(IChannel channel)
        {
            foreach (string filter in setting.GetFilterExclusionWords())
            {
                foreach (string filted in channel.GetFilteredWords())
                {
                    if (filted.ToLower(CultureInfo.InvariantCulture).IndexOf(filter.ToLower(CultureInfo.InvariantCulture)) != -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        public virtual void FetchHeadline()
        {
            // 時刻をセットする
            lastCheckTime = DateTime.Now;

            WebStream st = null;

            try
            {
                // チャンネルのリスト
                ArrayList alChannels = new ArrayList();
                Channel channel = null;

                string searchWord = ((setting.SearchWord.Length != 0) ? "&s=" + setting.SearchWord : string.Empty);
                // 半角スペースと全角スペースを+に置き換える SHOUTcast上のURLでAND検索のスペースが+に置き換えられるため
                searchWord = searchWord.Replace(' ', '+').Replace("　", "+");
                Uri url = new Uri(Headline.SHOUTCAST_URL + "?" + searchWord);

                st = PocketLadioUtility.GetWebStream(url);
                WebTextFetch fetch = new WebTextFetch(st, Encoding.GetEncoding("Windows-1252"));
                if (HeadlineFetch != null)
                {
                    fetch.Fetch += HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    fetch.Fetching += HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    fetch.Fetched += HeadlineFetched;
                }
                string httpString = fetch.ReadToEnd();

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

                string[] lines = httpString.Split('\n', '\r');
                // 空行を取り除く
                {
                    ArrayList alLines = new ArrayList();
                    foreach (string line in lines)
                    {
                        Match emptyMatch = emptyRegex.Match(line);
                        if (line != string.Empty && emptyMatch.Success == false)
                        {
                            alLines.Add(line);
                        }
                    }
                    lines = (string[])alLines.ToArray(typeof(string));
                }

                #region HTML解析

                OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, -1));

                // 解析したヘッドラインの個数
                int analyzedCount = 0;

                // HTML解析
                for (int lineNumber = 0; lineNumber < lines.Length; ++lineNumber)
                {
                    Match stationMatch = stationRegex.Match(lines[lineNumber]);

                    if (stationMatch.Success)
                    {
                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                        {
                            /*** playlist.plsを検索 ***/
                            Match pathMatch = pathRegex.Match(lines[lineNumber]);

                            // tunein-station.plsが見つかった場合
                            if (pathMatch.Success)
                            {
                                channel = new Channel(this);

                                channel.PlayUrl = new Uri(pathMatch.Groups[1].Value);

                                /*** Titleを検索 ***/
                                Match titleMatch;

                                // Titleが見つからない場合は行を読み飛ばして検索する
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    titleMatch = titleRegex1.Match(lines[lineNumber]);

                                    // Titleが見つかった場合
                                    if (titleMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            titleMatch = titleRegex2.Match(lines[lineNumber]);
                                            if (titleMatch.Success)
                                            {
                                                channel.ClusterUrl = new Uri(titleMatch.Groups[1].Value);
                                                break;
                                            }
                                        }

                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            titleMatch = titleRegex3.Match(lines[lineNumber]);
                                            if (titleMatch.Success)
                                            {
                                                channel.Title = titleMatch.Groups[1].Value;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                /*** Playingを検索 ***/
                                Match playingMatch;

                                // Playingが見つからない場合は行を読み飛ばして検索する
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    playingMatch = playingNowRegex1.Match(lines[lineNumber]);

                                    // Playingが見つかった場合
                                    if (playingMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            playingMatch = playingNowRegex2.Match(lines[lineNumber]);
                                            if (playingMatch.Success)
                                            {
                                                channel.Playing = playingMatch.Groups[1].Value;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                /*** Genreを検索 ***/
                                Match genreMatch;

                                // Genreが見つからない場合は行を読み飛ばして検索する
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    genreMatch = genreRegex1.Match(lines[lineNumber]);

                                    // Genreが見つかった場合
                                    if (genreMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            genreMatch = genreRegex2.Match(lines[lineNumber]);
                                            if (genreMatch.Success)
                                            {
                                                channel.Genre = genreMatch.Groups[1].Value;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                /*** Listenerを検索 ***/
                                Match listenerMatch;

                                // Listenerが見つからない場合は行を読み飛ばして検索する
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    listenerMatch = listenerRegex1.Match(lines[lineNumber]);

                                    // Listenerが見つかった場合
                                    if (listenerMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            listenerMatch = listenerRegex2.Match(lines[lineNumber]);
                                            if (listenerMatch.Success)
                                            {
                                                try
                                                {
                                                    channel.Listener = int.Parse(listenerMatch.Groups[1].Value.Replace(",", string.Empty));
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
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                /*** Bitrateを検索 ***/
                                Match bitRateMatch;

                                // Bitrateが見つからない場合は行を読み飛ばして検索する
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    bitRateMatch = bitRateRegex1.Match(lines[lineNumber]);

                                    // Bitrateが見つかった場合
                                    if (bitRateMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            bitRateMatch = bitRateRegex2.Match(lines[lineNumber]);
                                            if (bitRateMatch.Success)
                                            {
                                                try
                                                {
                                                    channel.BitRate = int.Parse(bitRateMatch.Groups[1].Value);
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
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                alChannels.Add(channel);
                                OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++analyzedCount, -1));
                                break;
                            }
                        }
                    }
                }

                OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(analyzedCount, analyzedCount));

                channels = (Channel[])alChannels.ToArray(typeof(Channel));

                #endregion

            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
            }
        }

        /// <summary>
        /// ヘッドラインをネットから取得する前に発生するイベント
        /// </summary>
        public event FetchEventHandler HeadlineFetch;

        /// <summary>
        /// HeadlineFetchイベントの実行
        /// </summary>
        /// <param name="e">イベント</param>
        private void OnHeadlineFetch(FetchEventArgs e)
        {
            if (HeadlineFetch != null)
            {
                HeadlineFetch(this, e);
            }
        }

        /// <summary>
        /// ヘッドラインをネットから取得している最中に発生するイベント
        /// </summary>
        public event FetchEventHandler HeadlineFetching;

        /// <summary>
        /// HeadlineFetchingイベントの実行
        /// </summary>
        /// <param name="e">イベント</param>
        private void OnHeadlineFetching(FetchEventArgs e)
        {
            if (HeadlineFetching != null)
            {
                HeadlineFetching(this, e);
            }
        }

        /// <summary>
        /// ヘッドラインをネットから取得した後に発生するイベント
        /// </summary>
        public event FetchEventHandler HeadlineFetched;

        /// <summary>
        /// HeadlineFetchedイベントの実行
        /// </summary>
        /// <param name="e">イベント</param>
        private void OnHeadlineFetched(FetchEventArgs e)
        {
            if (HeadlineFetched != null)
            {
                HeadlineFetched(this, e);
            }
        }

        /// <summary>
        /// ヘッドラインを解析する前に発生するイベント
        /// </summary>
        public event HeadlineAnalyzeEventHandler HeadlineAnalyze;

        /// <summary>
        /// HeadlineAnalyzeイベントの実行
        /// </summary>
        /// <param name="e">イベント</param>
        private void OnHeadlineAnalyze(HeadlineAnalyzeEventArgs e)
        {
            if (HeadlineAnalyze != null)
            {
                HeadlineAnalyze(this, e);
            }
        }

        /// <summary>
        /// ヘッドラインを解析している最中に発生するイベント
        /// </summary>
        public event HeadlineAnalyzeEventHandler HeadlineAnalyzing;

        /// <summary>
        /// HeadlineAnalyzingイベントの実行
        /// </summary>
        /// <param name="e">イベント</param>
        private void OnHeadlineAnalyzing(HeadlineAnalyzeEventArgs e)
        {
            if (HeadlineAnalyzing != null)
            {
                HeadlineAnalyzing(this, e);
            }
        }

        /// <summary>
        /// ヘッドラインを解析した後に発生するイベント
        /// </summary>
        public event HeadlineAnalyzeEventHandler HeadlineAnalyzed;

        /// <summary>
        /// HeadlineAnalyzedイベントの実行
        /// </summary>
        /// <param name="e">イベント</param>
        private void OnHeadlineAnalyzed(HeadlineAnalyzeEventArgs e)
        {
            if (HeadlineAnalyzed != null)
            {
                HeadlineAnalyzed(this, e);
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
        /// フィルターに単語を登録するためにヘッドライン設定フォームを表示する
        /// </summary>
        /// <param name="filterWord">フィルターに追加する単語</param>
        public void ShowSettingFormForAddFilter(string filterWord)
        {
            SettingForm settingForm = new SettingForm(setting);
            settingForm.ShowDialogForAddWordFilter(filterWord);
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
