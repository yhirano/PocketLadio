using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using PocketLadio.Util;

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// �`�����l���̏ڍ׏��\���t�H�[��
    /// </summary>
    public class ChannelPropertyForm : System.Windows.Forms.Form
    {
        private Label NamCaptionLabel;
        private Label GnlCaptionLabel;
        private Label UrlCaptionLabel;
        private Label TimsCaptionLabel;
        private Label ClnCaptionLabel;
        private Label BitCaptionLabel;
        private Label NamLabel;
        private Label GnlLabel;
        private Label UrlLabel;
        private Label TimsLabel;
        private Label ClnLabel;
        private Label BitLabel;
        private Button AccessButton;
        private MenuItem OkMenuItem;
        private Button PlayButton;
        private MainMenu MainMenu;

        /// <summary>
        /// �`�����l��
        /// </summary>
        private Channel Channel;

        public ChannelPropertyForm(Channel channel)
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            this.Channel = channel;
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            this.NamCaptionLabel = new System.Windows.Forms.Label();
            this.NamLabel = new System.Windows.Forms.Label();
            this.GnlCaptionLabel = new System.Windows.Forms.Label();
            this.GnlLabel = new System.Windows.Forms.Label();
            this.UrlCaptionLabel = new System.Windows.Forms.Label();
            this.UrlLabel = new System.Windows.Forms.Label();
            this.TimsCaptionLabel = new System.Windows.Forms.Label();
            this.TimsLabel = new System.Windows.Forms.Label();
            this.ClnCaptionLabel = new System.Windows.Forms.Label();
            this.ClnLabel = new System.Windows.Forms.Label();
            this.BitCaptionLabel = new System.Windows.Forms.Label();
            this.BitLabel = new System.Windows.Forms.Label();
            this.AccessButton = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.OkMenuItem = new System.Windows.Forms.MenuItem();
            this.PlayButton = new System.Windows.Forms.Button();
            // 
            // NamCaptionLabel
            // 
            this.NamCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.NamCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.NamCaptionLabel.Text = "�ԑg��";
            // 
            // NamLabel
            // 
            this.NamLabel.Location = new System.Drawing.Point(3, 19);
            this.NamLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // GnlCaptionLabel
            // 
            this.GnlCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.GnlCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.GnlCaptionLabel.Text = "�W������";
            // 
            // GnlLabel
            // 
            this.GnlLabel.Location = new System.Drawing.Point(3, 51);
            this.GnlLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // UrlCaptionLabel
            // 
            this.UrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.UrlCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.UrlCaptionLabel.Text = "URL";
            // 
            // UrlLabel
            // 
            this.UrlLabel.Location = new System.Drawing.Point(3, 109);
            this.UrlLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // TimsCaptionLabel
            // 
            this.TimsCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.TimsCaptionLabel.Size = new System.Drawing.Size(88, 16);
            this.TimsCaptionLabel.Text = "�����J�n����";
            // 
            // TimsLabel
            // 
            this.TimsLabel.Location = new System.Drawing.Point(3, 167);
            this.TimsLabel.Size = new System.Drawing.Size(120, 16);
            // 
            // ClnCaptionLabel
            // 
            this.ClnCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.ClnCaptionLabel.Size = new System.Drawing.Size(120, 16);
            this.ClnCaptionLabel.Text = "���X�i���i����/�q�ׁj";
            // 
            // ClnLabel
            // 
            this.ClnLabel.Location = new System.Drawing.Point(3, 199);
            this.ClnLabel.Size = new System.Drawing.Size(120, 16);
            // 
            // BitCaptionLabel
            // 
            this.BitCaptionLabel.Location = new System.Drawing.Point(129, 183);
            this.BitCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.BitCaptionLabel.Text = "�r�b�g���[�g";
            // 
            // BitLabel
            // 
            this.BitLabel.Location = new System.Drawing.Point(129, 199);
            this.BitLabel.Size = new System.Drawing.Size(108, 16);
            // 
            // AccessButton
            // 
            this.AccessButton.Location = new System.Drawing.Point(165, 128);
            this.AccessButton.Size = new System.Drawing.Size(72, 20);
            this.AccessButton.Text = "&Access";
            this.AccessButton.Click += new System.EventHandler(this.AccessButton_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.Add(this.OkMenuItem);
            // 
            // OkMenuItem
            // 
            this.OkMenuItem.Text = "&OK";
            this.OkMenuItem.Click += new System.EventHandler(this.OkMenuItem_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(165, 70);
            this.PlayButton.Size = new System.Drawing.Size(72, 20);
            this.PlayButton.Text = "&Play";
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // ChannelPropertyForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.AccessButton);
            this.Controls.Add(this.BitLabel);
            this.Controls.Add(this.BitCaptionLabel);
            this.Controls.Add(this.ClnLabel);
            this.Controls.Add(this.ClnCaptionLabel);
            this.Controls.Add(this.TimsLabel);
            this.Controls.Add(this.TimsCaptionLabel);
            this.Controls.Add(this.UrlLabel);
            this.Controls.Add(this.UrlCaptionLabel);
            this.Controls.Add(this.GnlLabel);
            this.Controls.Add(this.GnlCaptionLabel);
            this.Controls.Add(this.NamLabel);
            this.Controls.Add(this.NamCaptionLabel);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "�ԑg�̏ڍ�";
            this.Resize += new System.EventHandler(this.ChannelPropertyForm_Resize);
            this.Load += new System.EventHandler(this.ChannelPropertyForm_Load);

        }
        #endregion

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����
        /// </summary>
        private void FixWindowSize()
        {
            // �������[�h�̏ꍇ
            if (this.Size.Width > this.Size.Height)
            {
                // �����̃E�B���h�E
                FixWindowSizeHorizon();
            }
            else
            {
                // �c���̃E�B���h�E
                FixWindowSizeVertical();
            }
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����i�����j
        /// </summary>
        private void FixWindowSizeVertical()
        {
            this.NamCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.GnlCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.UrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.TimsCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.ClnCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.BitCaptionLabel.Location = new System.Drawing.Point(129, 183);

            this.NamLabel.Location = new System.Drawing.Point(3, 19);
            this.NamLabel.Size = new System.Drawing.Size(234, 16);
            this.GnlLabel.Location = new System.Drawing.Point(3, 51);
            this.GnlLabel.Size = new System.Drawing.Size(234, 16);
            this.UrlLabel.Location = new System.Drawing.Point(3, 109);
            this.UrlLabel.Size = new System.Drawing.Size(234, 16);
            this.TimsLabel.Location = new System.Drawing.Point(3, 167);
            this.TimsLabel.Size = new System.Drawing.Size(120, 16);
            this.ClnLabel.Location = new System.Drawing.Point(3, 199);
            this.ClnLabel.Size = new System.Drawing.Size(120, 16);
            this.BitLabel.Location = new System.Drawing.Point(129, 199);
            this.BitLabel.Size = new System.Drawing.Size(108, 16);

            this.AccessButton.Location = new System.Drawing.Point(165, 128);
            this.PlayButton.Location = new System.Drawing.Point(165, 70);
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����i�����j
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.NamCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.GnlCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.UrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.TimsCaptionLabel.Location = new System.Drawing.Point(3, 132);
            this.ClnCaptionLabel.Location = new System.Drawing.Point(3, 148);
            this.BitCaptionLabel.Location = new System.Drawing.Point(3, 164);

            this.NamLabel.Location = new System.Drawing.Point(3, 19);
            this.NamLabel.Size = new System.Drawing.Size(314, 16);
            this.GnlLabel.Location = new System.Drawing.Point(3, 51);
            this.GnlLabel.Size = new System.Drawing.Size(314, 16);
            this.UrlLabel.Location = new System.Drawing.Point(3, 109);
            this.UrlLabel.Size = new System.Drawing.Size(236, 16);
            this.TimsLabel.Location = new System.Drawing.Point(129, 132);
            this.TimsLabel.Size = new System.Drawing.Size(120, 16);
            this.ClnLabel.Location = new System.Drawing.Point(129, 148);
            this.ClnLabel.Size = new System.Drawing.Size(120, 16);
            this.BitLabel.Location = new System.Drawing.Point(129, 164);
            this.BitLabel.Size = new System.Drawing.Size(108, 16);

            this.AccessButton.Location = new System.Drawing.Point(245, 109);
            this.PlayButton.Location = new System.Drawing.Point(245, 70);
        }

        private void ChannelPropertyForm_Load(object sender, System.EventArgs e)
        {
            FixWindowSize();
            NamLabel.Text = Channel.Nam.Trim();
            GnlLabel.Text = Channel.Gnl.Trim();
            UrlLabel.Text = Channel.Url.Trim();
            TimsLabel.Text = Channel.Tims.Trim();
            ClnLabel.Text = Channel.Cln.Trim() + " / " + Channel.Clns.Trim();
            if (Channel.Bit.Length != 0)
            {
                BitLabel.Text = Channel.Bit.Trim() + " Kbps";
            }
            else
            {
                BitLabel.Text = "Unknown";
            }
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                PocketLadioUtil.PlayStreaming(Channel.GetPlayUrl());
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("���f�B�A�v���C���[��������܂���", "�x��");
            }
        }

        private void AccessButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (UrlLabel.Text.Trim().Length != 0)
                {
                    PocketLadioUtil.AccessWebsite(Channel.GetWebsiteUrl());
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("�u���E�U��������܂���", "�x��");
            }
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ChannelPropertyForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

    }
}
