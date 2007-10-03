#region ディレクティブを使用する

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Xml;
using PocketLadio.Stations;
using MiscPocketCompactLibrary.Net;

#endregion

namespace PocketLadio.Stations.Icecast
{
    /// <summary>
    /// Icecastのヘッドライン
    /// </summary>
    public class Headline : PocketLadio.Stations.IHeadline
    {
        /// <summary>
        /// ヘッドラインの種類
        /// </summary>
        private const string KIND_NAME = "Icecast";

        /// <summary>
        /// IcecastのURL
        /// </summary>
        public const string ICECAST_URL = "http://dir.xiph.org/yp.xml";

        /// <summary>
        /// ヘッドラインのID（ヘッドラインを識別するためのキー）
        /// </summary>
        private readonly string id;

        /// <summary>
        /// ヘッドラインの設定
        /// </summary>
        private UserSetting setting;

        /// <summary>
        /// チャンネルのリスト
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
            setting.LoadSetting();
        }

        /// <summary>
        /// 起動時の初期化メソッド。
        /// </summary>
        public static void StartUpInitialize()
        {
            // 何もしない
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

                return (IChannel[])alChannels.ToArray(typeof(IChannel));
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
            XmlTextReader reader = null;

            try
            {
                // 番組のリスト
                ArrayList alChannels = new ArrayList();

                // チャンネル
                Channel channel = null;
                // itemタグの中にいるか
                bool inEntry = false;

                st = PocketLadioUtility.GetWebStream(new Uri(Headline.ICECAST_URL));

                reader = new XmlTextReader(st);

                // 解析したヘッドラインの個数
                int analyzedCount = 0;

                OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "entry")
                        {
                            inEntry = true;
                            channel = new Channel(this);
                        } // End of item
                        // Entryタグの中にいる場合
                        else if (inEntry == true)
                        {
                            if (reader.LocalName == "server_name")
                            {
                                channel.ServerName = reader.ReadString();
                            } // End of server_name
                            else if (reader.LocalName == "listen_url")
                            {
                                try
                                {
                                    channel.ListenUrl = new Uri(reader.ReadString());
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of listen_url
                            else if (reader.LocalName == "server_type")
                            {
                                channel.ServerType = reader.ReadString();
                            } // End of server_type
                            else if (reader.LocalName == "bitrate")
                            {
                                try
                                {
                                    channel.BitRate = int.Parse(reader.ReadString());
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
                            } // End of bitrate
                            else if (reader.LocalName == "channels")
                            {
                                channel.Channels = reader.ReadString();
                            } // End of channels
                            else if (reader.LocalName == "samplerate")
                            {
                                try
                                {
                                    channel.SampleRate = int.Parse(reader.ReadString());
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
                            } // End of samplerate
                            else if (reader.LocalName == "genre")
                            {
                                channel.Genre = reader.ReadString();
                            } // End of genre
                            else if (reader.LocalName == "current_song")
                            {
                                channel.CurrentSong = reader.ReadString();
                            } // End of current_song

                        } // End of entryタグの中にいる場合
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "entry")
                        {
                            inEntry = false;
                            alChannels.Add(channel);
                            OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++analyzedCount, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));
                            // 指定の数のヘッドラインを取得し終わったら終了
                            if (setting.FetchChannelNum != Icecast.UserSetting.ALL_CHANNEL_FETCH && analyzedCount >= setting.FetchChannelNum)
                            {
                                break;
                            }
                        }
                    }
                }

                OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(analyzedCount, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));

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
        /// 設定を保存していたファイルを削除する
        /// </summary>
        public virtual void DeleteUserSettingFile()
        {
            setting.DeleteUserSettingFile();
        }
    }
}
