using System;
using System.Collections;
using System.IO;
using System.Text;

namespace PocketLadio
{
    /// <summary>
    /// PodcastのMIMEタイプの優先度を格納するクラス
    /// </summary>
    public class RssPodcastMimePriority
    {

        /// <summary>
        /// PodcastのMIMEタイプの優先度テーブル。数値が高い方が優先度が高い。
        /// Key => string MIME, value => int Priority
        /// </summary>
        private static Hashtable rssPodcastMimePriorityTable = new Hashtable();

        /// <summary>
        /// PodcastのMIMEタイプの優先度ファイル
        /// </summary>
        private const string rssPodcastMimePriorityFileName = "PocketLadio.Resource.RssPodcastMimePriority.txt";

        /// <summary>
        /// PodcastのMIMEタイプの優先度ファイル
        /// </summary>
        private static string RssPodcastMimePriorityFileName
        {
            get { return rssPodcastMimePriorityFileName; }
        }

        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private RssPodcastMimePriority()
        {
        }

        /// <summary>
        /// PodcastのMIMEタイプの優先度をファイルから読み込む
        /// </summary>
        public static void LoadSetting()
        {
            StreamReader Sr =null;

            try
            {
                // 現在のコードを実行しているAssemblyを取得
                System.Reflection.Assembly ThisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                // 指定されたマニフェストリソースを読み込む
                Sr =
                    new StreamReader(ThisAssembly.GetManifestResourceStream(RssPodcastMimePriorityFileName),
                    Encoding.GetEncoding("shift-jis"));
                // 内容を読み込む
                string MimeString = Sr.ReadToEnd();

                string[] MimePriorityRawArray = MimeString.Split('\n');

                foreach (string MimePriorityRaw in MimePriorityRawArray)
                {
                    if (MimePriorityRaw != "")
                    {
                        string[] MimePriority = MimePriorityRaw.Split(',');
                        rssPodcastMimePriorityTable.Add(MimePriority[0].ToLower(), int.Parse(MimePriority[1]));
                    }
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally {
                if (Sr != null)
                {
                    Sr.Close();
                }
            }
        }

        /// <summary>
        /// PodcastのMIMEタイプの再生優先度を返す。数値が高い方が優先度が高い。
        /// 再生しないMIMEタイプの場合や、優先度が存在しないMIMEタイプ場合は0を返す。
        /// </summary>
        /// <param name="mime">MIMEタイプ</param>
        /// <returns></returns>
        public static int GetRssPodcastMimePriority(string mime)
        {
            string mimeLower = mime.ToLower();

            return (rssPodcastMimePriorityTable.ContainsKey(mimeLower) == false ? 0 : (int)rssPodcastMimePriorityTable[mimeLower]);
        }
    }
}
