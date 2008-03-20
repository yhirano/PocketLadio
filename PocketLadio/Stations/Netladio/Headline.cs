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

#endregion

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// ねとらじのヘッドライン
    /// </summary>
    public class Headline : PocketLadio.Stations.IHeadline
    {
        /// <summary>
        /// ヘッドラインの種類
        /// </summary>
        private const string KIND_NAME = "ねとらじ";

        /// <summary>
        /// ねとらじのヘッドラインのURL CSV
        /// </summary>
        public const string NETLADIO_HEADLINE_CVS_URL = "http://yp.ladio.livedoor.jp/stats/list.csv";

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        public const string NETLADIO_HEADLINE_XML_URL = "http://yp.ladio.livedoor.jp/stats/list.xml";

        /// <summary>
        /// ねとらじのヘッドラインのURL DAT v2(gzip)
        /// </summary>
        public const string NETLADIO_HEADLINE_DAT_V2_URL = "http://yp.ladio.livedoor.jp/stats/list.v2.dat";

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
            set {
                filtedChannelsCache = null;
                _channels = value;
            }
        }

        /// <summary>
        /// フィルタ済み番組のキャッシュ
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
            None, Nam, Tims, Cln, Clns, Bit
        }

        /// <summary>
        /// ソートの昇順・降順
        /// </summary>
        public enum SortScendings
        {
            Ascending, Descending
        }

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
        /// 起動時の初期化メソッド。何もしない。
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
                        if (0 < channel.Bit && channel.Bit < setting.FilterAboveBitRate)
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
                        if (channel.Bit > setting.FilterBelowBitRate)
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
                else if (setting.SortKind == SortKinds.Nam)
                {
                    alChannels.Sort((IComparer)new ChannelNamComparer());

                }
                else if (setting.SortKind == SortKinds.Tims)
                {
                    alChannels.Sort((IComparer)new ChannelTimsComparer());
                }
                else if (setting.SortKind == SortKinds.Cln)
                {
                    alChannels.Sort((IComparer)new ChannelClnComparer());
                }
                else if (setting.SortKind == SortKinds.Clns)
                {
                    alChannels.Sort((IComparer)new ChannelClnsComparer());
                }
                else if (setting.SortKind == SortKinds.Bit)
                {
                    alChannels.Sort((IComparer)new ChannelBitComparer());
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
        /// ヘッドラインをネットから取得する
        /// </summary>
        public virtual void FetchHeadline()
        {
            // 時刻をセットする
            lastCheckTime = DateTime.Now;

            switch (setting.HeadlineGetWay)
            {
                case UserSetting.HeadlineGetType.Cvs:
                    FetchHeadlineCvs();
                    break;
                case UserSetting.HeadlineGetType.Xml:
                    FetchHeadlineXml();
                    break;
                case UserSetting.HeadlineGetType.DatV2:
                    FetchHeadlineDatV2();
                    break;
                default:
                    // ここに到達することはあり得ない
                    Trace.Assert(false, "想定外の動作のため、終了します");
                    break;
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
        /// ヘッドラインをネットから取得する（CVS使用）
        /// </summary>
        private void FetchHeadlineCvs()
        {
            WebStream st = null;

            string[] channelsCvs;
            // チャンネルのリスト
            ArrayList alChannels = new ArrayList();

            try
            {
                st = PocketLadioUtility.GetWebStream(setting.HeadlineCsvUrl);
                WebTextFetch fetch = new WebTextFetch(st, Encoding.GetEncoding("shift-jis"));
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
                channelsCvs = httpString.Split('\n');
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
            }

            OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, channelsCvs.Length - 1));

            // 1行目はヘッダなので無視
            for (int count = 1; count < channelsCvs.Length; ++count)
            {
                if (channelsCvs[count].Length > 0)
                {
                    Channel channel = new Channel(this);
                    string[] channelCsv = channelsCvs[count].Split(',');

                    // CVS列が11列以上の場合のみ番組とみなす
                    if (channelCsv.Length >= 11)
                    {
                        // Url取得
                        try
                        {
                            if (channelCsv[0] != string.Empty)
                            {
                                channel.Url = new Uri(channelCsv[0]);
                            }
                        }
                        catch (UriFormatException)
                        {
                            ;
                        }
                        // PC上で起きることを確認したが、対処するべきか分からないのでとりあえず無視
                        catch (IndexOutOfRangeException)
                        {
                            ;
                        }

                        // Gnl取得
                        channel.Gnl = channelCsv[1];

                        // Nam取得
                        channel.Nam = channelCsv[2];

                        // Tit取得
                        channel.Tit = channelCsv[3];

                        // Mnt取得
                        channel.Mnt = channelCsv[4];

                        // Tim取得
                        channel.SetTim(channelCsv[5]);

                        // Tims取得
                        channel.SetTims(channelCsv[6]);

                        try
                        {
                            // Cln取得
                            channel.Cln = int.Parse(channelCsv[7]);
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

                        try
                        {
                            // Clns取得
                            channel.Clns = int.Parse(channelCsv[8]);
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

                        // Srv取得
                        channel.Srv = channelCsv[9];

                        // Prt取得
                        channel.Prt = channelCsv[10];

                        if (channelCsv.Length >= 12)
                        {
                            // Typ取得
                            channel.Typ = channelCsv[11];
                        }

                        if (channelCsv.Length >= 13)
                        {
                            try
                            {
                                // Bit取得
                                channel.Bit = int.Parse(channelCsv[12]);
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

                        alChannels.Add(channel);
                    }
                }

                OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(count, channelsCvs.Length - 1));
            }

            OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(channelsCvs.Length - 1, channelsCvs.Length - 1));

            channels = (Channel[])alChannels.ToArray(typeof(Channel));
        }

        /// <summary>
        /// ヘッドラインをネットから取得する（XML使用）
        /// </summary>
        private void FetchHeadlineXml()
        {
            WebStream st = null;
            XmlTextReader reader = null;

            try
            {
                // 番組のリスト
                ArrayList alChannels = new ArrayList();

                st = PocketLadioUtility.GetWebStream(setting.HeadlineXmlUrl);

                reader = new XmlTextReader(st);

                // チャンネル
                Channel channel = new Channel(this);
                // sourceタグの中にいるか
                bool inSourceFlag = false;

                // 解析したヘッドラインの個数
                int analyzedCount = 0;

                OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "source")
                        {
                            inSourceFlag = true;
                            channel = new Channel(this);
                        } // End of source
                        // sourceタグの中にいる場合
                        else if (inSourceFlag == true)
                        {
                            if (reader.LocalName == "url")
                            {
                                try
                                {
                                    string url = reader.ReadString();
                                    if (url != string.Empty)
                                    {
                                        channel.Url = new Uri(url);
                                    }
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of url
                            else if (reader.LocalName == "gnl")
                            {
                                channel.Gnl = reader.ReadString();
                            } // End of gnl
                            else if (reader.LocalName == "nam")
                            {
                                channel.Nam = reader.ReadString();
                            } // End of nam
                            else if (reader.LocalName == "tit")
                            {
                                channel.Tit = reader.ReadString();
                            } // End of tit
                            else if (reader.LocalName == "mnt")
                            {
                                channel.Mnt = reader.ReadString();
                            } // End of mnt
                            else if (reader.LocalName == "tim")
                            {
                                channel.SetTim(reader.ReadString());
                            } // End of tim
                            else if (reader.LocalName == "tims")
                            {
                                channel.SetTims(reader.ReadString());
                            } // End of tims
                            else if (reader.LocalName == "cln")
                            {
                                try
                                {
                                    channel.Cln = int.Parse(reader.ReadString());
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
                            } // End of cln
                            else if (reader.LocalName == "clns")
                            {
                                try
                                {
                                    channel.Clns = int.Parse(reader.ReadString());
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
                            } // End of clns
                            else if (reader.LocalName == "srv")
                            {
                                channel.Srv = reader.ReadString();
                            } // End of srv
                            else if (reader.LocalName == "prt")
                            {
                                channel.Prt = reader.ReadString();
                            } // End of prt
                            else if (reader.LocalName == "typ")
                            {
                                channel.Typ = reader.ReadString();
                            } // End of typ
                            else if (reader.LocalName == "bit")
                            {
                                try
                                {
                                    channel.Bit = int.Parse(reader.ReadString());
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
                            } // End of bit
                        } // End of sourceタグの中にいる場合
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "source")
                        {
                            inSourceFlag = false;
                            alChannels.Add(channel);
                            OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++analyzedCount, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));
                        }
                    }
                }

                OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(analyzedCount, analyzedCount));

                channels = (Channel[])alChannels.ToArray(typeof(Channel));
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        #region dat v2解析用正規表現

        private static readonly Regex urlRegex = new Regex("^URL=(.*)", RegexOptions.None);

        private static readonly Regex gnlRegex = new Regex("^GNL=(.*)", RegexOptions.None);

        private static readonly Regex namRegex = new Regex("^NAM=(.*)", RegexOptions.None);

        private static readonly Regex mntRegex = new Regex("^MNT=(.*)", RegexOptions.None);

        private static readonly Regex timsRegex = new Regex("^TIMS=(.*)", RegexOptions.None);

        private static readonly Regex clnRegex = new Regex(@"^CLN=(\d+)", RegexOptions.None);

        private static readonly Regex clnsRegex = new Regex(@"^CLNS=(\d+)", RegexOptions.None);

        private static readonly Regex maxRegex = new Regex(@"^MNT=(\d+)", RegexOptions.None);

        private static readonly Regex srvRegex = new Regex("^SRV=(.*)", RegexOptions.None);

        private static readonly Regex prtRegex = new Regex("^PRT=(.*)", RegexOptions.None);

        private static readonly Regex bitRegex = new Regex(@"^BIT=(\d+)", RegexOptions.None);

        private static readonly Regex songRegex = new Regex("^SONG=(.*)", RegexOptions.None);

        #endregion // dat v2解析用正規表現

        /// <summary>
        /// ヘッドラインをネットから取得する（DAT v2使用）
        /// </summary>
        private void FetchHeadlineDatV2()
        {
            WebStream st = null;

            string[] channelsDat;
            // チャンネルのリスト
            ArrayList alChannels = new ArrayList();

            try
            {
                st = PocketLadioUtility.GetWebStream(setting.HeadlineDatV2Url);
                WebTextFetch fetch = new WebTextFetch(st, Encoding.GetEncoding("shift-jis"));
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
                channelsDat = httpString.Split('\n');
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
            }

            // 番組の総数を数える
            int channelLength = 0;
            for (int i = 0; i < channelsDat.Length; ++i)
            {
                // 空行の場合
                if (channelsDat[i] == string.Empty)
                {
                    ++channelLength;
                }
            }

            OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, channelLength));

            Channel channel = null;
            // 解析済みの番組数
            int channelAnalyzed = 0;

            for (int count = 0; count < channelsDat.Length; ++count)
            {
                // Url取得
                Match urlMatch = urlRegex.Match(channelsDat[count]);
                if (urlMatch.Success)
                {
                    try
                    {
                        if (urlMatch.Groups[1].Value != string.Empty)
                        {
                            if (channel == null)
                            {
                                channel = new Channel(this);
                            }
                            channel.Url = new Uri(urlMatch.Groups[1].Value);
                        }
                    }
                    catch (UriFormatException)
                    {
                        ;
                    }

                    continue;
                }

                // Gnl取得
                Match gnlMatch = gnlRegex.Match(channelsDat[count]);
                if (gnlMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Gnl = gnlMatch.Groups[1].Value;

                    continue;
                }

                Match namMatch = namRegex.Match(channelsDat[count]);
                if (namMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Nam = namMatch.Groups[1].Value;

                    continue;
                }

                Match mntMatch = mntRegex.Match(channelsDat[count]);
                if (mntMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Mnt = mntMatch.Groups[1].Value;

                    continue;
                }

                Match timsMatch = timsRegex.Match(channelsDat[count]);
                if (timsMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.SetTims(timsMatch.Groups[1].Value);

                    continue;
                }

                // Cln取得
                Match clnMatch = clnRegex.Match(channelsDat[count]);
                if (clnMatch.Success)
                {
                    try
                    {
                        if (channel == null)
                        {
                            channel = new Channel(this);
                        }

                        channel.Cln = int.Parse(clnMatch.Groups[1].Value);
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

                    continue;
                }

                // Clns取得
                Match clnsMatch = clnsRegex.Match(channelsDat[count]);
                if (clnsMatch.Success)
                {
                    try
                    {
                        if (channel == null)
                        {
                            channel = new Channel(this);
                        }

                        channel.Clns = int.Parse(clnsMatch.Groups[1].Value);
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

                    continue;
                }

                Match maxMatch = maxRegex.Match(channelsDat[count]);
                if (maxMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    continue;
                }

                Match srvMatch = srvRegex.Match(channelsDat[count]);
                if (srvMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Srv = srvMatch.Groups[1].Value;

                    continue;
                }

                Match prtMatch = prtRegex.Match(channelsDat[count]);
                if (prtMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Prt = prtMatch.Groups[1].Value;

                    continue;
                }

                Match bitMatch = bitRegex.Match(channelsDat[count]);
                if (bitMatch.Success)
                {
                    try
                    {
                        if (channel == null)
                        {
                            channel = new Channel(this);
                        }

                        channel.Bit = int.Parse(bitMatch.Groups[1].Value);
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

                    continue;
                }

                Match songMatch = songRegex.Match(channelsDat[count]);
                if (prtMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Tit = songMatch.Groups[1].Value;

                    continue;
                }

                if (channelsDat[count] == string.Empty)
                {
                    if (channel != null)
                    {
                        alChannels.Add(channel);
                        OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++channelAnalyzed, channelLength));
                        channel = null;
                    }
                }
            }

            OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(channelLength, channelLength));

            channels = (Channel[])alChannels.ToArray(typeof(Channel));
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
        class ChannelNamComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Nam.CompareTo(((Channel)object2).Nam);
            }
        }

        /// <summary>
        /// 放送開始時間を比較
        /// </summary>
        class ChannelTimsComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                Channel channel1 = (Channel)object1;
                Channel channel2 = (Channel)object2;

                if (channel1.Tims > channel2.Tims)
                {
                    return 1;
                }
                if (channel1.Tims == channel2.Tims)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 現リスナ数を比較
        /// </summary>
        class ChannelClnComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Cln - ((Channel)object2).Cln;
            }
        }

        /// <summary>
        /// 述べリスナ数を比較
        /// </summary>
        class ChannelClnsComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Clns - ((Channel)object2).Clns;
            }
        }

        /// <summary>
        /// ビットレートを比較
        /// </summary>
        class ChannelBitComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Bit - ((Channel)object2).Bit;
            }
        }

        #endregion
    }
}
