using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using PocketLadio.Utility;

namespace PocketLadio.Stations.RssPodcast
{
    /// <summary>
    /// Podcastの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private TabControl podcastTabControl;
        private TabPage NetladioTabPage;
        private Label RssUrlLabel;
        private TextBox RssUrlTextBox;
        private MenuItem OkMenuItem;
        private MainMenu MainMenu;
        private TextBox HeadlineViewTypeTextBox;
        private Label HeadlineViewTypeLabel;
        private ContextMenu RssUrlContextMenu;
        private MenuItem CutRssUrlMenuItem;
        private MenuItem CopyRssUrlMenuItem;
        private MenuItem PasteRssUrlMenuItem;
        private ContextMenu HeadlineViewTypeContextMenu;
        private MenuItem CutHeadlineViewTypeMenuItem;
        private MenuItem CopyHeadlineViewTypeMenuItem;
        private MenuItem PasteHeadlineViewTypeMenuItem;

        /// <summary>
        /// 設定
        /// </summary>
        private UserSetting setting;


        public SettingForm(UserSetting setting)
        {
            this.setting = setting;

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
            this.podcastTabControl = new System.Windows.Forms.TabControl();
            this.NetladioTabPage = new System.Windows.Forms.TabPage();
            this.HeadlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.HeadlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.HeadlineViewTypeLabel = new System.Windows.Forms.Label();
            this.RssUrlTextBox = new System.Windows.Forms.TextBox();
            this.RssUrlContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.RssUrlLabel = new System.Windows.Forms.Label();
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
            // podcastTabControl
            // 
            this.podcastTabControl.Controls.Add(this.NetladioTabPage);
            this.podcastTabControl.Location = new System.Drawing.Point(0, 0);
            this.podcastTabControl.SelectedIndex = 0;
            this.podcastTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // NetladioTabPage
            // 
            this.NetladioTabPage.Controls.Add(this.HeadlineViewTypeTextBox);
            this.NetladioTabPage.Controls.Add(this.HeadlineViewTypeLabel);
            this.NetladioTabPage.Controls.Add(this.RssUrlTextBox);
            this.NetladioTabPage.Controls.Add(this.RssUrlLabel);
            this.NetladioTabPage.Location = new System.Drawing.Point(0, 0);
            this.NetladioTabPage.Size = new System.Drawing.Size(240, 245);
            this.NetladioTabPage.Text = "Podcast設定";
            // 
            // HeadlineViewTypeTextBox
            // 
            this.HeadlineViewTypeTextBox.ContextMenu = this.HeadlineViewTypeContextMenu;
            this.HeadlineViewTypeTextBox.Location = new System.Drawing.Point(3, 70);
            this.HeadlineViewTypeTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // HeadlineViewTypeContextMenu
            // 
            this.HeadlineViewTypeContextMenu.MenuItems.Add(this.CutHeadlineViewTypeMenuItem);
            this.HeadlineViewTypeContextMenu.MenuItems.Add(this.CopyHeadlineViewTypeMenuItem);
            this.HeadlineViewTypeContextMenu.MenuItems.Add(this.PasteHeadlineViewTypeMenuItem);
            // 
            // CutHeadlineViewTypeMenuItem
            // 
            this.CutHeadlineViewTypeMenuItem.Text = "切り取り(&T)";
            this.CutHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CutHeadlineViewTypeMenuItem_Click);
            // 
            // CopyHeadlineViewTypeMenuItem
            // 
            this.CopyHeadlineViewTypeMenuItem.Text = "コピー(&C)";
            this.CopyHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CopyHeadlineViewTypeMenuItem_Click);
            // 
            // PasteHeadlineViewTypeMenuItem
            // 
            this.PasteHeadlineViewTypeMenuItem.Text = "貼り付け(&P)";
            this.PasteHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.PasteHeadlineViewTypeMenuItem_Click);
            // 
            // HeadlineViewTypeLabel
            // 
            this.HeadlineViewTypeLabel.Location = new System.Drawing.Point(3, 47);
            this.HeadlineViewTypeLabel.Size = new System.Drawing.Size(130, 20);
            this.HeadlineViewTypeLabel.Text = "ヘッドラインの表示方法";
            // 
            // RssUrlTextBox
            // 
            this.RssUrlTextBox.ContextMenu = this.RssUrlContextMenu;
            this.RssUrlTextBox.Location = new System.Drawing.Point(3, 23);
            this.RssUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // RssUrlContextMenu
            // 
            this.RssUrlContextMenu.MenuItems.Add(this.CutRssUrlMenuItem);
            this.RssUrlContextMenu.MenuItems.Add(this.CopyRssUrlMenuItem);
            this.RssUrlContextMenu.MenuItems.Add(this.PasteRssUrlMenuItem);
            // 
            // CutRssUrlMenuItem
            // 
            this.CutRssUrlMenuItem.Text = "切り取り(&T)";
            this.CutRssUrlMenuItem.Click += new System.EventHandler(this.CutRssUrlMenuItem_Click);
            // 
            // CopyRssUrlMenuItem
            // 
            this.CopyRssUrlMenuItem.Text = "コピー(&C)";
            this.CopyRssUrlMenuItem.Click += new System.EventHandler(this.CopyRssUrlMenuItem_Click);
            // 
            // PasteRssUrlMenuItem
            // 
            this.PasteRssUrlMenuItem.Text = "貼り付け(&P)";
            this.PasteRssUrlMenuItem.Click += new System.EventHandler(this.PasteRssUrlMenuItem_Click);
            // 
            // RssUrlLabel
            // 
            this.RssUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.RssUrlLabel.Size = new System.Drawing.Size(109, 16);
            this.RssUrlLabel.Text = "PodcastのRSS URL";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.podcastTabControl);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "Podcast設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // 設定の読み込み
            RssUrlTextBox.Text = ((setting.RssUrl != null) ? setting.RssUrl.ToString() : "");
            HeadlineViewTypeTextBox.Text = setting.HeadlineViewType;
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
            try
            {
                setting.RssUrl = new Uri(RssUrlTextBox.Text.Trim());
            }
            catch (UriFormatException)
            {
                ;
            }
            setting.HeadlineViewType = HeadlineViewTypeTextBox.Text.Trim();

            try
            {
                setting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが書き込めませんでした", "設定ファイル書き込みエラー");
            }
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void CutRssUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(RssUrlTextBox);
        }

        private void CopyRssUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(RssUrlTextBox);
        }

        private void PasteRssUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(RssUrlTextBox);
        }

        private void CutHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(HeadlineViewTypeTextBox);
        }

        private void CopyHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(HeadlineViewTypeTextBox);
        }

        private void PasteHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(HeadlineViewTypeTextBox);
        }
    }
}
