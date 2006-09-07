using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using PocketLadio.Util;

namespace PocketLadio
{
    /// <summary>
    /// PocketLadioの設定を保持するクラス
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// 音声再生用のメディアプレーヤーのファイルパス
        /// </summary>
        private static string mediaPlayerPath = "\\Program Files\\TCPMP\\player.exe";

        /// <summary>
        /// 音声再生用のメディアプレーヤーのファイルパス
        /// </summary>
        public static string MediaPlayerPath
        {
            get { return UserSetting.mediaPlayerPath; }
            set { UserSetting.mediaPlayerPath = value; }
        }

        /// <summary>
        /// Webブラウザのファイルパス
        /// </summary>
        private static string browserPath = "\\Windows\\iexplore.exe";

        /// <summary>
        /// Webブラウザのファイルパス
        /// </summary>
        public static string BrowserPath
        {
            get { return UserSetting.browserPath; }
            set { UserSetting.browserPath = value; }
        }

        /// <summary>
        /// タイマーでチェックするか
        /// </summary>
        private static bool headlineTimerCheck = false;

        /// <summary>
        /// タイマーでチェックするか
        /// </summary>
        public static bool HeadlineTimerCheck
        {
            get { return UserSetting.headlineTimerCheck; }
            set { UserSetting.headlineTimerCheck = value; }
        }

        /// <summary>
        /// タイマーのチェック時間
        /// </summary>
        private static int headlineTimerMillSecond = 60000;

        /// <summary>
        /// タイマーのチェック時間
        /// </summary>
        public static int HeadlineTimerMillSecond
        {
            get { return UserSetting.headlineTimerMillSecond; }
            set
            {
                // 規定に収まる場合
                if (PocketLadioInfo.HeadlineCheckTimerMinimumMillSec <= value && value <= PocketLadioInfo.HeadlineCheckTimerMaximumMillSec)
                {
                    UserSetting.headlineTimerMillSecond = value;
                }
                // 規定よりも短い場合
                else if (value < PocketLadioInfo.HeadlineCheckTimerMinimumMillSec)
                {
                    UserSetting.headlineTimerMillSecond = PocketLadioInfo.HeadlineCheckTimerMinimumMillSec;
                }
                // 規定よりも長い場合
                else if (value > PocketLadioInfo.HeadlineCheckTimerMaximumMillSec)
                {
                    UserSetting.headlineTimerMillSecond = PocketLadioInfo.HeadlineCheckTimerMaximumMillSec;
                }
            }
        }

        /// <summary>
        /// プロキシを使用するか
        /// </summary>
        private static bool proxyUse = false;

        /// <summary>
        /// プロキシを使用するか
        /// </summary>
        public static bool ProxyUse
        {
            get { return UserSetting.proxyUse; }
            set { UserSetting.proxyUse = value; }
        }

        /// <summary>
        /// プロキシのサーバ名
        /// </summary>
        private static string proxyServer = "";

        /// <summary>
        /// プロキシのサーバ名
        /// </summary>
        public static string ProxyServer
        {
            get { return proxyServer; }
            set { proxyServer = value; }
        }

        /// <summary>
        /// プロキシのポート番号
        /// </summary>
        private static string proxyPort = "";

        /// <summary>
        /// プロキシのポート番号
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
        /// 検索フィルター
        /// </summary>
        private static string[] filterWords = new string[0];

        /// <summary>
        /// 検索フィルター
        /// </summary>
        public static string[] FilterWords
        {
            get { return UserSetting.filterWords; }
            set { UserSetting.filterWords = value; }
        }

        /// <summary>
        /// アプリケーションの設定ファイル
        /// </summary>
        private const string settingPath = "Setting.xml";

        /// <summary>
        /// アプリケーションの設定ファイル
        /// </summary>
        private static string SettingPath
        {
            get { return settingPath; }
        }

        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private UserSetting()
        {
        }

        /// <summary>
        /// アプリケーションの設定ファイルの保存場所を返す
        /// </summary>
        /// <returns>アプリケーションの設定ファイルの保存場所</returns>
        public static string GetSettingPath()
        {
            // アプリケーションの実行ディレクトリ + アプリケーションの設定ファイル
            return PocketLadioUtil.GetExecutablePath() + "\\" + SettingPath;
        }

        /// <summary>
        /// 設定をファイルから読み込む
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
        /// 設定をファイルに保存
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
