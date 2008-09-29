#region �f�B���N�e�B�u���g�p����

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// PocketLadio�̐ݒ�t�H�[��
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private MainMenu mainMenu;
        private MenuItem okMenuItem;
        private ContextMenu mediaPlayerPathContextMenu;
        private MenuItem cutMediaPlayerPathMenuItem;
        private MenuItem copyMediaPlayerPathMenuItem;
        private MenuItem pasteMediaPlayerPathMenuItem;
        private ContextMenu browserPathContextMenu;
        private MenuItem cutBrowserPathMenuItem;
        private MenuItem copyBrowserPathMenuItem;
        private MenuItem pasteBrowserPathMenuItem;
        private TabPage basicTabPage;
        private TextBox browserPathTextBox;
        private TextBox mediaPlayerPathTextBox;
        private Label browserPathLabel;
        private Label mediaPlayerPathLabel;
        private NumericUpDown headlineTimerSecondNumericUpDown;
        private Label headlineTimerSecondLabel;
        private TabPage networkTabPage;
        private TextBox proxyPortTextBox;
        private TextBox proxyServerTextBox;
        private Label proxyPortLabel;
        private Label proxyServerLabel;
        private ContextMenu proxyServerContextMenu;
        private MenuItem cutProxyServerMenuItem;
        private MenuItem copyProxyServerMenuItem;
        private MenuItem pasteProxyServerMenuItem;
        private ContextMenu proxyPortContextMenu;
        private MenuItem cutProxyPortMenuItem;
        private MenuItem copyProxyPortMenuItem;
        private MenuItem pasteProxyPortMenuItem;
        private Button browserPathReferenceButton;
        private Button mediaPlayerPathReferenceButton;
        private Panel proxySettingPanel;
        private RadioButton proxyUseOriginalSettingRadioButton;
        private RadioButton proxyUseOsSettingRadioButton;
        private RadioButton proxyUnuseRadioButton;
        private CheckBox playlistSaveLocalCheckBox;
        private TabPage viewTabPage;
        private NumericUpDown headlineListBoxFontSizeNumericUpDown;
        private CheckBox headlineListBoxFontSizeCheckBox;
        private TabControl settingTabControl;

        public SettingForm()
        {
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
            this.browserPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.copyBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.mediaPlayerPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutMediaPlayerPathMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMediaPlayerPathMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteMediaPlayerPathMenuItem = new System.Windows.Forms.MenuItem();
            this.basicTabPage = new System.Windows.Forms.TabPage();
            this.playlistSaveLocalCheckBox = new System.Windows.Forms.CheckBox();
            this.browserPathReferenceButton = new System.Windows.Forms.Button();
            this.mediaPlayerPathReferenceButton = new System.Windows.Forms.Button();
            this.browserPathTextBox = new System.Windows.Forms.TextBox();
            this.mediaPlayerPathTextBox = new System.Windows.Forms.TextBox();
            this.browserPathLabel = new System.Windows.Forms.Label();
            this.mediaPlayerPathLabel = new System.Windows.Forms.Label();
            this.headlineTimerSecondNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.headlineTimerSecondLabel = new System.Windows.Forms.Label();
            this.settingTabControl = new System.Windows.Forms.TabControl();
            this.viewTabPage = new System.Windows.Forms.TabPage();
            this.headlineListBoxFontSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.headlineListBoxFontSizeCheckBox = new System.Windows.Forms.CheckBox();
            this.networkTabPage = new System.Windows.Forms.TabPage();
            this.proxySettingPanel = new System.Windows.Forms.Panel();
            this.proxyUseOriginalSettingRadioButton = new System.Windows.Forms.RadioButton();
            this.proxyPortTextBox = new System.Windows.Forms.TextBox();
            this.proxyPortContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.copyProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.proxyUseOsSettingRadioButton = new System.Windows.Forms.RadioButton();
            this.proxyServerTextBox = new System.Windows.Forms.TextBox();
            this.proxyServerContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.copyProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.proxyUnuseRadioButton = new System.Windows.Forms.RadioButton();
            this.proxyPortLabel = new System.Windows.Forms.Label();
            this.proxyServerLabel = new System.Windows.Forms.Label();
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
            // browserPathContextMenu
            // 
            this.browserPathContextMenu.MenuItems.Add(this.cutBrowserPathMenuItem);
            this.browserPathContextMenu.MenuItems.Add(this.copyBrowserPathMenuItem);
            this.browserPathContextMenu.MenuItems.Add(this.pasteBrowserPathMenuItem);
            // 
            // cutBrowserPathMenuItem
            // 
            this.cutBrowserPathMenuItem.Text = "�؂���(&T)";
            this.cutBrowserPathMenuItem.Click += new System.EventHandler(this.CutBrowserPathMenuItem_Click);
            // 
            // copyBrowserPathMenuItem
            // 
            this.copyBrowserPathMenuItem.Text = "�R�s�[(&C)";
            this.copyBrowserPathMenuItem.Click += new System.EventHandler(this.CopyBrowserPathMenuItem_Click);
            // 
            // pasteBrowserPathMenuItem
            // 
            this.pasteBrowserPathMenuItem.Text = "�\��t��(&P)";
            this.pasteBrowserPathMenuItem.Click += new System.EventHandler(this.PasteBrowserPathMenuItem_Click);
            // 
            // mediaPlayerPathContextMenu
            // 
            this.mediaPlayerPathContextMenu.MenuItems.Add(this.cutMediaPlayerPathMenuItem);
            this.mediaPlayerPathContextMenu.MenuItems.Add(this.copyMediaPlayerPathMenuItem);
            this.mediaPlayerPathContextMenu.MenuItems.Add(this.pasteMediaPlayerPathMenuItem);
            // 
            // cutMediaPlayerPathMenuItem
            // 
            this.cutMediaPlayerPathMenuItem.Text = "�؂���(&T)";
            this.cutMediaPlayerPathMenuItem.Click += new System.EventHandler(this.CutMediaPlayeraPathMenuItem_Click);
            // 
            // copyMediaPlayerPathMenuItem
            // 
            this.copyMediaPlayerPathMenuItem.Text = "�R�s�[(&C)";
            this.copyMediaPlayerPathMenuItem.Click += new System.EventHandler(this.CopyMediaPlayeraPathMenuItem_Click);
            // 
            // pasteMediaPlayerPathMenuItem
            // 
            this.pasteMediaPlayerPathMenuItem.Text = "�\��t��(&P)";
            this.pasteMediaPlayerPathMenuItem.Click += new System.EventHandler(this.PasteMediaPlayeraPathMenuItem_Click);
            // 
            // basicTabPage
            // 
            this.basicTabPage.Controls.Add(this.playlistSaveLocalCheckBox);
            this.basicTabPage.Controls.Add(this.browserPathReferenceButton);
            this.basicTabPage.Controls.Add(this.mediaPlayerPathReferenceButton);
            this.basicTabPage.Controls.Add(this.browserPathTextBox);
            this.basicTabPage.Controls.Add(this.mediaPlayerPathTextBox);
            this.basicTabPage.Controls.Add(this.browserPathLabel);
            this.basicTabPage.Controls.Add(this.mediaPlayerPathLabel);
            this.basicTabPage.Controls.Add(this.headlineTimerSecondNumericUpDown);
            this.basicTabPage.Controls.Add(this.headlineTimerSecondLabel);
            this.basicTabPage.Location = new System.Drawing.Point(0, 0);
            this.basicTabPage.Size = new System.Drawing.Size(240, 245);
            this.basicTabPage.Text = "��{";
            // 
            // playlistSaveLocalCheckBox
            // 
            this.playlistSaveLocalCheckBox.Location = new System.Drawing.Point(3, 154);
            this.playlistSaveLocalCheckBox.Size = new System.Drawing.Size(234, 20);
            this.playlistSaveLocalCheckBox.Text = "�v���C���X�g�͈�x���[�J���ɕۑ�����";
            // 
            // browserPathReferenceButton
            // 
            this.browserPathReferenceButton.Location = new System.Drawing.Point(189, 114);
            this.browserPathReferenceButton.Size = new System.Drawing.Size(48, 20);
            this.browserPathReferenceButton.Text = "�Q��";
            this.browserPathReferenceButton.Click += new System.EventHandler(this.BrowserPathReferenceButton_Click);
            // 
            // mediaPlayerPathReferenceButton
            // 
            this.mediaPlayerPathReferenceButton.Location = new System.Drawing.Point(189, 71);
            this.mediaPlayerPathReferenceButton.Size = new System.Drawing.Size(48, 20);
            this.mediaPlayerPathReferenceButton.Text = "�Q��";
            this.mediaPlayerPathReferenceButton.Click += new System.EventHandler(this.MediaPlayerPathReferenceButton_Click);
            // 
            // browserPathTextBox
            // 
            this.browserPathTextBox.ContextMenu = this.browserPathContextMenu;
            this.browserPathTextBox.Location = new System.Drawing.Point(3, 114);
            this.browserPathTextBox.Size = new System.Drawing.Size(180, 21);
            this.browserPathTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BrowserPathTextBox_KeyUp);
            this.browserPathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BrowserPathTextBox_KeyDown);
            // 
            // mediaPlayerPathTextBox
            // 
            this.mediaPlayerPathTextBox.ContextMenu = this.mediaPlayerPathContextMenu;
            this.mediaPlayerPathTextBox.Location = new System.Drawing.Point(3, 71);
            this.mediaPlayerPathTextBox.Size = new System.Drawing.Size(180, 21);
            this.mediaPlayerPathTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MediaPlayerPathTextBox_KeyUp);
            this.mediaPlayerPathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MediaPlayerPathTextBox_KeyDown);
            // 
            // browserPathLabel
            // 
            this.browserPathLabel.Location = new System.Drawing.Point(3, 95);
            this.browserPathLabel.Size = new System.Drawing.Size(234, 16);
            this.browserPathLabel.Text = "�u���E�U�̃p�X";
            // 
            // mediaPlayerPathLabel
            // 
            this.mediaPlayerPathLabel.Location = new System.Drawing.Point(3, 52);
            this.mediaPlayerPathLabel.Size = new System.Drawing.Size(234, 16);
            this.mediaPlayerPathLabel.Text = "���f�B�A�v���[���[�̃p�X";
            // 
            // headlineTimerSecondNumericUpDown
            // 
            this.headlineTimerSecondNumericUpDown.Location = new System.Drawing.Point(182, 27);
            this.headlineTimerSecondNumericUpDown.ReadOnly = true;
            this.headlineTimerSecondNumericUpDown.Size = new System.Drawing.Size(55, 22);
            this.headlineTimerSecondNumericUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // headlineTimerSecondLabel
            // 
            this.headlineTimerSecondLabel.Location = new System.Drawing.Point(3, 4);
            this.headlineTimerSecondLabel.Size = new System.Drawing.Size(234, 20);
            this.headlineTimerSecondLabel.Text = "�w�b�h���C���̎����`�F�b�N�Ԋu(�b)";
            // 
            // settingTabControl
            // 
            this.settingTabControl.Controls.Add(this.basicTabPage);
            this.settingTabControl.Controls.Add(this.viewTabPage);
            this.settingTabControl.Controls.Add(this.networkTabPage);
            this.settingTabControl.Location = new System.Drawing.Point(0, 0);
            this.settingTabControl.SelectedIndex = 0;
            this.settingTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // viewTabPage
            // 
            this.viewTabPage.Controls.Add(this.headlineListBoxFontSizeNumericUpDown);
            this.viewTabPage.Controls.Add(this.headlineListBoxFontSizeCheckBox);
            this.viewTabPage.Location = new System.Drawing.Point(0, 0);
            this.viewTabPage.Size = new System.Drawing.Size(232, 242);
            this.viewTabPage.Text = "�\��";
            // 
            // headlineListBoxFontSizeNumericUpDown
            // 
            this.headlineListBoxFontSizeNumericUpDown.Location = new System.Drawing.Point(182, 30);
            this.headlineListBoxFontSizeNumericUpDown.ReadOnly = true;
            this.headlineListBoxFontSizeNumericUpDown.Size = new System.Drawing.Size(55, 22);
            this.headlineListBoxFontSizeNumericUpDown.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // headlineListBoxFontSizeCheckBox
            // 
            this.headlineListBoxFontSizeCheckBox.Location = new System.Drawing.Point(3, 7);
            this.headlineListBoxFontSizeCheckBox.Size = new System.Drawing.Size(234, 20);
            this.headlineListBoxFontSizeCheckBox.Text = "�ԑg�\�̃t�H���g�T�C�Y��ύX����ipt�j";
            // 
            // networkTabPage
            // 
            this.networkTabPage.Controls.Add(this.proxySettingPanel);
            this.networkTabPage.Location = new System.Drawing.Point(0, 0);
            this.networkTabPage.Size = new System.Drawing.Size(232, 242);
            this.networkTabPage.Text = "�l�b�g���[�N";
            // 
            // proxySettingPanel
            // 
            this.proxySettingPanel.Controls.Add(this.proxyUseOriginalSettingRadioButton);
            this.proxySettingPanel.Controls.Add(this.proxyPortTextBox);
            this.proxySettingPanel.Controls.Add(this.proxyUseOsSettingRadioButton);
            this.proxySettingPanel.Controls.Add(this.proxyServerTextBox);
            this.proxySettingPanel.Controls.Add(this.proxyUnuseRadioButton);
            this.proxySettingPanel.Controls.Add(this.proxyPortLabel);
            this.proxySettingPanel.Controls.Add(this.proxyServerLabel);
            this.proxySettingPanel.Location = new System.Drawing.Point(0, 0);
            this.proxySettingPanel.Size = new System.Drawing.Size(240, 169);
            // 
            // proxyUseOriginalSettingRadioButton
            // 
            this.proxyUseOriginalSettingRadioButton.Location = new System.Drawing.Point(3, 55);
            this.proxyUseOriginalSettingRadioButton.Size = new System.Drawing.Size(234, 20);
            this.proxyUseOriginalSettingRadioButton.Text = "�v���L�V��ݒ肷��";
            // 
            // proxyPortTextBox
            // 
            this.proxyPortTextBox.ContextMenu = this.proxyPortContextMenu;
            this.proxyPortTextBox.Location = new System.Drawing.Point(3, 140);
            this.proxyPortTextBox.Size = new System.Drawing.Size(74, 21);
            this.proxyPortTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProxyPortTextBox_KeyUp);
            this.proxyPortTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProxyPortTextBox_KeyDown);
            // 
            // proxyPortContextMenu
            // 
            this.proxyPortContextMenu.MenuItems.Add(this.cutProxyPortMenuItem);
            this.proxyPortContextMenu.MenuItems.Add(this.copyProxyPortMenuItem);
            this.proxyPortContextMenu.MenuItems.Add(this.pasteProxyPortMenuItem);
            // 
            // cutProxyPortMenuItem
            // 
            this.cutProxyPortMenuItem.Text = "�؂���(&T)";
            this.cutProxyPortMenuItem.Click += new System.EventHandler(this.CutProxyPortMenuItem_Click);
            // 
            // copyProxyPortMenuItem
            // 
            this.copyProxyPortMenuItem.Text = "�R�s�[(&C)";
            this.copyProxyPortMenuItem.Click += new System.EventHandler(this.CopyProxyPortMenuItem_Click);
            // 
            // pasteProxyPortMenuItem
            // 
            this.pasteProxyPortMenuItem.Text = "�\��t��(&P)";
            this.pasteProxyPortMenuItem.Click += new System.EventHandler(this.PasteProxyPortMenuItem_Click);
            // 
            // proxyUseOsSettingRadioButton
            // 
            this.proxyUseOsSettingRadioButton.Checked = true;
            this.proxyUseOsSettingRadioButton.Location = new System.Drawing.Point(3, 29);
            this.proxyUseOsSettingRadioButton.Size = new System.Drawing.Size(234, 20);
            this.proxyUseOsSettingRadioButton.Text = "OS�Őݒ肵���v���L�V���g�p����";
            // 
            // proxyServerTextBox
            // 
            this.proxyServerTextBox.ContextMenu = this.proxyServerContextMenu;
            this.proxyServerTextBox.Location = new System.Drawing.Point(3, 97);
            this.proxyServerTextBox.Size = new System.Drawing.Size(234, 21);
            this.proxyServerTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProxyServerTextBox_KeyUp);
            this.proxyServerTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProxyServerTextBox_KeyDown);
            // 
            // proxyServerContextMenu
            // 
            this.proxyServerContextMenu.MenuItems.Add(this.cutProxyServerMenuItem);
            this.proxyServerContextMenu.MenuItems.Add(this.copyProxyServerMenuItem);
            this.proxyServerContextMenu.MenuItems.Add(this.pasteProxyServerMenuItem);
            // 
            // cutProxyServerMenuItem
            // 
            this.cutProxyServerMenuItem.Text = "�؂���(&T)";
            this.cutProxyServerMenuItem.Click += new System.EventHandler(this.CutProxyServerMenuItem_Click);
            // 
            // copyProxyServerMenuItem
            // 
            this.copyProxyServerMenuItem.Text = "�R�s�[(&C)";
            this.copyProxyServerMenuItem.Click += new System.EventHandler(this.CopyProxyServerMenuItem_Click);
            // 
            // pasteProxyServerMenuItem
            // 
            this.pasteProxyServerMenuItem.Text = "�\��t��(&P)";
            this.pasteProxyServerMenuItem.Click += new System.EventHandler(this.PasteProxyServerMenuItem_Click);
            // 
            // proxyUnuseRadioButton
            // 
            this.proxyUnuseRadioButton.Location = new System.Drawing.Point(3, 3);
            this.proxyUnuseRadioButton.Size = new System.Drawing.Size(234, 20);
            this.proxyUnuseRadioButton.Text = "�v���L�V�ɐڑ����Ȃ�";
            // 
            // proxyPortLabel
            // 
            this.proxyPortLabel.Location = new System.Drawing.Point(3, 121);
            this.proxyPortLabel.Size = new System.Drawing.Size(234, 16);
            this.proxyPortLabel.Text = "�v���L�V�̃|�[�g�ԍ� �i��F 8080�j";
            // 
            // proxyServerLabel
            // 
            this.proxyServerLabel.Location = new System.Drawing.Point(3, 78);
            this.proxyServerLabel.Size = new System.Drawing.Size(234, 16);
            this.proxyServerLabel.Text = "�v���L�V�T�[�o �i��Fproxy.example.com�j";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.settingTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "�ݒ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // �w�b�h���C���`�F�b�N�^�C�}�[�̏���Ɖ���
            headlineTimerSecondNumericUpDown.Minimum = PocketLadioInfo.HeadlineCheckTimerMinimumMillSec / 1000;
            headlineTimerSecondNumericUpDown.Maximum = PocketLadioInfo.HeadlineCheckTimerMaximumMillSec / 1000;

            // �ԑg�\�̃t�H���g�T�C�Y����Ƃ̉���
            headlineListBoxFontSizeNumericUpDown.Minimum = PocketLadioInfo.HeadlineListBoxFontSizeMinimumPt;
            headlineListBoxFontSizeNumericUpDown.Maximum = PocketLadioInfo.HeadlineListBoxFontSizeMaximumPt;

            // �ݒ�̓ǂݍ���
            {
                headlineTimerSecondNumericUpDown.Text = (UserSetting.HeadlineTimerMillSecond / 1000).ToString();
                mediaPlayerPathTextBox.Text = UserSetting.MediaPlayerPath;
                browserPathTextBox.Text = UserSetting.BrowserPath;

                if (UserSetting.PlayListSave == true)
                {
                    playlistSaveLocalCheckBox.Checked = true;
                }
                else if (UserSetting.PlayListSave == false)
                {
                    playlistSaveLocalCheckBox.Checked = false;
                }

                headlineListBoxFontSizeCheckBox.Checked = UserSetting.HeadlineListBoxFontSizeChange;
                headlineListBoxFontSizeNumericUpDown.Text = UserSetting.HeadlineListBoxFontSize.ToString();

                if (UserSetting.ProxyUse == UserSetting.ProxyConnect.Unuse)
                {
                    proxyUnuseRadioButton.Checked = true;
                    proxyUseOsSettingRadioButton.Checked = false;
                    proxyUseOriginalSettingRadioButton.Checked = false;
                }
                else if (UserSetting.ProxyUse == UserSetting.ProxyConnect.OsSetting)
                {
                    proxyUnuseRadioButton.Checked = false;
                    proxyUseOsSettingRadioButton.Checked = true;
                    proxyUseOriginalSettingRadioButton.Checked = false;
                }
                else if (UserSetting.ProxyUse == UserSetting.ProxyConnect.OriginalSetting)
                {
                    proxyUnuseRadioButton.Checked = false;
                    proxyUseOsSettingRadioButton.Checked = false;
                    proxyUseOriginalSettingRadioButton.Checked = true;
                }
                else
                {
                    // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                    Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
                }

                proxyServerTextBox.Text = UserSetting.ProxyServer;
                proxyPortTextBox.Text = UserSetting.ProxyPort.ToString();
            }
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �ݒ�̏�������
            if (playlistSaveLocalCheckBox.Checked == true)
            {
                UserSetting.PlayListSave = true;
            }
            else if (playlistSaveLocalCheckBox.Checked == false)
            {
                UserSetting.PlayListSave = false;
            }

            UserSetting.MediaPlayerPath = mediaPlayerPathTextBox.Text.Trim();
            UserSetting.BrowserPath = browserPathTextBox.Text.Trim();

            try
            {
                UserSetting.HeadlineTimerMillSecond = Convert.ToInt32(headlineTimerSecondNumericUpDown.Text) * 1000;
            }
            catch (ArgumentException)
            {
                ;
            }
            catch (FormatException)
            {
                ;
            }
            catch (OverflowException)
            {
                ;
            }

            if (headlineListBoxFontSizeCheckBox.Checked == true)
            {
                UserSetting.HeadlineListBoxFontSizeChange = true;
            }
            else if (headlineListBoxFontSizeCheckBox.Checked == false)
            {
                UserSetting.HeadlineListBoxFontSizeChange = false;
            }
            try
            {
                UserSetting.HeadlineListBoxFontSize = Convert.ToInt32(headlineListBoxFontSizeNumericUpDown.Text);
            }
            catch (ArgumentException)
            {
                ;
            }
            catch (FormatException)
            {
                ;
            }
            catch (OverflowException)
            {
                ;
            }

            if (proxyUnuseRadioButton.Checked == true)
            {
                UserSetting.ProxyUse = UserSetting.ProxyConnect.Unuse;
            }
            else if (proxyUseOsSettingRadioButton.Checked == true)
            {
                UserSetting.ProxyUse = UserSetting.ProxyConnect.OsSetting;
            }
            else if (proxyUseOriginalSettingRadioButton.Checked == true)
            {
                UserSetting.ProxyUse = UserSetting.ProxyConnect.OriginalSetting;
            }
            else
            {
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }
            UserSetting.ProxyServer = proxyServerTextBox.Text.Trim();
            try
            {
                UserSetting.ProxyPort = int.Parse(proxyPortTextBox.Text.Trim());
            }
            catch (ArgumentException)
            {
                ;
            }
            catch (FormatException)
            {
                ;
            }
            catch (OverflowException)
            {
                ;
            }

            try
            {
                UserSetting.SaveSetting();
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

        private void CutMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(mediaPlayerPathTextBox);
        }

        private void CopyMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(mediaPlayerPathTextBox);
        }

        private void PasteMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(mediaPlayerPathTextBox);
        }

        private void CutBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(browserPathTextBox);
        }

        private void CopyBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(browserPathTextBox);
        }

        private void PasteBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(browserPathTextBox);
        }

        private void CutProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(proxyServerTextBox);
        }

        private void CopyProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(proxyServerTextBox);
        }

        private void PasteProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(proxyServerTextBox);
        }

        private void CutProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(proxyPortTextBox);
        }

        private void CopyProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(proxyPortTextBox);
        }

        private void PasteProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(proxyPortTextBox);
        }

        private void MediaPlayerPathReferenceButton_Click(object sender, EventArgs e)
        {
            SmartPDA.Windows.Forms.OpenFileDialog fd = SmartPDA.Windows.Forms.FileDialogFactory.MakeOpenFileDialog();

            if (Directory.Exists(Path.GetDirectoryName(mediaPlayerPathTextBox.Text.Trim())))
            {
                fd.InitialDirectory = Path.GetDirectoryName(mediaPlayerPathTextBox.Text.Trim());
            }
            fd.Filter = "*.exe|*.exe|*.*|*.*";
            fd.IconVisible = true;
            fd.Activation = ItemActivation.OneClick;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                mediaPlayerPathTextBox.Text = fd.FileName;
            }
            fd.Dispose();
        }

        private void BrowserPathReferenceButton_Click(object sender, EventArgs e)
        {
            SmartPDA.Windows.Forms.OpenFileDialog fd = SmartPDA.Windows.Forms.FileDialogFactory.MakeOpenFileDialog();

            if (Directory.Exists(Path.GetDirectoryName(browserPathTextBox.Text.Trim())))
            {
                fd.InitialDirectory = Path.GetDirectoryName(browserPathTextBox.Text.Trim());
            }
            fd.Filter = "*.exe|*.exe|*.*|*.*";
            fd.IconVisible = true;
            fd.Activation = ItemActivation.OneClick;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                browserPathTextBox.Text = fd.FileName;
            }
            fd.Dispose();
        }

        private void MediaPlayerPathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(mediaPlayerPathTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(mediaPlayerPathTextBox);
            }
        }

        private void MediaPlayerPathTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(mediaPlayerPathTextBox);
            }
        }

        private void BrowserPathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(browserPathTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(browserPathTextBox);
            }
        }

        private void BrowserPathTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(browserPathTextBox);
            }
        }

        private void ProxyServerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(proxyServerTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(proxyServerTextBox);
            }
        }

        private void ProxyServerTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(proxyServerTextBox);
            }
        }

        private void ProxyPortTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(proxyPortTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(proxyPortTextBox);
            }
        }

        private void ProxyPortTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(proxyPortTextBox);
            }
        }
    }
}
