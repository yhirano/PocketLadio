using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Xml;
using PocketLadio.Stations.Netladio;
using PocketLadio.Stations.RssPodcast;


namespace PocketLadio.Utility
{
    /// <summary>
    /// PocketLadioのユーティリティ
    /// </summary>
    public sealed class PocketLadioUtility
    {
        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PocketLadioUtility()
        {
        }

        /// <summary>
        /// PocketLadio起動時の初期化処理。
        /// </summary>
        public static void StartUpInitialize()
        {
            try
            {
                // 設定を読み込む
                UserSetting.LoadSetting();

                PocketLadio.Stations.Netladio.Headline.StartUpInitialize();
                PocketLadio.Stations.RssPodcast.Headline.StartUpInitialize();
            }
            catch (XmlException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }

        }

        /// <summary>
        /// ストリーミングを再生する。
        /// 再生用プログラムが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="url">ストリーミングのURL</param>
        public static void PlayStreaming(Uri streamingUrl)
        {
            // 再生用メディアプレイヤーが見つからない場合には例外を投げる
            if (File.Exists(UserSetting.MediaPlayerPath) == false)
            {
                throw new FileNotFoundException("Not found media player.");
            }
            if (streamingUrl == null)
            {
                return;
            }

            Process.CreateProcess(UserSetting.MediaPlayerPath, streamingUrl.ToString());
        }

        /// <summary>
        /// Webサイトにアクセスする。
        /// ブラウザが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="url">WebサイトのURL</param>
        public static void AccessWebsite(Uri websiteUrl)
        {
            // ブラウザが見つからない場合には例外を投げる
            if (File.Exists(UserSetting.BrowserPath) == false)
            {
                throw new FileNotFoundException("Not found web browser.");
            }
            if (websiteUrl == null)
            {
                return;
            }

            Process.CreateProcess(UserSetting.BrowserPath, websiteUrl.ToString());
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
