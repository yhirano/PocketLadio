#region ディレクティブを使用する

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using MiscPocketCompactLibrary.Reflection;

#endregion

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// ねとらじの設定を保持するクラス
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// ねとらじのヘッドラインのURL CSV
        /// </summary>
        private Uri headlineCsvUrl = new Uri("http://yp.ladio.livedoor.jp/stats/list.csv");

        /// <summary>
        /// ねとらじのヘッドラインのURL CSV
        /// </summary>
        public Uri HeadlineCsvUrl
        {
            get { return headlineCsvUrl; }
            set { headlineCsvUrl = value; }
        }

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        private Uri headlineXmlUrl = new Uri("http://yp.ladio.livedoor.jp/stats/list.xml");

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        public Uri HeadlineXmlUrl
        {
            get { return headlineXmlUrl; }
            set { headlineXmlUrl = value; }
        }

        /// <summary>
        /// ヘッドラインの取得方法
        /// </summary>
        private HeadlineGetType headlineGetWay = HeadlineGetType.Cvs;

        /// <summary>
        /// ヘッドラインの取得方法
        /// </summary>
        public HeadlineGetType HeadlineGetWay
        {
            get { return headlineGetWay; }
            set { headlineGetWay = value; }
        }

        /// <summary>
        /// ねとらじヘッドラインの取得方法の列挙
        /// </summary>
        public enum HeadlineGetType
        {
            Cvs, Xml
        };

        /// <summary>
        /// ねとらじヘッドラインの表示方法
        /// </summary>
        private string headlineViewType = "[[NAME]] - [[GENRE]] ([[CLN]]/[[CLNS]])";

        /// <summary>
        /// ねとらじヘッドラインの表示方法
        /// </summary>
        public string HeadlineViewType
        {
            get { return headlineViewType; }
            set { headlineViewType = value; }
        }

        /// <summary>
        /// 親ヘッドライン
        /// </summary>
        private readonly Headline parentHeadline;

        /// <summary>
        /// 設定のコンストラクタ
        /// </summary>
        /// <param name="ParentHeadline">親ヘッドライン</param>
        public UserSetting(Headline parentHeadline)
        {
            this.parentHeadline = parentHeadline;
        }

        /// <summary>
        /// ねとらじの設定ファイルの保存場所を返す
        /// </summary>
        /// <returns>設定ファイルの保存場所</returns>
        private string GetSettingPath()
        {
            // アプリケーションの実行ディレクトリ + アプリケーションの設定ファイル
            return AssemblyUtility.GetExecutablePath() + @"\" + "Setting.Netladio." + parentHeadline.GetId() + ".xml";
        }

        /// <summary>
        /// ねとらじの設定をファイルから読み込む
        /// </summary>
        public void LoadSetting()
        {
            // ファイルがない場合は読み込まず終了
            if (File.Exists(GetSettingPath()) == false)
            {
                return;
            }

            FileStream fs = null;
            XmlTextReader reader = null;

            try
            {
                fs = new FileStream(GetSettingPath(), FileMode.Open, FileAccess.Read);
                reader = new XmlTextReader(fs);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "HeadlineCsvUrl")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                try
                                {
                                    HeadlineCsvUrl = new Uri(reader.GetAttribute("url"));
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            }
                        } // End of HeadlineCsvUrl
                        else if (reader.LocalName == "HeadlineXmlUrl")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                try
                                {
                                    HeadlineXmlUrl = new Uri(reader.GetAttribute("url"));
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            }
                        } // End of HeadlineXmlUrl
                        else if (reader.LocalName == "HeadlineGetType")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                string type = reader.GetAttribute("type");
                                if (type == HeadlineGetType.Cvs.ToString())
                                {
                                    HeadlineGetWay = HeadlineGetType.Cvs;
                                }
                                else if (type == HeadlineGetType.Xml.ToString())
                                {
                                    HeadlineGetWay = HeadlineGetType.Xml;
                                }
                            }
                        } // End of HeadlineGetType
                        else if (reader.LocalName == "HeadlineViewType")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                HeadlineViewType = reader.GetAttribute("type");
                            }
                        } // End of HeadlineViewType
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

        /// <summary>
        /// ねとらじの設定をファイルに保存
        /// </summary>
        public void SaveSetting()
        {
            FileStream fs = null;
            XmlTextWriter writer = null;

            try
            {
                fs = new FileStream(GetSettingPath(), FileMode.Create, FileAccess.Write);
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

                writer.WriteStartElement("HeadlineCsvUrl");
                writer.WriteAttributeString("url", ((HeadlineCsvUrl != null) ? HeadlineCsvUrl.ToString() : ""));
                writer.WriteEndElement(); // End of HeadlineCsvUrl

                writer.WriteStartElement("HeadlineXmlUrl");
                writer.WriteAttributeString("url", ((HeadlineXmlUrl != null) ? HeadlineXmlUrl.ToString() : ""));
                writer.WriteEndElement(); // End of HeadlineXmlUrl

                writer.WriteStartElement("HeadlineGetType");
                writer.WriteAttributeString("type", HeadlineGetWay.ToString());
                writer.WriteEndElement(); // End of HeadlineGetType

                writer.WriteStartElement("HeadlineViewType");
                writer.WriteAttributeString("type", HeadlineViewType);
                writer.WriteEndElement(); // End of HeadlineViewType

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

        /// <summary>
        /// 設定を保存していたファイルを削除する
        /// </summary>
        public void DeleteUserSettingFile()
        {
            if (File.Exists(GetSettingPath()))
            {
                File.Delete(GetSettingPath());
            }
        }
    }
}
