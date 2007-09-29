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
using System.Diagnostics;
using PocketLadio.Stations;
using MiscPocketCompactLibrary.Net;
using MiscPocketCompactLibrary.Reflection;
using MiscPocketCompactLibrary.Diagnostics;
using MiscPocketCompactLibrary.Windows.Forms;

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
        private MenuItem stationSettingMenuItem;
        private MenuItem versionInfoMenuItem;
        private MenuItem separateMenuItem1;
        private MenuItem exitMenuItem;
        private Button playButton;
        private ListBox headlineListBox;
        private Button updateButton;
        private CheckBox filterCheckBox;
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
        private string selectedStationID = string.Empty;

        /// <summary>
        /// CheckHeadline()�̓���r�������̂��߂̃t���O
        /// </summary>
        private bool checkHeadlineNowFlag;
        private StatusBar mainStatusBar;

        /// <summary>
        /// �A���J�[�R���g���[���̃��X�g
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

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
            this.updateButton = new System.Windows.Forms.Button();
            this.filterCheckBox = new System.Windows.Forms.CheckBox();
            this.headlineCheckTimer = new System.Windows.Forms.Timer();
            this.stationListComboBox = new System.Windows.Forms.ComboBox();
            this.mainStatusBar = new System.Windows.Forms.StatusBar();
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
            this.browserMenuItem.Text = "Web�T�C�g�ɃA�N�Z�X(&A)";
            this.browserMenuItem.Click += new System.EventHandler(this.BrowserMenuItem_Click);
            // 
            // channelPropertyMenuItem
            // 
            this.channelPropertyMenuItem.Text = "�ԑg�̏ڍ�(&R)";
            this.channelPropertyMenuItem.Click += new System.EventHandler(this.ChannelPropertyMenuItem_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(3, 3);
            this.updateButton.Size = new System.Drawing.Size(72, 20);
            this.updateButton.Text = "&Update";
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // filterCheckBox
            // 
            this.filterCheckBox.Location = new System.Drawing.Point(181, 3);
            this.filterCheckBox.Size = new System.Drawing.Size(56, 20);
            this.filterCheckBox.Text = "&Filter";
            this.filterCheckBox.CheckStateChanged += new System.EventHandler(this.FilterCheckBox_CheckStateChanged);
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
            // mainStatusBar
            // 
            this.mainStatusBar.Location = new System.Drawing.Point(0, 246);
            this.mainStatusBar.Size = new System.Drawing.Size(240, 22);
            this.mainStatusBar.Text = "No check - 0 CHs";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.stationListComboBox);
            this.Controls.Add(this.filterCheckBox);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.headlineListBox);
            this.Controls.Add(this.playButton);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "PocketLadio";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineListBox_KeyDown);
            this.Load += new System.EventHandler(this.MainForm_Load);

        }
        #endregion

        /// <summary>
        /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
        /// </summary>

        static void Main()
        {
            try
            {
                Application.Run(new MainForm());

                // �I������
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
            catch (Exception ex)
            {
                // ���O�ɗ�O������������
                Log exceptionLog = new Log(AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.ExceptionLogFile);
                string logContents = PocketLadioInfo.VersionNumber + "\r\n" + ex.Message + "\r\n" + ex.ToString();
                exceptionLog.LogThis(logContents, Log.LogPrefix.date);

                Trace.Assert(false, "�\�����Ȃ��G���[�������������߁A�I�����܂�");
#if DEBUG
                // �f�o�b�K�ŗ�O���e���m�F���邽�߁A��O���A�v���P�[�V�����̊O�ɏo��
                throw ex;
#endif // DEBUG
            }
        }

        /// <summary>
        /// �ԑg���X�g���X�V����
        /// </summary>
        /// <param name="channels">�ԑg�̔z��</param>
        private void DrawChannelList(IChannel[] channels)
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
            if (headlineListBox.SelectedIndex != -1)
            {
                Uri playUrl = StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetPlayUrl();

                // URL����̏ꍇ�́A�x�����o���ďI��
                if (playUrl == null)
                {
                    MessageBox.Show("�ԑg��URL������܂���", "�Đ��G���[");
                    return;
                }

                try
                {
                    if (UserSetting.PlayListSave == false)
                    {
                        PocketLadioUtility.PlayStreaming(playUrl);
                    }
                    // �ԑg���v���C���X�g�������ꍇ�ɁA��[���[�J���ɕۑ�����
                    else
                    {
                        string playListExtension = Path.GetExtension(playUrl.AbsolutePath);

                        if (IsPlayListExtension(playListExtension) == true)
                        {
                            // �����̃v���C���X�g���폜
                            foreach (string extension in PocketLadioInfo.PlayListExtensions)
                            {
                                if (File.Exists(AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.GeneratePlayListFileName + extension))
                                {
                                    File.Delete(AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.GeneratePlayListFileName + extension);
                                }
                            }

                            // ��[�v���C���X�g�����[�J���ɕۑ����āA������v���[���[�ɓn���B
                            string playListPath
                                = AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.GeneratePlayListFileName + playListExtension;
                            PocketLadioUtility.FetchFile(playUrl, playListPath);
                            PocketLadioUtility.PlayStreaming(playListPath);
                        }
                        else
                        {
                            PocketLadioUtility.PlayStreaming(playUrl);
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("���f�B�A�v���C���[��������܂���", "�x��");
                }
                catch (WebException)
                {
                    MessageBox.Show(playUrl + " ��������܂���", "�x��");
                }
            }
        }

        /// <summary>
        /// �g���q���v���C���X�g���𔻒肷��
        /// </summary>
        /// <param name="extension">�g���q</param>
        /// <returns>�v���C���X�g�������ꍇ��true�A�����łȂ��ꍇ��false</returns>
        private bool IsPlayListExtension(string extension)
        {
            // �v���C���X�g���𔻒�
            foreach (string playListExtension in PocketLadioInfo.PlayListExtensions)
            {
                if (playListExtension == extension)
                {
                    return true;
                }
            }

            return false;
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

            #region UI�O����

            // �t�H�[�����R���g���[������������I��s�ɂ���
            updateButton.Enabled = false;
            playButton.Enabled = false;
            filterCheckBox.Enabled = false;
            stationListComboBox.Enabled = false;
            headlineListBox.Enabled = false;

            #endregion

            mainStatusBar.Text = string.Format("�ڑ���...");
            mainStatusBar.Refresh();

            try
            {
                #region �ԑg�擾����

                // �ԑg���擾����
                StationList.FetchHeadlineOfCurrentStation();

                // �ԑg���擾�ł��Ȃ������ꍇ
                if (StationList.LastCheckTimeOfCurrentStation.Equals(DateTime.MinValue))
                {
                    mainStatusBar.Text = "No Check - 0 CHs";
                }
                // �ԑg���擾�ł����ꍇ
                else
                {
                    mainStatusBar.Text = "Last " + StationList.LastCheckTimeOfCurrentStation.ToString()
                        + " - " + StationList.GetChannelsOfCurrentStation().Length.ToString() + " CHs";
                }

                #endregion

                // �ԑg���X�g���X�V����
                DrawChannelList(StationList.GetChannelsFilteredOfCurrentStation());
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
                MessageBox.Show(StationList.StationNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            catch (SocketException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("�ԑg�\���擾�ł��܂���ł���", "�l�b�g���[�N�G���[");
            }
            catch (NotSupportedException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.StationNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            catch (XmlException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("XML�`���̃w�b�h���C��������ɏ����ł��܂���ł���", "XML�G���[");
            }
            catch (ArgumentException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.StationNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            finally
            {
                #region  UI�㏈��

                // �t�H�[�����R���g���[����I���\�ɉ񕜂���
                updateButton.Enabled = true;
                playButton.Enabled = true;
                filterCheckBox.Enabled = true;
                stationListComboBox.Enabled = true;
                headlineListBox.Enabled = true;

                #endregion

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
        /// �R���g���[���ɃA���J�[���Z�b�g����
        /// </summary>
        private void SetAnchorControl()
        {
            anchorControlList.Add(new AnchorLayout(updateButton, AnchorStyles.Top | AnchorStyles.Left, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(playButton, AnchorStyles.Top | AnchorStyles.Left, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(filterCheckBox, AnchorStyles.Top | AnchorStyles.Right, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(stationListComboBox, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(headlineListBox, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
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
                if (StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetPlayUrl() != null)
                {
                    channelPropertyMenuItem.Enabled = true;
                }
                else
                {
                    channelPropertyMenuItem.Enabled = false;
                }
                playMenuItem.Enabled = true;
                if (StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetWebsiteUrl() != null)
                {
                    browserMenuItem.Enabled = true;
                }
                else
                {
                    browserMenuItem.Enabled = false;
                }
            }
        }

        /// <summary>
        /// �e�����ǂ̐ݒ胁�j���[�Ɛ؂�ւ��{�b�N�X�̒ǉ�
        /// </summary>
        private void AddStationsSettingAndComboBoxItem()
        {
            // �����ǂ����݂���ꍇ�̏���
            // �e�����ǂ̐ݒ胁�j���[�ǉ��Ɛ؂�ւ��{�b�N�X�ǉ����s��
            if (StationList.GetStationList().Length != 0)
            {
                #region �e�����ǂ̐ݒ胁�j���[��ǉ�����

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

                #endregion

                #region  �e�����ǂ̐؂�ւ��{�b�N�X��ǉ�����

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

                #endregion

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
            Close();
            Application.Exit();
        }

        private void VersionInfoMenuItem_Click(object sender, System.EventArgs e)
        {
            VersionInfoForm versionInfoForm = new VersionInfoForm();
            versionInfoForm.ShowDialog();
            versionInfoForm.Dispose();
        }

        private void UpdateButton_Click(object sender, System.EventArgs e)
        {
            CheckHeadline();
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            PlayStreaming();
        }

        private void FilterCheckBox_CheckStateChanged(object sender, System.EventArgs e)
        {
            #region UI�O����

            // �t�H�[������������I��s�ɂ���
            this.Enabled = false;

            #endregion

            #region �t�B���^�����O����

            if (filterCheckBox.Checked == true)
            {
                StationList.FilterEnable = true;
                // �t�B���^�[�̗L�������ݒ���I���ɂ���
                UserSetting.FilterEnable = true;
            }
            else
            {
                StationList.FilterEnable = false;
                // �t�B���^�[�̗L�������ݒ���I�t�ɂ���
                UserSetting.FilterEnable = false;
            }
            DrawChannelList(StationList.GetChannelsFilteredOfCurrentStation());

            #endregion

            #region UI�㏈��

            // �t�H�[����I���\�ɉ񕜂���
            this.Enabled = true;

            #endregion
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
                // �N�����̃`�F�b�N
                PocketLadioSpecificProcess.StartUpCheck();
            }
            catch (DllNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();

                return;
            }

            try
            {
                // �N�����̏�����
                PocketLadioSpecificProcess.StartUpInitialize();

                // �w�b�h���C���^�C�}�[�̐ݒ肪�L���ȏꍇ�A�^�C�}�[�𓮍삳����
                if (UserSetting.HeadlineTimerCheck == true)
                {
                    HeadlineCheckTimerStart();
                }
                else
                {
                    HeadlineCheckTimerStop();
                }

                AddStationsSettingAndComboBoxItem();

                // �ԑg���X�g������ꍇ
                if (StationList.GetChannelsFilteredOfCurrentStation().Length > 0)
                {
                    DrawChannelList(StationList.GetChannelsFilteredOfCurrentStation());
                }

                // �t�B���^�[�̗L�������ݒ肪�L���ȏꍇ�A�t�B���^�[�𓮍삳����
                if (UserSetting.FilterEnable == true)
                {
                    filterCheckBox.Checked = true;
                }
                else
                {
                    filterCheckBox.Checked = false;
                }

                StationList.HeadlineFetching += new FetchEventHandler(StationList_HeadlineFetching);
                StationList.HeadlineAnalyzing += new HeadlineAnalyzeEventHandler(StationList_HeadlineAnalyzing);
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

            SetAnchorControl();
            FixWindowSize();
        }

        void StationList_HeadlineFetching(object sender, FetchEventArgs e)
        {
            if (e.IsUnknownContentSize == false)
            {
                mainStatusBar.Text = string.Format("�w�b�h���C���擾 {0}KB / {1}KB", e.FetchedSize / 1024, e.ContentSize / 1024);
            }
            else
            {
                mainStatusBar.Text = string.Format("�w�b�h���C���擾 {0}KB", e.FetchedSize / 1024);
            }
            mainStatusBar.Refresh();
        }

        void StationList_HeadlineAnalyzing(object sender, HeadlineAnalyzeEventArgs e)
        {
            if (e.IsUnknownWholeCount == false)
            {
                mainStatusBar.Text = string.Format("�ԑg��͒� {0} / {1}", e.AnalyzedCount, e.WholeCount);
            }
            else
            {
                mainStatusBar.Text = string.Format("�ԑg��͒� {0}", e.AnalyzedCount);
            }
            mainStatusBar.Refresh();
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
            HeadlineTimerIntervalChange(UserSetting.HeadlineTimerMillSecond);

            AddStationsSettingAndComboBoxItem();
        }

        private void StationListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region UI�O����

            // �t�H�[�����R���g���[������������I��s�ɂ���
            updateButton.Enabled = false;
            playButton.Enabled = false;
            filterCheckBox.Enabled = false;
            stationListComboBox.Enabled = false;
            headlineListBox.Enabled = false;

            #endregion

            #region �؂�ւ�����

            StationList.ChangeCurrentStationAt(stationListComboBox.SelectedIndex);
            if (StationList.LastCheckTimeOfCurrentStation.Equals(DateTime.MinValue))
            {
                mainStatusBar.Text = "No Check - 0 CHs";
            }
            else
            {
                mainStatusBar.Text = "Last " + StationList.LastCheckTimeOfCurrentStation.ToString() + " - " + StationList.GetChannelsOfCurrentStation().Length.ToString() + " CHs";
            }
            DrawChannelList(StationList.GetChannelsFilteredOfCurrentStation());

            // �I�����Ă��������ǂ��L������
            if (stationListComboBox.SelectedIndex != -1)
            {
                selectedStationID = StationList.StationIdOfCurrentStation;
            }

            #endregion

            #region UI�㏈��

            // �t�H�[�����R���g���[����I���\�ɉ񕜂���
            updateButton.Enabled = true;
            playButton.Enabled = true;
            filterCheckBox.Enabled = true;
            stationListComboBox.Enabled = true;
            headlineListBox.Enabled = true;

            #endregion
        }

        private void StationsSettingMenuItem_Click(object sender, EventArgs e)
        {
            StationsSettingForm stationSettingForm = new StationsSettingForm();
            stationSettingForm.ShowDialog();
            stationSettingForm.Dispose();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (UserSetting.HeadlineListBoxFontSizeChange == true)
            {
                headlineListBox.Font = new Font(headlineListBox.Font.Name, UserSetting.HeadlineListBoxFontSize, headlineListBox.Font.Style);
            }
            else
            {
                headlineListBox.Font = new Font(headlineListBox.Font.Name, PocketLadioInfo.HeadlineListBoxDefaultFontSize, headlineListBox.Font.Style);
            }
        }
    }
}
