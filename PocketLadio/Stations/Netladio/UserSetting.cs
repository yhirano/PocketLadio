#region ディレクティブを使用する

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Diagnostics;
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
        private Uri headlineCsvUrl = new Uri(Headline.NETLADIO_HEADLINE_CVS_URL);

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
        private Uri headlineXmlUrl = new Uri(Headline.NETLADIO_HEADLINE_XML_URL);

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        public Uri HeadlineXmlUrl
        {
            get { return headlineXmlUrl; }
            set { headlineXmlUrl = value; }
        }

        /// <summary>
        /// ねとらじのヘッドラインのURL DAT v2
        /// </summary>
        private Uri headlineDatV2Url = new Uri(Headline.NETLADIO_HEADLINE_DAT_V2_URL);

        /// <summary>
        /// ねとらじのヘッドラインのURL DAT v2
        /// </summary>
        public Uri HeadlineDatV2Url
        {
            get { return headlineDatV2Url; }
            set { headlineDatV2Url = value; }
        }

        /// <summary>
        /// ヘッドラインの取得方法
        /// </summary>
        private HeadlineGetType headlineGetWay = HeadlineGetType.DatV2;

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
            Cvs, Xml, DatV2
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
        /// 一致単語フィルター
        /// </summary>
        private string[] filterMatchWords = new string[0];

        /// <summary>
        /// 除外単語フィルター
        /// </summary>
        private string[] filterExclusionWords = new string[0];

        /// <summary>
        /// ビットレート（～以下）フィルターを使用するか
        /// </summary>
        private bool filterBelowBitRateUse = false;

        /// <summary>
        /// ビットレート（～以下）フィルターを使用するか
        /// </summary>
        public bool FilterBelowBitRateUse
        {
            get { return filterBelowBitRateUse; }
            set
            {
                if (filterBelowBitRateUse != value)
                {
                    filterBelowBitRateUse = value;
                    OnFilterChanged();
                }
            }
        }

        /// <summary>
        /// ビットレート（～以下）フィルター
        /// </summary>
        private int filterBelowBitRate = 320;

        /// <summary>
        /// ビットレート（～以下）フィルター
        /// </summary>
        public int FilterBelowBitRate
        {
            get
            {
                if (filterBelowBitRate >= 0)
                {
                    return filterBelowBitRate;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value >= 0 && filterBelowBitRate != value)
                {
                    filterBelowBitRate = value;
                    OnFilterChanged();
                }
                else
                {
                    ;
                }
            }
        }

        /// <summary>
        /// ビットレート（～以上）フィルターを使用するか
        /// </summary>
        private bool filterAboveBitRateUse = false;

        /// <summary>
        /// ビットレート（～以上）フィルターを使用するか
        /// </summary>
        public bool FilterAboveBitRateUse
        {
            get { return filterAboveBitRateUse; }
            set
            {
                if (filterAboveBitRateUse != value)
                {
                    filterAboveBitRateUse = value;
                    OnFilterChanged();
                }
            }
        }

        /// <summary>
        /// ビットレート（～以上）フィルター
        /// </summary>
        private int filterAboveBitRate = 0;

        /// <summary>
        /// ビットレート（～以上）フィルター
        /// </summary>
        public int FilterAboveBitRate
        {
            get
            {
                if (filterAboveBitRate >= 0)
                {
                    return filterAboveBitRate;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value >= 0 && filterAboveBitRate != value)
                {
                    filterAboveBitRate = value;
                    OnFilterChanged();
                }
                else
                {
                    ;
                }
            }
        }

        /// <summary>
        /// ソートフィルター
        /// </summary>
        private Headline.SortKinds sortKind = Headline.SortKinds.None;

        /// <summary>
        /// ソートフィルター
        /// </summary>
        public Headline.SortKinds SortKind
        {
            get { return sortKind; }
            set
            {
                if (sortKind != value)
                {
                    sortKind = value;
                    OnFilterChanged();
                }
            }
        }

        /// <summary>
        /// ソートの昇順・降順
        /// </summary>
        private Headline.SortScendings sortScending = Headline.SortScendings.Ascending;

        /// <summary>
        /// ソートの昇順・降順
        /// </summary>
        public Headline.SortScendings SortScending
        {
            get { return sortScending; }
            set
            {
                if (sortScending != value)
                {
                    sortScending = value;
                    OnFilterChanged();
                }
            }
        }

        /// <summary>
        /// 親ヘッドライン
        /// </summary>
        private readonly Headline parentHeadline;

        /// <summary>
        /// 親ヘッドライン
        /// </summary>
        public Headline ParentHeadline
        {
            get { return parentHeadline; }
        }

        /// <summary>
        /// 設定のコンストラクタ
        /// </summary>
        /// <param name="ParentHeadline">親ヘッドライン</param>
        public UserSetting(Headline parentHeadline)
        {
            this.parentHeadline = parentHeadline;
        }

        /// <summary>
        /// 一致単語フィルターを返す
        /// </summary>
        /// <returns>一致単語フィルター</returns>
        public string[] GetFilterMatchWords()
        {
            return filterMatchWords;
        }

        /// <summary>
        /// 一致単語フィルターをセットする
        /// </summary>
        /// <param name="filterWords">単語フィルター</param>
        public void SetFilterMatchWords(string[] filterWords)
        {
            // フィルタの内容が変化したかを調べる
            bool isChanged = false;
            if (filterWords.Length != filterMatchWords.Length)
            {
                isChanged = true;
            }
            else
            {
                for (int i = 0; i < filterWords.Length && i < filterMatchWords.Length; ++i)
                {
                    if (filterWords[i] != filterMatchWords[i])
                    {
                        isChanged = true;
                        break;
                    }
                }
            }

            if (isChanged == true)
            {
                filterMatchWords = filterWords;
                OnFilterChanged();
            }
        }

        /// <summary>
        /// 除外単語フィルターを返す
        /// </summary>
        /// <returns>除外単語フィルター</returns>
        public string[] GetFilterExclusionWords()
        {
            return filterExclusionWords;
        }

        /// <summary>
        /// 除外単語フィルターをセットする
        /// </summary>
        /// <param name="filterWords">除外フィルター</param>
        public void SetFilterExclusionWords(string[] filterWords)
        {
            // フィルタの内容が変化したかを調べる
            bool isChanged = false;
            if (filterWords.Length != filterExclusionWords.Length)
            {
                isChanged = true;
            }
            else
            {
                for (int i = 0; i < filterWords.Length && i < filterExclusionWords.Length; ++i)
                {
                    if (filterWords[i] != filterExclusionWords[i])
                    {
                        isChanged = true;
                        break;
                    }
                }
            }

            if (isChanged == true)
            {
                filterExclusionWords = filterWords;
                OnFilterChanged();
            }
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

                ArrayList alMatchFilterWords = new ArrayList();
                ArrayList alExclusionFilterWords = new ArrayList();

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
                        else if (reader.LocalName == "HeadlineCsvUrl")
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
                        else if (reader.LocalName == "HeadlineDatV2Url")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                try
                                {
                                    HeadlineDatV2Url = new Uri(reader.GetAttribute("url"));
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
                                else if (type == HeadlineGetType.DatV2.ToString())
                                {
                                    HeadlineGetWay = HeadlineGetType.DatV2;
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
                        // Filterタグの中にいる場合
                        else if (inFilterFlag == true)
                        {
                            if (reader.LocalName == "Word")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    string type = reader.GetAttribute("type");
                                    switch (type)
                                    {
                                        case "exclusion":
                                            alExclusionFilterWords.Add(reader.GetAttribute("word"));
                                            break;
                                        case "match":
                                        default:
                                            alMatchFilterWords.Add(reader.GetAttribute("word"));
                                            break;
                                    }
                                }
                            } // End of Word
                            else if (reader.LocalName == "AboveBitRate")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    string use = reader.GetAttribute("use");
                                    if (use == bool.TrueString)
                                    {
                                        FilterAboveBitRateUse = true;
                                    }
                                    else if (use == bool.FalseString)
                                    {
                                        FilterAboveBitRateUse = false;
                                    }

                                    try
                                    {
                                        string bitRate = reader.GetAttribute("bitrate");
                                        FilterAboveBitRate = int.Parse(bitRate);
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
                            } // End of AboveBitRate
                            else if (reader.LocalName == "BelowBitRate")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    string use = reader.GetAttribute("use");
                                    if (use == bool.TrueString)
                                    {
                                        FilterBelowBitRateUse = true;
                                    }
                                    else if (use == bool.FalseString)
                                    {
                                        FilterBelowBitRateUse = false;
                                    }

                                    try
                                    {
                                        string bitRate = reader.GetAttribute("bitrate");
                                        FilterBelowBitRate = int.Parse(bitRate);
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
                            } // End of BelowBitRate
                            else if (reader.LocalName == "Sort")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    string kind = reader.GetAttribute("kind");
                                    if (kind == Headline.SortKinds.None.ToString())
                                    {
                                        SortKind = Headline.SortKinds.None;
                                    }
                                    else if (kind == Headline.SortKinds.Nam.ToString())
                                    {
                                        SortKind = Headline.SortKinds.Nam;
                                    }
                                    else if (kind == Headline.SortKinds.Tims.ToString())
                                    {
                                        SortKind = Headline.SortKinds.Tims;
                                    }
                                    else if (kind == Headline.SortKinds.Cln.ToString())
                                    {
                                        SortKind = Headline.SortKinds.Cln;
                                    }
                                    else if (kind == Headline.SortKinds.Clns.ToString())
                                    {
                                        SortKind = Headline.SortKinds.Clns;
                                    }
                                    else if (kind == Headline.SortKinds.Bit.ToString())
                                    {
                                        SortKind = Headline.SortKinds.Bit;
                                    }
                                    else
                                    {
                                        // ここに到達することはあり得ない
                                        Trace.Assert(false, "想定外の動作のため、終了します");
                                    }

                                    string scending = reader.GetAttribute("scending");
                                    if (scending == Headline.SortScendings.Ascending.ToString())
                                    {
                                        SortScending = Headline.SortScendings.Ascending;
                                    }
                                    else if (scending == Headline.SortScendings.Descending.ToString())
                                    {
                                        SortScending = Headline.SortScendings.Descending;
                                    }
                                }
                            } // End of Sort
                        } // End of Filterタグの中にいる場合
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        inFilterFlag = false;
                        SetFilterMatchWords((string[])alMatchFilterWords.ToArray(typeof(string)));
                        SetFilterExclusionWords((string[])alExclusionFilterWords.ToArray(typeof(string)));
                    }
                }
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
                writer.WriteAttributeString("url", ((HeadlineCsvUrl != null) ? HeadlineCsvUrl.ToString() : string.Empty));
                writer.WriteEndElement(); // End of HeadlineCsvUrl

                writer.WriteStartElement("HeadlineXmlUrl");
                writer.WriteAttributeString("url", ((HeadlineXmlUrl != null) ? HeadlineXmlUrl.ToString() : string.Empty));
                writer.WriteEndElement(); // End of HeadlineXmlUrl

                writer.WriteStartElement("HeadlineDatV2Url");
                writer.WriteAttributeString("url", ((HeadlineDatV2Url != null) ? HeadlineDatV2Url.ToString() : string.Empty));
                writer.WriteEndElement(); // End of HeadlineDatV2Url

                writer.WriteStartElement("HeadlineGetType");
                writer.WriteAttributeString("type", HeadlineGetWay.ToString());
                writer.WriteEndElement(); // End of HeadlineGetType

                writer.WriteStartElement("HeadlineViewType");
                writer.WriteAttributeString("type", HeadlineViewType);
                writer.WriteEndElement(); // End of HeadlineViewType

                writer.WriteStartElement("Filter");

                foreach (string filterWord in GetFilterMatchWords())
                {
                    writer.WriteStartElement("Word");
                    writer.WriteAttributeString("type", "match");
                    writer.WriteAttributeString("word", filterWord);
                    writer.WriteEndElement(); // End of Word
                }

                foreach (string filterWord in GetFilterExclusionWords())
                {
                    writer.WriteStartElement("Word");
                    writer.WriteAttributeString("type", "exclusion");
                    writer.WriteAttributeString("word", filterWord);
                    writer.WriteEndElement(); // End of Word
                }

                writer.WriteStartElement("AboveBitRate");
                writer.WriteAttributeString("use", FilterAboveBitRateUse.ToString());
                writer.WriteAttributeString("bitrate", FilterAboveBitRate.ToString());
                writer.WriteEndElement(); // End of AboveBitRate

                writer.WriteStartElement("BelowBitRate");
                writer.WriteAttributeString("use", FilterBelowBitRateUse.ToString());
                writer.WriteAttributeString("bitrate", FilterBelowBitRate.ToString());
                writer.WriteEndElement(); // End of BelowBitRate

                writer.WriteStartElement("Sort");
                writer.WriteAttributeString("kind", SortKind.ToString());
                writer.WriteAttributeString("scending", SortScending.ToString());
                writer.WriteEndElement(); // End of Sort

                writer.WriteEndElement(); // End of Filter

                writer.WriteEndElement(); // End of Content.

                writer.WriteEndElement(); // End of Setting.

                writer.WriteEndDocument();
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

        /// <summary>
        /// フィルターが変更された場合に発生するイベント
        /// </summary>
        public event EventHandler FilterChanged;

        private void OnFilterChanged()
        {
            if (FilterChanged != null)
            {
                FilterChanged(this, EventArgs.Empty);
            }
        }
    }
}
