using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using PocketLadio.Util;

namespace PocketLadio
{
    /// <summary>
    /// ���[�e�B���e�B���\�b�h��PocketLadio�̌ŗL�����L�q���Ă���N���X
    /// </summary>
    public class Controller
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
        private const string versionNumber = "0.13";

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

        /// <summary>
        /// �w�b�h���C���`�F�b�N�^�C�}�[�̏��
        /// </summary>
        private const int headlineCheckTimerMaximumMillSec = 600000;

        /// <summary>
        /// �w�b�h���C���`�F�b�N�^�C�}�[�̏��
        /// </summary>
        public static int HeadlineCheckTimerMaximumMillSec
        {
            get { return headlineCheckTimerMaximumMillSec; }
        }

        /// <summary>
        ///�w�b�h���C���`�F�b�N�^�C�}�[�̉��� 
        /// </summary>
        private const int headlineCheckTimerMinimumMillSec = 20000;

        /// <summary>
        ///�w�b�h���C���`�F�b�N�^�C�}�[�̉��� 
        /// </summary>
        public static int HeadlineCheckTimerMinimumMillSec
        {
            get { return headlineCheckTimerMinimumMillSec; }
        }

        /// <summary>
        /// �V���O���g���̂��߃v���C�x�[�g
        /// </summary>
        private Controller()
        {
        }

        /// <summary>
        /// �ݒ�̓ǂݍ���
        /// </summary>
        public static void LoadSettings()
        {
            UserSetting.LoadSetting();
        }

        /// <summary>
        /// �X�g���[�~���O���Đ�����
        /// </summary>
        /// <param name="url">�X�g���[�~���O��URL</param>
        public static void PlayStreaming(string url)
        {
            Process.CreateProcess(UserSetting.MediaPlayerPath, url);
        }

        /// <summary>
        /// Web�T�C�g�ɃA�N�Z�X����
        /// </summary>
        /// <param name="url">Web�T�C�g��URL</param>
        public static void AccessWebSite(string url)
        {
            Process.CreateProcess(UserSetting.BrowserPath, url);
        }

        /// <summary>
        /// �A�v���P�[�V�����̎��s�f�B���N�g���̃p�X��Ԃ�
        /// </summary>
        /// <returns>�A�v���P�[�V�����̎��s�f�B���N�g���̃p�X</returns>
        public static string GetExecutablePath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        }
    }
}
