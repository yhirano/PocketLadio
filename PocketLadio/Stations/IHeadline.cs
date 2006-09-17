#region ディレクティブを使用する

using System;

#endregion

namespace PocketLadio.Stations
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
        /// ヘッドラインをネットから取得する
        /// </summary>
        void FetchHeadline();

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
        /// 設定を保存していたファイルを削除する
        /// </summary>
        void DeleteUserSettingFile();
    }
}
