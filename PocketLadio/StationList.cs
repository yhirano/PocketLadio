#region ディレクティブを使用する

using System;
using System.Collections;
using PocketLadio.Stations;
using MiscPocketCompactLibrary.Net;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// 放送局の種類列挙
    /// </summary>
    public enum StationKinds
    {
        Netladio, RssPodcast, ShoutCast, Icecast
    };

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
        /// 現在の放送局のヘッドラインを返す
        /// </summary>
        public static IHeadline StationHeadlineOfCurrentStation
        {
            get { return ((currentStation != null) ? currentStation.Headline : null); }
        }

        /// <summary>
        /// 放送局のIDを返す
        /// </summary>
        public static string StationIdOfCurrentStation
        {
            get { return (currentStation != null ? currentStation.Id : string.Empty); }
        }

        /// <summary>
        /// 放送局の名前を返す
        /// </summary>
        public static string StationNameOfCurrentStation
        {
            get { return (currentStation != null ? currentStation.Name : string.Empty); }
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
        /// 放送局を生成して返す
        /// </summary>
        /// <param name="name">放送局の名前</param>
        /// <param name="stationKind">放送局の種類</param>
        /// <returns>生成した放送局</returns>
        public static Station CreateStation(string name, StationKinds stationKind)
        {
            string id;
            bool isExistId = false;

            do
            {
                id = DateTime.Now.ToString("yyyyMMddHHmmssff");
                isExistId = false;
                foreach (Station station in GetStationList())
                {
                    if (station.Id == id)
                    {
                        isExistId = true;
                        break;
                    }
                }
            } while (isExistId == true);

            return new Station(id, name, stationKind);
        }

        /// <summary>
        /// 放送局のリストをセットする
        /// </summary>
        /// <param name="Stations">放送局のリスト</param>
        /// <returns></returns>
        public static void SetStationList(Station[] stations)
        {
            // 放送局に変化があったか
            bool changed = false;

            // 放送局に変化があったかを調べる
            if (stations.Length != StationList.stations.Length)
            {
                changed = true;
            }
            else
            {
                for (int i = 0; i < stations.Length && i < StationList.stations.Length; ++i)
                {
                    if (stations[i] != StationList.stations[i])
                    {
                        changed = true;
                        break;
                    }
                }
            }

            if (changed == true)
            {
                StationList.stations = stations;

                // 現在の放送局をクリアする
                currentStation = null;

                OnStationListChanged();
            }
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
            if (FilterEnable == true)
            {
                return currentStation.Headline.GetChannelsFiltered();
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
        public static void FetchHeadlineOfCurrentStation()
        {
            if (currentStation != null)
            {
                if (HeadlineFetch != null)
                {
                    currentStation.Headline.HeadlineFetch += HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    currentStation.Headline.HeadlineFetching += HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    currentStation.Headline.HeadlineFetched += HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    currentStation.Headline.HeadlineAnalyze += HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    currentStation.Headline.HeadlineAnalyzing += HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    currentStation.Headline.HeadlineAnalyzed += HeadlineAnalyzed;
                }

                currentStation.Headline.FetchHeadline();

                if (HeadlineFetch != null)
                {
                    currentStation.Headline.HeadlineFetch -= HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    currentStation.Headline.HeadlineFetching -= HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    currentStation.Headline.HeadlineFetched -= HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    currentStation.Headline.HeadlineAnalyze -= HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    currentStation.Headline.HeadlineAnalyzing -= HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    currentStation.Headline.HeadlineAnalyzed -= HeadlineAnalyzed;
                }
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

        /// <summary>
        /// ヘッドラインをネットから取得する前に発生するイベント
        /// </summary>
        public static event FetchEventHandler HeadlineFetch;

        /// <summary>
        /// ヘッドラインをネットから取得している最中に発生するイベント
        /// </summary>
        public static event FetchEventHandler HeadlineFetching;

        /// <summary>
        /// ヘッドラインをネットから取得した後に発生するイベント
        /// </summary>
        public static event FetchEventHandler HeadlineFetched;

        /// <summary>
        /// ヘッドラインを解析する前に発生するイベント
        /// </summary>
        public static event HeadlineAnalyzeEventHandler HeadlineAnalyze;

        /// <summary>
        /// ヘッドラインを解析している最中に発生するイベント
        /// </summary>
        public static event HeadlineAnalyzeEventHandler HeadlineAnalyzing;

        /// <summary>
        /// ヘッドラインを解析した後に発生するイベント
        /// </summary>
        public static event HeadlineAnalyzeEventHandler HeadlineAnalyzed;

        /// <summary>
        /// StationListに変化があった場合に起こるイベント
        /// </summary>
        public static event EventHandler StationListChanged;

        private static void OnStationListChanged()
        {
            if (StationListChanged != null)
            {
                StationListChanged(null, EventArgs.Empty);
            }
        }
    }
}
