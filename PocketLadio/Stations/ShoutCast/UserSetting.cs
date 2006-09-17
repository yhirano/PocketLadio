#region �f�B���N�e�B�u���g�p����

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using PocketLadio.Utility;

#endregion

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// SHOUTcast�̐ݒ��ێ�����N���X
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// SHOUTcast��URL
        /// </summary>
        public readonly static Uri ShoutcastUrl = new Uri("http://www.shoutcast.com/");

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
        /// �ő�r�b�g���[�g�̐ݒ�e�[�u���̃L�[
        /// </summary>
        private string maxBitRateKey = "All";

        /// <summary>
        /// �ő�r�b�g���[�g�̐ݒ�e�[�u���̃L�[
        /// </summary>
        public string MaxBitRateKey
        {
            get { return maxBitRateKey; }
            set { maxBitRateKey = value; }
        }

        /// <summary>
        /// �ő�r�b�g���[�g�̐ݒ�e�[�u���B
        /// key => �ݒ�, value => �r�b�g���[�g���l
        /// </summary>
        private static Hashtable maxBitRateTable =
            new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant,
            CaseInsensitiveComparer.DefaultInvariant);

        /// <summary>
        /// �ő�r�b�g���[�g�̐ݒ�e�[�u���B
        /// key => �ݒ�, value => �r�b�g���[�g���l
        /// </summary>
        public static Hashtable MaxBitRateTable
        {
            get { return UserSetting.maxBitRateTable; }
        }

        /// <summary>
        /// �ő�r�b�g���[�g
        /// </summary>
        public string MaxBitRate
        {
            get
            {
                return ((maxBitRateTable.ContainsKey(maxBitRateKey) == true) ?
                    (string)maxBitRateTable[maxBitRateKey] : "");
            }
        }

        /// <summary>
        /// �w�b�h���C���擾��
        /// </summary>
        private int perView = 10;

        /// <summary>
        /// �w�b�h���C���擾��
        /// </summary>
        public int PerView
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
        /// �˂Ƃ炶�w�b�h���C���̕\�����@
        /// </summary>
        private string headlineViewType = "[[RANK]] [[TITLE]]";

        /// <summary>
        /// �˂Ƃ炶�w�b�h���C���̕\�����@
        /// </summary>
        public string HeadlineViewType
        {
            get { return headlineViewType; }
            set { headlineViewType = value; }
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
        /// �˂Ƃ炶�̐ݒ�t�@�C���̕ۑ��ꏊ��Ԃ�
        /// </summary>
        /// <returns>�ݒ�t�@�C���̕ۑ��ꏊ</returns>
        private string GetSettingPath()
        {
            // �A�v���P�[�V�����̎��s�f�B���N�g�� + �A�v���P�[�V�����̐ݒ�t�@�C��
            return PocketLadioUtility.GetExecutablePath() + @"\" + "Setting.SHOUTcast." + parentHeadline.GetId() + ".xml";
        }

        /// <summary>
        /// �˂Ƃ炶�̐ݒ���t�@�C������ǂݍ���
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

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "SearchWord")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                SearchWord = reader.GetAttribute("word");
                            }
                        } // End of SearchWord
                        else if (reader.LocalName == "MaxBitRate")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                maxBitRateKey = reader.GetAttribute("key");
                            }
                        } // End of MaxBitRate
                        /*
                        else if (reader.LocalName == "PerView")
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                try
                                {
                                    PerView = int.Parse(reader.GetAttribute("view"));
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
        /// �˂Ƃ炶�̐ݒ���t�@�C���ɕۑ�
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

                writer.WriteStartElement("MaxBitRate");
                writer.WriteAttributeString("key", maxBitRateKey);
                writer.WriteEndElement(); // End of MaxBitRate

                writer.WriteStartElement("PerView");
                writer.WriteAttributeString("view", PerView.ToString());
                writer.WriteEndElement(); // End of PerView

                writer.WriteStartElement("IgnoreHtmlAnalyze");
                writer.WriteAttributeString("firstto", IgnoreHtmlAnalyzeFirstTo.ToString());
                writer.WriteAttributeString("endfrom", IgnoreHtmlAnalyzeEndFrom.ToString());
                writer.WriteEndElement(); // End of IgnoreHtmlAnalyze

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