#region �f�B���N�e�B�u���g�p����

using System;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// PocketLadio�̌ŗL�����L�q���Ă���N���X
    /// </summary>
    public sealed class PocketLadioInfo
    {
        #region �A�v���P�[�V�����ŗL�̏��

        /// <summary>
        /// �A�v���P�[�V������
        /// </summary>
        private const string APPLICATION_NAME = "PocketLadio";

        /// <summary>
        /// �A�v���P�[�V������
        /// </summary>
        public static string ApplicationName
        {
            get { return APPLICATION_NAME; }
        }

        /// <summary>
        /// �A�v���P�[�V�����̃o�[�W����
        /// </summary>
        private const string VERSION_NUMBER = "0.19";

        /// <summary>
        /// �A�v���P�[�V�����̃o�[�W����
        /// </summary>
        public static string VersionNumber
        {
            get { return VERSION_NUMBER; }
        }

        /// <summary>
        /// ���쌠���
        /// </summary>
        private const string COPYRIGHT = "Copyright (C) 2005-2006 Uraroji";

        /// <summary>
        /// ���쌠���
        /// </summary>
        public static string Copyright
        {
            get { return COPYRIGHT; }
        }

        #endregion

        #region �A�v���P�[�V�����̐ݒ�

        /// <summary>
        /// �w�b�h���C���`�F�b�N�^�C�}�[�̏��
        /// </summary>
        private const int HEADLINE_CHECK_TIMER_MAXIMUM_MILL_SEC = 600000;

        /// <summary>
        /// �w�b�h���C���`�F�b�N�^�C�}�[�̏��
        /// </summary>
        public static int HeadlineCheckTimerMaximumMillSec
        {
            get { return HEADLINE_CHECK_TIMER_MAXIMUM_MILL_SEC; }
        }

        /// <summary>
        ///�w�b�h���C���`�F�b�N�^�C�}�[�̉��� 
        /// </summary>
        private const int HEADLINE_CHECK_TIMER_MINIMUM_MILL_SEC = 20000;

        /// <summary>
        ///�w�b�h���C���`�F�b�N�^�C�}�[�̉��� 
        /// </summary>
        public static int HeadlineCheckTimerMinimumMillSec
        {
            get { return HEADLINE_CHECK_TIMER_MINIMUM_MILL_SEC; }
        }

        /// <summary>
        /// Web�ڑ����̃^�C���A�E�g����
        /// </summary>
        private const int WEB_REQUEST_TIMEOUT_MILL_SEC = 20000;

        /// <summary>
        /// Web�ڑ����̃^�C���A�E�g����
        /// </summary>
        public static int WebRequestTimeoutMillSec
        {
            get { return WEB_REQUEST_TIMEOUT_MILL_SEC; }
        }

        /// <summary>
        /// �l�b�g�A�N�Z�X����UserAgent�ݒ�
        /// </summary>
        private const string USER_AGENT = APPLICATION_NAME + "/" + VERSION_NUMBER;

        /// <summary>
        /// �l�b�g�A�N�Z�X����UserAgent�ݒ�
        /// </summary>
        public static string UserAgent
        {
            get { return USER_AGENT; }
        }

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL CSV
        /// </summary>
        private const string NETLADIO_HEADLINE_CVS_URL = "http://yp.ladio.livedoor.jp/stats/list.csv";

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL CSV
        /// </summary>
        public static string NetladioHeadlineCsvUrl
        {
            get { return NETLADIO_HEADLINE_CVS_URL; }
        } 

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL XML
        /// </summary>
        private const string NETLADIO_HEADLINE_XML_URL = "http://yp.ladio.livedoor.jp/stats/list.xml";

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL XML
        /// </summary>
        public static string NetladioHeadlineXmlUrl
        {
            get { return NETLADIO_HEADLINE_XML_URL; }
        } 

        /// <summary>
        /// Podcast��MIME�^�C�v�̗D��x�t�@�C��
        /// </summary>
        private const string RSS_PODCAST_MIME_PRIORITY_FILE
            = "PocketLadio.Resource.RssPodcastMimePriority.txt";

        /// <summary>
        /// Podcast��MIME�^�C�v�̗D��x�t�@�C��
        /// </summary>
        public static string RssPodcastMimePriorityFile
        {
            get { return RSS_PODCAST_MIME_PRIORITY_FILE; }
        } 

        /// <summary>
        /// SHOUTcast��Max Bit Rate�ݒ�̐ݒ�\���Ǝ��ےl�������t�@�C��
        /// </summary>
        private const string SHOUTCAST_MAX_BIT_RATE_SETTING_FILE
            = "PocketLadio.Resource.ShoutCastMaxBitRateSetting.txt";

        /// <summary>
        /// SHOUTcast��Max Bit Rate�ݒ�̐ݒ�\���Ǝ��ےl�������t�@�C��
        /// </summary>
        public static string ShoutcastMaxBitRateSettingFile
        {
            get { return SHOUTCAST_MAX_BIT_RATE_SETTING_FILE; }
        } 

        /// <summary>
        /// SHOUTcast�̃w�b�h���C���\�����̋K��l�t�@�C��
        /// </summary>
        private const string SHOUTCAST_PER_VIEW_SETTING_FILE
            = "PocketLadio.Resource.ShoutCastPerViewSetting.txt";

        /// <summary>
        /// SHOUTcast�̃w�b�h���C���\�����̋K��l�t�@�C��
        /// </summary>
        public static string ShoutcastPerViewSettingFile
        {
            get { return SHOUTCAST_PER_VIEW_SETTING_FILE; }
        } 

        /// <summary>
        /// SHOUTcast��URL
        /// </summary>
        private const string SHOUTCAST_URL = "http://www.shoutcast.com/";

        /// <summary>
        /// SHOUTcast��URL
        /// </summary>
        public static string ShoutcastUrl
        {
            get { return SHOUTCAST_URL; }
        } 

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C��
        /// </summary>
        private const string SETTING_FILE = "Setting.xml";

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C��
        /// </summary>
        public static string SettingFile
        {
            get { return SETTING_FILE; }
        } 


        #endregion

        /// <summary>
        /// �V���O���g���̂��߃v���C�x�[�g
        /// </summary>
        private PocketLadioInfo()
        {
        }
    }
}
