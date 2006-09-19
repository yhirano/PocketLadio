#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio.Stations.RssPodcast
{
    /// <summary>
    /// Podcastの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private TabControl podcastTabControl;
        private TabPage podcastTabPage;
        private Label rssUrlLabel;
        private TextBox rssUrlTextBox;
        private MenuItem okMenuItem;
        private MainMenu mainMenu;
        private TextBox headlineViewTypeTextBox;
        private Label headlineViewTypeLabel;
        private ContextMenu rssUrlContextMenu;
        private MenuItem cutRssUrlMenuItem;
        private MenuItem copyRssUrlMenuItem;
        private MenuItem pasteRssUrlMenuItem;
        private ContextMenu headlineViewTypeContextMenu;
        private MenuItem cutHeadlineViewTypeMenuItem;
        private MenuItem copyHeadlineViewTypeMenuItem;
        private MenuItem pasteHeadlineViewTypeMenuItem;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.podcastTabControl = new System.Windows.Forms.TabControl();
            this.podcastTabPage = new System.Windows.Forms.TabPage();
            this.headlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.headlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.copyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineViewTypeLabel = new System.Windows.Forms.Label();
            this.rssUrlTextBox = new System.Windows.Forms.TextBox();
            this.rssUrlContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.copyRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteRssUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.rssUrlLabel = new System.Windows.Forms.Label();
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
            // podcastTabControl
            // 
            this.podcastTabControl.Controls.Add(this.podcastTabPage);
            this.podcastTabControl.Location = new System.Drawing.Point(0, 0);
            this.podcastTabControl.SelectedIndex = 0;
            this.podcastTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // podcastTabPage
            // 
            this.podcastTabPage.Controls.Add(this.headlineViewTypeTextBox);
            this.podcastTabPage.Controls.Add(this.headlineViewTypeLabel);
            this.podcastTabPage.Controls.Add(this.rssUrlTextBox);
            this.podcastTabPage.Controls.Add(this.rssUrlLabel);
            this.podcastTabPage.Location = new System.Drawing.Point(0, 0);
            this.podcastTabPage.Size = new System.Drawing.Size(240, 245);
            this.podcastTabPage.Text = "Podcast設定";
            // 
            // headlineViewTypeTextBox
            // 
            this.headlineViewTypeTextBox.ContextMenu = this.headlineViewTypeContextMenu;
            this.headlineViewTypeTextBox.Location = new System.Drawing.Point(3, 70);
            this.headlineViewTypeTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // headlineViewTypeContextMenu
            // 
            this.headlineViewTypeContextMenu.MenuItems.Add(this.cutHeadlineViewTypeMenuItem);
            this.headlineViewTypeContextMenu.MenuItems.Add(this.copyHeadlineViewTypeMenuItem);
            this.headlineViewTypeContextMenu.MenuItems.Add(this.pasteHeadlineViewTypeMenuItem);
            // 
            // cutHeadlineViewTypeMenuItem
            // 
            this.cutHeadlineViewTypeMenuItem.Text = "切り取り(&T)";
            this.cutHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CutHeadlineViewTypeMenuItem_Click);
            // 
            // copyHeadlineViewTypeMenuItem
            // 
            this.copyHeadlineViewTypeMenuItem.Text = "コピー(&C)";
            this.copyHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CopyHeadlineViewTypeMenuItem_Click);
            // 
            // pasteHeadlineViewTypeMenuItem
            // 
            this.pasteHeadlineViewTypeMenuItem.Text = "貼り付け(&P)";
            this.pasteHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.PasteHeadlineViewTypeMenuItem_Click);
            // 
            // headlineViewTypeLabel
            // 
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 47);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(130, 20);
            this.headlineViewTypeLabel.Text = "ヘッドラインの表示方法";
            // 
            // rssUrlTextBox
            // 
            this.rssUrlTextBox.ContextMenu = this.rssUrlContextMenu;
            this.rssUrlTextBox.Location = new System.Drawing.Point(3, 23);
            this.rssUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // rssUrlContextMenu
            // 
            this.rssUrlContextMenu.MenuItems.Add(this.cutRssUrlMenuItem);
            this.rssUrlContextMenu.MenuItems.Add(this.copyRssUrlMenuItem);
            this.rssUrlContextMenu.MenuItems.Add(this.pasteRssUrlMenuItem);
            // 
            // cutRssUrlMenuItem
            // 
            this.cutRssUrlMenuItem.Text = "切り取り(&T)";
            this.cutRssUrlMenuItem.Click += new System.EventHandler(this.CutRssUrlMenuItem_Click);
            // 
            // copyRssUrlMenuItem
            // 
            this.copyRssUrlMenuItem.Text = "コピー(&C)";
            this.copyRssUrlMenuItem.Click += new System.EventHandler(this.CopyRssUrlMenuItem_Click);
            // 
            // pasteRssUrlMenuItem
            // 
            this.pasteRssUrlMenuItem.Text = "貼り付け(&P)";
            this.pasteRssUrlMenuItem.Click += new System.EventHandler(this.PasteRssUrlMenuItem_Click);
            // 
            // rssUrlLabel
            // 
            this.rssUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.rssUrlLabel.Size = new System.Drawing.Size(109, 16);
            this.rssUrlLabel.Text = "PodcastのRSS URL";
            // 
            // settingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.podcastTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "Podcast設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // 設定の読み込み
            rssUrlTextBox.Text = ((setting.RssUrl != null) ? setting.RssUrl.ToString() : "");
            headlineViewTypeTextBox.Text = setting.HeadlineViewType;
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
            try
            {
                setting.RssUrl = new Uri(rssUrlTextBox.Text.Trim());
            }
            catch (UriFormatException)
            {
                ;
            }
            setting.HeadlineViewType = headlineViewTypeTextBox.Text.Trim();

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
            ClipboardTextBox.Cut(rssUrlTextBox);
        }

        private void CopyRssUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(rssUrlTextBox);
        }

        private void PasteRssUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(rssUrlTextBox);
        }

        private void CutHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(headlineViewTypeTextBox);
        }

        private void CopyHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(headlineViewTypeTextBox);
        }

        private void PasteHeadlineViewTypeMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(headlineViewTypeTextBox);
        }
    }
}
