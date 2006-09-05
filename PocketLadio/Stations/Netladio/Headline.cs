using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Xml;
using PocketLadio.Stations.Interface;
using PocketLadio.Stations.Util;

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// ねとらじのヘッドライン
    /// </summary>
    public class Headline : PocketLadio.Stations.Interface.IHeadline
    {
        /// <summary>
        /// ヘッドラインの種類
        /// </summary>
        private const string KindName = "ねとらじ";

        /// <summary>
        /// ヘッドラインのID（ヘッドラインを識別するためのキー）
        /// </summary>
        private readonly string ID;

        /// <summary>
        /// ヘッドラインの設定
        /// </summary>
        private UserSetting Setting;

        /// <summary>
        /// 番組のリスト
        /// </summary>
        private Chanel[] Chanels = new Chanel[0];

        /// <summary>
        /// ヘッドラインを取得した時間
        /// </summary>
        private DateTime LastCheckTime = DateTime.MinValue;

        /// <summary>
        /// ヘッドラインのコンストラクタ
        /// </summary>
        /// <param name="ID">ヘッドラインのID</param>
        public Headline(string id)
        {
            this.ID = id;
            Setting = new UserSetting(this);

            try
            {
                Setting.LoadSetting();
            }
            catch (XmlException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
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

            try
            {
                if (Setting.HeadlineGetType == UserSetting.HeadlineGetTypeEnum.Cvs)
                {
                    WebGetHeadlineCvs();
                }
                else if (Setting.HeadlineGetType == UserSetting.HeadlineGetTypeEnum.Xml)
                {
                    WebGetHeadlineXml();
                }
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
        /// ヘッドラインの設定を返す
        /// </summary>
        /// <returns>ヘッドラインの設定</returns>
        public UserSetting GetUserSetting()
        {
            return Setting;
        }

        /// <summary>
        /// ヘッドラインをネットから取得する（CVS使用）
        /// </summary>
        private void WebGetHeadlineCvs()
        {
            Stream St = null;
            StreamReader Sr = null;

            try
            {
                // チャンネルのリスト
                ArrayList AlChanels = new ArrayList();

                St = HeadlineUtil.GetHttpStream(Setting.HeadlineCsvUrl);
                Sr = new StreamReader(St, Encoding.GetEncoding("shift-jis"));
                string HttpString = Sr.ReadToEnd();
                string[] ChanelsCvs = HttpString.Split('\n');

                // 1行目はヘッダなので無視
                for (int Count = 1; Count < ChanelsCvs.Length; ++Count)
                {
                    if (ChanelsCvs[Count] != "")
                    {
                        Chanel Chanel = new Chanel(this);
                        string[] ChanelCsv = ChanelsCvs[Count].Split(',');

                        // Url取得
                        Chanel.Url = ChanelCsv[0];

                        // Gnl取得
                        Chanel.Gnl = ChanelCsv[1];

                        // Nam取得
                        Chanel.Nam = ChanelCsv[2];

                        // Tit取得
                        Chanel.Tit = ChanelCsv[3];

                        // Mnt取得
                        Chanel.Mnt = ChanelCsv[4];

                        // Tim取得
                        Chanel.Tim = ChanelCsv[5];

                        // Tims取得
                        Chanel.Tims = ChanelCsv[6];

                        // Cln取得
                        Chanel.Cln = ChanelCsv[7];

                        // Clns取得
                        Chanel.Clns = ChanelCsv[8];

                        // Srv取得
                        Chanel.Srv = ChanelCsv[9];

                        // Prt取得
                        Chanel.Prt = ChanelCsv[10];

                        if (ChanelCsv.Length >= 12)
                        {
                            // Typ取得
                            Chanel.Typ = ChanelCsv[11];
                        }

                        if (ChanelCsv.Length >= 13)
                        {
                            // Bit取得
                            Chanel.Bit = ChanelCsv[12];
                        }

                        AlChanels.Add(Chanel);
                    }
                }

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
            catch (UriFormatException ex)
            {
                throw ex;
            }
            catch (NotSupportedException ex)
            {
                throw ex;
            }
            finally
            {
                if (St != null)
                {
                    St.Close();
                }
                if (Sr != null)
                {
                    Sr.Close();
                }
            }
        }

        /// <summary>
        /// ヘッドラインをネットから取得する（XML使用）
        /// </summary>
        private void WebGetHeadlineXml()
        {
            Stream Sr = null;
            XmlTextReader Reader = null;

            try
            {
                // 番組のリスト
                ArrayList AlChanels = new ArrayList();

                Sr = HeadlineUtil.GetHttpStream(Setting.HeadlineXmlUrl);
                Reader = new XmlTextReader(Sr);

                Chanel Chanel = new Chanel(this);
                while (Reader.Read())
                {
                    if (Reader.NodeType == XmlNodeType.Element)
                    {
                        if (Reader.LocalName.Equals("source"))
                        {
                            Chanel = new Chanel(this);
                        } // End of source
                        if (Reader.LocalName.Equals("url"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("url")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Url = Reader.Value;
                                }
                            }
                        } // End of url
                        if (Reader.LocalName.Equals("gnl"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("gnl")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Gnl = Reader.Value;
                                }
                            }
                        } // End of gnl
                        if (Reader.LocalName.Equals("tit"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("tit")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Tit = Reader.Value;
                                }
                            }
                        } // End of tit
                        if (Reader.LocalName.Equals("mnt"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("mnt")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Mnt = Reader.Value;
                                }
                            }
                        } // End of mnt
                        if (Reader.LocalName.Equals("tim"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("tim")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Tim = Reader.Value;
                                }
                            }
                        } // End of tim
                        if (Reader.LocalName.Equals("tims"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("tims")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Tims = Reader.Value;
                                }
                            }
                        } // End of tims
                        if (Reader.LocalName.Equals("cln"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("cln")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Cln = Reader.Value;
                                }
                            }
                        } // End of cln
                        if (Reader.LocalName.Equals("clns"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("clns")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Clns = Reader.Value;
                                }
                            }
                        } // End of clns
                        if (Reader.LocalName.Equals("srv"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("srv")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Srv = Reader.Value;
                                }
                            }
                        } // End of srv
                        if (Reader.LocalName.Equals("prt"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("prt")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Prt = Reader.Value;
                                }
                            }
                        } // End of prt
                        if (Reader.LocalName.Equals("typ"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("typ")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Typ = Reader.Value;
                                }
                            }
                        } // End of typ
                        if (Reader.LocalName.Equals("bit"))
                        {
                            while (!(Reader.NodeType == XmlNodeType.EndElement && Reader.LocalName.Equals("bit")))
                            {
                                Reader.Read();
                                if (Reader.NodeType == XmlNodeType.Text)
                                {
                                    Chanel.Bit = Reader.Value;
                                }
                            }
                        } // End of bit
                    }
                    else if (Reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (Reader.LocalName.Equals("source"))
                        {
                            AlChanels.Add(Chanel);
                        }
                    }
                }

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
            catch (UriFormatException ex)
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
            finally
            {
                if (Sr != null)
                {
                    Sr.Close();
                }
                if (Reader != null)
                {
                    Reader.Close();
                }
            }
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
        /// 設定を保存していたファイルを削除する
        /// </summary>
        public virtual void DeleteUserSettingFile()
        {
            Setting.DeleteUserSettingFile();
        }
    }
}
