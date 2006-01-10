using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using PocketLadio.Util;

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
        public const string VersionNumber = "0.6";
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
        /// 設定の読み込み
        /// </summary>
        public static void LoadSettings()
        {
            UserSetting.LoadSetting();
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
