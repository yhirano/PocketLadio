using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using PocketLadio.Util;

namespace PocketLadio
{
    /// <summary>
    /// PocketLadio�̐ݒ�t�H�[��
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private MainMenu MainMenu;
        private Label HeadlineTimerSecondLabel;
        private MenuItem OkMenuItem;
        private TabControl SettingTabControl;
        private TabPage PocketLadioTabPage;
        private Label BrowserPathLabel;
        private Label MediaPlayerPathLabel;
        private TextBox MediaPlayerPathTextBox;
        private TextBox BrowserPathTextBox;
        private TabPage StationListTabPage;
        private TextBox StationNameTextBox;
        private ListBox StationListBox;
        private Button DeleteButton;
        private Button AddButton;
        private ComboBox StationKindComboBox;
        private ContextMenu StationListBoxContextMenu;
        private MenuItem DeleteMenuItem;
        private NumericUpDown HeadlineTimerSecondNumericUpDown;
        private ContextMenu StationNameContextMenu;
        private MenuItem CutStationNameMenuItem;
        private MenuItem CopyStationNameMenuItem;
        private MenuItem PasteStationNameMenuItem;
        private ContextMenu MediaPlayeraPathContextMenu;
        private MenuItem CutMediaPlayeraPathMenuItem;
        private MenuItem CopyMediaPlayeraPathMenuItem;
        private MenuItem PasteMediaPlayeraPathMenuItem;
        private ContextMenu BrowserPathContextMenu;
        private MenuItem CutBrowserPathMenuItem;
        private MenuItem CopyBrowserPathMenuItem;
        private MenuItem PasteBrowserPathMenuItem;
        private MenuItem SettingMenuItem;
        private Label StationListLabel;
        private Label AddStationLabel;

