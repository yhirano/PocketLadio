using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
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
            Stream St = null;
            XmlTextReader Reader = null;

            try
            {
                // 番組のリスト
                ArrayList AlChanels = new ArrayList();

                St = HeadlineUtil.GetHttpStream(Setting.HeadlineXmlUrl);
                Reader = new XmlTextReader(St);

                // チャンネル
                Chanel Chanel = new Chanel(this);
                // sourceタグの中にいるか
                bool InSourceFlag = false;

                while (Reader.Read())
                {
                    if (Reader.NodeType == XmlNodeType.Element)
                    {
                        if (Reader.LocalName.Equals("source"))
                        {
                            InSourceFlag = true;
                            Chanel = new Chanel(this);
                        } // End of source

                        // sourceタグの中にいる場合
                        if (InSourceFlag == true)
                        {
                            if (Reader.LocalName == "url")
                            {
                                Chanel.Url = Reader.ReadString();
                            } // End of url
                            else if (Reader.LocalName == "gnl")
                            {
                                Chanel.Gnl = Reader.ReadString();
                            } // End of gnl
                            else if (Reader.LocalName == "nam")
                            {
                                Chanel.Nam = Reader.ReadString();
                            } // End of nam
                            else if (Reader.LocalName == "tit")
                            {
                                Chanel.Tit = Reader.ReadString();
                            } // End of tit
                            else if (Reader.LocalName == "mnt")
                            {
                                Chanel.Mnt = Reader.ReadString();
                            } // End of mnt
                            else if (Reader.LocalName == "tim")
                            {
                                Chanel.Tim = Reader.ReadString();
                            } // End of tim
                            else if (Reader.LocalName == "tims")
                            {
                                Chanel.Tims = Reader.ReadString();
                            } // End of tims
                            else if (Reader.LocalName == "cln")
                            {
                                Chanel.Cln = Reader.ReadString();
                            } // End of cln
                            else if (Reader.LocalName == "clns")
                            {
                                Chanel.Clns = Reader.ReadString();
                            } // End of clns
                            else if (Reader.LocalName == "srv")
                            {
                                Chanel.Srv = Reader.ReadString();
                            } // End of srv
                            else if (Reader.LocalName == "prt")
                            {
                                Chanel.Prt = Reader.ReadString();
                            } // End of prt
                            else if (Reader.LocalName == "typ")
                            {
                                Chanel.Typ = Reader.ReadString();
                            } // End of typ
                            else if (Reader.LocalName == "bit")
                            {
                                Chanel.Bit = Reader.ReadString();
                            } // End of bit
                        }
                    }
                    else if (Reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (Reader.LocalName == "source")
                        {
                            InSourceFlag = false;
                            AlChanels.Add(Chanel);
                        }
                    }
                }

                Chanels = (Chanel[])AlChanels.ToArray(typeof(Chanel));
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
                if (St != null)
                {
                    St.Close();
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
