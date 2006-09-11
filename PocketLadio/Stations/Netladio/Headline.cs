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
    /// �˂Ƃ炶�̃w�b�h���C��
    /// </summary>
    public class Headline : PocketLadio.Stations.Interface.IHeadline
    {
        /// <summary>
        /// �w�b�h���C���̎��
        /// </summary>
        private const string KindName = "�˂Ƃ炶";

        /// <summary>
        /// �w�b�h���C����ID�i�w�b�h���C�������ʂ��邽�߂̃L�[�j
        /// </summary>
        private readonly string ID;

        /// <summary>
        /// �w�b�h���C���̐ݒ�
        /// </summary>
        private UserSetting Setting;

        /// <summary>
        /// �ԑg�̃��X�g
        /// </summary>
        private Chanel[] Chanels = new Chanel[0];

        /// <summary>
        /// �w�b�h���C�����擾��������
        /// </summary>
        private DateTime LastCheckTime = DateTime.MinValue;

        /// <summary>
        /// �w�b�h���C���̃R���X�g���N�^
        /// </summary>
        /// <param name="ID">�w�b�h���C����ID</param>
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
        /// �w�b�h���C����ID��Ԃ�
        /// </summary>
        /// <returns>�w�b�h���C����ID</returns>
        public virtual string GetID()
        {
            return ID;
        }

        /// <summary>
        /// �w�b�h���C���̎�ނ̖��O��Ԃ�
        /// </summary>
        /// <returns>�w�b�h���C���̎�ނ̖��O</returns>
        public virtual string GetKindName()
        {
            return KindName;
        }

        /// <summary>
        /// �擾���Ă���ԑg�̃��X�g��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̃��X�g</returns>
        public virtual IChanel[] GetChanels()
        {
            return Chanels;
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����
        /// </summary>
        public virtual void WebGetHeadline()
        {
            // �������Z�b�g����
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
        /// �w�b�h���C�����l�b�g����擾����������Ԃ��B
        /// ���擾�̏ꍇ��DateTime.MinValue��Ԃ��B
        /// </summary>
        /// <returns>�w�b�h���C�����l�b�g����擾��������</returns>
        public virtual DateTime GetLastCheckTime()
        {
            return LastCheckTime;
        }

        /// <summary>
        /// �w�b�h���C���̐ݒ��Ԃ�
        /// </summary>
        /// <returns>�w�b�h���C���̐ݒ�</returns>
        public UserSetting GetUserSetting()
        {
            return Setting;
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����iCVS�g�p�j
        /// </summary>
        private void WebGetHeadlineCvs()
        {
            Stream St = null;
            StreamReader Sr = null;

            try
            {
                // �`�����l���̃��X�g
                ArrayList AlChanels = new ArrayList();

                St = HeadlineUtil.GetHttpStream(Setting.HeadlineCsvUrl);
                Sr = new StreamReader(St, Encoding.GetEncoding("shift-jis"));
                string HttpString = Sr.ReadToEnd();
                string[] ChanelsCvs = HttpString.Split('\n');

                // 1�s�ڂ̓w�b�_�Ȃ̂Ŗ���
                for (int Count = 1; Count < ChanelsCvs.Length; ++Count)
                {
                    if (ChanelsCvs[Count] != "")
                    {
                        Chanel Chanel = new Chanel(this);
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
        /// �w�b�h���C�����l�b�g����擾����iXML�g�p�j
        /// </summary>
        private void WebGetHeadlineXml()
        {
            Stream St = null;
            XmlTextReader Reader = null;

            try
            {
                // �ԑg�̃��X�g
                ArrayList AlChanels = new ArrayList();

                St = HeadlineUtil.GetHttpStream(Setting.HeadlineXmlUrl);
                Reader = new XmlTextReader(St);

                // �`�����l��
                Chanel Chanel = new Chanel(this);
                // source�^�O�̒��ɂ��邩
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

                        // source�^�O�̒��ɂ���ꍇ
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
        /// �w�b�h���C���̐ݒ�t�H�[����\������
        /// </summary>
        /// <returns>�w�b�h���C���̐ݒ�t�H�[��</returns>
        public virtual void ShowSettingForm()
        {
            SettingForm SettingForm = new SettingForm(Setting);
            SettingForm.ShowDialog();
            SettingForm.Dispose();
        }

        /// <summary>
        /// �ԑg�̏ڍ׃t�H�[����\������
        /// </summary>
        /// <param name="Chanel">�ԑg</param>
        /// <returns>�ԑg�̏ڍ׃t�H�[��</returns>
        public virtual void ShowPropertyForm(IChanel chanel)
        {
            ChanelPropertyForm ChanelPropertyForm = new ChanelPropertyForm((Chanel)chanel);
            ChanelPropertyForm.ShowDialog();
            ChanelPropertyForm.Dispose();
        }

        /// <summary>
        /// �ݒ��ۑ����Ă����t�@�C�����폜����
        /// </summary>
        public virtual void DeleteUserSettingFile()
        {
            Setting.DeleteUserSettingFile();
        }
    }
}
