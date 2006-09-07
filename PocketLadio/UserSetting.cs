using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using PocketLadio.Util;

namespace PocketLadio
{
    /// <summary>
    /// PocketLadio�̐ݒ��ێ�����N���X
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// �����Đ��p�̃��f�B�A�v���[���[�̃t�@�C���p�X
        /// </summary>
        private static string mediaPlayerPath = "\\Program Files\\TCPMP\\player.exe";

        /// <summary>
        /// �����Đ��p�̃��f�B�A�v���[���[�̃t�@�C���p�X
        /// </summary>
        public static string MediaPlayerPath
        {
            get { return UserSetting.mediaPlayerPath; }
            set { UserSetting.mediaPlayerPath = value; }
        }

        /// <summary>
        /// Web�u���E�U�̃t�@�C���p�X
        /// </summary>
        private static string browserPath = "\\Windows\\iexplore.exe";

        /// <summary>
        /// Web�u���E�U�̃t�@�C���p�X
        /// </summary>
        public static string BrowserPath
        {
            get { return UserSetting.browserPath; }
            set { UserSetting.browserPath = value; }
        }

        /// <summary>
        /// �^�C�}�[�Ń`�F�b�N���邩
        /// </summary>
        private static bool headlineTimerCheck = false;

        /// <summary>
        /// �^�C�}�[�Ń`�F�b�N���邩
        /// </summary>
        public static bool HeadlineTimerCheck
        {
            get { return UserSetting.headlineTimerCheck; }
            set { UserSetting.headlineTimerCheck = value; }
        }

        /// <summary>
        /// �^�C�}�[�̃`�F�b�N����
        /// </summary>
        private static int headlineTimerMillSecond = 60000;

        /// <summary>
        /// �^�C�}�[�̃`�F�b�N����
        /// </summary>
        public static int HeadlineTimerMillSecond
        {
            get { return UserSetting.headlineTimerMillSecond; }
            set
            {
                // �K��Ɏ��܂�ꍇ
                if (PocketLadioInfo.HeadlineCheckTimerMinimumMillSec <= value && value <= PocketLadioInfo.HeadlineCheckTimerMaximumMillSec)
                {
                    UserSetting.headlineTimerMillSecond = value;
                }
                // �K������Z���ꍇ
                else if (value < PocketLadioInfo.HeadlineCheckTimerMinimumMillSec)
                {
                    UserSetting.headlineTimerMillSecond = PocketLadioInfo.HeadlineCheckTimerMinimumMillSec;
                }
                // �K����������ꍇ
                else if (value > PocketLadioInfo.HeadlineCheckTimerMaximumMillSec)
                {
                    UserSetting.headlineTimerMillSecond = PocketLadioInfo.HeadlineCheckTimerMaximumMillSec;
                }
            }
        }

        /// <summary>
        /// �v���L�V���g�p���邩
        /// </summary>
        private static bool proxyUse = false;

        /// <summary>
        /// �v���L�V���g�p���邩
        /// </summary>
        public static bool ProxyUse
        {
            get { return UserSetting.proxyUse; }
            set { UserSetting.proxyUse = value; }
        }

        /// <summary>
        /// �v���L�V�̃T�[�o��
        /// </summary>
        private static string proxyServer = "";

        /// <summary>
        /// �v���L�V�̃T�[�o��
        /// </summary>
        public static string ProxyServer
        {
            get { return proxyServer; }
            set { proxyServer = value; }
        }

        /// <summary>
        /// �v���L�V�̃|�[�g�ԍ�
        /// </summary>
        private static string proxyPort = "";

