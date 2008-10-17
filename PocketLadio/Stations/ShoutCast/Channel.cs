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
        private string path = string.Empty;

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
        private string rank = string.Empty;

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
        private string title = string.Empty;

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
        private string playing = string.Empty;

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
        private int listener = UNKNOWN_LISTENER_NUM;

        /// <summary>
        /// リスナ数
        /// </summary>
        public int Listener
        {
            get { return listener; }
            set { listener = value; }
        }

        /// <summary>
        /// リスナ数が不明
        /// </summary>
        public const int UNKNOWN_LISTENER_NUM = -1;

        /// <summary>
        /// 述べリスナ数
        /// </summary>
        private int listenerTotal = UNKNOWN_LISTENER_NUM;

        /// <summary>
        /// 述べリスナ数
        /// </summary>
        public int ListenerTotal
        {
            get { return listenerTotal; }
            set { listenerTotal = value; }
        }

        /// <summary>
        /// カテゴリ
        /// </summary>
        private string category = string.Empty;

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
        private int bitRate = UNKNOWN_BITRATE;

        /// <summary>
        /// ビットレート
        /// </summary>
        public int BitRate
        {
            get { return bitRate; }
            set { bitRate = value; }
        }

        /// <summary>
        /// ビットレートが不明
        /// </summary>
        public const int UNKNOWN_BITRATE = -1;

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
                return new Uri(Headline.SHOUTCAST_URL + path);
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
                    .Replace("[[LISTENER]]", ((Listener != Channel.UNKNOWN_LISTENER_NUM) ? Listener.ToString() : "na"))
                    .Replace("[[LISTENERTOTAL]]", ((ListenerTotal != Channel.UNKNOWN_LISTENER_NUM) ? ListenerTotal.ToString() : "na"))
                    .Replace("[[CATEGORY]]", Category)
                    .Replace("[[BIT]]", ((BitRate != Channel.UNKNOWN_BITRATE) ? BitRate.ToString() : "na"));
            }

            return view;
        }

        /// <summary>
        /// フィルタリング対象のワードを返す。
        /// 返されたワードに従い、フィルタリングを行う。
        /// </summary>
        /// <returns>フィルタリング対象のワード</returns>
        public virtual string[] GetFilteredWords()
        {
            if (GetPlayUrl() != null)
            {
                return new string[] { Title, Category, GetPlayUrl().ToString() };
            }
            else
            {
                return new string[] { Title, Category };
            }
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
