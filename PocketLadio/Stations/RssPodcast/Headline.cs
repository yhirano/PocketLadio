﻿#region ディレクティブを使用する

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
        /// PodcastのMIMEタイプの優先度テーブルを初期化する。
        /// </summary>
        public static void StartUpInitialize()
        {
            try
            {
                RssPodcastMimePriority.Initialize();
            }
            catch (ArgumentNullException) {
                throw;
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
        /// フィルタリングした番組の結果を返す
        /// </summary>
        /// <returns>フィルタリングした番組のリスト</returns>
        public virtual IChannel[] GetChannelsFiltered()
        {
            // 単語フィルタが存在する場合
            if (setting.GetFilterWords().Length > 0)
            {
                ArrayList alChannels = new ArrayList();

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
                return GetChannels();
            }
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
                Channel channel = new Channel(this);
                // itemタグの中にいるか
                bool inItemFlag = false;

                st = PocketLadioUtility.GetWebStream(setting.RssUrl);

                reader = new XmlTextReader(st);

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
                                string enclosureLength = "";
                                string enclosureType = "";

                                try
                                {
                                    if (reader.MoveToFirstAttribute())
                                    {
                                        enclosureUrl = new Uri(reader.GetAttribute("url"));
                                        enclosureLength = reader.GetAttribute("length");
                                        enclosureType = reader.GetAttribute("type");
                                    }

                                    // エンクロージャー要素追加
                                    channel.SetEnclosure(enclosureUrl, enclosureLength, enclosureType);
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