        /// <summary>
        /// �v���L�V�̃|�[�g�ԍ�
        /// </summary>
        public static string ProxyPort
        {
            get
            {
                try
                {
                    if (0x00 <= int.Parse(proxyPort) && int.Parse(proxyPort) <= 0xFFFF)
                    {
                        return proxyPort;
                    }
                    else {
                        return "";
                    }
                }
                catch (ArgumentException)
                {
                    return "";
                }
                catch (FormatException)
                {
                    return "";
                }
                catch (OverflowException)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    if (0x00 <= int.Parse(value) && int.Parse(value) <= 0xFFFF)
                    {
                        proxyPort = value;
                    }
                }
                catch (ArgumentException)
                {
                    ;
                }
                catch (FormatException)
                {
                    ;
                }
                catch (OverflowException)
                {
                    ;
                }
            }

        }

        /// <summary>
        /// �����t�B���^�[
        /// </summary>
        private static string[] filterWords = new string[0];

        /// <summary>
        /// �����t�B���^�[
        /// </summary>
        public static string[] FilterWords
        {
            get { return UserSetting.filterWords; }
            set { UserSetting.filterWords = value; }
        }

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C��
        /// </summary>
        private const string settingPath = "Setting.xml";

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C��
        /// </summary>
        private static string SettingPath
        {
            get { return settingPath; }
        }

        /// <summary>
        /// �V���O���g���̂��߃v���C�x�[�g
        /// </summary>
        private UserSetting()
        {
        }

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C���̕ۑ��ꏊ��Ԃ�
        /// </summary>
        /// <returns>�A�v���P�[�V�����̐ݒ�t�@�C���̕ۑ��ꏊ</returns>
        public static string GetSettingPath()
        {
            // �A�v���P�[�V�����̎��s�f�B���N�g�� + �A�v���P�[�V�����̐ݒ�t�@�C��
            return PocketLadioUtil.GetExecutablePath() + "\\" + SettingPath;
        }

        /// <summary>
        /// �ݒ���t�@�C������ǂݍ���
        /// </summary>
        public static void LoadSetting()
        {
            if (File.Exists(GetSettingPath()))
            {
                FileStream Fs = null;
                XmlTextReader Reader = null;

                try
                {
                    Fs = new FileStream(GetSettingPath(), FileMode.Open, FileAccess.Read);
                    Reader = new XmlTextReader(Fs);

                    ArrayList AlFilterWords = new ArrayList();
                    ArrayList AlStation = new ArrayList();

                    while (Reader.Read())
                    {
                        if (Reader.NodeType == XmlNodeType.Element)
                        {
                            if (Reader.LocalName.Equals("Station"))
                            {
                                string ID = "";
                                string Name = "";
                                Station.StationKindEnum StationKind = Station.StationKindEnum.Netladio;

                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("id"))
                                        {
                                            ID = Reader.Value;
                                        }
                                        else if (Reader.Name.Equals("name"))
                                        {
                                            Name = Reader.Value;
                                        }
                                        else if (Reader.Name.Equals("kind"))
                                        {
                                            if (Reader.Value.Equals(Station.StationKindEnum.Netladio.ToString()))
                                            {
                                                StationKind = Station.StationKindEnum.Netladio;
                                            }
                                            else if (Reader.Value.Equals(Station.StationKindEnum.RssPodcast.ToString()))
                                            {
                                                StationKind = Station.StationKindEnum.RssPodcast;
                                            }
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }

                                AlStation.Add(new Station(ID, Name, StationKind));
                            } // End of Station

                            if (Reader.LocalName.Equals("MediaPlayerPath"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("path"))
                                        {
                                            MediaPlayerPath = Reader.Value;
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of MediaPlayerPath

                            if (Reader.LocalName.Equals("BrowserPath"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("path"))
                                        {
                                            BrowserPath = Reader.Value;
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of BrowserPath

                            if (Reader.LocalName.Equals("Proxy"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("use"))
                                        {
                                            if (Reader.Value.Equals(bool.TrueString))
                                            {
                                                ProxyUse = true;
                                            }
                                            else if (Reader.Value.Equals(bool.FalseString))
                                            {
                                                ProxyUse = false;
                                            }
                                        }
                                        else if (Reader.Name.Equals("server"))
                                        {
                                            ProxyServer = Reader.Value;
                                        }
                                        else if (Reader.Name.Equals("port"))
                                        {
                                            ProxyPort = Reader.Value;
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of Proxy

                            if (Reader.LocalName.Equals("HeadlineTimer"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("check"))
                                        {
                                            if (Reader.Value.Equals(bool.TrueString))
                                            {
                                                HeadlineTimerCheck = true;
                                            }
                                            else if (Reader.Value.Equals(bool.FalseString))
                                            {
                                                HeadlineTimerCheck = false;
                                            }
                                        }
                                        else if (Reader.Name.Equals("millsecond"))
                                        {
                                            try
                                            {
                                                HeadlineTimerMillSecond = Convert.ToInt32(Reader.Value);
                                            }
                                            catch (ArgumentException)
                                            {
                                                ;
                                            }
                                            catch (FormatException)
                                            {
                                                ;
                                            }
                                            catch (OverflowException)
                                            {
                                                ;
                                            }
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of HeadlineTimer

                            if (Reader.LocalName.Equals("Filter"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("word"))
                                        {
                                            AlFilterWords.Add(Reader.Value);
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of Filter

                        }
                        else if (Reader.NodeType == XmlNodeType.EndElement)
                        {
                            if (Reader.LocalName.Equals("StationList"))
                            {
                                StationList.SetStationList((Station[])AlStation.ToArray(typeof(Station)));
                            }
                            else if (Reader.LocalName.Equals("FilterWords"))
                            {
                                FilterWords = (string[])AlFilterWords.ToArray(typeof(string));
                            }
                        }
                    }
                }
                catch (XmlException)
                {
                    throw;
                }
                catch (IOException)
                {
                    throw;
                }
                finally
                {
                    Reader.Close();
                    Fs.Close();
                }
            }
        }

        /// <summary>
        /// �ݒ���t�@�C���ɕۑ�
        /// </summary>
        public static void SaveSetting()
        {
            FileStream Fs = null;
            XmlTextWriter Writer = null;

            try
            {
                Fs = new FileStream(GetSettingPath(), FileMode.Create, FileAccess.Write);
                Writer = new XmlTextWriter(Fs, Encoding.GetEncoding("utf-8"));

                Writer.Formatting = Formatting.Indented;
                Writer.WriteStartDocument(true);

                Writer.WriteStartElement("Setting");

                Writer.WriteStartElement("Header");

                Writer.WriteStartElement("Name");
                Writer.WriteAttributeString("name", PocketLadioInfo.ApplicationName);
                Writer.WriteEndElement(); // End of Name.
                Writer.WriteStartElement("Version");
                Writer.WriteAttributeString("version", PocketLadioInfo.VersionNumber);
                Writer.WriteEndElement(); // End of Version.

                Writer.WriteStartElement("Date");
                Writer.WriteAttributeString("date", DateTime.Now.ToString());
                Writer.WriteEndElement(); // End of Date.

                Writer.WriteEndElement(); // End of Header.

                Writer.WriteStartElement("Content");

                Writer.WriteStartElement("StationList");
                foreach (Station Station in StationList.GetStationList())
                {
                    Writer.WriteStartElement("Station");
                    Writer.WriteAttributeString("id", Station.GetHeadlineID());
                    Writer.WriteAttributeString("name", Station.GetName());
                    Writer.WriteAttributeString("kind", Station.GetStationKind().ToString());
                    Writer.WriteEndElement(); // End of Station
                }
                Writer.WriteEndElement(); // End of StationList

                Writer.WriteStartElement("MediaPlayerPath");
                Writer.WriteAttributeString("path", MediaPlayerPath);
                Writer.WriteEndElement(); // End of MediaPlayerPath

                Writer.WriteStartElement("BrowserPath");
                Writer.WriteAttributeString("path", BrowserPath);
                Writer.WriteEndElement(); // End of BrowserPath

                Writer.WriteStartElement("Proxy");
                Writer.WriteAttributeString("use", ProxyUse.ToString());
                Writer.WriteAttributeString("server", ProxyServer);
                Writer.WriteAttributeString("port", ProxyPort);
                Writer.WriteEndElement(); // End of Porxy

                Writer.WriteStartElement("HeadlineTimer");
                Writer.WriteAttributeString("check", HeadlineTimerCheck.ToString());
                Writer.WriteAttributeString("millsecond", HeadlineTimerMillSecond.ToString());
                Writer.WriteEndElement(); // End of HeadlineTimer

                Writer.WriteStartElement("FilterWords");
                foreach (string FilterWord in FilterWords)
                {
                    Writer.WriteStartElement("Filter");
                    Writer.WriteAttributeString("word", FilterWord);
                    Writer.WriteEndElement(); // End of Filter
                }
                Writer.WriteEndElement(); // End of FilterWords

                Writer.WriteEndElement(); // End of Content.

                Writer.WriteEndElement(); // End of Setting.

                Writer.WriteEndDocument();
            }
            catch (IOException)
            {
                throw;
            }
            finally
            {
                Writer.Close();
                Fs.Close();
            }
        }
    }
}
