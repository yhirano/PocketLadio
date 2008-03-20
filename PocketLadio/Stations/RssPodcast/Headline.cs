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

namespace PocketLadio.Stations.RssPodcast
{
    public class Headline : PocketLadio.Stations.IHeadline
    {
        /// <summary>
        /// ヘッドラインの種類
        /// </summary>
        private const string KIND_NAME = "Podcast";

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
        /// PodcastのMIMEタイプの優先度テーブルを初期化する。
        /// </summary>
        public static void StartUpInitialize()
        {
            RssPodcastMimePriority.Initialize();
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
                #region 単語フィルタ処理

                ArrayList alChannels = new ArrayList();

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

            WebStream st = null;
            XmlTextReader reader = null;

            try
            {
                // 番組のリスト
                ArrayList alChannels = new ArrayList();

                // チャンネル
                Channel channel = null;
                // itemタグの中にいるか
                bool inItemFlag = false;

                // Enclosureの一時リスト
                ArrayList alTempEnclosure = new ArrayList();

                st = PocketLadioUtility.GetWebStream(setting.RssUrl);

                reader = new XmlTextReader(st);

                // 解析したヘッドラインの個数
                int analyzedCount = 0;

                OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "item")
                        {
                            inItemFlag = true;
                            channel = new Channel(this);
                        } // End of item
                        // itemタグの中にいる場合
                        else if (inItemFlag == true)
                        {
                            if (reader.LocalName == "title")
                            {
                                channel.Title = reader.ReadString();
                            } // End of title
                            else if (reader.LocalName == "description")
                            {
                                channel.Description = reader.ReadString();
                            } // End of description
                            else if (reader.LocalName == "link")
                            {
                                try
                                {
                                    channel.Link = new Uri(reader.ReadString());
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of link
                            else if (reader.LocalName == "pubDate")
                            {
                                channel.SetDate(reader.ReadString());
                            } // End of pubDate
                            else if (reader.LocalName == "category")
                            {
                                channel.Category = reader.ReadString();
                            } // End of category
                            else if (reader.LocalName == "author")
                            {
                                channel.Author = reader.ReadString();
                            } // End of author
                            else if (reader.LocalName == "guid")
                            {
                                try
                                {
                                    channel.Link = new Uri(reader.ReadString());
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of guid
                            else if (reader.LocalName == "enclosure")
                            {
                                Uri enclosureUrl = null;
                                string enclosureLength = string.Empty;
                                string enclosureType = string.Empty;

                                try
                                {
                                    if (reader.MoveToFirstAttribute())
                                    {
                                        enclosureUrl = new Uri(reader.GetAttribute("url"));
                                        enclosureLength = reader.GetAttribute("length");
                                        enclosureType = reader.GetAttribute("type");
                                    }

                                    if (enclosureLength == null)
                                    {
                                        enclosureLength = string.Empty;
                                    }
                                    if (enclosureType == null)
                                    {
                                        enclosureType = string.Empty;
                                    }

                                    // Enclosureタグの数だけ、 Enclosure一時リストにEnclosureの内容を追加していく
                                    Enclosure enclosure = new Enclosure(enclosureUrl, enclosureLength, enclosureType);
                                    if (enclosure.IsPodcast() == true)
                                    {
                                        alTempEnclosure.Add(enclosure);
                                    }
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of enclosure
                        } // End of itemタグの中にいる場合
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "item")
                        {
                            inItemFlag = false;
                            // Enclosureの要素の数だけ、Channelの複製を作る
                            if (alTempEnclosure.Count != 0)
                            {
                                foreach (Enclosure enclosure in alTempEnclosure)
                                {
                                    Channel clonedChannel = (Channel)channel.Clone(this);
                                    clonedChannel.Url = enclosure.Url;
                                    clonedChannel.Length = enclosure.Length;
                                    clonedChannel.Type = enclosure.Type;
                                    alChannels.Add(clonedChannel);
                                    OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++analyzedCount, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));
                                }
                            }

                            // Enclosure一時リストをクリア
                            alTempEnclosure.Clear();
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

        /// <summary>
        /// RSSのEnclosure要素
        /// </summary>
        private class Enclosure
        {
            Uri url;

            public Uri Url
            {
                get { return url; }
            }

            string length;

            public string Length
            {
                get { return length; }
            }

            string type;

            public string Type
            {
                get { return type; }
            }

            public Enclosure(Uri url, string length, string type)
            {
                this.url = url;
                this.length = length;
                this.type = type;
            }

            /// <summary>
            /// このEnclosure要素は再生可能なPodcastかを判断する
            /// </summary>
            /// <returns>このEnclosure要素は再生可能なPodcastが再生可能な場合はtrue。</returns>
            public bool IsPodcast()
            {
                if (type == null || type == string.Empty)
                {
                    return false;
                }

                return ((RssPodcastMimePriority.GetRssPodcastMimePriority(type) != 0) ? true : false);
            }
        }
    }
}
