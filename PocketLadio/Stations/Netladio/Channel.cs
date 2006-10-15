#region �f�B���N�e�B�u���g�p����

using System;

#endregion

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
            set { mnt = value; }
        }

        /// <summary>
        /// Unix epoch�ł̕����J�n����
        /// </summary>
        private int tim;

        /// <summary>
        /// Unix epoch�ł̕����J�n����
        /// </summary>
        public int Tim
        {
            get { return tim; }
        }

        /// <summary>
        /// yy/mm/dd hh:mm:ss�@�\�L�ł̕����J�n����
        /// </summary>
        private DateTime tims = DateTime.Now;

        /// <summary>
        /// yy/mm/dd hh:mm:ss�@�\�L�ł̕����J�n����
        /// </summary>
        public DateTime Tims
        {
            get { return tims; }
        }

        /// <summary>
        /// �����X�i��
        /// </summary>
        private int cln = -1;

        /// <summary>
        /// �����X�i��
        /// </summary>
        public int Cln
        {
            get { return cln; }
            set { cln = value; }
        }

        /// <summary>
        /// ���׃��X�i��
        /// </summary>
        private int clns = -1;

        /// <summary>
        /// ���׃��X�i��
        /// </summary>
        public int Clns
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
        private int bit = -1;

        /// <summary>
        /// �r�b�g���[�g
        /// </summary>
        public int Bit
        {
            get { return bit; }
            set { bit = value; }
        }

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
        /// �ԑg�̔z�M�������Z�b�g����B
        /// �����̏����� "yy'/'MM'/'dd HH':'mm':'ss" �B
        /// </summary>
        /// <param name="date">�ԑg�̔z�M�����̕�����</param>
        public void SetTims(string date)
        {
            try
            {
                tims = DateTime.ParseExact(date, "yy'/'MM'/'dd HH':'mm':'ss",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    System.Globalization.DateTimeStyles.None);
            }
            catch (FormatException)
            {
                tims = DateTime.Now;
            }
        }

        /// <summary>
        /// �ԑg�̔z�M�������Z�b�g����B
        /// Unix epoch�Ŏw�肷��B
        /// </summary>
        /// <param name="date">�ԑg�̔z�M�����iUnix epoch�j</param>
        public void SetTim(string date)
        {
            try
            {
                tim = int.Parse(date);
            }
            catch (ArgumentException)
            {
                tim = 0;
            }
            catch (FormatException)
            {
                tim = 0;
            }
            catch (OverflowException)
            {
                tim = 0;
            }
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
            return url;
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
                view = view.Replace("[[NAME]]", Nam)
                    .Replace("[[GENRE]]", Gnl)
                    .Replace("[[CLN]]", ((Cln >= 0) ? Cln.ToString() : "na"))
                    .Replace("[[CLNS]]", ((Clns >= 0) ? Clns.ToString() : "na"))
                    .Replace("[[TITLE]]", Tit)
                    .Replace("[[TIMES]]", Tims.ToString())
                    .Replace("[[BIT]]", Bit.ToString());
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
            return Nam + " " + Gnl;
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
