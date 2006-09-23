#region ディレクティブを使用する

using System;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// PocketLadioの固有情報を記述しているクラス
    /// </summary>
    public sealed class PocketLadioInfo
    {
        #region アプリケーション固有の情報

        /// <summary>
        /// アプリケーション名
        /// </summary>
        private const string applicationName = "PocketLadio";

        /// <summary>
        /// アプリケーション名
        /// </summary>
        public static string ApplicationName
        {
            get { return applicationName; }
        }

        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        private const string versionNumber = "0.19";

        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        public static string VersionNumber
        {
            get { return versionNumber; }
        }

        /// <summary>
        /// 著作権情報
        /// </summary>
        private const string copyright = "Copyright (C) 2005-2006 Uraroji";

        /// <summary>
        /// 著作権情報
        /// </summary>
        public static string Copyright
        {
            get { return copyright; }
        }

        #endregion

        #region アプリケーションの設定

        /// <summary>
        /// ヘッドラインチェックタイマーの上限
        /// </summary>
        private const int HEADLINE_CHECK_TIMER_MAXIMUM_MILL_SEC = 600000;

        /// <summary>
        /// ヘッドラインチェックタイマーの上限
        /// </summary>
        public static int HeadlineCheckTimerMaximumMillSec
        {
            get { return HEADLINE_CHECK_TIMER_MAXIMUM_MILL_SEC; }
        }

        /// <summary>
        ///ヘッドラインチェックタイマーの下限 
        /// </summary>
        private const int HEADLINE_CHECK_TIMER_MINIMUM_MILL_SEC = 20000;

        /// <summary>
        ///ヘッドラインチェックタイマーの下限 
        /// </summary>
        public static int HeadlineCheckTimerMinimumMillSec
        {
            get { return HEADLINE_CHECK_TIMER_MINIMUM_MILL_SEC; }
        }

        /// <summary>
        /// Web接続時のタイムアウト時間
        /// </summary>
        private const int WEB_REQUEST_TIMEOUT_MILL_SEC = 20000;

        /// <summary>
        /// Web接続時のタイムアウト時間
        /// </summary>
        public static int WebRequestTimeoutMillSec
        {
            get { return WEB_REQUEST_TIMEOUT_MILL_SEC; }
        }

        /// <summary>
        /// ネットアクセス時のUserAgent設定
        /// </summary>
        private const string USER_AGENT = applicationName + "/" + versionNumber;

        /// <summary>
        /// ネットアクセス時のUserAgent設定
        /// </summary>
        public static string UserAgent
        {
            get { return USER_AGENT; }
        }

        #endregion

        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PocketLadioInfo()
        {
        }
    }
}
