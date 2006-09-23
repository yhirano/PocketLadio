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
        private const string applicationName = "PocketLadio";

        /// <summary>
        /// �A�v���P�[�V������
        /// </summary>
        public static string ApplicationName
        {
            get { return applicationName; }
        }

        /// <summary>
        /// �A�v���P�[�V�����̃o�[�W����
        /// </summary>
        private const string versionNumber = "0.19";

        /// <summary>
        /// �A�v���P�[�V�����̃o�[�W����
        /// </summary>
        public static string VersionNumber
        {
            get { return versionNumber; }
        }

        /// <summary>
        /// ���쌠���
        /// </summary>
        private const string copyright = "Copyright (C) 2005-2006 Uraroji";

        /// <summary>
        /// ���쌠���
        /// </summary>
        public static string Copyright
        {
            get { return copyright; }
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
        private const string USER_AGENT = applicationName + "/" + versionNumber;

        /// <summary>
        /// �l�b�g�A�N�Z�X����UserAgent�ݒ�
        /// </summary>
        public static string UserAgent
        {
            get { return USER_AGENT; }
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
