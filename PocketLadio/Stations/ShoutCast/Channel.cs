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
        /// �Đ�URL�ւ̃p�X
        /// </summary>
        private string path = "";

        /// <summary>
        /// �Đ�URL�ւ̃p�X
        /// </summary>
        public string Path
        {
            set { path = value; }
        }

        /// <summary>
        /// �����N
        /// </summary>
        private string rank = "";

        /// <summary>
        /// �����N
        /// </summary>
        public string Rank
        {
            get { return rank; }
            set { rank = value; }
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
        private string title = "";

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
        private string playing = "";

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
        private string listener = "";

        /// <summary>
        /// ���X�i��
        /// </summary>
        public string Listener
        {
            get { return listener; }
            set { listener = value; }
        }

        /// <summary>
        /// �J�e�S��
        /// </summary>
        private string category = "";

        /// <summary>
        /// �J�e�S��
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// �r�b�g���[�g
        /// </summary>
        private string bitRate = "";

        /// <summary>
        /// �r�b�g���[�g
        /// </summary>
        public string BitRate
        {
            get { return bitRate; }
            set { bitRate = value; }
        }

        /// <summary>
        /// �e�w�b�h���C��
        /// </summary>
        private readonly Headline parentHeadline;

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
                return new Uri(parentHeadline.ShoutCastUrl.ToString() + path);
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
                view = view.Replace("[[RANK]]", Rank)
                    .Replace("[[TITLE]]", Title)
                    .Replace("[[PLAYING]]", Playing)
                    .Replace("[[LISTENER]]", Listener)
                    .Replace("[[CATEGORY]]", Category)
                    .Replace("[[BIT]]", BitRate);
            }

            return view;
        }

        /// <summary>
        /// �t�B���^�����O�Ώۂ̃��[�h��Ԃ��B
        /// �Ԃ��ꂽ���[�h�ɏ]���A�t�B���^�����O���s���B
        /// </summary>
        /// <returns>�t�B���^�����O�Ώۂ̃��[�h</returns>
        public virtual string GetFilteredWord()
        {
            return Title + " " + Category;
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