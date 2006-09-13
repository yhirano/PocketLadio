using System;

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̔ԑg
    /// </summary>
    public class Channel : PocketLadio.Stations.IChannel
    {

        /// <summary>
        /// DSP�c�[���Ŏw�肳���URL
        /// </summary>
        private Uri url;

        /// <summary>
        /// DSP�c�[���Ŏw�肳���URL
        /// </summary>
        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        /// DSP�c�[���Ŏw�肳���W��������
        /// </summary>
        private string gnl = "";

        /// <summary>
        /// DSP�c�[���Ŏw�肳���W��������
        /// </summary>
        public string Gnl
        {
            get { return gnl; }
            set { gnl = value; }
        }

        /// <summary>
        /// DSP�c�[���Ŏw�肳���^�C�g����
        /// </summary>
        private string nam = "";

        /// <summary>
        /// DSP�c�[���Ŏw�肳���^�C�g����
        /// </summary>
        public string Nam
        {
            get { return nam; }
            set { nam = value; }
        }

        /// <summary>
        /// DSP�c�[�������M���錻�݂̋Ȗ����
        /// </summary>
        private string tit = "";

        /// <summary>
        /// DSP�c�[�������M���錻�݂̋Ȗ����
        /// </summary>
        public string Tit
        {
            get { return tit; }
            set { tit = value; }
        }

        /// <summary>
        /// �}�E���g�|�C���g
        /// </summary>
        private string mnt = "";

        /// <summary>
        /// �}�E���g�|�C���g
        /// </summary>
        public string Mnt
        {
            get { return mnt; }
            set { mnt = value; }
        }

        /// <summary>
        /// Unix epoch�ł̕����J�n����
        /// </summary>
        private string tim = "";

        /// <summary>
        /// Unix epoch�ł̕����J�n����
        /// </summary>
        public string Tim
        {
            get { return tim; }
            set { tim = value; }
        }

        /// <summary>
        /// yy/mm/dd hh:mm:ss�@�\�L�ł̕����J�n����
        /// </summary>
        private string tims = "";

        /// <summary>
        /// yy/mm/dd hh:mm:ss�@�\�L�ł̕����J�n����
        /// </summary>
        public string Tims
        {
            get { return tims; }
            set { tims = value; }
        }

        /// <summary>
        /// �����X�i��
        /// </summary>
        private string cln = "";

        /// <summary>
        /// �����X�i��
        /// </summary>
        public string Cln
        {
            get { return cln; }
            set { cln = value; }
        }

        /// <summary>
        /// ���׃��X�i��
        /// </summary>
        private string clns = "";

        /// <summary>
        /// ���׃��X�i��
        /// </summary>
        public string Clns
        {
            get { return clns; }
            set { clns = value; }
        }

        /// <summary>
        /// �z�M�T�[�o�z�X�g��
        /// </summary>
        private string srv = "";

        /// <summary>
        /// �z�M�T�[�o�z�X�g��
        /// </summary>
        public string Srv
        {
            get { return srv; }
            set { srv = value; }
        }

        /// <summary>
        /// �z�M�T�[�o�|�[�g�ԍ�
        /// </summary>
        private string prt = "";

        /// <summary>
        /// �z�M�T�[�o�|�[�g�ԍ�
        /// </summary>
        public string Prt
        {
            get { return prt; }
            set { prt = value; }
        }

        /// <summary>
        /// �z�M�T�[�o�̎��
        /// </summary>
        private string typ = "";

        /// <summary>
        /// �z�M�T�[�o�̎��
        /// </summary>
        public string Typ
        {
            get { return typ; }
            set { typ = value; }
        }

        /// <summary>
        /// �r�b�g���[�g
        /// </summary>
        private string bit = "";

        /// <summary>
        /// �r�b�g���[�g
        /// </summary>
        public string Bit
        {
            get { return bit; }
            set { bit = value; }
        }

        /// <summary>
        /// �e�w�b�h���C��
        /// </summary>
        private readonly Headline ParentHeadline;

        /// <summary>
        /// �`�����l���̃R���X�g���N�^
        /// </summary>
        /// <param name="ParentHeadline">�e�w�b�h���C��</param>
        public Channel(Headline parentHeadline)
        {
            this.ParentHeadline = parentHeadline;
        }

        /// <summary>
        /// �ԑg�̕���URL��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̕���URL</returns>
        public virtual Uri GetPlayUrl()
        {
            return new Uri("http://" + srv + ":" + prt + mnt + ".m3u");
        }

        /// <summary>
        /// �ԑg�̃E�F�u�T�C�gURL��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̃E�F�u�T�C�gURL</returns>
        public virtual Uri GetWebsiteUrl()
        {
            return Url;
        }

        /// <summary>
        /// �ԑg�̕\�����@�ɏ]���Ĕԑg�̏���Ԃ�
        /// </summary>
        /// <returns>�ԑg�̕\�����@�ɏ]�����ԑg�̏��</returns>
        public virtual string GetChannelView()
        {
            string view = ParentHeadline.HeadlineViewType;
            if (view.Length != 0)
            {
                view = view.Replace("[[NAME]]", nam);
                view = view.Replace("[[GENRE]]", gnl);
                view = view.Replace("[[CLN]]", cln);
                view = view.Replace("[[CLNS]]", clns);
                view = view.Replace("[[TITLE]]", tit);
                view = view.Replace("[[TIMES]]", tims);
                view = view.Replace("[[BIT]]", bit);
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
            return nam + " " + gnl;
        }
    }
}
