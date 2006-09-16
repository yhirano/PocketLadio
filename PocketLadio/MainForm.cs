#region ディレクティブを使用する

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
    /// アプリケーションのメインフォーム
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
        /// 選択されていた放送局のID
        /// </summary>
        private string selectedStationID = "";

        /// <summary>
        /// CheckHeadline()の動作排他処理のためのフラグ
        /// </summary>
        private bool checkHeadlineNowFlag;

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
            this.menuMenuItem.Text = "メニュー(&M)";
            // 
            // headlineCheckTimerMenuItem
            // 
            this.headlineCheckTimerMenuItem.Text = "ヘッドラインを一定間隔でチェック(&T)";
            this.headlineCheckTimerMenuItem.Click += new System.EventHandler(this.HeadlineCheckTimerMenuItem_Click);
            // 
            // separateMenuItem1
            // 
            this.separateMenuItem1.Text = "-";
            // 
            // stationsSettingMenuItem
            // 
            this.stationsSettingMenuItem.Text = "放送局の追加と削除 (&A)";
            this.stationsSettingMenuItem.Click += new System.EventHandler(this.StationsSettingMenuItem_Click);
            // 
            // stationSettingMenuItem
            // 
            this.stationSettingMenuItem.Text = "放送局の設定(&S)";
            // 
            // filterSettingMenuItem
            // 
            this.filterSettingMenuItem.Text = "フィルターの追加と削除(&F)";
            this.filterSettingMenuItem.Click += new System.EventHandler(this.FilterSettingMenuItem_Click);
            // 
            // separateMenuItem2
            // 
            this.separateMenuItem2.Text = "-";
            // 
            // pocketLadioSettingMenuItem
            // 
            this.pocketLadioSettingMenuItem.Text = "PocketLadio設定(&P)";
            this.pocketLadioSettingMenuItem.Click += new System.EventHandler(this.PocketLadioSettingMenuItem_Click);
            // 
            // separateMenuItem3
            // 
            this.separateMenuItem3.Text = "-";
            // 
            // versionInfoMenuItem
            // 
            this.versionInfoMenuItem.Text = "バージョン情報(&A)";
            this.versionInfoMenuItem.Click += new System.EventHandler(this.VersionInfoMenuItem_Click);
            // 
            // separateMenuItem4
            // 
            this.separateMenuItem4.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Text = "終了(&X)";
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
            this.playMenuItem.Text = "再生(&P)";
            this.playMenuItem.Click += new System.EventHandler(this.PlayMenuItem_Click);
            // 
            // browserMenuItem
            // 
            this.browserMenuItem.Text = "ブラウザでアクセス(&A)";
            this.browserMenuItem.Click += new System.EventHandler(this.BrowserMenuItem_Click);
            // 
            // channelPropertyMenuItem
            // 
            this.channelPropertyMenuItem.Text = "番組の詳細(&R)";
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
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>

        static void Main()
        {
            Application.Run(new MainForm());
        }

        /// <summary>
        /// 番組リストを更新する
        /// </summary>
        /// <param name="channels">番組の配列</param>
        private void UpdateRadioList(IChannel[] channels)
        {
            // いったん番組リストを画面から消す
            headlineListBox.Visible = false;

            headlineListBox.Items.Clear();
            foreach (IChannel channel in channels)
            {
                headlineListBox.Items.Add(channel.GetChannelView());
            }

            // 番組リストを描画する
            headlineListBox.Visible = true;
        }

        /// <summary>
        /// ストリーミングを再生する
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
                MessageBox.Show("メディアプレイヤーが見つかりません", "警告");
            }
        }

        /// <summary>
        /// Webサイトへアクセスする
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
                MessageBox.Show("ブラウザが見つかりません", "警告");
            }
        }

        /// <summary>
        /// ヘッドラインを取得、表示する
        /// </summary>
        private void CheckHeadline()
        {
            // CheckHeadline()が処理の場合は何もせず終了
            if (checkHeadlineNowFlag == true)
            {
                return;
            }

            // 排他処理のためのフラグを立てる
            checkHeadlineNowFlag = true;

            // 放送局選択ボックスが選択可能だったフラグ
            bool StationListComboBoxEnabledFlag = false;

            try
            {
                /** UI前処理 **/
                // GetボタンとFilterチェックボックスをいったん選択不可にする
                getButton.Enabled = false;
                filterCheckBox.Enabled = false;

                // 放送局選択ボックスが選択可能だった場合にのみ、いったん選択不可にする
                // （放送局がひとつも設定されていない場合には、元々選択不可のため）
                if (stationListComboBox.Enabled == true)
                {
                    stationListComboBox.Enabled = false;
                    // 放送局選択ボックスが選択可能だったフラグを立てる
                    StationListComboBoxEnabledFlag = true;
                }

                /** 番組取得処理 **/
                // 番組を取得する
                StationList.WebGetHeadlineOfCurrentStation();

                // 番組が取得できなかった場合
                if (StationList.LastCheckTimeOfCurrentStation.Equals(DateTime.MinValue))
                {
                    infomationLabel.Text = "No Check - 0 CHs";
                }
                // 番組が取得できた場合
                else
                {
                    infomationLabel.Text = "Last " + StationList.LastCheckTimeOfCurrentStation.ToString()
                        + " - " + StationList.GetChannelsOfCurrentStation().Length.ToString() + " CHs";
                }

                // 番組リストを更新する
                UpdateRadioList(StationList.GetChannelsFilteredOfCurrentStation());
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
            catch (UriFormatException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.HeadlineNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            catch (SocketException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("番組表を取得できませんでした", "ネットワークエラー");
            }
            catch (NotSupportedException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.HeadlineNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            catch (XmlException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show("XML形式のヘッドラインが正常に処理できませんでした", "XMLエラー");
            }
            catch (ArgumentException)
            {
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.HeadlineNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            finally
            {
                /** UI後処理 **/
                // GetボタンとFilterチェックボックスを選択可能に回復する
                getButton.Enabled = true;
                filterCheckBox.Enabled = true;

                // 放送局選択ボックスが選択可能だった場合にのみ、選択可能に回復する
                // （放送局がひとつも設定されていない場合には、元々選択不可のため）
                if (StationListComboBoxEnabledFlag == true)
                {
                    stationListComboBox.Enabled = true;
                }

                // 排他処理のためのフラグを下げる
                checkHeadlineNowFlag = false;
            }
        }

        /// <summary>
        /// タイマーのスタート時の処理
        /// </summary>
        private void HeadlineCheckTimerStart()
        {
            UserSetting.HeadlineTimerCheck = true;
            headlineCheckTimerMenuItem.Checked = true;
            headlineCheckTimer.Interval = UserSetting.HeadlineTimerMillSecond;
            headlineCheckTimer.Enabled = true;
        }

        /// <summary>
        /// タイマーのストップ時の処理
        /// </summary>
        private void HeadlineCheckTimerStop()
        {
            UserSetting.HeadlineTimerCheck = false;
            headlineCheckTimerMenuItem.Checked = false;
            headlineCheckTimer.Enabled = false;
        }

        /// <summary>
        /// タイマーのインターバルが変更されたときの処理
        /// </summary>
        /// <param name="interval">タイマーのインターバル</param>
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
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
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
        /// 放送局情報を持った継承MenuItemクラス
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
        /// 放送局ごとの設定をクリック
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
        /// 各放送局の設定メニューと切り替えボックスの追加
        /// </summary>
        private void StationsSettingAndCheckBoxAdd()
        {
            // 放送局が存在する場合の処理
            // 各放送局の設定メニュー追加と切り替えボックス追加を行う
            if (StationList.GetStationList().Length != 0)
            {
                // 各放送局の設定メニューを追加処理
                {
                    // 各放送局の設定メニューをいったん選択不可にする
                    this.stationSettingMenuItem.Enabled = false;
                    // 各放送局の設定メニューをいったんクリアする
                    this.stationSettingMenuItem.MenuItems.Clear();
                    // 各放送局の設定メニューを追加
                    foreach (Station station in StationList.GetStationList())
                    {
                        StationMenuItem EachStationSettingMenuItem = new StationMenuItem();
                        this.stationSettingMenuItem.MenuItems.Add(EachStationSettingMenuItem);
                        EachStationSettingMenuItem.Text = station.DisplayName + " 設定";
                        EachStationSettingMenuItem.SetStation(station);
                        EachStationSettingMenuItem.Click += new System.EventHandler(this.EachStationSettingMenuItem_Click);
                    }
                    // 設定メニューが追加し終わったので、各放送局の設定メニューを選択可能にする
                    this.stationSettingMenuItem.Enabled = true;
                }

                // 各放送局の切り替えボックスを追加処理
                {
                    // 各放送局の切り替えボックスをいったん選択不可にする
                    this.stationListComboBox.Enabled = false;
                    // 各放送局の切り替えボックスをいったんクリアする
                    this.stationListComboBox.Items.Clear();
                    // 各放送局の切り替えボックスの追加
                    foreach (Station station in StationList.GetStationList())
                    {
                        this.stationListComboBox.Items.Add(station.DisplayName);
                    }
                    // 切り替えボックスが追加し終わったので、各放送局の切り替えボックスを選択可能にする
                    this.stationListComboBox.Enabled = true;
                }

                // 以前に選択されていた放送局を選択し直す
                for (int count = 0; count < stationListComboBox.Items.Count; ++count)
                {
                    if (StationList.GetStationList()[count].HeadlineId == selectedStationID)
                    {
                        this.stationListComboBox.SelectedIndex = count;
                        break;
                    }
                }

                // 放送局が選択されておらず、かつ放送局がある場合
                if (this.stationListComboBox.SelectedIndex == -1 && this.stationListComboBox.Items.Count > 0)
                {
                    // トップの放送局を選択
                    this.stationListComboBox.SelectedIndex = 0;
                }
            }
            // 放送局が存在しない場合の処理
            // 放送局の設定メニューと切り替えボックスを選択不可にする
            else
            {
                // 各放送局の設定メニューを選択不可にする
                this.stationSettingMenuItem.Enabled = false;
                // 各放送局の設定メニューをクリアする
                this.stationSettingMenuItem.MenuItems.Clear();

                // 各放送局の切り替えボックスを選択不可にする
                this.stationListComboBox.Enabled = false;
                // 各放送局の切り替えボックスをクリアする
                this.stationListComboBox.Items.Clear();
            }
        }

        /// <summary>
        /// 設定の読み込み
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                // 起動時の初期化
                PocketLadioUtility.StartUpInitialize();

                // ヘッドラインが有効な場合、ヘッドラインを動作させる
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
                if (StationList.GetChannelsFilteredOfCurrentStation().Length > 0)
                {
                    UpdateRadioList(StationList.GetChannelsFilteredOfCurrentStation());
                }
            }
            catch (XmlException)
            {
                MessageBox.Show("設定ファイルが読み込めませんでした", "設定ファイルの解析エラー");
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが読み込めませんでした", "設定ファイルの読み込みエラー");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("設定ファイルが読み込めませんでした", "設定ファイルの読み込みエラー");
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
            /** UI前処理 **/
            // Filterチェックボックスをいったん選択不可にする
            filterCheckBox.Enabled = false;

            /** フィルタリング処理 **/
            if (filterCheckBox.Checked == true)
            {
                StationList.FilterEnable = true;
            }
            else
            {
                StationList.FilterEnable = false;
            }
            UpdateRadioList(StationList.GetChannelsFilteredOfCurrentStation());

            /** UI後処理 **/
            // Filterチェックボックスを選択可能に回復する
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
            // 初期化処理
            LoadSettings();

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
            try
            {
                // 設定ファイルの書き込み
                UserSetting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが書き込めませんでした", "設定ファイル書き込みエラー");
            }
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
            // 入力ボタンを押したとき
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

            // 選択していた放送局を記憶する
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
