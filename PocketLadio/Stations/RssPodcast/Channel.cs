using System;
using System.Collections;
using PocketLadio.Stations;

namespace PocketLadio.Stations.RssPodcast
{
    public class Channel : PocketLadio.Stations.IChannel
    {
        /// <summary>
        /// 番組のタイトル
        /// </summary>
        private string title = "";

        /// <summary>
        /// 番組のタイトル
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// 番組のサイト
        /// </summary>
        private Uri link;

        /// <summary>
        /// 番組のサイト
        /// </summary>
        public Uri Link
        {
            set { link = value; }
        }

        /// <summary>
        /// 番組の詳細
        /// </summary>
        private string description = "";

        /// <summary>
        /// 番組の詳細
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 番組の配信日時
        /// </summary>
        private string date = "";

        /// <summary>
        /// 番組の配信日時
        /// </summary>
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// 番組のカテゴリ
        /// </summary>
        private string category = "";

        /// <summary>
        /// 番組のカテゴリ
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// 番組の著者
        /// </summary>
        private string author = "";

        /// <summary>
        /// 番組の著者
        /// </summary>
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        /// <summary>
        /// 再生URL
        /// </summary>
        private Uri url;

        /// <summary>
        /// 再生URL
        /// </summary>
        public Uri Url
        {
            set { url = value; }
        }

        /// <summary>
        /// 番組の長さ
        /// </summary>
        private string length = "";

        /// <summary>
        /// 番組の長さ
        /// </summary>
        public string Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// 番組のタイプ
        /// </summary>
        private string type = "";

        /// <summary>
        /// 番組のタイプ
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// 親ヘッドライン
        /// </summary>
        private readonly Headline ParentHeadline;

        /// <summary>
        /// チャンネルのコンストラクタ
        /// </summary>
        /// <param name="ParentHeadline">親ヘッドライン</param>
        public Channel(Headline parentHeadline)
        {
            this.ParentHeadline = parentHeadline;
        }

        /// <summary>
        /// 番組の再生URLを返す
        /// </summary>
        /// <returns>番組の再生URL</returns>
        public virtual Uri GetPlayUrl()
        {
            return url;
        }

        /// <summary>
        /// 番組のサイトを返す
        /// </summary>
        /// <returns>番組のサイト</returns>
        public virtual Uri GetWebsiteUrl()
        {
            return link;
        }

        /// <summary>
        /// 番組の表示方法に従って番組の情報を返す
        /// </summary>
        /// <returns>番組の表示方法に従った番組の情報</returns>
        public virtual string GetChannelView()
        {
            string view = ParentHeadline.HeadlineViewType;
            if (view.Length != 0)
            {
                view = view.Replace("[[TITLE]]", Title)
                    .Replace("[[DESCRIPTION]]", Description)
                    .Replace("[[CATEGORY]]", Category)
                    .Replace("[[AUTHOR]]", Author);
            }

            return view;
        }

        /// <summary>
        /// フィルタリング対象のワードを返す。
        /// 返されたワードに従い、フィルタリングを行う。
        /// </summary>
        /// <returns>フィルタリング対象のワード</returns>
        public virtual string GetFilteredWord()
        {
            return Title + " " + Description + " " + Author;
        }

        /// <summary>
        /// エンクロージャー要素をセットする。
        /// エンクロージャー要素の追加が複数回あった場合には、MIME Typeの優先度設定に従い、優先度の高いエンクロージャー要素の内容をセットする。
        /// </summary>
        /// <param name="podcastUrl">エンクロージャーのURL</param>
        /// <param name="length">エンクロージャーのLENGTH</param>
        /// <param name="type">エンクロージャーのTYPE</param>
        public void SetEnclosure(Uri podcastUrl, string length, string type)
        {
            // Urlがまだセットされていない場合はとりあえずUrl、Length、Typeを設定して終了
            if (GetPlayUrl() == null)
            {
                this.Url = podcastUrl;
                this.Length = length;
                this.Type = type;

                return;
            }

            // 現在セットされているエンクロージャー要素より、新たに指定されたエンクロージャー要素の方が優先度の高い場合は、
            // 新しい方のエンクロージャー要素をセットする。
            if (RssPodcastMimePriority.GetRssPodcastMimePriority(this.Type) < RssPodcastMimePriority.GetRssPodcastMimePriority(type)) {
                this.Url = podcastUrl;
                this.Length = length;
                this.Type = type;
            }
        }
    }
}
