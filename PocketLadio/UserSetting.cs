using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;

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
        public static string MediaPlayerPath = "\\Program Files\\TCPMP\\player.exe";

        /// <summary>
        /// Web�u���E�U�̃t�@�C���p�X
        /// </summary>
        public static string BrowserPath = "\\Windows\\iexplore.exe";

        /// <summary>
        /// �^�C�}�[�Ń`�F�b�N���邩
        /// </summary>
        public static bool HeadlineTimerCheck = false;

        /// <summary>
        /// �^�C�}�[�̃`�F�b�N����
        /// </summary>
        public static int HeadlineTimerMillSecond = 60000;

        /// <summary>
        /// �����t�B���^�[
        /// </summary>
        public static string[] FilterWords = new string[0];

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C��
        /// </summary>
        private const string SettingPath = "Setting.xml";

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
            return Controller.GetExecutablePath() + "\\" + SettingPath;
        }

        /// <summary>
        /// �ݒ���t�@�C������ǂݍ���
        /// </summary>
        public static void LoadSetting()
        {
            if (File.Exists(GetSettingPath()))
            {
                FileStream Fs = new FileStream(GetSettingPath(), FileMode.Open, FileAccess.Read);
                Encoding Encode = Encoding.GetEncoding("utf-8");
                XmlTextReader Reader = new XmlTextReader(Fs);

                ArrayList AlFilterWords = new ArrayList();
                ArrayList AlStation = new ArrayList();

                try
                {
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
                                                int _HeadlineMillSecond = Convert.ToInt32(Reader.Value);
                                                // �K������Z���ꍇ
                                                if (_HeadlineMillSecond < Controller.HeadlineCheckTimerMinimumMillSec)
                                                {
                                                    HeadlineTimerMillSecond = Controller.HeadlineCheckTimerMinimumMillSec;
                                                }
                                                else
                                                {
                                                    HeadlineTimerMillSecond = _HeadlineMillSecond;
                                                }
                                                // �K����������ꍇ
                                                if (_HeadlineMillSecond > Controller.HeadlineCheckTimerMaximumMillSec)
                                                {
                                                    HeadlineTimerMillSecond = Controller.HeadlineCheckTimerMaximumMillSec;
                                                }
                                                else
                                                {
                                                    HeadlineTimerMillSecond = _HeadlineMillSecond;
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
                    ;
                }
                catch (IOException)
                {
                    ;
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
            FileStream Fs = new FileStream(GetSettingPath(), FileMode.Create, FileAccess.Write);
            Encoding Encode = Encoding.GetEncoding("utf-8");
            XmlTextWriter Writer = new XmlTextWriter(Fs, Encode);

            try
            {
                Writer.Formatting = Formatting.Indented;
                Writer.WriteStartDocument(true);

                Writer.WriteStartElement("Setting");

                Writer.WriteStartElement("Header");

                Writer.WriteStartElement("Name");
                Writer.WriteAttributeString("name", Controller.ApplicationName);
                Writer.WriteEndElement(); // End of Name.
                Writer.WriteStartElement("Version");
                Writer.WriteAttributeString("version", Controller.VersionNumber);
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
                ;
            }
            finally
            {
                Writer.Close();
                Fs.Close();
            }
        }
    }
}
