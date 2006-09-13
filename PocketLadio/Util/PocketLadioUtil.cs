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
    public sealed class PocketLadioUtil
    {
        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PocketLadioUtil()
        {
        }

        /// <summary>
        /// ストリーミングを再生する。
        /// 再生用プログラムが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="url">ストリーミングのURL</param>
        public static void PlayStreaming(string streamingUrl)
        {
            // 再生用メディアプレイヤーが見つからない場合には例外を投げる
            if (File.Exists(UserSetting.MediaPlayerPath) == false)
            {
                throw new FileNotFoundException("Not found media player.");
            }

            Process.CreateProcess(UserSetting.MediaPlayerPath, streamingUrl);
        }

        /// <summary>
        /// Webサイトにアクセスする。
        /// ブラウザが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="url">WebサイトのURL</param>
        public static void AccessWebsite(string websiteUrl)
        {
            // ブラウザが見つからない場合には例外を投げる
            if (File.Exists(UserSetting.BrowserPath) == false)
            {
                throw new FileNotFoundException("Not found web browser.");
            }

            Process.CreateProcess(UserSetting.BrowserPath, websiteUrl);
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
