using System;
using System.Drawing;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using PocketLadio.Util;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// ねとらじの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private TabControl tabControl1;
        private TabPage NetladioTabPage;
        private Label HeadlineCvsUrlLabel;
        private Label HeadlineXmlUrlLabel;
        private TextBox HeadlineCsvUrlTextBox;
        private TextBox HeadlineXmlUrlTextBox;
        private Panel panel1;
        private RadioButton HeadlineGetTypeCvsRadioButton;
        private RadioButton HeadlineGetTypeXmlRadioButton;
        private Label HeadlineViewTypeLabel;
        private MenuItem OkMenuItem;
        private MainMenu MainMenu;
        private Label HeadlineGetTypeLabel;
        private TextBox HeadlineViewTypeTextBox;
        private ContextMenu HeadlineViewTypeContextMenu;
        private MenuItem CutHeadlineViewTypeMenuItem;
        private MenuItem CopyHeadlineViewTypeMenuItem;
        private MenuItem PasteHeadlineViewTypeMenuItem;
        private ContextMenu HeadlineCvsUrlContextMenu;
        private MenuItem CutHeadlineCvsUrlMenuItem;
        private MenuItem CopyHeadlineCvsUrlMenuItem;
        private MenuItem PasteHeadlineCvsUrlMenuItem;

        /// <summary>
        /// 設定
        /// </summary>
        private UserSetting Setting;


        public SettingForm(UserSetting Setting)
        {
            this.Setting = Setting;

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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NetladioTabPage = new System.Windows.Forms.TabPage();
            this.HeadlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.HeadlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.HeadlineViewTypeLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HeadlineGetTypeLabel = new System.Windows.Forms.Label();
            this.HeadlineGetTypeXmlRadioButton = new System.Windows.Forms.RadioButton();
            this.HeadlineGetTypeCvsRadioButton = new System.Windows.Forms.RadioButton();
            this.HeadlineXmlUrlTextBox = new System.Windows.Forms.TextBox();
            this.HeadlineCsvUrlTextBox = new System.Windows.Forms.TextBox();
            this.HeadlineCvsUrlContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.HeadlineXmlUrlLabel = new System.Windows.Forms.Label();
            this.HeadlineCvsUrlLabel = new System.Windows.Forms.Label();
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NetladioTabPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 268);
            // 
            // NetladioTabPage
            // 
            this.NetladioTabPage.Controls.Add(this.HeadlineViewTypeTextBox);
            this.NetladioTabPage.Controls.Add(this.HeadlineViewTypeLabel);
            this.NetladioTabPage.Controls.Add(this.panel1);
            this.NetladioTabPage.Controls.Add(this.HeadlineXmlUrlTextBox);
            this.NetladioTabPage.Controls.Add(this.HeadlineCsvUrlTextBox);
            this.NetladioTabPage.Controls.Add(this.HeadlineXmlUrlLabel);
            this.NetladioTabPage.Controls.Add(this.HeadlineCvsUrlLabel);
            this.NetladioTabPage.Location = new System.Drawing.Point(0, 0);
            this.NetladioTabPage.Size = new System.Drawing.Size(240, 245);
            this.NetladioTabPage.Text = "ねとらじ設定";
            // 
            // HeadlineViewTypeTextBox
            // 
            this.HeadlineViewTypeTextBox.ContextMenu = this.HeadlineViewTypeContextMenu;
            this.HeadlineViewTypeTextBox.Location = new System.Drawing.Point(3, 162);
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
            this.HeadlineViewTypeLabel.Location = new System.Drawing.Point(3, 139);
            this.HeadlineViewTypeLabel.Size = new System.Drawing.Size(135, 20);
            this.HeadlineViewTypeLabel.Text = "ヘッドラインの表示方法";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.HeadlineGetTypeLabel);
            this.panel1.Controls.Add(this.HeadlineGetTypeXmlRadioButton);
            this.panel1.Controls.Add(this.HeadlineGetTypeCvsRadioButton);
            this.panel1.Location = new System.Drawing.Point(0, 91);
            this.panel1.Size = new System.Drawing.Size(240, 45);
            // 
            // HeadlineGetTypeLabel
            // 
            this.HeadlineGetTypeLabel.Location = new System.Drawing.Point(3, 0);
            this.HeadlineGetTypeLabel.Size = new System.Drawing.Size(135, 20);
            this.HeadlineGetTypeLabel.Text = "ヘッドラインの取得方法";
            // 
            // HeadlineGetTypeXmlRadioButton
            // 
            this.HeadlineGetTypeXmlRadioButton.Enabled = false;
            this.HeadlineGetTypeXmlRadioButton.Location = new System.Drawing.Point(57, 23);
            this.HeadlineGetTypeXmlRadioButton.Size = new System.Drawing.Size(48, 20);
            this.HeadlineGetTypeXmlRadioButton.Text = "XML";
            // 
            // HeadlineGetTypeCvsRadioButton
            // 
            this.HeadlineGetTypeCvsRadioButton.Checked = true;
            this.HeadlineGetTypeCvsRadioButton.Location = new System.Drawing.Point(3, 22);
            this.HeadlineGetTypeCvsRadioButton.Size = new System.Drawing.Size(48, 20);
            this.HeadlineGetTypeCvsRadioButton.Text = "CVS";
            // 
            // HeadlineXmlUrlTextBox
            // 
            this.HeadlineXmlUrlTextBox.Enabled = false;
            this.HeadlineXmlUrlTextBox.Location = new System.Drawing.Point(3, 64);
            this.HeadlineXmlUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // HeadlineCsvUrlTextBox
            // 
            this.HeadlineCsvUrlTextBox.ContextMenu = this.HeadlineCvsUrlContextMenu;
            this.HeadlineCsvUrlTextBox.Location = new System.Drawing.Point(3, 23);
            this.HeadlineCsvUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // HeadlineCvsUrlContextMenu
            // 
            this.HeadlineCvsUrlContextMenu.MenuItems.Add(this.CutHeadlineCvsUrlMenuItem);
            this.HeadlineCvsUrlContextMenu.MenuItems.Add(this.CopyHeadlineCvsUrlMenuItem);
            this.HeadlineCvsUrlContextMenu.MenuItems.Add(this.PasteHeadlineCvsUrlMenuItem);
            // 
            // CutHeadlineCvsUrlMenuItem
            // 
            this.CutHeadlineCvsUrlMenuItem.Text = "切り取り(&T)";
            this.CutHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.CutHeadlineCvsUrlMenuItem_Click);
            // 
            // CopyHeadlineCvsUrlMenuItem
            // 
            this.CopyHeadlineCvsUrlMenuItem.Text = "コピー(&C)";
            this.CopyHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.CopyHeadlineCvsUrlMenuItem_Click);
            // 
            // PasteHeadlineCvsUrlMenuItem
            // 
            this.PasteHeadlineCvsUrlMenuItem.Text = "貼り付け(&P)";
            this.PasteHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.PasteHeadlineCvsUrlMenuItem_Click);
            // 
            // HeadlineXmlUrlLabel
            // 
            this.HeadlineXmlUrlLabel.Location = new System.Drawing.Point(3, 47);
            this.HeadlineXmlUrlLabel.Size = new System.Drawing.Size(124, 16);
            this.HeadlineXmlUrlLabel.Text = "ヘッドラインのURL XML";
            // 
            // HeadlineCvsUrlLabel
            // 
            this.HeadlineCvsUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.HeadlineCvsUrlLabel.Size = new System.Drawing.Size(124, 16);
            this.HeadlineCvsUrlLabel.Text = "ヘッドラインのURL CSV";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "ねとらじ設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // 設定の読み込み
            HeadlineCsvUrlTextBox.Text = Setting.HeadlineCsvUrl;
            HeadlineXmlUrlTextBox.Text = Setting.HeadlineXmlUrl;
            if (Setting.HeadlineGetType == UserSetting.HeadlineGetTypeEnum.Cvs)
            {
                HeadlineGetTypeCvsRadioButton.Checked = true;
                HeadlineGetTypeXmlRadioButton.Checked = false;
            }
            else if (Setting.HeadlineGetType == UserSetting.HeadlineGetTypeEnum.Xml)
            {
                HeadlineGetTypeCvsRadioButton.Checked = false;
                HeadlineGetTypeXmlRadioButton.Checked = true;
            }
            HeadlineViewTypeTextBox.Text = Setting.HeadlineViewType;
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
            Setting.HeadlineCsvUrl = HeadlineCsvUrlTextBox.Text.Trim();
            Setting.HeadlineXmlUrl = HeadlineXmlUrlTextBox.Text.Trim();
            if (HeadlineGetTypeCvsRadioButton.Checked)
            {
                Setting.HeadlineGetType = UserSetting.HeadlineGetTypeEnum.Cvs;
            }
            else if (HeadlineGetTypeXmlRadioButton.Checked)
            {
                Setting.HeadlineGetType = UserSetting.HeadlineGetTypeEnum.Xml;
            }
            Setting.HeadlineViewType = HeadlineViewTypeTextBox.Text.Trim();

            try
            {
                Setting.SaveSetting();
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

        private void CutHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(HeadlineCsvUrlTextBox);
        }

        private void CopyHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(HeadlineCsvUrlTextBox);
        }

        private void PasteHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(HeadlineCsvUrlTextBox);
        }
    }
}
