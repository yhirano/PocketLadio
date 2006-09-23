#region �f�B���N�e�B�u���g�p����

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// �t�B���^�[�̒ǉ��ƍ폜�̂��߂̃t�H�[��
    /// </summary>
    public class FilterSettingForm : System.Windows.Forms.Form
    {
        private TextBox addWordTextBox;
        private Button addWordButton;
        private ListBox filterListBox;
        private ContextMenu filterListBoxContextMenu;
        private MenuItem deleteMenuItem;
        private Button deleteButton;
        private MainMenu mainMenu;
        private ContextMenu addWordContextMenu;
        private MenuItem cutMenuItem;
        private MenuItem copyMenuItem;
        private MenuItem pasteMenuItem;
        private Label addFilterLabel;
        private Label filterListLabel;
        private MenuItem okMenuItem;

        public FilterSettingForm()
        {
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
            this.addWordTextBox = new System.Windows.Forms.TextBox();
            this.addWordContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteMenuItem = new System.Windows.Forms.MenuItem();
            this.addWordButton = new System.Windows.Forms.Button();
            this.filterListBox = new System.Windows.Forms.ListBox();
            this.filterListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.deleteMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteButton = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.addFilterLabel = new System.Windows.Forms.Label();
            this.filterListLabel = new System.Windows.Forms.Label();
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
            this.addWordContextMenu.MenuItems.Add(this.cutMenuItem);
            this.addWordContextMenu.MenuItems.Add(this.copyMenuItem);
            this.addWordContextMenu.MenuItems.Add(this.pasteMenuItem);
            // 
            // cutMenuItem
            // 
            this.cutMenuItem.Text = "�؂���(&T)";
            this.cutMenuItem.Click += new System.EventHandler(this.CutMenuItem_Click);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Text = "�R�s�[(&C)";
            this.copyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Text = "�\��t��(&P)";
            this.pasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
            // 
            // addWordButton
            // 
            this.addWordButton.Location = new System.Drawing.Point(165, 27);
            this.addWordButton.Size = new System.Drawing.Size(72, 20);
            this.addWordButton.Text = "�ǉ�(&A)";
            this.addWordButton.Click += new System.EventHandler(this.AddWordButton_Click);
            // 
            // filterListBox
            // 
            this.filterListBox.ContextMenu = this.filterListBoxContextMenu;
            this.filterListBox.Location = new System.Drawing.Point(3, 71);
            this.filterListBox.Size = new System.Drawing.Size(234, 170);
            // 
            // filterListBoxContextMenu
            // 
            this.filterListBoxContextMenu.MenuItems.Add(this.deleteMenuItem);
            this.filterListBoxContextMenu.Popup += new System.EventHandler(this.FilterListBoxContextMenu_Popup);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Text = "�폜(&D)";
            this.deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(165, 245);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.deleteButton.Text = "�폜(&D)";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
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
            // addFilterLabel
            // 
            this.addFilterLabel.Location = new System.Drawing.Point(3, 4);
            this.addFilterLabel.Size = new System.Drawing.Size(100, 20);
            this.addFilterLabel.Text = "�t�B���^�[�̒ǉ�";
            // 
            // filterListLabel
            // 
            this.filterListLabel.Location = new System.Drawing.Point(3, 51);
            this.filterListLabel.Size = new System.Drawing.Size(100, 20);
            this.filterListLabel.Text = "�t�B���^�[�ꗗ";
            // 
            // FilterSettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.filterListLabel);
            this.Controls.Add(this.addFilterLabel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.filterListBox);
            this.Controls.Add(this.addWordButton);
            this.Controls.Add(this.addWordTextBox);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "�t�B���^�[�̒ǉ��ƍ폜";
            this.Resize += new System.EventHandler(this.FilterSettingForm_Resize);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FilterSettingForm_Closing);
            this.Load += new System.EventHandler(this.FilterSettingForm_Load);

        }
        #endregion

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����
        /// </summary>
        private void FixWindowSize()
        {
            // �������[�h�̏ꍇ
            if (this.Size.Width > this.Size.Height)
            {
                // �����̃E�B���h�E
                FixWindowSizeHorizon();
            }
            else
            {
                // �c���̃E�B���h�E
                FixWindowSizeVertical();
            }
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����i�����j
        /// </summary>
        private void FixWindowSizeVertical()
        {
            this.addWordTextBox.Location = new System.Drawing.Point(3, 27);
            this.addWordTextBox.Size = new System.Drawing.Size(156, 21);
            this.addWordButton.Location = new System.Drawing.Point(165, 27);
            this.addWordButton.Size = new System.Drawing.Size(72, 20);
            this.filterListBox.Location = new System.Drawing.Point(3, 71);
            this.filterListBox.Size = new System.Drawing.Size(234, 170);
            this.deleteButton.Location = new System.Drawing.Point(165, 245);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.addFilterLabel.Location = new System.Drawing.Point(3, 4);
            this.addFilterLabel.Size = new System.Drawing.Size(100, 20);
            this.filterListLabel.Location = new System.Drawing.Point(3, 51);
            this.filterListLabel.Size = new System.Drawing.Size(100, 20);
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����i�����j
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.addWordTextBox.Location = new System.Drawing.Point(3, 27);
            this.addWordTextBox.Size = new System.Drawing.Size(236, 21);
            this.addWordButton.Location = new System.Drawing.Point(245, 27);
            this.addWordButton.Size = new System.Drawing.Size(72, 20);
            this.filterListBox.Location = new System.Drawing.Point(3, 71);
            this.filterListBox.Size = new System.Drawing.Size(314, 86);
            this.deleteButton.Location = new System.Drawing.Point(245, 163);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.addFilterLabel.Location = new System.Drawing.Point(3, 4);
            this.addFilterLabel.Size = new System.Drawing.Size(100, 20);
            this.filterListLabel.Location = new System.Drawing.Point(3, 51);
            this.filterListLabel.Size = new System.Drawing.Size(100, 20);
        }

        private void FilterSettingForm_Load(object sender, System.EventArgs e)
        {
            // �t�H�[�����̒��g�̃T�C�Y��K���ɕύX����
            FixWindowSize();

            // �t�B���^�[���X�g�Ƀt�B���^�̓��e��ǉ�����
            foreach (string word in UserSetting.GetFilterWords())
            {
                filterListBox.Items.Add(word);
            }
        }

        private void FilterSettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

            // �ݒ���t�@�C���ɏ�������
            ArrayList alFilterWord = new ArrayList();
            IEnumerator filterEnumerator = filterListBox.Items.GetEnumerator();
            while (filterEnumerator.MoveNext())
            {
                alFilterWord.Add(((string)filterEnumerator.Current).Trim());
            }
            UserSetting.SetFilterWords((string[])alFilterWord.ToArray(typeof(string)));
            try
            {
                UserSetting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("�ݒ�t�@�C�����������߂܂���ł���", "�ݒ�t�@�C���������݃G���[");
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

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void FilterListBoxContextMenu_Popup(object sender, System.EventArgs e)
        {
            if (filterListBox.SelectedIndex == -1)
            {
                deleteMenuItem.Enabled = false;
            }
            else
            {
                deleteMenuItem.Enabled = true;
            }
        }

        private void FilterSettingForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

        private void CutMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(addWordTextBox);
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(addWordTextBox);
        }

        private void PasteMenuItem_Click(object sender, EventArgs e)
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
            else if (e.KeyCode == Keys.X && e.Control) {
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
