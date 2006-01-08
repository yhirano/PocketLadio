using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// ねとらじの設定を保持するクラス
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// ねとらじのヘッドラインのURL CSV
        /// </summary>
        public string HeadlineCsvUrl = "http://yp.ladio.livedoor.jp/stats/list.csv";

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        public string HeadlineXmlUrl = "http://yp.ladio.livedoor.jp/stats/list.xml";

        /// <summary>
        /// ヘッドラインの取得方法
        /// </summary>
        public HeadlineGetTypeEnum HeadlineGetType = HeadlineGetTypeEnum.Cvs;

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
        public string HeadlineViewType = "[[NAME]] - [[GENRE]] ([[CLN]]/[[CLNS]])";

        /// <summary>
        /// 親ヘッドライン
        /// </summary>
        private Headline ParentHeadline;

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
            return Controller.GetExecutablePath() + "\\" + "Setting.Netladio." + ParentHeadline.GetID() + ".xml";
        }

        /// <summary>
        /// ねとらじの設定をファイルから読み込む
        /// </summary>
        public void LoadSetting()
        {
            if (File.Exists(GetSettingPath()))
            {
                FileStream Fs = new FileStream(GetSettingPath(), FileMode.Open, FileAccess.Read);
                Encoding Encode = Encoding.GetEncoding("utf-8");
                XmlTextReader Reader = new XmlTextReader(Fs);

                ArrayList AlFilterWords = new ArrayList();

                try
                {
                    while (Reader.Read())
                    {
                        if (Reader.NodeType == XmlNodeType.Element)
                        {
                            if (Reader.LocalName.Equals("HeadlineCsvUrl"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("url"))
                                        {
                                            HeadlineCsvUrl = Reader.Value;
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of HeadlineCsvUrl

                            if (Reader.LocalName.Equals("HeadlineXmlUrl"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("url"))
                                        {
                                            HeadlineXmlUrl = Reader.Value;
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of HeadlineXmlUrl

                            if (Reader.LocalName.Equals("HeadlineGetType"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("type"))
                                        {
                                            if (Reader.Value.Equals(HeadlineGetTypeEnum.Cvs.ToString()))
                                            {
                                                HeadlineGetType = HeadlineGetTypeEnum.Cvs;
                                            }
                                            else if (Reader.Value.Equals(HeadlineGetTypeEnum.Xml.ToString()))
                                            {
                                                HeadlineGetType = HeadlineGetTypeEnum.Xml;
                                            }
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of HeadlineGetType


                            if (Reader.LocalName.Equals("HeadlineViewType"))
                            {
                                if (Reader.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        if (Reader.Name.Equals("type"))
                                        {
                                            HeadlineViewType = Reader.Value;
                                        }
                                    } while (Reader.MoveToNextAttribute());
                                }
                            } // End of HeadlineViewType
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
        /// ねとらじの設定をファイルに保存
        /// </summary>
        public void SaveSetting()
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
