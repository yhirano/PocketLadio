#region ディレクティブを使用する

using System;
using System.IO;
using System.Xml;
using MiscPocketCompactLibrary.Reflection;

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
        /// PocketLadio起動時のチェック処理。
        /// </summary>
        public static void StartUpCheck()
        {
            // MiscPocketCompactLibrary.dllが見つからない場合は例外を投げる
            if (File.Exists(AssemblyUtility.GetExecutablePath() + @"\MiscPocketCompactLibrary.dll") == false)
            {
                throw new DllNotFoundException("Not found MiscPocketCompactLibrary.dll.");
            }
            // FileDialog.dllが見つからない場合は例外を投げる
            if (File.Exists(AssemblyUtility.GetExecutablePath() + @"\FileDialog.dll") == false)
            {
                throw new DllNotFoundException("Not found FileDialog.dll.");
            }
            // GetFileInfo.dllが見つからない場合は例外を投げる
            if (File.Exists(AssemblyUtility.GetExecutablePath() + @"\GetFileInfo.dll") == false)
            {
                throw new DllNotFoundException("Not found GetFileInfo.dll.");
            }
        }

        /// <summary>
        /// PocketLadio起動時の初期化処理。
        /// </summary>
        public static void StartUpInitialize()
        {
            // 設定を読み込む
            UserSetting.LoadSetting();

            PocketLadio.Stations.Netladio.Headline.StartUpInitialize();
            PocketLadio.Stations.RssPodcast.Headline.StartUpInitialize();
            PocketLadio.Stations.ShoutCast.Headline.StartUpInitialize();
            PocketLadio.Stations.Icecast.Headline.StartUpInitialize();
        }

        /// <summary>
        /// PocketLadio終了時の処理。
        /// </summary>
        public static void ExitDisable()
        {
            // 設定ファイルの書き込み
            UserSetting.SaveSetting();
        }
    }
}
