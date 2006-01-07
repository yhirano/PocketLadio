using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Xml;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// ねとらじのヘッドライン
    /// </summary>
    public class Headline
    {
        // チャンネルのリスト
        private static Chanel[] Chanels = new Chanel[0];

        /// <summary>
        /// シングルトンのためprivate
        /// </summary>
        private Headline()
        {
        }

        /// <summary>
        /// 取得しているチャンネルのリストを返す
        /// </summary>
        /// <returns>チャンネルのリスト</returns>
        public static Chanel[] GetChanels()
        {
            return Chanels;
        }

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        public static void WebGetHeadline()
        {
            try
            {
                if (UserSetting.HeadlineGetType == UserSetting.HeadlineGetTypeEnum.Cvs)
                {
                    WebGetHeadlineCvs();
                }
                else if (UserSetting.HeadlineGetType == UserSetting.HeadlineGetTypeEnum.Xml)
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
        /// ヘッドラインをネットから取得する（CVS使用）
        /// </summary>
        private static void WebGetHeadlineCvs()
        {
            // チャンネルのリスト
            ArrayList AlChanels = new ArrayList();

            try
            {
                WebRequest Req = WebRequest.Create(UserSetting.HeadlineCsvUrl);
                Req.Timeout = 20000;
                WebResponse Result = Req.GetResponse();
                Stream ReceiveStream = Result.GetResponseStream();
                Encoding Encode = Encoding.GetEncoding("shift-jis");
                StreamReader Sr = new StreamReader(ReceiveStream, Encode);
                string HttpString = Sr.ReadToEnd();
                ReceiveStream.Close();
                Sr.Close();
                string[] ChanelsCvs = HttpString.Split('\n');

                // 1行目はヘッダなので無視
                for (int Count = 1; Count < ChanelsCvs.Length; Count++)
                {
                    if (ChanelsCvs[Count] != "")
                    {
                        Chanel Chanel = new Chanel();
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
        }

        /// <summary>
        /// ヘッドラインをネットから取得する（XML使用）
        /// </summary>
        private static void WebGetHeadlineXml()
        {
            // チャンネルのリスト
            ArrayList AlChanels = new ArrayList();

            try
            {
                WebRequest req = WebRequest.Create(UserSetting.HeadlineCsvUrl);
                req.Timeout = 20000;
                WebResponse result = req.GetResponse();
                Stream receiveStream = result.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(receiveStream, encode);
                receiveStream.Close();

                XmlTextReader xtr = new XmlTextReader(sr);

                Chanel Chanel = new Chanel();
                while (xtr.Read())
                {
                    if (xtr.NodeType == XmlNodeType.Element)
                    {
                        if (xtr.LocalName.Equals("source"))
                        {
                            Chanel = new Chanel();
                        } // End of source
                        if (xtr.LocalName.Equals("url"))
                        {
                            Chanel.Url = xtr.Value;
                        } // End of url
                        if (xtr.LocalName.Equals("gnl"))
                        {
                            Chanel.Gnl = xtr.Value;
                        } // End of gnl
                        if (xtr.LocalName.Equals("tit"))
                        {
                            Chanel.Tit = xtr.Value;
                        } // End of tit
                        if (xtr.LocalName.Equals("mnt"))
                        {
                            Chanel.Mnt = xtr.Value;
                        } // End of mnt
                        if (xtr.LocalName.Equals("tim"))
                        {
                            Chanel.Tim = xtr.Value;
                        } // End of tim
                        if (xtr.LocalName.Equals("tims"))
                        {
                            Chanel.Tims = xtr.Value;
                        } // End of tims
                        if (xtr.LocalName.Equals("cln"))
                        {
                            Chanel.Cln = xtr.Value;
                        } // End of cln
                        if (xtr.LocalName.Equals("clns"))
                        {
                            Chanel.Clns = xtr.Value;
                        } // End of clns
                        if (xtr.LocalName.Equals("srv"))
                        {
                            Chanel.Srv = xtr.Value;
                        } // End of srv
                        if (xtr.LocalName.Equals("prt"))
                        {
                            Chanel.Prt = xtr.Value;
                        } // End of prt
                        if (xtr.LocalName.Equals("typ"))
                        {
                            Chanel.Typ = xtr.Value;
                        } // End of typ
                        if (xtr.LocalName.Equals("bit"))
                        {
                            Chanel.Bit = xtr.Value;
                        } // End of bit
                    }
                    else if (xtr.NodeType == XmlNodeType.EndElement)
                    {
                        if (xtr.LocalName.Equals("source"))
                        {
                            AlChanels.Add(Chanel);
                        }
                    }
                }

                xtr.Close();
                sr.Close();

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
        }
    }
}
