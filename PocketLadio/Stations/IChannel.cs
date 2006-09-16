#region ディレクティブを使用する

using System;

#endregion

namespace PocketLadio.Stations
{
    /// <summary>
    /// 番組インターフェース
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// 番組の放送URLを返す
        /// </summary>
        /// <returns>番組の放送URL</returns>
        Uri GetPlayUrl();

        /// <summary>
        /// 番組のウェブサイトURLを返す
        /// </summary>
        /// <returns>番組のウェブサイトURL</returns>
        Uri GetWebsiteUrl();

        /// <summary>
        /// 番組の表示方法に従って番組の情報を返す
        /// </summary>
        /// <returns>番組の表示方法に従った番組の情報</returns>
        string GetChannelView();

        /// <summary>
        /// フィルタリング対象のワードを返す。
        /// 返されたワードに従い、フィルタリングを行う。
        /// </summary>
        /// <returns>フィルタリング対象のワード</returns>
        string GetFilteredWord();

        /// <summary>
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="channel">番組</param>
        void ShowPropertyForm();
    }
}
