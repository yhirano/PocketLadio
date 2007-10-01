#region ディレクティブを使用する

using System;

#endregion

namespace PocketLadio.Stations.Icecast
{
    /// <summary>
    /// Icecastの番組
    /// </summary>
    public class Channel : PocketLadio.Stations.IChannel
    {
        /// <summary>
        /// サーバー名
        /// </summary>
        private string serverName = string.Empty;

        /// <summary>
        /// サーバー名
        /// </summary>
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        /// <summary>
        /// Url
        /// </summary>
        private Uri listenUrl;

        /// <summary>
        /// Url
        /// </summary>
        public Uri ListenUrl
        {
            get { return listenUrl; }
            set { listenUrl = value; }
        }

        /// <summary>
        /// ストリーミングの種類
        /// </summary>
        private string serverType = string.Empty;

        /// <summary>
        /// ストリーミングの種類
        /// </summary>
        public string ServerType
        {
            get { return serverType; }
            set { serverType = value; }
        }

        /// <summary>
        /// ビットレート
        /// </summary>
        private string bitrate = string.Empty;

        /// <summary>
        /// ビットレート
        /// </summary>
        public string Bitrate
        {
            get { return bitrate; }
            set { bitrate = value; }
        }

        /// <summary>
        /// チャンネル数
        /// </summary>
        private string channels = string.Empty;

        /// <summary>
        /// チャンネル数
        /// </summary>
        public string Channels
        {
            get { return channels; }
            set { channels = value; }
        }

        /// <summary>
        /// サンプリングレート
        /// </summary>
        private int sampleRate = UNKNOWN_SAMPLE_RATE;

        /// <summary>
        /// サンプリングレート
        /// </summary>
        public int SampleRate
        {
            get { return sampleRate; }
            set { sampleRate = value; }
        }

        /// <summary>
        /// サンプリングレートが不明
        /// </summary>
        public const int UNKNOWN_SAMPLE_RATE = -1;

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
        /// 現在の音楽
        /// </summary>
        private string currentSong = string.Empty;

        /// <summary>
        /// 現在の音楽
        /// </summary>
        public string CurrentSong
        {
            get { return currentSong; }
            set { currentSong = value; }
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
        /// 番組の再生URLを返す
        /// </summary>
        /// <returns>番組の再生URL</returns>
        public virtual Uri GetPlayUrl()
        {
            return listenUrl;
        }

        /// <summary>
        /// 番組のサイトを返す
        /// </summary>
        /// <returns>番組のサイト</returns>
        public virtual Uri GetWebsiteUrl()
        {
            return null;
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
                view = view.Replace("[[SREVERNAME]]", ServerName)
                    .Replace("[[SERVERTYPE]]", ServerType)
                    .Replace("[[GENRE]]", Genre)
                    .Replace("[[CURRENTSONG]]", CurrentSong);
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
            return ServerName + " " + Genre + " " + CurrentSong;
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
