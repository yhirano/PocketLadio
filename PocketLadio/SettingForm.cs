using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace PocketLadio
{
    /// <summary>
    /// PocketLadioの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuItem OkMenuItem;
        private System.Windows.Forms.TabControl SettingTabControl;
        private System.Windows.Forms.TabPage PocketLadioTabPage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox MediaPlayerPathTextBox;
        private System.Windows.Forms.TextBox BrowserPathTextBox;
        private TabPage StationListTabPage;
        private TextBox StationNameTextBox;
        private ListBox StationListBox;
        private Button DeleteButton;
        private Button AddButton;
        private ComboBox StationKindComboBox;
        private ContextMenu StationListBoxContextMenu;
        private MenuItem menuItem1;
        private System.Windows.Forms.NumericUpDown HeadlineTimerSecondNumericUpDown;

        /// <summary>
        /// 放送局のリスト
        /// </summary>
        private ArrayList AlStationList = new ArrayList();

        public SettingForm()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.OkMenuItem = new System.Windows.Forms.MenuItem();
            this.SettingTabControl = new System.Windows.Forms.TabControl();
            this.StationListTabPage = new System.Windows.Forms.TabPage();
            this.StationListBox = new System.Windows.Forms.ListBox();
            this.StationListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.StationKindComboBox = new System.Windows.Forms.ComboBox();
            this.StationNameTextBox = new System.Windows.Forms.TextBox();
            this.PocketLadioTabPage = new System.Windows.Forms.TabPage();
            this.BrowserPathTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.MediaPlayerPathTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.HeadlineTimerSecondNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
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
            this.StationListTabPage.Controls.Add(this.StationListBox);
            this.StationListTabPage.Controls.Add(this.DeleteButton);
            this.StationListTabPage.Controls.Add(this.AddButton);
            this.StationListTabPage.Controls.Add(this.StationKindComboBox);
            this.StationListTabPage.Controls.Add(this.StationNameTextBox);
            this.StationListTabPage.Location = new System.Drawing.Point(0, 0);
            this.StationListTabPage.Size = new System.Drawing.Size(240, 245);
            this.StationListTabPage.Text = "放送局のリスト";
            // 
            // StationListBox
            // 
            this.StationListBox.ContextMenu = this.StationListBoxContextMenu;
            this.StationListBox.Location = new System.Drawing.Point(3, 57);
            this.StationListBox.Size = new System.Drawing.Size(234, 156);
            // 
            // StationListBoxContextMenu
            // 
            this.StationListBoxContextMenu.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "削除(&D)";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(165, 220);
            this.DeleteButton.Size = new System.Drawing.Size(72, 20);
            this.DeleteButton.Text = "削除(&D)";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(165, 31);
            this.AddButton.Size = new System.Drawing.Size(72, 20);
            this.AddButton.Text = "追加(&A)";
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // StationKindComboBox
            // 
            this.StationKindComboBox.Items.Add("ねとらじ");
            this.StationKindComboBox.Items.Add("Podcast");
            this.StationKindComboBox.Location = new System.Drawing.Point(137, 3);
            this.StationKindComboBox.Size = new System.Drawing.Size(100, 22);
            this.StationKindComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // StationNameTextBox
            // 
            this.StationNameTextBox.Location = new System.Drawing.Point(3, 3);
            this.StationNameTextBox.Size = new System.Drawing.Size(128, 21);
            // 
            // PocketLadioTabPage
            // 
            this.PocketLadioTabPage.Controls.Add(this.BrowserPathTextBox);
            this.PocketLadioTabPage.Controls.Add(this.label8);
            this.PocketLadioTabPage.Controls.Add(this.MediaPlayerPathTextBox);
            this.PocketLadioTabPage.Controls.Add(this.label9);
            this.PocketLadioTabPage.Controls.Add(this.HeadlineTimerSecondNumericUpDown);
            this.PocketLadioTabPage.Controls.Add(this.label5);
            this.PocketLadioTabPage.Location = new System.Drawing.Point(0, 0);
            this.PocketLadioTabPage.Size = new System.Drawing.Size(232, 242);
            this.PocketLadioTabPage.Text = "PocketLadio設定";
            // 
            // BrowserPathTextBox
            // 
            this.BrowserPathTextBox.Location = new System.Drawing.Point(3, 114);
            this.BrowserPathTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 95);
            this.label8.Size = new System.Drawing.Size(79, 16);
            this.label8.Text = "ブラウザのパス";
            // 
            // MediaPlayerPathTextBox
            // 
            this.MediaPlayerPathTextBox.Location = new System.Drawing.Point(3, 71);
            this.MediaPlayerPathTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 52);
            this.label9.Size = new System.Drawing.Size(132, 16);
            this.label9.Text = "メディアプレーヤーのパス";
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
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Size = new System.Drawing.Size(188, 20);
            this.label5.Text = "ヘッドラインの自動チェック間隔(秒)";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.SettingTabControl);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // ヘッドラインチェックタイマーの上限と下限
            HeadlineTimerSecondNumericUpDown.Minimum = Controller.HeadlineCheckTimerMinimumMillSec / 1000;
            HeadlineTimerSecondNumericUpDown.Maximum = Controller.HeadlineCheckTimerMaximumMillSec / 1000;

            // 設定の読み込み
            MediaPlayerPathTextBox.Text = UserSetting.MediaPlayerPath;
            BrowserPathTextBox.Text = UserSetting.BrowserPath;
            HeadlineTimerSecondNumericUpDown.Text = (UserSetting.HeadlineTimerMillSecond / 1000).ToString();

            // 放送局情報の読み込み
            foreach (Station Station in StationList.GetStationList())
            {
                AlStationList.Add(Station);
                StationListBox.Items.Add(Station.GetDisplayName());
            }
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
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
                if (StationKindComboBox.Text.Trim().Equals("ねとらじ"))
                {
                    Station Station = new Station(DateTime.Now.ToString("yyyyMMddHHmmssff"), StationNameTextBox.Text.Trim(), Station.StationKindEnum.Netladio);
                    AlStationList.Add(Station);
                    StationListBox.Items.Add(Station.GetDisplayName());
                }
                else if (StationKindComboBox.Text.Trim().Equals("Podcast"))
                {
                    Station Station = new Station(DateTime.Now.ToString("yyyyMMddHHmmssff"), StationNameTextBox.Text.Trim(), Station.StationKindEnum.RssPodcast);
                    AlStationList.Add(Station);
                    StationListBox.Items.Add(Station.GetDisplayName());
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (StationListBox.SelectedIndex != -1)
            {
                AlStationList.RemoveAt(StationListBox.SelectedIndex);
                StationListBox.Items.RemoveAt(StationListBox.SelectedIndex);
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (StationListBox.SelectedIndex != -1)
            {
                AlStationList.RemoveAt(StationListBox.SelectedIndex);
                StationListBox.Items.RemoveAt(StationListBox.SelectedIndex);
            }
        }
    }
}
