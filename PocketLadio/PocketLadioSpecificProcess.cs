#region ディレクティブを使用する

using System;
using System.IO;
using System.Xml;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// PocketLadio固有の処理を記述しているクラス
    /// </summary>
    public sealed class PocketLadioSpecificProcess
    {
        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PocketLadioSpecificProcess()
        {
        }

        /// <summary>
        /// PocketLadio起動時の初期化処理。
        /// </summary>
        public static void StartUpInitialize()
        {
            try
            {
                // 設定を読み込む
                UserSetting.LoadSetting();

                PocketLadio.Stations.Netladio.Headline.StartUpInitialize();
                PocketLadio.Stations.RssPodcast.Headline.StartUpInitialize();
                PocketLadio.Stations.ShoutCast.Headline.StartUpInitialize();
            }
            catch (XmlException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        /// <summary>
        /// PocketLadio終了時の処理。
        /// </summary>
        public static void ExitDisable()
        {
            try
            {
                // 設定ファイルの書き込み
                UserSetting.SaveSetting();
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}
