#region �f�B���N�e�B�u���g�p����

using System;
using System.Drawing;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using PocketLadio.Utility;

#endregion

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̐ݒ�t�H�[��
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private TabControl netladioTabControl;
        private TabPage netladioTabPage;
        private Label headlineCvsUrlLabel;
        private Label headlineXmlUrlLabel;
        private TextBox headlineCsvUrlTextBox;
        private TextBox headlineXmlUrlTextBox;
        private Panel headlineGetWayPanel;
        private RadioButton headlineGetWayCvsRadioButton;
        private RadioButton headlineGetWayXmlRadioButton;
        private Label headlineViewTypeLabel;
        private MenuItem okMenuItem;
        private MainMenu mainMenu;
        private Label headlineGetWayLabel;
        private TextBox headlineViewTypeTextBox;
        private ContextMenu headlineViewTypeContextMenu;
        private MenuItem cutHeadlineViewTypeMenuItem;
        private MenuItem copyHeadlineViewTypeMenuItem;
        private MenuItem pasteHeadlineViewTypeMenuItem;
        private ContextMenu headlineCvsUrlContextMenu;
        private MenuItem cutHeadlineCvsUrlMenuItem;
        private MenuItem copyHeadlineCvsUrlMenuItem;
        private MenuItem pasteHeadlineCvsUrlMenuItem;

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
            this.netladioTabControl = new System.Windows.Forms.TabControl();
            this.netladioTabPage = new System.Windows.Forms.TabPage();
            this.headlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.headlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.copyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineViewTypeLabel = new System.Windows.Forms.Label();
            this.headlineGetWayPanel = new System.Windows.Forms.Panel();
            this.headlineGetWayLabel = new System.Windows.Forms.Label();
            this.headlineGetWayXmlRadioButton = new System.Windows.Forms.RadioButton();
            this.headlineGetWayCvsRadioButton = new System.Windows.Forms.RadioButton();
            this.headlineXmlUrlTextBox = new System.Windows.Forms.TextBox();
            this.headlineCsvUrlTextBox = new System.Windows.Forms.TextBox();
            this.headlineCvsUrlContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.copyHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineXmlUrlLabel = new System.Windows.Forms.Label();
            this.headlineCvsUrlLabel = new System.Windows.Forms.Label();
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
            // netladioTabControl
            // 
            this.netladioTabControl.Controls.Add(this.netladioTabPage);
            this.netladioTabControl.Location = new System.Drawing.Point(0, 0);
            this.netladioTabControl.SelectedIndex = 0;
            this.netladioTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // netladioTabPage
            // 
            this.netladioTabPage.Controls.Add(this.headlineViewTypeTextBox);
            this.netladioTabPage.Controls.Add(this.headlineViewTypeLabel);
            this.netladioTabPage.Controls.Add(this.headlineGetWayPanel);
            this.netladioTabPage.Controls.Add(this.headlineXmlUrlTextBox);
            this.netladioTabPage.Controls.Add(this.headlineCsvUrlTextBox);
            this.netladioTabPage.Controls.Add(this.headlineXmlUrlLabel);
            this.netladioTabPage.Controls.Add(this.headlineCvsUrlLabel);
            this.netladioTabPage.Location = new System.Drawing.Point(0, 0);
            this.netladioTabPage.Size = new System.Drawing.Size(240, 245);
            this.netladioTabPage.Text = "�˂Ƃ炶�ݒ�";
            // 
            // headlineViewTypeTextBox
            // 
            this.headlineViewTypeTextBox.ContextMenu = this.headlineViewTypeContextMenu;
            this.headlineViewTypeTextBox.Location = new System.Drawing.Point(3, 162);
            this.headlineViewTypeTextBox.Size = new System.Drawing.Size(234, 21);
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
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 139);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(135, 20);
            this.headlineViewTypeLabel.Text = "�w�b�h���C���̕\�����@";
            // 
            // headlineGetWayPanel
            // 
            this.headlineGetWayPanel.Controls.Add(this.headlineGetWayLabel);
            this.headlineGetWayPanel.Controls.Add(this.headlineGetWayXmlRadioButton);
            this.headlineGetWayPanel.Controls.Add(this.headlineGetWayCvsRadioButton);
            this.headlineGetWayPanel.Location = new System.Drawing.Point(0, 91);
            this.headlineGetWayPanel.Size = new System.Drawing.Size(240, 45);
            // 
            // headlineGetWayLabel
            // 
            this.headlineGetWayLabel.Location = new System.Drawing.Point(3, 0);
            this.headlineGetWayLabel.Size = new System.Drawing.Size(135, 20);
            this.headlineGetWayLabel.Text = "�w�b�h���C���̎擾���@";
            // 
            // headlineGetWayXmlRadioButton
            // 
            this.headlineGetWayXmlRadioButton.Enabled = false;
            this.headlineGetWayXmlRadioButton.Location = new System.Drawing.Point(57, 23);
            this.headlineGetWayXmlRadioButton.Size = new System.Drawing.Size(48, 20);
            this.headlineGetWayXmlRadioButton.Text = "XML";
            // 
            // headlineGetWayCvsRadioButton
            // 
            this.headlineGetWayCvsRadioButton.Checked = true;
            this.headlineGetWayCvsRadioButton.Location = new System.Drawing.Point(3, 22);
            this.headlineGetWayCvsRadioButton.Size = new System.Drawing.Size(48, 20);
            this.headlineGetWayCvsRadioButton.Text = "CVS";
            // 
            // headlineXmlUrlTextBox
            // 
            this.headlineXmlUrlTextBox.Enabled = false;
            this.headlineXmlUrlTextBox.Location = new System.Drawing.Point(3, 64);
            this.headlineXmlUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // headlineCsvUrlTextBox
            // 
            this.headlineCsvUrlTextBox.ContextMenu = this.headlineCvsUrlContextMenu;
            this.headlineCsvUrlTextBox.Location = new System.Drawing.Point(3, 23);
            this.headlineCsvUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // headlineCvsUrlContextMenu
            // 
            this.headlineCvsUrlContextMenu.MenuItems.Add(this.cutHeadlineCvsUrlMenuItem);
            this.headlineCvsUrlContextMenu.MenuItems.Add(this.copyHeadlineCvsUrlMenuItem);
            this.headlineCvsUrlContextMenu.MenuItems.Add(this.pasteHeadlineCvsUrlMenuItem);
            // 
            // cutHeadlineCvsUrlMenuItem
            // 
            this.cutHeadlineCvsUrlMenuItem.Text = "�؂���(&T)";
            this.cutHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.CutHeadlineCvsUrlMenuItem_Click);
            // 
            // copyHeadlineCvsUrlMenuItem
            // 
            this.copyHeadlineCvsUrlMenuItem.Text = "�R�s�[(&C)";
            this.copyHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.CopyHeadlineCvsUrlMenuItem_Click);
            // 
            // pasteHeadlineCvsUrlMenuItem
            // 
            this.pasteHeadlineCvsUrlMenuItem.Text = "�\��t��(&P)";
            this.pasteHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.PasteHeadlineCvsUrlMenuItem_Click);
            // 
            // headlineXmlUrlLabel
            // 
            this.headlineXmlUrlLabel.Location = new System.Drawing.Point(3, 47);
            this.headlineXmlUrlLabel.Size = new System.Drawing.Size(124, 16);
            this.headlineXmlUrlLabel.Text = "�w�b�h���C����URL XML";
            // 
            // headlineCvsUrlLabel
            // 
            this.headlineCvsUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.headlineCvsUrlLabel.Size = new System.Drawing.Size(124, 16);
            this.headlineCvsUrlLabel.Text = "�w�b�h���C����URL CSV";
            // 
            // settingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.netladioTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "�˂Ƃ炶�ݒ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // �ݒ�̓ǂݍ���
            headlineCsvUrlTextBox.Text = ((setting.HeadlineCsvUrl != null) ? setting.HeadlineCsvUrl.ToString() : "");
            headlineXmlUrlTextBox.Text = ((setting.HeadlineXmlUrl != null) ? setting.HeadlineXmlUrl.ToString() : "");
            if (setting.HeadlineGetWay == UserSetting.HeadlineGetType.Cvs)
            {
                headlineGetWayCvsRadioButton.Checked = true;
                headlineGetWayXmlRadioButton.Checked = false;
            }
            else if (setting.HeadlineGetWay == UserSetting.HeadlineGetType.Xml)
            {
                headlineGetWayCvsRadioButton.Checked = false;
                headlineGetWayXmlRadioButton.Checked = true;
            }
            headlineViewTypeTextBox.Text = setting.HeadlineViewType;
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �ݒ�̏�������
            try
            {
                setting.HeadlineCsvUrl = new Uri(headlineCsvUrlTextBox.Text.Trim());
            }
            catch (UriFormatException)
            {
                ;
            }
            try
            {
                setting.HeadlineXmlUrl = new Uri(headlineXmlUrlTextBox.Text.Trim());
            }
            catch (UriFormatException)
            {
                ;
            }
            if (headlineGetWayCvsRadioButton.Checked)
            {
                setting.HeadlineGetWay = UserSetting.HeadlineGetType.Cvs;
            }
            else if (headlineGetWayXmlRadioButton.Checked)
            {
                setting.HeadlineGetWay = UserSetting.HeadlineGetType.Xml;
            }
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

        private void CutHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(headlineCsvUrlTextBox);
        }

        private void CopyHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(headlineCsvUrlTextBox);
        }

        private void PasteHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(headlineCsvUrlTextBox);
        }
    }
}
