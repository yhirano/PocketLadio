#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// PocketLadioの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private MainMenu mainMenu;
        private MenuItem okMenuItem;
        private ContextMenu mediaPlayeraPathContextMenu;
        private MenuItem cutMediaPlayeraPathMenuItem;
        private MenuItem copyMediaPlayeraPathMenuItem;
        private MenuItem pasteMediaPlayeraPathMenuItem;
        private ContextMenu browserPathContextMenu;
        private MenuItem cutBrowserPathMenuItem;
        private MenuItem copyBrowserPathMenuItem;
        private MenuItem pasteBrowserPathMenuItem;
        private TabPage pocketLadioTabPage;
        private TextBox browserPathTextBox;
        private TextBox mediaPlayerPathTextBox;
        private Label browserPathLabel;
        private Label mediaPlayerPathLabel;
        private NumericUpDown headlineTimerSecondNumericUpDown;
        private Label headlineTimerSecondLabel;
        private TabPage networkTabPage;
        private TextBox proxyPortTextBox;
        private TextBox proxyServerTextBox;
        private Label proxyPortLabel;
        private Label proxyServerLabel;
        private ContextMenu proxyServerContextMenu;
        private MenuItem cutProxyServerMenuItem;
        private MenuItem copyProxyServerMenuItem;
        private MenuItem pasteProxyServerMenuItem;
        private ContextMenu proxyPortContextMenu;
        private MenuItem cutProxyPortMenuItem;
        private MenuItem copyProxyPortMenuItem;
        private MenuItem pasteProxyPortMenuItem;
        private CheckBox proxyUseCheckBox;
        private TabControl settingTabControl;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.browserPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.copyBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.mediaPlayeraPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.pocketLadioTabPage = new System.Windows.Forms.TabPage();
            this.browserPathTextBox = new System.Windows.Forms.TextBox();
            this.mediaPlayerPathTextBox = new System.Windows.Forms.TextBox();
            this.browserPathLabel = new System.Windows.Forms.Label();
            this.mediaPlayerPathLabel = new System.Windows.Forms.Label();
            this.headlineTimerSecondNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.headlineTimerSecondLabel = new System.Windows.Forms.Label();
            this.settingTabControl = new System.Windows.Forms.TabControl();
            this.networkTabPage = new System.Windows.Forms.TabPage();
            this.proxyUseCheckBox = new System.Windows.Forms.CheckBox();
            this.proxyPortTextBox = new System.Windows.Forms.TextBox();
            this.proxyPortContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.copyProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.proxyServerTextBox = new System.Windows.Forms.TextBox();
            this.proxyServerContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.copyProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.proxyPortLabel = new System.Windows.Forms.Label();
            this.proxyServerLabel = new System.Windows.Forms.Label();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.okMenuItem);
            // 
            // okMenuItem
            // 
            this.okMenuItem.Text = "&OK";
            this.okMenuItem.Click += new System.EventHandler(this.OkMenuItem_Click);
            // 
            // browserPathContextMenu
            // 
            this.browserPathContextMenu.MenuItems.Add(this.cutBrowserPathMenuItem);
            this.browserPathContextMenu.MenuItems.Add(this.copyBrowserPathMenuItem);
            this.browserPathContextMenu.MenuItems.Add(this.pasteBrowserPathMenuItem);
            // 
            // cutBrowserPathMenuItem
            // 
            this.cutBrowserPathMenuItem.Text = "切り取り(&T)";
            this.cutBrowserPathMenuItem.Click += new System.EventHandler(this.CutBrowserPathMenuItem_Click);
            // 
            // copyBrowserPathMenuItem
            // 
            this.copyBrowserPathMenuItem.Text = "コピー(&C)";
            this.copyBrowserPathMenuItem.Click += new System.EventHandler(this.CopyBrowserPathMenuItem_Click);
            // 
            // pasteBrowserPathMenuItem
            // 
            this.pasteBrowserPathMenuItem.Text = "貼り付け(&P)";
            this.pasteBrowserPathMenuItem.Click += new System.EventHandler(this.PasteBrowserPathMenuItem_Click);
            // 
            // mediaPlayeraPathContextMenu
            // 
            this.mediaPlayeraPathContextMenu.MenuItems.Add(this.cutMediaPlayeraPathMenuItem);
            this.mediaPlayeraPathContextMenu.MenuItems.Add(this.copyMediaPlayeraPathMenuItem);
            this.mediaPlayeraPathContextMenu.MenuItems.Add(this.pasteMediaPlayeraPathMenuItem);
            // 
            // cutMediaPlayeraPathMenuItem
            // 
            this.cutMediaPlayeraPathMenuItem.Text = "切り取り(&T)";
            this.cutMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.CutMediaPlayeraPathMenuItem_Click);
            // 
            // copyMediaPlayeraPathMenuItem
            // 
            this.copyMediaPlayeraPathMenuItem.Text = "コピー(&C)";
            this.copyMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.CopyMediaPlayeraPathMenuItem_Click);
            // 
            // pasteMediaPlayeraPathMenuItem
            // 
            this.pasteMediaPlayeraPathMenuItem.Text = "貼り付け(&P)";
            this.pasteMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.PasteMediaPlayeraPathMenuItem_Click);
            // 
            // pocketLadioTabPage
            // 
            this.pocketLadioTabPage.Controls.Add(this.browserPathTextBox);
            this.pocketLadioTabPage.Controls.Add(this.mediaPlayerPathTextBox);
            this.pocketLadioTabPage.Controls.Add(this.browserPathLabel);
            this.pocketLadioTabPage.Controls.Add(this.mediaPlayerPathLabel);
            this.pocketLadioTabPage.Controls.Add(this.headlineTimerSecondNumericUpDown);
            this.pocketLadioTabPage.Controls.Add(this.headlineTimerSecondLabel);
            this.pocketLadioTabPage.Location = new System.Drawing.Point(0, 0);
            this.pocketLadioTabPage.Size = new System.Drawing.Size(240, 245);
            this.pocketLadioTabPage.Text = "PocketLadio設定";
            // 
            // browserPathTextBox
            // 
            this.browserPathTextBox.ContextMenu = this.browserPathContextMenu;
            this.browserPathTextBox.Location = new System.Drawing.Point(3, 114);
            this.browserPathTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // mediaPlayerPathTextBox
            // 
            this.mediaPlayerPathTextBox.ContextMenu = this.mediaPlayeraPathContextMenu;
            this.mediaPlayerPathTextBox.Location = new System.Drawing.Point(3, 71);
            this.mediaPlayerPathTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // browserPathLabel
            // 
            this.browserPathLabel.Location = new System.Drawing.Point(3, 95);
            this.browserPathLabel.Size = new System.Drawing.Size(79, 16);
            this.browserPathLabel.Text = "ブラウザのパス";
            // 
            // mediaPlayerPathLabel
            // 
            this.mediaPlayerPathLabel.Location = new System.Drawing.Point(3, 52);
            this.mediaPlayerPathLabel.Size = new System.Drawing.Size(132, 16);
            this.mediaPlayerPathLabel.Text = "メディアプレーヤーのパス";
            // 
            // headlineTimerSecondNumericUpDown
            // 
            this.headlineTimerSecondNumericUpDown.Location = new System.Drawing.Point(182, 27);
            this.headlineTimerSecondNumericUpDown.ReadOnly = true;
            this.headlineTimerSecondNumericUpDown.Size = new System.Drawing.Size(55, 22);
            this.headlineTimerSecondNumericUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // headlineTimerSecondLabel
            // 
            this.headlineTimerSecondLabel.Location = new System.Drawing.Point(3, 4);
            this.headlineTimerSecondLabel.Size = new System.Drawing.Size(188, 20);
            this.headlineTimerSecondLabel.Text = "ヘッドラインの自動チェック間隔(秒)";
            // 
            // settingTabControl
            // 
            this.settingTabControl.Controls.Add(this.pocketLadioTabPage);
            this.settingTabControl.Controls.Add(this.networkTabPage);
            this.settingTabControl.Location = new System.Drawing.Point(0, 0);
            this.settingTabControl.SelectedIndex = 0;
            this.settingTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // networkTabPage
            // 
            this.networkTabPage.Controls.Add(this.proxyUseCheckBox);
            this.networkTabPage.Controls.Add(this.proxyPortTextBox);
            this.networkTabPage.Controls.Add(this.proxyServerTextBox);
            this.networkTabPage.Controls.Add(this.proxyPortLabel);
            this.networkTabPage.Controls.Add(this.proxyServerLabel);
            this.networkTabPage.Location = new System.Drawing.Point(0, 0);
            this.networkTabPage.Size = new System.Drawing.Size(232, 242);
            this.networkTabPage.Text = "ネットワーク設定";
            // 
            // proxyUseCheckBox
            // 
            this.proxyUseCheckBox.Location = new System.Drawing.Point(3, 3);
            this.proxyUseCheckBox.Size = new System.Drawing.Size(135, 20);
            this.proxyUseCheckBox.Text = "プロキシを使用する";
            // 
            // proxyPortTextBox
            // 
            this.proxyPortTextBox.ContextMenu = this.proxyPortContextMenu;
            this.proxyPortTextBox.Location = new System.Drawing.Point(3, 88);
            this.proxyPortTextBox.Size = new System.Drawing.Size(74, 21);
            // 
            // proxyPortContextMenu
            // 
            this.proxyPortContextMenu.MenuItems.Add(this.cutProxyPortMenuItem);
            this.proxyPortContextMenu.MenuItems.Add(this.copyProxyPortMenuItem);
            this.proxyPortContextMenu.MenuItems.Add(this.pasteProxyPortMenuItem);
            // 
            // cutProxyPortMenuItem
            // 
            this.cutProxyPortMenuItem.Text = "切り取り(&T)";
            this.cutProxyPortMenuItem.Click += new System.EventHandler(this.CutProxyPortMenuItem_Click);
            // 
            // copyProxyPortMenuItem
            // 
            this.copyProxyPortMenuItem.Text = "コピー(&C)";
            this.copyProxyPortMenuItem.Click += new System.EventHandler(this.CopyProxyPortMenuItem_Click);
            // 
            // pasteProxyPortMenuItem
            // 
            this.pasteProxyPortMenuItem.Text = "貼り付け(&P)";
            this.pasteProxyPortMenuItem.Click += new System.EventHandler(this.PasteProxyPortMenuItem_Click);
            // 
            // proxyServerTextBox
            // 
            this.proxyServerTextBox.ContextMenu = this.proxyServerContextMenu;
            this.proxyServerTextBox.Location = new System.Drawing.Point(3, 45);
            this.proxyServerTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // proxyServerContextMenu
            // 
            this.proxyServerContextMenu.MenuItems.Add(this.cutProxyServerMenuItem);
            this.proxyServerContextMenu.MenuItems.Add(this.copyProxyServerMenuItem);
            this.proxyServerContextMenu.MenuItems.Add(this.pasteProxyServerMenuItem);
            // 
            // cutProxyServerMenuItem
            // 
            this.cutProxyServerMenuItem.Text = "切り取り(&T)";
            this.cutProxyServerMenuItem.Click += new System.EventHandler(this.CutProxyServerMenuItem_Click);
            // 
            // copyProxyServerMenuItem
            // 
            this.copyProxyServerMenuItem.Text = "コピー(&C)";
            this.copyProxyServerMenuItem.Click += new System.EventHandler(this.CopyProxyServerMenuItem_Click);
            // 
            // pasteProxyServerMenuItem
            // 
            this.pasteProxyServerMenuItem.Text = "貼り付け(&P)";
            this.pasteProxyServerMenuItem.Click += new System.EventHandler(this.PasteProxyServerMenuItem_Click);
            // 
            // proxyPortLabel
            // 
            this.proxyPortLabel.Location = new System.Drawing.Point(3, 69);
            this.proxyPortLabel.Size = new System.Drawing.Size(192, 16);
            this.proxyPortLabel.Text = "プロキシのポート番号 （例： 8080）";
            // 
            // proxyServerLabel
            // 
            this.proxyServerLabel.Location = new System.Drawing.Point(3, 26);
            this.proxyServerLabel.Size = new System.Drawing.Size(230, 16);
            this.proxyServerLabel.Text = "プロキシサーバ （例：proxy.example.com）";
            // 
            // settingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.settingTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // ヘッドラインチェックタイマーの上限と下限
            headlineTimerSecondNumericUpDown.Minimum = PocketLadioInfo.HeadlineCheckTimerMinimumMillSec / 1000;
            headlineTimerSecondNumericUpDown.Maximum = PocketLadioInfo.HeadlineCheckTimerMaximumMillSec / 1000;

            // 設定の読み込み
            mediaPlayerPathTextBox.Text = UserSetting.MediaPlayerPath;
            browserPathTextBox.Text = UserSetting.BrowserPath;
            proxyUseCheckBox.Checked = UserSetting.ProxyUse;
            proxyServerTextBox.Text = UserSetting.ProxyServer;
            proxyPortTextBox.Text = UserSetting.ProxyPort.ToString();
            headlineTimerSecondNumericUpDown.Text = (UserSetting.HeadlineTimerMillSecond / 1000).ToString();
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
            UserSetting.MediaPlayerPath = mediaPlayerPathTextBox.Text.Trim();
            UserSetting.BrowserPath = browserPathTextBox.Text.Trim();
            UserSetting.ProxyUse = proxyUseCheckBox.Checked;
            UserSetting.ProxyServer = proxyServerTextBox.Text.Trim();
            UserSetting.ProxyPort = int.Parse(proxyPortTextBox.Text.Trim());
            
            try
            {
                UserSetting.HeadlineTimerMillSecond = Convert.ToInt32(headlineTimerSecondNumericUpDown.Text) * 1000;
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

            try
            {
                UserSetting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが書き込めませんでした", "設定ファイル書き込みエラー");
            }
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                // プロキシサーバ設定・プロキシポート設定のどちらかに何かが入力されている場合かつ、プロキシポートの設定が不正な場合
                if ((proxyServerTextBox.Text.Trim().Length != 0 || proxyPortTextBox.Text.Trim().Length != 0)
                    && (int.Parse(proxyPortTextBox.Text) < 0x00 || int.Parse(proxyPortTextBox.Text) > 0xFFFF))
                {
                    MessageBox.Show("プロキシのポート番号は0～65535で設定してください");
                }
                else
                {
                    this.Close();
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("プロキシのポート番号は0～65535で設定してください");
            }
            catch (FormatException)
            {
                MessageBox.Show("プロキシのポート番号は0～65535で設定してください");
            }
            catch (OverflowException)
            {
                MessageBox.Show("プロキシのポート番号は0～65535で設定してください");
            }
        }

        private void CutMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(mediaPlayerPathTextBox);
        }

        private void CopyMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(mediaPlayerPathTextBox);
        }

        private void PasteMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(mediaPlayerPathTextBox);
        }

        private void CutBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(browserPathTextBox);
        }

        private void CopyBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(browserPathTextBox);
        }

        private void PasteBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(browserPathTextBox);
        }

        private void CutProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(proxyServerTextBox);
        }

        private void CopyProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(proxyServerTextBox);
        }

        private void PasteProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(proxyServerTextBox);
        }

        private void CutProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(proxyPortTextBox);
        }

        private void CopyProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(proxyPortTextBox);
        }

        private void PasteProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(proxyPortTextBox);
        }
    }
}
