#region ディレクティブを使用する

using System;

#endregion

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// SHOUTcastの番組
    /// </summary>
    public class Channel : PocketLadio.Stations.IChannel
    {
        /// <summary>
        /// 再生URLへのパス
        /// </summary>
        private string path = "";

        /// <summary>
        /// 再生URLへのパス
        /// </summary>
        public string Path
        {
            set { path = value; }
        }

        /// <summary>
        /// ランク
        /// </summary>
        private string rank = "";

        /// <summary>
        /// ランク
        /// </summary>
        public string Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        /// <summary>
        /// サイトURL
        /// </summary>
        private Uri clusterUrl;

        /// <summary>
        /// サイトURL
        /// </summary>
        public Uri ClusterUrl
        {
            set { clusterUrl = value; }
        }

        /// <summary>
        /// タイトル
        /// </summary>
        private string title = "";

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// 現在演奏中の曲
        /// </summary>
        private string playing = "";

        /// <summary>
        /// 現在演奏中の曲
        /// </summary>
        public string Playing
        {
            get { return playing; }
            set { playing = value; }
        }

        /// <summary>
        /// リスナ数
        /// </summary>
        private int listener = -1;

        /// <summary>
        /// リスナ数
        /// </summary>
        public int Listener
        {
            get { return listener; }
            set { listener = value; }
        }

        /// <summary>
        /// カテゴリ
        /// </summary>
        private string category = "";

        /// <summary>
        /// カテゴリ
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// ビットレート
        /// </summary>
        private int bitRate = -1;

        /// <summary>
        /// ビットレート
        /// </summary>
        public int BitRate
        {
            get { return bitRate; }
            set { bitRate = value; }
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
        /// 番組の放送URLを返す
        /// </summary>
        /// <returns>番組の放送URL</returns>
        public virtual Uri GetPlayUrl()
        {
            try
            {
                return new Uri(PocketLadioInfo.ShoutcastUrl + path);
            }
            catch (UriFormatException)
            {
                return null;
            }
        }

        /// <summary>
        /// 番組のウェブサイトURLを返す
        /// </summary>
        /// <returns>番組のウェブサイトURL</returns>
        public virtual Uri GetWebsiteUrl()
        {
            return clusterUrl;
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
                view = view.Replace("[[RANK]]", Rank)
                    .Replace("[[TITLE]]", Title)
                    .Replace("[[PLAYING]]", Playing)
                    .Replace("[[LISTENER]]", ((Listener > 0) ? Listener.ToString() : "na"))
                    .Replace("[[CATEGORY]]", Category)
                    .Replace("[[BIT]]", ((BitRate > 0) ? BitRate.ToString() : "na"));
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
            return Title + " " + Category;
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
    }
}
