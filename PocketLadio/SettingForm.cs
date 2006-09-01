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
    /// PocketLadioの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private MainMenu MainMenu;
        private MenuItem OkMenuItem;
        private ContextMenu MediaPlayeraPathContextMenu;
        private MenuItem CutMediaPlayeraPathMenuItem;
        private MenuItem CopyMediaPlayeraPathMenuItem;
        private MenuItem PasteMediaPlayeraPathMenuItem;
        private ContextMenu BrowserPathContextMenu;
        private MenuItem CutBrowserPathMenuItem;
        private MenuItem CopyBrowserPathMenuItem;
        private MenuItem PasteBrowserPathMenuItem;
        private TabPage PocketLadioTabPage;
        private TextBox BrowserPathTextBox;
        private TextBox MediaPlayerPathTextBox;
        private Label BrowserPathLabel;
        private Label MediaPlayerPathLabel;
        private NumericUpDown HeadlineTimerSecondNumericUpDown;
        private Label HeadlineTimerSecondLabel;
        private TabPage NetworkTabPage;
        private TextBox ProxyPortTextBox;
        private TextBox ProxyServerTextBox;
        private Label ProxyPortLabel;
        private Label ProxyServerLabel;
        private ContextMenu ProxyServerContextMenu;
        private MenuItem CutProxyServerMenuItem;
        private MenuItem CopyProxyServerMenuItem;
        private MenuItem PasteProxyServerMenuItem;
        private ContextMenu ProxyPortContextMenu;
        private MenuItem CutProxyPortMenuItem;
        private MenuItem CopyProxyPortMenuItem;
        private MenuItem PasteProxyPortMenuItem;
        private TabControl SettingTabControl;

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
            this.BrowserPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.MediaPlayeraPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.PocketLadioTabPage = new System.Windows.Forms.TabPage();
            this.BrowserPathTextBox = new System.Windows.Forms.TextBox();
            this.MediaPlayerPathTextBox = new System.Windows.Forms.TextBox();
            this.BrowserPathLabel = new System.Windows.Forms.Label();
            this.MediaPlayerPathLabel = new System.Windows.Forms.Label();
            this.HeadlineTimerSecondNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.HeadlineTimerSecondLabel = new System.Windows.Forms.Label();
            this.SettingTabControl = new System.Windows.Forms.TabControl();
            this.NetworkTabPage = new System.Windows.Forms.TabPage();
            this.ProxyPortTextBox = new System.Windows.Forms.TextBox();
            this.ProxyServerTextBox = new System.Windows.Forms.TextBox();
            this.ProxyPortLabel = new System.Windows.Forms.Label();
            this.ProxyServerLabel = new System.Windows.Forms.Label();
            this.ProxyServerContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.ProxyPortContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteProxyPortMenuItem = new System.Windows.Forms.MenuItem();
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
            // BrowserPathContextMenu
            // 
            this.BrowserPathContextMenu.MenuItems.Add(this.CutBrowserPathMenuItem);
            this.BrowserPathContextMenu.MenuItems.Add(this.CopyBrowserPathMenuItem);
            this.BrowserPathContextMenu.MenuItems.Add(this.PasteBrowserPathMenuItem);
            // 
            // CutBrowserPathMenuItem
            // 
            this.CutBrowserPathMenuItem.Text = "切り取り(&T)";
            this.CutBrowserPathMenuItem.Click += new System.EventHandler(this.CutBrowserPathMenuItem_Click);
            // 
            // CopyBrowserPathMenuItem
            // 
            this.CopyBrowserPathMenuItem.Text = "コピー(&C)";
            this.CopyBrowserPathMenuItem.Click += new System.EventHandler(this.CopyBrowserPathMenuItem_Click);
            // 
            // PasteBrowserPathMenuItem
            // 
            this.PasteBrowserPathMenuItem.Text = "貼り付け(&P)";
            this.PasteBrowserPathMenuItem.Click += new System.EventHandler(this.PasteBrowserPathMenuItem_Click);
            // 
            // MediaPlayeraPathContextMenu
            // 
            this.MediaPlayeraPathContextMenu.MenuItems.Add(this.CutMediaPlayeraPathMenuItem);
            this.MediaPlayeraPathContextMenu.MenuItems.Add(this.CopyMediaPlayeraPathMenuItem);
            this.MediaPlayeraPathContextMenu.MenuItems.Add(this.PasteMediaPlayeraPathMenuItem);
            // 
            // CutMediaPlayeraPathMenuItem
            // 
            this.CutMediaPlayeraPathMenuItem.Text = "切り取り(&T)";
            this.CutMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.CutMediaPlayeraPathMenuItem_Click);
            // 
            // CopyMediaPlayeraPathMenuItem
            // 
            this.CopyMediaPlayeraPathMenuItem.Text = "コピー(&C)";
            this.CopyMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.CopyMediaPlayeraPathMenuItem_Click);
            // 
            // PasteMediaPlayeraPathMenuItem
            // 
            this.PasteMediaPlayeraPathMenuItem.Text = "貼り付け(&P)";
            this.PasteMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.PasteMediaPlayeraPathMenuItem_Click);
            // 
            // PocketLadioTabPage
            // 
            this.PocketLadioTabPage.Controls.Add(this.BrowserPathTextBox);
            this.PocketLadioTabPage.Controls.Add(this.MediaPlayerPathTextBox);
            this.PocketLadioTabPage.Controls.Add(this.BrowserPathLabel);
            this.PocketLadioTabPage.Controls.Add(this.MediaPlayerPathLabel);
            this.PocketLadioTabPage.Controls.Add(this.HeadlineTimerSecondNumericUpDown);
            this.PocketLadioTabPage.Controls.Add(this.HeadlineTimerSecondLabel);
            this.PocketLadioTabPage.Location = new System.Drawing.Point(0, 0);
            this.PocketLadioTabPage.Size = new System.Drawing.Size(240, 245);
            this.PocketLadioTabPage.Text = "PocketLadio設定";
            // 
            // BrowserPathTextBox
            // 
            this.BrowserPathTextBox.ContextMenu = this.BrowserPathContextMenu;
            this.BrowserPathTextBox.Location = new System.Drawing.Point(3, 114);
            this.BrowserPathTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // MediaPlayerPathTextBox
            // 
            this.MediaPlayerPathTextBox.ContextMenu = this.MediaPlayeraPathContextMenu;
            this.MediaPlayerPathTextBox.Location = new System.Drawing.Point(3, 71);
            this.MediaPlayerPathTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // BrowserPathLabel
            // 
            this.BrowserPathLabel.Location = new System.Drawing.Point(3, 95);
            this.BrowserPathLabel.Size = new System.Drawing.Size(79, 16);
            this.BrowserPathLabel.Text = "ブラウザのパス";
            // 
            // MediaPlayerPathLabel
            // 
            this.MediaPlayerPathLabel.Location = new System.Drawing.Point(3, 52);
            this.MediaPlayerPathLabel.Size = new System.Drawing.Size(132, 16);
            this.MediaPlayerPathLabel.Text = "メディアプレーヤーのパス";
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
            this.HeadlineTimerSecondLabel.Text = "ヘッドラインの自動チェック間隔(秒)";
            // 
            // SettingTabControl
            // 
            this.SettingTabControl.Controls.Add(this.PocketLadioTabPage);
            this.SettingTabControl.Controls.Add(this.NetworkTabPage);
            this.SettingTabControl.Location = new System.Drawing.Point(0, 0);
            this.SettingTabControl.SelectedIndex = 0;
            this.SettingTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // NetworkTabPage
            // 
            this.NetworkTabPage.Controls.Add(this.ProxyPortTextBox);
            this.NetworkTabPage.Controls.Add(this.ProxyServerTextBox);
            this.NetworkTabPage.Controls.Add(this.ProxyPortLabel);
            this.NetworkTabPage.Controls.Add(this.ProxyServerLabel);
            this.NetworkTabPage.Location = new System.Drawing.Point(0, 0);
            this.NetworkTabPage.Size = new System.Drawing.Size(240, 245);
            this.NetworkTabPage.Text = "ネットワーク設定";
            // 
            // ProxyPortTextBox
            // 
            this.ProxyPortTextBox.ContextMenu = this.ProxyPortContextMenu;
            this.ProxyPortTextBox.Location = new System.Drawing.Point(3, 66);
            this.ProxyPortTextBox.Size = new System.Drawing.Size(74, 21);
            // 
            // ProxyServerTextBox
            // 
            this.ProxyServerTextBox.ContextMenu = this.ProxyServerContextMenu;
            this.ProxyServerTextBox.Location = new System.Drawing.Point(3, 23);
            this.ProxyServerTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // ProxyPortLabel
            // 
            this.ProxyPortLabel.Location = new System.Drawing.Point(3, 47);
            this.ProxyPortLabel.Size = new System.Drawing.Size(192, 16);
            this.ProxyPortLabel.Text = "プロキシのポート番号 （例： 8080）";
            // 
            // ProxyServerLabel
            // 
            this.ProxyServerLabel.Location = new System.Drawing.Point(3, 4);
            this.ProxyServerLabel.Size = new System.Drawing.Size(230, 16);
            this.ProxyServerLabel.Text = "プロキシサーバ （例：proxy.example.com）";
            // 
            // ProxyServerContextMenu
            // 
            this.ProxyServerContextMenu.MenuItems.Add(this.CutProxyServerMenuItem);
            this.ProxyServerContextMenu.MenuItems.Add(this.CopyProxyServerMenuItem);
            this.ProxyServerContextMenu.MenuItems.Add(this.PasteProxyServerMenuItem);
            // 
            // CutProxyServerMenuItem
            // 
            this.CutProxyServerMenuItem.Text = "切り取り(&T)";
            this.CutProxyServerMenuItem.Click += new System.EventHandler(this.CutProxyServerMenuItem_Click);
            // 
            // CopyProxyServerMenuItem
            // 
            this.CopyProxyServerMenuItem.Text = "コピー(&C)";
            this.CopyProxyServerMenuItem.Click += new System.EventHandler(this.CopyProxyServerMenuItem_Click);
            // 
            // PasteProxyServerMenuItem
            // 
            this.PasteProxyServerMenuItem.Text = "貼り付け(&P)";
            this.PasteProxyServerMenuItem.Click += new System.EventHandler(this.PasteProxyServerMenuItem_Click);
            // 
            // ProxyPortContextMenu
            // 
            this.ProxyPortContextMenu.MenuItems.Add(this.CutProxyPortMenuItem);
            this.ProxyPortContextMenu.MenuItems.Add(this.CopyProxyPortMenuItem);
            this.ProxyPortContextMenu.MenuItems.Add(this.PasteProxyPortMenuItem);
            // 
            // CutProxyPortMenuItem
            // 
            this.CutProxyPortMenuItem.Text = "切り取り(&T)";
            this.CutProxyPortMenuItem.Click += new System.EventHandler(this.CutProxyPortMenuItem_Click);
            // 
            // CopyProxyPortMenuItem
            // 
            this.CopyProxyPortMenuItem.Text = "コピー(&C)";
            this.CopyProxyPortMenuItem.Click += new System.EventHandler(this.CopyProxyPortMenuItem_Click);
            // 
            // PasteProxyPortMenuItem
            // 
            this.PasteProxyPortMenuItem.Text = "貼り付け(&P)";
            this.PasteProxyPortMenuItem.Click += new System.EventHandler(this.PasteProxyPortMenuItem_Click);
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
            ProxyServerTextBox.Text = UserSetting.ProxyServer;
            ProxyPortTextBox.Text = UserSetting.ProxyPort;
            HeadlineTimerSecondNumericUpDown.Text = (UserSetting.HeadlineTimerMillSecond / 1000).ToString();
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
            UserSetting.MediaPlayerPath = MediaPlayerPathTextBox.Text.Trim();
            UserSetting.BrowserPath = BrowserPathTextBox.Text.Trim();
            UserSetting.ProxyServer = ProxyServerTextBox.Text.Trim();
            UserSetting.ProxyPort = ProxyPortTextBox.Text.Trim();
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
            UserSetting.SaveSetting();
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
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

        private void CutProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(ProxyServerTextBox);
        }

        private void CopyProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(ProxyServerTextBox);
        }

        private void PasteProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(ProxyServerTextBox);
        }

        private void CutProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(ProxyPortTextBox);
        }

        private void CopyProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(ProxyPortTextBox);
        }

        private void PasteProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(ProxyPortTextBox);
        }
    }
}
