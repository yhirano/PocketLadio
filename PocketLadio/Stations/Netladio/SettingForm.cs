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

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// �˂Ƃ炶�̐ݒ�t�H�[��
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
        private TabPage filter2TabPage;
        private CheckBox filterAboveBitRateUseCheckBox;
        private TextBox filterAboveBitRateTextBox;
        private Label filterAboveBitRateLabel;
        private Label filterBelowBitRateLabel;
        private CheckBox filterBelowBitRateUseCheckBox;
        private TextBox filterBelowBitRateTextBox;
        private Label sortLabel;
        private ComboBox sortKindComboBox;
        private Panel panel1;
        private RadioButton sortDescendingRadioButton;
        private RadioButton sortAscendingRadioButton;

        /// <summary>
        /// �ݒ�
        /// </summary>
        private UserSetting setting;


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
            this.filterListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.deleteFilterListMenuItem = new System.Windows.Forms.MenuItem();
            this.addWordButton = new System.Windows.Forms.Button();
            this.addWordTextBox = new System.Windows.Forms.TextBox();
            this.addWordContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.copyAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteAddWordMenuItem = new System.Windows.Forms.MenuItem();
            this.filter2TabPage = new System.Windows.Forms.TabPage();
            this.filterBelowBitRateLabel = new System.Windows.Forms.Label();
            this.filterBelowBitRateUseCheckBox = new System.Windows.Forms.CheckBox();
            this.filterBelowBitRateTextBox = new System.Windows.Forms.TextBox();
            this.filterAboveBitRateLabel = new System.Windows.Forms.Label();
            this.filterAboveBitRateUseCheckBox = new System.Windows.Forms.CheckBox();
            this.filterAboveBitRateTextBox = new System.Windows.Forms.TextBox();
            this.sortLabel = new System.Windows.Forms.Label();
            this.sortKindComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sortAscendingRadioButton = new System.Windows.Forms.RadioButton();
            this.sortDescendingRadioButton = new System.Windows.Forms.RadioButton();
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
            this.netladioTabControl.Controls.Add(this.filter2TabPage);
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
            this.netladioTabPage.Text = "�˂Ƃ炶�ݒ�";
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
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 139);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(135, 20);
            this.headlineViewTypeLabel.Text = "�w�b�h���C���̕\�����@";
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
            this.headlineGetWayLabel.Text = "�w�b�h���C���̎擾���@";
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
            this.cutHeadlineCvsUrlMenuItem.Text = "�؂���(&T)";
            this.cutHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.CutHeadlineCvsUrlMenuItem_Click);
            // 
            // copyHeadlineCvsUrlMenuItem
            // 
            this.copyHeadlineCvsUrlMenuItem.Text = "�R�s�[(&C)";
            this.copyHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.CopyHeadlineCvsUrlMenuItem_Click);
            // 
            // pasteHeadlineCvsUrlMenuItem
            // 
            this.pasteHeadlineCvsUrlMenuItem.Text = "�\��t��(&P)";
            this.pasteHeadlineCvsUrlMenuItem.Click += new System.EventHandler(this.PasteHeadlineCvsUrlMenuItem_Click);
            // 
            // headlineXmlUrlLabel
            // 
            this.headlineXmlUrlLabel.Location = new System.Drawing.Point(3, 47);
            this.headlineXmlUrlLabel.Size = new System.Drawing.Size(124, 16);
            this.headlineXmlUrlLabel.Text = "�w�b�h���C����URL XML";
            // 
            // headlineCvsUrlLabel
            // 
            this.headlineCvsUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.headlineCvsUrlLabel.Size = new System.Drawing.Size(124, 16);
            this.headlineCvsUrlLabel.Text = "�w�b�h���C����URL CSV";
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
            this.filterTabPage.Text = "�t�B���^�[�ݒ�";
            // 
            // filterListLabel
            // 
            this.filterListLabel.Location = new System.Drawing.Point(3, 51);
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
            this.deleteFilterListMenuItem.Text = "�폜(&D)";
            this.deleteFilterListMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // addWordButton
            // 
            this.addWordButton.Location = new System.Drawing.Point(165, 27);
            this.addWordButton.Size = new System.Drawing.Size(72, 20);
            this.addWordButton.Text = "�ǉ�(&A)";
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
            this.filter2TabPage.Controls.Add(this.panel1);
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
            this.filter2TabPage.Text = "�t�B���^�[�ݒ�2";
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
            // sortLabel
            // 
            this.sortLabel.Location = new System.Drawing.Point(3, 121);
            this.sortLabel.Size = new System.Drawing.Size(57, 20);
            this.sortLabel.Text = "���ёւ�";
            // 
            // sortKindComboBox
            // 
            this.sortKindComboBox.Items.Add("���ёւ����Ȃ�");
            this.sortKindComboBox.Items.Add("�^�C�g��");
            this.sortKindComboBox.Items.Add("�����J�n����");
            this.sortKindComboBox.Items.Add("�����X�i��");
            this.sortKindComboBox.Items.Add("�����X�i��");
            this.sortKindComboBox.Items.Add("�r�b�g���[�g");
            this.sortKindComboBox.Location = new System.Drawing.Point(66, 121);
            this.sortKindComboBox.Size = new System.Drawing.Size(171, 22);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sortDescendingRadioButton);
            this.panel1.Controls.Add(this.sortAscendingRadioButton);
            this.panel1.Location = new System.Drawing.Point(66, 149);
            this.panel1.Size = new System.Drawing.Size(171, 28);
            // 
            // sortAscendingRadioButton
            // 
            this.sortAscendingRadioButton.Checked = true;
            this.sortAscendingRadioButton.Location = new System.Drawing.Point(3, 3);
            this.sortAscendingRadioButton.Size = new System.Drawing.Size(79, 20);
            this.sortAscendingRadioButton.Text = "����";
            // 
            // sortDescendingRadioButton
            // 
            this.sortDescendingRadioButton.Location = new System.Drawing.Point(88, 3);
            this.sortDescendingRadioButton.Size = new System.Drawing.Size(79, 20);
            this.sortDescendingRadioButton.Text = "�~��";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.netladioTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "�˂Ƃ炶�ݒ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // �ݒ�̓ǂݍ���
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

            // �t�B���^�[���X�g�Ƀt�B���^�̓��e��ǉ�����
            foreach (string word in setting.GetFilterWords())
            {
                filterListBox.Items.Add(word);
            }

            // �r�b�g���[�g�t�B���^�[��ǂݍ���
            filterAboveBitRateUseCheckBox.Checked = setting.FilterAboveBitRateUse;
            filterAboveBitRateTextBox.Text = setting.FilterAboveBitRate.ToString();
            filterBelowBitRateUseCheckBox.Checked = setting.FilterBelowBitRateUse;
            filterBelowBitRateTextBox.Text = setting.FilterBelowBitRate.ToString();

            // �\�[�g��ނ�ǂݍ���
            if (setting.SortKind == Headline.SortKind.None)
            {
                sortKindComboBox.SelectedIndex = 0;
            }
            else if (setting.SortKind == Headline.SortKind.Nam)
            {
                sortKindComboBox.SelectedIndex = 1;
            }
            else if (setting.SortKind == Headline.SortKind.Tims)
            {
                sortKindComboBox.SelectedIndex = 2;
            }
            else if (setting.SortKind == Headline.SortKind.Cln)
            {
                sortKindComboBox.SelectedIndex = 3;
            }
            else if (setting.SortKind == Headline.SortKind.Clns)
            {
                sortKindComboBox.SelectedIndex = 4;
            }
            else if (setting.SortKind == Headline.SortKind.Bit)
            {
                sortKindComboBox.SelectedIndex = 5;
            }
            else
            {
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
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
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �t�B���^�[��ǉ����Y��Ă���Ǝv����ꍇ
            if (addWordTextBox.Text.Trim().Length != 0)
            {
                // �ǉ����邩�𕷂�
                DialogResult result = MessageBox.Show(
                    addWordTextBox.Text.Trim() + "��ǉ����܂����H\n�i" + addWordTextBox.Text.Trim() + "�͂܂��ǉ�����Ă��܂���j"
                    , "����", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    filterListBox.Items.Add(addWordTextBox.Text.Trim());
                    addWordTextBox.Text = "";
                }
            }

            // �ݒ�̏�������
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
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }
            setting.HeadlineViewType = headlineViewTypeTextBox.Text.Trim();

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

            // �\�[�g��ނ���������
            if (sortKindComboBox.SelectedIndex == 0)
            {
                setting.SortKind = Headline.SortKind.None;
            }
            else if (sortKindComboBox.SelectedIndex == 1)
            {
                setting.SortKind = Headline.SortKind.Nam;
            }
            else if (sortKindComboBox.SelectedIndex == 2)
            {
                setting.SortKind = Headline.SortKind.Tims;
            }
            else if (sortKindComboBox.SelectedIndex == 3)
            {
                setting.SortKind = Headline.SortKind.Cln;
            }
            else if (sortKindComboBox.SelectedIndex == 4)
            {
                setting.SortKind = Headline.SortKind.Clns;
            }
            else if (sortKindComboBox.SelectedIndex == 5)
            {
                setting.SortKind = Headline.SortKind.Bit;
            }
            else
            {
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
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
                // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
            }

            try
            {
                setting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("�ݒ�t�@�C�����������߂܂���ł���", "�ݒ�t�@�C���������݃G���[");
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
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(headlineCsvUrlTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(headlineCsvUrlTextBox);
            }
        }

        private void HeadlineCsvUrlTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(headlineCsvUrlTextBox);
            }
        }

        private void HeadlineXmlUrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(headlineXmlUrlTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(headlineXmlUrlTextBox);
            }
        }

        private void HeadlineXmlUrlTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(headlineXmlUrlTextBox);
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
            // ���̓{�^�����������Ƃ�
            if (e.KeyCode == Keys.Enter)
            {
                AddWordButton_Click(sender, e);
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

        private void ProxyPortTextBox_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void ProxyPortTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
