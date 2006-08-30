using System;
using System.Collections;
using PocketLadio.StationInterface;

namespace PocketLadio.RssPodcast
{
    public class Chanel : PocketLadio.StationInterface.IChanel
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
        private string link = "";

        /// <summary>
        /// 番組のサイト
        /// </summary>
        public string Link
        {
            get { return link; }
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
        private string url = "";

        /// <summary>
        /// 再生URL
        /// </summary>
        public string Url
        {
            get { return url; }
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
        /// エンクロージャー要素リスト
        /// </summary>
        private ArrayList AlEnclosure = new ArrayList();

        /// <summary>
        /// 親ヘッドライン
        /// </summary>
        private Headline ParentHeadline;

        /// <summary>
        /// 再生する番組のタイプ。前の方ほど優先度が高い。
        /// </summary>
        private static string[] MatchedEnclosureTypeList = new string[] { 
            // MP3
            "audio/mpeg", "audio/mp3", "audio/mpg",  "audio/x-mpeg", "audio/mpeg3", "audio/x-mpeg3",
            // Ogg
            "audio/ogg", "application/ogg", "application/x-ogg",
            // WMA
            "audio/x-ms-wma",
            // Mpeg4
            "video/mp4", "video/x-m4v",
            // WMV
            "video/x-ms-wmv", "application/x-ms-wmv", 
            // ASF/WMV
            "application/x-mplayer2",
            // ASF
            "video/x-ms-asf", "video/x-ms-wm", "video/x-ms-asf-plugin",
            // Mpeg1
            "video/mpeg", "video/mpg", "video/x-mpeg",
            // AVI
            "video/avi", "video/msvideo", "video/x-msvideo", 
            // ビットストリーム
            "application/octet-stream",
            // ASX
            "application/x-drm-v2",
            // WAV
            "audio/wav", "audio/x-wav"
        };

        /// <summary>
        /// チャンネルのコンストラクタ
        /// </summary>
        /// <param name="ParentHeadline">親ヘッドライン</param>
        public Chanel(Headline parentHeadline)
        {
            this.ParentHeadline = parentHeadline;
        }

        public virtual string GetPlayUrl()
        {
            return Url;
        }

        public virtual string GetWebSiteUrl()
        {
            return Link;
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
                View = View.Replace("[[TITLE]]", Title);
                View = View.Replace("[[DESCRIPTION]]", Description);
                View = View.Replace("[[CATEGORY]]", Category);
                View = View.Replace("[[AUTHOR]]", Author);
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
            return Title + " " + Description + " " + Author;
        }

        /// <summary>
        /// エンクロージャー要素を追加する
        /// </summary>
        /// <param name="url">エンクロージャーのURL</param>
        /// <param name="length">エンクロージャーのLENGTH</param>
        /// <param name="type">エンクロージャーのTYPE</param>
        public void AddEnclosure(string url, string length, string type)
        {
            AlEnclosure.Add(new Enclosure(url, length, type));
        }

        /// <summary>
        /// エンクロージャー要素のリストから再生すべき番組の情報を解決し、本クラスの属性とする。
        /// エンクロージャー要素が複数あった場合、優先的にビデオ・オーディオのエンクロージャー要素を再生する番組として採用する。
        /// 解決後は、本クラスのUrl/Length/Typeが更新される。
        /// </summary>
        public void SolvedChannelPlayContentsFormEnclosures()
        {
            // エンクロージャー要素が1つしかない場合は、そのエンクロージャー要素を採用する
            if (AlEnclosure.Count == 1)
            {
                Url = ((Enclosure)AlEnclosure[0]).Url;
                Length = ((Enclosure)AlEnclosure[0]).Length;
                Type = ((Enclosure)AlEnclosure[0]).Type;

                return;
            }
            // エンクロージャー要素が存在しない場合は終了
            else if (AlEnclosure.Count == 0)
            {
                return;
            }
            // エンクロージャー要素が複数存在する場合
            else
            {
                // 適合するエンクロージャー要素が見つかったかのフラグ
                bool FindedMatchedEnclosure = false;

                // エンクロージャー要素の番組タイプから、採用するものを検索する
                foreach (Enclosure Enclosure in AlEnclosure)
                {
                    foreach (string MatchedEnclosureType in MatchedEnclosureTypeList)
                    {
                        if (Enclosure.Type == MatchedEnclosureType)
                        {
                            // フラグを立てる
                            FindedMatchedEnclosure = true;
                            Url = Enclosure.Url;
                            Length = Enclosure.Length;
                            Type = Enclosure.Type;

                            break;
                        }
                    }
                    if (FindedMatchedEnclosure == true)
                    {
                        break;
                    }
                }

                // 適合するエンクロージャー要素が見つからない場合は、1番目の要素をとりあえず採用する
                if (FindedMatchedEnclosure == false)
                {
                    Url = ((Enclosure)AlEnclosure[0]).Url;
                    Length = ((Enclosure)AlEnclosure[0]).Length;
                    Type = ((Enclosure)AlEnclosure[0]).Type;
                }
            }
        }
    }

    /// <summary>
    /// エンクロージャー要素クラス
    /// </summary>
    class Enclosure
    {
        /// <summary>
        /// 再生URL
        /// </summary>
        private string url = "";

        /// <summary>
        /// 再生URL
        /// </summary>
        public string Url
        {
            get { return url; }
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
        /// エンクロージャー要素のコンストラクタ
        /// </summary>
        public Enclosure(string url, string length, string type)
        {
            this.url = url;
            this.length = length;
            this.type = type;
        }
    }

}
