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
        public const string SHOUTCAST_URL = "http://www.shoutcast.com/directory/search_results.jsp";

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
            None, Title, Listener, BitRate
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
        /// �󔒁B
        /// </summary>
        private readonly static Regex emptyRegex =
            new Regex(@"^\s+$", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// �����ǁB
        /// </summary>
        private readonly static Regex stationRegex =
            new Regex(@"<div\s+[^>]*id=""\d+""[^>]*>$", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Path��͗p�B
        /// </summary>
        private readonly static Regex pathRegex =
            new Regex(@"<a\s+[^>]*href=""(.*tunein-station\.pls[^""]*)""[^>]*>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Title��͗p1�B
        /// </summary>
        private readonly static Regex titleRegex1 =
            new Regex(@"Station:</span>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Title��͗p2�B
        /// </summary>
        private readonly static Regex titleRegex2 =
            new Regex(@"<a\s+[^>]*href=""([^""]*)""[^>]*>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Title��͗p3�B
        /// </summary>
        private readonly static Regex titleRegex3 =
            new Regex(@"^\s*(.+?)$", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Playing��͗p1�B
        /// </summary>
        private readonly static Regex playingNowRegex1 =
            new Regex(@"Now Playing:</span>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Playing��͗p2�B
        /// </summary>
        private readonly static Regex playingNowRegex2 =
            new Regex(@"^\s*(.+?)</span>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Genre��͗p1�B
        /// </summary>
        private readonly static Regex genreRegex1 =
            new Regex(@"Genre:</span>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Genre��͗p2�B
        /// </summary>
        private readonly static Regex genreRegex2 =
            new Regex(@"(.+?)</span>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Listener��͗p1�B
        /// </summary>
        private readonly static Regex listenerRegex1 =
            new Regex(@"Listeners:</span>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// Listener��͗p2�B
        /// </summary>
        private readonly static Regex listenerRegex2 =
            new Regex(@"([\d\,]+)</span>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// BitRate��͗p1�B
        /// </summary>
        private readonly static Regex bitRateRegex1 =
            new Regex(@"Bitrate:</span>", RegexOptions.None);

        /// <summary>
        /// HTML��͗p���K�\���B
        /// BitRate��͗p2�B
        /// </summary>
        private readonly static Regex bitRateRegex2 =
            new Regex(@"(\d+)</span>", RegexOptions.None);

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
                Uri url = new Uri(Headline.SHOUTCAST_URL + "?" + searchWord);

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

                string[] lines = httpString.Split('\n', '\r');
                // ��s����菜��
                {
                    ArrayList alLines = new ArrayList();
                    foreach (string line in lines)
                    {
                        Match emptyMatch = emptyRegex.Match(line);
                        if (line != string.Empty && emptyMatch.Success == false)
                        {
                            alLines.Add(line);
                        }
                    }
                    lines = (string[])alLines.ToArray(typeof(string));
                }

                #region HTML���

                OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, -1));

                // ��͂����w�b�h���C���̌�
                int analyzedCount = 0;

                // HTML���
                for (int lineNumber = 0; lineNumber < lines.Length; ++lineNumber)
                {
                    Match stationMatch = stationRegex.Match(lines[lineNumber]);

                    if (stationMatch.Success)
                    {
                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                        {
                            /*** playlist.pls������ ***/
                            Match pathMatch = pathRegex.Match(lines[lineNumber]);

                            // tunein-station.pls�����������ꍇ
                            if (pathMatch.Success)
                            {
                                channel = new Channel(this);

                                channel.PlayUrl = new Uri(pathMatch.Groups[1].Value);

                                /*** Title������ ***/
                                Match titleMatch;

                                // Title��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    titleMatch = titleRegex1.Match(lines[lineNumber]);

                                    // Title�����������ꍇ
                                    if (titleMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            titleMatch = titleRegex2.Match(lines[lineNumber]);
                                            if (titleMatch.Success)
                                            {
                                                channel.ClusterUrl = new Uri(titleMatch.Groups[1].Value);
                                                break;
                                            }
                                        }

                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            titleMatch = titleRegex3.Match(lines[lineNumber]);
                                            if (titleMatch.Success)
                                            {
                                                channel.Title = titleMatch.Groups[1].Value;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                /*** Playing������ ***/
                                Match playingMatch;

                                // Playing��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    playingMatch = playingNowRegex1.Match(lines[lineNumber]);

                                    // Playing�����������ꍇ
                                    if (playingMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            playingMatch = playingNowRegex2.Match(lines[lineNumber]);
                                            if (playingMatch.Success)
                                            {
                                                channel.Playing = playingMatch.Groups[1].Value;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                /*** Genre������ ***/
                                Match genreMatch;

                                // Genre��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    genreMatch = genreRegex1.Match(lines[lineNumber]);

                                    // Genre�����������ꍇ
                                    if (genreMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            genreMatch = genreRegex2.Match(lines[lineNumber]);
                                            if (genreMatch.Success)
                                            {
                                                channel.Genre = genreMatch.Groups[1].Value;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                /*** Listener������ ***/
                                Match listenerMatch;

                                // Listener��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    listenerMatch = listenerRegex1.Match(lines[lineNumber]);

                                    // Listener�����������ꍇ
                                    if (listenerMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            listenerMatch = listenerRegex2.Match(lines[lineNumber]);
                                            if (listenerMatch.Success)
                                            {
                                                try
                                                {
                                                    channel.Listener = int.Parse(listenerMatch.Groups[1].Value.Replace(",", string.Empty));
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
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                /*** Bitrate������ ***/
                                Match bitRateMatch;

                                // Bitrate��������Ȃ��ꍇ�͍s��ǂݔ�΂��Č�������
                                for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                {
                                    bitRateMatch = bitRateRegex1.Match(lines[lineNumber]);

                                    // Bitrate�����������ꍇ
                                    if (bitRateMatch.Success)
                                    {
                                        for (++lineNumber; lineNumber < lines.Length; ++lineNumber)
                                        {
                                            bitRateMatch = bitRateRegex2.Match(lines[lineNumber]);
                                            if (bitRateMatch.Success)
                                            {
                                                try
                                                {
                                                    channel.BitRate = int.Parse(bitRateMatch.Groups[1].Value);
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
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                alChannels.Add(channel);
                                OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++analyzedCount, -1));
                                break;
                            }
                        }
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
