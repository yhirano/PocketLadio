#region �f�B���N�e�B�u���g�p����

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using MiscPocketCompactLibrary.Reflection;

#endregion

namespace PocketLadio.Stations.RssPodcast
{
    /// <summary>
    /// Podcast�̐ݒ��ێ�����N���X
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// Podcast��RSS��URL
        /// </summary>
        private Uri rssUrl;

        /// <summary>
        /// Podcast��RSS��URL
        /// </summary>
        public Uri RssUrl
        {
            get { return rssUrl; }
            set { rssUrl = value; }
        }

        /// <summary>
        /// Podcast��RSS�̕\�����@
        /// </summary>
        private string headlineViewType = "[[TITLE]] - [[DESCRIPTION]]";

        /// <summary>
        /// Podcast��RSS�̕\�����@
        /// </summary>
        public string HeadlineViewType
        {
            get { return headlineViewType; }
            set { headlineViewType = value; }
        }

        /// <summary>
        /// ��v�P��t�B���^�[
        /// </summary>
        private string[] filterMatchWords = new string[0];

        /// <summary>
        /// ���O�P��t�B���^�[
        /// </summary>
        private string[] filterExclusionWords = new string[0];

        /// <summary>
        /// �e�w�b�h���C��
        /// </summary>
        private readonly Headline parentHeadline;

        /// <summary>
        /// �e�w�b�h���C��
        /// </summary>
        public Headline ParentHeadline
        {
            get { return parentHeadline; }
        } 


        /// <summary>
        /// �ݒ�̃R���X�g���N�^
        /// </summary>
        /// <param name="ParentHeadline">�e�w�b�h���C��</param>
        public UserSetting(Headline parentHeadline)
        {
            this.parentHeadline = parentHeadline;
        }

        /// <summary>
        /// ��v�P��t�B���^�[��Ԃ�
        /// </summary>
        /// <returns>��v�P��t�B���^�[</returns>
        public string[] GetFilterMatchWords()
        {
            return filterMatchWords;
        }

        /// <summary>
        /// ��v�P��t�B���^�[���Z�b�g����
        /// </summary>
        /// <param name="filterWords">�P��t�B���^�[</param>
        public void SetFilterMatchWords(string[] filterWords)
        {
            // �t�B���^�[�̓��e���ω��������𒲂ׂ�
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
        /// ���O�P��t�B���^�[��Ԃ�
        /// </summary>
        /// <returns>���O�P��t�B���^�[</returns>
        public string[] GetFilterExclusionWords()
        {
            return filterExclusionWords;
        }

        /// <summary>
        /// ���O�P��t�B���^�[���Z�b�g����
        /// </summary>
        /// <param name="filterWords">���O�t�B���^�[</param>
        public void SetFilterExclusionWords(string[] filterWords)
        {
            // �t�B���^�[�̓��e���ω��������𒲂ׂ�
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
        /// Podcast�̐ݒ�t�@�C���̕ۑ��ꏊ��Ԃ�
        /// </summary>
        /// <returns>�ݒ�t�@�C���̕ۑ��ꏊ</returns>
        private string GetSettingPath()
        {
            // �A�v���P�[�V�����̎��s�f�B���N�g�� + �A�v���P�[�V�����̐ݒ�t�@�C��
            return AssemblyUtility.GetExecutablePath() + @"\" + "Setting.RssPodcast." + parentHeadline.GetId() + ".xml";
        }

        /// <summary>
        /// Podcast�̐ݒ���t�@�C������ǂݍ���
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

            ArrayList alMatchFilterWords = new ArrayList();
            ArrayList alExclusionFilterWords = new ArrayList();

            // Filter�^�O�̒��ɂ��邩
            bool inFilterFlag = false;

            try
            {
                fs = new FileStream(GetSettingPath(), FileMode.Open, FileAccess.Read);
                reader = new XmlTextReader(fs);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "Filter") {
                                inFilterFlag = true;
                        }
                        else if (reader.LocalName == "RssUrl")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                try
                                {
                                    RssUrl = new Uri(reader.GetAttribute("url"));
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            }
                        } // End of RssUrl
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
                        } // End of Filter�^�O�̒��ɂ���ꍇ
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "Filter")
                        {
                            inFilterFlag = false;
                            SetFilterMatchWords((string[])alMatchFilterWords.ToArray(typeof(string)));
                            SetFilterExclusionWords((string[])alExclusionFilterWords.ToArray(typeof(string)));
                        }
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
        /// Podcast�̐ݒ���t�@�C���ɕۑ�
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

                writer.WriteStartElement("RssUrl");
                writer.WriteAttributeString("url", ((RssUrl != null) ? RssUrl.ToString() : string.Empty));
                writer.WriteEndElement(); // End of RssUrl

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
        /// �ݒ��ۑ����Ă����t�@�C�����폜����
        /// </summary>
        public void DeleteUserSettingFile()
        {
            if (File.Exists(GetSettingPath()))
            {
                File.Delete(GetSettingPath());
            }
        }

        /// <summary>
        /// �t�B���^�[���ύX���ꂽ�ꍇ�ɔ�������C�x���g
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
