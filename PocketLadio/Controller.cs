using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections;
using PocketLadio.Netladio;

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
        public const string ApplicationName = "PocketLadio";
        /// <summary>
        /// �A�v���P�[�V�����̃o�[�W����
        /// </summary>
        public const string VersionNumber = "0.5";
        /// <summary>
        /// ���쌠���
        /// </summary>
        public const string Copyright = "Copyright (C) 2005-2006 Uraroji";
        #endregion

        /// <summary>
        /// �w�b�h���C���`�F�b�N�^�C�}�[�̏��
        /// </summary>
        public const int HeadlineCheckTimerMaximumMillSec = 600000;
        /// <summary>
        ///�w�b�h���C���`�F�b�N�^�C�}�[�̉��� 
        /// </summary>
        public const int HeadlineCheckTimerMinimumMillSec = 20000;

        /// <summary>
        /// �V���O���g���̂��߃v���C�x�[�g
        /// </summary>
        private Controller()
        {
        }

        /// <summary>
        /// �v���Z�X�𐶐�����
        /// </summary>
        /// <param name="lpApplicationName">���s�\���W���[���̖��O</param>
        /// <param name="lpCommandLine">�R�}���h���C���̕�����</param>
        /// <param name="lpProcess"></param>
        /// <param name="lpThread"></param>
        /// <param name="bInheritHandles"></param>
        /// <param name="dwCreationFlags"></param>
        /// <param name="lpEnvironment"></param>
        /// <param name="lpCurrentDirectory"></param>
        /// <param name="lpStartupInfo"></param>
        /// <param name="lpProcessInformation"></param>
        /// <returns></returns>
        [DllImport("coredll.dll")]
        private static extern int CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            int lpProcess,
            int lpThread,
            bool bInheritHandles,
            uint dwCreationFlags,
            int lpEnvironment,
            int lpCurrentDirectory,
            int lpStartupInfo,
            int lpProcessInformation);

        /// <summary>
        /// �ݒ�̓ǂݍ���
        /// </summary>
        public static void LoadSettings()
        {
            UserSetting.LoadSetting();
        }

        /// <summary>
        /// �V���Ƀv���Z�X���N������
        /// </summary>
        /// <param name="applicationPath">���s����A�v���P�[�V�����̃p�X</param>
        /// <returns>�v���Z�X���N���ł�����</returns>
        private static int CreateProcess(string applicationPath)
        {
            if (File.Exists(applicationPath) == true)
            {
                return CreateProcess(applicationPath, null, 0, 0, false, 0, 0, 0, 0, 0);
            }

            return -1;
        }

        /// <summary>
        /// �V���Ƀv���Z�X���N������
        /// </summary>
        /// <param name="applicationPath">���s����A�v���P�[�V�����̃p�X</param>
        /// <param name="commandLine">�A�v���P�[�V�����ɓn���R�}���h���C��</param>
        /// <returns>�v���Z�X���N���ł�����</returns>
        private static int CreateProcess(string applicationPath, string commandLine)
        {
            if (File.Exists(applicationPath) == true)
            {
                return CreateProcess(applicationPath, commandLine, 0, 0, false, 0, 0, 0, 0, 0);
            }

            return -1;

        }

        /// <summary>
        /// �X�g���[�~���O���Đ�����
        /// </summary>
        /// <param name="url">�X�g���[�~���O��URL</param>
        public static void PlayStreaming(string url)
        {
            Controller.CreateProcess(UserSetting.MediaPlayerPath, url);
        }

        /// <summary>
        /// Web�T�C�g�ɃA�N�Z�X����
        /// </summary>
        /// <param name="url">Web�T�C�g��URL</param>
        public static void AccessWebSite(string url)
        {
            Controller.CreateProcess(UserSetting.BrowserPath, url);
        }

        /// <summary>
        /// �A�v���P�[�V�����̎��s�f�B���N�g���̃p�X��Ԃ�
        /// </summary>
        /// <returns>�A�v���P�[�V�����̎��s�f�B���N�g���̃p�X</returns>
        public static string GetExecutablePath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        }

        public static void PlaySound(string strFileName)
        {
            Helpers.PlaySound(strFileName, IntPtr.Zero, Helpers.PlaySoundFlags.SND_FILENAME | Helpers.PlaySoundFlags.SND_ASYNC);
        }

        internal class Helpers
        {
            [Flags]
            public enum PlaySoundFlags : int
            {
                SND_SYNC = 0x0000,		// play synchronously (default)
                SND_ASYNC = 0x0001,		// play asynchronously
                SND_NODEFAULT = 0x0002,	// silence (!default) if sound not found
                SND_MEMORY = 0x0004,	// pszSound points to a memory file
                SND_LOOP = 0x0008,		// loop the sound until next sndPlaySound
                SND_NOSTOP = 0x0010,	// don't stop any currently playing sound
                SND_NOWAIT = 0x00002000,	// don't wait if the driver is busy
                SND_ALIAS = 0x00010000,		// name is a registry alias
                SND_ALIAS_ID = 0x00110000,	// alias is a predefined ID
                SND_FILENAME = 0x00020000,	// name is file name
                SND_RESOURCE = 0x00040004	// name is resource name or atom
            }

            [DllImport("coredll")]
            public static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);
        }
    }
}
