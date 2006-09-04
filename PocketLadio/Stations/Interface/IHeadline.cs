using System;

namespace PocketLadio.Stations.Interface
{
    /// <summary>
    /// ヘッドラインインターフェース
    /// </summary>
    public interface IHeadline
    {
        /// <summary>
        /// ヘッドラインのIDを返す
        /// </summary>
        /// <returns>ヘッドラインのID</returns>
        string GetID();

        /// <summary>
        /// ヘッドラインの種類の名前を返す
        /// </summary>
        /// <returns>ヘッドラインの種類の名前</returns>
        string GetKindName();
        
        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        IChanel[] GetChanels();

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        void WebGetHeadline();

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
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="Chanel">番組</param>
        void ShowPropertyForm(IChanel chanel);

        /// <summary>
        /// 設定を保存していたファイルを削除する
        /// </summary>
        void DeleteUserSettingFile();
    }
}
