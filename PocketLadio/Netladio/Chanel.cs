using System;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̔ԑg
    /// </summary>
    public class Chanel : PocketLadio.StationInterface.IChanel
    {

        /// <summary>
        /// DSP�c�[���Ŏw�肳���URL
        /// </summary>
        public string Url = "";

        /// <summary>
        /// DSP�c�[���Ŏw�肳���W��������
        /// </summary>
        public string Gnl = "";

        /// <summary>
        /// DSP�c�[���Ŏw�肳���^�C�g����
        /// </summary>
        public string Nam = "";

        /// <summary>
        /// DSP�c�[�������M���錻�݂̋Ȗ����
        /// </summary>
        public string Tit = "";

        /// <summary>
        /// �}�E���g�|�C���g
        /// </summary>
        public string Mnt = "";

        /// <summary>
        /// Unix epoch�ł̕����J�n����
        /// </summary>
        public string Tim = "";

        /// <summary>
        /// yy/mm/dd hh:mm:ss�@�\�L�ł̕����J�n����
        /// </summary>
        public string Tims = "";

        /// <summary>
        /// �����X�i��
        /// </summary>
        public string Cln = "";

        /// <summary>
        /// ���׃��X�i��
        /// </summary>
        public string Clns = "";

        /// <summary>
        /// �z�M�T�[�o�z�X�g��
        /// </summary>
        public string Srv = "";

        /// <summary>
        /// �z�M�T�[�o�|�[�g�ԍ�
        /// </summary>
        public string Prt = "";

        /// <summary>
        /// �z�M�T�[�o�̎��
        /// </summary>
        public string Typ = "";

        /// <summary>
        /// �r�b�g���[�g
        /// </summary>
        public string Bit = "";

        /// <summary>
        /// �e�w�b�h���C��
        /// </summary>
        private Headline ParentHeadline;

        /// <summary>
        /// �`�����l���̃R���X�g���N�^
        /// </summary>
        /// <param name="ParentHeadline">�e�w�b�h���C��</param>
        public Chanel(Headline parentHeadline)
        {
            this.ParentHeadline = parentHeadline;
        }

        /// <summary>
        /// �ԑg�̕���URL��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̕���URL</returns>
        public virtual string GetPlayUrl()
        {
            return "http://" + Srv + ":" + Prt + Mnt + ".m3u";
        }

        /// <summary>
        /// �ԑg�̃E�F�u�T�C�gURL��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̃E�F�u�T�C�gURL</returns>
        public virtual string GetWebSiteUrl()
        {
            return Url;
        }

        /// <summary>
        /// �ԑg�̕\�����@�ɏ]���Ĕԑg�̏���Ԃ�
        /// </summary>
        /// <returns>�ԑg�̕\�����@�ɏ]�����ԑg�̏��</returns>
        public virtual string GetChanelView()
        {
            string View = (string)ParentHeadline.GetUserSetting().HeadlineViewType.Clone();
            if (!View.Equals(""))
            {
                View = View.Replace("[[NAME]]", Nam);
                View = View.Replace("[[GENRE]]", Gnl);
                View = View.Replace("[[CLN]]", Cln);
                View = View.Replace("[[CLNS]]", Clns);
                View = View.Replace("[[TITLE]]", Tit);
                View = View.Replace("[[TIMES]]", Tims);
                View = View.Replace("[[BIT]]", Bit);
            }

            return View;
        }

        /// <summary>
        /// �t�B���^�����O�Ώۂ̃��[�h��Ԃ��B
        /// �Ԃ��ꂽ���[�h�ɏ]���A�t�B���^�����O���s���B
        /// </summary>
        /// <returns>�t�B���^�����O�Ώۂ̃��[�h</returns>
        public virtual string GetFilterdWord()
        {
            //return Nam + " " + Gnl + " " + Nam + " " + Tit;
            return Nam + " " + Gnl;
        }
    }
}
