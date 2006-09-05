using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using PocketLadio.Util;
using System.Xml;


namespace PocketLadio.Util
{
    /// <summary>
    /// PocketLadioのユーティリティ
    /// </summary>
    public class PocketLadioUtil
    {
        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PocketLadioUtil()
        {
        }

        /// <summary>
        /// ストリーミングを再生する
        /// </summary>
        /// <param name="url">ストリーミングのURL</param>
        public static void PlayStreaming(string url)
        {
            Process.CreateProcess(UserSetting.MediaPlayerPath, url);
        }

        /// <summary>
        /// Webサイトにアクセスする
        /// </summary>
        /// <param name="url">WebサイトのURL</param>
        public static void AccessWebSite(string url)
        {
            Process.CreateProcess(UserSetting.BrowserPath, url);
        }

        /// <summary>
        /// アプリケーションの実行ディレクトリのパスを返す
        /// </summary>
        /// <returns>アプリケーションの実行ディレクトリのパス</returns>
        public static string GetExecutablePath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        }
    }
}
