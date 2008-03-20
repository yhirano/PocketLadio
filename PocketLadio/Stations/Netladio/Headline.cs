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

#endregion

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̃w�b�h���C��
    /// </summary>
    public class Headline : PocketLadio.Stations.IHeadline
    {
        /// <summary>
        /// �w�b�h���C���̎��
        /// </summary>
        private const string KIND_NAME = "�˂Ƃ炶";

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL CSV
        /// </summary>
        public const string NETLADIO_HEADLINE_CVS_URL = "http://yp.ladio.livedoor.jp/stats/list.csv";

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL XML
        /// </summary>
        public const string NETLADIO_HEADLINE_XML_URL = "http://yp.ladio.livedoor.jp/stats/list.xml";

        /// <summary>
        /// �˂Ƃ炶�̃w�b�h���C����URL DAT v2(gzip)
        /// </summary>
        public const string NETLADIO_HEADLINE_DAT_V2_URL = "http://yp.ladio.livedoor.jp/stats/list.v2.dat";

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
            set {
                filtedChannelsCache = null;
                _channels = value;
            }
        }

        /// <summary>
        /// �t�B���^�ςݔԑg�̃L���b�V��
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
            None, Nam, Tims, Cln, Clns, Bit
        }

        /// <summary>
        /// �\�[�g�̏����E�~��
        /// </summary>
        public enum SortScendings
        {
            Ascending, Descending
        }

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
        /// �N�����̏��������\�b�h�B�������Ȃ��B
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
                        if (0 < channel.Bit && channel.Bit < setting.FilterAboveBitRate)
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
                        if (channel.Bit > setting.FilterBelowBitRate)
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
                else if (setting.SortKind == SortKinds.Nam)
                {
                    alChannels.Sort((IComparer)new ChannelNamComparer());

                }
                else if (setting.SortKind == SortKinds.Tims)
                {
                    alChannels.Sort((IComparer)new ChannelTimsComparer());
                }
                else if (setting.SortKind == SortKinds.Cln)
                {
                    alChannels.Sort((IComparer)new ChannelClnComparer());
                }
                else if (setting.SortKind == SortKinds.Clns)
                {
                    alChannels.Sort((IComparer)new ChannelClnsComparer());
                }
                else if (setting.SortKind == SortKinds.Bit)
                {
                    alChannels.Sort((IComparer)new ChannelBitComparer());
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
        /// �w�b�h���C�����l�b�g����擾����
        /// </summary>
        public virtual void FetchHeadline()
        {
            // �������Z�b�g����
            lastCheckTime = DateTime.Now;

            switch (setting.HeadlineGetWay)
            {
                case UserSetting.HeadlineGetType.Cvs:
                    FetchHeadlineCvs();
                    break;
                case UserSetting.HeadlineGetType.Xml:
                    FetchHeadlineXml();
                    break;
                case UserSetting.HeadlineGetType.DatV2:
                    FetchHeadlineDatV2();
                    break;
                default:
                    // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                    Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
                    break;
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
        /// �w�b�h���C�����l�b�g����擾����iCVS�g�p�j
        /// </summary>
        private void FetchHeadlineCvs()
        {
            WebStream st = null;

            string[] channelsCvs;
            // �`�����l���̃��X�g
            ArrayList alChannels = new ArrayList();

            try
            {
                st = PocketLadioUtility.GetWebStream(setting.HeadlineCsvUrl);
                WebTextFetch fetch = new WebTextFetch(st, Encoding.GetEncoding("shift-jis"));
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
                channelsCvs = httpString.Split('\n');
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
            }

            OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, channelsCvs.Length - 1));

            // 1�s�ڂ̓w�b�_�Ȃ̂Ŗ���
            for (int count = 1; count < channelsCvs.Length; ++count)
            {
                if (channelsCvs[count].Length > 0)
                {
                    Channel channel = new Channel(this);
                    string[] channelCsv = channelsCvs[count].Split(',');

                    // CVS��11��ȏ�̏ꍇ�̂ݔԑg�Ƃ݂Ȃ�
                    if (channelCsv.Length >= 11)
                    {
                        // Url�擾
                        try
                        {
                            if (channelCsv[0] != string.Empty)
                            {
                                channel.Url = new Uri(channelCsv[0]);
                            }
                        }
                        catch (UriFormatException)
                        {
                            ;
                        }
                        // PC��ŋN���邱�Ƃ��m�F�������A�Ώ�����ׂ���������Ȃ��̂łƂ肠��������
                        catch (IndexOutOfRangeException)
                        {
                            ;
                        }

                        // Gnl�擾
                        channel.Gnl = channelCsv[1];

                        // Nam�擾
                        channel.Nam = channelCsv[2];

                        // Tit�擾
                        channel.Tit = channelCsv[3];

                        // Mnt�擾
                        channel.Mnt = channelCsv[4];

                        // Tim�擾
                        channel.SetTim(channelCsv[5]);

                        // Tims�擾
                        channel.SetTims(channelCsv[6]);

                        try
                        {
                            // Cln�擾
                            channel.Cln = int.Parse(channelCsv[7]);
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

                        try
                        {
                            // Clns�擾
                            channel.Clns = int.Parse(channelCsv[8]);
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

                        // Srv�擾
                        channel.Srv = channelCsv[9];

                        // Prt�擾
                        channel.Prt = channelCsv[10];

                        if (channelCsv.Length >= 12)
                        {
                            // Typ�擾
                            channel.Typ = channelCsv[11];
                        }

                        if (channelCsv.Length >= 13)
                        {
                            try
                            {
                                // Bit�擾
                                channel.Bit = int.Parse(channelCsv[12]);
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

                        alChannels.Add(channel);
                    }
                }

                OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(count, channelsCvs.Length - 1));
            }

            OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(channelsCvs.Length - 1, channelsCvs.Length - 1));

            channels = (Channel[])alChannels.ToArray(typeof(Channel));
        }

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����iXML�g�p�j
        /// </summary>
        private void FetchHeadlineXml()
        {
            WebStream st = null;
            XmlTextReader reader = null;

            try
            {
                // �ԑg�̃��X�g
                ArrayList alChannels = new ArrayList();

                st = PocketLadioUtility.GetWebStream(setting.HeadlineXmlUrl);

                reader = new XmlTextReader(st);

                // �`�����l��
                Channel channel = new Channel(this);
                // source�^�O�̒��ɂ��邩
                bool inSourceFlag = false;

                // ��͂����w�b�h���C���̌�
                int analyzedCount = 0;

                OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "source")
                        {
                            inSourceFlag = true;
                            channel = new Channel(this);
                        } // End of source
                        // source�^�O�̒��ɂ���ꍇ
                        else if (inSourceFlag == true)
                        {
                            if (reader.LocalName == "url")
                            {
                                try
                                {
                                    string url = reader.ReadString();
                                    if (url != string.Empty)
                                    {
                                        channel.Url = new Uri(url);
                                    }
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of url
                            else if (reader.LocalName == "gnl")
                            {
                                channel.Gnl = reader.ReadString();
                            } // End of gnl
                            else if (reader.LocalName == "nam")
                            {
                                channel.Nam = reader.ReadString();
                            } // End of nam
                            else if (reader.LocalName == "tit")
                            {
                                channel.Tit = reader.ReadString();
                            } // End of tit
                            else if (reader.LocalName == "mnt")
                            {
                                channel.Mnt = reader.ReadString();
                            } // End of mnt
                            else if (reader.LocalName == "tim")
                            {
                                channel.SetTim(reader.ReadString());
                            } // End of tim
                            else if (reader.LocalName == "tims")
                            {
                                channel.SetTims(reader.ReadString());
                            } // End of tims
                            else if (reader.LocalName == "cln")
                            {
                                try
                                {
                                    channel.Cln = int.Parse(reader.ReadString());
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
                            } // End of cln
                            else if (reader.LocalName == "clns")
                            {
                                try
                                {
                                    channel.Clns = int.Parse(reader.ReadString());
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
                            } // End of clns
                            else if (reader.LocalName == "srv")
                            {
                                channel.Srv = reader.ReadString();
                            } // End of srv
                            else if (reader.LocalName == "prt")
                            {
                                channel.Prt = reader.ReadString();
                            } // End of prt
                            else if (reader.LocalName == "typ")
                            {
                                channel.Typ = reader.ReadString();
                            } // End of typ
                            else if (reader.LocalName == "bit")
                            {
                                try
                                {
                                    channel.Bit = int.Parse(reader.ReadString());
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
                            } // End of bit
                        } // End of source�^�O�̒��ɂ���ꍇ
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "source")
                        {
                            inSourceFlag = false;
                            alChannels.Add(channel);
                            OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++analyzedCount, HeadlineAnalyzeEventArgs.UNKNOWN_WHOLE_COUNT));
                        }
                    }
                }

                OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(analyzedCount, analyzedCount));

                channels = (Channel[])alChannels.ToArray(typeof(Channel));
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        #region dat v2��͗p���K�\��

        private static readonly Regex urlRegex = new Regex("^URL=(.*)", RegexOptions.None);

        private static readonly Regex gnlRegex = new Regex("^GNL=(.*)", RegexOptions.None);

        private static readonly Regex namRegex = new Regex("^NAM=(.*)", RegexOptions.None);

        private static readonly Regex mntRegex = new Regex("^MNT=(.*)", RegexOptions.None);

        private static readonly Regex timsRegex = new Regex("^TIMS=(.*)", RegexOptions.None);

        private static readonly Regex clnRegex = new Regex(@"^CLN=(\d+)", RegexOptions.None);

        private static readonly Regex clnsRegex = new Regex(@"^CLNS=(\d+)", RegexOptions.None);

        private static readonly Regex maxRegex = new Regex(@"^MNT=(\d+)", RegexOptions.None);

        private static readonly Regex srvRegex = new Regex("^SRV=(.*)", RegexOptions.None);

        private static readonly Regex prtRegex = new Regex("^PRT=(.*)", RegexOptions.None);

        private static readonly Regex bitRegex = new Regex(@"^BIT=(\d+)", RegexOptions.None);

        private static readonly Regex songRegex = new Regex("^SONG=(.*)", RegexOptions.None);

        #endregion // dat v2��͗p���K�\��

        /// <summary>
        /// �w�b�h���C�����l�b�g����擾����iDAT v2�g�p�j
        /// </summary>
        private void FetchHeadlineDatV2()
        {
            WebStream st = null;

            string[] channelsDat;
            // �`�����l���̃��X�g
            ArrayList alChannels = new ArrayList();

            try
            {
                st = PocketLadioUtility.GetWebStream(setting.HeadlineDatV2Url);
                WebTextFetch fetch = new WebTextFetch(st, Encoding.GetEncoding("shift-jis"));
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
                channelsDat = httpString.Split('\n');
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
            }

            // �ԑg�̑����𐔂���
            int channelLength = 0;
            for (int i = 0; i < channelsDat.Length; ++i)
            {
                // ��s�̏ꍇ
                if (channelsDat[i] == string.Empty)
                {
                    ++channelLength;
                }
            }

            OnHeadlineAnalyze(new HeadlineAnalyzeEventArgs(0, channelLength));

            Channel channel = null;
            // ��͍ς݂̔ԑg��
            int channelAnalyzed = 0;

            for (int count = 0; count < channelsDat.Length; ++count)
            {
                // Url�擾
                Match urlMatch = urlRegex.Match(channelsDat[count]);
                if (urlMatch.Success)
                {
                    try
                    {
                        if (urlMatch.Groups[1].Value != string.Empty)
                        {
                            if (channel == null)
                            {
                                channel = new Channel(this);
                            }
                            channel.Url = new Uri(urlMatch.Groups[1].Value);
                        }
                    }
                    catch (UriFormatException)
                    {
                        ;
                    }

                    continue;
                }

                // Gnl�擾
                Match gnlMatch = gnlRegex.Match(channelsDat[count]);
                if (gnlMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Gnl = gnlMatch.Groups[1].Value;

                    continue;
                }

                Match namMatch = namRegex.Match(channelsDat[count]);
                if (namMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Nam = namMatch.Groups[1].Value;

                    continue;
                }

                Match mntMatch = mntRegex.Match(channelsDat[count]);
                if (mntMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Mnt = mntMatch.Groups[1].Value;

                    continue;
                }

                Match timsMatch = timsRegex.Match(channelsDat[count]);
                if (timsMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.SetTims(timsMatch.Groups[1].Value);

                    continue;
                }

                // Cln�擾
                Match clnMatch = clnRegex.Match(channelsDat[count]);
                if (clnMatch.Success)
                {
                    try
                    {
                        if (channel == null)
                        {
                            channel = new Channel(this);
                        }

                        channel.Cln = int.Parse(clnMatch.Groups[1].Value);
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

                    continue;
                }

                // Clns�擾
                Match clnsMatch = clnsRegex.Match(channelsDat[count]);
                if (clnsMatch.Success)
                {
                    try
                    {
                        if (channel == null)
                        {
                            channel = new Channel(this);
                        }

                        channel.Clns = int.Parse(clnsMatch.Groups[1].Value);
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

                    continue;
                }

                Match maxMatch = maxRegex.Match(channelsDat[count]);
                if (maxMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    continue;
                }

                Match srvMatch = srvRegex.Match(channelsDat[count]);
                if (srvMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Srv = srvMatch.Groups[1].Value;

                    continue;
                }

                Match prtMatch = prtRegex.Match(channelsDat[count]);
                if (prtMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Prt = prtMatch.Groups[1].Value;

                    continue;
                }

                Match bitMatch = bitRegex.Match(channelsDat[count]);
                if (bitMatch.Success)
                {
                    try
                    {
                        if (channel == null)
                        {
                            channel = new Channel(this);
                        }

                        channel.Bit = int.Parse(bitMatch.Groups[1].Value);
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

                    continue;
                }

                Match songMatch = songRegex.Match(channelsDat[count]);
                if (prtMatch.Success)
                {
                    if (channel == null)
                    {
                        channel = new Channel(this);
                    }

                    channel.Tit = songMatch.Groups[1].Value;

                    continue;
                }

                if (channelsDat[count] == string.Empty)
                {
                    if (channel != null)
                    {
                        alChannels.Add(channel);
                        OnHeadlineAnalyzing(new HeadlineAnalyzeEventArgs(++channelAnalyzed, channelLength));
                        channel = null;
                    }
                }
            }

            OnHeadlineAnalyzed(new HeadlineAnalyzeEventArgs(channelLength, channelLength));

            channels = (Channel[])alChannels.ToArray(typeof(Channel));
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
        class ChannelNamComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Nam.CompareTo(((Channel)object2).Nam);
            }
        }

        /// <summary>
        /// �����J�n���Ԃ��r
        /// </summary>
        class ChannelTimsComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                Channel channel1 = (Channel)object1;
                Channel channel2 = (Channel)object2;

                if (channel1.Tims > channel2.Tims)
                {
                    return 1;
                }
                if (channel1.Tims == channel2.Tims)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// �����X�i�����r
        /// </summary>
        class ChannelClnComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Cln - ((Channel)object2).Cln;
            }
        }

        /// <summary>
        /// �q�׃��X�i�����r
        /// </summary>
        class ChannelClnsComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Clns - ((Channel)object2).Clns;
            }
        }

        /// <summary>
        /// �r�b�g���[�g���r
        /// </summary>
        class ChannelBitComparer : IComparer
        {
            public int Compare(object object1, object object2)
            {
                return ((Channel)object1).Bit - ((Channel)object2).Bit;
            }
        }

        #endregion
    }
}
