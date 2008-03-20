#region ディレクティブを使用する

using System;
using System.Diagnostics;
using PocketLadio.Stations;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// 放送局
    /// </summary>
    public class Station
    {
        /// <summary>
        /// 放送局のID
        /// </summary>
        private string id;

        /// <summary>
        /// 放送局のID
        /// </summary>
        public string Id
        {
            get { return id; }
        }

        /// <summary>
        /// 放送局の名前
        /// </summary>
        private string name;

        /// <summary>
        /// ヘッドライン
        /// </summary>
        private IHeadline headline;

        /// <summary>
        /// 放送局の種類
        /// </summary>
        private StationKinds kind;

        /// <summary>
        /// 表示用の名前
        /// </summary>
        public string DisplayName
        {
            get { return name + " - " + headline.GetKindName(); }
        }

        /// <summary>
        /// 所持しているヘッドライン
        /// </summary>
        public IHeadline Headline
        {
            get { return headline; }
        }

        /// <summary>
        /// 放送局の名前
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 放送局の種類を返す
        /// </summary>
        public StationKinds Kind
        {
            get { return kind; }
        }

        /// <summary>
        /// 放送局のコンストラクタ
        /// </summary>
        /// <param name="id">放送局のID</param>
        /// <param name="name">放送局の名前</param>
        /// <param name="stationKind">放送局の種類</param>
        public Station(string id, string name, StationKinds stationKind)
        {
            if (id == null)
            {
                throw new ArgumentNullException("StationのIDにNullは指定できません");
            }
            if (id == string.Empty)
            {
                throw new ArgumentException("StationのIDに空文字は指定できません");
            }

            this.id = id;
            this.name = name;
            this.kind = stationKind;

            switch (kind)
            {
                case StationKinds.Netladio:
                    headline = new PocketLadio.Stations.Netladio.Headline(id, this);
                    break;
                case StationKinds.RssPodcast:
                    headline = new PocketLadio.Stations.RssPodcast.Headline(id, this);
                    break;
                case StationKinds.ShoutCast:
                    headline = new PocketLadio.Stations.ShoutCast.Headline(id, this);
                    break;
                case StationKinds.Icecast:
                    headline = new PocketLadio.Stations.Icecast.Headline(id, this);
                    break;
                default:
                    // ここには到達しない
                    Trace.Assert(false, "想定外の動作のため、終了します");
                    break;
            }
        }
    }
}
