using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using PocketLadio.Util;

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̐ݒ��ێ�����N���X
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL CSV
        /// </summary>
        private string headlineCsvUrl = "http://yp.ladio.livedoor.jp/stats/list.csv";

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL CSV
        /// </summary>
        public string HeadlineCsvUrl
        {
            get { return headlineCsvUrl; }
            set { headlineCsvUrl = value; }
        }

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL XML
        /// </summary>
        private string headlineXmlUrl = "http://yp.ladio.livedoor.jp/stats/list.xml";

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL XML
        /// </summary>
        public string HeadlineXmlUrl
        {
            get { return headlineXmlUrl; }
            set { headlineXmlUrl = value; }
        }

        /// <summary>
        /// �w�b�h���C���̎擾���@
        /// </summary>
        private HeadlineGetTypeEnum headlineGetType = HeadlineGetTypeEnum.Cvs;

        /// <summary>
        /// �w�b�h���C���̎擾���@
        /// </summary>
        public HeadlineGetTypeEnum HeadlineGetType
        {
            get { return headlineGetType; }
            set { headlineGetType = value; }
        }

        /// <summary>
        /// �˂Ƃ炶�w�b�h���C���̎擾���@�̗�
        /// </summary>
        public enum HeadlineGetTypeEnum
        {
            Cvs, Xml
        };

        /// <summary>
        /// �˂Ƃ炶�w�b�h���C���̕\�����@
        /// </summary>
        private string headlineViewType = "[[NAME]] - [[GENRE]] ([[CLN]]/[[CLNS]])";

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
        private readonly Headline ParentHeadline;

        /// <summary>
        /// �ݒ�̃R���X�g���N�^
        /// </summary>
        /// <param name="ParentHeadline">�e�w�b�h���C��</param>
        public UserSetting(Headline parentHeadline)
        {
            this.ParentHeadline = parentHeadline;
        }

        /// <summary>
        /// �˂Ƃ炶�̐ݒ�t�@�C���̕ۑ��ꏊ��Ԃ�
        /// </summary>
        /// <returns>�ݒ�t�@�C���̕ۑ��ꏊ</returns>
        private string GetSettingPath()
        {
            // �A�v���P�[�V�����̎��s�f�B���N�g�� + �A�v���P�[�V�����̐ݒ�t�@�C��
            return PocketLadioUtil.GetExecutablePath() + "\\" + "Setting.Netladio." + ParentHeadline.GetID() + ".xml";
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
        /// �˂Ƃ炶�̐ݒ���t�@�C���ɕۑ�
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
