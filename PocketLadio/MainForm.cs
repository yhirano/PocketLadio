using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using PocketLadio.Netladio;

namespace PocketLadio
{
    /// <summary>
    /// �A�v���P�[�V�����̃��C���t�H�[��
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MenuItem MenuMenuItem;
        private System.Windows.Forms.MenuItem HeadlineCheckTimerMenuItem;
        private System.Windows.Forms.MenuItem FilterSettingMenuItem;
        private System.Windows.Forms.MenuItem SettingMenuItem;
        private System.Windows.Forms.MenuItem VersionInfoMenuItem;
        private System.Windows.Forms.MenuItem SeparateMenuItem1;
        private System.Windows.Forms.MenuItem ExitMenuItem;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.ListBox HeadlineListBox;
        private System.Windows.Forms.Button GetButton;
        private System.Windows.Forms.CheckBox FilterCheckBox;
        private System.Windows.Forms.Label InfomationLabel;
        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.ContextMenu HeadlineContextMenu;
        private System.Windows.Forms.MenuItem PlayMenuItem;
        private System.Windows.Forms.MenuItem BrowserMenuItem;
        private System.Windows.Forms.MenuItem ChanelPropertyMenuItem;
        private System.Windows.Forms.MenuItem PocketLadioSettingMenuItem;
        private System.Windows.Forms.MenuItem NetladioSettingMenuItem;
        private System.Windows.Forms.Timer HeadlineCheckTimer;

