#region ディレクティブを使用する

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
    /// アプリケーションのメインフォーム
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// 選択されていた放送局のID
        /// </summary>
        private string selectedStationID = string.Empty;

        /// <summary>
        /// CheckHeadline()の動作排他処理のためのフラグ
        /// </summary>
        private bool checkHeadlineNowFlag;

        /// <summary>
        /// アンカーコントロールのリスト
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

        /// <summary>
        /// フィルタリング中か
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
            this.channelMenuItem.Text = "番組(&C)";
            this.channelMenuItem.Popup += new System.EventHandler(this.channelMenuItem_Popup);
            // 
            // playOfChannelMenuItem
            // 
            this.playOfChannelMenuItem.Text = "再生(&P)";
            this.playOfChannelMenuItem.Click += new System.EventHandler(this.playOfChannelMenuItem_Click);
            // 
            // browserOfChannelMenuItem
            // 
            this.browserOfChannelMenuItem.Text = "Webサイトにアクセス(&A)";
            this.browserOfChannelMenuItem.Click += new System.EventHandler(this.browserOfChannelMenuItem_Click);
            // 
            // channelPropertyOfChannelMenuItem
            // 
            this.channelPropertyOfChannelMenuItem.Text = "番組の詳細(&P)";
            this.channelPropertyOfChannelMenuItem.Click += new System.EventHandler(this.channelPropertyOfChannelMenuItem_Click);
            // 
            // separateMenuItem5
            // 
            this.separateMenuItem5.Text = "-";
            // 
            // updateMenuItem
            // 
            this.updateMenuItem.Text = "ヘッドライン更新(&U)";
            this.updateMenuItem.Click += new System.EventHandler(this.updateMenuItem_Click);
            // 
            // filterMenuItem
            // 
            this.filterMenuItem.Text = "フィルター(&F)";
            this.filterMenuItem.Click += new System.EventHandler(this.filterMenuItem_Click);
            // 
            // separateMenuItem6
            // 
            this.separateMenuItem6.Text = "-";
            // 
            // selectStationMenuItem
            // 
            this.selectStationMenuItem.Text = "放送局の選択(&S)";
            this.selectStationMenuItem.Click += new System.EventHandler(this.selectStationMenuItem_Click);
            // 
            // selectChannelMenuItem
            // 
            this.selectChannelMenuItem.Text = "番組の選択(&C)";
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
            this.playMenuItem.Text = "再生(&P)";
            this.playMenuItem.Click += new System.EventHandler(this.PlayMenuItem_Click);
            // 
            // browserMenuItem
            // 
            this.browserMenuItem.Text = "Webサイトにアクセス(&A)";
            this.browserMenuItem.Click += new System.EventHandler(this.BrowserMenuItem_Click);
            // 
            // separateMenuItem7
            // 
            this.separateMenuItem7.Text = "-";
            // 
            // addFilterMenuItem
            // 
            this.addFilterMenuItem.Text = "フィルターに登録(&F)";
            this.addFilterMenuItem.Click += new System.EventHandler(this.addFilterMenuItem_Click);
            // 
            // addPlayUrlToFilterMenuItem
            // 
            this.addPlayUrlToFilterMenuItem.Text = "再生URLをフィルターに登録(&I)";
            this.addPlayUrlToFilterMenuItem.Click += new System.EventHandler(this.addPlayUrlToFilterMenuItem_Click);
            // 
            // separateMenuItem8
            // 
            this.separateMenuItem8.Text = "-";
            // 
            // channelPropertyMenuItem
            // 
            this.channelPropertyMenuItem.Text = "番組の詳細(&R)";
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
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>

        static void Main()
        {
            try
            {
                Application.Run(new MainForm());

                // 終了処理
                try
                {
                    // 終了時処理
                    PocketLadioSpecificProcess.ExitDisable();
                }
                catch (IOException)
                {
                    MessageBox.Show("設定ファイルが書き込めませんでした", "設定ファイル書き込みエラー");
                }
            }
            catch (Exception ex)
            {
                // ログに例外情報を書き込む
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
                // デバッガで例外内容を確認するため、例外をアプリケーションの外に出す
                throw ex;
#else
                Trace.Assert(false, "予期しないエラーが発生したため、終了します");
#endif
            }
        }

        /// <summary>
        /// 番組リストを更新する
        /// </summary>
        /// <param name="channels">番組の配列</param>
        private void DrawChannelList(IChannel[] channels)
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
            if (headlineListBox.SelectedIndex != -1 && headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
            {
                Uri playUrl = StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex].GetPlayUrl();

                // URLが空の場合は、警告を出して終了
                if (playUrl == null)
                {
                    MessageBox.Show("番組のURLがありません", "再生エラー");
                    return;
                }

                try
                {
                    if (UserSetting.PlayListSave == false)
                    {
                        PocketLadioUtility.PlayStreaming(playUrl);
                    }
                    // 番組がプレイリストだった場合に、一端ローカルに保存する
                    else
                    {
                        string playListExtension = Path.GetExtension(playUrl.AbsolutePath);

                        if (IsPlayListExtension(playListExtension) == true)
                        {
                            // 既存のプレイリストを削除
                            foreach (string extension in PocketLadioInfo.PlayListExtensions)
                            {
                                if (File.Exists(AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.GeneratePlayListFileName + extension))
                                {
                                    File.Delete(AssemblyUtility.GetExecutablePath() + @"\" + PocketLadioInfo.GeneratePlayListFileName + extension);
                                }
                            }

                            // 一端プレイリストをローカルに保存して、それをプレーヤーに渡す。
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
                    MessageBox.Show("メディアプレイヤーが見つかりません", "警告");
                }
                catch (WebException)
                {
                    MessageBox.Show(playUrl + " が見つかりません", "警告");
                }
            }
        }

        /// <summary>
        /// 拡張子がプレイリストかを判定する
        /// </summary>
        /// <param name="extension">拡張子</param>
        /// <returns>プレイリストだった場合はtrue、そうでない場合はfalse</returns>
        private bool IsPlayListExtension(string extension)
        {
            // プレイリストかを判定
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
        /// 番組の詳細フォームを表示する
        /// </summary>
        private void ShowStationProperty()
        {
            if (headlineListBox.SelectedIndex != -1 && headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
            {
                StationList.ShowPropertyFormOfCurrentStation(StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex]);
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

            #region UI前処理

            // フォーム内コントロールをいったん選択不可にする
            UpdateButtonEnable(false);
            PlayButtonEnable(false);
            filterCheckBox.Enabled = false;
            stationListComboBox.Enabled = false;
            headlineListBox.Enabled = false;
            updateMenuItem.Enabled = false;

            #endregion

            mainStatusBar.Text = "接続中...";
            mainStatusBar.Refresh();

            try
            {
                #region 番組取得処理

                // 番組を取得する
                StationList.FetchHeadlineOfCurrentStation();

                // 番組が取得できなかった場合
                if (StationList.LastCheckTimeOfCurrentStation.Equals(DateTime.MinValue))
                {
                    mainStatusBar.Text = "No Check - 0 CHs";
                }
                // 番組が取得できた場合
                else
                {
                    mainStatusBar.Text = "Last " + StationList.LastCheckTimeOfCurrentStation.ToString()
                        + " - " + StationList.GetChannelsOfCurrentStation().Length.ToString() + " CHs";
                }

                #endregion

                // 番組リストを更新する
                DrawChannelList(StationList.GetChannelsFilteredOfCurrentStation());
            }
            catch (WebException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("番組表を取得できませんでした", "接続エラー");
            }
            catch (OutOfMemoryException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("メモリが足りません", "メモリエラー");
            }
            catch (IOException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("記録デバイスが何らかのエラーです", "デバイスエラー");
            }
            catch (UriFormatException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.StationNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            catch (SocketException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("番組表を取得できませんでした", "ネットワークエラー");
            }
            catch (NotSupportedException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.StationNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            catch (XmlException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show("XML形式のヘッドラインが正常に処理できませんでした", "XMLエラー");
            }
            catch (ArgumentException)
            {
                mainStatusBar.Text = "No Check - 0 CHs";
                HeadlineCheckTimerStop();
                MessageBox.Show(StationList.StationNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            finally
            {
                #region  UI後処理

                // フォーム内コントロールを選択可能に回復する
                UpdateButtonEnable(true);
                PlayButtonEnable(true);
                filterCheckBox.Enabled = true;
                stationListComboBox.Enabled = true;
                headlineListBox.Enabled = true;
                updateMenuItem.Enabled = true;
                WriteSelectedChannelInfomation();

                #endregion

                // 排他処理のためのフラグを下げる
                checkHeadlineNowFlag = false;
            }
        }

        /// <summary>
        /// フィルタリングを切り替える
        /// </summary>
        /// <param name="filtering">フィルタリングを有効にするか</param>
        private void Filtering(bool filtering)
        {
            if (isFiltering != filtering)
            {
                #region UI前処理

                // フォーム内コントロールをいったん選択不可にする
                UpdateButtonEnable(false);
                PlayButtonEnable(false);
                filterCheckBox.Enabled = false;
                stationListComboBox.Enabled = false;
                headlineListBox.Enabled = false;
                updateMenuItem.Enabled = false;

                #endregion

                #region フィルタリング処理

                if (filtering == true)
                {
                    StationList.FilterEnable = true;
                    // フィルターの有効無効設定をオンにする
                    UserSetting.FilterEnable = true;
                }
                else
                {
                    StationList.FilterEnable = false;
                    // フィルターの有効無効設定をオフにする
                    UserSetting.FilterEnable = false;
                }
                DrawChannelList(StationList.GetChannelsFilteredOfCurrentStation());

                #endregion

                #region UI後処理

                // フォーム内コントロールを選択可能に回復する
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
        /// UpdateButtonを有効にする。
        /// UpdateButtonが有効の指定でも、法則局の選択がない場合にはUpdateButtonを無効にする。
        /// </summary>
        private void UpdateButtonEnable()
        {
            UpdateButtonEnable(true);
        }

        /// <summary>
        /// UpdateButtonを有効にする。
        /// UpdateButtonが有効の指定でも、法則局の選択がない場合にはUpdateButtonを無効にする。
        /// </summary>
        /// <param name="enable">UpdateButtonを有効にするか</param>
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
        /// PlayButtonの有効にする。
        /// PlayButtonが有効の指定でも、リストボックスに選択がない場合と、選択したリストボックスのコンテンツが
        /// 再生内容を持っていないときにはPlayButtonを無効にする。
        /// </summary>
        private void PlayButtonEnable()
        {
            PlayButtonEnable(true);
        }

        /// <summary>
        /// PlayButtonの有効無効を切り替える。
        /// PlayButtonが有効の指定でも、リストボックスに選択がない場合と、選択したリストボックスのコンテンツが
        /// 再生内容を持っていないときにはPlayButtonを無効にする。
        /// </summary>
        /// <param name="enable">PlayButtonを有効にするか</param>
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
        /// 選択している番組の情報をラベルに書く
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
        /// コントロールにアンカーをセットする
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
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する
        /// </summary>
        private void FixWindowSize()
        {
            foreach (AnchorLayout anchorLayout in anchorControlList)
            {
                anchorLayout.LayoutControl();
            }
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
        /// 各放送局の設定メニューと切り替えボックスの追加
        /// </summary>
        private void AddStationsSettingAndComboBoxItem()
        {
            // 放送局が存在する場合の処理
            // 各放送局の設定メニュー追加と切り替えボックス追加を行う
            if (StationList.GetStationList().Length != 0)
            {
                #region 各放送局の設定メニューを追加処理

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

                #endregion

                #region  各放送局の切り替えボックスを追加処理

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

                #endregion

                // 以前に選択されていた放送局を選択し直す
                for (int count = 0; count < stationListComboBox.Items.Count; ++count)
                {
                    if (StationList.GetStationList()[count].Id == selectedStationID)
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
                // 選択中の番組
                IChannel channel = StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex];

                // 選択中の番組の表示名をフィルターに登録する
                StationList.StationHeadlineOfCurrentStation.ShowSettingFormForAddFilter(channel.GetChannelView());
            }
        }

        private void addPlayUrlToFilterMenuItem_Click(object sender, EventArgs e)
        {
            if (StationList.StationHeadlineOfCurrentStation != null
                && headlineListBox.SelectedIndex != -1
                && headlineListBox.SelectedIndex < StationList.GetChannelsFilteredOfCurrentStation().Length)
            {
                // 選択中の番組
                IChannel channel = StationList.GetChannelsFilteredOfCurrentStation()[headlineListBox.SelectedIndex];

                // 選択中の番組のPlayURLをフィルターに登録する
                StationList.StationHeadlineOfCurrentStation.ShowSettingFormForAddFilter(channel.GetPlayUrl().ToString());
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                // 起動時のチェック
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
                // 起動時の初期化
                PocketLadioSpecificProcess.StartUpInitialize();

                // ヘッドラインタイマーの設定が有効な場合、タイマーを動作させる
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

                // 番組リストがある場合
                if (StationList.GetChannelsFilteredOfCurrentStation().Length > 0)
                {
                    DrawChannelList(StationList.GetChannelsFilteredOfCurrentStation());
                }

                // フィルターの有効無効設定が有効な場合、フィルターを動作させる
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
                mainStatusBar.Text = string.Format("ヘッドライン取得 {0}KB / {1}KB", e.FetchedSize / 1024, e.ContentSize / 1024);
            }
            else
            {
                mainStatusBar.Text = string.Format("ヘッドライン取得 {0}KB", e.FetchedSize / 1024);
            }
            mainStatusBar.Refresh();
        }

        void StationList_HeadlineAnalyzing(object sender, HeadlineAnalyzeEventArgs e)
        {
            if (e.IsUnknownWholeCount == false)
            {
                mainStatusBar.Text = string.Format("番組解析中 {0} / {1}", e.AnalyzedCount, e.WholeCount);
            }
            else
            {
                mainStatusBar.Text = string.Format("番組解析中 {0}", e.AnalyzedCount);
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
                // 入力ボタンを押したとき
                case Keys.Enter:
                    PlayStreaming();
                    break;
            }
        }

        private void StationListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region UI前処理

            // フォーム内コントロールをいったん選択不可にする
            UpdateButtonEnable(false);
            PlayButtonEnable(false);
            filterCheckBox.Enabled = false;
            headlineListBox.Enabled = false;
            updateMenuItem.Enabled = false;

            #endregion

            #region 切り替え処理

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

            // 選択していた放送局を記憶する
            if (stationListComboBox.SelectedIndex != -1)
            {
                selectedStationID = StationList.StationIdOfCurrentStation;
            }

            #endregion

            #region UI後処理

            // フォーム内コントロールを選択可能に回復する
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

            // 番組リストが選択されていない場合は、先頭の項目を選択する
            if (headlineListBox.SelectedIndex == -1 && headlineListBox.Items.Count > 0)
            {
                headlineListBox.SelectedIndex = 0;
                headlineListBox.Focus();
            }
        }

        #region コンボボックスのドロップダウンを開く

        [DllImport("coredll.dll")]
        private static extern IntPtr GetCapture();

        [DllImport("coredll.dll")]
        static extern IntPtr SetCapture(IntPtr hWnd);

        /// <summary>
        /// ControlのWindow handleを返す
        /// </summary>
        /// <param name="control">Control</param>
        /// <returns>ControlのWindow handle</returns>
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
        /// コンボボックスのドロップダウンを開く
        /// </summary>
        /// <param name="comboBox">ドロップダウンを開くコンボボックス</param>
        private void droppedDownCombobox(ComboBox comboBox)
        {
            SendMessage(GetHWnd(comboBox), CB_SHOWDROPDOWN, TRUE, 0);
        }

        #endregion // コンボボックスのドロップダウンを開く

        private void selectStationMenuItem_Click(object sender, EventArgs e)
        {
            stationListComboBox.Focus();
            droppedDownCombobox(stationListComboBox);
        }

        private void selectChannelMenuItem_Click(object sender, EventArgs e)
        {
            headlineListBox.Focus();
            // アイテムを選択する
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
