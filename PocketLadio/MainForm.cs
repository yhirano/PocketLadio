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
    /// アプリケーションのメインフォーム
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MenuItem MenuMenuItem;
        private System.Windows.Forms.MenuItem HeadlineCheckTimerMenuItem;
        private System.Windows.Forms.MenuItem FilterSettingMenuItem;
        private System.Windows.Forms.MenuItem StationSettingMenuItem;
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
        private ComboBox StationListComboBox;
        private System.Windows.Forms.Timer HeadlineCheckTimer;

        /// <summary>
        /// 選択されていた放送局のID
        /// </summary>
        private string SelectedStationID = "";

        public MainForm()
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
            this.MenuMenuItem = new System.Windows.Forms.MenuItem();
            this.HeadlineCheckTimerMenuItem = new System.Windows.Forms.MenuItem();
            this.FilterSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.PocketLadioSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.StationSettingMenuItem = new System.Windows.Forms.MenuItem();
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
            this.StationListComboBox = new System.Windows.Forms.ComboBox();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.Add(this.MenuMenuItem);
            // 
            // MenuMenuItem
            // 
            this.MenuMenuItem.MenuItems.Add(this.HeadlineCheckTimerMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.FilterSettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.PocketLadioSettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.StationSettingMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.VersionInfoMenuItem);
            this.MenuMenuItem.MenuItems.Add(this.SeparateMenuItem1);
            this.MenuMenuItem.MenuItems.Add(this.ExitMenuItem);
            this.MenuMenuItem.Text = "メニュー(&M)";
            // 
            // HeadlineCheckTimerMenuItem
            // 
            this.HeadlineCheckTimerMenuItem.Text = "ヘッドラインを一定間隔でチェック(&T)";
            this.HeadlineCheckTimerMenuItem.Click += new System.EventHandler(this.HeadlineCheckTimerMenuItem_Click);
            // 
            // FilterSettingMenuItem
            // 
            this.FilterSettingMenuItem.Text = "フィルター設定(&F)";
            this.FilterSettingMenuItem.Click += new System.EventHandler(this.FilterSettingMenuItem_Click);
            // 
            // PocketLadioSettingMenuItem
            // 
            this.PocketLadioSettingMenuItem.Text = "PocketLadio設定(&P)";
            this.PocketLadioSettingMenuItem.Click += new System.EventHandler(this.PocketLadioSettingMenuItem_Click);
            // 
            // StationSettingMenuItem
            // 
            this.StationSettingMenuItem.Text = "放送局の設定(&S)";
            // 
            // VersionInfoMenuItem
            // 
            this.VersionInfoMenuItem.Text = "バージョン情報(&A)";
            this.VersionInfoMenuItem.Click += new System.EventHandler(this.VersionInfoMenuItem_Click);
            // 
            // SeparateMenuItem1
            // 
            this.SeparateMenuItem1.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Text = "終了(&X)";
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
            this.PlayMenuItem.Text = "再生(&P)";
            this.PlayMenuItem.Click += new System.EventHandler(this.PlayMenuItem_Click);
            // 
            // BrowserMenuItem
            // 
            this.BrowserMenuItem.Text = "ブラウザでアクセス(&A)";
            this.BrowserMenuItem.Click += new System.EventHandler(this.BrowserMenuItem_Click);
            // 
            // ChanelPropertyMenuItem
            // 
            this.ChanelPropertyMenuItem.Text = "番組の詳細(&R)";
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
            this.StationListComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
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
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>

        static void Main()
        {
            Application.Run(new MainForm());
        }

        /// <summary>
        /// 番組リストを更新する
        /// </summary>
        /// <param name="chanels">番組の配列</param>
        private void UpdateRadioList(IChanel[] chanels)
        {
            HeadlineListBox.Items.Clear();

            foreach (IChanel Chanel in chanels)
            {
                HeadlineListBox.Items.Add(Chanel.GetChanelView());
            }
        }

        /// <summary>
        /// ストリーミングを再生する
        /// </summary>
        private void PlayStreaming()
        {
            if (HeadlineListBox.SelectedIndex != -1)
            {
                Controller.PlayStreaming(StationList.GetChanelsFilteredOfCurrentStation()[HeadlineListBox.SelectedIndex].GetPlayUrl());
            }
        }

        /// <summary>
        /// Webサイトへアクセスする
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
        /// ヘッドラインを取得、表示する
        /// </summary>
        private void CheckHeadline()
        {
            try
            {
                lock (this)
                {
                    StationList.WebGetHeadlineOfCurrentStation();
                    if (StationList.GetLastCheckTimeOfCurrentStation().Equals(DateTime.MinValue))
                    {
                        InfomationLabel.Text = "No Check - 0 CHs";
                    }
                    else
                    {
                        InfomationLabel.Text = "Last " + StationList.GetLastCheckTimeOfCurrentStation().ToString() + " - " + StationList.GetChanelsOfCurrentStation().Length.ToString() + " CHs";
                    }
                    UpdateRadioList(StationList.GetChanelsFilteredOfCurrentStation());
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
        /// タイマーのスタート時の処理
        /// </summary>
        private void HeadlineCheckTimerStart()
        {
            UserSetting.HeadlineTimerCheck = true;
            HeadlineCheckTimerMenuItem.Checked = true;
            HeadlineCheckTimer.Interval = UserSetting.HeadlineTimerMillSecond;
            HeadlineCheckTimer.Enabled = true;
        }

        /// <summary>
        /// タイマーのストップ時の処理
        /// </summary>
        private void HeadlineCheckTimerStop()
        {
            UserSetting.HeadlineTimerCheck = false;
            HeadlineCheckTimerMenuItem.Checked = false;
            HeadlineCheckTimer.Enabled = false;
        }

        /// <summary>
        /// タイマーのインターバルが変更されたときの処理
        /// </summary>
        /// <param name="intarval">タイマーのインターバル</param>
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
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する
        /// </summary>
        private void FixWindowSize()
        {
            // 水平モードの場合
            if (this.Size.Width > this.Size.Height)
            {
                // 横長のウィンドウ
                FixWindowSizeHorizon();
            }
            else
            {
                // 縦長のウィンドウ
                FixWindowSizeVertical();
            }
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（垂直）
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
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
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
        /// Station情報を持った継承MenuItemクラス
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
        /// Stationごとの設定をクリック
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
        /// 各Stationの設定メニューと切り替えボックスの追加
        /// </summary>
        private void StationsSettingAndCheckBoxAdd()
        {
            // 各Stationの設定メニューを追加
            this.StationSettingMenuItem.MenuItems.Clear();
            foreach (Station Station in StationList.GetStationList())
            {
                StationMenuItem EachStationSettingMenuItem = new StationMenuItem();
                this.StationSettingMenuItem.MenuItems.Add(EachStationSettingMenuItem);
                EachStationSettingMenuItem.Text = Station.GetDisplayName() + " 設定";
                EachStationSettingMenuItem.SetStation(Station);
                EachStationSettingMenuItem.Click += new System.EventHandler(this.EachStationSettingMenuItem_Click);
            }

            // 各Stationの切り替えボックスの追加
            StationListComboBox.Items.Clear();
            foreach (Station Station in StationList.GetStationList())
            {
                StationListComboBox.Items.Add(Station.GetDisplayName());
            }

            // 以前に選択されていた放送局を選択し直す
            for (int Count = 0; Count < StationListComboBox.Items.Count; Count++)
            {
                if (StationList.GetStationList()[Count].GetHeadlineID() == SelectedStationID)
                {
                    StationListComboBox.SelectedIndex = Count;
                    break;
                }
            }
            // 放送局が選択されておらず、かつ放送局がある場合
            if (StationListComboBox.SelectedIndex == -1 && StationListComboBox.Items.Count > 0)
            {
                // トップの放送局を選択
                StationListComboBox.SelectedIndex = 0;
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
            GetButton.Enabled = false;
            StationListComboBox.Enabled = false;

            try
            {
                CheckHeadline();
            }
            catch (WebException)
            {
                MessageBox.Show("番組表を取得できませんでした", "接続エラー");
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("メモリが足りません", "メモリエラー");
            }
            catch (IOException)
            {
                MessageBox.Show("記録デバイスが何らかのエラーです", "デバイスエラー");
            }
            catch (XmlException)
            {
                MessageBox.Show("XML形式のヘッドラインが正常に処理できませんでした", "XMLエラー");
            }
            finally
            {
                GetButton.Enabled = true;
                StationListComboBox.Enabled = true;
            }
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            PlayStreaming();
        }

        private void FilterCheckBox_CheckStateChanged(object sender, System.EventArgs e)
        {
            if (FilterCheckBox.Checked == true)
            {
                StationList.FilterEnable = true;
            }
            else
            {
                StationList.FilterEnable = false;
            }
            UpdateRadioList(StationList.GetChanelsFilteredOfCurrentStation());
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

            // 番組リストがある場合
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
            GetButton.Enabled = false;
            StationListComboBox.Enabled = false;

            try
            {
                CheckHeadline();
            }
            catch (WebException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("番組表を取得できませんでした", "接続エラー");
            }
            catch (OutOfMemoryException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("メモリが足りません", "メモリエラー");
            }
            catch (IOException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("記録デバイスが何らかのエラーです", "デバイスエラー");
            }
            catch (XmlException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("XML形式のヘッドラインが正常に処理できませんでした", "XMLエラー");
            }
            finally
            {
                GetButton.Enabled = true;
                StationListComboBox.Enabled = true;
            }
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
            // 入力ボタンを押したとき
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
            
            // 選択していた放送局を記憶する
            if (StationListComboBox.SelectedIndex != -1)
            {
                SelectedStationID = StationList.GetHeadlineIDOfCurrentStation();
            }
        }
    }
}
