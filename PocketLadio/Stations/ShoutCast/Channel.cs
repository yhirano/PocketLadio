#region �f�B���N�e�B�u���g�p����

using System;

#endregion

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// SHOUTcast�̔ԑg
    /// </summary>
    public class Channel : PocketLadio.Stations.IChannel
    {
        /// <summary>
        /// �Đ�URL
        /// </summary>
        private Uri playUrl;

        /// <summary>
        /// �Đ�URL
        /// </summary>
        public Uri PlayUrl
        {
            set { playUrl = value; }
        }

        /// <summary>
        /// �T�C�gURL
        /// </summary>
        private Uri clusterUrl;

        /// <summary>
        /// �T�C�gURL
        /// </summary>
        public Uri ClusterUrl
        {
            set { clusterUrl = value; }
        }

        /// <summary>
        /// �^�C�g��
        /// </summary>
        private string title = string.Empty;

        /// <summary>
        /// �^�C�g��
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// ���݉��t���̋�
        /// </summary>
        private string playing = string.Empty;

        /// <summary>
        /// ���݉��t���̋�
        /// </summary>
        public string Playing
        {
            get { return playing; }
            set { playing = value; }
        }

        /// <summary>
        /// ���X�i��
        /// </summary>
        private int listener = UNKNOWN_LISTENER_NUM;

        /// <summary>
        /// ���X�i��
        /// </summary>
        public int Listener
        {
            get { return listener; }
            set { listener = value; }
        }

        /// <summary>
        /// ���X�i�����s��
        /// </summary>
        public const int UNKNOWN_LISTENER_NUM = -1;

        /// <summary>
        /// �W������
        /// </summary>
        private string genre = string.Empty;

        /// <summary>
        /// �W������
        /// </summary>
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        /// <summary>
        /// �r�b�g���[�g
        /// </summary>
        private int bitRate = UNKNOWN_BITRATE;

        /// <summary>
        /// �r�b�g���[�g
        /// </summary>
        public int BitRate
        {
            get { return bitRate; }
            set { bitRate = value; }
        }

        /// <summary>
        /// �r�b�g���[�g���s��
        /// </summary>
        public const int UNKNOWN_BITRATE = -1;

        /// <summary>
        /// �e�w�b�h���C��
        /// </summary>
        private readonly Headline parentHeadline;

        /// <summary>
        /// �e�w�b�h���C��
        /// </summary>
        public virtual IHeadline ParentHeadline
        {
            get { return parentHeadline; }
        }

        /// <summary>
        /// �`�����l���̃R���X�g���N�^
        /// </summary>
        /// <param name="ParentHeadline">�e�w�b�h���C��</param>
        public Channel(Headline parentHeadline)
        {
            this.parentHeadline = parentHeadline;
        }

        /// <summary>
        /// �ԑg�̕���URL��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̕���URL</returns>
        public virtual Uri GetPlayUrl()
        {
            try
            {
                return playUrl;
            }
            catch (UriFormatException)
            {
                return null;
            }
        }

        /// <summary>
        /// �ԑg�̃E�F�u�T�C�gURL��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̃E�F�u�T�C�gURL</returns>
        public virtual Uri GetWebsiteUrl()
        {
            return clusterUrl;
        }

        /// <summary>
        /// �ԑg�̕\�����@�ɏ]���Ĕԑg�̏���Ԃ�
        /// </summary>
        /// <returns>�ԑg�̕\�����@�ɏ]�����ԑg�̏��</returns>
        public virtual string GetChannelView()
        {
            string view = parentHeadline.HeadlineViewType;
            if (view.Length != 0)
            {
                view = view.Replace("[[TITLE]]", Title)
                    .Replace("[[PLAYING]]", Playing)
                    .Replace("[[LISTENER]]", ((Listener != Channel.UNKNOWN_LISTENER_NUM) ? Listener.ToString() : "na"))
                    .Replace("[[GENRE]]", Genre)
                    .Replace("[[CATEGORY]]", Genre)
                    .Replace("[[BIT]]", ((BitRate != Channel.UNKNOWN_BITRATE) ? BitRate.ToString() : "na"))
                    .Replace("[[RANK]]", string.Empty) // Ver 0.46���[[RANK]]�͔�T�|�[�g
                    .Replace("[[LISTENERTOTAL]]", string.Empty) // Ver 0.46���[[LISTENERTOTAL]]�͔�T�|�[�g
                    ;
            }

            return view;
        }

        /// <summary>
        /// �t�B���^�����O�Ώۂ̃��[�h��Ԃ��B
        /// �Ԃ��ꂽ���[�h�ɏ]���A�t�B���^�����O���s���B
        /// </summary>
        /// <returns>�t�B���^�����O�Ώۂ̃��[�h</returns>
        public virtual string[] GetFilteredWords()
        {
            if (GetPlayUrl() != null)
            {
                return new string[] { Title, Genre, GetPlayUrl().ToString() };
            }
            else
            {
                return new string[] { Title, Genre };
            }
        }

        /// <summary>
        /// �ԑg�̏ڍ׃t�H�[����\������
        /// </summary>
        /// <param name="channel">�ԑg</param>
        /// <returns>�ԑg�̏ڍ׃t�H�[��</returns>
        public virtual void ShowPropertyForm()
        {
            ChannelPropertyForm channelPropertyForm = new ChannelPropertyForm(this);
            channelPropertyForm.ShowDialog();
            channelPropertyForm.Dispose();
        }
    }
}
