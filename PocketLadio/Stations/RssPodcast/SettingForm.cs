#region �f�B���N�e�B�u���g�p����

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
    /// Podcast�̐ݒ�t�H�[��
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
        private TabPage filterTabPage;
        private Label filterListLabel;
        private Label addFilterLabel;
        private Button deleteButton;
        private ListBox filterListBox;
        private Button addWordButton;
        private TextBox addWordTextBox;
        private ContextMenu addWordContextMenu;
        private MenuItem cutAddWordMenuItem;
        private MenuItem copyAddWordMenuItem;
        private MenuItem pasteAddWordMenuItem;
        private ContextMenu filterListBoxContextMenu;
        private MenuItem deleteFilterListMenuItem;

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
            this.podcastTabControl.Controls.Add(this.filterTabPage);
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
            this.podcastTabPage.Text = "Podcast�ݒ�";
            // 
            // headlineViewTypeTextBox
            // 
            this.headlineViewTypeTextBox.ContextMenu = this.headlineViewTypeContextMenu;
            this.headlineViewTypeTextBox.Location = new System.Drawing.Point(3, 70);
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
            this.headlineViewTypeLabel.Location = new System.Drawing.Point(3, 47);
            this.headlineViewTypeLabel.Size = new System.Drawing.Size(130, 20);
            this.headlineViewTypeLabel.Text = "�w�b�h���C���̕\�����@";
            // 
            // rssUrlTextBox
            // 
            this.rssUrlTextBox.ContextMenu = this.rssUrlContextMenu;
            this.rssUrlTextBox.Location = new System.Drawing.Point(3, 23);
            this.rssUrlTextBox.Size = new System.Drawing.Size(234, 21);
            this.rssUrlTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RssUrlTextBox_KeyUp);
            this.rssUrlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RssUrlTextBox_KeyDown);
            // 
            // rssUrlContextMenu
            // 
            this.rssUrlContextMenu.MenuItems.Add(this.cutRssUrlMenuItem);
            this.rssUrlContextMenu.MenuItems.Add(this.copyRssUrlMenuItem);
            this.rssUrlContextMenu.MenuItems.Add(this.pasteRssUrlMenuItem);
            // 
            // cutRssUrlMenuItem
            // 
            this.cutRssUrlMenuItem.Text = "�؂���(&T)";
            this.cutRssUrlMenuItem.Click += new System.EventHandler(this.CutRssUrlMenuItem_Click);
            // 
            // copyRssUrlMenuItem
            // 
            this.copyRssUrlMenuItem.Text = "�R�s�[(&C)";
            this.copyRssUrlMenuItem.Click += new System.EventHandler(this.CopyRssUrlMenuItem_Click);
            // 
            // pasteRssUrlMenuItem
            // 
            this.pasteRssUrlMenuItem.Text = "�\��t��(&P)";
            this.pasteRssUrlMenuItem.Click += new System.EventHandler(this.PasteRssUrlMenuItem_Click);
            // 
            // rssUrlLabel
            // 
            this.rssUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.rssUrlLabel.Size = new System.Drawing.Size(109, 16);
            this.rssUrlLabel.Text = "Podcast��RSS URL";
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
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.podcastTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "Podcast�ݒ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            #region �ݒ�̓ǂݍ���

            rssUrlTextBox.Text = ((setting.RssUrl != null) ? setting.RssUrl.ToString() : "");
            headlineViewTypeTextBox.Text = setting.HeadlineViewType;

            // �t�B���^�[���X�g�ɒP��t�B���^�̓��e��ǉ�����
            foreach (string word in setting.GetFilterWords())
            {
                filterListBox.Items.Add(word);
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
                    filterListBox.Items.Add(addWordTextBox.Text.Trim());
                    addWordTextBox.Text = "";
                }
            }

            #region �ݒ�̏�������

            try
            {
                setting.RssUrl = new Uri(rssUrlTextBox.Text.Trim());
            }
            catch (UriFormatException)
            {
                ;
            }
            setting.HeadlineViewType = headlineViewTypeTextBox.Text.Trim();

            ArrayList alFilterWord = new ArrayList();
            IEnumerator filterEnumerator = filterListBox.Items.GetEnumerator();
            while (filterEnumerator.MoveNext())
            {
                alFilterWord.Add(((string)filterEnumerator.Current).Trim());
            }
            setting.SetFilterWords((string[])alFilterWord.ToArray(typeof(string)));

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

        private void RssUrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(rssUrlTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(rssUrlTextBox);
            }
        }

        private void RssUrlTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(rssUrlTextBox);
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
    }
}
