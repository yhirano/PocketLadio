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
        private string headlineViewType = "[[SERVERNAME]]";

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

                writer.WriteStartElement("AboveBitRate");
                writer.WriteAttributeString("use", FilterAboveBitRateUse.ToString());
                writer.WriteAttributeString("bitrate", FilterAboveBitRate.ToString());
                writer.WriteEndElement(); // End of AboveBitRate

                writer.WriteStartElement("BelowBitRate");
                writer.WriteAttributeString("use", FilterBelowBitRateUse.ToString());
                writer.WriteAttributeString("bitrate", FilterBelowBitRate.ToString());
                writer.WriteEndElement(); // End of BelowBitRate

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