        public MainForm()
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
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.MenuMenuItem = new System.Windows.Forms.MenuItem();
            this.HeadlineCheckTimerMenuItem = new System.Windows.Forms.MenuItem();
            this.FilterSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.SettingMenuItem = new System.Windows.Forms.MenuItem();
            this.PocketLadioSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.NetladioSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.VersionInfoMenuItem = new System.Windows.Forms.MenuItem();
            this.SeparateMenuItem1 = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.PlayButton = new System.Windows.Forms.Button();
            this.HeadlineListBox = new System.Windows.Forms.ListBox();
            this.HeadlineContextMenu = new System.Windows.Forms.ContextMenu();
            this.PlayMenuItem = new System.Windows.Forms.MenuItem();
            this.BrowserMenuItem = new System.Windows.Forms.MenuItem();
            this.ChanelPropertyMenuItem = new System.Windows.Forms.MenuItem();
            this.GetButton = new System.Windows.Forms.Button();
            this.FilterCheckBox = new System.Windows.Forms.CheckBox();
            this.InfomationLabel = new System.Windows.Forms.Label();
            this.HeadlineCheckTimer = new System.Windows.Forms.Timer();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.Add(this.MenuMenuItem);
            // 
            // MenuMenuItem
            // 
            this.MenuMenuItem.MenuItems.Add(this.HeadlineCheckTimerMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.FilterSettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.SettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.VersionInfoMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.SeparateMenuItem1);
            this.MenuMenuItem.MenuItems.Add(this.ExitMenuItem);
            this.MenuMenuItem.Text = "���j���[(&M)";
            // 
            // HeadlineCheckTimerMenuItem
            // 
            this.HeadlineCheckTimerMenuItem.Text = "�w�b�h���C�������Ԋu�Ń`�F�b�N(&T)";
            this.HeadlineCheckTimerMenuItem.Click += new System.EventHandler(this.HeadlineCheckTimerMenuItem_Click);
            // 
            // FilterSettingMenuItem
            // 
            this.FilterSettingMenuItem.Text = "�t�B���^�[�ݒ�(&F)";
            this.FilterSettingMenuItem.Click += new System.EventHandler(this.FilterSettingMenuItem_Click);
            // 
            // SettingMenuItem
            // 
            this.SettingMenuItem.MenuItems.Add(this.PocketLadioSettingMenuItem);
            this.SettingMenuItem.MenuItems.Add(this.NetladioSettingMenuItem);
            this.SettingMenuItem.Text = "�ݒ�(&S)";
            // 
            // PocketLadioSettingMenuItem
            // 
            this.PocketLadioSettingMenuItem.Text = "PocketLadio�ݒ�(&P)";
            this.PocketLadioSettingMenuItem.Click += new System.EventHandler(this.PocketLadioSettingMenuItem_Click);
            // 
            // NetladioSettingMenuItem
            // 
            this.NetladioSettingMenuItem.Text = "�˂Ƃ炶�ݒ�(&N)";
            this.NetladioSettingMenuItem.Click += new System.EventHandler(this.NetladioSettingMenuItem_Click);
            // 
            // VersionInfoMenuItem
            // 
            this.VersionInfoMenuItem.Text = "�o�[�W�������(&A)";
            this.VersionInfoMenuItem.Click += new System.EventHandler(this.VersionInfoMenuItem_Click);
            // 
            // SeparateMenuItem1
            // 
            this.SeparateMenuItem1.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Text = "�I��(&X)";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(81, 3);
            this.PlayButton.Size = new System.Drawing.Size(72, 20);
            this.PlayButton.Text = "&Play";
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // HeadlineListBox
            // 
            this.HeadlineListBox.ContextMenu = this.HeadlineContextMenu;
            this.HeadlineListBox.Location = new System.Drawing.Point(3, 32);
            this.HeadlineListBox.Size = new System.Drawing.Size(234, 212);
            this.HeadlineListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            // 
            // HeadlineContextMenu
            // 
            this.HeadlineContextMenu.MenuItems.Add(this.PlayMenuItem);
            this.HeadlineContextMenu.MenuItems.Add(this.BrowserMenuItem);
            this.HeadlineContextMenu.MenuItems.Add(this.ChanelPropertyMenuItem);
            this.HeadlineContextMenu.Popup += new System.EventHandler(this.HeadlineContextMenu_Popup);
            // 
            // PlayMenuItem
            // 
            this.PlayMenuItem.Text = "�Đ�(&P)";
            this.PlayMenuItem.Click += new System.EventHandler(this.PlayMenuItem_Click);
            // 
            // BrowserMenuItem
            // 
            this.BrowserMenuItem.Text = "�u���E�U�ŃA�N�Z�X(&A)";
            this.BrowserMenuItem.Click += new System.EventHandler(this.BrowserMenuItem_Click);
            // 
            // ChanelPropertyMenuItem
            // 
            this.ChanelPropertyMenuItem.Text = "�ԑg�̏ڍ�(&R)";
            this.ChanelPropertyMenuItem.Click += new System.EventHandler(this.ChanelPropertyMenuItem_Click);
            // 
            // GetButton
            // 
            this.GetButton.Location = new System.Drawing.Point(3, 3);
            this.GetButton.Size = new System.Drawing.Size(72, 20);
            this.GetButton.Text = "&Get";
            this.GetButton.Click += new System.EventHandler(this.GetButton_Click);
            // 
            // FilterCheckBox
            // 
            this.FilterCheckBox.Location = new System.Drawing.Point(181, 3);
            this.FilterCheckBox.Size = new System.Drawing.Size(56, 20);
            this.FilterCheckBox.Text = "&Filter";
            this.FilterCheckBox.CheckStateChanged += new System.EventHandler(this.FilterCheckBox_CheckStateChanged);
            // 
            // InfomationLabel
            // 
            this.InfomationLabel.Location = new System.Drawing.Point(3, 247);
            this.InfomationLabel.Size = new System.Drawing.Size(234, 16);
            this.InfomationLabel.Text = "No check - 0 CHs";
            this.InfomationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // HeadlineCheckTimer
            // 
            this.HeadlineCheckTimer.Tick += new System.EventHandler(this.HeadlineCheckTimer_Tick);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.InfomationLabel);
            this.Controls.Add(this.FilterCheckBox);
            this.Controls.Add(this.GetButton);
            this.Controls.Add(this.HeadlineListBox);
            this.Controls.Add(this.PlayButton);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "PocketLadio";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.GotFocus += new System.EventHandler(this.MainForm_GotFocus);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Load += new System.EventHandler(this.MainForm_Load);

        }
        #endregion

        /// <summary>
        /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
        /// </summary>

        static void Main()
        {
            Application.Run(new MainForm());
        }

        /// <summary>
        /// �ԑg���X�g���X�V����
        /// </summary>
        /// <param name="chanels">�ԑg�̔z��</param>
        private void UpdateRadioList(Chanel[] chanels)
        {
            HeadlineListBox.Items.Clear();

            foreach (Chanel Chanel in chanels)
            {
                HeadlineListBox.Items.Add(Chanel.GetChanelView());
            }
        }

        /// <summary>
        /// �X�g���[�~���O���Đ�����
        /// </summary>
        private void PlayStreaming()
        {
            if (HeadlineListBox.SelectedIndex != -1)
            {
                Controller.PlayStreaming(ChanelList.GetChanels()[HeadlineListBox.SelectedIndex].GetPlayUrl());
            }
        }

        /// <summary>
        /// Web�T�C�g�փA�N�Z�X����
        /// </summary>
        private void AccessWebSite()
        {
            if (HeadlineListBox.SelectedIndex != -1)
            {
                if (ChanelList.GetChanels()[HeadlineListBox.SelectedIndex].Url != "")
                {
                    Controller.AccessWebSite(ChanelList.GetChanels()[HeadlineListBox.SelectedIndex].Url);
                }
            }
        }

        /// <summary>
        /// �w�b�h���C�����擾����
        /// </summary>
        private void CheckHeadline()
        {
            try
            {
                lock (this)
                {
                    Headline.WebGetHeadline();
                    InfomationLabel.Text = "Last " + DateTime.Now.ToString() + " - " + Headline.GetChanels().Length.ToString() + " CHs";
                    UpdateRadioList(ChanelList.GetChanels());
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (OutOfMemoryException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (XmlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// �^�C�}�[�̃X�^�[�g���̏���
        /// </summary>
        private void HeadlineCheckTimerStart()
        {
            UserSetting.HeadlineTimerCheck = true;
            HeadlineCheckTimerMenuItem.Checked = true;
            HeadlineCheckTimer.Interval = UserSetting.HeadlineTimerMillSecond;
            HeadlineCheckTimer.Enabled = true;
        }

        /// <summary>
        /// �^�C�}�[�̃X�g�b�v���̏���
        /// </summary>
        private void HeadlineCheckTimerStop()
        {
            UserSetting.HeadlineTimerCheck = false;
            HeadlineCheckTimerMenuItem.Checked = false;
            HeadlineCheckTimer.Enabled = false;
        }

        /// <summary>
        /// �^�C�}�[�̃C���^�[�o�����ύX���ꂽ�Ƃ��̏���
        /// </summary>
        /// <param name="intarval">�^�C�}�[�̃C���^�[�o��</param>
        public void HeadlineTimerIntarvalChange(int intarval)
        {
            if (UserSetting.HeadlineTimerCheck == true)
            {
                HeadlineCheckTimer.Enabled = false;
                HeadlineCheckTimer.Interval = intarval;
                HeadlineCheckTimer.Enabled = true;
            }
            else
            {
                HeadlineCheckTimer.Interval = intarval;
            }
        }

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
            this.GetButton.Location = new System.Drawing.Point(3, 3);
            this.PlayButton.Location = new System.Drawing.Point(81, 3);
            this.FilterCheckBox.Location = new System.Drawing.Point(181, 3);
            this.HeadlineListBox.Location = new System.Drawing.Point(3, 32);
            this.HeadlineListBox.Size = new System.Drawing.Size(234, 212);
            this.InfomationLabel.Location = new System.Drawing.Point(3, 247);
            this.InfomationLabel.Size = new System.Drawing.Size(234, 16);
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����i�����j
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.GetButton.Location = new System.Drawing.Point(3, 3);
            this.PlayButton.Location = new System.Drawing.Point(81, 3);
            this.FilterCheckBox.Location = new System.Drawing.Point(261, 3);
            this.HeadlineListBox.Location = new System.Drawing.Point(3, 32);
            this.HeadlineListBox.Size = new System.Drawing.Size(314, 128);
            this.InfomationLabel.Location = new System.Drawing.Point(3, 163);
            this.InfomationLabel.Size = new System.Drawing.Size(314, 16);
        }

        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void VersionInfoMenuItem_Click(object sender, System.EventArgs e)
        {
            VersionInfoForm versionInfoForm = new VersionInfoForm();
            DialogResult result = versionInfoForm.ShowDialog();
            versionInfoForm.Dispose();
        }

        private void FilterSettingMenuItem_Click(object sender, System.EventArgs e)
        {
            FilterSettingForm filterSettingForm = new FilterSettingForm();
            DialogResult result = filterSettingForm.ShowDialog();
            filterSettingForm.Dispose();
        }

        private void GetButton_Click(object sender, System.EventArgs e)
        {
            GetButton.Enabled = false;

            try
            {
                CheckHeadline();
            }
            catch (WebException)
            {
                MessageBox.Show("�ԑg�\���擾�ł��܂���ł���", "�ڑ��G���[");
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("������������܂���", "�������G���[");
            }
            catch (IOException)
            {
                MessageBox.Show("�L�^�f�o�C�X�����炩�̃G���[�ł�", "�f�o�C�X�G���[");
            }
            catch (XmlException)
            {
                MessageBox.Show("XML�`���̃w�b�h���C��������ɏ����ł��܂���ł���", "XML�G���[");
            }
            finally
            {
                GetButton.Enabled = true;
            }

            GetButton.Enabled = true;
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            PlayStreaming();
        }

        private void FilterCheckBox_CheckStateChanged(object sender, System.EventArgs e)
        {
            if (FilterCheckBox.Checked == true)
            {
                ChanelList.FilterEnable = true;
            }
            else
            {
                ChanelList.FilterEnable = false;
            }
            UpdateRadioList(ChanelList.GetChanels());
        }

        private void ChanelPropertyMenuItem_Click(object sender, System.EventArgs e)
        {
            if (HeadlineListBox.SelectedIndex != -1 && ChanelList.GetChanels().Length > 0)
            {
                ChanelPropertyForm ChanelPropertyForm = new ChanelPropertyForm(ChanelList.GetChanels()[HeadlineListBox.SelectedIndex]);
                DialogResult Result = ChanelPropertyForm.ShowDialog();
                ChanelPropertyForm.Dispose();
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Controller.LoadSettings();
            if (UserSetting.HeadlineTimerCheck == true)
            {
                HeadlineCheckTimerStart();
            }
            else
            {
                HeadlineCheckTimerStop();
            }
            
            // �ԑg���X�g������ꍇ
            if (ChanelList.GetChanels().Length > 0)
            {
                UpdateRadioList(ChanelList.GetChanels());
            }

            FixWindowSize();
        }

        private void HeadlineContextMenu_Popup(object sender, System.EventArgs e)
        {
            if (HeadlineListBox.SelectedIndex == -1)
            {
                ChanelPropertyMenuItem.Enabled = false;
                PlayMenuItem.Enabled = false;
                BrowserMenuItem.Enabled = false;
            }
            else
            {
                ChanelPropertyMenuItem.Enabled = true;
                PlayMenuItem.Enabled = true;
                BrowserMenuItem.Enabled = true;
            }
        }

        private void PlayMenuItem_Click(object sender, System.EventArgs e)
        {
            PlayStreaming();
        }

        private void BrowserMenuItem_Click(object sender, System.EventArgs e)
        {
            AccessWebSite();
        }

        private void HeadlineCheckTimerMenuItem_Click(object sender, System.EventArgs e)
        {
            if (UserSetting.HeadlineTimerCheck == true)
            {
                HeadlineCheckTimerStop();
            }
            else if (UserSetting.HeadlineTimerCheck == false)
            {
                HeadlineCheckTimerStart();
            }
        }

        private void HeadlineCheckTimer_Tick(object sender, System.EventArgs e)
        {
            GetButton.Enabled = false;

            try
            {
                CheckHeadline();
            }
            catch (WebException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("�ԑg�\���擾�ł��܂���ł���", "�ڑ��G���[");
            }
            catch (OutOfMemoryException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("������������܂���", "�������G���[");
            }
            catch (IOException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("�L�^�f�o�C�X�����炩�̃G���[�ł�", "�f�o�C�X�G���[");
            }
            catch (XmlException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("XML�`���̃w�b�h���C��������ɏ����ł��܂���ł���", "XML�G���[");
            }
            finally
            {
                GetButton.Enabled = true;
            }

            GetButton.Enabled = true;
        }

        private void MainForm_GotFocus(object sender, System.EventArgs e)
        {
            HeadlineCheckTimer.Interval = UserSetting.HeadlineTimerMillSecond;
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserSetting.SaveSetting();
        }

        private void PocketLadioSettingMenuItem_Click(object sender, System.EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            DialogResult result = settingForm.ShowDialog();
            settingForm.Dispose();
        }

        private void NetladioSettingMenuItem_Click(object sender, System.EventArgs e)
        {
            PocketLadio.Netladio.SettingForm settingForm = new PocketLadio.Netladio.SettingForm();
            DialogResult result = settingForm.ShowDialog();
            settingForm.Dispose();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // ���̓{�^�����������Ƃ�
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                PlayStreaming();
            }

        }
    }
}
