using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace PocketLadio.Stations.Util
{
    /// <summary>
    /// Headline用の共通ユーティリティ
    /// </summary>
    public sealed class HeadlineUtil
    {
        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private HeadlineUtil()
        {
        }

        /// <summary>
        /// HTTPレスポンスをストリームとして返す。
        /// プロキシ設定やタイムアウトなどの情報については、PocketLadio.UserSettingやControllerを参照している。
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>HTTPレスポンスのストリーム</returns>
        public static Stream GetHttpStream(string url) {
            Stream st = null;
            try
            {
                WebRequest req = WebRequest.Create(url);
                req.Timeout = PocketLadioInfo.WebRequestTimeoutMillSec;

                // HTTPプロトコルでネットにアクセスする場合
                if (req.GetType() == typeof(System.Net.HttpWebRequest))
                {
                    // UserAgentを付加
                    ((HttpWebRequest)req).UserAgent = PocketLadioInfo.UserAgent;

                    // プロキシの設定が存在した場合、プロキシを設定
                    if (PocketLadio.UserSetting.ProxyUse == true && !(PocketLadio.UserSetting.ProxyServer.Length == 0 || PocketLadio.UserSetting.ProxyPort.Length == 0))
                    {
                        ((HttpWebRequest)req).Proxy =
                            new WebProxy(PocketLadio.UserSetting.ProxyServer, int.Parse(PocketLadio.UserSetting.ProxyPort));
                    }
                }

                WebResponse Result = req.GetResponse();
                st = Result.GetResponseStream();
            }
            catch (WebException)
            {
                throw;
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (UriFormatException)
            {
                throw;
            }
            catch (SocketException)
            {
                throw;
            }

            return st;
        }
    }
}
