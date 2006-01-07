using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Xml;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̃w�b�h���C��
    /// </summary>
    public class Headline
    {
        // �`�����l���̃��X�g
        private static Chanel[] Chanels = new Chanel[0];

        /// <summary>
        /// �V���O���g���̂���private
        /// </summary>
        private Headline()
        {
        }

        /// <summary>
        /// �擾���Ă���`�����l���̃��X�g��Ԃ�
        /// </summary>
        /// <returns>�`�����l���̃��X�g</returns>
        public static Chanel[] GetChanels()
        {
            return Chanels;
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����
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
        /// �w�b�h���C�����l�b�g����擾����iCVS�g�p�j
        /// </summary>
        private static void WebGetHeadlineCvs()
        {
            // �`�����l���̃��X�g
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

                // 1�s�ڂ̓w�b�_�Ȃ̂Ŗ���
                for (int Count = 1; Count < ChanelsCvs.Length; Count++)
                {
                    if (ChanelsCvs[Count] != "")
                    {
                        Chanel Chanel = new Chanel();
                        string[] ChanelCsv = ChanelsCvs[Count].Split(',');

                        // Url�擾
                        Chanel.Url = ChanelCsv[0];

                        // Gnl�擾
                        Chanel.Gnl = ChanelCsv[1];

                        // Nam�擾
                        Chanel.Nam = ChanelCsv[2];

                        // Tit�擾
                        Chanel.Tit = ChanelCsv[3];

                        // Mnt�擾
                        Chanel.Mnt = ChanelCsv[4];

                        // Tim�擾
                        Chanel.Tim = ChanelCsv[5];

                        // Tims�擾
                        Chanel.Tims = ChanelCsv[6];

                        // Cln�擾
                        Chanel.Cln = ChanelCsv[7];

                        // Clns�擾
                        Chanel.Clns = ChanelCsv[8];

                        // Srv�擾
                        Chanel.Srv = ChanelCsv[9];

                        // Prt�擾
                        Chanel.Prt = ChanelCsv[10];

                        if (ChanelCsv.Length >= 12)
                        {
                            // Typ�擾
                            Chanel.Typ = ChanelCsv[11];
                        }

                        if (ChanelCsv.Length >= 13)
                        {
                            // Bit�擾
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
        /// �w�b�h���C�����l�b�g����擾����iXML�g�p�j
        /// </summary>
        private static void WebGetHeadlineXml()
        {
            // �`�����l���̃��X�g
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
