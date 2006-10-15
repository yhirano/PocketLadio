#region ディレクティブを使用する

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using MiscPocketCompactLibrary.Reflection;

#endregion

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// SHOUTcastの設定を保持するクラス
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// 検索単語
        /// </summary>
        private string searchWord = "";

        /// <summary>
        /// 検索単語
        /// </summary>
        public string SearchWord
        {
            get { return searchWord; }
            set { searchWord = value; }
        }

        /// <summary>
        /// ヘッドライン取得数
        /// </summary>
        private string perView = "10";

        /// <summary>
        /// ヘッドライン取得数
        /// </summary>
        public string PerView
        {
            get { return perView; }
            set { perView = value; }
        }

        /// <summary>
        /// ヘッドライン取得数の設定可能値の配列
        /// </summary>
        private static string[] perViewArray = new string[0];

        /// <summary>
        /// ヘッドライン取得数の設定可能値の配列
        /// </summary>
        public static string[] PerViewArray
        {
            get { return UserSetting.perViewArray; }
            set { UserSetting.perViewArray = value; }
        }
       
        /// <summary>
        /// HTML解析時に、HTMLの解析をしない先頭からの行数。
        /// 200を指定した場合には、0～200行目は解析しない。
        /// （高速化のために使用する。）
        /// </summary>
        private int ignoreHtmlAnalyzeFirstTo = 200;

        /// <summary>
        /// HTML解析時に、HTMLの解析をしない先頭からの行数。
        /// 200を指定した場合には、1～200行目は解析しない。
        /// （高速化のために使用する。）
        /// </summary>
        public int IgnoreHtmlAnalyzeFirstTo
        {
            get { return ignoreHtmlAnalyzeFirstTo; }
            set { ignoreHtmlAnalyzeFirstTo = value; }
        }

        /// <summary>
        /// HTML解析時に、HTMLの解析をしない行末からの行数。
        /// 250を指定した場合には、行末から250行前～行末は解析しない。
        /// （高速化のために使用する。）
        /// </summary>
        private int ignoreHtmlAnalyzeEndFrom = 250;

        /// <summary>
        /// HTML解析時に、HTMLの解析をしない行末からの行数。
        /// 250を指定した場合には、行末から250行前～行末は解析しない。
        /// （高速化のために使用する。）
        /// </summary>
        public int IgnoreHtmlAnalyzeEndFrom
        {
            get { return ignoreHtmlAnalyzeEndFrom; }
            set { ignoreHtmlAnalyzeEndFrom = value; }
        }

        /// <summary>
        /// Shoutcastヘッドラインの表示方法
        /// </summary>
        private string headlineViewType = "[[RANK]] [[TITLE]]";

        /// <summary>
        /// Shoutcastヘッドラインの表示方法
        /// </summary>
        public string HeadlineViewType
        {
            get { return headlineViewType; }
            set { headlineViewType = value; }
        }

        /// <summary>
        /// 単語フィルター
        /// </summary>
        private String[] filterWords = new String[0];

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
        /// 単語フィルターを返す
        /// </summary>
        /// <returns>単語フィルター</returns>
        public string[] GetFilterWords()
        {
            return filterWords;
        }

        /// <summary>
        /// 単語フィルターをセットする
        /// </summary>
        /// <param name="filterWord">単語フィルター</param>
        public void SetFilterWords(string[] filterWord)
        {
            filterWords = filterWord;
        }

        /// <summary>
        /// Shoutcastの設定ファイルの保存場所を返す
        /// </summary>
        /// <returns>設定ファイルの保存場所</returns>
        private string GetSettingPath()
        {
            // アプリケーションの実行ディレクトリ + アプリケーションの設定ファイル
            return AssemblyUtility.GetExecutablePath() + @"\" + "Setting.SHOUTcast." + parentHeadline.GetId() + ".xml";
        }

        /// <summary>
        /// Shoutcastの設定をファイルから読み込む
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

                ArrayList alFilterWords = new ArrayList();

                // Filterタグの中にいるか
                bool inFilterFlag = false;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "Filter")
                        {
                            inFilterFlag = true;
                        }
                        else if (reader.LocalName == "SearchWord")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                SearchWord = reader.GetAttribute("word");
                            }
                        } // End of SearchWord
                        else if (reader.LocalName == "PerView")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                try
                                {
                                    perView = reader.GetAttribute("view");
                                }
                                catch (ArgumentException)
                                {
                                    ;
                                }
                                catch (FormatException)
                                {
                                    ;
                                }
                            }
                        } // End of PerView
                        /*
                        else if (reader.LocalName == "IgnoreHtmlAnalyze")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                try
                                {
                                    IgnoreHtmlAnalyzeFirstTo = int.Parse(reader.GetAttribute("firstto"));
                                    IgnoreHtmlAnalyzeEndFrom = int.Parse(reader.GetAttribute("endfrom"));
                                    if (IgnoreHtmlAnalyzeFirstTo < 0)
                                    {
                                        IgnoreHtmlAnalyzeFirstTo = 0;
                                    }
                                    if (IgnoreHtmlAnalyzeEndFrom < 0)
                                    {
                                        IgnoreHtmlAnalyzeEndFrom = 0;
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
                            }
                        } // End of IgnoreHtmlAnalyze
                        */
                        else if (reader.LocalName == "HeadlineViewType")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                HeadlineViewType = reader.GetAttribute("type");
                            }
                        } // End of HeadlineViewType
                        // Filterタグの中にいる場合
                        else if (inFilterFlag == true)
                        {
                            if (reader.LocalName == "Word")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    alFilterWords.Add(reader.GetAttribute("word"));
                                }
                            } // End of Filter
                        } // End of Filterタグの中にいる場合
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "Filter")
                        {
                            inFilterFlag = false;
                            SetFilterWords((string[])alFilterWords.ToArray(typeof(string)));
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

        /// <summary>
        /// Shoutcastの設定をファイルに保存
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

                writer.WriteStartElement("SearchWord");
                writer.WriteAttributeString("word", SearchWord);
                writer.WriteEndElement(); // End of SearchWord

                writer.WriteStartElement("PerView");
                writer.WriteAttributeString("view", perView);
                writer.WriteEndElement(); // End of PerView

                writer.WriteStartElement("IgnoreHtmlAnalyze");
                writer.WriteAttributeString("firstto", IgnoreHtmlAnalyzeFirstTo.ToString());
                writer.WriteAttributeString("endfrom", IgnoreHtmlAnalyzeEndFrom.ToString());
                writer.WriteEndElement(); // End of IgnoreHtmlAnalyze

                writer.WriteStartElement("HeadlineViewType");
                writer.WriteAttributeString("type", HeadlineViewType);
                writer.WriteEndElement(); // End of HeadlineViewType

                writer.WriteStartElement("Filter");
                foreach (string filterWord in GetFilterWords())
                {
                    writer.WriteStartElement("Word");
                    writer.WriteAttributeString("word", filterWord);
                    writer.WriteEndElement(); // End of Word
                }
                writer.WriteEndElement(); // End of Filter

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
