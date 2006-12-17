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
        private const string VERSION_NUMBER = "0.26";

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
        /// 水平モード時のVGAとQVGAのサイズの境界条件値。
        /// メインフォームの幅がこの値より大きいとVGAと見なす。
        /// </summary>
        private const int VGA_WINDOW_WIDTH_BOUNDARY_CONDITION_HORIZON = 320;

        /// <summary>
        /// 水平モード時のVGAとQVGAのサイズの境界条件値。
        /// メインフォームの幅がこの値より大きいとVGAと見なす。
        /// </summary>
        public static int VgaWindowWidthBoundaryConditionHorizon
        {
            get { return VGA_WINDOW_WIDTH_BOUNDARY_CONDITION_HORIZON; }
        } 

        /// <summary>
        /// 垂直モード時のVGAとQVGAのサイズの境界条件値。
        /// メインフォームの幅がこの値より大きいとVGAと見なす。
        /// </summary>
        private const int VGA_WINDOW_WIDTH_BOUNDARY_CONDITION_VERTICAL = 240;

        /// <summary>
        /// 垂直モード時のVGAとQVGAのサイズの境界条件値。
        /// メインフォームの幅がこの値より大きいとVGAと見なす。
        /// </summary>
        public static int VgaWindowWidthBoundaryConditionVertical
        {
            get { return VGA_WINDOW_WIDTH_BOUNDARY_CONDITION_VERTICAL; }
        }

        /// <summary>
        /// 垂直モード時のVGAとQVGAのサイズの境界条件値。
        /// メインフォームの高さがこの値より小さいとSQVGAと見なす。
        /// </summary>
        private const int SQVGA_WINDOW_HEIGHT_BOUNDARY_CONDITION = 190;

        /// <summary>
        /// 垂直モード時のVGAとQVGAのサイズの境界条件値。
        /// メインフォームの高さがこの値より小さいとSQVGAと見なす。
        /// </summary>
        public static int SqvgaWindowWidthBoundaryCondition
        {
            get { return SQVGA_WINDOW_HEIGHT_BOUNDARY_CONDITION; }
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
        /// SHOUTcastのURL
        /// </summary>
        private const string SHOUTCAST_URL = "http://www.shoutcast.com";

        /// <summary>
        /// SHOUTcastのURL
        /// </summary>
        public static string ShoutcastUrl
        {
            get { return SHOUTCAST_URL; }
        }

        /// <summary>
        /// SHOUTcastの番組表取得数のリスト
        /// </summary>
        private static string[] shoutcastPerViewNums = { "5", "10", "25", "30", "50", "100" };

        /// <summary>
        /// SHOUTcastの番組表取得数のリスト
        /// </summary>
        public static string[] ShoutcastPerViewNums
        {
            get { return PocketLadioInfo.shoutcastPerViewNums; }
        }

        /// <summary>
        /// 番組がプレイリストだった場合に作成するファイル名（拡張子はつけない）
        /// </summary>
        private const string GENERATE_PLAYLIST_FILE_NAME = "PocketLadio_playlist";

        /// <summary>
        /// 番組がプレイリストだった場合に作成するファイル名（拡張子はつけない）
        /// </summary>
        public static string GeneratePlayListFileName
        {
            get { return GENERATE_PLAYLIST_FILE_NAME; }
        }

        /// <summary>
        /// プレイリストと見なす拡張子
        /// </summary>
        private static string[] playListExtensions = { ".m3u", ".pls" };

        /// <summary>
        /// プレイリストと見なす拡張子
        /// </summary>
        public static string[] PlayListExtensions
        {
            get { return PocketLadioInfo.playListExtensions; }
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

        /// <summary>
        /// 例外に出力するログファイル
        /// </summary>
        private const string EXCEPTION_LOG_FILE = "PocketLadioExceptionLog.log";

        /// <summary>
        /// 例外に出力するログファイル
        /// </summary>
        public static string ExceptionLogFile
        {
            get { return EXCEPTION_LOG_FILE; }
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
