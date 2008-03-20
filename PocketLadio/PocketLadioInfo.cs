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
        private const string VERSION_NUMBER = "0.41";

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
        private const string COPYRIGHT = "Copyright (C) 2005-2008 Y.Hirano";

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
        /// ���f�B�A�v���[���[�̃p�X�̃f�t�H���g�ݒ�
        /// </summary>
        private const string DEFAULT_MEDIA_PLAYER_PATH = @"\Program Files\TCPMP\player.exe";

        /// <summary>
        /// ���f�B�A�v���[���[�̃p�X�̃f�t�H���g�ݒ�
        /// </summary>
        public static string DefaultMediaPlayerPath
        {
            get { return DEFAULT_MEDIA_PLAYER_PATH; }
        }

        /// <summary>
        /// �u���E�U�̃p�X�̃f�t�H���g�ݒ�
        /// </summary>
        private const string DEFAULT_BROWSER_PATH = @"\Windows\iexplore.exe";

        /// <summary>
        /// �u���E�U�̃p�X�̃f�t�H���g�ݒ�
        /// </summary>
        public static string DefaultBrowserPath
        {
            get { return DEFAULT_BROWSER_PATH; }
        }

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
        /// �w�b�h���C���`�F�b�N�^�C�}�[�̉��� 
        /// </summary>
        private const int HEADLINE_CHECK_TIMER_MINIMUM_MILL_SEC = 20000;

        /// <summary>
        /// �w�b�h���C���`�F�b�N�^�C�}�[�̉��� 
        /// </summary>
        public static int HeadlineCheckTimerMinimumMillSec
        {
            get { return HEADLINE_CHECK_TIMER_MINIMUM_MILL_SEC; }
        }

        /// <summary>
        /// �ԑg�\�̃t�H���g�T�C�Y�̏��
        /// </summary>
        private const int HEADLINE_LIST_BOX_FONT_SIZE_MAXIMUM_PT = 18;

        /// <summary>
        /// �ԑg�\�̃t�H���g�T�C�Y�̏��
        /// </summary>
        public static int HeadlineListBoxFontSizeMaximumPt
        {
            get { return HEADLINE_LIST_BOX_FONT_SIZE_MAXIMUM_PT; }
        }

        /// <summary>
        /// �ԑg�\�̃t�H���g�T�C�Y�̉���
        /// </summary>
        private const int HEADLINE_LIST_BOX_FONT_SIZE_MINIMUM_PT = 6;

        /// <summary>
        /// �ԑg�\�̃t�H���g�T�C�Y�̉���
        /// </summary>
        public static int HeadlineListBoxFontSizeMinimumPt
        {
            get { return HEADLINE_LIST_BOX_FONT_SIZE_MINIMUM_PT; }
        }

        /// <summary>
        /// �ԑg�\�̃f�t�H���g�t�H���g�T�C�Y
        /// </summary>
        private const int HEADLINE_LIST_BOX_DEFAULT_FONT_SIZE = 9;

        /// <summary>
        /// �ԑg�\�̃f�t�H���g�t�H���g�T�C�Y
        /// </summary>
        public static int HeadlineListBoxDefaultFontSize
        {
            get { return HEADLINE_LIST_BOX_DEFAULT_FONT_SIZE; }
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
        /// �ԑg���v���C���X�g�������ꍇ�ɍ쐬����t�@�C�����i�g���q�͂��Ȃ��j
        /// </summary>
        private const string GENERATE_PLAYLIST_FILE_NAME = "PocketLadio_playlist";

        /// <summary>
        /// �ԑg���v���C���X�g�������ꍇ�ɍ쐬����t�@�C�����i�g���q�͂��Ȃ��j
        /// </summary>
        public static string GeneratePlayListFileName
        {
            get { return GENERATE_PLAYLIST_FILE_NAME; }
        }

        /// <summary>
        /// �v���C���X�g�ƌ��Ȃ��g���q
        /// </summary>
        private static string[] playListExtensions = { ".m3u", ".pls" };

        /// <summary>
        /// �v���C���X�g�ƌ��Ȃ��g���q
        /// </summary>
        public static string[] PlayListExtensions
        {
            get { return PocketLadioInfo.playListExtensions; }
        }

        /// <summary>
        /// ���C���t�H�[���̕��B
        /// �t�H�[���f�U�C���͂��̕����x�[�X��Control��u���Ă���B
        /// </summary>
        private const int FORM_BASE_WIDRH = 240;

        /// <summary>
        /// ���C���t�H�[���̕��B
        /// �t�H�[���f�U�C���͂��̕����x�[�X��Control��u���Ă���B
        /// </summary>
        public static int FormBaseWidth
        {
            get { return FORM_BASE_WIDRH; }
        }

        /// <summary>
        /// ���C���t�H�[���̍����B
        /// �t�H�[���f�U�C���͂��̍������x�[�X��Control��u���Ă���B
        /// </summary>
        private const int FORM_BASE_HIGHT = 268;

        /// <summary>
        /// ���C���t�H�[���̍����B
        /// �t�H�[���f�U�C���͂��̍������x�[�X��Control��u���Ă���B
        /// </summary>
        public static int FormBaseHight
        {
            get { return FORM_BASE_HIGHT; }
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

        /// <summary>
        /// ��O�ɏo�͂��郍�O�t�@�C��
        /// </summary>
        private const string EXCEPTION_LOG_FILE = "PocketLadioExceptionLog.log";

        /// <summary>
        /// ��O�ɏo�͂��郍�O�t�@�C��
        /// </summary>
        public static string ExceptionLogFile
        {
            get { return EXCEPTION_LOG_FILE; }
        }

#if SHOUTCAST_HTTP_LOG
        /// <summary>
        /// Shoutcast��Http�ʐM���e�̃��O�t�@�C��
        /// </summary>
        private const string SHOUTCAST_HTTP_LOG = "ShoutcastHttpLog.log";

        /// <summary>
        /// Shoutcast��Http�ʐM���e�̃��O�t�@�C��
        /// </summary>
        public static string ShoutcastHttpLog
        {
            get { return SHOUTCAST_HTTP_LOG; }
        }
#endif

        #endregion

        /// <summary>
        /// �V���O���g���̂��߃v���C�x�[�g
        /// </summary>
        private PocketLadioInfo()
        {
        }
    }
}
