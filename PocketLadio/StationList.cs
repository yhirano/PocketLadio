using System;
using System.Collections;
using PocketLadio.Stations.Interface;

namespace PocketLadio
{
    /// <summary>
    /// 放送局のリスト
    /// </summary>
    public sealed class StationList
    {
        /// <summary>
        /// フィルターの有効無効
        /// </summary>
        private static bool filterEnable = false;

        /// <summary>
        /// フィルターの有効無効
        /// </summary>
        public static bool FilterEnable
        {
            get { return StationList.filterEnable; }
            set { StationList.filterEnable = value; }
        }

        /// <summary>
        /// 放送局のリスト
        /// </summary>
        private static Station[] stations = new Station[0];

        /// <summary>
        /// 放送局のリスト
        /// </summary>
        public static Station[] Stations
        {
            get { return StationList.stations; }
            set { StationList.stations = value; }
        }

        /// <summary>
        /// 現在の放送局
        /// </summary>
        private static Station currentStation;

        /// <summary>
        /// シングルトンのためprivate
        /// </summary>
        private StationList()
        {
        }

        /// <summary>
        /// 現在の放送局を変更する
        /// </summary>
        /// <param name="number">何番目の放送局か（0から始まる）</param>
        public static void ChangeCurrentStationAt(int number)
        {
            currentStation = Stations[number];
        }

        /// <summary>
        /// 放送局のリストを返す
        /// </summary>
        /// <returns>放送局のリスト</returns>
        public static Station[] GetStationList()
        {
            return Stations;
        }

        /// <summary>
        /// 放送局のリストをセットする
        /// </summary>
        /// <param name="Stations">放送局のリスト</param>
        /// <returns></returns>
        public static void SetStationList(Station[] stations)
        {
            Stations = stations;

            // 現在の放送局をクリアする
            currentStation = null;
        }

        /// <summary>
        /// 放送局の持つヘッドラインのIDを返す
        /// </summary>
        /// <returns>放送局の持つヘッドラインのID</returns>
        public static string GetHeadlineIdOfCurrentStation()
        {
            return (currentStation != null ? currentStation.GetHeadlineId() : "");
        }

        /// <summary>
        /// 放送局の名前を返す
        /// </summary>
        /// <returns>放送局の名前</returns>
        public static string GetHeadlineNameOfCurrentStation()
        {
            return (currentStation != null ? currentStation.GetName() : "");
        }

        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public static IChannel[] GetChannelsOfCurrentStation()
        {
            return currentStation.GetHeadline().GetChannels();
        }

        /// <summary>
        /// 取得している番組のリストを返す。
        /// フィルタリングが有効な場合にはフィルタリングした番組の結果を返す。
        /// フィルタリングは指定されたフィルタのor条件となる。
        /// </summary>
        /// <returns>フィルタリングされた番組のリスト</returns>
        public static IChannel[] GetChannelsFilteredOfCurrentStation()
        {
            if (currentStation == null)
            {
                return new IChannel[0];
            }

            // フィルタが存在する場合
            if (FilterEnable == true && UserSetting.GetFilterWords().Length > 0)
            {
                ArrayList alChannels = new ArrayList();

                foreach (IChannel channel in currentStation.GetHeadline().GetChannels())
                {
                    foreach (string filter in UserSetting.GetFilterWords())
                    {
                        if (channel.GetFilteredWord().IndexOf(filter) != -1)
                        {
                            alChannels.Add(channel);
                            break;
                        }
                    }
                }

                return (IChannel[])alChannels.ToArray(typeof(IChannel));
            }
            // フィルタが存在しない場合
            else
            {
                return currentStation.GetHeadline().GetChannels();
            }
        }

        /// <summary>
        /// 取得しているヘッドラインのネットから最終取得時刻を返す。
        /// 値を返せない場合はDateTime.MinValueを返す。
        /// </summary>
        /// <returns>ヘッドラインのネットから最終取得時刻</returns>
        public static DateTime GetLastCheckTimeOfCurrentStation()
        {
            return (currentStation != null ? currentStation.GetHeadline().GetLastCheckTime() : DateTime.MinValue);
        }

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        public static void WebGetHeadlineOfCurrentStation()
        {
            if (currentStation != null)
            {
                currentStation.GetHeadline().WebGetHeadline();
            }
        }

        /// <summary>
        /// ヘッドラインの設定フォームを表示する
        /// </summary>
        public static void ShowSettingForm(Station station)
        {
            if (station != null)
            {
                station.GetHeadline().ShowSettingForm();
            }
        }

        /// <summary>
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="channel">番組</param>
        public static void ShowPropertyFormOfCurrentStation(IChannel channel)
        {
            if (currentStation != null)
            {
                currentStation.GetHeadline().ShowPropertyForm(channel);
            }
        }
    }
}
