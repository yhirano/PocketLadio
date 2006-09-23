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
        private const string APPLICATION_NAME = "PocketLadio";

        /// <summary>
        /// アプリケーション名
        /// </summary>
        public static string ApplicationName
        {
            get { return APPLICATION_NAME; }
        }

        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        private const string VERSION_NUMBER = "0.19";

        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        public static string VersionNumber
        {
            get { return VERSION_NUMBER; }
        }

        /// <summary>
        /// 著作権情報
        /// </summary>
        private const string COPYRIGHT = "Copyright (C) 2005-2006 Uraroji";

        /// <summary>
        /// 著作権情報
        /// </summary>
        public static string Copyright
        {
            get { return COPYRIGHT; }
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
        private const string USER_AGENT = APPLICATION_NAME + "/" + VERSION_NUMBER;

        /// <summary>
        /// ネットアクセス時のUserAgent設定
        /// </summary>
        public static string UserAgent
        {
            get { return USER_AGENT; }
        }

        /// <summary>
        /// ねとらじのヘッドラインのURL CSV
        /// </summary>
        private const string NETLADIO_HEADLINE_CVS_URL = "http://yp.ladio.livedoor.jp/stats/list.csv";

        /// <summary>
        /// ねとらじのヘッドラインのURL CSV
        /// </summary>
        public static string NetladioHeadlineCsvUrl
        {
            get { return NETLADIO_HEADLINE_CVS_URL; }
        } 

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        private const string NETLADIO_HEADLINE_XML_URL = "http://yp.ladio.livedoor.jp/stats/list.xml";

        /// <summary>
        /// ねとらじのヘッドラインのURL XML
        /// </summary>
        public static string NetladioHeadlineXmlUrl
        {
            get { return NETLADIO_HEADLINE_XML_URL; }
        } 

        /// <summary>
        /// PodcastのMIMEタイプの優先度ファイル
        /// </summary>
        private const string RSS_PODCAST_MIME_PRIORITY_FILE
            = "PocketLadio.Resource.RssPodcastMimePriority.txt";

        /// <summary>
        /// PodcastのMIMEタイプの優先度ファイル
        /// </summary>
        public static string RssPodcastMimePriorityFile
        {
            get { return RSS_PODCAST_MIME_PRIORITY_FILE; }
        } 

        /// <summary>
        /// SHOUTcastのMax Bit Rate設定の設定表示と実際値を示すファイル
        /// </summary>
        private const string SHOUTCAST_MAX_BIT_RATE_SETTING_FILE
            = "PocketLadio.Resource.ShoutCastMaxBitRateSetting.txt";

        /// <summary>
        /// SHOUTcastのMax Bit Rate設定の設定表示と実際値を示すファイル
        /// </summary>
        public static string ShoutcastMaxBitRateSettingFile
        {
            get { return SHOUTCAST_MAX_BIT_RATE_SETTING_FILE; }
        } 

        /// <summary>
        /// SHOUTcastのヘッドライン表示数の規定値ファイル
        /// </summary>
        private const string SHOUTCAST_PER_VIEW_SETTING_FILE
            = "PocketLadio.Resource.ShoutCastPerViewSetting.txt";

        /// <summary>
        /// SHOUTcastのヘッドライン表示数の規定値ファイル
        /// </summary>
        public static string ShoutcastPerViewSettingFile
        {
            get { return SHOUTCAST_PER_VIEW_SETTING_FILE; }
        } 

        /// <summary>
        /// SHOUTcastのURL
        /// </summary>
        private const string SHOUTCAST_URL = "http://www.shoutcast.com/";

        /// <summary>
        /// SHOUTcastのURL
        /// </summary>
        public static string ShoutcastUrl
        {
            get { return SHOUTCAST_URL; }
        } 

        /// <summary>
        /// アプリケーションの設定ファイル
        /// </summary>
        private const string SETTING_FILE = "Setting.xml";

        /// <summary>
        /// アプリケーションの設定ファイル
        /// </summary>
        public static string SettingFile
        {
            get { return SETTING_FILE; }
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
