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
        /// �w�b�h���C�����l�b�g����擾����iXML�g�p�j
        /// </summary>
        private void WebGetHeadlineXml()
        {
            Stream Sr = null;
            XmlTextReader Reader = null;

            try
            {
                // �ԑg�̃��X�g
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
