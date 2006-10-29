#region ディレクティブを使用する

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using PocketLadio.Stations;

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
            None, Nam, Tims, Cln, Clns, Bit
        }

        /// <summary>
        /// ソートの昇順・降順
        /// </summary>
        public enum SortScending
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

            if (setting.SortKind == SortKind.None)
            {
                ;
            }
            else if (setting.SortKind == SortKind.Nam)
            {
                alChannels.Sort((IComparer)new ChannelNamComparer());

            }
            else if (setting.SortKind == SortKind.Tims)
            {
                alChannels.Sort((IComparer)new ChannelTimsComparer());
            }
            else if (setting.SortKind == SortKind.Cln)
            {
                alChannels.Sort((IComparer)new ChannelClnComparer());
            }
            else if (setting.SortKind == SortKind.Clns)
            {
                alChannels.Sort((IComparer)new ChannelClnsComparer());
            }
            else if (setting.SortKind == SortKind.Bit)
            {
                alChannels.Sort((IComparer)new ChannelBitComparer());
            }
            else {
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

            try
            {
                if (setting.HeadlineGetWay == UserSetting.HeadlineGetType.Cvs)
                {
                    FetchHeadlineCvs();
                }
                else if (setting.HeadlineGetWay == UserSetting.HeadlineGetType.Xml)
                {
                    FetchHeadlineXml();
                }
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
            catch (XmlException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
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
            Stream st = null;
            StreamReader sr = null;

            try
            {
                // チャンネルのリスト
                ArrayList alChannels = new ArrayList();

                st = PocketLadioUtility.GetWebStream(setting.HeadlineCsvUrl);

                sr = new StreamReader(st, Encoding.GetEncoding("shift-jis"));
                string httpString = sr.ReadToEnd();
                string[] channelsCvs = httpString.Split('\n');

                // 1行目はヘッダなので無視
                for (int count = 1; count < channelsCvs.Length; ++count)
                {
                    if (channelsCvs[count].Length != 0)
                    {
                        Channel channel = new Channel(this);
                        string[] channelCsv = channelsCvs[count].Split(',');

                        // Url取得
                        try
                        {
                            channel.Url = new Uri(channelCsv[0]);
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

                channels = (Channel[])alChannels.ToArray(typeof(Channel));
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
        /// ヘッドラインをネットから取得する（XML使用）
        /// </summary>
        private void FetchHeadlineXml()
        {
            Stream st = null;
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
                                    channel.Url = new Uri(reader.ReadString());
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
                        }
                    }
                }

                channels = (Channel[])alChannels.ToArray(typeof(Channel));
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
            catch (SocketException)
            {
                throw;
            }
            catch (XmlException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
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
