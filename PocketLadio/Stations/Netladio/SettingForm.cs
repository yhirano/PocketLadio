#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// ねとらじの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private TabControl netladioTabControl;
        private TabPage netladioTabPage;
        private Label headlineCvsUrlLabel;
        private Label headlineXmlUrlLabel;
        private TextBox headlineCsvUrlTextBox;
        private TextBox headlineXmlUrlTextBox;
        private Panel headlineGetWayPanel;
        private RadioButton headlineGetWayCvsRadioButton;
        private RadioButton headlineGetWayXmlRadioButton;
        private Label headlineViewTypeLabel;
        private MenuItem okMenuItem;
        private MainMenu mainMenu;
        private Label headlineGetWayLabel;
        private TextBox headlineViewTypeTextBox;
        private ContextMenu headlineViewTypeContextMenu;
        private MenuItem cutHeadlineViewTypeMenuItem;
        private MenuItem copyHeadlineViewTypeMenuItem;
        private MenuItem pasteHeadlineViewTypeMenuItem;
        private ContextMenu headlineCvsUrlContextMenu;
        private MenuItem cutHeadlineCvsUrlMenuItem;
        private MenuItem copyHeadlineCvsUrlMenuItem;
        private MenuItem pasteHeadlineCvsUrlMenuItem;
        private TabPage filterTabPage;
        private Label filterListLabel;
        private Label addFilterLabel;
        private Button deleteButton;
        private ListBox filterListBox;
        private Button addWordButton;
        private TextBox addWordTextBox;
        private ContextMenu filterListBoxContextMenu;
        private MenuItem deleteFilterListMenuItem;
        private ContextMenu addWordContextMenu;
        private MenuItem cutAddWordMenuItem;
        private MenuItem copyAddWordMenuItem;
        private MenuItem pasteAddWordMenuItem;

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
            this.netladioTabControl = new System.Windows.Forms.TabControl();
            this.netladioTabPage = new System.Windows.Forms.TabPage();
            this.headlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.headlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.copyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineViewTypeLabel = new System.Windows.Forms.Label();
            this.headlineGetWayPanel = new System.Windows.Forms.Panel();
            this.headlineGetWayLabel = new System.Windows.Forms.Label();
            this.headlineGetWayXmlRadioButton = new System.Windows.Forms.RadioButton();
            this.headlineGetWayCvsRadioButton = new System.Windows.Forms.RadioButton();
            this.headlineXmlUrlTextBox = new System.Windows.Forms.TextBox();
            this.headlineCsvUrlTextBox = new System.Windows.Forms.TextBox();
            this.headlineCvsUrlContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.copyHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteHeadlineCvsUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineXmlUrlLabel = new System.Windows.Forms.Label();
            this.headlineCvsUrlLabel = new System.Windows.Forms.Label();
            this.filterTabPage = new System.Windows.Forms.TabPage();
            this.filterListLabel = new System.Windows.Forms.Label();
            this.addFilterLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.filterListBox = new System.Windows.Forms.ListBox();
            this.addWordButton = new System.Windows.Forms.Button();
            this.addWordTextBox = new System.Windows.Forms.TextBox();
            this.addWordContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.copyAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.filterListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.deleteFilterListMenuItem = new System.Windows.Forms.MenuItem();
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
            // netladioTabControl
            // 
            this.netladioTabControl.Controls.Add(this.netladioTabPage);
            this.netladioTabControl.Controls.Add(this.filterTabPage);
            this.netladioTabControl.Location = new System.Drawing.Point(0, 0);
            this.netladioTabControl.SelectedIndex = 0;
            this.netladioTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // netladioTabPage
            // 
            this.netladioTabPage.Controls.Add(this.headlineViewTypeTextBox);
            this.netladioTabPage.Controls.Add(this.headlineViewTypeLabel);
            this.netladioTabPage.Controls.Add(this.headlineGetWayPanel);
            this.netladioTabPage.Controls.Add(this.headlineXmlUrlTextBox);
            this.netladioTabPage.Controls.Add(this.headlineCsvUrlTextBox);
            this.netladioTabPage.Controls.Add(this.headlineXmlUrlLabel);
            this.netladioTabPage.Controls.Add(this.headlineCvsUrlLabel);
            this.netladioTabPage.Location = new System.Drawing.Point(0, 0);
            this.netladioTabPage.Size = new System.Drawing.Size(240, 245);
            this.netladioTabPage.Text = "ねとらじ設定";
            // 
            // headlineViewTypeTextBox
            // 
            this.headlineViewTypeTextBox.ContextMenu = this.headlineViewTypeContextMenu;
            this.headlineViewTypeTextBox.Location = new System.Drawing.Point(3, 162);
            this.headlineViewTypeTextBox.Size = new System.Drawing.Size(234, 21);
            this.headlineViewTypeTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HeadlineViewTypeTextBox_KeyUp);
            this.headlineViewTypeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineViewTypeTextBox_KeyDown);
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
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 139);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(135, 20);
            this.headlineViewTypeLabel.Text = "ヘッドラインの表示方法";
            // 
            // headlineGetWayPanel
            // 
            this.headlineGetWayPanel.Controls.Add(this.headlineGetWayLabel);
            this.headlineGetWayPanel.Controls.Add(this.headlineGetWayXmlRadioButton);
            this.headlineGetWayPanel.Controls.Add(this.headlineGetWayCvsRadioButton);
            this.headlineGetWayPanel.Location = new System.Drawing.Point(0, 91);
            this.headlineGetWayPanel.Size = new System.Drawing.Size(240, 45);
            // 
            // headlineGetWayLabel
            // 
            this.headlineGetWayLabel.Location = new System.Drawing.Point(3, 0);
            this.headlineGetWayLabel.Size = new System.Drawing.Size(135, 20);
            this.headlineGetWayLabel.Text = "ヘッドラインの取得方法";
            // 
            // headlineGetWayXmlRadioButton
            // 
            this.headlineGetWayXmlRadioButton.Enabled = false;
            this.headlineGetWayXmlRadioButton.Location = new System.Drawing.Point(57, 23);
            this.headlineGetWayXmlRadioButton.Size = new System.Drawing.Size(48, 20);
            this.headlineGetWayXmlRadioButton.Text = "XML";
            // 
            // headlineGetWayCvsRadioButton
            // 
            this.headlineGetWayCvsRadioButton.Checked = true;
            this.headlineGetWayCvsRadioButton.Location = new System.Drawing.Point(3, 22);
            this.headlineGetWayCvsRadioButton.Size = new System.Drawing.Size(48, 20);
            this.headlineGetWayCvsRadioButton.Text = "CVS";
            // 
            // headlineXmlUrlTextBox
            // 
            this.headlineXmlUrlTextBox.Enabled = false;
            this.headlineXmlUrlTextBox.Location = new System.Drawing.Point(3, 64);
            this.headlineXmlUrlTextBox.Size = new System.Drawing.Size(234, 21);
            this.headlineXmlUrlTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HeadlineXmlUrlTextBox_KeyUp);
            this.headlineXmlUrlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineXmlUrlTextBox_KeyDown);
            // 
            // headlineCsvUrlTextBox
            // 
            this.headlineCsvUrlTextBox.ContextMenu = this.headlineCvsUrlContextMenu;
            this.headlineCsvUrlTextBox.Location = new System.Drawing.Point(3, 23);
            this.headlineCsvUrlTextBox.Size = new System.Drawing.Size(234, 21);
            this.headlineCsvUrlTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HeadlineCsvUrlTextBox_KeyUp);
            this.headlineCsvUrlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineCsvUrlTextBox_KeyDown);
            // 
            // headlineCvsUrlContextMenu
            // 
            this.headlineCvsUrlContextMenu.MenuItems.Add(this.cutHeadlineCvsUrlMenuItem);
            this.headlineCvsUrlContextMenu.MenuItems.Add(this.copyHeadlineCvsUrlMenuItem);
            this.headlineCvsUrlContextMenu.MenuItems.Add(this.pasteHeadlineCvsUrlMenuItem);
            // 
            // cutHeadlineCvsUrlMenuItem
            // 
            this.cutHeadlineCvsUrlMenuItem.Text = "切り取り(&T)";
            this.cutHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.CutHeadlineCvsUrlMenuItem_Click);
            // 
            // copyHeadlineCvsUrlMenuItem
            // 
            this.copyHeadlineCvsUrlMenuItem.Text = "コピー(&C)";
            this.copyHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.CopyHeadlineCvsUrlMenuItem_Click);
            // 
            // pasteHeadlineCvsUrlMenuItem
            // 
            this.pasteHeadlineCvsUrlMenuItem.Text = "貼り付け(&P)";
            this.pasteHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.PasteHeadlineCvsUrlMenuItem_Click);
            // 
            // headlineXmlUrlLabel
            // 
            this.headlineXmlUrlLabel.Location = new System.Drawing.Point(3, 47);
            this.headlineXmlUrlLabel.Size = new System.Drawing.Size(124, 16);
            this.headlineXmlUrlLabel.Text = "ヘッドラインのURL XML";
            // 
            // headlineCvsUrlLabel
            // 
            this.headlineCvsUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.headlineCvsUrlLabel.Size = new System.Drawing.Size(124, 16);
            this.headlineCvsUrlLabel.Text = "ヘッドラインのURL CSV";
            // 
            // filterTabPage
            // 
            this.filterTabPage.Controls.Add(this.filterListLabel);
            this.filterTabPage.Controls.Add(this.addFilterLabel);
            this.filterTabPage.Controls.Add(this.deleteButton);
            this.filterTabPage.Controls.Add(this.filterListBox);
            this.filterTabPage.Controls.Add(this.addWordButton);
            this.filterTabPage.Controls.Add(this.addWordTextBox);
            this.filterTabPage.Location = new System.Drawing.Point(0, 0);
            this.filterTabPage.Size = new System.Drawing.Size(240, 245);
            this.filterTabPage.Text = "フィルター設定";
            // 
            // filterListLabel
            // 
            this.filterListLabel.Location = new System.Drawing.Point(3, 51);
            this.filterListLabel.Size = new System.Drawing.Size(100, 20);
            this.filterListLabel.Text = "フィルター一覧";
            // 
            // addFilterLabel
            // 
            this.addFilterLabel.Location = new System.Drawing.Point(3, 4);
            this.addFilterLabel.Size = new System.Drawing.Size(100, 20);
            this.addFilterLabel.Text = "フィルターの追加";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(165, 219);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.deleteButton.Text = "削除(&D)";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // filterListBox
            // 
            this.filterListBox.ContextMenu = this.filterListBoxContextMenu;
            this.filterListBox.Location = new System.Drawing.Point(3, 71);
            this.filterListBox.Size = new System.Drawing.Size(234, 142);
            // 
            // addWordButton
            // 
            this.addWordButton.Location = new System.Drawing.Point(165, 27);
            this.addWordButton.Size = new System.Drawing.Size(72, 20);
            this.addWordButton.Text = "追加(&A)";
            this.addWordButton.Click += new System.EventHandler(this.AddWordButton_Click);
            // 
            // addWordTextBox
            // 
            this.addWordTextBox.ContextMenu = this.addWordContextMenu;
            this.addWordTextBox.Location = new System.Drawing.Point(3, 27);
            this.addWordTextBox.Size = new System.Drawing.Size(156, 21);
            this.addWordTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AddWordTextBox_KeyUp);
            this.addWordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddWordTextBox_KeyPress);
            this.addWordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddWordTextBox_KeyDown);
            // 
            // addWordContextMenu
            // 
            this.addWordContextMenu.MenuItems.Add(this.cutAddWordMenuItem);
            this.addWordContextMenu.MenuItems.Add(this.copyAddWordMenuItem);
            this.addWordContextMenu.MenuItems.Add(this.pasteAddWordMenuItem);
            // 
            // cutAddWordMenuItem
            // 
            this.cutAddWordMenuItem.Text = "切り取り(&T)";
            this.cutAddWordMenuItem.Click += new System.EventHandler(this.CutAddWordMenuItem_Click);
            // 
            // copyAddWordMenuItem
            // 
            this.copyAddWordMenuItem.Text = "コピー(&C)";
            this.copyAddWordMenuItem.Click += new System.EventHandler(this.CopyAddWordMenuItem_Click);
            // 
            // pasteAddWordMenuItem
            // 
            this.pasteAddWordMenuItem.Text = "貼り付け(&P)";
            this.pasteAddWordMenuItem.Click += new System.EventHandler(this.PasteAddWordMenuItem_Click);
            // 
            // filterListBoxContextMenu
            // 
            this.filterListBoxContextMenu.MenuItems.Add(this.deleteFilterListMenuItem);
            this.filterListBoxContextMenu.Popup += new System.EventHandler(this.FilterListBoxContextMenu_Popup);
            // 
            // deleteFilterListMenuItem
            // 
            this.deleteFilterListMenuItem.Text = "削除(&D)";
            this.deleteFilterListMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.netladioTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "ねとらじ設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // 設定の読み込み
            headlineCsvUrlTextBox.Text = ((setting.HeadlineCsvUrl != null) ? setting.HeadlineCsvUrl.ToString() : "");
            headlineXmlUrlTextBox.Text = ((setting.HeadlineXmlUrl != null) ? setting.HeadlineXmlUrl.ToString() : "");
            if (setting.HeadlineGetWay == UserSetting.HeadlineGetType.Cvs)
            {
                headlineGetWayCvsRadioButton.Checked = true;
                headlineGetWayXmlRadioButton.Checked = false;
            }
            else if (setting.HeadlineGetWay == UserSetting.HeadlineGetType.Xml)
            {
                headlineGetWayCvsRadioButton.Checked = false;
                headlineGetWayXmlRadioButton.Checked = true;
            }
            headlineViewTypeTextBox.Text = setting.HeadlineViewType;

            // フィルターリストにフィルタの内容を追加する
            foreach (string word in setting.GetFilterWords())
            {
                filterListBox.Items.Add(word);
            }
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // フィルターを追加し忘れていると思われる場合
            if (addWordTextBox.Text.Trim().Length != 0)
            {
                // 追加するかを聞く
                DialogResult result = MessageBox.Show(
                    addWordTextBox.Text.Trim() + "を追加しますか？\n（" + addWordTextBox.Text.Trim() + "はまだ追加されていません）"
                    , "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    filterListBox.Items.Add(addWordTextBox.Text.Trim());
                    addWordTextBox.Text = "";
                }
            }

            // 設定の書き込み
            try
            {
                setting.HeadlineCsvUrl = new Uri(headlineCsvUrlTextBox.Text.Trim());
            }
            catch (UriFormatException)
            {
                ;
            }

            ArrayList alFilterWord = new ArrayList();
            IEnumerator filterEnumerator = filterListBox.Items.GetEnumerator();
            while (filterEnumerator.MoveNext())
            {
                alFilterWord.Add(((string)filterEnumerator.Current).Trim());
            }
            setting.SetFilterWords((string[])alFilterWord.ToArray(typeof(string)));

            try
            {
                setting.HeadlineXmlUrl = new Uri(headlineXmlUrlTextBox.Text.Trim());
            }
            catch (UriFormatException)
            {
                ;
            }
            if (headlineGetWayCvsRadioButton.Checked)
            {
                setting.HeadlineGetWay = UserSetting.HeadlineGetType.Cvs;
            }
            else if (headlineGetWayXmlRadioButton.Checked)
            {
                setting.HeadlineGetWay = UserSetting.HeadlineGetType.Xml;
            }
            else
            {
                // ここに到達することはあり得ない
                Trace.Assert(false, "想定外の動作のため、終了します");
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

        private void CutHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(headlineCsvUrlTextBox);
        }

        private void CopyHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(headlineCsvUrlTextBox);
        }

        private void PasteHeadlineCvsUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(headlineCsvUrlTextBox);
        }

        private void HeadlineCsvUrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 切り取りショートカット
            if (e.KeyCode == Keys.X && e.Control) {
                ClipboardTextBox.Cut(headlineCsvUrlTextBox);
            }
            // 貼り付けショートカット
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(headlineCsvUrlTextBox);
            }
        }

        private void HeadlineCsvUrlTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // コピーショートカット
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(headlineCsvUrlTextBox);
            }
        }

        private void HeadlineXmlUrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 切り取りショートカット
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(headlineXmlUrlTextBox);
            }
            // 貼り付けショートカット
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(headlineXmlUrlTextBox);
            }
        }

        private void HeadlineXmlUrlTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // コピーショートカット
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(headlineXmlUrlTextBox);
            }
        }

        private void HeadlineViewTypeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 切り取りショートカット
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(headlineViewTypeTextBox);
            }
            // 貼り付けショートカット
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(headlineViewTypeTextBox);
            }
        }

        private void HeadlineViewTypeTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // コピーショートカット
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(headlineViewTypeTextBox);
            }
        }

        private void AddWordButton_Click(object sender, System.EventArgs e)
        {
            if (addWordTextBox.Text.Trim().Length != 0)
            {
                filterListBox.Items.Add(addWordTextBox.Text.Trim());
                addWordTextBox.Text = "";
            }
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            if (filterListBox.SelectedIndex != -1)
            {
                filterListBox.Items.RemoveAt(filterListBox.SelectedIndex);
            }
        }

        private void DeleteMenuItem_Click(object sender, System.EventArgs e)
        {
            if (filterListBox.SelectedIndex != -1)
            {
                filterListBox.Items.RemoveAt(filterListBox.SelectedIndex);
            }
        }

        private void FilterListBoxContextMenu_Popup(object sender, System.EventArgs e)
        {
            if (filterListBox.SelectedIndex == -1)
            {
                deleteFilterListMenuItem.Enabled = false;
            }
            else
            {
                deleteFilterListMenuItem.Enabled = true;
            }
        }

        private void CutAddWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(addWordTextBox);
        }

        private void CopyAddWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(addWordTextBox);
        }

        private void PasteAddWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(addWordTextBox);
        }

        private void AddWordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力ボタンを押したとき
            if (e.KeyCode == Keys.Enter)
            {
                AddWordButton_Click(sender, e);
            }
            // 切り取りショートカット
            else if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(addWordTextBox);
            }
            // 貼り付けショートカット
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(addWordTextBox);
            }
        }

        private void AddWordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 入力ボタンを押したときの音を消すため
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void AddWordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // コピーショートカット
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(addWordTextBox);
            }
        }
    }
}
