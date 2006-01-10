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
    /// ユーティリティメソッドとPocketLadioの固有情報を記述しているクラス
    /// </summary>
    public class Controller
    {
        #region アプリケーション固有の情報
        /// <summary>
        /// アプリケーション名
        /// </summary>
        public const string ApplicationName = "PocketLadio";
        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        public const string VersionNumber = "0.5";
        /// <summary>
        /// 著作権情報
        /// </summary>
        public const string Copyright = "Copyright (C) 2005-2006 Uraroji";
        #endregion

        /// <summary>
        /// ヘッドラインチェックタイマーの上限
        /// </summary>
        public const int HeadlineCheckTimerMaximumMillSec = 600000;
        /// <summary>
        ///ヘッドラインチェックタイマーの下限 
        /// </summary>
        public const int HeadlineCheckTimerMinimumMillSec = 20000;

        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private Controller()
        {
        }

        /// <summary>
        /// プロセスを生成する
        /// </summary>
        /// <param name="lpApplicationName">実行可能モジュールの名前</param>
        /// <param name="lpCommandLine">コマンドラインの文字列</param>
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
        /// 設定の読み込み
        /// </summary>
        public static void LoadSettings()
        {
            UserSetting.LoadSetting();
        }

        /// <summary>
        /// 新たにプロセスを起動する
        /// </summary>
        /// <param name="applicationPath">実行するアプリケーションのパス</param>
        /// <returns>プロセスが起動できたか</returns>
        private static int CreateProcess(string applicationPath)
        {
            if (File.Exists(applicationPath) == true)
            {
                return CreateProcess(applicationPath, null, 0, 0, false, 0, 0, 0, 0, 0);
            }

            return -1;
        }

        /// <summary>
        /// 新たにプロセスを起動する
        /// </summary>
        /// <param name="applicationPath">実行するアプリケーションのパス</param>
        /// <param name="commandLine">アプリケーションに渡すコマンドライン</param>
        /// <returns>プロセスが起動できたか</returns>
        private static int CreateProcess(string applicationPath, string commandLine)
        {
            if (File.Exists(applicationPath) == true)
            {
                return CreateProcess(applicationPath, commandLine, 0, 0, false, 0, 0, 0, 0, 0);
            }

            return -1;

        }

        /// <summary>
        /// ストリーミングを再生する
        /// </summary>
        /// <param name="url">ストリーミングのURL</param>
        public static void PlayStreaming(string url)
        {
            Controller.CreateProcess(UserSetting.MediaPlayerPath, url);
        }

        /// <summary>
        /// Webサイトにアクセスする
        /// </summary>
        /// <param name="url">WebサイトのURL</param>
        public static void AccessWebSite(string url)
        {
            Controller.CreateProcess(UserSetting.BrowserPath, url);
        }

        /// <summary>
        /// アプリケーションの実行ディレクトリのパスを返す
        /// </summary>
        /// <returns>アプリケーションの実行ディレクトリのパス</returns>
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
