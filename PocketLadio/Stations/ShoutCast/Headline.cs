using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using PocketLadio.Utility;
using PocketLadio.Stations;

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// SHOUTcast�̃w�b�h���C��
    /// </summary>
    public class Headline : PocketLadio.Stations.IHeadline
    {
        /// <summary>
        /// �w�b�h���C���̎��
        /// </summary>
        private const string KIND_NAME = "SHOUTcast";

        /// <summary>
        /// �w�b�h���C����ID�i�w�b�h���C�������ʂ��邽�߂̃L�[�j
        /// </summary>
        private readonly string id;

        /// <summary>
        /// �w�b�h���C���̐ݒ�
        /// </summary>
        private UserSetting setting;

        /// <summary>
        /// �ԑg�̃��X�g
        /// </summary>
        private Channel[] channels = new Channel[0];

        /// <summary>
        /// �w�b�h���C�����擾��������
        /// </summary>
        private DateTime lastCheckTime = DateTime.MinValue;

        /// <summary>
        /// �ԑg�̕\�����@�ݒ�
        /// </summary>
        public string HeadlineViewType
        {
            get { return setting.HeadlineViewType; }
        }

        /// <summary>
        /// SHOUTcast��URL
        /// </summary>
        public Uri ShoutCastUrl
        {
            get { return UserSetting.SHOUTCAST_URL; }
        }

        #region HTML��͗p���K�\��

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Path��͗p�B
        /// </summary>
        private readonly static Regex pathRegex = new Regex(
            @"<a\s+[^>]*href\s*=\s*(?:""(?<1>.*playlist\.pls[^""]*)""|(?<1>.*playlist\.pls[^\s>]+))[^>]*>(?<2>[^<]*)",
            RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Rank��͗p�B
        /// </summary>
        private readonly static Regex rankRegex = new Regex(@"(\d+)</b>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Category��͗p�B
        /// </summary>
        private readonly static Regex categoryRegex = new Regex(@"^.*(\[.+?\])", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// ClusterUrl��͗p�B
        /// </summary>
        private readonly static Regex clusterUrlRegex = new Regex(
            @"<a\s+[^>]*href\s*=\s*(?:""(?<1>.*[^""]*)""|(?<1>.*[^\s>]+))[^>]*>(?<2>[^<]*)",
            RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Title��͗p�B
        /// </summary>
        private readonly static Regex titleRegex = new Regex(@"<a.*[^>]*>(.+?)</a>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Listener��͗p�B
        /// </summary>
        private readonly static Regex listenerRegex = new Regex(@"\D*(\d+/\d+)</font>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Playing��͗p1�B
        /// </summary>
        private readonly static Regex playingNowRegex = new Regex(@"^.*Now Playing:</font>(.*)", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Playing��͗p2�B
        /// </summary>
        private readonly static Regex playingRegex = new Regex(@"\s*(.+?)</font.*$", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// BitRate��͗p�B
        /// </summary>
        private readonly static Regex bitRateRegex = new Regex(@"\D(\d+)</font>", RegexOptions.None);

        /// HTML��͗p���K�\���B
        /// </summary>
        private readonly static Regex maybeRankLineRegex = new Regex(@"^.*</b>", RegexOptions.None);

        #endregion

        /// <summary>
        /// Max Bit Rate�ݒ�̐ݒ�\���Ǝ��ےl�������t�@�C��
        /// </summary>
        private const string SHOUTCAST_MAX_BIT_RATE_SETTING_FILE
            = "PocketLadio.Resource.ShoutCastMaxBitRateSetting.txt";

        /// <summary>
        /// �w�b�h���C���̃R���X�g���N�^
        /// </summary>
        /// <param name="ID">�w�b�h���C����ID</param>
        public Headline(string id)
        {
            this.id = id;
            setting = new UserSetting(this);

            try
            {
                setting.LoadSetting();
            }
            catch (XmlException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
        }

        /// <summary>
        /// �N�����̏��������\�b�h�BMax bit rate�ݒ�̃n�b�V���̏��������s��
        /// </summary>
        public static void StartUpInitialize()
        {
            StreamReader sr = null;

            try
            {
                // ���݂̃R�[�h�����s���Ă���Assembly���擾
                System.Reflection.Assembly thisAssembly
                    = System.Reflection.Assembly.GetExecutingAssembly();
                // �w�肳�ꂽ�}�j�t�F�X�g���\�[�X��ǂݍ���
                sr =
                    new StreamReader(thisAssembly.GetManifestResourceStream(SHOUTCAST_MAX_BIT_RATE_SETTING_FILE),
                    Encoding.GetEncoding("shift-jis"));
                // ���e��ǂݍ���
                string bitRateString = sr.ReadToEnd();

                string[] maxBitRateRawArray = bitRateString.Split('\n');

                foreach (string maxBitRateRaw in maxBitRateRawArray)
                {
                    if (maxBitRateRaw.Length != 0)
                    {
                        string[] maxBitRate = maxBitRateRaw.Split(',');
                        UserSetting.MaxBitRateTable.Add(maxBitRate[0], maxBitRate[1].Trim());
                    }
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

        }

        /// <summary>
        /// �w�b�h���C����ID��Ԃ�
        /// </summary>
        /// <returns>�w�b�h���C����ID</returns>
        public virtual string GetId()
        {
            return id;
        }

        /// <summary>
        /// �w�b�h���C���̎�ނ̖��O��Ԃ�
        /// </summary>
        /// <returns>�w�b�h���C���̎�ނ̖��O</returns>
        public virtual string GetKindName()
        {
            return KIND_NAME;
        }

        /// <summary>
        /// �擾���Ă���ԑg�̃��X�g��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̃��X�g</returns>
        public virtual IChannel[] GetChannels()
        {
            return channels;
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����
        /// </summary>
        public virtual void WebGetHeadline()
        {
            // �������Z�b�g����
            lastCheckTime = DateTime.Now;

            Stream st = null;
            StreamReader sr = null;

            try
            {
                // �`�����l���̃��X�g
                ArrayList alChannels = new ArrayList();
                Channel channel = null;

                string searchWord = ((setting.SearchWord.Length != 0) ? "?s=" + setting.SearchWord : "");
                // ���p�X�y�[�X�ƑS�p�X�y�[�X��+�ɒu�������� SHOUTcast���URL��AND�����̃X�y�[�X��+�ɒu���������邽��
                searchWord = searchWord.Replace(' ', '+').Replace("�@", "+");

                string perView = ((setting.PerView.ToString().Length != 0) ? "&numresult=" + setting.PerView : "");
                string maxBitRate = ((setting.MaxBitRate.Length != 0) ? "&bitrate=" + setting.MaxBitRate : "");
                Uri url = new Uri(ShoutCastUrl.ToString() + searchWord + perView + maxBitRate);

                st = PocketLadioUtility.GetHttpStream(url);
                sr = new StreamReader(st, Encoding.GetEncoding("Windows-1252"));
                string httpString = sr.ReadToEnd();
                string[] lines = httpString.Split('\n');

                #region HTML���

                // ���ʂ炵���s
                string maybeRankLine = "";

                // 1�`�w��s�ڂ܂ł�HTML����͂��Ȃ�
                int analyzeHtmlFirstTo = setting.IgnoreHtmlAnalyzeFirstTo;
                // �w��s�ڂ���s���܂ł�HTML����͂��Ȃ�
                int analyzeHtmlLast = lines.Length - setting.IgnoreHtmlAnalyzeEndFrom;

                // HTML���
                for (int lineNumber = analyzeHtmlFirstTo; lineNumber < analyzeHtmlLast; ++lineNumber)
                {
                    /*** playlist.pls������ ***/
                    Match pathMatch = pathRegex.Match(lines[lineNumber].Trim());

                    // playlist.pls�����������ꍇ
                    if (pathMatch.Success)
                    {
                        channel = new Channel(this);

                        channel.Path = pathMatch.Groups[1].Value;

                        /*** Rank������ ***/
                        Match rankMatch = rankRegex.Match(maybeRankLine);

                        // Rank�����������ꍇ
                        if (rankMatch.Success)
                        {
                            channel.Rank = rankMatch.Groups[1].Value;
                        }

                        /*** Category������ ***/
                        Match categoryMatch;

                        // Category��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                        for (++lineNumber; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            categoryMatch = categoryRegex.Match(lines[lineNumber].Trim());

                            // Category�����������ꍇ
                            if (categoryMatch.Success)
                            {
                                channel.Category = categoryMatch.Groups[1].Value;
                                break;
                            }
                        }

                        /*** ClusterUrl������ ***/
                        Match clusterUrlMatch;

                        // ClusterUrl��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                        for (; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            clusterUrlMatch = clusterUrlRegex.Match(lines[lineNumber].Trim());

                            // Category�����������ꍇ
                            if (clusterUrlMatch.Success)
                            {
                                try
                                {
                                    channel.ClusterUrl = new Uri(clusterUrlMatch.Groups[1].Value);
                                }
                                catch (UriFormatException)
                                {
                                    channel.ClusterUrl = null;
                                }
                                break;
                            }
                        }

                        /*** Title������ ***/
                        Match titleMatch;

                        // Title��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                        for (; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            titleMatch = titleRegex.Match(lines[lineNumber].Trim());

                            // Title�����������ꍇ
                            if (titleMatch.Success)
                            {
                                channel.Title = titleMatch.Groups[1].Value;
                                break;
                            }
                        }

                        /*** Listener������ ***/
                        Match listenerMatch = listenerRegex.Match(lines[lineNumber].Trim());
                        for (; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            listenerMatch = listenerRegex.Match(lines[lineNumber].Trim());
                            if (listenerMatch.Success)
                            {
                                break;
                            }

                            // Now Playing:�͑��݂��Ȃ��ꍇ������̂Ń��X�i�[�����o�̒��Ń`�F�b�N���s��
                            Match playingNowMatch = playingNowRegex.Match(lines[lineNumber].Trim());
                            if (playingNowMatch.Success)
                            {
                                Match playingMatch = playingRegex.Match(playingNowMatch.Groups[1].Value.Trim());
                                if (playingMatch.Success)
                                {
                                    channel.Playing = playingMatch.Groups[1].Value;
                                }
                            }

                        }
                        channel.Listener = listenerMatch.Groups[1].Value;

                        /*** Bitrate������ ***/
                        Match bitrateMatch;

                        // Bitrate��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                        for (++lineNumber; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            bitrateMatch = bitRateRegex.Match(lines[lineNumber].Trim());

                            // Bitrate�����������ꍇ
                            if (bitrateMatch.Success)
                            {
                                channel.BitRate = bitrateMatch.Groups[1].Value;
                                break;
                            }
                        }
                        alChannels.Add(channel);
                    }

                    /*** Rank�炵���s��ۑ����� ***/
                    Match maybeRankLineMatch = maybeRankLineRegex.Match(lines[lineNumber]);

                    if (maybeRankLineMatch.Success)
                    {
                        maybeRankLine = lines[lineNumber];
                    }
                }

                channels = (Channel[])alChannels.ToArray(typeof(Channel));

                #endregion
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
            catch (NotSupportedException)
            {
                throw;
            }
            catch (SocketException)
            {
                throw;
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����������Ԃ��B
        /// ���擾�̏ꍇ��DateTime.MinValue��Ԃ��B
        /// </summary>
        /// <returns>�w�b�h���C�����l�b�g����擾��������</returns>
        public virtual DateTime GetLastCheckTime()
        {
            return lastCheckTime;
        }

        /// <summary>
        /// �w�b�h���C���̐ݒ�t�H�[����\������
        /// </summary>
        /// <returns>�w�b�h���C���̐ݒ�t�H�[��</returns>
        public virtual void ShowSettingForm()
        {
            settingForm settingForm = new settingForm(setting);
            settingForm.ShowDialog();
            settingForm.Dispose();
        }

        /// <summary>
        /// �ԑg�̏ڍ׃t�H�[����\������
        /// </summary>
        /// <param name="channel">�ԑg</param>
        /// <returns>�ԑg�̏ڍ׃t�H�[��</returns>
        public virtual void ShowPropertyForm(IChannel channel)
        {
            ChannelPropertyForm channelPropertyForm = new ChannelPropertyForm((Channel)channel);
            channelPropertyForm.ShowDialog();
            channelPropertyForm.Dispose();
        }

        /// <summary>
        /// �ݒ��ۑ����Ă����t�@�C�����폜����
        /// </summary>
        public virtual void DeleteUserSettingFile()
        {
            setting.DeleteUserSettingFile();
        }
    }
}
