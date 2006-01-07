using System;
using System.Collections;
using PocketLadio.Netladio;

namespace PocketLadio
{
    /// <summary>
    /// チャンネルのリスト
    /// </summary>
    public class ChanelList
    {
        /// <summary>
        /// フィルターの有効無効
        /// </summary>
        public static bool FilterEnable = false;
        
        /// <summary>
        /// シングルトンのためprivate
        /// </summary>
        private ChanelList()
        {
        }

        /// <summary>
        /// 取得しているチャンネルのリストを返す。
        /// フィルタリングが有効な場合にはフィルタリングしたチャンネルの結果を返す。
        /// フィルタリングは指定されたフィルタのor条件となる。
        /// </summary>
        /// <returns>フィルタリングされたチャンネルのリスト</returns>
        public static Chanel[] GetChanels()
        {
            // フィルタが存在する場合
            if (FilterEnable == true && UserSetting.FilterWords.Length > 0)
            {
                ArrayList AlChanels = new ArrayList();

                foreach (Chanel Chanel in Headline.GetChanels())
                {
                    foreach (string Filter in PocketLadio.UserSetting.FilterWords)
                    {
                        if (Chanel.Gnl.IndexOf(Filter) != -1 || Chanel.Nam.IndexOf(Filter) != -1)
                        {
                            AlChanels.Add(Chanel);
                        }
                    }
                }

                return (Chanel[])AlChanels.ToArray(typeof(Chanel));
            }
            // フィルタが存在しない場合
            else
            {
                return Headline.GetChanels();
            }
        }

    }
}