        /// <summary>
        /// �����ǂ̃��X�g
        /// </summary>
        private ArrayList AlStationList = new ArrayList();

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
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.OkMenuItem = new System.Windows.Forms.MenuItem();
            this.SettingTabControl = new System.Windows.Forms.TabControl();
            this.StationListTabPage = new System.Windows.Forms.TabPage();
            this.StationListLabel = new System.Windows.Forms.Label();
            this.AddStationLabel = new System.Windows.Forms.Label();
            this.StationListBox = new System.Windows.Forms.ListBox();
            this.StationListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.SettingMenuItem = new System.Windows.Forms.MenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.MenuItem();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.StationKindComboBox = new System.Windows.Forms.ComboBox();
            this.StationNameTextBox = new System.Windows.Forms.TextBox();
            this.StationNameContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutStationNameMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyStationNameMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteStationNameMenuItem = new System.Windows.Forms.MenuItem();
            this.PocketLadioTabPage = new System.Windows.Forms.TabPage();
            this.BrowserPathTextBox = new System.Windows.Forms.TextBox();
            this.BrowserPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.BrowserPathLabel = new System.Windows.Forms.Label();
            this.MediaPlayerPathTextBox = new System.Windows.Forms.TextBox();
            this.MediaPlayeraPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.MediaPlayerPathLabel = new System.Windows.Forms.Label();
            this.HeadlineTimerSecondNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.HeadlineTimerSecondLabel = new System.Windows.Forms.Label();
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
            // SettingTabControl
            // 
            this.SettingTabControl.Controls.Add(this.StationListTabPage);
            this.SettingTabControl.Controls.Add(this.PocketLadioTabPage);
            this.SettingTabControl.Location = new System.Drawing.Point(0, 0);
            this.SettingTabControl.SelectedIndex = 0;
            this.SettingTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // StationListTabPage
            // 
            this.StationListTabPage.Controls.Add(this.StationListLabel);
            this.StationListTabPage.Controls.Add(this.AddStationLabel);
            this.StationListTabPage.Controls.Add(this.StationListBox);
            this.StationListTabPage.Controls.Add(this.DeleteButton);
            this.StationListTabPage.Controls.Add(this.AddButton);
            this.StationListTabPage.Controls.Add(this.StationKindComboBox);
            this.StationListTabPage.Controls.Add(this.StationNameTextBox);
            this.StationListTabPage.Location = new System.Drawing.Point(0, 0);
            this.StationListTabPage.Size = new System.Drawing.Size(240, 245);
            this.StationListTabPage.Text = "�����ǂ̃��X�g";
            // 
            // StationListLabel
            // 
            this.StationListLabel.Location = new System.Drawing.Point(3, 77);
            this.StationListLabel.Size = new System.Drawing.Size(84, 20);
            this.StationListLabel.Text = "�����ǈꗗ";
            // 
            // AddStationLabel
            // 
            this.AddStationLabel.Location = new System.Drawing.Point(3, 4);
            this.AddStationLabel.Size = new System.Drawing.Size(84, 20);
            this.AddStationLabel.Text = "�����ǂ̒ǉ�";
            // 
            // StationListBox
            // 
            this.StationListBox.ContextMenu = this.StationListBoxContextMenu;
            this.StationListBox.Location = new System.Drawing.Point(3, 99);
            this.StationListBox.Size = new System.Drawing.Size(234, 114);
            // 
            // StationListBoxContextMenu
            // 
            this.StationListBoxContextMenu.MenuItems.Add(this.SettingMenuItem);
            this.StationListBoxContextMenu.MenuItems.Add(this.DeleteMenuItem);
            // 
            // SettingMenuItem
            // 
            this.SettingMenuItem.Text = "�ݒ�(&S)";
            this.SettingMenuItem.Click += new System.EventHandler(this.SettingMenuItem_Click);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Text = "�폜(&D)";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(165, 220);
            this.DeleteButton.Size = new System.Drawing.Size(72, 20);
            this.DeleteButton.Text = "�폜(&D)";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(165, 54);
            this.AddButton.Size = new System.Drawing.Size(72, 20);
            this.AddButton.Text = "�ǉ�(&A)";
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // StationKindComboBox
            // 
            this.StationKindComboBox.Items.Add("�˂Ƃ炶");
            this.StationKindComboBox.Items.Add("Podcast");
            this.StationKindComboBox.Location = new System.Drawing.Point(137, 27);
            this.StationKindComboBox.Size = new System.Drawing.Size(100, 22);
            // 
            // StationNameTextBox
            // 
            this.StationNameTextBox.ContextMenu = this.StationNameContextMenu;
            this.StationNameTextBox.Location = new System.Drawing.Point(3, 27);
            this.StationNameTextBox.Size = new System.Drawing.Size(128, 21);
            // 
            // StationNameContextMenu
            // 
            this.StationNameContextMenu.MenuItems.Add(this.CutStationNameMenuItem);
            this.StationNameContextMenu.MenuItems.Add(this.CopyStationNameMenuItem);
            this.StationNameContextMenu.MenuItems.Add(this.PasteStationNameMenuItem);
            // 
            // CutStationNameMenuItem
            // 
            this.CutStationNameMenuItem.Text = "�؂���(&T)";
            this.CutStationNameMenuItem.Click += new System.EventHandler(this.CutStationNameMenuItem_Click);
            // 
            // CopyStationNameMenuItem
            // 
            this.CopyStationNameMenuItem.Text = "�R�s�[(&C)";
            this.CopyStationNameMenuItem.Click += new System.EventHandler(this.CopyStationNameMenuItem_Click);
            // 
            // PasteStationNameMenuItem
            // 
            this.PasteStationNameMenuItem.Text = "�\��t��(&P)";
            this.PasteStationNameMenuItem.Click += new System.EventHandler(this.PasteStationNameMenuItem_Click);
            // 
            // PocketLadioTabPage
            // 
            this.PocketLadioTabPage.Controls.Add(this.BrowserPathTextBox);
            this.PocketLadioTabPage.Controls.Add(this.BrowserPathLabel);
            this.PocketLadioTabPage.Controls.Add(this.MediaPlayerPathTextBox);
            this.PocketLadioTabPage.Controls.Add(this.MediaPlayerPathLabel);
            this.PocketLadioTabPage.Controls.Add(this.HeadlineTimerSecondNumericUpDown);
            this.PocketLadioTabPage.Controls.Add(this.HeadlineTimerSecondLabel);
            this.PocketLadioTabPage.Location = new System.Drawing.Point(0, 0);
            this.PocketLadioTabPage.Size = new System.Drawing.Size(232, 242);
            this.PocketLadioTabPage.Text = "PocketLadio�ݒ�";
            // 
            // BrowserPathTextBox
            // 
            this.BrowserPathTextBox.ContextMenu = this.BrowserPathContextMenu;
            this.BrowserPathTextBox.Location = new System.Drawing.Point(3, 114);
            this.BrowserPathTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // BrowserPathContextMenu
            // 
            this.BrowserPathContextMenu.MenuItems.Add(this.CutBrowserPathMenuItem);
            this.BrowserPathContextMenu.MenuItems.Add(this.CopyBrowserPathMenuItem);
            this.BrowserPathContextMenu.MenuItems.Add(this.PasteBrowserPathMenuItem);
            // 
            // CutBrowserPathMenuItem
            // 
            this.CutBrowserPathMenuItem.Text = "�؂���(&T)";
            this.CutBrowserPathMenuItem.Click += new System.EventHandler(this.CutBrowserPathMenuItem_Click);
            // 
            // CopyBrowserPathMenuItem
            // 
            this.CopyBrowserPathMenuItem.Text = "�R�s�[(&C)";
            this.CopyBrowserPathMenuItem.Click += new System.EventHandler(this.CopyBrowserPathMenuItem_Click);
            // 
            // PasteBrowserPathMenuItem
            // 
            this.PasteBrowserPathMenuItem.Text = "�\��t��(&P)";
            this.PasteBrowserPathMenuItem.Click += new System.EventHandler(this.PasteBrowserPathMenuItem_Click);
            // 
            // BrowserPathLabel
            // 
            this.BrowserPathLabel.Location = new System.Drawing.Point(3, 95);
            this.BrowserPathLabel.Size = new System.Drawing.Size(79, 16);
            this.BrowserPathLabel.Text = "�u���E�U�̃p�X";
            // 
            // MediaPlayerPathTextBox
            // 
            this.MediaPlayerPathTextBox.ContextMenu = this.MediaPlayeraPathContextMenu;
            this.MediaPlayerPathTextBox.Location = new System.Drawing.Point(3, 71);
            this.MediaPlayerPathTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // MediaPlayeraPathContextMenu
            // 
            this.MediaPlayeraPathContextMenu.MenuItems.Add(this.CutMediaPlayeraPathMenuItem);
            this.MediaPlayeraPathContextMenu.MenuItems.Add(this.CopyMediaPlayeraPathMenuItem);
            this.MediaPlayeraPathContextMenu.MenuItems.Add(this.PasteMediaPlayeraPathMenuItem);
            // 
            // CutMediaPlayeraPathMenuItem
            // 
            this.CutMediaPlayeraPathMenuItem.Text = "�؂���(&T)";
            this.CutMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.CutMediaPlayeraPathMenuItem_Click);
            // 
            // CopyMediaPlayeraPathMenuItem
            // 
            this.CopyMediaPlayeraPathMenuItem.Text = "�R�s�[(&C)";
            this.CopyMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.CopyMediaPlayeraPathMenuItem_Click);
            // 
            // PasteMediaPlayeraPathMenuItem
            // 
            this.PasteMediaPlayeraPathMenuItem.Text = "�\��t��(&P)";
            this.PasteMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.PasteMediaPlayeraPathMenuItem_Click);
            // 
            // MediaPlayerPathLabel
            // 
            this.MediaPlayerPathLabel.Location = new System.Drawing.Point(3, 52);
            this.MediaPlayerPathLabel.Size = new System.Drawing.Size(132, 16);
            this.MediaPlayerPathLabel.Text = "���f�B�A�v���[���[�̃p�X";
            // 
            // HeadlineTimerSecondNumericUpDown
            // 
            this.HeadlineTimerSecondNumericUpDown.Location = new System.Drawing.Point(182, 27);
            this.HeadlineTimerSecondNumericUpDown.ReadOnly = true;
            this.HeadlineTimerSecondNumericUpDown.Size = new System.Drawing.Size(55, 22);
            this.HeadlineTimerSecondNumericUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // HeadlineTimerSecondLabel
            // 
            this.HeadlineTimerSecondLabel.Location = new System.Drawing.Point(3, 4);
            this.HeadlineTimerSecondLabel.Size = new System.Drawing.Size(188, 20);
            this.HeadlineTimerSecondLabel.Text = "�w�b�h���C���̎����`�F�b�N�Ԋu(�b)";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.SettingTabControl);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "�ݒ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // �w�b�h���C���`�F�b�N�^�C�}�[�̏���Ɖ���
            HeadlineTimerSecondNumericUpDown.Minimum = Controller.HeadlineCheckTimerMinimumMillSec / 1000;
            HeadlineTimerSecondNumericUpDown.Maximum = Controller.HeadlineCheckTimerMaximumMillSec / 1000;

            // �ݒ�̓ǂݍ���
            MediaPlayerPathTextBox.Text = UserSetting.MediaPlayerPath;
            BrowserPathTextBox.Text = UserSetting.BrowserPath;
            HeadlineTimerSecondNumericUpDown.Text = (UserSetting.HeadlineTimerMillSecond / 1000).ToString();

            // �����Ǐ��̓ǂݍ���
            foreach (Station Station in StationList.GetStationList())
            {
                AlStationList.Add(Station);
                StationListBox.Items.Add(Station.GetDisplayName());
            }
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �ݒ�̏�������
            UserSetting.MediaPlayerPath = MediaPlayerPathTextBox.Text.Trim();
            UserSetting.BrowserPath = BrowserPathTextBox.Text.Trim();
            try
            {
                UserSetting.HeadlineTimerMillSecond = Convert.ToInt32(HeadlineTimerSecondNumericUpDown.Text) * 1000;
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
            StationList.SetStationList((Station[])AlStationList.ToArray(typeof(Station)));
            UserSetting.SaveSetting();
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (StationNameTextBox.Text.Trim() != "" && StationKindComboBox.Text.Trim() != "")
            {
                if (StationKindComboBox.Text.Trim().Equals("�˂Ƃ炶"))
                {
                    Station Station = new Station(DateTime.Now.ToString("yyyyMMddHHmmssff"), StationNameTextBox.Text.Trim(), Station.StationKindEnum.Netladio);
                    AlStationList.Add(Station);
                    StationListBox.Items.Add(Station.GetDisplayName());
                    StationNameTextBox.Text = "";
                }
                else if (StationKindComboBox.Text.Trim().Equals("Podcast"))
                {
                    Station Station = new Station(DateTime.Now.ToString("yyyyMMddHHmmssff"), StationNameTextBox.Text.Trim(), Station.StationKindEnum.RssPodcast);
                    AlStationList.Add(Station);
                    StationListBox.Items.Add(Station.GetDisplayName());
                    StationNameTextBox.Text = "";

                    // �ݒ��ʂ��Ăяo��
                    Station.GetHeadline().ShowSettingForm();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (StationListBox.SelectedIndex != -1)
            {
                // �ݒ�t�@�C���̍폜
                ((Station)AlStationList[StationListBox.SelectedIndex]).GetHeadline().DeleteUserSettingFile();

                AlStationList.RemoveAt(StationListBox.SelectedIndex);
                StationListBox.Items.RemoveAt(StationListBox.SelectedIndex);
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (StationListBox.SelectedIndex != -1)
            {
                // �ݒ�t�@�C���̍폜
                ((Station)AlStationList[StationListBox.SelectedIndex]).GetHeadline().DeleteUserSettingFile();

                AlStationList.RemoveAt(StationListBox.SelectedIndex);
                StationListBox.Items.RemoveAt(StationListBox.SelectedIndex);
            }
        }


        private void SettingMenuItem_Click(object sender, EventArgs e)
        {
            if (StationListBox.SelectedIndex != -1)
            {
                ((Station)AlStationList[StationListBox.SelectedIndex]).GetHeadline().ShowSettingForm();
            }
        }

        private void CutStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(StationNameTextBox);
        }

        private void CopyStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(StationNameTextBox);
        }

        private void PasteStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(StationNameTextBox);
        }

        private void CutMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(MediaPlayerPathTextBox);
        }

        private void CopyMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(MediaPlayerPathTextBox);
        }

        private void PasteMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(MediaPlayerPathTextBox);
        }

        private void CutBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(BrowserPathTextBox);
        }

        private void CopyBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(BrowserPathTextBox);
        }

        private void PasteBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(BrowserPathTextBox);
        }
    }
}
