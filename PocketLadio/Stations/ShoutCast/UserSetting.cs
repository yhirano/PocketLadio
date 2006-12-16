#region �f�B���N�e�B�u���g�p����

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Diagnostics;
using MiscPocketCompactLibrary.Reflection;

#endregion

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// SHOUTcast�̐ݒ��ێ�����N���X
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// �����P��
        /// </summary>
        private string searchWord = "";

        /// <summary>
        /// �����P��
        /// </summary>
        public string SearchWord
        {
            get { return searchWord; }
            set { searchWord = value; }
        }

        /// <summary>
        /// �w�b�h���C���擾��
        /// </summary>
        private string perView = "10";

        /// <summary>
        /// �w�b�h���C���擾��
        /// </summary>
        public string PerView
        {
            get { return perView; }
            set { perView = value; }
        }

        /// <summary>
        /// HTML��͎��ɁAHTML�̉�͂����Ȃ��擪����̍s���B
        /// 200���w�肵���ꍇ�ɂ́A0�`200�s�ڂ͉�͂��Ȃ��B
        /// �i�������̂��߂Ɏg�p����B�j
        /// </summary>
        private int ignoreHtmlAnalyzeFirstTo = 200;

        /// <summary>
        /// HTML��͎��ɁAHTML�̉�͂����Ȃ��擪����̍s���B
        /// 200���w�肵���ꍇ�ɂ́A1�`200�s�ڂ͉�͂��Ȃ��B
        /// �i�������̂��߂Ɏg�p����B�j
        /// </summary>
        public int IgnoreHtmlAnalyzeFirstTo
        {
            get { return ignoreHtmlAnalyzeFirstTo; }
            set { ignoreHtmlAnalyzeFirstTo = value; }
        }

        /// <summary>
        /// HTML��͎��ɁAHTML�̉�͂����Ȃ��s������̍s���B
        /// 250���w�肵���ꍇ�ɂ́A�s������250�s�O�`�s���͉�͂��Ȃ��B
        /// �i�������̂��߂Ɏg�p����B�j
        /// </summary>
        private int ignoreHtmlAnalyzeEndFrom = 250;

        /// <summary>
        /// HTML��͎��ɁAHTML�̉�͂����Ȃ��s������̍s���B
        /// 250���w�肵���ꍇ�ɂ́A�s������250�s�O�`�s���͉�͂��Ȃ��B
        /// �i�������̂��߂Ɏg�p����B�j
        /// </summary>
        public int IgnoreHtmlAnalyzeEndFrom
        {
            get { return ignoreHtmlAnalyzeEndFrom; }
            set { ignoreHtmlAnalyzeEndFrom = value; }
        }

        /// <summary>
        /// Shoutcast�w�b�h���C���̕\�����@
        /// </summary>
        private string headlineViewType = "[[RANK]] [[TITLE]]";

        /// <summary>
        /// Shoutcast�w�b�h���C���̕\�����@
        /// </summary>
        public string HeadlineViewType
        {
            get { return headlineViewType; }
            set { headlineViewType = value; }
        }

        /// <summary>
        /// �P��t�B���^�[
        /// </summary>
        private String[] filterWords = new String[0];

        /// <summary>
        /// �r�b�g���[�g�i�`�ȉ��j�t�B���^�[���g�p���邩
        /// </summary>
        private bool filterBelowBitRateUse = false;

        /// <summary>
        /// �r�b�g���[�g�i�`�ȉ��j�t�B���^�[���g�p���邩
        /// </summary>
        public bool FilterBelowBitRateUse
        {
            get { return filterBelowBitRateUse; }
            set { filterBelowBitRateUse = value; }
        }

        /// <summary>
        /// �r�b�g���[�g�i�`�ȉ��j�t�B���^�[
        /// </summary>
        private int filterBelowBitRate = 320;

        /// <summary>
        /// �r�b�g���[�g�i�`�ȉ��j�t�B���^�[
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
                if (value >= 0)
                {
                    filterBelowBitRate = value;
                }
                else
                {
                    ;
                }
            }
        }

        /// <summary>
        /// �r�b�g���[�g�i�`�ȏ�j�t�B���^�[���g�p���邩
        /// </summary>
        private bool filterAboveBitRateUse = false;

        /// <summary>
        /// �r�b�g���[�g�i�`�ȏ�j�t�B���^�[���g�p���邩
        /// </summary>
        public bool FilterAboveBitRateUse
        {
            get { return filterAboveBitRateUse; }
            set { filterAboveBitRateUse = value; }
        }

        /// <summary>
        /// �r�b�g���[�g�i�`�ȏ�j�t�B���^�[
        /// </summary>
        private int filterAboveBitRate = 0;

        /// <summary>
        /// �r�b�g���[�g�i�`�ȏ�j�t�B���^�[
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
                if (value >= 0)
                {
                    filterAboveBitRate = value;
                }
                else
                {
                    ;
                }
            }
        }

        /// <summary>
        /// �\�[�g�t�B���^�[
        /// </summary>
        private Headline.SortKind sortKind = Headline.SortKind.None;

        /// <summary>
        /// �\�[�g�t�B���^�[
        /// </summary>
        public Headline.SortKind SortKind
        {
            get { return sortKind; }
            set { sortKind = value; }
        }

        /// <summary>
        /// �\�[�g�̏����E�~��
        /// </summary>
        private Headline.SortScending sortScending = Headline.SortScending.Ascending;

        /// <summary>
        /// �\�[�g�̏����E�~��
        /// </summary>
        public Headline.SortScending SortScending
        {
            get { return sortScending; }
            set { sortScending = value; }
        }

        /// <summary>
        /// �e�w�b�h���C��
        /// </summary>
        private readonly Headline parentHeadline;

        /// <summary>
        /// �ݒ�̃R���X�g���N�^
        /// </summary>
        /// <param name="ParentHeadline">�e�w�b�h���C��</param>
        public UserSetting(Headline parentHeadline)
        {
            this.parentHeadline = parentHeadline;
        }

        /// <summary>
        /// �P��t�B���^�[��Ԃ�
        /// </summary>
        /// <returns>�P��t�B���^�[</returns>
        public string[] GetFilterWords()
        {
            return filterWords;
        }

        /// <summary>
        /// �P��t�B���^�[���Z�b�g����
        /// </summary>
        /// <param name="filterWord">�P��t�B���^�[</param>
        public void SetFilterWords(string[] filterWord)
        {
            filterWords = filterWord;
        }

        /// <summary>
        /// Shoutcast�̐ݒ�t�@�C���̕ۑ��ꏊ��Ԃ�
        /// </summary>
        /// <returns>�ݒ�t�@�C���̕ۑ��ꏊ</returns>
        private string GetSettingPath()
        {
            // �A�v���P�[�V�����̎��s�f�B���N�g�� + �A�v���P�[�V�����̐ݒ�t�@�C��
            return AssemblyUtility.GetExecutablePath() + @"\" + "Setting.SHOUTcast." + parentHeadline.GetId() + ".xml";
        }

        /// <summary>
        /// Shoutcast�̐ݒ���t�@�C������ǂݍ���
        /// </summary>
        public void LoadSetting()
        {
            // �t�@�C�����Ȃ��ꍇ�͓ǂݍ��܂��I��
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

                // Filter�^�O�̒��ɂ��邩
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
                        // Filter�^�O�̒��ɂ���ꍇ
                        else if (inFilterFlag == true)
                        {
                            if (reader.LocalName == "Word")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    alFilterWords.Add(reader.GetAttribute("word"));
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
                                    if (kind == Headline.SortKind.None.ToString())
                                    {
                                        SortKind = Headline.SortKind.None;
                                    }
                                    else if (kind == Headline.SortKind.Title.ToString())
                                    {
                                        SortKind = Headline.SortKind.Title;
                                    }
                                    else if (kind == Headline.SortKind.Listener.ToString())
                                    {
                                        SortKind = Headline.SortKind.Listener;
                                    }
                                    else if (kind == Headline.SortKind.BitRate.ToString())
                                    {
                                        SortKind = Headline.SortKind.BitRate;
                                    }
                                    else
                                    {
                                        // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                                        Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
                                    }

                                    string scending = reader.GetAttribute("scending");
                                    if (scending == Headline.SortScending.Ascending.ToString())
                                    {
                                        SortScending = Headline.SortScending.Ascending;
                                    }
                                    else if (scending == Headline.SortScending.Descending.ToString())
                                    {
                                        SortScending = Headline.SortScending.Descending;
                                    }
                                }
                            } // End of Sort
                        } // End of Filter�^�O�̒��ɂ���ꍇ
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
        /// Shoutcast�̐ݒ���t�@�C���ɕۑ�
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
        /// �ݒ��ۑ����Ă����t�@�C�����폜����
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
