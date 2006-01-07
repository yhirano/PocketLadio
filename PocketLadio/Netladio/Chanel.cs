using System;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̔ԑg
    /// </summary>
    public class Chanel
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

        public Chanel()
        {
        }

        /// <summary>
        /// �ԑg�̕���URL��Ԃ�
        /// </summary>
        /// <returns>�ԑg�̕���URL</returns>
        public string GetPlayUrl()
        {
            return "http://" + Srv + ":" + Prt + Mnt + ".m3u";
        }

        /// <summary>
        /// �`�����l���̕\�����@�ɏ]���ă`�����l���̏���Ԃ�
        /// </summary>
        /// <returns>�`�����l���̕\�����@�ɏ]�����`�����l���̏��</returns>
        public string GetChanelView()
        {
            string View = (string)UserSetting.HeadlineViewType.Clone();
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
    }
}
