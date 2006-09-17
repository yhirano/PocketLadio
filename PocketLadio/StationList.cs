#region ディレクティブを使用する

using System;
using System.Collections;
using PocketLadio.Stations;

#endregion

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
        private static bool filterEnable;

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
        /// 現在の放送局
        /// </summary>
        private static Station currentStation;

        /// <summary>
        /// 放送局のIDを返す
        /// </summary>
        public static string StationIdOfCurrentStation
        {
            get { return (currentStation != null ? currentStation.Id : ""); }
        }

        /// <summary>
        /// 放送局の名前を返す
        /// </summary>
        public static string StationNameOfCurrentStation
        {
            get { return (currentStation != null ? currentStation.Name : ""); }
        }

        /// <summary>
        /// 取得しているヘッドラインのネットから最終取得時刻を返す。
        /// 値を返せない場合はDateTime.MinValueを返す。
        /// </summary>
        public static DateTime LastCheckTimeOfCurrentStation
        {
            get { return (currentStation != null ? currentStation.Headline.GetLastCheckTime() : DateTime.MinValue); }
        }

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
            currentStation = stations[number];
        }

        /// <summary>
        /// 放送局のリストを返す
        /// </summary>
        /// <returns>放送局のリスト</returns>
        public static Station[] GetStationList()
        {
            return stations;
        }

        /// <summary>
        /// 放送局のリストをセットする
        /// </summary>
        /// <param name="Stations">放送局のリスト</param>
        /// <returns></returns>
        public static void SetStationList(Station[] stations)
        {
            StationList.stations = stations;

            // 現在の放送局をクリアする
            currentStation = null;
        }

        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public static IChannel[] GetChannelsOfCurrentStation()
        {
            return currentStation.Headline.GetChannels();
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

                foreach (IChannel channel in currentStation.Headline.GetChannels())
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
                return currentStation.Headline.GetChannels();
            }
        }

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        public static void WebGetHeadlineOfCurrentStation()
        {
            if (currentStation != null)
            {
                currentStation.Headline.FetchHeadline();
            }
        }

        /// <summary>
        /// ヘッドラインの設定フォームを表示する
        /// </summary>
        public static void ShowSettingForm(Station station)
        {
            if (station != null)
            {
                station.Headline.ShowSettingForm();
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
                channel.ShowPropertyForm();
            }
        }
    }
}
