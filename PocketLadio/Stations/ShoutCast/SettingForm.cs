#region �f�B���N�e�B�u���g�p����

using System;
using System.Drawing;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// �˂Ƃ炶�̐ݒ�t�H�[��
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private TabControl shoutCastSettingTabControl;
        private TabPage shoutCastTabPage;
        private MenuItem okMenuItem;
        private MainMenu mainMenu;
        private ContextMenu headlineViewTypeContextMenu;
        private MenuItem cutHeadlineViewTypeMenuItem;
        private MenuItem copyHeadlineViewTypeMenuItem;
        private MenuItem pasteHeadlineViewTypeMenuItem;
        private ContextMenu searchWordContextMenu;
        private MenuItem cutSearchWordMenuItem;
        private MenuItem copySearchWordMenuItem;
        private MenuItem pastSearchWordMenuItem;
        private TextBox headlineViewTypeTextBox;
        private Label headlineViewTypeLabel;
        private TextBox searchWordTextBox;
        private Label maxBitRateLabel;
        private Label searchWordLabel;
        private ComboBox maxBitRateComboBox;
        private ComboBox perViewComboBox;
        private Label perViewLabel;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.shoutCastSettingTabControl = new System.Windows.Forms.TabControl();
            this.shoutCastTabPage = new System.Windows.Forms.TabPage();
            this.perViewComboBox = new System.Windows.Forms.ComboBox();
            this.perViewLabel = new System.Windows.Forms.Label();
            this.maxBitRateComboBox = new System.Windows.Forms.ComboBox();
            this.headlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.headlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.copyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineViewTypeLabel = new System.Windows.Forms.Label();
            this.searchWordTextBox = new System.Windows.Forms.TextBox();
            this.searchWordContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutSearchWordMenuItem = new System.Windows.Forms.MenuItem();
            this.copySearchWordMenuItem = new System.Windows.Forms.MenuItem();
            this.pastSearchWordMenuItem = new System.Windows.Forms.MenuItem();
            this.maxBitRateLabel = new System.Windows.Forms.Label();
            this.searchWordLabel = new System.Windows.Forms.Label();
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
            // shoutCastSettingTabControl
            // 
            this.shoutCastSettingTabControl.Controls.Add(this.shoutCastTabPage);
            this.shoutCastSettingTabControl.Location = new System.Drawing.Point(0, 0);
            this.shoutCastSettingTabControl.SelectedIndex = 0;
            this.shoutCastSettingTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // shoutCastTabPage
            // 
            this.shoutCastTabPage.Controls.Add(this.perViewComboBox);
            this.shoutCastTabPage.Controls.Add(this.perViewLabel);
            this.shoutCastTabPage.Controls.Add(this.maxBitRateComboBox);
            this.shoutCastTabPage.Controls.Add(this.headlineViewTypeTextBox);
            this.shoutCastTabPage.Controls.Add(this.headlineViewTypeLabel);
            this.shoutCastTabPage.Controls.Add(this.searchWordTextBox);
            this.shoutCastTabPage.Controls.Add(this.maxBitRateLabel);
            this.shoutCastTabPage.Controls.Add(this.searchWordLabel);
            this.shoutCastTabPage.Location = new System.Drawing.Point(0, 0);
            this.shoutCastTabPage.Size = new System.Drawing.Size(240, 245);
            this.shoutCastTabPage.Text = "SHOUTcast�ݒ�";
            // 
            // perViewComboBox
            // 
            this.perViewComboBox.Location = new System.Drawing.Point(3, 110);
            this.perViewComboBox.Size = new System.Drawing.Size(122, 22);
            // 
            // perViewLabel
            // 
            this.perViewLabel.Location = new System.Drawing.Point(3, 91);
            this.perViewLabel.Size = new System.Drawing.Size(86, 16);
            this.perViewLabel.Text = "Per View";
            // 
            // maxBitRateComboBox
            // 
            this.maxBitRateComboBox.Location = new System.Drawing.Point(3, 66);
            this.maxBitRateComboBox.Size = new System.Drawing.Size(122, 22);
            // 
            // headlineViewTypeTextBox
            // 
            this.headlineViewTypeTextBox.ContextMenu = this.headlineViewTypeContextMenu;
            this.headlineViewTypeTextBox.Location = new System.Drawing.Point(3, 158);
            this.headlineViewTypeTextBox.Size = new System.Drawing.Size(234, 21);
            this.headlineViewTypeTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HeadlineViewTypeTextBox_KeyUp);
            this.headlineViewTypeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineViewTypeTextBox_KeyDown);
            // 
            // headlineViewTypeContextMenu
            // 
            this.headlineViewTypeContextMenu.MenuItems.Add(this.cutHeadlineViewTypeMenuItem);
            this.headlineViewTypeContextMenu.MenuItems.Add(this.copyHeadlineViewTypeMenuItem);
            this.headlineViewTypeContextMenu.MenuItems.Add(this.pasteHeadlineViewTypeMenuItem);
            // 
            // cutHeadlineViewTypeMenuItem
            // 
            this.cutHeadlineViewTypeMenuItem.Text = "�؂���(&T)";
            this.cutHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CutHeadlineViewTypeMenuItem_Click);
            // 
            // copyHeadlineViewTypeMenuItem
            // 
            this.copyHeadlineViewTypeMenuItem.Text = "�R�s�[(&C)";
            this.copyHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CopyHeadlineViewTypeMenuItem_Click);
            // 
            // pasteHeadlineViewTypeMenuItem
            // 
            this.pasteHeadlineViewTypeMenuItem.Text = "�\��t��(&P)";
            this.pasteHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.PasteHeadlineViewTypeMenuItem_Click);
            // 
            // headlineViewTypeLabel
            // 
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 135);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(135, 20);
            this.headlineViewTypeLabel.Text = "�w�b�h���C���̕\�����@";
            // 
            // searchWordTextBox
            // 
            this.searchWordTextBox.ContextMenu = this.searchWordContextMenu;
            this.searchWordTextBox.Location = new System.Drawing.Point(3, 23);
            this.searchWordTextBox.Size = new System.Drawing.Size(234, 21);
            this.searchWordTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchWordTextBox_KeyUp);
            this.searchWordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchWordTextBox_KeyDown);
            // 
            // searchWordContextMenu
            // 
            this.searchWordContextMenu.MenuItems.Add(this.cutSearchWordMenuItem);
            this.searchWordContextMenu.MenuItems.Add(this.copySearchWordMenuItem);
            this.searchWordContextMenu.MenuItems.Add(this.pastSearchWordMenuItem);
            // 
            // cutSearchWordMenuItem
            // 
            this.cutSearchWordMenuItem.Text = "�؂���(&T)";
            this.cutSearchWordMenuItem.Click += new System.EventHandler(this.CutSearchWordMenuItem_Click);
            // 
            // copySearchWordMenuItem
            // 
            this.copySearchWordMenuItem.Text = "�R�s�[(&C)";
            this.copySearchWordMenuItem.Click += new System.EventHandler(this.CopySearchWordMenuItem_Click);
            // 
            // pastSearchWordMenuItem
            // 
            this.pastSearchWordMenuItem.Text = "�\��t��(&P)";
            this.pastSearchWordMenuItem.Click += new System.EventHandler(this.PasteSearchWordMenuItem_Click);
            // 
            // maxBitRateLabel
            // 
            this.maxBitRateLabel.Location = new System.Drawing.Point(3, 47);
            this.maxBitRateLabel.Size = new System.Drawing.Size(86, 16);
            this.maxBitRateLabel.Text = "Max bit rate";
            // 
            // searchWordLabel
            // 
            this.searchWordLabel.Location = new System.Drawing.Point(3, 4);
            this.searchWordLabel.Size = new System.Drawing.Size(86, 16);
            this.searchWordLabel.Text = "Search word";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.shoutCastSettingTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "SHOUTcast�ݒ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            #region �R���{�{�b�N�X�̏�����

            // Max bit rate�R���{�{�b�N�X�̏�����
            foreach (string bitrateKey in UserSetting.MaxBitRateTable.Keys)
            {
                maxBitRateComboBox.Items.Add(bitrateKey);
            }

            // Per View�R���{�{�b�N�X�̏�����
            foreach (string perViewKey in UserSetting.PerViewArray)
            {
                perViewComboBox.Items.Add(perViewKey);
            }

            #endregion

            #region �ݒ�̓ǂݍ���

            // �����Ώۂ̓ǂݍ���
            searchWordTextBox.Text = setting.SearchWord.Trim();

            // maxBitRateComboBox�̈ʒu���킹
            maxBitRateComboBox.SelectedIndex = 0;
            if (UserSetting.MaxBitRateTable.ContainsKey(setting.MaxBitRateKey) == true)
            {
                for (int count = 0; count < maxBitRateComboBox.Items.Count; ++count)
                {
                    maxBitRateComboBox.SelectedIndex = count;
                    if (maxBitRateComboBox.SelectedItem.ToString() == setting.MaxBitRateKey)
                    {
                        break;
                    }
                }
            }

            // perViewComboBox�̈ʒu���킹
            perViewComboBox.SelectedIndex = 0;
            for (int count = 0; count < perViewComboBox.Items.Count; ++count)
            {
                perViewComboBox.SelectedIndex = count;
                if (perViewComboBox.SelectedItem.ToString() == setting.PerView)
                {
                    break;
                }
            }

            //�w�b�h���C���\�����@�̓ǂݍ���
            headlineViewTypeTextBox.Text = setting.HeadlineViewType;

            #endregion
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �ݒ�̏�������
            setting.SearchWord = searchWordTextBox.Text.Trim();
            setting.MaxBitRateKey = maxBitRateComboBox.SelectedItem.ToString().Trim();
            setting.PerView = perViewComboBox.SelectedItem.ToString().Trim();
            setting.HeadlineViewType = headlineViewTypeTextBox.Text.Trim();

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

        private void CutHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(headlineViewTypeTextBox);
        }

        private void CopyHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(headlineViewTypeTextBox);
        }

        private void PasteHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(headlineViewTypeTextBox);
        }

        private void CutSearchWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(searchWordTextBox);
        }

        private void CopySearchWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(searchWordTextBox);
        }

        private void PasteSearchWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(searchWordTextBox);
        }

        private void SearchWordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(searchWordTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(searchWordTextBox);
            }
        }

        private void SearchWordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(searchWordTextBox);
            }
        }

        private void HeadlineViewTypeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(headlineViewTypeTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(headlineViewTypeTextBox);
            }
        }

        private void HeadlineViewTypeTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(headlineViewTypeTextBox);
            }
        }
    }
}
