#region ディレクティブを使用する

using System;

#endregion

namespace PocketLadio.RssPodcast
{

    /// <summary>
    /// エンクロージャー要素クラス
    /// </summary>
    public class Enclosure
    {
        /// <summary>
        /// 再生URL
        /// </summary>
        public string Url = "";

        /// <summary>
        /// 番組の長さ
        /// </summary>
        public string Length = "";

        /// <summary>
        /// 番組のタイプ
        /// </summary>
        public string Type = "";

        /// <summary>
        /// エンクロージャー要素のコンストラクタ
        /// </summary>
        public Enclosure()
        {
        }
    }
}
