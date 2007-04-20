#region �f�B���N�e�B�u���g�p����

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// �`�����l���̏ڍ׏��\���t�H�[��
    /// </summary>
    public class ChannelPropertyForm : System.Windows.Forms.Form
    {
        private Button accessButton;
        private MenuItem okMenuItem;
        private Button playButton;
        private MainMenu mainMenu;
        private ListView propertyListView;
        private ColumnHeader infomationColumnHeader;
        private ColumnHeader discriptionColumnHeader;

        /// <summary>
        /// �`�����l��
        /// </summary>
        private Channel channel;

        /// <summary>
        /// �A���J�[�R���g���[���̃��X�g
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

        public ChannelPropertyForm(Channel channel)
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            this.channel = channel;
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
            this.accessButton = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.playButton = new System.Windows.Forms.Button();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.infomationColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.discriptionColumnHeader = new System.Windows.Forms.ColumnHeader();
            // 
            // accessButton
            // 
            this.accessButton.Location = new System.Drawing.Point(165, 245);
            this.accessButton.Size = new System.Drawing.Size(72, 20);
            this.accessButton.Text = "&Access";
            this.accessButton.Click += new System.EventHandler(this.AccessButton_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.okMenuItem);
            // 
            // okMenuItem
            // 
            this.okMenuItem.Text = "&OK";
            this.okMenuItem.Click += new System.EventHandler(this.OkMenuItem_Click);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(87, 245);
            this.playButton.Size = new System.Drawing.Size(72, 20);
            this.playButton.Text = "&Play";
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // propertyListView
            // 
            this.propertyListView.Columns.Add(this.infomationColumnHeader);
            this.propertyListView.Columns.Add(this.discriptionColumnHeader);
            this.propertyListView.Location = new System.Drawing.Point(3, 3);
            this.propertyListView.Size = new System.Drawing.Size(234, 236);
            this.propertyListView.View = System.Windows.Forms.View.Details;
            // 
            // infomationColumnHeader
            // 
            this.infomationColumnHeader.Text = "���";
            this.infomationColumnHeader.Width = 60;
            // 
            // discriptionColumnHeader
            // 
            this.discriptionColumnHeader.Text = "�ڍ�";
            this.discriptionColumnHeader.Width = 150;
            // 
            // ChannelPropertyForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.propertyListView);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.accessButton);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "�ԑg�̏ڍ�";
            this.Resize += new System.EventHandler(this.ChannelPropertyForm_Resize);
            this.Load += new System.EventHandler(this.ChannelPropertyForm_Load);

        }
        #endregion

        /// <summary>
        /// �R���g���[���ɃA���J�[���Z�b�g����
        /// </summary>
        private void SetAnchorControl()
        {
            anchorControlList.Add(new AnchorLayout(propertyListView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(playButton, AnchorStyles.Right | AnchorStyles.Bottom, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(accessButton, AnchorStyles.Right | AnchorStyles.Bottom, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����
        /// </summary>
        private void FixWindowSize()
        {
            foreach (AnchorLayout anchorLayout in anchorControlList)
            {
                anchorLayout.LayoutControl();
            }
        }

        /// <summary>
        /// ListView�̃R�����T�C�Y�������Œ�������
        /// </summary>
        /// <param name="listView">ListView</param>
        private void AutoResizeColumnListView(ListView listView)
        {
            foreach (ColumnHeader ch in listView.Columns)
            {
                ch.Width = -2;
            }
        }

        private void ChannelPropertyForm_Load(object sender, System.EventArgs e)
        {
            SetAnchorControl();
            FixWindowSize();

            string[] namPorperty = { "�ԑg��", channel.Nam.Trim() };
            string[] gnlPorperty = { "�W������", channel.Gnl.Trim() };
            string[] urlPorperty = { "URL", ((channel.GetWebsiteUrl() != null) ? channel.GetWebsiteUrl().ToString().Trim() : "") };
            string[] timsPorperty = { "�����J�n����", channel.Tims.ToString().Trim() };

            // ���X�i���\���i�[������
            string cln = string.Empty;
            if (channel.Cln >= 0)
            {
                cln = channel.Cln.ToString();
            }
            if (channel.Clns >= 0)
            {
                cln += " / " + channel.Clns.ToString();
            }
            string[] clnPorperty = { "���X�i���i����/�q�ׁj", cln };

            string bit = string.Empty;
            if (channel.Bit > 0)
            {
                bit = channel.Bit.ToString() + " Kbps";
            }
            string[] bitPorperty = { "�r�b�g���[�g", bit };

            propertyListView.Items.Add(new ListViewItem(namPorperty));
            propertyListView.Items.Add(new ListViewItem(gnlPorperty));
            propertyListView.Items.Add(new ListViewItem(urlPorperty));
            propertyListView.Items.Add(new ListViewItem(timsPorperty));
            propertyListView.Items.Add(new ListViewItem(clnPorperty));
            propertyListView.Items.Add(new ListViewItem(bitPorperty));

            AutoResizeColumnListView(propertyListView);
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                PocketLadioUtility.PlayStreaming(channel.GetPlayUrl());
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
                if (channel.GetWebsiteUrl() != null)
                {
                    PocketLadioUtility.AccessWebsite(channel.GetWebsiteUrl());
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
