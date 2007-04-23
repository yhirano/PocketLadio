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
        /// �\�[�g�̎��
        /// </summary>
        public enum SortKind
        {
            None, Title, Listener, ListenerTotal, BitRate
        }

        /// <summary>
        /// �\�[�g�̏����E�~��
        /// </summary>
        public enum SortScending
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
            this.id = id;
            this.parentStation = parentStation;
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
            ArrayList alChannels = new ArrayList();

            #region �P��t�B���^����

            // �P��t�B���^�����݂���ꍇ
            if (setting.GetFilterWords().Length > 0)
            {
                foreach (IChannel channel in GetChannels())
                {
                    foreach (string filter in setting.GetFilterWords())
                    {
                        if (channel.GetFilteredWord().IndexOf(filter) != -1)
                        {
                            alChannels.Add(channel);
                            break;
                        }
                    }
                }
            }
            // �P��t�B���^�����݂��Ȃ��ꍇ
            else
            {
                alChannels.AddRange(GetChannels());
            }

            #endregion

            #region �Œ�r�b�g���[�g�t�B���^����

            ArrayList alDeleteChannels = new ArrayList();

            // �Œ�r�b�g���[�g�t�B���^�����݂���ꍇ
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


            #region �ő�r�b�g���[�g�t�B���^����

            alDeleteChannels.Clear();

            // �ő�r�b�g���[�g�t�B���^�����݂���ꍇ
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

            if (setting.SortKind == SortKind.None)
            {
                ;
            }
            else if (setting.SortKind == SortKind.Title)
            {
                alChannels.Sort((IComparer)new ChannelTitleComparer());

            }
            else if (setting.SortKind == SortKind.Listener)
            {
                alChannels.Sort((IComparer)new ChannelListenerComparer());
            }
            else if (setting.SortKind == SortKind.ListenerTotal)
            {
                alChannels.Sort((IComparer)new ChannelListenerTotalComparer());
            }
            else if (setting.SortKind == SortKind.BitRate)
            {
                alChannels.Sort((IComparer)new ChannelBitRateComparer());
            }
            else
            {
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }

            // �~���̏ꍇ
            if (setting.SortKind != SortKind.None && setting.SortScending == SortScending.Descending)
            {
                alChannels.Reverse();
            }

            #endregion

            return (IChannel[])alChannels.ToArray(typeof(IChannel));
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����
        /// </summary>
        public virtual void FetchHeadline()
        {
            // �������Z�b�g����
            lastCheckTime = DateTime.Now;

            WebStream st = null;
            StreamReader sr = null;

            try
            {
                // �`�����l���̃��X�g
                ArrayList alChannels = new ArrayList();
                Channel channel = null;

                string searchWord = ((setting.SearchWord.Length != 0) ? "&s=" + setting.SearchWord : "");
                // ���p�X�y�[�X�ƑS�p�X�y�[�X��+�ɒu�������� SHOUTcast���URL��AND�����̃X�y�[�X��+�ɒu���������邽��
                searchWord = searchWord.Replace(' ', '+').Replace("�@", "+");

                string perView = ((setting.PerView.ToString().Length != 0) ? "&numresult=" + setting.PerView : "");
                Uri url = new Uri(PocketLadioInfo.ShoutcastUrl + "/?" + searchWord + perView);

                st = PocketLadioUtility.GetWebStream(url);

                sr = new StreamReader(st, Encoding.GetEncoding("Windows-1252"));
                string httpString = sr.ReadToEnd();

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
                catch (IOException)
                {
                    throw;
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
            SettingForm settingForm = new SettingForm(setting);
            settingForm.ShowDialog();
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
