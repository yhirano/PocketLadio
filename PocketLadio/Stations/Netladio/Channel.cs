#region ディレクティブを使用する

using System;

#endregion

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
            set { mnt = value; }
        }

        /// <summary>
        /// Unix epochでの放送開始時間
        /// </summary>
        private int tim;

        /// <summary>
        /// Unix epochでの放送開始時間
        /// </summary>
        public int Tim
        {
            get { return tim; }
        }

        /// <summary>
        /// yy/mm/dd hh:mm:ss　表記での放送開始時間
        /// </summary>
        private DateTime tims = DateTime.Now;

        /// <summary>
        /// yy/mm/dd hh:mm:ss　表記での放送開始時間
        /// </summary>
        public DateTime Tims
        {
            get { return tims; }
        }

        /// <summary>
        /// 現リスナ数
        /// </summary>
        private int cln = -1;

        /// <summary>
        /// 現リスナ数
        /// </summary>
        public int Cln
        {
            get { return cln; }
            set { cln = value; }
        }

        /// <summary>
        /// 延べリスナ数
        /// </summary>
        private int clns = -1;

        /// <summary>
        /// 延べリスナ数
        /// </summary>
        public int Clns
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
        private int bit = -1;

        /// <summary>
        /// ビットレート
        /// </summary>
        public int Bit
        {
            get { return bit; }
            set { bit = value; }
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
        /// 日時の書式は "yy'/'MM'/'dd HH':'mm':'ss" 。
        /// </summary>
        /// <param name="date">番組の配信日時の文字列</param>
        public void SetTims(string date)
        {
            try
            {
                tims = DateTime.ParseExact(date, "yy'/'MM'/'dd HH':'mm':'ss",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    System.Globalization.DateTimeStyles.None);
            }
            catch (FormatException)
            {
                tims = DateTime.Now;
            }
        }

        /// <summary>
        /// 番組の配信日時をセットする。
        /// Unix epochで指定する。
        /// </summary>
        /// <param name="date">番組の配信日時（Unix epoch）</param>
        public void SetTim(string date)
        {
            try
            {
                tim = int.Parse(date);
            }
            catch (ArgumentException)
            {
                tim = 0;
            }
            catch (FormatException)
            {
                tim = 0;
            }
            catch (OverflowException)
            {
                tim = 0;
            }
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
            return url;
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
                view = view.Replace("[[NAME]]", Nam)
                    .Replace("[[GENRE]]", Gnl)
                    .Replace("[[CLN]]", ((Cln >= 0) ? Cln.ToString() : "na"))
                    .Replace("[[CLNS]]", ((Clns >= 0) ? Clns.ToString() : "na"))
                    .Replace("[[TITLE]]", Tit)
                    .Replace("[[TIMES]]", Tims.ToString())
                    .Replace("[[BIT]]", Bit.ToString());
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
            return Nam + " " + Gnl;
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
