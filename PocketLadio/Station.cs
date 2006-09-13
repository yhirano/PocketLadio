using System;
using PocketLadio.Stations.Interface;

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
        private string HeadlineId;

        /// <summary>
        /// 放送局の名前
        /// </summary>
        private string Name;

        /// <summary>
        /// ヘッドライン
        /// </summary>
        private IHeadline Headline;

        /// <summary>
        /// 放送局の種類
        /// </summary>
        private StationKind Kind;

        /// <summary>
        /// 放送局の種類列挙
        /// </summary>
        public enum StationKind
        {
            Netladio, RssPodcast
        };

        /// <summary>
        /// 放送局のコンストラクタ
        /// </summary>
        /// <param name="ID">ヘッドラインのID</param>
        /// <param name="name">放送局の名前</param>
        /// <param name="StationKind">放送局の種類</param>
        public Station(string headlineID, string name, StationKind stationKind)
        {
            this.HeadlineId = headlineID;
            this.Name = name;
            this.Kind = stationKind;

            if (Kind.Equals(StationKind.Netladio))
            {
                Headline = new PocketLadio.Stations.Netladio.Headline(HeadlineId);
            }
            else if (Kind.Equals(StationKind.RssPodcast))
            {
                Headline = new PocketLadio.Stations.RssPodcast.Headline(HeadlineId);
            }
        }

        /// <summary>
        /// ヘッドラインのIDを返す
        /// </summary>
        /// <returns>ヘッドラインのID</returns>
        public string GetHeadlineId()
        {
            return HeadlineId;
        }

        /// <summary>
        /// 放送局の名前を返す
        /// </summary>
        /// <returns>放送局の名前</returns>
        public string GetName()
        {
            return Name;
        }

        /// <summary>
        /// 放送局の種類を返す
        /// </summary>
        /// <returns>放送局の種類</returns>
        public StationKind GetStationKind()
        {
            return Kind;
        }

        /// <summary>
        /// 所持しているヘッドラインを返す
        /// </summary>
        /// <returns>ヘッドライン</returns>
        public IHeadline GetHeadline()
        {
            return Headline;
        }

        /// <summary>
        /// 表示用の名前を返す
        /// </summary>
        /// <returns>表示用の名前</returns>
        public string GetDisplayName() {
            return Name + " - " + Headline.GetKindName();
        }
    }
}
