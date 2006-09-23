﻿#region ディレクティブを使用する

using System;
using System.IO;
using MiscPocketCompactLibrary.Runtime.InteropServices;
using MiscPocketCompactLibrary.Net;

#endregion

namespace PocketLadio
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
        /// 音声ファイルを再生する。
        /// 再生用プログラムが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="filePath">音声ファイルのパス</param>
        public static void PlayStreaming(string filePath)
        {
            // 再生用メディアプレイヤーが見つからない場合には例外を投げる
            if (File.Exists(UserSetting.MediaPlayerPath) == false)
            {
                throw new FileNotFoundException("Not found media player.");
            }
            if (filePath == "")
            {
                return;
            }

            ProcessUtility.CreateProcess(UserSetting.MediaPlayerPath, filePath);
        }

        /// <summary>
        /// ストリーミングを再生する。
        /// 再生用プログラムが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="streamingUrl">ストリーミングのURL</param>
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

            ProcessUtility.CreateProcess(UserSetting.MediaPlayerPath, streamingUrl.ToString());
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

            ProcessUtility.CreateProcess(UserSetting.BrowserPath, websiteUrl.ToString());
        }

        /// <summary>
        /// Web上のストリームを返す
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Web上のストリーム</returns>
        public static Stream GetWebStream(Uri url)
        {
            WebStream ws = new WebStream(url);
            ws.ProxyUse = PocketLadio.UserSetting.ProxyUse;
            ws.ProxyServer = PocketLadio.UserSetting.ProxyServer;
            ws.ProxyPort = PocketLadio.UserSetting.ProxyPort;
            ws.TimeOut = PocketLadioInfo.WebRequestTimeoutMillSec;
            ws.UserAgent = PocketLadioInfo.UserAgent;
            Stream st = ws.GetWebStream();

            return st;
        }

        /// <summary>
        /// Web上のストリームをダウンロードする
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="fileName">保存するファイル名</param>
        public static void FetchFile(Uri url, string fileName)
        {
            WebStream ws = new WebStream(url);
            ws.ProxyUse = PocketLadio.UserSetting.ProxyUse;
            ws.ProxyServer = PocketLadio.UserSetting.ProxyServer;
            ws.ProxyPort = PocketLadio.UserSetting.ProxyPort;
            ws.TimeOut = PocketLadioInfo.WebRequestTimeoutMillSec;
            ws.UserAgent = PocketLadioInfo.UserAgent;
            ws.FetchFile(fileName);
        }

        /// <summary>
        /// Web上のストリームをダウンロードする
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="fileName">保存するファイル名</param>
        /// <param name="doDownloadProgressMinimum">ファイルサイズの最小値（0）をセットするデリゲート</param>
        /// <param name="doSetDownloadProgressMaximum">ファイルサイズをセットするデリゲート</param>
        /// <param name="doSetDownloadProgressValue">ダウンロード済みのファイルサイズをセットするデリゲート</param>
        public static void FetchFile(Uri url, string fileName,
            WebStream.SetDownloadProgressMinimumInvoker doDownloadProgressMinimum,
            WebStream.SetDownloadProgressMaximumInvoker doSetDownloadProgressMaximum,
            WebStream.SetDownloadProgressValueInvoker doSetDownloadProgressValue)
        {
            WebStream ws = new WebStream(url);
            ws.ProxyUse = PocketLadio.UserSetting.ProxyUse;
            ws.ProxyServer = PocketLadio.UserSetting.ProxyServer;
            ws.ProxyPort = PocketLadio.UserSetting.ProxyPort;
            ws.TimeOut = PocketLadioInfo.WebRequestTimeoutMillSec;
            ws.UserAgent = PocketLadioInfo.UserAgent;
            ws.FetchFile(fileName, doDownloadProgressMinimum, doSetDownloadProgressMaximum, doSetDownloadProgressValue);
        }
    }
}