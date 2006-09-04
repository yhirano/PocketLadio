using System;

namespace PocketLadio.Stations.Interface
{
    /// <summary>
    /// 番組インターフェース
    /// </summary>
    public interface IChanel
    {
        /// <summary>
        /// 番組の放送URLを返す
        /// </summary>
        /// <returns>番組の放送URL</returns>
        string GetPlayUrl();

        /// <summary>
        /// 番組のウェブサイトURLを返す
        /// </summary>
        /// <returns>番組のウェブサイトURL</returns>
        string GetWebSiteUrl();

        /// <summary>
        /// 番組の表示方法に従って番組の情報を返す
        /// </summary>
        /// <returns>番組の表示方法に従った番組の情報</returns>
        string GetChanelView();

        /// <summary>
        /// フィルタリング対象のワードを返す。
        /// 返されたワードに従い、フィルタリングを行う。
        /// </summary>
        /// <returns>フィルタリング対象のワード</returns>
        string GetFilterdWord();
    }
}
