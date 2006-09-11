using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using PocketLadio.Util;

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
        private string headlineCsvUrl = "http://yp.ladio.livedoor.jp/stats/list.csv";

        /// <summary>
        /// ねとらじのヘッドラインのURL CSV
        /// </summary>
        public string HeadlineCsvUrl
        {
            get { return headlineCsvUrl; }
            set { headlineCsvUrl = value; }
        }

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        private string headlineXmlUrl = "http://yp.ladio.livedoor.jp/stats/list.xml";

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        public string HeadlineXmlUrl
        {
            get { return headlineXmlUrl; }
            set { headlineXmlUrl = value; }
        }

        /// <summary>
        /// ヘッドラインの取得方法
        /// </summary>
        private HeadlineGetTypeEnum headlineGetType = HeadlineGetTypeEnum.Cvs;

        /// <summary>
        /// ヘッドラインの取得方法
        /// </summary>
        public HeadlineGetTypeEnum HeadlineGetType
        {
            get { return headlineGetType; }
            set { headlineGetType = value; }
        }

        /// <summary>
        /// ねとらじヘッドラインの取得方法の列挙
        /// </summary>
        public enum HeadlineGetTypeEnum
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
        private readonly Headline ParentHeadline;

        /// <summary>
        /// 設定のコンストラクタ
        /// </summary>
        /// <param name="ParentHeadline">親ヘッドライン</param>
        public UserSetting(Headline parentHeadline)
        {
            this.ParentHeadline = parentHeadline;
        }

        /// <summary>
        /// ねとらじの設定ファイルの保存場所を返す
        /// </summary>
        /// <returns>設定ファイルの保存場所</returns>
        private string GetSettingPath()
        {
            // アプリケーションの実行ディレクトリ + アプリケーションの設定ファイル
            return PocketLadioUtil.GetExecutablePath() + "\\" + "Setting.Netladio." + ParentHeadline.GetID() + ".xml";
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

            FileStream Fs = null;
            XmlTextReader Reader = null;

            try
            {
                Fs = new FileStream(GetSettingPath(), FileMode.Open, FileAccess.Read);
                Reader = new XmlTextReader(Fs);

                ArrayList AlFilterWords = new ArrayList();

                while (Reader.Read())
                {
                    if (Reader.NodeType == XmlNodeType.Element)
                    {
                        if (Reader.LocalName == "HeadlineCsvUrl")
                        {
                            if (Reader.MoveToFirstAttribute())
                            {
                                HeadlineCsvUrl = Reader.GetAttribute("url");
                            }
                        } // End of HeadlineCsvUrl
                        else if (Reader.LocalName == "HeadlineXmlUrl")
                        {
                            if (Reader.MoveToFirstAttribute())
                            {
                                HeadlineXmlUrl = Reader.GetAttribute("url");
                            }
                        } // End of HeadlineXmlUrl
                        else if (Reader.LocalName == "HeadlineGetType")
                        {
                            if (Reader.MoveToFirstAttribute())
                            {
                                string type = Reader.GetAttribute("type");
                                if (type == HeadlineGetTypeEnum.Cvs.ToString())
                                {
                                    HeadlineGetType = HeadlineGetTypeEnum.Cvs;
                                }
                                else if (type == HeadlineGetTypeEnum.Xml.ToString())
                                {
                                    HeadlineGetType = HeadlineGetTypeEnum.Xml;
                                }
                            }
                        } // End of HeadlineGetType
                        else if (Reader.LocalName == "HeadlineViewType")
                        {
                            if (Reader.MoveToFirstAttribute())
                            {
                                HeadlineViewType = Reader.GetAttribute("type");
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
                Reader.Close();
                Fs.Close();
            }

        }

        /// <summary>
        /// ねとらじの設定をファイルに保存
        /// </summary>
        public void SaveSetting()
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

                Writer.WriteStartElement("HeadlineCsvUrl");
                Writer.WriteAttributeString("url", HeadlineCsvUrl);
                Writer.WriteEndElement(); // End of HeadlineCsvUrl

                Writer.WriteStartElement("HeadlineXmlUrl");
                Writer.WriteAttributeString("url", HeadlineXmlUrl);
                Writer.WriteEndElement(); // End of HeadlineXmlUrl

                Writer.WriteStartElement("HeadlineGetType");
                Writer.WriteAttributeString("type", HeadlineGetType.ToString());
                Writer.WriteEndElement(); // End of HeadlineGetType

                Writer.WriteStartElement("HeadlineViewType");
                Writer.WriteAttributeString("type", HeadlineViewType);
                Writer.WriteEndElement(); // End of HeadlineViewType

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
