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

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// ねとらじの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private TabControl shoutCastSettingTabControl;
        private TabPage shoutCastTabPage;
        private MenuItem okMenuItem;
        private MainMenu mainMenu;
        private ContextMenu headlineViewTypeContextMenu;
        private MenuItem cutHeadlineViewTypeMenuItem;
        private MenuItem copyHeadlineViewTypeMenuItem;
        private MenuItem pasteHeadlineViewTypeMenuItem;
        private ContextMenu searchWordContextMenu;
        private MenuItem cutSearchWordMenuItem;
        private MenuItem copySearchWordMenuItem;
        private MenuItem pastSearchWordMenuItem;
        private TextBox headlineViewTypeTextBox;
        private Label headlineViewTypeLabel;
        private TextBox searchWordTextBox;
        private Label searchWordLabel;
        private ComboBox perViewComboBox;
        private Label perViewLabel;
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
        private TabPage filter2TabPage;
        private Panel sortScendingPanel;
        private ComboBox sortKindComboBox;
        private Label sortLabel;
        private Label filterBelowBitRateLabel;
        private CheckBox filterBelowBitRateUseCheckBox;
        private TextBox filterBelowBitRateTextBox;
        private Label filterAboveBitRateLabel;
        private CheckBox filterAboveBitRateUseCheckBox;
        private TextBox filterAboveBitRateTextBox;
        private RadioButton sortDescendingRadioButton;
        private RadioButton sortAscendingRadioButton;

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
            this.shoutCastSettingTabControl = new System.Windows.Forms.TabControl();
            this.shoutCastTabPage = new System.Windows.Forms.TabPage();
            this.perViewComboBox = new System.Windows.Forms.ComboBox();
            this.perViewLabel = new System.Windows.Forms.Label();
            this.headlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.headlineViewTypeContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.copyHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteHeadlineViewTypeMenuItem = new System.Windows.Forms.MenuItem();
            this.headlineViewTypeLabel = new System.Windows.Forms.Label();
            this.searchWordTextBox = new System.Windows.Forms.TextBox();
            this.searchWordContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutSearchWordMenuItem = new System.Windows.Forms.MenuItem();
            this.copySearchWordMenuItem = new System.Windows.Forms.MenuItem();
            this.pastSearchWordMenuItem = new System.Windows.Forms.MenuItem();
            this.searchWordLabel = new System.Windows.Forms.Label();
            this.filterTabPage = new System.Windows.Forms.TabPage();
            this.filterListLabel = new System.Windows.Forms.Label();
            this.addFilterLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.filterListBox = new System.Windows.Forms.ListBox();
            this.filterListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.deleteFilterListMenuItem = new System.Windows.Forms.MenuItem();
            this.addWordButton = new System.Windows.Forms.Button();
            this.addWordTextBox = new System.Windows.Forms.TextBox();
            this.addWordContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.copyAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.filter2TabPage = new System.Windows.Forms.TabPage();
            this.sortScendingPanel = new System.Windows.Forms.Panel();
            this.sortDescendingRadioButton = new System.Windows.Forms.RadioButton();
            this.sortAscendingRadioButton = new System.Windows.Forms.RadioButton();
            this.sortKindComboBox = new System.Windows.Forms.ComboBox();
            this.sortLabel = new System.Windows.Forms.Label();
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
            // shoutCastSettingTabControl
            // 
            this.shoutCastSettingTabControl.Controls.Add(this.shoutCastTabPage);
            this.shoutCastSettingTabControl.Controls.Add(this.filterTabPage);
            this.shoutCastSettingTabControl.Controls.Add(this.filter2TabPage);
            this.shoutCastSettingTabControl.Location = new System.Drawing.Point(0, 0);
            this.shoutCastSettingTabControl.SelectedIndex = 0;
            this.shoutCastSettingTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // shoutCastTabPage
            // 
            this.shoutCastTabPage.Controls.Add(this.perViewComboBox);
            this.shoutCastTabPage.Controls.Add(this.perViewLabel);
            this.shoutCastTabPage.Controls.Add(this.headlineViewTypeTextBox);
            this.shoutCastTabPage.Controls.Add(this.headlineViewTypeLabel);
            this.shoutCastTabPage.Controls.Add(this.searchWordTextBox);
            this.shoutCastTabPage.Controls.Add(this.searchWordLabel);
            this.shoutCastTabPage.Location = new System.Drawing.Point(0, 0);
            this.shoutCastTabPage.Size = new System.Drawing.Size(240, 245);
            this.shoutCastTabPage.Text = "SHOUTcast設定";
            // 
            // perViewComboBox
            // 
            this.perViewComboBox.Location = new System.Drawing.Point(3, 66);
            this.perViewComboBox.Size = new System.Drawing.Size(122, 22);
            // 
            // perViewLabel
            // 
            this.perViewLabel.Location = new System.Drawing.Point(3, 47);
            this.perViewLabel.Size = new System.Drawing.Size(86, 16);
            this.perViewLabel.Text = "Per View";
            // 
            // headlineViewTypeTextBox
            // 
            this.headlineViewTypeTextBox.ContextMenu = this.headlineViewTypeContextMenu;
            this.headlineViewTypeTextBox.Location = new System.Drawing.Point(3, 114);
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
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 91);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(135, 20);
            this.headlineViewTypeLabel.Text = "ヘッドラインの表示方法";
            // 
            // searchWordTextBox
            // 
            this.searchWordTextBox.ContextMenu = this.searchWordContextMenu;
            this.searchWordTextBox.Location = new System.Drawing.Point(3, 23);
            this.searchWordTextBox.Size = new System.Drawing.Size(234, 21);
            this.searchWordTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchWordTextBox_KeyUp);
            this.searchWordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchWordTextBox_KeyDown);
            // 
            // searchWordContextMenu
            // 
            this.searchWordContextMenu.MenuItems.Add(this.cutSearchWordMenuItem);
            this.searchWordContextMenu.MenuItems.Add(this.copySearchWordMenuItem);
            this.searchWordContextMenu.MenuItems.Add(this.pastSearchWordMenuItem);
            // 
            // cutSearchWordMenuItem
            // 
            this.cutSearchWordMenuItem.Text = "切り取り(&T)";
            this.cutSearchWordMenuItem.Click += new System.EventHandler(this.CutSearchWordMenuItem_Click);
            // 
            // copySearchWordMenuItem
            // 
            this.copySearchWordMenuItem.Text = "コピー(&C)";
            this.copySearchWordMenuItem.Click += new System.EventHandler(this.CopySearchWordMenuItem_Click);
            // 
            // pastSearchWordMenuItem
            // 
            this.pastSearchWordMenuItem.Text = "貼り付け(&P)";
            this.pastSearchWordMenuItem.Click += new System.EventHandler(this.PasteSearchWordMenuItem_Click);
            // 
            // searchWordLabel
            // 
            this.searchWordLabel.Location = new System.Drawing.Point(3, 4);
            this.searchWordLabel.Size = new System.Drawing.Size(86, 16);
            this.searchWordLabel.Text = "Search word";
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
            this.filterTabPage.Size = new System.Drawing.Size(232, 242);
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
            // filter2TabPage
            // 
            this.filter2TabPage.Controls.Add(this.sortScendingPanel);
            this.filter2TabPage.Controls.Add(this.sortKindComboBox);
            this.filter2TabPage.Controls.Add(this.sortLabel);
            this.filter2TabPage.Controls.Add(this.filterBelowBitRateLabel);
            this.filter2TabPage.Controls.Add(this.filterBelowBitRateUseCheckBox);
            this.filter2TabPage.Controls.Add(this.filterBelowBitRateTextBox);
            this.filter2TabPage.Controls.Add(this.filterAboveBitRateLabel);
            this.filter2TabPage.Controls.Add(this.filterAboveBitRateUseCheckBox);
            this.filter2TabPage.Controls.Add(this.filterAboveBitRateTextBox);
            this.filter2TabPage.Location = new System.Drawing.Point(0, 0);
            this.filter2TabPage.Size = new System.Drawing.Size(240, 245);
            this.filter2TabPage.Text = "フィルター設定2";
            // 
            // sortScendingPanel
            // 
            this.sortScendingPanel.Controls.Add(this.sortDescendingRadioButton);
            this.sortScendingPanel.Controls.Add(this.sortAscendingRadioButton);
            this.sortScendingPanel.Location = new System.Drawing.Point(66, 149);
            this.sortScendingPanel.Size = new System.Drawing.Size(171, 28);
            // 
            // sortDescendingRadioButton
            // 
            this.sortDescendingRadioButton.Location = new System.Drawing.Point(88, 3);
            this.sortDescendingRadioButton.Size = new System.Drawing.Size(79, 20);
            this.sortDescendingRadioButton.Text = "降順";
            // 
            // sortAscendingRadioButton
            // 
            this.sortAscendingRadioButton.Checked = true;
            this.sortAscendingRadioButton.Location = new System.Drawing.Point(3, 3);
            this.sortAscendingRadioButton.Size = new System.Drawing.Size(79, 20);
            this.sortAscendingRadioButton.Text = "昇順";
            // 
            // sortKindComboBox
            // 
            this.sortKindComboBox.Items.Add("並び替えしない");
            this.sortKindComboBox.Items.Add("タイトル");
            this.sortKindComboBox.Items.Add("リスナ数");
            this.sortKindComboBox.Items.Add("述べリスナ数");
            this.sortKindComboBox.Items.Add("ビットレート");
            this.sortKindComboBox.Location = new System.Drawing.Point(66, 121);
            this.sortKindComboBox.Size = new System.Drawing.Size(171, 22);
            // 
            // sortLabel
            // 
            this.sortLabel.Location = new System.Drawing.Point(3, 121);
            this.sortLabel.Size = new System.Drawing.Size(57, 20);
            this.sortLabel.Text = "並び替え";
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
            this.Controls.Add(this.shoutCastSettingTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "SHOUTcast設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            #region コンボボックスの初期化

            // Per Viewコンボボックスの初期化
            foreach (string perViewKey in PocketLadioInfo.ShoutcastPerViewNums)
            {
                perViewComboBox.Items.Add(perViewKey);
            }

            #endregion

            #region 設定の読み込み

            // 検索対象の読み込み
            searchWordTextBox.Text = setting.SearchWord.Trim();

            // perViewComboBoxの位置あわせ
            perViewComboBox.SelectedIndex = 0;
            for (int count = 0; count < perViewComboBox.Items.Count; ++count)
            {
                perViewComboBox.SelectedIndex = count;
                if (perViewComboBox.SelectedItem.ToString() == setting.PerView)
                {
                    break;
                }
            }

            //ヘッドライン表示方法の読み込み
            headlineViewTypeTextBox.Text = setting.HeadlineViewType;

            // フィルターリストに単語フィルタの内容を追加する
            foreach (string word in setting.GetFilterWords())
            {
                filterListBox.Items.Add(word);
            }

            // ビットレートフィルターを読み込む
            filterAboveBitRateUseCheckBox.Checked = setting.FilterAboveBitRateUse;
            filterAboveBitRateTextBox.Text = setting.FilterAboveBitRate.ToString();
            filterBelowBitRateUseCheckBox.Checked = setting.FilterBelowBitRateUse;
            filterBelowBitRateTextBox.Text = setting.FilterBelowBitRate.ToString();

            // ソート種類を読み込む
            if (setting.SortKind == Headline.SortKind.None)
            {
                sortKindComboBox.SelectedIndex = 0;
            }
            else if (setting.SortKind == Headline.SortKind.Title)
            {
                sortKindComboBox.SelectedIndex = 1;
            }
            else if (setting.SortKind == Headline.SortKind.Listener)
            {
                sortKindComboBox.SelectedIndex = 2;
            }
            else if (setting.SortKind == Headline.SortKind.ListenerTotal)
            {
                sortKindComboBox.SelectedIndex = 3;
            }
            else if (setting.SortKind == Headline.SortKind.BitRate)
            {
                sortKindComboBox.SelectedIndex = 4;
            }
            else
            {
                // ここに到達することはあり得ない
                Trace.Assert(false, "想定外の動作のため、終了します");
            }

            if (setting.SortScending == Headline.SortScending.Ascending)
            {
                sortDescendingRadioButton.Checked = false;
                sortAscendingRadioButton.Checked = true;
            }
            else if (setting.SortScending == Headline.SortScending.Descending)
            {
                sortAscendingRadioButton.Checked = false;
                sortDescendingRadioButton.Checked = true;
            }
            else
            {
                // ここに到達することはあり得ない
                Trace.Assert(false, "想定外の動作のため、終了します");
            }
            
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
                    filterListBox.Items.Add(addWordTextBox.Text.Trim());
                    addWordTextBox.Text = "";
                }
            }

            #region 設定の書き込み

            setting.SearchWord = searchWordTextBox.Text.Trim();
            setting.PerView = perViewComboBox.SelectedItem.ToString().Trim();
            setting.HeadlineViewType = headlineViewTypeTextBox.Text.Trim();

            #region 単語フィルターの書き込み
            
            ArrayList alFilterWord = new ArrayList();
            IEnumerator filterEnumerator = filterListBox.Items.GetEnumerator();
            while (filterEnumerator.MoveNext())
            {
                alFilterWord.Add(((string)filterEnumerator.Current).Trim());
            }
            setting.SetFilterWords((string[])alFilterWord.ToArray(typeof(string)));


            #endregion

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

            #region ソート種類を書き込み

            if (sortKindComboBox.Text.Trim() == "並び替えしない")
            {
                setting.SortKind = Headline.SortKind.None;
            }
            else if (sortKindComboBox.Text.Trim() == "タイトル")
            {
                setting.SortKind = Headline.SortKind.Title;
            }
            else if (sortKindComboBox.Text.Trim() == "リスナ数")
            {
                setting.SortKind = Headline.SortKind.Listener;
            }
            else if (sortKindComboBox.Text.Trim() == "述べリスナ数")
            {
                setting.SortKind = Headline.SortKind.ListenerTotal;
            }
            else if (sortKindComboBox.Text.Trim() == "ビットレート")
            {
                setting.SortKind = Headline.SortKind.BitRate;
            }
            else
            {
                // ここに到達することはあり得ない
                Trace.Assert(false, "想定外の動作のため、終了します");
            }

            if (sortAscendingRadioButton.Checked == true)
            {
                setting.SortScending = Headline.SortScending.Ascending;
            }
            else if (sortDescendingRadioButton.Checked == true)
            {
                setting.SortScending = Headline.SortScending.Descending;
            }
            else
            {
                // ここに到達することはあり得ない
                Trace.Assert(false, "想定外の動作のため、終了します");
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

        private void CutSearchWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(searchWordTextBox);
        }

        private void CopySearchWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(searchWordTextBox);
        }

        private void PasteSearchWordMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(searchWordTextBox);
        }

        private void SearchWordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 切り取りショートカット
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(searchWordTextBox);
            }
            // 貼り付けショートカット
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(searchWordTextBox);
            }
        }

        private void SearchWordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // コピーショートカット
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(searchWordTextBox);
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
