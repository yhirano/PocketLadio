using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using PocketLadio.Utility;

namespace PocketLadio.Stations.RssPodcast
{
    /// <summary>
    /// Podcast�̐ݒ�t�H�[��
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private TabControl tabControl1;
        private TabPage NetladioTabPage;
        private Label RssUrlLabel;
        private TextBox RssUrlTextBox;
        private MenuItem OkMenuItem;
        private MainMenu MainMenu;
        private TextBox HeadlineViewTypeTextBox;
        private Label HeadlineViewTypeLabel;
        private ContextMenu RssUrlContextMenu;
        private MenuItem CutRssUrlMenuItem;
        private MenuItem CopyRssUrlMenuItem;
        private MenuItem PasteRssUrlMenuItem;
        private ContextMenu HeadlineViewTypeContextMenu;
        private MenuItem CutHeadlineViewTypeMenuItem;
        private MenuItem CopyHeadlineViewTypeMenuItem;
        private MenuItem PasteHeadlineViewTypeMenuItem;

        /// <summary>
        /// �ݒ�
        /// </summary>
        private UserSetting setting;


        public SettingForm(UserSetting setting)
        {
            this.setting = setting;

            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();
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
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.OkMenuItem = new System.Windows.Forms.MenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NetladioTabPage = new System.Windows.Forms.TabPage();
            this.HeadlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.HeadlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.HeadlineViewTypeLabel = new System.Windows.Forms.Label();
            this.RssUrlTextBox = new System.Windows.Forms.TextBox();
            this.RssUrlContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.RssUrlLabel = new System.Windows.Forms.Label();
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NetladioTabPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 268);
            // 
            // NetladioTabPage
            // 
            this.NetladioTabPage.Controls.Add(this.HeadlineViewTypeTextBox);
            this.NetladioTabPage.Controls.Add(this.HeadlineViewTypeLabel);
            this.NetladioTabPage.Controls.Add(this.RssUrlTextBox);
            this.NetladioTabPage.Controls.Add(this.RssUrlLabel);
            this.NetladioTabPage.Location = new System.Drawing.Point(0, 0);
            this.NetladioTabPage.Size = new System.Drawing.Size(240, 245);
            this.NetladioTabPage.Text = "Podcast�ݒ�";
            // 
            // HeadlineViewTypeTextBox
            // 
            this.HeadlineViewTypeTextBox.ContextMenu = this.HeadlineViewTypeContextMenu;
            this.HeadlineViewTypeTextBox.Location = new System.Drawing.Point(3, 70);
            this.HeadlineViewTypeTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // HeadlineViewTypeContextMenu
            // 
            this.HeadlineViewTypeContextMenu.MenuItems.Add(this.CutHeadlineViewTypeMenuItem);
            this.HeadlineViewTypeContextMenu.MenuItems.Add(this.CopyHeadlineViewTypeMenuItem);
            this.HeadlineViewTypeContextMenu.MenuItems.Add(this.PasteHeadlineViewTypeMenuItem);
            // 
            // CutHeadlineViewTypeMenuItem
            // 
            this.CutHeadlineViewTypeMenuItem.Text = "�؂���(&T)";
            this.CutHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CutHeadlineViewTypeMenuItem_Click);
            // 
            // CopyHeadlineViewTypeMenuItem
            // 
            this.CopyHeadlineViewTypeMenuItem.Text = "�R�s�[(&C)";
            this.CopyHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CopyHeadlineViewTypeMenuItem_Click);
            // 
            // PasteHeadlineViewTypeMenuItem
            // 
            this.PasteHeadlineViewTypeMenuItem.Text = "�\��t��(&P)";
            this.PasteHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.PasteHeadlineViewTypeMenuItem_Click);
            // 
            // HeadlineViewTypeLabel
            // 
            this.HeadlineViewTypeLabel.Location = new System.Drawing.Point(3, 47);
            this.HeadlineViewTypeLabel.Size = new System.Drawing.Size(130, 20);
            this.HeadlineViewTypeLabel.Text = "�w�b�h���C���̕\�����@";
            // 
            // RssUrlTextBox
            // 
            this.RssUrlTextBox.ContextMenu = this.RssUrlContextMenu;
            this.RssUrlTextBox.Location = new System.Drawing.Point(3, 23);
            this.RssUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // RssUrlContextMenu
            // 
            this.RssUrlContextMenu.MenuItems.Add(this.CutRssUrlMenuItem);
            this.RssUrlContextMenu.MenuItems.Add(this.CopyRssUrlMenuItem);
            this.RssUrlContextMenu.MenuItems.Add(this.PasteRssUrlMenuItem);
            // 
            // CutRssUrlMenuItem
            // 
            this.CutRssUrlMenuItem.Text = "�؂���(&T)";
            this.CutRssUrlMenuItem.Click += new System.EventHandler(this.CutRssUrlMenuItem_Click);
            // 
            // CopyRssUrlMenuItem
            // 
            this.CopyRssUrlMenuItem.Text = "�R�s�[(&C)";
            this.CopyRssUrlMenuItem.Click += new System.EventHandler(this.CopyRssUrlMenuItem_Click);
            // 
            // PasteRssUrlMenuItem
            // 
            this.PasteRssUrlMenuItem.Text = "�\��t��(&P)";
            this.PasteRssUrlMenuItem.Click += new System.EventHandler(this.PasteRssUrlMenuItem_Click);
            // 
            // RssUrlLabel
            // 
            this.RssUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.RssUrlLabel.Size = new System.Drawing.Size(109, 16);
            this.RssUrlLabel.Text = "Podcast��RSS URL";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "Podcast�ݒ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // �ݒ�̓ǂݍ���
            RssUrlTextBox.Text = ((setting.RssUrl != null) ? setting.RssUrl.ToString() : "");
            HeadlineViewTypeTextBox.Text = setting.HeadlineViewType;
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �ݒ�̏�������
            try
            {
                setting.RssUrl = new Uri(RssUrlTextBox.Text.Trim());
            }
            catch (UriFormatException)
            {
                ;
            }
            setting.HeadlineViewType = HeadlineViewTypeTextBox.Text.Trim();

            try
            {
                setting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("�ݒ�t�@�C�����������߂܂���ł���", "�ݒ�t�@�C���������݃G���[");
            }
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void CutRssUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(RssUrlTextBox);
        }

        private void CopyRssUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(RssUrlTextBox);
        }

        private void PasteRssUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(RssUrlTextBox);
        }

        private void CutHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(HeadlineViewTypeTextBox);
        }

        private void CopyHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(HeadlineViewTypeTextBox);
        }

        private void PasteHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(HeadlineViewTypeTextBox);
        }
    }
}
