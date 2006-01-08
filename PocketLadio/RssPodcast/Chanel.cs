using System;
using PocketLadio.StationInterface;

namespace PocketLadio.RssPodcast
{
    public class Chanel : PocketLadio.StationInterface.IChanel
    {
        /// <summary>
        /// 番組のタイトル
        /// </summary>
        public string Title = "";

        /// <summary>
        /// 番組のサイト
        /// </summary>
        public string Link = "";

        /// <summary>
        /// 番組の詳細
        /// </summary>
        public string Description = "";

        /// <summary>
        /// 番組の配信日時
        /// </summary>
        public string Date = "";

        /// <summary>
        /// 番組のカテゴリ
        /// </summary>
        public string Category = "";

        /// <summary>
        /// 番組の著者
        /// </summary>
        public string Author = "";

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
        /// 親ヘッドライン
        /// </summary>
        private Headline ParentHeadline;

        /// <summary>
        /// チャンネルのコンストラクタ
        /// </summary>
        /// <param name="ParentHeadline">親ヘッドライン</param>
        public Chanel(Headline parentHeadline)
        {
            this.ParentHeadline = parentHeadline;
        }

        public virtual string GetPlayUrl()
        {
            return Url;
        }

        public virtual string GetWebSiteUrl()
        {
            return Link;
        }

        /// <summary>
        /// 番組の表示方法に従って番組の情報を返す
        /// </summary>
        /// <returns>番組の表示方法に従った番組の情報</returns>
        public virtual string GetChanelView()
        {
            string View = (string)ParentHeadline.GetUserSetting().HeadlineViewType.Clone();
            if (!View.Equals(""))
            {
                View = View.Replace("[[TITLE]]", Title);
                View = View.Replace("[[DESCRIPTION]]", Description);
                View = View.Replace("[[CATEGORY]]", Category);
                View = View.Replace("[[AUTHOR]]", Author);
            }

            return View;
        }

        /// <summary>
        /// フィルタリング対象のワードを返す。
        /// 返されたワードに従い、フィルタリングを行う。
        /// </summary>
        /// <returns>フィルタリング対象のワード</returns>
        public virtual string GetFilterdWord()
        {
            return Title + " " + Description + " " + Author;
        }
    }
}
