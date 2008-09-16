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
        /// 再生URL
        /// </summary>
        private Uri playUrl;

        /// <summary>
        /// 再生URL
        /// </summary>
        public Uri PlayUrl
        {
            set { playUrl = value; }
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
        /// ジャンル
        /// </summary>
        private string genre = string.Empty;

        /// <summary>
        /// ジャンル
        /// </summary>
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
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
                return playUrl;
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
                view = view.Replace("[[TITLE]]", Title)
                    .Replace("[[PLAYING]]", Playing)
                    .Replace("[[LISTENER]]", ((Listener != Channel.UNKNOWN_LISTENER_NUM) ? Listener.ToString() : "na"))
                    .Replace("[[GENRE]]", Genre)
                    .Replace("[[CATEGORY]]", Genre)
                    .Replace("[[BIT]]", ((BitRate != Channel.UNKNOWN_BITRATE) ? BitRate.ToString() : "na"))
                    .Replace("[[RANK]]", string.Empty) // Ver 0.46より[[RANK]]は非サポート
                    .Replace("[[LISTENERTOTAL]]", string.Empty) // Ver 0.46より[[LISTENERTOTAL]]は非サポート
                    ;
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
                return new string[] { Title, Genre, GetPlayUrl().ToString() };
            }
            else
            {
                return new string[] { Title, Genre };
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
