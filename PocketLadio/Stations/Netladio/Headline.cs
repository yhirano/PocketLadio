#region �f�B���N�e�B�u���g�p����

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Xml;
using PocketLadio.Stations;

#endregion

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̃w�b�h���C��
    /// </summary>
    public class Headline : PocketLadio.Stations.IHeadline
    {
        /// <summary>
        /// �w�b�h���C���̎��
        /// </summary>
        private const string KIND_NAME = "�˂Ƃ炶";

        /// <summary>
        /// �w�b�h���C����ID�i�w�b�h���C�������ʂ��邽�߂̃L�[�j
        /// </summary>
        private readonly string id;

        /// <summary>
        /// �w�b�h���C���̐ݒ�
        /// </summary>
        private UserSetting setting;

        /// <summary>
        /// �ԑg�̃��X�g
        /// </summary>
        private Channel[] channels = new Channel[0];

        /// <summary>
        /// �w�b�h���C�����擾��������
        /// </summary>
        private DateTime lastCheckTime = DateTime.MinValue;

        /// <summary>
        /// �ԑg�̕\�����@�ݒ�
        /// </summary>
        public string HeadlineViewType
        {
            get { return setting.HeadlineViewType; }
        }

        /// <summary>
        /// �e������
        /// </summary>
        private readonly Station parentStation;

        /// <summary>
        /// �e������
        /// </summary>
        public virtual Station ParentStation
        {
            get { return parentStation; }
        }

        /// <summary>
        /// �w�b�h���C���̃R���X�g���N�^
        /// </summary>
        /// <param name="id">�w�b�h���C����ID</param>
        /// <param name="parentStation">�e������</param>
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
        /// �N�����̏��������\�b�h�B�������Ȃ��B
        /// </summary>
        public static void StartUpInitialize()
        {
            ;
        }

        /// <summary>
        /// �w�b�h���C����ID��Ԃ�
        /// </summary>
        /// <returns>�w�b�h���C����ID</returns>
        public virtual string GetId()
        {
            return id;
        }

        /// <summary>
        /// �w�b�h���C���̎�ނ̖��O��Ԃ�
        /// </summary>
        /// <returns>�w�b�h���C���̎�ނ̖��O</returns>
        public virtual string GetKindName()
        {
            return KIND_NAME;
        }

        /// <summary>
        /// �擾���Ă���ԑg�̃��X�g��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̃��X�g</returns>
        public virtual IChannel[] GetChannels()
        {
            return channels;
        }

        /// <summary>
        /// �t�B���^�����O�����ԑg�̌��ʂ�Ԃ�
        /// </summary>
        /// <returns>�t�B���^�����O�����ԑg�̃��X�g</returns>
        public virtual IChannel[] GetChannelsFiltered()
        {
            // �t�B���^�����݂���ꍇ
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
            // �t�B���^�����݂��Ȃ��ꍇ
            else
            {
                return GetChannels();
            }
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����
        /// </summary>
        public virtual void FetchHeadline()
        {
            // �������Z�b�g����
            lastCheckTime = DateTime.Now;

            try
            {
                if (setting.HeadlineGetWay == UserSetting.HeadlineGetType.Cvs)
                {
                    FetchHeadlineCvs();
                }
                else if (setting.HeadlineGetWay == UserSetting.HeadlineGetType.Xml)
                {
                    FetchHeadlineXml();
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
            return lastCheckTime;
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����iCVS�g�p�j
        /// </summary>
        private void FetchHeadlineCvs()
        {
            Stream st = null;
            StreamReader sr = null;

            try
            {
                // �`�����l���̃��X�g
                ArrayList alChannels = new ArrayList();

                st = PocketLadioUtility.GetWebStream(setting.HeadlineCsvUrl);

                sr = new StreamReader(st, Encoding.GetEncoding("shift-jis"));
                string httpString = sr.ReadToEnd();
                string[] channelsCvs = httpString.Split('\n');

                // 1�s�ڂ̓w�b�_�Ȃ̂Ŗ���
                for (int count = 1; count < channelsCvs.Length; ++count)
                {
                    if (channelsCvs[count].Length != 0)
                    {
                        Channel channel = new Channel(this);
                        string[] channelCsv = channelsCvs[count].Split(',');

                        // Url�擾
                        try
                        {
                            channel.Url = new Uri(channelCsv[0]);
                        }
                        catch (UriFormatException)
                        {
                            ;
                        }
                        // PC��ŋN���邱�Ƃ��m�F�������A�Ώ�����ׂ���������Ȃ��̂łƂ肠��������
                        catch (IndexOutOfRangeException)
                        {
                            ;
                        }

                        // Gnl�擾
                        channel.Gnl = channelCsv[1];

                        // Nam�擾
                        channel.Nam = channelCsv[2];

                        // Tit�擾
                        channel.Tit = channelCsv[3];

                        // Mnt�擾
                        channel.Mnt = channelCsv[4];

                        // Tim�擾
                        channel.SetTim(channelCsv[5]);

                        // Tims�擾
                        channel.SetTims(channelCsv[6]);

                        // Cln�擾
                        channel.Cln = channelCsv[7];

                        // Clns�擾
                        channel.Clns = channelCsv[8];

                        // Srv�擾
                        channel.Srv = channelCsv[9];

                        // Prt�擾
                        channel.Prt = channelCsv[10];

                        if (channelCsv.Length >= 12)
                        {
                            // Typ�擾
                            channel.Typ = channelCsv[11];
                        }

                        if (channelCsv.Length >= 13)
                        {
                            // Bit�擾
                            channel.Bit = channelCsv[12];
                        }

                        alChannels.Add(channel);
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
                if (st != null)
                {
                    st.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����iXML�g�p�j
        /// </summary>
        private void FetchHeadlineXml()
        {
            Stream st = null;
            XmlTextReader reader = null;

            try
            {
                // �ԑg�̃��X�g
                ArrayList alChannels = new ArrayList();

                st = PocketLadioUtility.GetWebStream(setting.HeadlineXmlUrl);

                reader = new XmlTextReader(st);

                // �`�����l��
                Channel channel = new Channel(this);
                // source�^�O�̒��ɂ��邩
                bool inSourceFlag = false;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "source")
                        {
                            inSourceFlag = true;
                            channel = new Channel(this);
                        } // End of source
                        // source�^�O�̒��ɂ���ꍇ
                        else if (inSourceFlag == true)
                        {
                            if (reader.LocalName == "url")
                            {
                                try
                                {
                                    channel.Url = new Uri(reader.ReadString());
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of url
                            else if (reader.LocalName == "gnl")
                            {
                                channel.Gnl = reader.ReadString();
                            } // End of gnl
                            else if (reader.LocalName == "nam")
                            {
                                channel.Nam = reader.ReadString();
                            } // End of nam
                            else if (reader.LocalName == "tit")
                            {
                                channel.Tit = reader.ReadString();
                            } // End of tit
                            else if (reader.LocalName == "mnt")
                            {
                                channel.Mnt = reader.ReadString();
                            } // End of mnt
                            else if (reader.LocalName == "tim")
                            {
                                channel.SetTim(reader.ReadString());
                            } // End of tim
                            else if (reader.LocalName == "tims")
                            {
                                channel.SetTims(reader.ReadString());
                            } // End of tims
                            else if (reader.LocalName == "cln")
                            {
                                channel.Cln = reader.ReadString();
                            } // End of cln
                            else if (reader.LocalName == "clns")
                            {
                                channel.Clns = reader.ReadString();
                            } // End of clns
                            else if (reader.LocalName == "srv")
                            {
                                channel.Srv = reader.ReadString();
                            } // End of srv
                            else if (reader.LocalName == "prt")
                            {
                                channel.Prt = reader.ReadString();
                            } // End of prt
                            else if (reader.LocalName == "typ")
                            {
                                channel.Typ = reader.ReadString();
                            } // End of typ
                            else if (reader.LocalName == "bit")
                            {
                                channel.Bit = reader.ReadString();
                            } // End of bit
                        } // End of source�^�O�̒��ɂ���ꍇ
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "source")
                        {
                            inSourceFlag = false;
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
        /// �w�b�h���C���̐ݒ�t�H�[����\������
        /// </summary>
        /// <returns>�w�b�h���C���̐ݒ�t�H�[��</returns>
        public virtual void ShowSettingForm()
        {
            SettingForm settingForm = new SettingForm(setting);
            settingForm.ShowDialog();
            settingForm.Dispose();
        }

        /// <summary>
        /// �ݒ��ۑ����Ă����t�@�C�����폜����
        /// </summary>
        public virtual void DeleteUserSettingFile()
        {
            setting.DeleteUserSettingFile();
        }
    }
}
