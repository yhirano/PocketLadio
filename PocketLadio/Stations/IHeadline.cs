#region ディレクティブを使用する

using System;
using MiscPocketCompactLibrary.Net;

#endregion

namespace PocketLadio.Stations
{
    /// <summary>
    /// ヘッドライン解析のイベントハンドラ
    /// </summary>
    /// <param name="sender">送信元</param>
    /// <param name="e">イベント</param>
    public delegate void HeadlineAnalyzeEventHandler(object sender, HeadlineAnalyzeEventArgs e);

    /// <summary>
    /// ヘッドラインインターフェース
    /// </summary>
    public interface IHeadline
    {
        /// <summary>
        /// 親放送局
        /// </summary>
        Station ParentStation { get; }

        /// <summary>
        /// ヘッドラインのIDを返す
        /// </summary>
        /// <returns>ヘッドラインのID</returns>
        string GetId();

        /// <summary>
        /// ヘッドラインの種類の名前を返す
        /// </summary>
        /// <returns>ヘッドラインの種類の名前</returns>
        string GetKindName();
        
        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        IChannel[] GetChannels();

        /// <summary>
        /// フィルタリングした番組の結果を返す
        /// </summary>
        /// <returns>フィルタリングした番組のリスト</returns>
        IChannel[] GetChannelsFiltered();

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        void FetchHeadline();

        /// <summary>
        /// ヘッドラインをネットから取得する前に発生するイベント
        /// </summary>
        event FetchEventHandler HeadlineFetch;

        /// <summary>
        /// ヘッドラインをネットから取得している最中に発生するイベント
        /// </summary>
        event FetchEventHandler HeadlineFetching;

        /// <summary>
        /// ヘッドラインをネットから取得した後に発生するイベント
        /// </summary>
        event FetchEventHandler HeadlineFetched;

        /// <summary>
        /// ヘッドラインを解析する前に発生するイベント
        /// </summary>
        event HeadlineAnalyzeEventHandler HeadlineAnalyze;

        /// <summary>
        /// ヘッドラインを解析している最中に発生するイベント
        /// </summary>
        event HeadlineAnalyzeEventHandler HeadlineAnalyzing;

        /// <summary>
        /// ヘッドラインを解析した後に発生するイベント
        /// </summary>
        event HeadlineAnalyzeEventHandler HeadlineAnalyzed;

        /// <summary>
        /// ヘッドラインをネットから取得した時刻を返す
        /// </summary>
        /// <returns>ヘッドラインをネットから取得した時刻</returns>
        DateTime GetLastCheckTime();

        /// <summary>
        /// ヘッドラインの設定フォームを表示する
        /// </summary>
        void ShowSettingForm();

        /// <summary>
        /// フィルターに単語を登録するためにヘッドライン設定フォームを表示する
        /// </summary>
        /// <param name="filterWord">フィルターに追加する単語</param>
        void ShowSettingFormForAddFilter(string filterWord);

        /// <summary>
        /// 設定を保存していたファイルを削除する
        /// </summary>
        void DeleteUserSettingFile();
    }
}
