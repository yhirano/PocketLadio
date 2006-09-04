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
        private string HeadlineID;

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
        private StationKindEnum StationKind;

        /// <summary>
        /// 放送局の種類列挙
        /// </summary>
        public enum StationKindEnum
        {
            Netladio, RssPodcast
        };

        /// <summary>
        /// 放送局のコンストラクタ
        /// </summary>
        /// <param name="ID">ヘッドラインのID</param>
        /// <param name="name">放送局の名前</param>
        /// <param name="StationKind">放送局の種類</param>
        public Station(string headlineID, string name, StationKindEnum stationKind)
        {
            this.HeadlineID = headlineID;
            this.Name = name;
            this.StationKind = stationKind;

            if (StationKind.Equals(StationKindEnum.Netladio))
            {
                Headline = new PocketLadio.Stations.Netladio.Headline(HeadlineID);
            }
            else if (StationKind.Equals(StationKindEnum.RssPodcast))
            {
                Headline = new PocketLadio.Stations.RssPodcast.Headline(HeadlineID);
            }
        }

        /// <summary>
        /// ヘッドラインのIDを返す
        /// </summary>
        /// <returns>ヘッドラインのID</returns>
        public string GetHeadlineID()
        {
            return HeadlineID;
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
        public StationKindEnum GetStationKind()
        {
            return StationKind;
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
