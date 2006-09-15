using System;
using System.Diagnostics;
using PocketLadio.Stations;

namespace PocketLadio
{
    /// <summary>
    /// 放送局
    /// </summary>
    public class Station
    {
        /// <summary>
        /// ヘッドラインのID
        /// </summary>
        private string headlineId;

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
        private StationKind kind;

        /// <summary>
        /// 放送局の種類列挙
        /// </summary>
        public enum StationKind
        {
            Netladio, RssPodcast, ShoutCast
        };

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
        /// ヘッドラインのID
        /// </summary>
        public string HeadlineId
        {
            get { return headlineId; }
        }

        /// <summary>
        /// 放送局の名前
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// 放送局の種類を返す
        /// </summary>
        public StationKind Kind
        {
            get { return kind; }
        }

        /// <summary>
        /// 放送局のコンストラクタ
        /// </summary>
        /// <param name="ID">ヘッドラインのID</param>
        /// <param name="name">放送局の名前</param>
        /// <param name="StationKind">放送局の種類</param>
        public Station(string headlineID, string name, StationKind stationKind)
        {
            this.headlineId = headlineID;
            this.name = name;
            this.kind = stationKind;

            if (kind.Equals(StationKind.Netladio))
            {
                headline = new PocketLadio.Stations.Netladio.Headline(headlineId);
            }
            else if (kind.Equals(StationKind.RssPodcast))
            {
                headline = new PocketLadio.Stations.RssPodcast.Headline(headlineId);
            }
            else if (kind.Equals(StationKind.ShoutCast))
            {
                headline = new PocketLadio.Stations.ShoutCast.Headline(headlineId);
            }
            else
            {
                // ここには到達しない
                Trace.Assert(false, "想定外の動作のため、終了します");
            }
        }
    }
}
