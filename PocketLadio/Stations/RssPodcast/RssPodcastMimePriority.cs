using System;
using System.Collections;
using System.IO;
using System.Text;

namespace PocketLadio.Stations.RssPodcast
{
    /// <summary>
    /// PodcastのMIMEタイプの優先度を格納するクラス
    /// </summary>
    public sealed class RssPodcastMimePriority
    {

        /// <summary>
        /// PodcastのMIMEタイプの優先度テーブル。数値が高い方が優先度が高い。
        /// Key => string MIME, value => int Priority
        /// </summary>
        private static Hashtable rssPodcastMimePriorityTable =
            new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant,
            CaseInsensitiveComparer.DefaultInvariant);

        /// <summary>
        /// PodcastのMIMEタイプの優先度ファイル
        /// </summary>
        private const string rssPodcastMimePriorityFileName
            = "PocketLadio.Resource.RssPodcastMimePriority.txt";

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
        /// PodcastのMIMEタイプの優先度をファイルから読み込み、
        /// Mimeの優先度を決定する。
        /// </summary>
        public static void Initialize()
        {
            StreamReader sr =null;

            try
            {
                // 現在のコードを実行しているAssemblyを取得
                System.Reflection.Assembly thisAssembly
                    = System.Reflection.Assembly.GetExecutingAssembly();
                // 指定されたマニフェストリソースを読み込む
                sr =
                    new StreamReader(thisAssembly.GetManifestResourceStream(RssPodcastMimePriorityFileName),
                    Encoding.GetEncoding("shift-jis"));
                // 内容を読み込む
                string mimeString = sr.ReadToEnd();

                string[] mimePriorityRawArray = mimeString.Split('\n');

                foreach (string mimePriorityRaw in mimePriorityRawArray)
                {
                    if (mimePriorityRaw.Length != 0)
                    {
                        string[] MimePriority = mimePriorityRaw.Split(',');
                        rssPodcastMimePriorityTable.Add(MimePriority[0], int.Parse(MimePriority[1]));
                    }
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally {
                if (sr != null)
                {
                    sr.Close();
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
            if (mime == null)
            {
                return 0;
            }

            return ((rssPodcastMimePriorityTable.ContainsKey(mime)) == false ? 0 : (int)rssPodcastMimePriorityTable[mime]);
        }
    }
}
