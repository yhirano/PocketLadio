#region �f�B���N�e�B�u���g�p����

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
    /// �˂Ƃ炶�̐ݒ�t�H�[��
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// �ݒ�
        /// </summary>
        private UserSetting setting;

        /// <summary>
        /// ��v�P��t�B���^�[
        /// </summary>
        private ArrayList alFilterMatchWords = new ArrayList();

        /// <summary>
        /// ���O�P��t�B���^�[
        /// </summary>
        private ArrayList alFilterExclusionWords = new ArrayList();

        /// <summary>
        /// SHOUTcast�̔ԑg�\�擾���̃��X�g
        /// </summary>
        private static readonly string[] shoutcastPerViewNums = { "5", "10", "25", "30", "50", "100" };

        private TabControl shoutCastTabControl;
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
        private Button addMatchWordButton;
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
        private TabPage stationTabPage;
        private TextBox stationNameTextBox;
        private Label stationNameLabel;
        private Button addExclusionWordButton;

        public SettingForm(UserSetting setting)
        {
            this.setting = setting;

            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.shoutCastTabControl = new System.Windows.Forms.TabControl();
            this.stationTabPage = new System.Windows.Forms.TabPage();
            this.stationNameTextBox = new System.Windows.Forms.TextBox();
            this.stationNameLabel = new System.Windows.Forms.Label();
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
            this.addMatchWordButton = new System.Windows.Forms.Button();
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
            this.addExclusionWordButton = new System.Windows.Forms.Button();
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
            this.shoutCastTabControl.Controls.Add(this.stationTabPage);
            this.shoutCastTabControl.Controls.Add(this.shoutCastTabPage);
            this.shoutCastTabControl.Controls.Add(this.filterTabPage);
            this.shoutCastTabControl.Controls.Add(this.filter2TabPage);
            this.shoutCastTabControl.Location = new System.Drawing.Point(0, 0);
            this.shoutCastTabControl.SelectedIndex = 0;
            this.shoutCastTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // stationTabPage
            // 
            this.stationTabPage.Controls.Add(this.stationNameTextBox);
            this.stationTabPage.Controls.Add(this.stationNameLabel);
            this.stationTabPage.Location = new System.Drawing.Point(0, 0);
            this.stationTabPage.Size = new System.Drawing.Size(240, 245);
            this.stationTabPage.Text = "������";
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
            this.stationNameLabel.Text = "�����ǖ�";
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
            this.shoutCastTabPage.Size = new System.Drawing.Size(232, 242);
            this.shoutCastTabPage.Text = "SHOUTcast";
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
            this.cutHeadlineViewTypeMenuItem.Text = "�؂���(&T)";
            this.cutHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CutHeadlineViewTypeMenuItem_Click);
            // 
            // copyHeadlineViewTypeMenuItem
            // 
            this.copyHeadlineViewTypeMenuItem.Text = "�R�s�[(&C)";
            this.copyHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.CopyHeadlineViewTypeMenuItem_Click);
            // 
            // pasteHeadlineViewTypeMenuItem
            // 
            this.pasteHeadlineViewTypeMenuItem.Text = "�\��t��(&P)";
            this.pasteHeadlineViewTypeMenuItem.Click += new System.EventHandler(this.PasteHeadlineViewTypeMenuItem_Click);
            // 
            // headlineViewTypeLabel
            // 
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 91);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(135, 20);
            this.headlineViewTypeLabel.Text = "�w�b�h���C���̕\�����@";
            // 
            // searchWordTextBox
            // 
            this.searchWordTextBox.ContextMenu = this.searchWordContextMenu;
            this.searchWordTextBox.Location = new System.Drawing.Point(3, 23);
            this.searchWordTextBox.Size = new System.Drawing.Size(234, 21);
            this.searchWordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchWordTextBox_KeyDown);
            this.searchWordTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchWordTextBox_KeyUp);
            // 
            // searchWordContextMenu
            // 
            this.searchWordContextMenu.MenuItems.Add(this.cutSearchWordMenuItem);
            this.searchWordContextMenu.MenuItems.Add(this.copySearchWordMenuItem);
            this.searchWordContextMenu.MenuItems.Add(this.pastSearchWordMenuItem);
            // 
            // cutSearchWordMenuItem
            // 
            this.cutSearchWordMenuItem.Text = "�؂���(&T)";
            this.cutSearchWordMenuItem.Click += new System.EventHandler(this.CutSearchWordMenuItem_Click);
            // 
            // copySearchWordMenuItem
            // 
            this.copySearchWordMenuItem.Text = "�R�s�[(&C)";
            this.copySearchWordMenuItem.Click += new System.EventHandler(this.CopySearchWordMenuItem_Click);
            // 
            // pastSearchWordMenuItem
            // 
            this.pastSearchWordMenuItem.Text = "�\��t��(&P)";
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
            this.filterTabPage.Controls.Add(this.addExclusionWordButton);
            this.filterTabPage.Controls.Add(this.filterListLabel);
            this.filterTabPage.Controls.Add(this.addFilterLabel);
            this.filterTabPage.Controls.Add(this.deleteButton);
            this.filterTabPage.Controls.Add(this.filterListBox);
            this.filterTabPage.Controls.Add(this.addMatchWordButton);
            this.filterTabPage.Controls.Add(this.addWordTextBox);
            this.filterTabPage.Location = new System.Drawing.Point(0, 0);
            this.filterTabPage.Size = new System.Drawing.Size(240, 245);
            this.filterTabPage.Text = "�t�B���^�[";
            // 
            // filterListLabel
            // 
            this.filterListLabel.Location = new System.Drawing.Point(3, 76);
            this.filterListLabel.Size = new System.Drawing.Size(100, 20);
            this.filterListLabel.Text = "�t�B���^�[�ꗗ";
            // 
            // addFilterLabel
            // 
            this.addFilterLabel.Location = new System.Drawing.Point(3, 4);
            this.addFilterLabel.Size = new System.Drawing.Size(100, 20);
            this.addFilterLabel.Text = "�t�B���^�[�̒ǉ�";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(165, 219);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.deleteButton.Text = "�폜(&D)";
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
            this.deleteFilterListMenuItem.Text = "�폜(&D)";
            this.deleteFilterListMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // addMatchWordButton
            // 
            this.addMatchWordButton.Location = new System.Drawing.Point(87, 54);
            this.addMatchWordButton.Size = new System.Drawing.Size(72, 20);
            this.addMatchWordButton.Text = "��v(&M)";
            this.addMatchWordButton.Click += new System.EventHandler(this.AddMatchWordButton_Click);
            // 
            // addWordTextBox
            // 
            this.addWordTextBox.ContextMenu = this.addWordContextMenu;
            this.addWordTextBox.Location = new System.Drawing.Point(3, 27);
            this.addWordTextBox.Size = new System.Drawing.Size(234, 21);
            this.addWordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddWordTextBox_KeyDown);
            this.addWordTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AddWordTextBox_KeyUp);
            this.addWordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddWordTextBox_KeyPress);
            // 
            // addWordContextMenu
            // 
            this.addWordContextMenu.MenuItems.Add(this.cutAddWordMenuItem);
            this.addWordContextMenu.MenuItems.Add(this.copyAddWordMenuItem);
            this.addWordContextMenu.MenuItems.Add(this.pasteAddWordMenuItem);
            // 
            // cutAddWordMenuItem
            // 
            this.cutAddWordMenuItem.Text = "�؂���(&T)";
            this.cutAddWordMenuItem.Click += new System.EventHandler(this.CutAddWordMenuItem_Click);
            // 
            // copyAddWordMenuItem
            // 
            this.copyAddWordMenuItem.Text = "�R�s�[(&C)";
            this.copyAddWordMenuItem.Click += new System.EventHandler(this.CopyAddWordMenuItem_Click);
            // 
            // pasteAddWordMenuItem
            // 
            this.pasteAddWordMenuItem.Text = "�\��t��(&P)";
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
            this.filter2TabPage.Size = new System.Drawing.Size(232, 242);
            this.filter2TabPage.Text = "�t�B���^�[2";
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
            this.sortDescendingRadioButton.Text = "�~��";
            // 
            // sortAscendingRadioButton
            // 
            this.sortAscendingRadioButton.Checked = true;
            this.sortAscendingRadioButton.Location = new System.Drawing.Point(3, 3);
            this.sortAscendingRadioButton.Size = new System.Drawing.Size(79, 20);
            this.sortAscendingRadioButton.Text = "����";
            // 
            // sortKindComboBox
            // 
            this.sortKindComboBox.Items.Add("���ёւ����Ȃ�");
            this.sortKindComboBox.Items.Add("�^�C�g��");
            this.sortKindComboBox.Items.Add("���X�i��");
            this.sortKindComboBox.Items.Add("�q�׃��X�i��");
            this.sortKindComboBox.Items.Add("�r�b�g���[�g");
            this.sortKindComboBox.Location = new System.Drawing.Point(66, 121);
            this.sortKindComboBox.Size = new System.Drawing.Size(171, 22);
            // 
            // sortLabel
            // 
            this.sortLabel.Location = new System.Drawing.Point(3, 121);
            this.sortLabel.Size = new System.Drawing.Size(57, 20);
            this.sortLabel.Text = "���ёւ�";
            // 
            // filterBelowBitRateLabel
            // 
            this.filterBelowBitRateLabel.Location = new System.Drawing.Point(66, 83);
            this.filterBelowBitRateLabel.Size = new System.Drawing.Size(171, 20);
            this.filterBelowBitRateLabel.Text = "Kbps�ȉ�";
            // 
            // filterBelowBitRateUseCheckBox
            // 
            this.filterBelowBitRateUseCheckBox.Location = new System.Drawing.Point(3, 56);
            this.filterBelowBitRateUseCheckBox.Size = new System.Drawing.Size(234, 20);
            this.filterBelowBitRateUseCheckBox.Text = "�ő�r�b�g���[�g��ݒ肷��";
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
            this.filterAboveBitRateLabel.Text = "Kbps�ȏ�";
            // 
            // filterAboveBitRateUseCheckBox
            // 
            this.filterAboveBitRateUseCheckBox.Location = new System.Drawing.Point(3, 3);
            this.filterAboveBitRateUseCheckBox.Size = new System.Drawing.Size(234, 20);
            this.filterAboveBitRateUseCheckBox.Text = "�Œ�r�b�g���[�g��ݒ肷��";
            // 
            // filterAboveBitRateTextBox
            // 
            this.filterAboveBitRateTextBox.Location = new System.Drawing.Point(3, 29);
            this.filterAboveBitRateTextBox.Size = new System.Drawing.Size(57, 21);
            // 
            // addExclusionWordButton
            // 
            this.addExclusionWordButton.Location = new System.Drawing.Point(165, 54);
            this.addExclusionWordButton.Size = new System.Drawing.Size(72, 20);
            this.addExclusionWordButton.Text = "���O(&E)";
            this.addExclusionWordButton.Click += new System.EventHandler(this.addExclusionWordButton_Click);
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.shoutCastTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "SHOUTcast�ݒ�";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);

        }
        #endregion

        /// <summary>
        /// �P��t�B���^�[��ǉ����邽�߂ɐݒ�_�C�A���O���J��
        /// </summary>
        /// <param name="filterWord">�P��t�B���^�[�ɒǉ�����P��</param>
        public void ShowDialogForAddWordFilter(string filterWord)
        {
            shoutCastTabControl.SelectedIndex = 2;
            addWordTextBox.Text = filterWord;
            ShowDialog();
        }

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            #region �R���{�{�b�N�X�̏�����

            // Per View�R���{�{�b�N�X�̏�����
            foreach (string perViewKey in shoutcastPerViewNums)
            {
                perViewComboBox.Items.Add(perViewKey);
            }

            #endregion

            #region �ݒ�̓ǂݍ���

            stationNameTextBox.Text = setting.ParentHeadline.ParentStation.Name;

            // �����Ώۂ̓ǂݍ���
            searchWordTextBox.Text = setting.SearchWord.Trim();

            // perViewComboBox�̈ʒu���킹
            perViewComboBox.SelectedIndex = 0;
            for (int count = 0; count < perViewComboBox.Items.Count; ++count)
            {
                perViewComboBox.SelectedIndex = count;
                if (perViewComboBox.SelectedItem.ToString() == setting.PerView)
                {
                    break;
                }
            }

            //�w�b�h���C���\�����@�̓ǂݍ���
            headlineViewTypeTextBox.Text = setting.HeadlineViewType;

            // �t�B���^�[���X�g�ɒP��t�B���^�̓��e��ǉ�����
            alFilterMatchWords.AddRange(setting.GetFilterMatchWords());
            foreach (string word in setting.GetFilterMatchWords())
            {
                filterListBox.Items.Add("+ " + word);
            }

            // �t�B���^�[���X�g�ɒP��t�B���^�̓��e��ǉ�����
            alFilterExclusionWords.AddRange(setting.GetFilterExclusionWords());
            foreach (string word in setting.GetFilterExclusionWords())
            {
                filterListBox.Items.Add("- " + word);
            }

            // �r�b�g���[�g�t�B���^�[��ǂݍ���
            filterAboveBitRateUseCheckBox.Checked = setting.FilterAboveBitRateUse;
            filterAboveBitRateTextBox.Text = setting.FilterAboveBitRate.ToString();
            filterBelowBitRateUseCheckBox.Checked = setting.FilterBelowBitRateUse;
            filterBelowBitRateTextBox.Text = setting.FilterBelowBitRate.ToString();

            // �\�[�g��ނ�ǂݍ���
            if (setting.SortKind == Headline.SortKinds.None)
            {
                sortKindComboBox.SelectedIndex = 0;
            }
            else if (setting.SortKind == Headline.SortKinds.Title)
            {
                sortKindComboBox.SelectedIndex = 1;
            }
            else if (setting.SortKind == Headline.SortKinds.Listener)
            {
                sortKindComboBox.SelectedIndex = 2;
            }
            else if (setting.SortKind == Headline.SortKinds.ListenerTotal)
            {
                sortKindComboBox.SelectedIndex = 3;
            }
            else if (setting.SortKind == Headline.SortKinds.BitRate)
            {
                sortKindComboBox.SelectedIndex = 4;
            }
            else
            {
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }

            if (setting.SortScending == Headline.SortScendings.Ascending)
            {
                sortDescendingRadioButton.Checked = false;
                sortAscendingRadioButton.Checked = true;
            }
            else if (setting.SortScending == Headline.SortScendings.Descending)
            {
                sortAscendingRadioButton.Checked = false;
                sortDescendingRadioButton.Checked = true;
            }
            else
            {
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }

            #endregion
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �P��t�B���^�[��ǉ����Y��Ă���Ǝv����ꍇ
            if (addWordTextBox.Text.Trim().Length != 0)
            {
                // �ǉ����邩�𕷂�
                DialogResult result = MessageBox.Show(
                    addWordTextBox.Text.Trim() + "��ǉ����܂����H\n�i" + addWordTextBox.Text.Trim() + "�͂܂��ǉ�����Ă��܂���j"
                    , "����", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    alFilterExclusionWords.Add(addWordTextBox.Text.Trim());
                    filterListBox.Items.Add(addWordTextBox.Text.Trim());
                    addWordTextBox.Text = string.Empty;
                }
            }

            #region �ݒ�̏�������

            setting.ParentHeadline.ParentStation.Name = stationNameTextBox.Text.Trim();

            setting.SearchWord = searchWordTextBox.Text.Trim();
            setting.PerView = perViewComboBox.SelectedItem.ToString().Trim();
            setting.HeadlineViewType = headlineViewTypeTextBox.Text.Trim();

            setting.SetFilterMatchWords((string[])alFilterMatchWords.ToArray(typeof(string)));
            setting.SetFilterExclusionWords((string[])alFilterExclusionWords.ToArray(typeof(string)));

            #region �r�b�g���[�g�t�B���^�[�̗L���E�����ݒ菑������

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

            #region �\�[�g��ނ���������

            if (sortKindComboBox.Text.Trim() == "���ёւ����Ȃ�")
            {
                setting.SortKind = Headline.SortKinds.None;
            }
            else if (sortKindComboBox.Text.Trim() == "�^�C�g��")
            {
                setting.SortKind = Headline.SortKinds.Title;
            }
            else if (sortKindComboBox.Text.Trim() == "���X�i��")
            {
                setting.SortKind = Headline.SortKinds.Listener;
            }
            else if (sortKindComboBox.Text.Trim() == "�q�׃��X�i��")
            {
                setting.SortKind = Headline.SortKinds.ListenerTotal;
            }
            else if (sortKindComboBox.Text.Trim() == "�r�b�g���[�g")
            {
                setting.SortKind = Headline.SortKinds.BitRate;
            }
            else
            {
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }

            if (sortAscendingRadioButton.Checked == true)
            {
                setting.SortScending = Headline.SortScendings.Ascending;
            }
            else if (sortDescendingRadioButton.Checked == true)
            {
                setting.SortScending = Headline.SortScendings.Descending;
            }
            else
            {
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }

            #endregion

            try
            {
                setting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("�ݒ�t�@�C�����������߂܂���ł���", "�ݒ�t�@�C���������݃G���[");
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
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(searchWordTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(searchWordTextBox);
            }
        }

        private void SearchWordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(searchWordTextBox);
            }
        }

        private void HeadlineViewTypeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(headlineViewTypeTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(headlineViewTypeTextBox);
            }
        }

        private void HeadlineViewTypeTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(headlineViewTypeTextBox);
            }
        }

        private void AddMatchWordButton_Click(object sender, System.EventArgs e)
        {
            if (addWordTextBox.Text.Trim().Length != 0)
            {
                alFilterMatchWords.Add(addWordTextBox.Text.Trim());
                filterListBox.Items.Add("+ " + addWordTextBox.Text.Trim());
                addWordTextBox.Text = string.Empty;
            }
        }

        private void addExclusionWordButton_Click(object sender, EventArgs e)
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
            // ���̓{�^�����������Ƃ�
            if (e.KeyCode == Keys.Enter)
            {
                AddMatchWordButton_Click(sender, e);
            }
            // �؂���V���[�g�J�b�g
            else if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(addWordTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(addWordTextBox);
            }
        }

        private void AddWordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ���̓{�^�����������Ƃ��̉�����������
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void AddWordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(addWordTextBox);
            }
        }
    }
}
