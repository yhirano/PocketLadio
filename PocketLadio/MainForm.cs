using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using PocketLadio.StationInterface;

namespace PocketLadio
{
    /// <summary>
    /// �A�v���P�[�V�����̃��C���t�H�[��
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private MenuItem MenuMenuItem;
        private MenuItem HeadlineCheckTimerMenuItem;
        private MenuItem FilterSettingMenuItem;
        private MenuItem StationSettingMenuItem;
        private MenuItem VersionInfoMenuItem;
        private MenuItem SeparateMenuItem1;
        private MenuItem ExitMenuItem;
        private Button PlayButton;
        private ListBox HeadlineListBox;
        private Button GetButton;
        private CheckBox FilterCheckBox;
        private Label InfomationLabel;
        private MainMenu MainMenu;
        private ContextMenu HeadlineContextMenu;
        private MenuItem PlayMenuItem;
        private MenuItem BrowserMenuItem;
        private MenuItem ChanelPropertyMenuItem;
        private MenuItem PocketLadioSettingMenuItem;
        private ComboBox StationListComboBox;
        private Timer HeadlineCheckTimer;
        private MenuItem StationsSettingMenuItem;
        private MenuItem SeparateMenuItem2;
        private MenuItem SeparateMenuItem3;
        private MenuItem SeparateMenuItem4;

        /// <summary>
        /// �I������Ă��������ǂ�ID
        /// </summary>
        private string SelectedStationID = "";

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
            this.SeparateMenuItem1 = new System.Windows.Forms.MenuItem();
            this.StationsSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.FilterSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.StationSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.SeparateMenuItem2 = new System.Windows.Forms.MenuItem();
            this.PocketLadioSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.SeparateMenuItem3 = new System.Windows.Forms.MenuItem();
            this.VersionInfoMenuItem = new System.Windows.Forms.MenuItem();
            this.SeparateMenuItem4 = new System.Windows.Forms.MenuItem();
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
            this.StationListComboBox = new System.Windows.Forms.ComboBox();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.Add(this.MenuMenuItem);
            // 
            // MenuMenuItem
            // 
            this.MenuMenuItem.MenuItems.Add(this.HeadlineCheckTimerMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.SeparateMenuItem1);
            this.MenuMenuItem.MenuItems.Add(this.StationsSettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.StationSettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.FilterSettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.SeparateMenuItem2);
            this.MenuMenuItem.MenuItems.Add(this.PocketLadioSettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.SeparateMenuItem3);
            this.MenuMenuItem.MenuItems.Add(this.VersionInfoMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.SeparateMenuItem4);
            this.MenuMenuItem.MenuItems.Add(this.ExitMenuItem);
            this.MenuMenuItem.Text = "���j���[(&M)";
            // 
            // HeadlineCheckTimerMenuItem
            // 
            this.HeadlineCheckTimerMenuItem.Text = "�w�b�h���C�������Ԋu�Ń`�F�b�N(&T)";
            this.HeadlineCheckTimerMenuItem.Click += new System.EventHandler(this.HeadlineCheckTimerMenuItem_Click);
            // 
            // SeparateMenuItem1
            // 
            this.SeparateMenuItem1.Text = "-";
            // 
            // StationsSettingMenuItem
            // 
            this.StationsSettingMenuItem.Text = "�����ǂ̒ǉ��ƍ폜 (&A)";
            this.StationsSettingMenuItem.Click += new System.EventHandler(this.StationsSettingMenuItem_Click);
            // 
            // FilterSettingMenuItem
            // 
            this.FilterSettingMenuItem.Text = "�t�B���^�[�̒ǉ��ƍ폜(&F)";
            this.FilterSettingMenuItem.Click += new System.EventHandler(this.FilterSettingMenuItem_Click);
            // 
            // StationSettingMenuItem
            // 
            this.StationSettingMenuItem.Text = "�����ǂ̐ݒ�(&S)";
            // 
            // SeparateMenuItem2
            // 
            this.SeparateMenuItem2.Text = "-";
            // 
            // PocketLadioSettingMenuItem
            // 
            this.PocketLadioSettingMenuItem.Text = "PocketLadio�ݒ�(&P)";
            this.PocketLadioSettingMenuItem.Click += new System.EventHandler(this.PocketLadioSettingMenuItem_Click);
            // 
            // SeparateMenuItem3
            // 
            this.SeparateMenuItem3.Text = "-";
            // 
            // VersionInfoMenuItem
            // 
            this.VersionInfoMenuItem.Text = "�o�[�W�������(&A)";
            this.VersionInfoMenuItem.Click += new System.EventHandler(this.VersionInfoMenuItem_Click);
            // 
            // SeparateMenuItem4
            // 
            this.SeparateMenuItem4.Text = "-";
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
            this.HeadlineListBox.Location = new System.Drawing.Point(3, 60);
            this.HeadlineListBox.Size = new System.Drawing.Size(234, 184);
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
            // StationListComboBox
            // 
            this.StationListComboBox.Location = new System.Drawing.Point(3, 29);
            this.StationListComboBox.Size = new System.Drawing.Size(234, 22);
            this.StationListComboBox.SelectedIndexChanged += new System.EventHandler(this.StationListComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.StationListComboBox);
            this.Controls.Add(this.InfomationLabel);
            this.Controls.Add(this.FilterCheckBox);
            this.Controls.Add(this.GetButton);
            this.Controls.Add(this.HeadlineListBox);
            this.Controls.Add(this.PlayButton);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "PocketLadio";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
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
        private void UpdateRadioList(IChanel[] chanels)
        {
            // ��������ԑg���X�g����ʂ������
            HeadlineListBox.Visible = false;
            
            HeadlineListBox.Items.Clear();
            foreach (IChanel Chanel in chanels)
            {
                HeadlineListBox.Items.Add(Chanel.GetChanelView());
            }

            // �ԑg���X�g��`�悷��
            HeadlineListBox.Visible = true;
        }

