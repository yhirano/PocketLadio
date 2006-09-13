using System;

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// ねとらじの番組
    /// </summary>
    public class Channel : PocketLadio.Stations.IChannel
    {

        /// <summary>
        /// DSPツールで指定されるURL
        /// </summary>
        private Uri url;

        /// <summary>
        /// DSPツールで指定されるURL
        /// </summary>
        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        /// DSPツールで指定されるジャンル欄
        /// </summary>
        private string gnl = "";

        /// <summary>
        /// DSPツールで指定されるジャンル欄
        /// </summary>
        public string Gnl
        {
            get { return gnl; }
            set { gnl = value; }
        }

        /// <summary>
        /// DSPツールで指定されるタイトル欄
        /// </summary>
        private string nam = "";

        /// <summary>
        /// DSPツールで指定されるタイトル欄
        /// </summary>
        public string Nam
        {
            get { return nam; }
            set { nam = value; }
        }

        /// <summary>
        /// DSPツールが送信する現在の曲名情報
        /// </summary>
        private string tit = "";

        /// <summary>
        /// DSPツールが送信する現在の曲名情報
        /// </summary>
        public string Tit
        {
            get { return tit; }
            set { tit = value; }
        }

        /// <summary>
        /// マウントポイント
        /// </summary>
        private string mnt = "";

        /// <summary>
        /// マウントポイント
        /// </summary>
        public string Mnt
        {
            get { return mnt; }
            set { mnt = value; }
        }

        /// <summary>
        /// Unix epochでの放送開始時間
        /// </summary>
        private string tim = "";

        /// <summary>
        /// Unix epochでの放送開始時間
        /// </summary>
        public string Tim
        {
            get { return tim; }
            set { tim = value; }
        }

        /// <summary>
        /// yy/mm/dd hh:mm:ss　表記での放送開始時間
        /// </summary>
        private string tims = "";

        /// <summary>
        /// yy/mm/dd hh:mm:ss　表記での放送開始時間
        /// </summary>
        public string Tims
        {
            get { return tims; }
            set { tims = value; }
        }

        /// <summary>
        /// 現リスナ数
        /// </summary>
        private string cln = "";

        /// <summary>
        /// 現リスナ数
        /// </summary>
        public string Cln
        {
            get { return cln; }
            set { cln = value; }
        }

        /// <summary>
        /// 延べリスナ数
        /// </summary>
        private string clns = "";

        /// <summary>
        /// 延べリスナ数
        /// </summary>
        public string Clns
        {
            get { return clns; }
            set { clns = value; }
        }

        /// <summary>
        /// 配信サーバホスト名
        /// </summary>
        private string srv = "";

        /// <summary>
        /// 配信サーバホスト名
        /// </summary>
        public string Srv
        {
            get { return srv; }
            set { srv = value; }
        }

        /// <summary>
        /// 配信サーバポート番号
        /// </summary>
        private string prt = "";

        /// <summary>
        /// 配信サーバポート番号
        /// </summary>
        public string Prt
        {
            get { return prt; }
            set { prt = value; }
        }

        /// <summary>
        /// 配信サーバの種類
        /// </summary>
        private string typ = "";

        /// <summary>
        /// 配信サーバの種類
        /// </summary>
        public string Typ
        {
            get { return typ; }
            set { typ = value; }
        }

        /// <summary>
        /// ビットレート
        /// </summary>
        private string bit = "";

        /// <summary>
        /// ビットレート
        /// </summary>
        public string Bit
        {
            get { return bit; }
            set { bit = value; }
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
        /// 番組の放送URLを返す
        /// </summary>
        /// <returns>番組の放送URL</returns>
        public virtual Uri GetPlayUrl()
        {
            return new Uri("http://" + srv + ":" + prt + mnt + ".m3u");
        }

        /// <summary>
        /// 番組のウェブサイトURLを返す
        /// </summary>
        /// <returns>番組のウェブサイトURL</returns>
        public virtual Uri GetWebsiteUrl()
        {
            return Url;
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
                view = view.Replace("[[NAME]]", nam);
                view = view.Replace("[[GENRE]]", gnl);
                view = view.Replace("[[CLN]]", cln);
                view = view.Replace("[[CLNS]]", clns);
                view = view.Replace("[[TITLE]]", tit);
                view = view.Replace("[[TIMES]]", tims);
                view = view.Replace("[[BIT]]", bit);
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
            return nam + " " + gnl;
        }
    }
}
