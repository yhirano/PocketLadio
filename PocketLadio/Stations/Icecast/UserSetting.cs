#region �f�B���N�e�B�u���g�p����

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using MiscPocketCompactLibrary.Reflection;

#endregion

namespace PocketLadio.Stations.Icecast
{
    /// <summary>
    /// Icecast�̐ݒ��ێ�����N���X
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// �ԑg���擾���鐔�̃f�t�H���g
        /// </summary>
        private const int DEFAULT_FETCH_CHANNEL_NUM = 100;

        /// <summary>
        /// �ԑg���擾���鐔
        /// </summary>
        private int fetchChannelNum = DEFAULT_FETCH_CHANNEL_NUM;

        /// <summary>
        /// �ԑg���擾���鐔
        /// </summary>
        public int FetchChannelNum
        {
            get { return fetchChannelNum; }
            set
            {
                if (value > 0)
                {
                    fetchChannelNum = value;
                }
                else
                {
                    fetchChannelNum = DEFAULT_FETCH_CHANNEL_NUM;
                }
            }
        }

        /// <summary>
        /// �S�Ă̔ԑg���擾����ꍇ�́AFetchChannelNum��ALL_CHANNEL_FETCH�ɂȂ�B
        /// </summary>
        public const int ALL_CHANNEL_FETCH = -1;

        /// <summary>
        /// Icecast�̕\�����@
        /// </summary>
        private string headlineViewType = "[[SREVERNAME]]";

        /// <summary>
        /// Icecast�̕\�����@
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
        /// �S�Ă̔ԑg���擾����悤�ɐݒ肷��
        /// </summary>
        public void FetchNumAllChannel()
        {
            fetchChannelNum = ALL_CHANNEL_FETCH;
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
        /// Podcast�̐ݒ�t�@�C���̕ۑ��ꏊ��Ԃ�
        /// </summary>
        /// <returns>�ݒ�t�@�C���̕ۑ��ꏊ</returns>
        private string GetSettingPath()
        {
            // �A�v���P�[�V�����̎��s�f�B���N�g�� + �A�v���P�[�V�����̐ݒ�t�@�C��
            return AssemblyUtility.GetExecutablePath() + @"\" + "Setting.Icecast." + parentHeadline.GetId() + ".xml";
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

            ArrayList alFilterWords = new ArrayList();

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
                        if (reader.LocalName == "Filter")
                        {
                            inFilterFlag = true;
                        }
                        else if (reader.LocalName == "FetchChannelNum")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                try
                                {
                                    fetchChannelNum = int.Parse(reader.GetAttribute("num"));
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
                        }// End of FetchChannelNum
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

                writer.WriteStartElement("FetchChannelNum");
                writer.WriteAttributeString("num", FetchChannelNum.ToString());
                writer.WriteEndElement(); // End of FetchChannelNum

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