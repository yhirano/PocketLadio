#region �f�B���N�e�B�u���g�p����

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using PocketLadio.Stations;
using PocketLadio.Utility;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// �A�v���P�[�V�����̃��C���t�H�[��
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private MenuItem menuMenuItem;
        private MenuItem headlineCheckTimerMenuItem;
        private MenuItem filterSettingMenuItem;
        private MenuItem stationSettingMenuItem;
        private MenuItem versionInfoMenuItem;
        private MenuItem separateMenuItem1;
        private MenuItem exitMenuItem;
        private Button playButton;
        private ListBox headlineListBox;
        private Button getButton;
        private CheckBox filterCheckBox;
        private Label infomationLabel;
        private MainMenu mainMenu;
        private ContextMenu headlineContextMenu;
        private MenuItem playMenuItem;
        private MenuItem browserMenuItem;
        private MenuItem channelPropertyMenuItem;
        private MenuItem pocketLadioSettingMenuItem;
        private ComboBox stationListComboBox;
        private Timer headlineCheckTimer;
        private MenuItem stationsSettingMenuItem;
        private MenuItem separateMenuItem2;
        private MenuItem separateMenuItem3;
        private MenuItem separateMenuItem4;

        /// <summary>
        /// �I������Ă��������ǂ�ID
        /// </summary>
        private string selectedStationID = "";

        /// <summary>
        /// CheckHeadline()�̓���r�������̂��߂̃t���O
        /// </summary>
        private bool checkHeadlineNowFlag;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineCheckTimerMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem1 = new System.Windows.Forms.MenuItem();
            this.stationsSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.stationSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.filterSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem2 = new System.Windows.Forms.MenuItem();
            this.pocketLadioSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem3 = new System.Windows.Forms.MenuItem();
            this.versionInfoMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem4 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.playButton = new System.Windows.Forms.Button();
            this.headlineListBox = new System.Windows.Forms.ListBox();
            this.headlineContextMenu = new System.Windows.Forms.ContextMenu();
            this.playMenuItem = new System.Windows.Forms.MenuItem();
            this.browserMenuItem = new System.Windows.Forms.MenuItem();
            this.channelPropertyMenuItem = new System.Windows.Forms.MenuItem();
            this.getButton = new System.Windows.Forms.Button();
            this.filterCheckBox = new System.Windows.Forms.CheckBox();
            this.infomationLabel = new System.Windows.Forms.Label();
            this.headlineCheckTimer = new System.Windows.Forms.Timer();
            this.stationListComboBox = new System.Windows.Forms.ComboBox();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuMenuItem);
            // 
            // menuMenuItem
            // 
            this.menuMenuItem.MenuItems.Add(this.headlineCheckTimerMenuItem);
            this.menuMenuItem.MenuItems.Add(this.separateMenuItem1);
            this.menuMenuItem.MenuItems.Add(this.stationsSettingMenuItem);
            this.menuMenuItem.MenuItems.Add(this.stationSettingMenuItem);
            this.menuMenuItem.MenuItems.Add(this.filterSettingMenuItem);
            this.menuMenuItem.MenuItems.Add(this.separateMenuItem2);
            this.menuMenuItem.MenuItems.Add(this.pocketLadioSettingMenuItem);
            this.menuMenuItem.MenuItems.Add(this.separateMenuItem3);
            this.menuMenuItem.MenuItems.Add(this.versionInfoMenuItem);
            this.menuMenuItem.MenuItems.Add(this.separateMenuItem4);
            this.menuMenuItem.MenuItems.Add(this.exitMenuItem);
            this.menuMenuItem.Text = "���j���[(&M)";
            // 
            // headlineCheckTimerMenuItem
            // 
            this.headlineCheckTimerMenuItem.Text = "�w�b�h���C�������Ԋu�Ń`�F�b�N(&T)";
            this.headlineCheckTimerMenuItem.Click += new System.EventHandler(this.HeadlineCheckTimerMenuItem_Click);
            // 
            // separateMenuItem1
            // 
            this.separateMenuItem1.Text = "-";
            // 
            // stationsSettingMenuItem
            // 
            this.stationsSettingMenuItem.Text = "�����ǂ̒ǉ��ƍ폜 (&A)";
            this.stationsSettingMenuItem.Click += new System.EventHandler(this.StationsSettingMenuItem_Click);
            // 
            // stationSettingMenuItem
            // 
            this.stationSettingMenuItem.Text = "�����ǂ̐ݒ�(&S)";
            // 
            // filterSettingMenuItem
            // 
            this.filterSettingMenuItem.Text = "�t�B���^�[�̒ǉ��ƍ폜(&F)";
            this.filterSettingMenuItem.Click += new System.EventHandler(this.FilterSettingMenuItem_Click);
            // 
            // separateMenuItem2
            // 
            this.separateMenuItem2.Text = "-";
            // 
            // pocketLadioSettingMenuItem
            // 
            this.pocketLadioSettingMenuItem.Text = "PocketLadio�ݒ�(&P)";
            this.pocketLadioSettingMenuItem.Click += new System.EventHandler(this.PocketLadioSettingMenuItem_Click);
            // 
            // separateMenuItem3
            // 
            this.separateMenuItem3.Text = "-";
            // 
            // versionInfoMenuItem
            // 
            this.versionInfoMenuItem.Text = "�o�[�W�������(&A)";
            this.versionInfoMenuItem.Click += new System.EventHandler(this.VersionInfoMenuItem_Click);
            // 
            // separateMenuItem4
            // 
            this.separateMenuItem4.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Text = "�I��(&X)";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(81, 3);
            this.playButton.Size = new System.Drawing.Size(72, 20);
            this.playButton.Text = "&Play";
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // headlineListBox
            // 
            this.headlineListBox.ContextMenu = this.headlineContextMenu;
            this.headlineListBox.Location = new System.Drawing.Point(3, 60);
            this.headlineListBox.Size = new System.Drawing.Size(234, 184);
            this.headlineListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineListBox_KeyDown);
            // 
            // headlineContextMenu
            // 
            this.headlineContextMenu.MenuItems.Add(this.playMenuItem);
            this.headlineContextMenu.MenuItems.Add(this.browserMenuItem);
            this.headlineContextMenu.MenuItems.Add(this.channelPropertyMenuItem);
            this.headlineContextMenu.Popup += new System.EventHandler(this.HeadlineContextMenu_Popup);
            // 
            // playMenuItem
            // 
            this.playMenuItem.Text = "�Đ�(&P)";
            this.playMenuItem.Click += new System.EventHandler(this.PlayMenuItem_Click);
            // 
            // browserMenuItem
            // 
            this.browserMenuItem.Text = "�u���E�U�ŃA�N�Z�X(&A)";
            this.browserMenuItem.Click += new System.EventHandler(this.BrowserMenuItem_Click);
            // 
            // channelPropertyMenuItem
            // 
            this.channelPropertyMenuItem.Text = "�ԑg�̏ڍ�(&R)";
            this.channelPropertyMenuItem.Click += new System.EventHandler(this.ChannelPropertyMenuItem_Click);
            // 
            // getButton
            // 
            this.getButton.Location = new System.Drawing.Point(3, 3);
            this.getButton.Size = new System.Drawing.Size(72, 20);
            this.getButton.Text = "&Get";
            this.getButton.Click += new System.EventHandler(this.GetButton_Click);
            // 
            // filterCheckBox
            // 
            this.filterCheckBox.Location = new System.Drawing.Point(181, 3);
            this.filterCheckBox.Size = new System.Drawing.Size(56, 20);
            this.filterCheckBox.Text = "&Filter";
            this.filterCheckBox.CheckStateChanged += new System.EventHandler(this.FilterCheckBox_CheckStateChanged);
            // 
            // infomationLabel
            // 
            this.infomationLabel.Location = new System.Drawing.Point(3, 247);
            this.infomationLabel.Size = new System.Drawing.Size(234, 16);
            this.infomationLabel.Text = "No check - 0 CHs";
            this.infomationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // headlineCheckTimer
            // 
            this.headlineCheckTimer.Tick += new System.EventHandler(this.HeadlineCheckTimer_Tick);
            // 
            // stationListComboBox
            // 
            this.stationListComboBox.Location = new System.Drawing.Point(3, 29);
            this.stationListComboBox.Size = new System.Drawing.Size(234, 22);
            this.stationListComboBox.SelectedIndexChanged += new System.EventHandler(this.StationListComboBox_SelectedIndexChanged);
            // 
            // mainForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.stationListComboBox);
            this.Controls.Add(this.infomationLabel);
            this.Controls.Add(this.filterCheckBox);
            this.Controls.Add(this.getButton);
            this.Controls.Add(this.headlineListBox);
            this.Controls.Add(this.playButton);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "PocketLadio";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineListBox_KeyDown);
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
        /// <param name="channels">�ԑg�̔z��</param>
        private void UpdateRadioList(IChannel[] channels)
        {
            // ��������ԑg���X�g����ʂ������
            headlineListBox.Visible = false;

            headlineListBox.Items.Clear();
            foreach (IChannel channel in channels)
            {
                headlineListBox.Items.Add(channel.GetChannelView());
            }

            // �ԑg���X�g��`�悷��
            headlineListBox.Visible = true;
        }

        /// <summary>
        /// �X�g���[�~���O���Đ�����
        /// </summary>
        private void PlayStreaming()
        {
            try
            {
                if (headlineListBox.SelectedIndex != -1)
                {
                    PocketLadioUtility.PlayStreaming(StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetPlayUrl());
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("���f�B�A�v���C���[��������܂���", "�x��");
            }
        }

        /// <summary>
        /// Web�T�C�g�փA�N�Z�X����
        /// </summary>
        private void AccessWebSite()
        {
            try
            {
                if (headlineListBox.SelectedIndex != -1)
                {
                    if (StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetWebsiteUrl() != null)
                    {
                        PocketLadioUtility.AccessWebsite(StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetWebsiteUrl());
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("�u���E�U��������܂���", "�x��");
            }
        }

        /// <summary>
        /// �w�b�h���C�����擾�A�\������
        /// </summary>
        private void CheckHeadline()
        {
            // CheckHeadline()�������̏ꍇ�͉��������I��
            if (checkHeadlineNowFlag == true)
            {
                return;
            }

            // �r�������̂��߂̃t���O�𗧂Ă�
            checkHeadlineNowFlag = true;

            // �����ǑI���{�b�N�X���I���\�������t���O
            bool StationListComboBoxEnabledFlag = false;

            try
            {
                /** UI�O���� **/
                // Get�{�^����Filter�`�F�b�N�{�b�N�X����������I��s�ɂ���
                getButton.Enabled = false;
                filterCheckBox.Enabled = false;

                // �����ǑI���{�b�N�X���I���\�������ꍇ�ɂ̂݁A��������I��s�ɂ���
                // �i�����ǂ��ЂƂ��ݒ肳��Ă��Ȃ��ꍇ�ɂ́A���X�I��s�̂��߁j
                if (stationListComboBox.Enabled == true)
                {
                    stationListComboBox.Enabled = false;
                    // �����ǑI���{�b�N�X���I���\�������t���O�𗧂Ă�
                    StationListComboBoxEnabledFlag = true;
                }

                /** �ԑg�擾���� **/
                // �ԑg���擾����
                StationList.WebGetHeadlineOfCurrentStation();

                // �ԑg���擾�ł��Ȃ������ꍇ
                if (StationList.LastCheckTimeOfCurrentStation.Equals(DateTime.MinValue))
                {
                    infomationLabel.Text = "No Check - 0 CHs";
                }
                // �ԑg���擾�ł����ꍇ
                else
                {
                    infomationLabel.Text = "Last " + StationList.LastCheckTimeOfCurrentStation.ToString()
                        + " - " + StationList.GetChannelsOfCurrentStation().Length.ToString() + " CHs";
                }

                // �ԑg���X�g���X�V����
                UpdateRadioList(StationList.GetChannelsFilteredOfCurrentStation());
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
            catch (UriFormatException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.HeadlineNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            catch (SocketException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("�ԑg�\���擾�ł��܂���ł���", "�l�b�g���[�N�G���[");
            }
            catch (NotSupportedException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.HeadlineNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            catch (XmlException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("XML�`���̃w�b�h���C��������ɏ����ł��܂���ł���", "XML�G���[");
            }
            catch (ArgumentException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.HeadlineNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            finally
            {
                /** UI�㏈�� **/
                // Get�{�^����Filter�`�F�b�N�{�b�N�X��I���\�ɉ񕜂���
                getButton.Enabled = true;
                filterCheckBox.Enabled = true;

                // �����ǑI���{�b�N�X���I���\�������ꍇ�ɂ̂݁A�I���\�ɉ񕜂���
                // �i�����ǂ��ЂƂ��ݒ肳��Ă��Ȃ��ꍇ�ɂ́A���X�I��s�̂��߁j
                if (StationListComboBoxEnabledFlag == true)
                {
                    stationListComboBox.Enabled = true;
                }

                // �r�������̂��߂̃t���O��������
                checkHeadlineNowFlag = false;
            }
        }

        /// <summary>
        /// �^�C�}�[�̃X�^�[�g���̏���
        /// </summary>
        private void HeadlineCheckTimerStart()
        {
            UserSetting.HeadlineTimerCheck = true;
            headlineCheckTimerMenuItem.Checked = true;
            headlineCheckTimer.Interval = UserSetting.HeadlineTimerMillSecond;
            headlineCheckTimer.Enabled = true;
        }

        /// <summary>
        /// �^�C�}�[�̃X�g�b�v���̏���
        /// </summary>
        private void HeadlineCheckTimerStop()
        {
            UserSetting.HeadlineTimerCheck = false;
            headlineCheckTimerMenuItem.Checked = false;
            headlineCheckTimer.Enabled = false;
        }

        /// <summary>
        /// �^�C�}�[�̃C���^�[�o�����ύX���ꂽ�Ƃ��̏���
        /// </summary>
        /// <param name="interval">�^�C�}�[�̃C���^�[�o��</param>
        public void HeadlineTimerIntervalChange(int interval)
        {
            if (UserSetting.HeadlineTimerCheck == true)
            {
                headlineCheckTimer.Enabled = false;
                headlineCheckTimer.Interval = interval;
                headlineCheckTimer.Enabled = true;
            }
            else
            {
                headlineCheckTimer.Interval = interval;
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
            this.getButton.Location = new System.Drawing.Point(3, 3);
            this.playButton.Location = new System.Drawing.Point(81, 3);
            this.filterCheckBox.Location = new System.Drawing.Point(181, 3);
            this.headlineListBox.Location = new System.Drawing.Point(3, 60);
            this.headlineListBox.Size = new System.Drawing.Size(234, 184);
            this.stationListComboBox.Location = new System.Drawing.Point(3, 29);
            this.stationListComboBox.Size = new System.Drawing.Size(234, 22);
            this.infomationLabel.Location = new System.Drawing.Point(3, 247);
            this.infomationLabel.Size = new System.Drawing.Size(234, 16);
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����i�����j
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.getButton.Location = new System.Drawing.Point(3, 3);
            this.playButton.Location = new System.Drawing.Point(81, 3);
            this.filterCheckBox.Location = new System.Drawing.Point(261, 3);
            this.headlineListBox.Location = new System.Drawing.Point(3, 60);
            this.headlineListBox.Size = new System.Drawing.Size(314, 100);
            this.stationListComboBox.Location = new System.Drawing.Point(3, 29);
            this.stationListComboBox.Size = new System.Drawing.Size(314, 22);
            this.infomationLabel.Location = new System.Drawing.Point(3, 163);
            this.infomationLabel.Size = new System.Drawing.Size(314, 16);
        }

        /// <summary>
        /// �����Ǐ����������p��MenuItem�N���X
        /// </summary>
        class StationMenuItem : MenuItem
        {
            private Station station;

            public Station GetStation()
            {
                return station;
            }

            public void SetStation(Station station)
            {
                this.station = station;
            }
        }

        /// <summary>
        /// �����ǂ��Ƃ̐ݒ���N���b�N
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EachStationSettingMenuItem_Click(object sender, System.EventArgs e)
        {
            StationList.ShowSettingForm(((StationMenuItem)sender).GetStation());
        }

        private void HeadlineContextMenu_Popup(object sender, System.EventArgs e)
        {
            if (headlineListBox.SelectedIndex == -1)
            {
                channelPropertyMenuItem.Enabled = false;
                playMenuItem.Enabled = false;
                browserMenuItem.Enabled = false;
            }
            else
            {
                channelPropertyMenuItem.Enabled = true;
                playMenuItem.Enabled = true;
                browserMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// �e�����ǂ̐ݒ胁�j���[�Ɛ؂�ւ��{�b�N�X�̒ǉ�
        /// </summary>
        private void StationsSettingAndCheckBoxAdd()
        {
            // �����ǂ����݂���ꍇ�̏���
            // �e�����ǂ̐ݒ胁�j���[�ǉ��Ɛ؂�ւ��{�b�N�X�ǉ����s��
            if (StationList.GetStationList().Length != 0)
            {
                // �e�����ǂ̐ݒ胁�j���[��ǉ�����
                {
                    // �e�����ǂ̐ݒ胁�j���[����������I��s�ɂ���
                    this.stationSettingMenuItem.Enabled = false;
                    // �e�����ǂ̐ݒ胁�j���[����������N���A����
                    this.stationSettingMenuItem.MenuItems.Clear();
                    // �e�����ǂ̐ݒ胁�j���[��ǉ�
                    foreach (Station station in StationList.GetStationList())
                    {
                        StationMenuItem EachStationSettingMenuItem = new StationMenuItem();
                        this.stationSettingMenuItem.MenuItems.Add(EachStationSettingMenuItem);
                        EachStationSettingMenuItem.Text = station.DisplayName + " �ݒ�";
                        EachStationSettingMenuItem.SetStation(station);
                        EachStationSettingMenuItem.Click += new System.EventHandler(this.EachStationSettingMenuItem_Click);
                    }
                    // �ݒ胁�j���[���ǉ����I������̂ŁA�e�����ǂ̐ݒ胁�j���[��I���\�ɂ���
                    this.stationSettingMenuItem.Enabled = true;
                }

                // �e�����ǂ̐؂�ւ��{�b�N�X��ǉ�����
                {
                    // �e�����ǂ̐؂�ւ��{�b�N�X����������I��s�ɂ���
                    this.stationListComboBox.Enabled = false;
                    // �e�����ǂ̐؂�ւ��{�b�N�X����������N���A����
                    this.stationListComboBox.Items.Clear();
                    // �e�����ǂ̐؂�ւ��{�b�N�X�̒ǉ�
                    foreach (Station station in StationList.GetStationList())
                    {
                        this.stationListComboBox.Items.Add(station.DisplayName);
                    }
                    // �؂�ւ��{�b�N�X���ǉ����I������̂ŁA�e�����ǂ̐؂�ւ��{�b�N�X��I���\�ɂ���
                    this.stationListComboBox.Enabled = true;
                }

                // �ȑO�ɑI������Ă��������ǂ�I��������
                for (int count = 0; count < stationListComboBox.Items.Count; ++count)
                {
                    if (StationList.GetStationList()[count].Id == selectedStationID)
                    {
                        this.stationListComboBox.SelectedIndex = count;
                        break;
                    }
                }

                // �����ǂ��I������Ă��炸�A�������ǂ�����ꍇ
                if (this.stationListComboBox.SelectedIndex == -1 && this.stationListComboBox.Items.Count > 0)
                {
                    // �g�b�v�̕����ǂ�I��
                    this.stationListComboBox.SelectedIndex = 0;
                }
            }
            // �����ǂ����݂��Ȃ��ꍇ�̏���
            // �����ǂ̐ݒ胁�j���[�Ɛ؂�ւ��{�b�N�X��I��s�ɂ���
            else
            {
                // �e�����ǂ̐ݒ胁�j���[��I��s�ɂ���
                this.stationSettingMenuItem.Enabled = false;
                // �e�����ǂ̐ݒ胁�j���[���N���A����
                this.stationSettingMenuItem.MenuItems.Clear();

                // �e�����ǂ̐؂�ւ��{�b�N�X��I��s�ɂ���
                this.stationListComboBox.Enabled = false;
                // �e�����ǂ̐؂�ւ��{�b�N�X���N���A����
                this.stationListComboBox.Items.Clear();
            }
        }

        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void VersionInfoMenuItem_Click(object sender, System.EventArgs e)
        {
            VersionInfoForm versionInfoForm = new VersionInfoForm();
            versionInfoForm.ShowDialog();
            versionInfoForm.Dispose();
        }

        private void FilterSettingMenuItem_Click(object sender, System.EventArgs e)
        {
            FilterSettingForm filterSettingForm = new FilterSettingForm();
            filterSettingForm.ShowDialog();
            filterSettingForm.Dispose();
        }

        private void GetButton_Click(object sender, System.EventArgs e)
        {
            CheckHeadline();
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            PlayStreaming();
        }

        private void FilterCheckBox_CheckStateChanged(object sender, System.EventArgs e)
        {
            /** UI�O���� **/
            // Filter�`�F�b�N�{�b�N�X����������I��s�ɂ���
            filterCheckBox.Enabled = false;

            /** �t�B���^�����O���� **/
            if (filterCheckBox.Checked == true)
            {
                StationList.FilterEnable = true;
            }
            else
            {
                StationList.FilterEnable = false;
            }
            UpdateRadioList(StationList.GetChannelsFilteredOfCurrentStation());

            /** UI�㏈�� **/
            // Filter�`�F�b�N�{�b�N�X��I���\�ɉ񕜂���
            filterCheckBox.Enabled = true;
        }

        private void ChannelPropertyMenuItem_Click(object sender, System.EventArgs e)
        {
            if (headlineListBox.SelectedIndex != -1 && StationList.GetChannelsFilteredOfCurrentStation().Length > 0)
            {
                StationList.ShowPropertyFormOfCurrentStation(StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex]);
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                // �N�����̏�����
                PocketLadioSpecificProcess.StartUpInitialize();

                // �w�b�h���C�����L���ȏꍇ�A�w�b�h���C���𓮍삳����
                if (UserSetting.HeadlineTimerCheck == true)
                {
                    HeadlineCheckTimerStart();
                }
                else
                {
                    HeadlineCheckTimerStop();
                }

                StationsSettingAndCheckBoxAdd();

                // �ԑg���X�g������ꍇ
                if (StationList.GetChannelsFilteredOfCurrentStation().Length > 0)
                {
                    UpdateRadioList(StationList.GetChannelsFilteredOfCurrentStation());
                }
            }
            catch (XmlException)
            {
                MessageBox.Show("�ݒ�t�@�C�����ǂݍ��߂܂���ł���", "�ݒ�t�@�C���̉�̓G���[");
            }
            catch (IOException)
            {
                MessageBox.Show("�ݒ�t�@�C�����ǂݍ��߂܂���ł���", "�ݒ�t�@�C���̓ǂݍ��݃G���[");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("�ݒ�t�@�C�����ǂݍ��߂܂���ł���", "�ݒ�t�@�C���̓ǂݍ��݃G���[");
            }

            FixWindowSize();
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // �I��������
                PocketLadioSpecificProcess.ExitDisable();
            }
            catch (IOException)
            {
                MessageBox.Show("�ݒ�t�@�C�����������߂܂���ł���", "�ݒ�t�@�C���������݃G���[");
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
            CheckHeadline();
        }

        private void PocketLadioSettingMenuItem_Click(object sender, System.EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
            settingForm.Dispose();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

        private void HeadlineListBox_KeyDown(object sender, KeyEventArgs e)
        {
            // ���̓{�^�����������Ƃ�
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                PlayStreaming();
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            headlineCheckTimer.Interval = UserSetting.HeadlineTimerMillSecond;

            StationsSettingAndCheckBoxAdd();
        }

        private void StationListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StationList.ChangeCurrentStationAt(stationListComboBox.SelectedIndex);
            if (StationList.LastCheckTimeOfCurrentStation.Equals(DateTime.MinValue))
            {
                infomationLabel.Text = "No Check - 0 CHs";
            }
            else
            {
                infomationLabel.Text = "Last " + StationList.LastCheckTimeOfCurrentStation.ToString() + " - " + StationList.GetChannelsOfCurrentStation().Length.ToString() + " CHs";
            }
            UpdateRadioList(StationList.GetChannelsFilteredOfCurrentStation());

            // �I�����Ă��������ǂ��L������
            if (stationListComboBox.SelectedIndex != -1)
            {
                selectedStationID = StationList.HeadlineIdOfCurrentStation;
            }
        }

        private void StationsSettingMenuItem_Click(object sender, EventArgs e)
        {
            StationsSettingForm stationSettingForm = new StationsSettingForm();
            stationSettingForm.ShowDialog();
            stationSettingForm.Dispose();
        }
    }
}
