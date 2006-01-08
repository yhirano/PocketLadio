using System;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// ねとらじの番組
    /// </summary>
    public class Chanel : PocketLadio.StationInterface.IChanel
    {

        /// <summary>
        /// DSPツールで指定されるURL
        /// </summary>
        public string Url = "";

        /// <summary>
        /// DSPツールで指定されるジャンル欄
        /// </summary>
        public string Gnl = "";

        /// <summary>
        /// DSPツールで指定されるタイトル欄
        /// </summary>
        public string Nam = "";

        /// <summary>
        /// DSPツールが送信する現在の曲名情報
        /// </summary>
        public string Tit = "";

        /// <summary>
        /// マウントポイント
        /// </summary>
        public string Mnt = "";

        /// <summary>
        /// Unix epochでの放送開始時間
        /// </summary>
        public string Tim = "";

        /// <summary>
        /// yy/mm/dd hh:mm:ss　表記での放送開始時間
        /// </summary>
        public string Tims = "";

        /// <summary>
        /// 現リスナ数
        /// </summary>
        public string Cln = "";

        /// <summary>
        /// 延べリスナ数
        /// </summary>
        public string Clns = "";

        /// <summary>
        /// 配信サーバホスト名
        /// </summary>
        public string Srv = "";

        /// <summary>
        /// 配信サーバポート番号
        /// </summary>
        public string Prt = "";

        /// <summary>
        /// 配信サーバの種類
        /// </summary>
        public string Typ = "";

        /// <summary>
        /// ビットレート
        /// </summary>
        public string Bit = "";

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

        /// <summary>
        /// 番組の放送URLを返す
        /// </summary>
        /// <returns>番組の放送URL</returns>
        public virtual string GetPlayUrl()
        {
            return "http://" + Srv + ":" + Prt + Mnt + ".m3u";
        }

        /// <summary>
        /// 番組のウェブサイトURLを返す
        /// </summary>
        /// <returns>番組のウェブサイトURL</returns>
        public virtual string GetWebSiteUrl()
        {
            return Url;
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
                View = View.Replace("[[NAME]]", Nam);
                View = View.Replace("[[GENRE]]", Gnl);
                View = View.Replace("[[CLN]]", Cln);
                View = View.Replace("[[CLNS]]", Clns);
                View = View.Replace("[[TITLE]]", Tit);
                View = View.Replace("[[TIMES]]", Tims);
                View = View.Replace("[[BIT]]", Bit);
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
            //return Nam + " " + Gnl + " " + Nam + " " + Tit;
            return Nam + " " + Gnl;
        }
    }
}
