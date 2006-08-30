using System;
using System.Collections;
using PocketLadio.StationInterface;

namespace PocketLadio
{
    /// <summary>
    /// 放送局のリスト
    /// </summary>
    public class StationList
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
        private static Station CurrentStation;

        /// <summary>
        /// シングルトンのためprivate
        /// </summary>
        private StationList()
        {
        }

        /// <summary>
        /// 現在の放送局を変更する
        /// </summary>
        /// <param name="param">何番目の放送局か（0から始まる）</param>
        public static void ChangeCurrentStationAt(int param)
        {
            CurrentStation = Stations[param];
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
            CurrentStation = null;
        }

        /// <summary>
        /// 放送局の持つヘッドラインのIDを返す
        /// </summary>
        /// <returns>放送局の持つヘッドラインのID</returns>
        public static string GetHeadlineIDOfCurrentStation()
        {
            if (CurrentStation != null)
            {
                return CurrentStation.GetHeadlineID();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 放送局の名前を返す
        /// </summary>
        /// <returns>放送局の名前</returns>
        public static string GetHeadlineNameOfCurrentStation()
        {
            if (CurrentStation != null)
            {
                return CurrentStation.GetName();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public static IChanel[] GetChanelsOfCurrentStation()
        {
            return CurrentStation.GetHeadline().GetChanels();
        }

        /// <summary>
        /// 取得している番組のリストを返す。
        /// フィルタリングが有効な場合にはフィルタリングした番組の結果を返す。
        /// フィルタリングは指定されたフィルタのor条件となる。
        /// </summary>
        /// <returns>フィルタリングされた番組のリスト</returns>
        public static IChanel[] GetChanelsFilteredOfCurrentStation()
        {
            if (CurrentStation == null)
            {
                return new IChanel[0];
            }

            // フィルタが存在する場合
            if (FilterEnable == true && UserSetting.FilterWords.Length > 0)
            {
                ArrayList AlChanels = new ArrayList();

                foreach (IChanel Chanel in CurrentStation.GetHeadline().GetChanels())
                {
                    foreach (string Filter in UserSetting.FilterWords)
                    {
                        if (Chanel.GetFilterdWord().IndexOf(Filter) != -1 && !AlChanels.Contains(Chanel))
                        {
                            AlChanels.Add(Chanel);
                        }
                    }
                }

                return (IChanel[])AlChanels.ToArray(typeof(IChanel));
            }
            // フィルタが存在しない場合
            else
            {
                return CurrentStation.GetHeadline().GetChanels();
            }
        }

        /// <summary>
        /// 取得しているヘッドラインのネットから最終取得時刻を返す。
        /// 値を返せない場合はDateTime.MinValueを返す。
        /// </summary>
        /// <returns>ヘッドラインのネットから最終取得時刻</returns>
        public static DateTime GetLastCheckTimeOfCurrentStation()
        {
            if (CurrentStation != null)
            {
                return CurrentStation.GetHeadline().GetLastCheckTime();
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        public static void WebGetHeadlineOfCurrentStation()
        {
            if (CurrentStation != null)
            {
                CurrentStation.GetHeadline().WebGetHeadline();
            }
        }

        /// <summary>
        /// ヘッドラインの設定フォームを表示する
        /// </summary>
        public static void ShowSettingForm(Station station)
        {
            station.GetHeadline().ShowSettingForm();
        }

        /// <summary>
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="Chanel">番組</param>
        public static void ShowPropertyFormOfCurrentStation(IChanel Chanel)
        {
            if (CurrentStation != null)
            {
                CurrentStation.GetHeadline().ShowPropertyForm(Chanel);
            }
        }
    }
}
