using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Xml;
using PocketLadio.Stations.Interface;
using PocketLadio.Stations.Util;

namespace PocketLadio.Stations.RssPodcast
{
    public class Headline : PocketLadio.Stations.Interface.IHeadline
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
            XmlTextReader reader = null;

            try
            {
                // 番組のリスト
                ArrayList alChannels = new ArrayList();

                // チャンネル
                Channel channel = new Channel(this);
                // itemタグの中にいるか
                bool inItemFlag = false;

                st = HeadlineUtil.GetHttpStream(setting.RssUrl);
                reader = new XmlTextReader(st);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName.Equals("item"))
                        {
                            inItemFlag = true;
                            channel = new Channel(this);
                        } // End of item

                        // itemタグの中にいる場合
                        if (inItemFlag == true)
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
                                channel.Link = reader.ReadString();
                            } // End of link
                            else if (reader.LocalName == "pubDate")
                            {
                                        channel.Date = reader.ReadString();
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
                                channel.Link = reader.ReadString();
                            } // End of guid
                            else if (reader.LocalName == "enclosure")
                            {
                                string enclosureUrl = "";
                                string enclosureLength = "";
                                string enclosureType = "";

                                if (reader.MoveToFirstAttribute())
                                {
                                    enclosureUrl = reader.GetAttribute("url");
                                    enclosureLength = reader.GetAttribute("length");
                                    enclosureType = reader.GetAttribute("type");
                                }

                                // エンクロージャー要素追加
                                channel.SetEnclosure(enclosureUrl, enclosureLength, enclosureType);
                            } // End of enclosure
                        }
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
        /// ヘッドラインの設定を返す
        /// </summary>
        /// <returns>ヘッドラインの設定</returns>
        public UserSetting GetUserSetting()
        {
            return setting;
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
