#region �f�B���N�e�B�u���g�p����

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Diagnostics;
using MiscPocketCompactLibrary.Reflection;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// PocketLadio�̐ݒ��ێ�����N���X
    /// </summary>
    public sealed class UserSetting
    {
        /// <summary>
        /// �t�B���^�[�ݒ�
        /// </summary>
        private static bool filterEnable;

        /// <summary>
        /// �t�B���^�[�ݒ�
        /// </summary>
        public static bool FilterEnable
        {
            get { return UserSetting.filterEnable; }
            set { UserSetting.filterEnable = value; }
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
        /// �v���C���X�g�͈�[���[�J���ɕۑ����邩
        /// </summary>
        private static bool playListSave = true;

        /// <summary>
        /// �v���C���X�g�͈�[���[�J���ɕۑ����邩
        /// </summary>
        public static bool PlayListSave
        {
            get { return UserSetting.playListSave; }
            set { UserSetting.playListSave = value; }
        }

        /// <summary>
        /// �����Đ��p�̃��f�B�A�v���[���[�̃t�@�C���p�X
        /// </summary>
        private static string mediaPlayerPath = PocketLadioInfo.DefaultMediaPlayerPath;

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
        private static string browserPath = PocketLadioInfo.DefaultBrowserPath;

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
        private static bool headlineTimerCheck;

        /// <summary>
        /// �^�C�}�[�Ń`�F�b�N���邩
        /// </summary>
        public static bool HeadlineTimerCheck
        {
            get { return UserSetting.headlineTimerCheck; }
            set { UserSetting.headlineTimerCheck = value; }
        }

        /// <summary>
        /// �ԑg�\�̃t�H���g�T�C�Y��ύX���邩
        /// </summary>
        private static bool headlineListBoxFontSizeChange;

        /// <summary>
        /// �ԑg�\�̃t�H���g�T�C�Y��ύX���邩
        /// </summary>
        public static bool HeadlineListBoxFontSizeChange
        {
            get { return UserSetting.headlineListBoxFontSizeChange; }
            set { UserSetting.headlineListBoxFontSizeChange = value; }
        }

        /// <summary>
        /// �ԑg�\�̃t�H���g�T�C�Y
        /// </summary>
        private static int headlineListBoxFontSize = PocketLadioInfo.HeadlineListBoxDefaultFontSize;

        /// <summary>
        /// �ԑg�\�̃t�H���g�T�C�Y
        /// </summary>
        public static int HeadlineListBoxFontSize
        {
            get { return UserSetting.headlineListBoxFontSize; }
            set
            {
                // �K��Ɏ��܂�ꍇ
                if (PocketLadioInfo.HeadlineListBoxFontSizeMinimumPt <= value && value <= PocketLadioInfo.HeadlineListBoxFontSizeMaximumPt)
                {
                    UserSetting.headlineListBoxFontSize = value;
                }
                // �K������Z���ꍇ
                else if (value < PocketLadioInfo.HeadlineListBoxFontSizeMinimumPt)
                {
                    UserSetting.headlineListBoxFontSize = PocketLadioInfo.HeadlineListBoxFontSizeMinimumPt;
                }
                // �K����������ꍇ
                else if (value > PocketLadioInfo.HeadlineListBoxFontSizeMaximumPt)
                {
                    UserSetting.headlineListBoxFontSize = PocketLadioInfo.HeadlineListBoxFontSizeMaximumPt;
                }
            }
        }

        /// <summary>
        /// �v���L�V�̐ڑ����@��
        /// </summary>
        public enum ProxyConnect
        {
            Unuse, OsSetting, OriginalSetting
        }

        /// <summary>
        /// �v���L�V�̐ڑ����@
        /// </summary>
        private static ProxyConnect proxyUse = ProxyConnect.OsSetting;

        /// <summary>
        /// �v���L�V�̐ڑ����@
        /// </summary>
        public static ProxyConnect ProxyUse
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
        private static int proxyPort = 0;

        /// <summary>
        /// �v���L�V�̃|�[�g�ԍ�
        /// </summary>
        public static int ProxyPort
        {
            get
            {
                if (0x00 <= proxyPort && proxyPort <= 0xFFFF)
                {
                    return proxyPort;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (0x00 <= value && value <= 0xFFFF)
                {
                    proxyPort = value;
                }
                else
                {
                    ;
                }
            }
        }

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C���̕ۑ��ꏊ
        /// </summary>
        private static string SettingPath
        {
            get
            {
                // �A�v���P�[�V�����̎��s�f�B���N�g�� + �A�v���P�[�V�����̐ݒ�t�@�C��
                return AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.SettingFile;
            }
        }

        /// <summary>
        /// �V���O���g���̂��߃v���C�x�[�g
        /// </summary>
        private UserSetting()
        {
        }

        /// <summary>
        /// �ݒ���t�@�C������ǂݍ���
        /// </summary>
        public static void LoadSetting()
        {
            if (File.Exists(SettingPath))
            {
                FileStream fs = null;
                XmlTextReader reader = null;

                try
                {
                    fs = new FileStream(SettingPath, FileMode.Open, FileAccess.Read);
                    reader = new XmlTextReader(fs);

                    ArrayList alStation = new ArrayList();

                    // StationList�^�O�̒��ɂ��邩
                    bool inStationListFlag = false;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.LocalName == "StationList")
                            {
                                inStationListFlag = true;
                            } // End of StationList
                            // StationList�^�O�̒��ɂ���ꍇ
                            else if (inStationListFlag == true)
                            {
                                if (reader.LocalName == "Station")
                                {
                                    string id = "";
                                    string name = "";
                                    Station.StationKind stationKind = Station.StationKind.Netladio;

                                    if (reader.MoveToFirstAttribute())
                                    {
                                        id = reader.GetAttribute("id");
                                        name = reader.GetAttribute("name");
                                        string kind = reader.GetAttribute("kind");
                                        if (kind == Station.StationKind.Netladio.ToString())
                                        {
                                            stationKind = Station.StationKind.Netladio;
                                        }
                                        else if (kind == Station.StationKind.RssPodcast.ToString())
                                        {
                                            stationKind = Station.StationKind.RssPodcast;
                                        }
                                        else if (kind == Station.StationKind.ShoutCast.ToString())
                                        {
                                            stationKind = Station.StationKind.ShoutCast;
                                        }
                                        else
                                        {
                                            // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                                            Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
                                        }

                                        alStation.Add(new Station(id, name, stationKind));
                                    }
                                } // End of Station
                            } // End of StationList�^�O�̒��ɂ���ꍇ
                            else if (reader.LocalName == "FilterEnable")
                            {
                                string enable;
                                enable = reader.GetAttribute("enable");
                                if (enable == bool.TrueString)
                                {
                                    FilterEnable = true;
                                }
                                else if (enable == bool.FalseString)
                                {
                                    FilterEnable = false;
                                }
                            } // End of FilterEnable
                            else if (reader.LocalName.Equals("HeadlineTimer"))
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    string check = reader.GetAttribute("check");
                                    if (check == bool.TrueString)
                                    {
                                        HeadlineTimerCheck = true;
                                    }
                                    else if (check == bool.FalseString)
                                    {
                                        HeadlineTimerCheck = false;
                                    }
                                    try
                                    {
                                        HeadlineTimerMillSecond = Convert.ToInt32(reader.GetAttribute("millsecond"));
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
                            } // End of HeadlineTimer
                            else if (reader.LocalName == "MediaPlayerPath")
                            {
                                MediaPlayerPath = reader.GetAttribute("path");
                            } // End of MediaPlayerPath
                            else if (reader.LocalName == "BrowserPath")
                            {
                                BrowserPath = reader.GetAttribute("path");
                            } // End of BrowserPath
                            else if (reader.LocalName == "PlayListSave")
                            {
                                string save;
                                save = reader.GetAttribute("save");
                                if (save == bool.TrueString)
                                {
                                    PlayListSave = true;
                                }
                                else if (save == bool.FalseString)
                                {
                                    PlayListSave = false;
                                }
                            } // End of PlayListSave
                            else if (reader.LocalName.Equals("HeadlineListBoxFont"))
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    string check = reader.GetAttribute("change");
                                    if (check == bool.TrueString)
                                    {
                                        HeadlineListBoxFontSizeChange = true;
                                    }
                                    else if (check == bool.FalseString)
                                    {
                                        HeadlineListBoxFontSizeChange = false;
                                    }
                                    try
                                    {
                                        HeadlineListBoxFontSize = Convert.ToInt32(reader.GetAttribute("size"));
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
                            } // End of HeadlineListBoxFont
                            else if (reader.LocalName == "Proxy")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    string use = reader.GetAttribute("use");
                                    if (use == ProxyConnect.Unuse.ToString())
                                    {
                                        ProxyUse = ProxyConnect.Unuse;
                                    }
                                    else if (use == ProxyConnect.OsSetting.ToString())
                                    {
                                        ProxyUse = ProxyConnect.OsSetting;
                                    }
                                    else if (use == ProxyConnect.OriginalSetting.ToString())
                                    {
                                        ProxyUse = ProxyConnect.OriginalSetting;
                                    }

                                    ProxyServer = reader.GetAttribute("server");

                                    try
                                    {
                                        string port = reader.GetAttribute("port");
                                        ProxyPort = int.Parse(port);
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
                            } // End of Proxy
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            if (reader.LocalName == "StationList")
                            {
                                inStationListFlag = false;
                                StationList.SetStationList((Station[])alStation.ToArray(typeof(Station)));
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
                    reader.Close();
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// �ݒ���t�@�C���ɕۑ�
        /// </summary>
        public static void SaveSetting()
        {
            FileStream fs = null;
            XmlTextWriter writer = null;

            try
            {
                fs = new FileStream(SettingPath, FileMode.Create, FileAccess.Write);
                writer = new XmlTextWriter(fs, Encoding.GetEncoding("utf-8"));

                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);

                writer.WriteStartElement("Setting");

                writer.WriteStartElement("Header");

                writer.WriteStartElement("Name");
                writer.WriteAttributeString("name", PocketLadioInfo.ApplicationName);
                writer.WriteEndElement(); // End of Name.
                writer.WriteStartElement("Version");
                writer.WriteAttributeString("version", PocketLadioInfo.VersionNumber);
                writer.WriteEndElement(); // End of Version.

                writer.WriteStartElement("Date");
                writer.WriteAttributeString("date", DateTime.Now.ToString());
                writer.WriteEndElement(); // End of Date.

                writer.WriteEndElement(); // End of Header.

                writer.WriteStartElement("Content");

                writer.WriteStartElement("StationList");
                foreach (Station station in StationList.GetStationList())
                {
                    writer.WriteStartElement("Station");
                    writer.WriteAttributeString("id", station.Id);
                    writer.WriteAttributeString("name", station.Name);
                    writer.WriteAttributeString("kind", station.Kind.ToString());
                    writer.WriteEndElement(); // End of Station
                }
                writer.WriteEndElement(); // End of StationList

                writer.WriteStartElement("FilterEnable");
                writer.WriteAttributeString("enable", FilterEnable.ToString());
                writer.WriteEndElement(); // End of FilterEnable

                writer.WriteStartElement("HeadlineTimer");
                writer.WriteAttributeString("check", HeadlineTimerCheck.ToString());
                writer.WriteAttributeString("millsecond", HeadlineTimerMillSecond.ToString());
                writer.WriteEndElement(); // End of HeadlineTimer

                writer.WriteStartElement("MediaPlayerPath");
                writer.WriteAttributeString("path", MediaPlayerPath);
                writer.WriteEndElement(); // End of MediaPlayerPath

                writer.WriteStartElement("BrowserPath");
                writer.WriteAttributeString("path", BrowserPath);
                writer.WriteEndElement(); // End of BrowserPath

                writer.WriteStartElement("PlayListSave");
                writer.WriteAttributeString("save", PlayListSave.ToString());
                writer.WriteEndElement(); // End of PlayListSave

                writer.WriteStartElement("HeadlineListBoxFont");
                writer.WriteAttributeString("change", HeadlineListBoxFontSizeChange.ToString());
                writer.WriteAttributeString("size", HeadlineListBoxFontSize.ToString());
                writer.WriteEndElement(); // End of HeadlineListBoxFont

                writer.WriteStartElement("Proxy");
                writer.WriteAttributeString("use", ProxyUse.ToString());
                writer.WriteAttributeString("server", ProxyServer);
                writer.WriteAttributeString("port", ProxyPort.ToString());
                writer.WriteEndElement(); // End of Porxy

                writer.WriteEndElement(); // End of Content.

                writer.WriteEndElement(); // End of Setting.

                writer.WriteEndDocument();
            }
            catch (IOException)
            {
                throw;
            }
            finally
            {
                writer.Close();
                fs.Close();
            }
        }
    }
}
