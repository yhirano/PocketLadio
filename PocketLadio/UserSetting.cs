#region ディレクティブを使用する

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
    /// PocketLadioの設定を保持するクラス
    /// </summary>
    public sealed class UserSetting
    {
        /// <summary>
        /// フィルター設定
        /// </summary>
        private static bool filterEnable;

        /// <summary>
        /// フィルター設定
        /// </summary>
        public static bool FilterEnable
        {
            get { return UserSetting.filterEnable; }
            set { UserSetting.filterEnable = value; }
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
        /// プレイリストは一端ローカルに保存するか
        /// </summary>
        private static bool playListSave = true;

        /// <summary>
        /// プレイリストは一端ローカルに保存するか
        /// </summary>
        public static bool PlayListSave
        {
            get { return UserSetting.playListSave; }
            set { UserSetting.playListSave = value; }
        }

        /// <summary>
        /// 音声再生用のメディアプレーヤーのファイルパス
        /// </summary>
        private static string mediaPlayerPath = PocketLadioInfo.DefaultMediaPlayerPath;

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
        private static string browserPath = PocketLadioInfo.DefaultBrowserPath;

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
        private static bool headlineTimerCheck;

        /// <summary>
        /// タイマーでチェックするか
        /// </summary>
        public static bool HeadlineTimerCheck
        {
            get { return UserSetting.headlineTimerCheck; }
            set { UserSetting.headlineTimerCheck = value; }
        }

        /// <summary>
        /// 番組表のフォントサイズを変更するか
        /// </summary>
        private static bool headlineListBoxFontSizeChange;

        /// <summary>
        /// 番組表のフォントサイズを変更するか
        /// </summary>
        public static bool HeadlineListBoxFontSizeChange
        {
            get { return UserSetting.headlineListBoxFontSizeChange; }
            set { UserSetting.headlineListBoxFontSizeChange = value; }
        }

        /// <summary>
        /// 番組表のフォントサイズ
        /// </summary>
        private static int headlineListBoxFontSize = PocketLadioInfo.HeadlineListBoxDefaultFontSize;

        /// <summary>
        /// 番組表のフォントサイズ
        /// </summary>
        public static int HeadlineListBoxFontSize
        {
            get { return UserSetting.headlineListBoxFontSize; }
            set
            {
                // 規定に収まる場合
                if (PocketLadioInfo.HeadlineListBoxFontSizeMinimumPt <= value && value <= PocketLadioInfo.HeadlineListBoxFontSizeMaximumPt)
                {
                    UserSetting.headlineListBoxFontSize = value;
                }
                // 規定よりも短い場合
                else if (value < PocketLadioInfo.HeadlineListBoxFontSizeMinimumPt)
                {
                    UserSetting.headlineListBoxFontSize = PocketLadioInfo.HeadlineListBoxFontSizeMinimumPt;
                }
                // 規定よりも長い場合
                else if (value > PocketLadioInfo.HeadlineListBoxFontSizeMaximumPt)
                {
                    UserSetting.headlineListBoxFontSize = PocketLadioInfo.HeadlineListBoxFontSizeMaximumPt;
                }
            }
        }

        /// <summary>
        /// プロキシの接続方法列挙
        /// </summary>
        public enum ProxyConnect
        {
            Unuse, OsSetting, OriginalSetting
        }

        /// <summary>
        /// プロキシの接続方法
        /// </summary>
        private static ProxyConnect proxyUse = ProxyConnect.OsSetting;

        /// <summary>
        /// プロキシの接続方法
        /// </summary>
        public static ProxyConnect ProxyUse
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
        private static int proxyPort = 0;

        /// <summary>
        /// プロキシのポート番号
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
        /// アプリケーションの設定ファイルの保存場所
        /// </summary>
        private static string SettingPath
        {
            get
            {
                // アプリケーションの実行ディレクトリ + アプリケーションの設定ファイル
                return AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.SettingFile;
            }
        }

        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private UserSetting()
        {
        }

        /// <summary>
        /// 設定をファイルから読み込む
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

                    // StationListタグの中にいるか
                    bool inStationListFlag = false;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.LocalName == "StationList")
                            {
                                inStationListFlag = true;
                            } // End of StationList
                            // StationListタグの中にいる場合
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
                                            // ここに到達することはあり得ない
                                            Trace.Assert(false, "想定外の動作のため、終了します");
                                        }

                                        alStation.Add(new Station(id, name, stationKind));
                                    }
                                } // End of Station
                            } // End of StationListタグの中にいる場合
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
        /// 設定をファイルに保存
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
