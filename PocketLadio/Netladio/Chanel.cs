using System;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// ねとらじの番組
    /// </summary>
    public class Chanel
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

        public Chanel()
        {
        }

        /// <summary>
        /// 番組の放送URLを返す
        /// </summary>
        /// <returns>番組の放送URL</returns>
        public string GetPlayUrl()
        {
            return "http://" + Srv + ":" + Prt + Mnt + ".m3u";
        }

        /// <summary>
        /// チャンネルの表示方法に従ってチャンネルの情報を返す
        /// </summary>
        /// <returns>チャンネルの表示方法に従ったチャンネルの情報</returns>
        public string GetChanelView()
        {
            string View = (string)UserSetting.HeadlineViewType.Clone();
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
    }
}