        /// <summary>
        /// �X�g���[�~���O���Đ�����
        /// </summary>
        private void PlayStreaming()
        {
            if (HeadlineListBox.SelectedIndex != -1)
            {
                Controller.PlayStreaming(StationList.GetChanelsFilteredOfCurrentStation()[HeadlineListBox.SelectedIndex].GetPlayUrl());
            }
        }

        /// <summary>
        /// Web�T�C�g�փA�N�Z�X����
        /// </summary>
        private void AccessWebSite()
        {
            if (HeadlineListBox.SelectedIndex != -1)
            {
                if (StationList.GetChanelsFilteredOfCurrentStation()[HeadlineListBox.SelectedIndex].GetWebSiteUrl() != "")
                {
                    Controller.AccessWebSite(StationList.GetChanelsFilteredOfCurrentStation()[HeadlineListBox.SelectedIndex].GetWebSiteUrl());
                }
            }
        }

        /// <summary>
        /// �w�b�h���C�����擾�A�\������
        /// </summary>
        private void CheckHeadline()
        {
            lock (this)
            {
                // �����ǑI���{�b�N�X���I���\�������t���O
                bool StationListComboBoxEnabledFlag = false;

                try
                {
                    /** UI�O���� **/
                    // Get�{�^����Filter�`�F�b�N�{�b�N�X����������I��s�ɂ���
                    GetButton.Enabled = false;
                    FilterCheckBox.Enabled = false;

                    // �����ǑI���{�b�N�X���I���\�������ꍇ�ɂ̂݁A��������I��s�ɂ���
                    // �i�����ǂ��ЂƂ��ݒ肳��Ă��Ȃ��ꍇ�ɂ́A���X�I��s�̂��߁j
                    if (StationListComboBox.Enabled == true)
                    {
                        StationListComboBox.Enabled = false;
                        // �����ǑI���{�b�N�X���I���\�������t���O�𗧂Ă�
                        StationListComboBoxEnabledFlag = true;
                    }

                    /** �ԑg�擾���� **/
                    // �ԑg���擾����
                    StationList.WebGetHeadlineOfCurrentStation();

                    // �ԑg���擾�ł��Ȃ������ꍇ
                    if (StationList.GetLastCheckTimeOfCurrentStation().Equals(DateTime.MinValue))
                    {
                        InfomationLabel.Text = "No Check - 0 CHs";
                    }
                    // �ԑg���擾�ł����ꍇ
                    else
                    {
                        InfomationLabel.Text = "Last " + StationList.GetLastCheckTimeOfCurrentStation().ToString() 
                            + " - " + StationList.GetChanelsOfCurrentStation().Length.ToString() + " CHs";
                    }

                    // �ԑg���X�g���X�V����
                    UpdateRadioList(StationList.GetChanelsFilteredOfCurrentStation());
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
                catch (ArgumentException)
                {
                    HeadlineCheckTimerStop();
                    MessageBox.Show(StationList.GetHeadlineNameOfCurrentStation() + "��URL���s���ł�", "URL�G���[");
                }
                catch (UriFormatException)
                {
                    HeadlineCheckTimerStop();
                    MessageBox.Show(StationList.GetHeadlineNameOfCurrentStation() + "��URL���s���ł�", "URL�G���[");
                }
                catch (NotSupportedException)
                {
                    HeadlineCheckTimerStop();
                    MessageBox.Show(StationList.GetHeadlineNameOfCurrentStation() + "��URL���s���ł�", "URL�G���[");
                }
                finally
                {
                    /** UI�㏈�� **/
                    // Get�{�^����Filter�`�F�b�N�{�b�N�X��I���\�ɉ񕜂���
                    GetButton.Enabled = true;
                    FilterCheckBox.Enabled = true;

                    // �����ǑI���{�b�N�X���I���\�������ꍇ�ɂ̂݁A�I���\�ɉ񕜂���
                    // �i�����ǂ��ЂƂ��ݒ肳��Ă��Ȃ��ꍇ�ɂ́A���X�I��s�̂��߁j
                    if (StationListComboBoxEnabledFlag == true)
                    {
                        StationListComboBox.Enabled = true;
                    }
                }
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
            this.HeadlineListBox.Location = new System.Drawing.Point(3, 60);
            this.HeadlineListBox.Size = new System.Drawing.Size(234, 184);
            this.StationListComboBox.Location = new System.Drawing.Point(3, 29);
            this.StationListComboBox.Size = new System.Drawing.Size(234, 22);
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
            this.HeadlineListBox.Location = new System.Drawing.Point(3, 60);
            this.HeadlineListBox.Size = new System.Drawing.Size(314, 100);
            this.StationListComboBox.Location = new System.Drawing.Point(3, 29);
            this.StationListComboBox.Size = new System.Drawing.Size(314, 22);
            this.InfomationLabel.Location = new System.Drawing.Point(3, 163);
            this.InfomationLabel.Size = new System.Drawing.Size(314, 16);
        }

        /// <summary>
        /// �����Ǐ����������p��MenuItem�N���X
        /// </summary>
        class StationMenuItem : MenuItem
        {
            private Station Station;

            public Station GetStation()
            {
                return Station;
            }

            public void SetStation(Station station)
            {
                this.Station = station;
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
                    this.StationSettingMenuItem.Enabled = false;
                    // �e�����ǂ̐ݒ胁�j���[����������N���A����
                    this.StationSettingMenuItem.MenuItems.Clear();
                    // �e�����ǂ̐ݒ胁�j���[��ǉ�
                    foreach (Station Station in StationList.GetStationList())
                    {
                        StationMenuItem EachStationSettingMenuItem = new StationMenuItem();
                        this.StationSettingMenuItem.MenuItems.Add(EachStationSettingMenuItem);
                        EachStationSettingMenuItem.Text = Station.GetDisplayName() + " �ݒ�";
                        EachStationSettingMenuItem.SetStation(Station);
                        EachStationSettingMenuItem.Click += new System.EventHandler(this.EachStationSettingMenuItem_Click);
                    }
                    // �ݒ胁�j���[���ǉ����I������̂ŁA�e�����ǂ̐ݒ胁�j���[��I���\�ɂ���
                    this.StationSettingMenuItem.Enabled = true;
                }

                // �e�����ǂ̐؂�ւ��{�b�N�X��ǉ�����
                {
                    // �e�����ǂ̐؂�ւ��{�b�N�X����������I��s�ɂ���
                    this.StationListComboBox.Enabled = false;
                    // �e�����ǂ̐؂�ւ��{�b�N�X����������N���A����
                    this.StationListComboBox.Items.Clear();
                    // �e�����ǂ̐؂�ւ��{�b�N�X�̒ǉ�
                    foreach (Station Station in StationList.GetStationList())
                    {
                        this.StationListComboBox.Items.Add(Station.GetDisplayName());
                    }
                    // �؂�ւ��{�b�N�X���ǉ����I������̂ŁA�e�����ǂ̐؂�ւ��{�b�N�X��I���\�ɂ���
                    this.StationListComboBox.Enabled = true;
                }

                // �ȑO�ɑI������Ă��������ǂ�I��������
                for (int Count = 0; Count < StationListComboBox.Items.Count; ++Count)
                {
                    if (StationList.GetStationList()[Count].GetHeadlineID() == SelectedStationID)
                    {
                        this.StationListComboBox.SelectedIndex = Count;
                        break;
                    }
                }

                // �����ǂ��I������Ă��炸�A�������ǂ�����ꍇ
                if (this.StationListComboBox.SelectedIndex == -1 && this.StationListComboBox.Items.Count > 0)
                {
                    // �g�b�v�̕����ǂ�I��
                    this.StationListComboBox.SelectedIndex = 0;
                }
            }
            // �����ǂ����݂��Ȃ��ꍇ�̏���
            // �����ǂ̐ݒ胁�j���[�Ɛ؂�ւ��{�b�N�X��I��s�ɂ���
            else
            {
                // �e�����ǂ̐ݒ胁�j���[��I��s�ɂ���
                this.StationSettingMenuItem.Enabled = false;
                // �e�����ǂ̐ݒ胁�j���[���N���A����
                this.StationSettingMenuItem.MenuItems.Clear();

                // �e�����ǂ̐؂�ւ��{�b�N�X��I��s�ɂ���
                this.StationListComboBox.Enabled = false;
                // �e�����ǂ̐؂�ւ��{�b�N�X���N���A����
                this.StationListComboBox.Items.Clear();
            }
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
            FilterCheckBox.Enabled = false;

            /** �t�B���^�����O���� **/
            if (FilterCheckBox.Checked == true)
            {
                StationList.FilterEnable = true;
            }
            else
            {
                StationList.FilterEnable = false;
            }
            UpdateRadioList(StationList.GetChanelsFilteredOfCurrentStation());

            /** UI�㏈�� **/
            // Filter�`�F�b�N�{�b�N�X��I���\�ɉ񕜂���
            FilterCheckBox.Enabled = true;
        }

        private void ChanelPropertyMenuItem_Click(object sender, System.EventArgs e)
        {
            if (HeadlineListBox.SelectedIndex != -1 && StationList.GetChanelsFilteredOfCurrentStation().Length > 0)
            {
                StationList.ShowPropertyFormOfCurrentStation(StationList.GetChanelsFilteredOfCurrentStation()[HeadlineListBox.SelectedIndex]);
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

            StationsSettingAndCheckBoxAdd();

            // �ԑg���X�g������ꍇ
            if (StationList.GetChanelsFilteredOfCurrentStation().Length > 0)
            {
                UpdateRadioList(StationList.GetChanelsFilteredOfCurrentStation());
            }

            FixWindowSize();
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

        private void MainForm_Activated(object sender, EventArgs e)
        {
            HeadlineCheckTimer.Interval = UserSetting.HeadlineTimerMillSecond;

            StationsSettingAndCheckBoxAdd();
        }

        private void StationListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StationList.ChangeCurrentStationAt(StationListComboBox.SelectedIndex);
            if (StationList.GetLastCheckTimeOfCurrentStation().Equals(DateTime.MinValue))
            {
                InfomationLabel.Text = "No Check - 0 CHs";
            }
            else
            {
                InfomationLabel.Text = "Last " + StationList.GetLastCheckTimeOfCurrentStation().ToString() + " - " + StationList.GetChanelsOfCurrentStation().Length.ToString() + " CHs";
            }
            UpdateRadioList(StationList.GetChanelsFilteredOfCurrentStation());

            // �I�����Ă��������ǂ��L������
            if (StationListComboBox.SelectedIndex != -1)
            {
                SelectedStationID = StationList.GetHeadlineIDOfCurrentStation();
            }
        }

        private void StationsSettingMenuItem_Click(object sender, EventArgs e)
        {
            StationsSettingForm stationSettingForm = new StationsSettingForm();
            DialogResult result = stationSettingForm.ShowDialog();
            stationSettingForm.Dispose();
        }
    }
}
