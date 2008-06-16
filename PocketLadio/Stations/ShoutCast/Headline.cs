#region �f�B���N�e�B�u���g�p����

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using System.Diagnostics;
using System.Globalization;
using PocketLadio.Stations;
using MiscPocketCompactLibrary.Net;
using MiscPocketCompactLibrary.Reflection;

#endregion

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
        /// SHOUTcast��URL
        /// </summary>
        public const string SHOUTCAST_URL = "http://www.shoutcast.com";

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
        private Channel[] _channels = new Channel[0];

        /// <summary>
        /// �ԑg�̃��X�g
        /// </summary>
        private Channel[] channels
        {
            get { return _channels; }
            set
            {
                filtedChannelsCache = null;
                _channels = value;
            }
        }

        /// <summary>
        /// �t�B���^�[�ςݔԑg�̃L���b�V��
        /// </summary>
        private Channel[] filtedChannelsCache;

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
        /// �\�[�g�̎��
        /// </summary>
        public enum SortKinds
        {
            None, Title, Listener, ListenerTotal, BitRate
        }

        /// <summary>
        /// �\�[�g�̏����E�~��
        /// </summary>
        public enum SortScendings
        {
            Ascending, Descending
        }

        #region HTML��͗p���K�\��

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Path��͗p�B
        /// </summary>
        private readonly static Regex pathRegex =
            new Regex(@"<a\s+[^>]*href=""(.*playlist\.pls[^""]*)""[^>]*>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Rank��͗p�B
        /// </summary>
        private readonly static Regex rankRegex =
            new Regex(@"(\d+)</b>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Category��͗p�B
        /// </summary>
        private readonly static Regex categoryRegex =
            new Regex(@"^.*(\[.+?\])", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// ClusterUrl��͗p�B
        /// </summary>
        private readonly static Regex clusterUrlRegex =
            new Regex(@"<a\s+[^>]*href=""([^""]*)""[^>]*>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Title��͗p�B
        /// Willcom�������T�[�r�X�p�B
        /// </summary>
        private readonly static Regex titleRegex =
            new Regex(@"(.+?)</a>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Listener��͗p�B
        /// </summary>
        private readonly static Regex listenerRegex =
            new Regex(@"(\d+)/(\d+)</font>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Playing��͗p1�B
        /// </summary>
        private readonly static Regex playingNowRegex =
            new Regex(@"Now Playing:</font>(.*)", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Playing��͗p2�B
        /// </summary>
        private readonly static Regex playingRegex =
            new Regex(@"\s*(.+?)</font.*$", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// BitRate��͗p�B
        /// Willcom�������T�[�r�X�p�B
        /// </summary>
        private readonly static Regex bitRateRegex =
            new Regex(@"(\d+)</font>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Rank�炵���s�̉�͗p�B
        /// </summary>
        private readonly static Regex maybeRankLineRegex =
            new Regex(@"^.*</b>", RegexOptions.None);

        #endregion

        /// <summary>
        /// �e������
        /// </summary>
        private readonly Station parentStation;

        /// <summary>
        /// �e������
        /// </summary>
        public virtual Station ParentStation
        {
            get { return parentStation; }
        }

        /// <summary>
        /// �w�b�h���C���̃R���X�g���N�^
        /// </summary>
        /// <param name="id">�w�b�h���C����ID</param>
        /// <param name="parentStation">�e������</param>
        public Headline(string id, Station parentStation)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Headline��ID��Null�͎w��ł��܂���");
            }
            if (id == string.Empty)
            {
                throw new ArgumentException("Headline��ID�ɋ󕶎��͎w��ł��܂���");
            }
            if (parentStation == null)
            {
                throw new ArgumentNullException("Headline�̐e�����ǂ�Null�͎w��ł��܂���");
            }

            this.id = id;
            this.parentStation = parentStation;
            setting = new UserSetting(this);
            setting.LoadSetting();
            setting.FilterChanged += new EventHandler(setting_FilterChanged);
        }

        private void setting_FilterChanged(object sender, EventArgs e)
        {
            // �t�B���^�[�������ς�����ꍇ�́A�t�B���^�[�ԑg�L���b�V������ɂ���
            filtedChannelsCache = null;
        }

        /// <summary>
        /// �N�����̏��������\�b�h�B
        /// </summary>
        public static void StartUpInitialize()
        {
            ;
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
        /// �t�B���^�����O�����ԑg�̌��ʂ�Ԃ�
        /// </summary>
        /// <returns>�t�B���^�����O�����ԑg�̃��X�g</returns>
        public virtual IChannel[] GetChannelsFiltered()
        {
            // �t�B���^�[���ʃL���b�V������̏ꍇ�A�t�B���^�[���ʂ��L���b�V���Ɋi�[
            if (filtedChannelsCache == null)
            {
                ArrayList alChannels = new ArrayList();

                #region �P��t�B���^�[����

                // ��v�P��t�B���^�[�E���O�t�B���^�[�����݂���ꍇ
                if (setting.GetFilterMatchWords().Length > 0 && setting.GetFilterExclusionWords().Length > 0)
                {
                    foreach (IChannel channel in GetChannels())
                    {
                        if (IsMatchFilterMatchWords(channel) == true && IsMatchFilterExclusionWords(channel) == false)
                        {
                            alChannels.Add(channel);
                        }
                    }
                }
                // ��v�P��t�B���^�[�݂̂����݂���ꍇ
                else if (setting.GetFilterMatchWords().Length > 0 && setting.GetFilterExclusionWords().Length <= 0)
                {
                    foreach (IChannel channel in GetChannels())
                    {
                        if (IsMatchFilterMatchWords(channel) == true)
                        {
                            alChannels.Add(channel);
                        }
                    }
                }
                // ���O�t�B���^�[�݂̂����݂���ꍇ
                else if (setting.GetFilterMatchWords().Length <= 0 && setting.GetFilterExclusionWords().Length > 0)
                {
                    foreach (IChannel channel in GetChannels())
                    {
                        if (IsMatchFilterExclusionWords(channel) == false)
                        {
                            alChannels.Add(channel);
                        }
                    }
                }
                // �P��t�B���^�[�����݂��Ȃ��ꍇ
                else
                {
                    alChannels.AddRange(GetChannels());
                }

                #endregion

                #region �Œ�r�b�g���[�g�t�B���^�[����

                ArrayList alDeleteChannels = new ArrayList();

                // �Œ�r�b�g���[�g�t�B���^�[�����݂���ꍇ
                if (setting.FilterAboveBitRateUse == true)
                {
                    // �폜����ԑg�̃��X�g���쐬
                    foreach (Channel channel in alChannels)
                    {
                        if (0 < channel.BitRate && channel.BitRate < setting.FilterAboveBitRate)
                        {
                            alDeleteChannels.Add(channel);
                        }
                    }
                    // �ԑg���폜
                    foreach (Channel deleteChannel in alDeleteChannels)
                    {
                        alChannels.Remove(deleteChannel);
                    }
                }

                #endregion

                #region �ő�r�b�g���[�g�t�B���^�[����

                alDeleteChannels.Clear();

                // �ő�r�b�g���[�g�t�B���^�[�����݂���ꍇ
                if (setting.FilterBelowBitRateUse == true)
                {
                    foreach (Channel channel in alChannels)
                    {
                        if (channel.BitRate > setting.FilterBelowBitRate)
                        {
                            alDeleteChannels.Add(channel);
                        }
                    }
                    // �ԑg���폜
                    foreach (Channel deleteChannel in alDeleteChannels)
                    {
                        alChannels.Remove(deleteChannel);
                    }
                }

                #endregion

                #region �\�[�g����

                if (setting.SortKind == SortKinds.None)
                {
                    ;
                }
                else if (setting.SortKind == SortKinds.Title)
                {
                    alChannels.Sort((IComparer)new ChannelTitleComparer());

                }
                else if (setting.SortKind == SortKinds.Listener)
                {
                    alChannels.Sort((IComparer)new ChannelListenerComparer());
                }
                else if (setting.SortKind == SortKinds.ListenerTotal)
                {
                    alChannels.Sort((IComparer)new ChannelListenerTotalComparer());
                }
                else if (setting.SortKind == SortKinds.BitRate)
                {
                    alChannels.Sort((IComparer)new ChannelBitRateComparer());
                }
                else
                {
                    // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                    Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
                }

                // �~���̏ꍇ
                if (setting.SortKind != SortKinds.None && setting.SortScending == SortScendings.Descending)
                {
                    alChannels.Reverse();
                }

                #endregion

                // �t�B���^�[���ʂ��L���b�V���Ɋi�[
                filtedChannelsCache = (Channel[])alChannels.ToArray(typeof(Channel));
            }

            return filtedChannelsCache;
        }


        /// <summary>
        /// �ԑg����v�P��t�B���^�[�ɍ��v���邩�𒲂ׂ�
        /// </summary>
        /// <param name="channel">�ԑg</param>
        /// <returns>�ԑg����v�P��t�B���^�[�ɍ��v������true�A����ȊO��false</returns>
        private bool IsMatchFilterMatchWords(IChannel channel)
        {
            foreach (string filter in setting.GetFilterMatchWords())
            {
                foreach (string filted in channel.GetFilteredWords())
                {
                    if (filted.ToLower(CultureInfo.InvariantCulture).IndexOf(filter.ToLower(CultureInfo.InvariantCulture)) != -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// �ԑg�����O�P��t�B���^�[�ɍ��v���邩�𒲂ׂ�
        /// </summary>
        /// <param name="channel">�ԑg</param>
        /// <returns>�ԑg�����O�P��t�B���^�[�ɍ��v������true�A����ȊO��false</returns>
        private bool IsMatchFilterExclusionWords(IChannel channel)
        {
            foreach (string filter in setting.GetFilterExclusionWords())
            {
                foreach (string filted in channel.GetFilteredWords())
                {
                    if (filted.ToLower(CultureInfo.InvariantCulture).IndexOf(filter.ToLower(CultureInfo.InvariantCulture)) != -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����
        /// </summary>
        public virtual void FetchHeadline()
        {
            // �������Z�b�g����
            lastCheckTime = DateTime.Now;

            WebStream st = null;

            try
            {
                // �`�����l���̃��X�g
                ArrayList alChannels = new ArrayList();
                Channel channel = null;

                string searchWord = ((setting.SearchWord.Length != 0) ? "&s=" + setting.SearchWord : string.Empty);
                // ���p�X�y�[�X�ƑS�p�X�y�[�X��+�ɒu�������� SHOUTcast���URL��AND�����̃X�y�[�X��+�ɒu���������邽��
                searchWord = searchWord.Replace(' ', '+').Replace("�@", "+");

                string perView = ((setting.PerView.ToString().Length != 0) ? "&numresult=" + setting.PerView : string.Empty);
                Uri url = new Uri(Headline.SHOUTCAST_URL + "/?" + searchWord + perView);

                st = PocketLadioUtility.GetWebStream(url);
                WebTextFetch fetch = new WebTextFetch(st, Encoding.GetEncoding("Windows-1252"));
                if (HeadlineFetch != null)
                {
                    fetch.Fetch += HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    fetch.Fetching += HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    fetch.Fetched += HeadlineFetched;
                }
                string httpString = fetch.ReadToEnd();

#if SHOUTCAST_HTTP_LOG
                // Shoutcast��HTTP�̃��O�������o��
                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(
                            AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.ShoutcastHttpLog,
                            false,
                            Encoding.GetEncoding("Windows-1252"));
                    sw.Write(httpString);
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                    }
                }
#endif

                // �^�O�̌�ɉ��s������iWillcom�������T�[�r�X�Ή��̂��߁j
                httpString = httpString.Replace(">", ">\n");

                string[] lines = httpString.Split('\n');

                #region HTML���

                // ���ʂ炵���s
                string maybeRankLine = string.Empty;

                // 1�`�w��s�ڂ܂ł�HTML����͂��Ȃ�
                int analyzeHtmlFirstTo = setting.IgnoreHtmlAnalyzeFirstTo;
                // �w��s�ڂ���s���܂ł�HTML����͂��Ȃ�
                int analyzeHtmlLast = lines.Length - setting.IgnoreHtmlAnalyzeEndFrom;

                OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, -1));

                // ��͂����w�b�h���C���̌�
                int analyzedCount = 0;

                // HTML���
                for (int lineNumber = analyzeHtmlFirstTo; lineNumber < analyzeHtmlLast && lineNumber < lines.Length; ++lineNumber)
                {
                    /*** playlist.pls������ ***/
                    Match pathMatch = pathRegex.Match(lines[lineNumber]);

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
                            categoryMatch = categoryRegex.Match(lines[lineNumber]);

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
                            clusterUrlMatch = clusterUrlRegex.Match(lines[lineNumber]);

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
                            titleMatch = titleRegex.Match(lines[lineNumber]);

                            // Title�����������ꍇ
                            if (titleMatch.Success)
                            {
                                channel.Title = titleMatch.Groups[1].Value;
                                break;
                            }
                        }

                        /*** Listener������ ***/
                        Match listenerMatch = listenerRegex.Match(lines[lineNumber]);
                        for (; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            listenerMatch = listenerRegex.Match(lines[lineNumber]);

                            if (listenerMatch.Success)
                            {
                                break;
                            }

                            // Now Playing:�͑��݂��Ȃ��ꍇ������̂Ń��X�i�[�����o�̒��Ń`�F�b�N���s��
                            Match playingNowMatch = playingNowRegex.Match(lines[lineNumber]);
                            if (playingNowMatch.Success)
                            {
                                Match playingMatch = playingRegex.Match(playingNowMatch.Groups[1].Value);
                                if (playingMatch.Success)
                                {
                                    channel.Playing = playingMatch.Groups[1].Value;
                                }
                            }

                        }
                        try
                        {
                            channel.Listener = int.Parse(listenerMatch.Groups[1].Value);
                            channel.ListenerTotal = int.Parse(listenerMatch.Groups[2].Value);
                        }
                        catch (ArgumentException)
                        {
                            ;
                        }
                        catch (FormatException)
                        {
                            ;
                        }
                        catch (OverflowException)
                        {
                            ;
                        }

                        /*** Bitrate������ ***/
                        Match bitrateMatch;

                        // Bitrate��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                        for (++lineNumber; lineNumber < analyzeHtmlLast; ++lineNumber)
                        {
                            bitrateMatch = bitRateRegex.Match(lines[lineNumber]);

                            // Bitrate�����������ꍇ
                            if (bitrateMatch.Success)
                            {
                                try
                                {
                                    channel.BitRate = int.Parse(bitrateMatch.Groups[1].Value);
                                    break;
                                }
                                catch (ArgumentException)
                                {
                                    ;
                                }
                                catch (FormatException)
                                {
                                    ;
                                }
                                catch (OverflowException)
                                {
                                    ;
                                }
                            }
                        }
                        alChannels.Add(channel);
                        OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++analyzedCount, -1));
                    }

                    /*** Rank�炵���s��ۑ����� ***/
                    Match maybeRankLineMatch = maybeRankLineRegex.Match(lines[lineNumber]);

                    if (maybeRankLineMatch.Success)
                    {
                        maybeRankLine = lines[lineNumber];

                    }
                }

                OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(analyzedCount, analyzedCount));

                channels = (Channel[])alChannels.ToArray(typeof(Channel));

                #endregion

            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
            }
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����O�ɔ�������C�x���g
        /// </summary>
        public event FetchEventHandler HeadlineFetch;

        /// <summary>
        /// HeadlineFetch�C�x���g�̎��s
        /// </summary>
        /// <param name="e">�C�x���g</param>
        private void OnHeadlineFetch(FetchEventArgs e)
        {
            if (HeadlineFetch != null)
            {
                HeadlineFetch(this, e);
            }
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾���Ă���Œ��ɔ�������C�x���g
        /// </summary>
        public event FetchEventHandler HeadlineFetching;

        /// <summary>
        /// HeadlineFetching�C�x���g�̎��s
        /// </summary>
        /// <param name="e">�C�x���g</param>
        private void OnHeadlineFetching(FetchEventArgs e)
        {
            if (HeadlineFetching != null)
            {
                HeadlineFetching(this, e);
            }
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾������ɔ�������C�x���g
        /// </summary>
        public event FetchEventHandler HeadlineFetched;

        /// <summary>
        /// HeadlineFetched�C�x���g�̎��s
        /// </summary>
        /// <param name="e">�C�x���g</param>
        private void OnHeadlineFetched(FetchEventArgs e)
        {
            if (HeadlineFetched != null)
            {
                HeadlineFetched(this, e);
            }
        }

        /// <summary>
        /// �w�b�h���C������͂���O�ɔ�������C�x���g
        /// </summary>
        public event HeadlineAnalyzeEventHandler HeadlineAnalyze;

        /// <summary>
        /// HeadlineAnalyze�C�x���g�̎��s
        /// </summary>
        /// <param name="e">�C�x���g</param>
        private void OnHeadlineAnalyze(HeadlineAnalyzeEventArgs e)
        {
            if (HeadlineAnalyze != null)
            {
                HeadlineAnalyze(this, e);
            }
        }

        /// <summary>
        /// �w�b�h���C������͂��Ă���Œ��ɔ�������C�x���g
        /// </summary>
        public event HeadlineAnalyzeEventHandler HeadlineAnalyzing;

        /// <summary>
        /// HeadlineAnalyzing�C�x���g�̎��s
        /// </summary>
        /// <param name="e">�C�x���g</param>
        private void OnHeadlineAnalyzing(HeadlineAnalyzeEventArgs e)
        {
            if (HeadlineAnalyzing != null)
            {
                HeadlineAnalyzing(this, e);
            }
        }

        /// <summary>
        /// �w�b�h���C������͂�����ɔ�������C�x���g
        /// </summary>
        public event HeadlineAnalyzeEventHandler HeadlineAnalyzed;

        /// <summary>
        /// HeadlineAnalyzed�C�x���g�̎��s
        /// </summary>
        /// <param name="e">�C�x���g</param>
        private void OnHeadlineAnalyzed(HeadlineAnalyzeEventArgs e)
        {
            if (HeadlineAnalyzed != null)
            {
                HeadlineAnalyzed(this, e);
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
            SettingForm settingForm = new SettingForm(setting);
            settingForm.ShowDialog();
            settingForm.Dispose();
        }

        /// <summary>
        /// �t�B���^�[�ɒP���o�^���邽�߂Ƀw�b�h���C���ݒ�t�H�[����\������
        /// </summary>
        /// <param name="filterWord">�t�B���^�[�ɒǉ�����P��</param>
        public void ShowSettingFormForAddFilter(string filterWord)
        {
            SettingForm settingForm = new SettingForm(setting);
            settingForm.ShowDialogForAddWordFilter(filterWord);
            settingForm.Dispose();
        }

        /// <summary>
        /// �ݒ��ۑ����Ă����t�@�C�����폜����
        /// </summary>
        public virtual void DeleteUserSettingFile()
        {
            setting.DeleteUserSettingFile();
        }

        #region �\�[�g�p��r�N���X

        /// <summary>
        /// �^�C�g�����r
        /// </summary>
        class ChannelTitleComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Title.CompareTo(((Channel)object2).Title);
            }
        }

        /// <summary>
        /// ���X�i�����r
        /// </summary>
        class ChannelListenerComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Listener - ((Channel)object2).Listener;
            }
        }

        /// <summary>
        /// �q�׃��X�i�����r
        /// </summary>
        class ChannelListenerTotalComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).ListenerTotal - ((Channel)object2).ListenerTotal;
            }
        }

        /// <summary>
        /// �r�b�g���[�g���r
        /// </summary>
        class ChannelBitRateComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).BitRate - ((Channel)object2).BitRate;
            }
        }

        #endregion
    }
}
