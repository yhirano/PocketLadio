#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio.Stations.Icecast
{
    /// <summary>
    /// Icecastの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// 設定
        /// </summary>
        private UserSetting setting;

        /// <summary>
        /// 一致単語フィルター
        /// </summary>
        private ArrayList alFilterMatchWords = new ArrayList();

        /// <summary>
        /// 除外単語フィルター
        /// </summary>
        private ArrayList alFilterExclusionWords = new ArrayList();

        /// <summary>
        /// Icecastの番組表取得数のリスト
        /// </summary>
        private static readonly string[] icecastFetchChannelNums = { FETCH_ALL_CHANNEL, "10", "20", "50", "100", "200", "500", "1000" };

        /// <summary>
        /// 全ての番組を取得する
        /// </summary>
        private const string FETCH_ALL_CHANNEL = "All";

        private TabControl icecastTabControl;
        private TabPage icecastTabPage;
        private MenuItem okMenuItem;
        private MainMenu mainMenu;
        private TextBox headlineViewTypeTextBox;
        private Label headlineViewTypeLabel;
        private ContextMenu headlineViewTypeContextMenu;
        private MenuItem cutHeadlineViewTypeMenuItem;
        private MenuItem copyHeadlineViewTypeMenuItem;
        private MenuItem pasteHeadlineViewTypeMenuItem;
        private TabPage filterTabPage;
        private Label filterListLabel;
        private Label addFilterLabel;
        private Button deleteButton;
        private ListBox filterListBox;
        private Button addMatchWordButton;
        private TextBox addWordTextBox;
        private ContextMenu filterListBoxContextMenu;
        private MenuItem deleteFilterListMenuItem;
        private Label fetchChannelLabel;

        private TabPage stationTabPage;
        private TextBox stationNameTextBox;
        private Label stationNameLabel;
        private TabPage filter2TabPage;
        private Label filterBelowBitRateLabel;
        private CheckBox filterBelowBitRateUseCheckBox;
        private TextBox filterBelowBitRateTextBox;
        private Label filterAboveBitRateLabel;
        private CheckBox filterAboveBitRateUseCheckBox;
        private TextBox filterAboveBitRateTextBox;
        private DomainUpDown fetchChannelDomainUpDown;
        private Button addExclusionWordButton;

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
            this.icecastTabControl = new System.Windows.Forms.TabControl();
            this.stationTabPage = new System.Windows.Forms.TabPage();
            this.stationNameTextBox = new System.Windows.Forms.TextBox();
            this.stationNameLabel = new System.Windows.Forms.Label();
            this.icecastTabPage = new System.Windows.Forms.TabPage();
            this.fetchChannelDomainUpDown = new System.Windows.Forms.DomainUpDown();
            this.fetchChannelLabel = new System.Windows.Forms.Label();
            this.headlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.headlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.copyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineViewTypeLabel = new System.Windows.Forms.Label();
            this.filterTabPage = new System.Windows.Forms.TabPage();
            this.addExclusionWordButton = new System.Windows.Forms.Button();
            this.filterListLabel = new System.Windows.Forms.Label();
            this.addFilterLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.filterListBox = new System.Windows.Forms.ListBox();
            this.filterListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.deleteFilterListMenuItem = new System.Windows.Forms.MenuItem();
            this.addMatchWordButton = new System.Windows.Forms.Button();
            this.addWordTextBox = new System.Windows.Forms.TextBox();
            this.filter2TabPage = new System.Windows.Forms.TabPage();
            this.filterBelowBitRateLabel = new System.Windows.Forms.Label();
            this.filterBelowBitRateUseCheckBox = new System.Windows.Forms.CheckBox();
            this.filterBelowBitRateTextBox = new System.Windows.Forms.TextBox();
            this.filterAboveBitRateLabel = new System.Windows.Forms.Label();
            this.filterAboveBitRateUseCheckBox = new System.Windows.Forms.CheckBox();
            this.filterAboveBitRateTextBox = new System.Windows.Forms.TextBox();
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
            // icecastTabControl
            // 
            this.icecastTabControl.Controls.Add(this.stationTabPage);
            this.icecastTabControl.Controls.Add(this.icecastTabPage);
            this.icecastTabControl.Controls.Add(this.filterTabPage);
            this.icecastTabControl.Controls.Add(this.filter2TabPage);
            this.icecastTabControl.Location = new System.Drawing.Point(0, 0);
            this.icecastTabControl.SelectedIndex = 0;
            this.icecastTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // stationTabPage
            // 
            this.stationTabPage.Controls.Add(this.stationNameTextBox);
            this.stationTabPage.Controls.Add(this.stationNameLabel);
            this.stationTabPage.Location = new System.Drawing.Point(0, 0);
            this.stationTabPage.Size = new System.Drawing.Size(240, 245);
            this.stationTabPage.Text = "放送局";
            // 
            // stationNameTextBox
            // 
            this.stationNameTextBox.Location = new System.Drawing.Point(3, 27);
            this.stationNameTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // stationNameLabel
            // 
            this.stationNameLabel.Location = new System.Drawing.Point(3, 4);
            this.stationNameLabel.Size = new System.Drawing.Size(234, 20);
            this.stationNameLabel.Text = "放送局名";
            // 
            // icecastTabPage
            // 
            this.icecastTabPage.Controls.Add(this.fetchChannelDomainUpDown);
            this.icecastTabPage.Controls.Add(this.fetchChannelLabel);
            this.icecastTabPage.Controls.Add(this.headlineViewTypeTextBox);
            this.icecastTabPage.Controls.Add(this.headlineViewTypeLabel);
            this.icecastTabPage.Location = new System.Drawing.Point(0, 0);
            this.icecastTabPage.Size = new System.Drawing.Size(240, 245);
            this.icecastTabPage.Text = "Icecast";
            // 
            // fetchChannelDomainUpDown
            // 
            this.fetchChannelDomainUpDown.Location = new System.Drawing.Point(3, 23);
            this.fetchChannelDomainUpDown.Size = new System.Drawing.Size(100, 22);
            this.fetchChannelDomainUpDown.Text = "domainUpDown1";
            // 
            // fetchChannelLabel
            // 
            this.fetchChannelLabel.Location = new System.Drawing.Point(3, 4);
            this.fetchChannelLabel.Size = new System.Drawing.Size(86, 16);
            this.fetchChannelLabel.Text = "番組取得数";
            // 
            // headlineViewTypeTextBox
            // 
            this.headlineViewTypeTextBox.ContextMenu = this.headlineViewTypeContextMenu;
            this.headlineViewTypeTextBox.Location = new System.Drawing.Point(3, 71);
            this.headlineViewTypeTextBox.Size = new System.Drawing.Size(234, 21);
            this.headlineViewTypeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeadlineViewTypeTextBox_KeyDown);
            this.headlineViewTypeTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HeadlineViewTypeTextBox_KeyUp);
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
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 48);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(234, 20);
            this.headlineViewTypeLabel.Text = "ヘッドラインの表示方法";
            // 
            // filterTabPage
            // 
            this.filterTabPage.Controls.Add(this.addExclusionWordButton);
            this.filterTabPage.Controls.Add(this.filterListLabel);
            this.filterTabPage.Controls.Add(this.addFilterLabel);
            this.filterTabPage.Controls.Add(this.deleteButton);
            this.filterTabPage.Controls.Add(this.filterListBox);
            this.filterTabPage.Controls.Add(this.addMatchWordButton);
            this.filterTabPage.Controls.Add(this.addWordTextBox);
            this.filterTabPage.Location = new System.Drawing.Point(0, 0);
            this.filterTabPage.Size = new System.Drawing.Size(232, 242);
            this.filterTabPage.Text = "フィルター";
            // 
            // addExclusionWordButton
            // 
            this.addExclusionWordButton.Location = new System.Drawing.Point(161, 53);
            this.addExclusionWordButton.Size = new System.Drawing.Size(72, 20);
            this.addExclusionWordButton.Text = "除外(&E)";
            this.addExclusionWordButton.Click += new System.EventHandler(this.AddExclusionWordButton_Click);
            // 
            // filterListLabel
            // 
            this.filterListLabel.Location = new System.Drawing.Point(3, 76);
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
            this.filterListBox.Location = new System.Drawing.Point(3, 99);
            this.filterListBox.Size = new System.Drawing.Size(234, 114);
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
            // addMatchWordButton
            // 
            this.addMatchWordButton.Location = new System.Drawing.Point(83, 53);
            this.addMatchWordButton.Size = new System.Drawing.Size(72, 20);
            this.addMatchWordButton.Text = "一致(&M)";
            this.addMatchWordButton.Click += new System.EventHandler(this.AddMatchWordButton_Click);
            // 
            // addWordTextBox
            // 
            this.addWordTextBox.Location = new System.Drawing.Point(3, 27);
            this.addWordTextBox.Size = new System.Drawing.Size(230, 21);
            this.addWordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddWordTextBox_KeyDown);
            this.addWordTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AddWordTextBox_KeyUp);
            this.addWordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddWordTextBox_KeyPress);
            // 
            // filter2TabPage
            // 
            this.filter2TabPage.Controls.Add(this.filterBelowBitRateLabel);
            this.filter2TabPage.Controls.Add(this.filterBelowBitRateUseCheckBox);
            this.filter2TabPage.Controls.Add(this.filterBelowBitRateTextBox);
            this.filter2TabPage.Controls.Add(this.filterAboveBitRateLabel);
            this.filter2TabPage.Controls.Add(this.filterAboveBitRateUseCheckBox);
            this.filter2TabPage.Controls.Add(this.filterAboveBitRateTextBox);
            this.filter2TabPage.Location = new System.Drawing.Point(0, 0);
            this.filter2TabPage.Size = new System.Drawing.Size(232, 242);
            this.filter2TabPage.Text = "フィルター2";
            // 
            // filterBelowBitRateLabel
            // 
            this.filterBelowBitRateLabel.Location = new System.Drawing.Point(66, 83);
            this.filterBelowBitRateLabel.Size = new System.Drawing.Size(171, 20);
            this.filterBelowBitRateLabel.Text = "Kbps以下";
            // 
            // filterBelowBitRateUseCheckBox
            // 
            this.filterBelowBitRateUseCheckBox.Location = new System.Drawing.Point(3, 56);
            this.filterBelowBitRateUseCheckBox.Size = new System.Drawing.Size(234, 20);
            this.filterBelowBitRateUseCheckBox.Text = "最大ビットレートを設定する";
            // 
            // filterBelowBitRateTextBox
            // 
            this.filterBelowBitRateTextBox.Location = new System.Drawing.Point(3, 82);
            this.filterBelowBitRateTextBox.Size = new System.Drawing.Size(57, 21);
            // 
            // filterAboveBitRateLabel
            // 
            this.filterAboveBitRateLabel.Location = new System.Drawing.Point(66, 30);
            this.filterAboveBitRateLabel.Size = new System.Drawing.Size(171, 20);
            this.filterAboveBitRateLabel.Text = "Kbps以上";
            // 
            // filterAboveBitRateUseCheckBox
            // 
            this.filterAboveBitRateUseCheckBox.Location = new System.Drawing.Point(3, 3);
            this.filterAboveBitRateUseCheckBox.Size = new System.Drawing.Size(234, 20);
            this.filterAboveBitRateUseCheckBox.Text = "最低ビットレートを設定する";
            // 
            // filterAboveBitRateTextBox
            // 
            this.filterAboveBitRateTextBox.Location = new System.Drawing.Point(3, 29);
            this.filterAboveBitRateTextBox.Size = new System.Drawing.Size(57, 21);
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.icecastTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "Icecast設定";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);

        }
        #endregion

        /// <summary>
        /// 単語フィルターを追加するために設定ダイアログを開く
        /// </summary>
        /// <param name="filterWord">単語フィルターに追加する単語</param>
        public void ShowDialogForAddWordFilter(string filterWord)
        {
            icecastTabControl.SelectedIndex = 2;
            addWordTextBox.Text = filterWord;
            ShowDialog();
        }

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // コンボボックスの初期化
            foreach (string fetchChannelKey in icecastFetchChannelNums)
            {
                fetchChannelDomainUpDown.Items.Add(fetchChannelKey);
            }

            #region 設定の読み込み

            stationNameTextBox.Text = setting.ParentHeadline.ParentStation.Name;

            // fetchChannelComboBoxの位置あわせ
            if (setting.FetchChannelNum == UserSetting.ALL_CHANNEL_FETCH)
            {
                fetchChannelDomainUpDown.SelectedIndex = 0;
            }
            else
            {
                fetchChannelDomainUpDown.Text = setting.FetchChannelNum.ToString();
            }

            headlineViewTypeTextBox.Text = setting.HeadlineViewType;

            // フィルターリストに単語フィルタの内容を追加する
            alFilterMatchWords.AddRange(setting.GetFilterMatchWords());
            foreach (string word in setting.GetFilterMatchWords())
            {
                filterListBox.Items.Add("+ " + word);
            }

            // フィルターリストに単語フィルタの内容を追加する
            alFilterExclusionWords.AddRange(setting.GetFilterExclusionWords());
            foreach (string word in setting.GetFilterExclusionWords())
            {
                filterListBox.Items.Add("- " + word);
            }

            // ビットレートフィルターを読み込む
            filterAboveBitRateUseCheckBox.Checked = setting.FilterAboveBitRateUse;
            filterAboveBitRateTextBox.Text = setting.FilterAboveBitRate.ToString();
            filterBelowBitRateUseCheckBox.Checked = setting.FilterBelowBitRateUse;
            filterBelowBitRateTextBox.Text = setting.FilterBelowBitRate.ToString();

            #endregion
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 単語フィルターを追加し忘れていると思われる場合
            if (addWordTextBox.Text.Trim().Length != 0)
            {
                // 追加するかを聞く
                DialogResult result = MessageBox.Show(
                    addWordTextBox.Text.Trim() + "を追加しますか？\n（" + addWordTextBox.Text.Trim() + "はまだ追加されていません）"
                    , "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    alFilterExclusionWords.Add(addWordTextBox.Text.Trim());
                    filterListBox.Items.Add(addWordTextBox.Text.Trim());
                    addWordTextBox.Text = string.Empty;
                }
            }

            #region 設定の書き込み

            setting.ParentHeadline.ParentStation.Name = stationNameTextBox.Text.Trim();

            if (fetchChannelDomainUpDown.Text == FETCH_ALL_CHANNEL)
            {
                setting.FetchNumAllChannel();
            }
            else
            {
                try
                {
                    int fetchNum = int.Parse(fetchChannelDomainUpDown.Text.ToString());
                    setting.FetchChannelNum = fetchNum;
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
            }

            setting.HeadlineViewType = headlineViewTypeTextBox.Text.Trim();

            setting.SetFilterMatchWords((string[])alFilterMatchWords.ToArray(typeof(string)));
            setting.SetFilterExclusionWords((string[])alFilterExclusionWords.ToArray(typeof(string)));

            #region ビットレートフィルターの有効・無効設定書き込み

            setting.FilterAboveBitRateUse = filterAboveBitRateUseCheckBox.Checked;
            try
            {
                setting.FilterAboveBitRate = int.Parse(filterAboveBitRateTextBox.Text.Trim());
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

            setting.FilterBelowBitRateUse = filterBelowBitRateUseCheckBox.Checked;
            try
            {
                setting.FilterBelowBitRate = int.Parse(filterBelowBitRateTextBox.Text.Trim());
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

            #endregion

            try
            {
                setting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが書き込めませんでした", "設定ファイル書き込みエラー");
            }

            #endregion
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

        private void AddMatchWordButton_Click(object sender, System.EventArgs e)
        {
            if (addWordTextBox.Text.Trim().Length != 0)
            {
                alFilterMatchWords.Add(addWordTextBox.Text.Trim());
                filterListBox.Items.Add("+ "+addWordTextBox.Text.Trim());
                addWordTextBox.Text = string.Empty;
            }
        }

        private void AddExclusionWordButton_Click(object sender, EventArgs e)
        {
            if (addWordTextBox.Text.Trim().Length != 0)
            {
                alFilterExclusionWords.Add(addWordTextBox.Text.Trim());
                filterListBox.Items.Add("- " + addWordTextBox.Text.Trim());
                addWordTextBox.Text = string.Empty;
            }
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            if (filterListBox.SelectedIndex != -1)
            {
                switch (((string)filterListBox.Items[filterListBox.SelectedIndex]).Substring(0, 2))
                {
                    case "+ ":
                        alFilterMatchWords.Remove(((string)filterListBox.Items[filterListBox.SelectedIndex]).Substring(2));
                        break;
                    case "- ":
                        alFilterExclusionWords.Remove(((string)filterListBox.Items[filterListBox.SelectedIndex]).Substring(2));
                        break;
                    default:
                        break;
                }

                filterListBox.Items.RemoveAt(filterListBox.SelectedIndex);
            }
        }

        private void DeleteMenuItem_Click(object sender, System.EventArgs e)
        {
            if (filterListBox.SelectedIndex != -1)
            {
                switch (((string)filterListBox.Items[filterListBox.SelectedIndex]).Substring(0, 2))
                {
                    case "+ ":
                        alFilterMatchWords.Remove(((string)filterListBox.Items[filterListBox.SelectedIndex]).Substring(2));
                        break;
                    case "- ":
                        alFilterExclusionWords.Remove(((string)filterListBox.Items[filterListBox.SelectedIndex]).Substring(2));
                        break;
                    default:
                        break;
                }

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
                AddMatchWordButton_Click(sender, e);
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
