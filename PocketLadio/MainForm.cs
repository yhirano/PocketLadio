#region �f�B���N�e�B�u���g�p����

using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
        /// <summary>
        /// �I������Ă��������ǂ�ID
        /// </summary>
        private string selectedStationID = string.Empty;

        /// <summary>
        /// CheckHeadline()�̓���r�������̂��߂̃t���O
        /// </summary>
        private bool checkHeadlineNowFlag;

        /// <summary>
        /// �A���J�[�R���g���[���̃��X�g
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

        /// <summary>
        /// �t�B���^�����O����
        /// </summary>
        private bool isFiltering;

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
        private StatusBar mainStatusBar;
        private MenuItem channelMenuItem;
        private MenuItem playOfChannelMenuItem;
        private MenuItem browserOfChannelMenuItem;
        private MenuItem channelPropertyOfChannelMenuItem;
        private Label headlineInfomationLabel;
        private MenuItem separateMenuItem5;
        private MenuItem updateMenuItem;
        private MenuItem separateMenuItem6;
        private MenuItem selectStationMenuItem;
        private MenuItem selectChannelMenuItem;
        private MenuItem separateMenuItem7;
        private MenuItem addPlayUrlToFilterMenuItem;
        private MenuItem addFilterMenuItem;
        private MenuItem separateMenuItem8;
        private MenuItem filterMenuItem;

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
            this.channelMenuItem = new System.Windows.Forms.MenuItem();
            this.playOfChannelMenuItem = new System.Windows.Forms.MenuItem();
            this.browserOfChannelMenuItem = new System.Windows.Forms.MenuItem();
            this.channelPropertyOfChannelMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem5 = new System.Windows.Forms.MenuItem();
            this.updateMenuItem = new System.Windows.Forms.MenuItem();
            this.filterMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem6 = new System.Windows.Forms.MenuItem();
            this.selectStationMenuItem = new System.Windows.Forms.MenuItem();
            this.selectChannelMenuItem = new System.Windows.Forms.MenuItem();
            this.playButton = new System.Windows.Forms.Button();
            this.headlineListBox = new System.Windows.Forms.ListBox();
            this.headlineContextMenu = new System.Windows.Forms.ContextMenu();
            this.playMenuItem = new System.Windows.Forms.MenuItem();
            this.browserMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem7 = new System.Windows.Forms.MenuItem();
            this.addFilterMenuItem = new System.Windows.Forms.MenuItem();
            this.addPlayUrlToFilterMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem8 = new System.Windows.Forms.MenuItem();
            this.channelPropertyMenuItem = new System.Windows.Forms.MenuItem();
            this.updateButton = new System.Windows.Forms.Button();
            this.filterCheckBox = new System.Windows.Forms.CheckBox();
            this.headlineCheckTimer = new System.Windows.Forms.Timer();
            this.stationListComboBox = new System.Windows.Forms.ComboBox();
            this.mainStatusBar = new System.Windows.Forms.StatusBar();
            this.headlineInfomationLabel = new System.Windows.Forms.Label();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuMenuItem);
            this.mainMenu.MenuItems.Add(this.channelMenuItem);
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
            // channelMenuItem
            // 
            this.channelMenuItem.MenuItems.Add(this.playOfChannelMenuItem);
            this.channelMenuItem.MenuItems.Add(this.browserOfChannelMenuItem);
            this.channelMenuItem.MenuItems.Add(this.channelPropertyOfChannelMenuItem);
            this.channelMenuItem.MenuItems.Add(this.separateMenuItem5);
            this.channelMenuItem.MenuItems.Add(this.updateMenuItem);
            this.channelMenuItem.MenuItems.Add(this.filterMenuItem);
            this.channelMenuItem.MenuItems.Add(this.separateMenuItem6);
            this.channelMenuItem.MenuItems.Add(this.selectStationMenuItem);
            this.channelMenuItem.MenuItems.Add(this.selectChannelMenuItem);
            this.channelMenuItem.Text = "�ԑg(&C)";
            this.channelMenuItem.Popup += new System.EventHandler(this.channelMenuItem_Popup);
            // 
            // playOfChannelMenuItem
            // 
            this.playOfChannelMenuItem.Text = "�Đ�(&P)";
            this.playOfChannelMenuItem.Click += new System.EventHandler(this.playOfChannelMenuItem_Click);
            // 
            // browserOfChannelMenuItem
            // 
            this.browserOfChannelMenuItem.Text = "Web�T�C�g�ɃA�N�Z�X(&A)";
            this.browserOfChannelMenuItem.Click += new System.EventHandler(this.browserOfChannelMenuItem_Click);
            // 
            // channelPropertyOfChannelMenuItem
            // 
            this.channelPropertyOfChannelMenuItem.Text = "�ԑg�̏ڍ�(&P)";
            this.channelPropertyOfChannelMenuItem.Click += new System.EventHandler(this.channelPropertyOfChannelMenuItem_Click);
            // 
            // separateMenuItem5
            // 
            this.separateMenuItem5.Text = "-";
            // 
            // updateMenuItem
            // 
            this.updateMenuItem.Text = "�w�b�h���C���X�V(&U)";
            this.updateMenuItem.Click += new System.EventHandler(this.updateMenuItem_Click);
            // 
            // filterMenuItem
            // 
            this.filterMenuItem.Text = "�t�B���^�[(&F)";
            this.filterMenuItem.Click += new System.EventHandler(this.filterMenuItem_Click);
            // 
            // separateMenuItem6
            // 
            this.separateMenuItem6.Text = "-";
            // 
            // selectStationMenuItem
            // 
            this.selectStationMenuItem.Text = "�����ǂ̑I��(&S)";
            this.selectStationMenuItem.Click += new System.EventHandler(this.selectStationMenuItem_Click);
            // 
            // selectChannelMenuItem
            // 
            this.selectChannelMenuItem.Text = "�ԑg�̑I��(&C)";
            this.selectChannelMenuItem.Click += new System.EventHandler(this.selectChannelMenuItem_Click);
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
            this.headlineListBox.Location = new System.Drawing.Point(3, 57);
            this.headlineListBox.Size = new System.Drawing.Size(234, 142);
            this.headlineListBox.SelectedIndexChanged += new System.EventHandler(this.headlineListBox_SelectedIndexChanged);
            this.headlineListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineListBox_KeyDown);
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
            // separateMenuItem7
            // 
            this.separateMenuItem7.Text = "-";
            // 
            // addFilterMenuItem
            // 
            this.addFilterMenuItem.Text = "�t�B���^�[�ɓo�^(&F)";
            this.addFilterMenuItem.Click += new System.EventHandler(this.addFilterMenuItem_Click);
            // 
            // addPlayUrlToFilterMenuItem
            // 
            this.addPlayUrlToFilterMenuItem.Text = "�Đ�URL���t�B���^�[�ɓo�^(&I)";
            this.addPlayUrlToFilterMenuItem.Click += new System.EventHandler(this.addPlayUrlToFilterMenuItem_Click);
            // 
            // separateMenuItem8
            // 
            this.separateMenuItem8.Text = "-";
            // 
            // channelPropertyMenuItem
            // 
            this.channelPropertyMenuItem.Text = "�ԑg�̏ڍ�(&R)";
            this.channelPropertyMenuItem.Click += new System.EventHandler(this.ChannelPropertyMenuItem_Click);
            // 
            // headlineContextMenu
            // 
            this.headlineContextMenu.MenuItems.Add(this.playMenuItem);
            this.headlineContextMenu.MenuItems.Add(this.browserMenuItem);
            this.headlineContextMenu.MenuItems.Add(this.separateMenuItem7);
            this.headlineContextMenu.MenuItems.Add(this.addFilterMenuItem);
            this.headlineContextMenu.MenuItems.Add(this.addPlayUrlToFilterMenuItem);
            this.headlineContextMenu.MenuItems.Add(this.separateMenuItem8);
            this.headlineContextMenu.MenuItems.Add(this.channelPropertyMenuItem);
            this.headlineContextMenu.Popup += new System.EventHandler(this.HeadlineContextMenu_Popup);
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
            // headlineInfomationLabel
            // 
            this.headlineInfomationLabel.Location = new System.Drawing.Point(3, 202);
            this.headlineInfomationLabel.Size = new System.Drawing.Size(234, 41);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.headlineInfomationLabel);
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.stationListComboBox);
            this.Controls.Add(this.filterCheckBox);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.headlineListBox);
            this.Controls.Add(this.playButton);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "PocketLadio";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.Resize += new System.EventHandler(this.MainForm_Resize);

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
                StringBuilder error = new StringBuilder();

                error.Append("Application:       " +
                    PocketLadioInfo.ApplicationName + " " + PocketLadioInfo.VersionNumber + "\r\n");
                error.Append("Date:              " +
                    System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n");
                error.Append("OS:                " +
                    Environment.OSVersion.ToString() + "\r\n");
                error.Append("Culture:           " +
                    System.Globalization.CultureInfo.CurrentCulture.Name + "\r\n");
                error.Append("Exception class:   " +
                    ex.GetType().ToString() + "\r\n");
                error.Append("ToString:   " +
                    ex.ToString() + "\r\n");
                error.Append("Exception message: "
                     + "\r\n");
                error.Append(ex.Message);

                Exception innnerEx = ex.InnerException;
                while (innnerEx != null)
                {
                    error.Append(innnerEx.Message);
                    error.Append("\r\n");
                    innnerEx = innnerEx.InnerException;
                }

                error.Append("\r\n");
                error.Append("\r\n");

                exceptionLog.LogThis(error.ToString(), Log.LogPrefix.date);

#if DEBUG
                // �f�o�b�K�ŗ�O���e���m�F���邽�߁A��O���A�v���P�[�V�����̊O�ɏo��
                throw ex;
#else
                Trace.Assert(false, "�\�����Ȃ��G���[�������������߁A�I�����܂�");
#endif
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
            if (headlineListBox.SelectedIndex != -1 && headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
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
        /// �ԑg�̏ڍ׃t�H�[����\������
        /// </summary>
        private void ShowStationProperty()
        {
            if (headlineListBox.SelectedIndex != -1 && headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
            {
                StationList.ShowPropertyFormOfCurrentStation(StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex]);
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
            UpdateButtonEnable(false);
            PlayButtonEnable(false);
            filterCheckBox.Enabled = false;
            stationListComboBox.Enabled = false;
            headlineListBox.Enabled = false;
            updateMenuItem.Enabled = false;

            #endregion

            mainStatusBar.Text = "�ڑ���...";
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
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("�ԑg�\���擾�ł��܂���ł���", "�ڑ��G���[");
            }
            catch (OutOfMemoryException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("������������܂���", "�������G���[");
            }
            catch (IOException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("�L�^�f�o�C�X�����炩�̃G���[�ł�", "�f�o�C�X�G���[");
            }
            catch (UriFormatException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.StationNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            catch (SocketException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("�ԑg�\���擾�ł��܂���ł���", "�l�b�g���[�N�G���[");
            }
            catch (NotSupportedException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.StationNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            catch (XmlException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("XML�`���̃w�b�h���C��������ɏ����ł��܂���ł���", "XML�G���[");
            }
            catch (ArgumentException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.StationNameOfCurrentStation + "��URL���s���ł�", "URL�G���[");
            }
            finally
            {
                #region  UI�㏈��

                // �t�H�[�����R���g���[����I���\�ɉ񕜂���
                UpdateButtonEnable(true);
                PlayButtonEnable(true);
                filterCheckBox.Enabled = true;
                stationListComboBox.Enabled = true;
                headlineListBox.Enabled = true;
                updateMenuItem.Enabled = true;
                WriteSelectedChannelInfomation();

                #endregion

                // �r�������̂��߂̃t���O��������
                checkHeadlineNowFlag = false;
            }
        }

        /// <summary>
        /// �t�B���^�����O��؂�ւ���
        /// </summary>
        /// <param name="filtering">�t�B���^�����O��L���ɂ��邩</param>
        private void Filtering(bool filtering)
        {
            if (isFiltering != filtering)
            {
                #region UI�O����

                // �t�H�[�����R���g���[������������I��s�ɂ���
                UpdateButtonEnable(false);
                PlayButtonEnable(false);
                filterCheckBox.Enabled = false;
                stationListComboBox.Enabled = false;
                headlineListBox.Enabled = false;
                updateMenuItem.Enabled = false;

                #endregion

                #region �t�B���^�����O����

                if (filtering == true)
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

                // �t�H�[�����R���g���[����I���\�ɉ񕜂���
                UpdateButtonEnable(true);
                PlayButtonEnable(true);
                filterCheckBox.Enabled = true;
                stationListComboBox.Enabled = true;
                headlineListBox.Enabled = true;
                updateMenuItem.Enabled = true;
                WriteSelectedChannelInfomation();

                #endregion
            }
            isFiltering = filtering;
            filterCheckBox.Checked = filtering;
            filterMenuItem.Checked = filtering;
        }

        /// <summary>
        /// UpdateButton��L���ɂ���B
        /// UpdateButton���L���̎w��ł��A�@���ǂ̑I�����Ȃ��ꍇ�ɂ�UpdateButton�𖳌��ɂ���B
        /// </summary>
        private void UpdateButtonEnable()
        {
            UpdateButtonEnable(true);
        }

        /// <summary>
        /// UpdateButton��L���ɂ���B
        /// UpdateButton���L���̎w��ł��A�@���ǂ̑I�����Ȃ��ꍇ�ɂ�UpdateButton�𖳌��ɂ���B
        /// </summary>
        /// <param name="enable">UpdateButton��L���ɂ��邩</param>
        private void UpdateButtonEnable(bool enable)
        {
            if (enable == false)
            {
                updateButton.Enabled = false;
            }
            else
            {
                if (stationListComboBox.SelectedIndex == -1)
                {
                    updateButton.Enabled = false;
                }
                else
                {
                    updateButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// PlayButton�̗L���ɂ���B
        /// PlayButton���L���̎w��ł��A���X�g�{�b�N�X�ɑI�����Ȃ��ꍇ�ƁA�I���������X�g�{�b�N�X�̃R���e���c��
        /// �Đ����e�������Ă��Ȃ��Ƃ��ɂ�PlayButton�𖳌��ɂ���B
        /// </summary>
        private void PlayButtonEnable()
        {
            PlayButtonEnable(true);
        }

        /// <summary>
        /// PlayButton�̗L��������؂�ւ���B
        /// PlayButton���L���̎w��ł��A���X�g�{�b�N�X�ɑI�����Ȃ��ꍇ�ƁA�I���������X�g�{�b�N�X�̃R���e���c��
        /// �Đ����e�������Ă��Ȃ��Ƃ��ɂ�PlayButton�𖳌��ɂ���B
        /// </summary>
        /// <param name="enable">PlayButton��L���ɂ��邩</param>
        private void PlayButtonEnable(bool enable)
        {
            if (enable == false)
            {
                playButton.Enabled = false;
            }
            else
            {
                if (headlineListBox.SelectedIndex == -1 ||
                    StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetPlayUrl() == null)
                {
                    playButton.Enabled = false;
                }
                else
                {
                    playButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// �I�����Ă���ԑg�̏������x���ɏ���
        /// </summary>
        private void WriteSelectedChannelInfomation()
        {
            if (headlineListBox.SelectedIndex != -1 && headlineListBox.SelectedIndex < headlineListBox.Items.Count)
            {
                headlineInfomationLabel.Text = (string)headlineListBox.Items[headlineListBox.SelectedIndex];
            }
            else
            {
                headlineInfomationLabel.Text = string.Empty;
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
            anchorControlList.Add(new AnchorLayout(headlineInfomationLabel, AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
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
                addFilterMenuItem.Enabled = false;
                addPlayUrlToFilterMenuItem.Enabled = false;
            }
            else
            {
                if (StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetPlayUrl() != null)
                {
                    playMenuItem.Enabled = true;
                    addPlayUrlToFilterMenuItem.Enabled = true;
                }
                else
                {
                    playMenuItem.Enabled = false;
                    addPlayUrlToFilterMenuItem.Enabled = false;
                }

                if (StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetWebsiteUrl() != null)
                {
                    browserMenuItem.Enabled = true;
                }
                else
                {
                    browserMenuItem.Enabled = false;
                }

                if (headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
                {
                    channelPropertyMenuItem.Enabled = true;
                    addFilterMenuItem.Enabled = true;
                }
                else
                {
                    channelPropertyMenuItem.Enabled = false;
                    addFilterMenuItem.Enabled = false;
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
            Filtering(filterCheckBox.Checked);
        }

        private void ChannelPropertyMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowStationProperty();
        }

        private void addFilterMenuItem_Click(object sender, EventArgs e)
        {
            if (StationList.StationHeadlineOfCurrentStation != null 
                && headlineListBox.SelectedIndex != -1
                && headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
            {
                // �I�𒆂̔ԑg
                IChannel channel = StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex];

                // �I�𒆂̔ԑg�̕\�������t�B���^�[�ɓo�^����
                StationList.StationHeadlineOfCurrentStation.ShowSettingFormForAddFilter(channel.GetChannelView());
            }
        }

        private void addPlayUrlToFilterMenuItem_Click(object sender, EventArgs e)
        {
            if (StationList.StationHeadlineOfCurrentStation != null
                && headlineListBox.SelectedIndex != -1
                && headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
            {
                // �I�𒆂̔ԑg
                IChannel channel = StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex];

                // �I�𒆂̔ԑg��PlayURL���t�B���^�[�ɓo�^����
                StationList.StationHeadlineOfCurrentStation.ShowSettingFormForAddFilter(channel.GetPlayUrl().ToString());
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

                UserSetting.HeadlineTimerMillSecondChanged += new EventHandler(UserSetting_HeadlineTimerMillSecondChanged);

                StationList.StationListChanged += new EventHandler(StationList_StationListChanged);

                AddStationsSettingAndComboBoxItem();

                // �ԑg���X�g������ꍇ
                if (StationList.GetChannelsFilteredOfCurrentStation().Length > 0)
                {
                    DrawChannelList(StationList.GetChannelsFilteredOfCurrentStation());
                }

                // �t�B���^�[�̗L�������ݒ肪�L���ȏꍇ�A�t�B���^�[�𓮍삳����
                if (UserSetting.FilterEnable == true)
                {
                    Filtering(true);
                    isFiltering = true;
                }
                else
                {
                    Filtering(false);
                    isFiltering = false;
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

            StationList.HeadlineFetching += new FetchEventHandler(StationList_HeadlineFetching);
            StationList.HeadlineAnalyzing += new HeadlineAnalyzeEventHandler(StationList_HeadlineAnalyzing);

            SetAnchorControl();
            FixWindowSize();
            UpdateButtonEnable();
            PlayButtonEnable();
            WriteSelectedChannelInfomation();
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

        void StationList_StationListChanged(object sender, EventArgs e)
        {
            AddStationsSettingAndComboBoxItem();
        }

        void UserSetting_HeadlineTimerMillSecondChanged(object sender, EventArgs e)
        {
            HeadlineTimerIntervalChange(UserSetting.HeadlineTimerMillSecond);
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
            switch (e.KeyCode)
            {
                // ���̓{�^�����������Ƃ�
                case Keys.Enter:
                    PlayStreaming();
                    break;
            }
        }

        private void StationListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region UI�O����

            // �t�H�[�����R���g���[������������I��s�ɂ���
            UpdateButtonEnable(false);
            PlayButtonEnable(false);
            filterCheckBox.Enabled = false;
            headlineListBox.Enabled = false;
            updateMenuItem.Enabled = false;

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
            UpdateButtonEnable(true);
            PlayButtonEnable(true);
            filterCheckBox.Enabled = true;
            headlineListBox.Enabled = true;
            updateMenuItem.Enabled = true;
            WriteSelectedChannelInfomation();

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

        private void headlineListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlayButtonEnable();

            WriteSelectedChannelInfomation();
        }

        private void playOfChannelMenuItem_Click(object sender, EventArgs e)
        {
            PlayStreaming();
        }

        private void browserOfChannelMenuItem_Click(object sender, EventArgs e)
        {
            AccessWebSite();
        }

        private void channelPropertyOfChannelMenuItem_Click(object sender, EventArgs e)
        {
            ShowStationProperty();
        }

        private void channelMenuItem_Popup(object sender, EventArgs e)
        {
            if (headlineListBox.SelectedIndex == -1)
            {
                channelPropertyOfChannelMenuItem.Enabled = false;
                playOfChannelMenuItem.Enabled = false;
                browserOfChannelMenuItem.Enabled = false;
            }
            else
            {
                if (StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetPlayUrl() != null)
                {
                    playOfChannelMenuItem.Enabled = true;
                }
                else
                {
                    playOfChannelMenuItem.Enabled = false;
                }

                if (StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetWebsiteUrl() != null)
                {
                    browserOfChannelMenuItem.Enabled = true;
                }
                else
                {
                    browserOfChannelMenuItem.Enabled = false;
                }

                if (headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
                {
                    channelPropertyOfChannelMenuItem.Enabled = true;
                }
                else
                {
                    channelPropertyOfChannelMenuItem.Enabled = false;
                }
            }
        }

        private void updateMenuItem_Click(object sender, EventArgs e)
        {
            CheckHeadline();

            // �ԑg���X�g���I������Ă��Ȃ��ꍇ�́A�擪�̍��ڂ�I������
            if (headlineListBox.SelectedIndex == -1 && headlineListBox.Items.Count > 0)
            {
                headlineListBox.SelectedIndex = 0;
                headlineListBox.Focus();
            }
        }

        #region �R���{�{�b�N�X�̃h���b�v�_�E�����J��

        [DllImport("coredll.dll")]
        private static extern IntPtr GetCapture();

        [DllImport("coredll.dll")]
        static extern IntPtr SetCapture(IntPtr hWnd);

        /// <summary>
        /// Control��Window handle��Ԃ�
        /// </summary>
        /// <param name="control">Control</param>
        /// <returns>Control��Window handle</returns>
        private IntPtr GetHWnd(Control control)
        {
            IntPtr hOldWnd = GetCapture();
            control.Capture = true;
            IntPtr hWnd = GetCapture();
            control.Capture = false;
            SetCapture(hOldWnd);
            return hWnd;
        }

        private const uint CB_SHOWDROPDOWN = 0x014f;
        private const int TRUE = 1;
        private const int FALSE = 0;

        [DllImport("Coredll.dll", EntryPoint = "SendMessage", SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        /// <summary>
        /// �R���{�{�b�N�X�̃h���b�v�_�E�����J��
        /// </summary>
        /// <param name="comboBox">�h���b�v�_�E�����J���R���{�{�b�N�X</param>
        private void droppedDownCombobox(ComboBox comboBox)
        {
            SendMessage(GetHWnd(comboBox), CB_SHOWDROPDOWN, TRUE, 0);
        }

        #endregion // �R���{�{�b�N�X�̃h���b�v�_�E�����J��

        private void selectStationMenuItem_Click(object sender, EventArgs e)
        {
            stationListComboBox.Focus();
            droppedDownCombobox(stationListComboBox);
        }

        private void selectChannelMenuItem_Click(object sender, EventArgs e)
        {
            headlineListBox.Focus();
            // �A�C�e����I������
            if (headlineListBox.Items.Count > 0 && headlineListBox.SelectedIndex == -1)
            {
                headlineListBox.SelectedIndex = 0;
            }
        }

        private void filterMenuItem_Click(object sender, EventArgs e)
        {
            Filtering((isFiltering == true ? false : true));
        }
    }
}
