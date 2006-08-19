using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Xml;
using PocketLadio.StationInterface;

namespace PocketLadio.RssPodcast
{
    public class Headline : PocketLadio.StationInterface.IHeadline
    {
        /// <summary>
        /// ヘッドラインの種類
        /// </summary>
        private const string KindName = "Podcast";

        /// <summary>
        /// ヘッドラインのID（ヘッドラインを識別するためのキー）
        /// </summary>
        private string ID;

        /// <summary>
        /// ヘッドラインの設定
        /// </summary>
        private UserSetting Setting;

        /// <summary>
        /// チャンネルのリスト
        /// </summary>
        private Chanel[] Chanels = new Chanel[0];

        /// <summary>
        /// ヘッドラインを取得した時間
        /// </summary>
        private DateTime LastCheckTime = DateTime.MinValue;

        public Headline(string id)
        {
            this.ID = id;
            Setting = new UserSetting(this);
            Setting.LoadSetting();
        }

        /// <summary>
        /// ヘッドラインのIDを返す
        /// </summary>
        /// <returns>ヘッドラインのID</returns>
        public virtual string GetID()
        {
            return ID;
        }

        /// <summary>
        /// ヘッドラインの種類の名前を返す
        /// </summary>
        /// <returns>ヘッドラインの種類の名前</returns>
        public virtual string GetKindName()
        {
            return KindName;
        }

        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public virtual IChanel[] GetChanels()
        {
            return Chanels;
        }

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        public virtual void WebGetHeadline()
        {
            // 時刻をセットする
            LastCheckTime = DateTime.Now;

            // 番組のリスト
            ArrayList AlChanels = new ArrayList();
            try
            {
                // itemタグの中にいるか
                bool InItemFlag = false;
                XmlTextReader Reader = new XmlTextReader(Setting.RssUrl);

                Chanel Chanel = new Chanel(this);
                while (Reader.Read())
                {
                    if (Reader.NodeType == XmlNodeType.Element)
                    {
                        if (Reader.LocalName.Equals("item"))
                        {
                            InItemFlag = true;
                            Chanel = new Chanel(this);
                        } // End of item

                        // itemタグの中にいる場合
                        if (InItemFlag == true)
                        {
                            if (Reader.LocalName.Equals("title"))
                            {
                                while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("title")))
                                {
                                    Reader.Read();
                                    if (Reader.NodeType == XmlNodeType.Text)
                                    {
                                        Chanel.Title = Reader.Value;
                                    }
                                }
                            } // End of title
                            if (Reader.LocalName.Equals("description"))
                            {
                                while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("description")))
                                {
                                    Reader.Read();
                                    if (Reader.NodeType == XmlNodeType.Text)
                                    {
                                        Chanel.Description = Reader.Value;
                                    }
                                }
                            } // End of description
                            if (Reader.LocalName.Equals("link"))
                            {
                                while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("link")))
                                {
                                    Reader.Read();
                                    if (Reader.NodeType == XmlNodeType.Text)
                                    {
                                        Chanel.Link = Reader.Value;
                                    }
                                }
                            } // End of link
                            if (Reader.LocalName.Equals("pubDate"))
                            {
                                while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("pubDate")))
                                {
                                    Reader.Read();
                                    if (Reader.NodeType == XmlNodeType.Text)
                                    {
                                        Chanel.Date = Reader.Value;
                                    }
                                }
                            } // End of pubDate
                            if (Reader.LocalName.Equals("category"))
                            {
                                while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("category")))
                                {
                                    Reader.Read();
                                    if (Reader.NodeType == XmlNodeType.Text)
                                    {
                                        Chanel.Category = Reader.Value;
                                    }
                                }
                            } // End of category
                            if (Reader.LocalName.Equals("author"))
                            {
                                while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("author")))
                                {
                                    Reader.Read();
                                    if (Reader.NodeType == XmlNodeType.Text)
                                    {
                                        Chanel.Author = Reader.Value;
                                    }
                                }
                            } // End of author
                            if (Reader.LocalName.Equals("guid"))
                            {
                                while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("guid")))
                                {
                                    Reader.Read();
                                    if (Reader.NodeType == XmlNodeType.Text)
                                    {
                                        Chanel.Link = Reader.Value;
                                    }
                                }
                            } // End of guid
                            if (Reader.LocalName.Equals("enclosure"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("url"))
                                        {
                                            Chanel.Url = Reader.Value;
                                        }
                                        else if (Reader.Name.Equals("length"))
                                        {
                                            Chanel.Length = Reader.Value;
                                        }
                                        else if (Reader.Name.Equals("type"))
                                        {
                                            Chanel.Type = Reader.Value;
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of enclosure
                        }
                    }
                    else if (Reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (Reader.LocalName.Equals("item"))
                        {
                            InItemFlag = false;
                            AlChanels.Add(Chanel);
                        }
                    }
                }

                Reader.Close();

                Chanels = (Chanel[])AlChanels.ToArray(typeof(Chanel));
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (OutOfMemoryException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (XmlException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// ヘッドラインをネットから取得した時刻を返す。
        /// 未取得の場合はDateTime.MinValueを返す。
        /// </summary>
        /// <returns>ヘッドラインをネットから取得した時刻</returns>
        public virtual DateTime GetLastCheckTime()
        {
            return LastCheckTime;
        }

        /// <summary>
        /// ヘッドラインの設定フォームを表示する
        /// </summary>
        /// <returns>ヘッドラインの設定フォーム</returns>
        public virtual void ShowSettingForm()
        {
            SettingForm SettingForm = new SettingForm(Setting);
            SettingForm.ShowDialog();
            SettingForm.Dispose();
        }

        /// <summary>
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="Chanel">番組</param>
        /// <returns>番組の詳細フォーム</returns>
        public virtual void ShowPropertyForm(IChanel chanel)
        {
            ChanelPropertyForm ChanelPropertyForm = new ChanelPropertyForm((Chanel)chanel);
            ChanelPropertyForm.ShowDialog();
            ChanelPropertyForm.Dispose();
        }

        /// <summary>
        /// ヘッドラインの設定を返す
        /// </summary>
        /// <returns>ヘッドラインの設定</returns>
        public UserSetting GetUserSetting()
        {
            return Setting;
        }
    }
}
