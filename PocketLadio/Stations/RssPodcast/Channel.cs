#region ディレクティブを使用する

using System;

#endregion

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
        private DateTime date = DateTime.Now;

        /// <summary>
        /// 番組の配信日時
        /// </summary>
        public DateTime Date
        {
            get { return date; }
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
        private readonly Headline parentHeadline;

        /// <summary>
        /// 親ヘッドライン
        /// </summary>
        public virtual IHeadline ParentHeadline
        {
            get { return parentHeadline; }
        }

        /// <summary>
        /// チャンネルのコンストラクタ
        /// </summary>
        /// <param name="ParentHeadline">親ヘッドライン</param>
        public Channel(Headline parentHeadline)
        {
            this.parentHeadline = parentHeadline;
        }

        /// <summary>
        /// 番組の配信日時をセットする。
        /// 日時の書式は "ddd, d MMM yyyy HH':'mm':'ss zzz" 。
        /// </summary>
        /// <param name="pubDate">番組の配信日時の文字列</param>
        public void SetDate(string pubDate)
        {
            if (pubDate == null)
            {
                date = DateTime.Now;
            }

            try
            {
                date = DateTime.ParseExact(pubDate, "ddd, d MMM yyyy HH':'mm':'ss zzz",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    System.Globalization.DateTimeStyles.None);
            }
            catch (FormatException)
            {
                try
                {
                    date = DateTime.Parse(pubDate,
                        System.Globalization.DateTimeFormatInfo.InvariantInfo,
                        System.Globalization.DateTimeStyles.None);
                }
                catch (FormatException)
                {
                    date = DateTime.Now;
                }
            }
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
            string view = parentHeadline.HeadlineViewType;
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
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="channel">番組</param>
        /// <returns>番組の詳細フォーム</returns>
        public virtual void ShowPropertyForm()
        {
            ChannelPropertyForm channelPropertyForm = new ChannelPropertyForm(this);
            channelPropertyForm.ShowDialog();
            channelPropertyForm.Dispose();
        }

        /// <summary>
        /// 番組のクローンを返す
        /// </summary>
        /// <param name="parentHeadline">親ヘッドライン</param>
        /// <returns>番組のクローン</returns>
        public IChannel Clone(IHeadline parentHeadline)
        {
            Channel channel = new Channel((Headline)parentHeadline);
            channel.Title = (string)(Title.Clone());
            if (GetWebsiteUrl() != null)
            {
                channel.Link = new Uri(GetWebsiteUrl().ToString());
            }
            else
            {
                channel.Link = null;
            }
            channel.Description = (string)(channel.Description.Clone());
            channel.SetDate(
                Date.ToString("ddd, d MMM yyyy HH':'mm':'ss zzz",
                System.Globalization.DateTimeFormatInfo.InvariantInfo));
            channel.Category = (string)(Category.Clone());
            channel.Author = (string)(Author.Clone());
            if (GetPlayUrl() != null)
            {
                channel.Url = new Uri(GetPlayUrl().ToString());
            }
            else
            {
                channel.Url = null;
            }
            channel.Length = (string)(Length.Clone());
            channel.Type = (string)(Type.Clone());

            return channel;
        }
    }
}
