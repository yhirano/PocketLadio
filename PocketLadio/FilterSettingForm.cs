using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PocketLadio.Util;

namespace PocketLadio
{
    /// <summary>
    /// フィルターの追加と削除のためのフォーム
    /// </summary>
    public class FilterSettingForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox AddWordTextBox;
        private System.Windows.Forms.Button AddWordButton;
        private System.Windows.Forms.ListBox FilterListBox;
        private System.Windows.Forms.ContextMenu FilterListBoxContextMenu;
        private System.Windows.Forms.MenuItem DeleteMenuItem;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.MainMenu MainMenu;
        private ContextMenu AddWordContextMenu;
        private MenuItem CutMenuItem;
        private MenuItem CopyMenuItem;
        private MenuItem PasteMenuItem;
        private System.Windows.Forms.MenuItem OkMenuItem;

        public FilterSettingForm()
        {
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
            this.AddWordTextBox = new System.Windows.Forms.TextBox();
            this.AddWordButton = new System.Windows.Forms.Button();
            this.FilterListBox = new System.Windows.Forms.ListBox();
            this.FilterListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.DeleteMenuItem = new System.Windows.Forms.MenuItem();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.OkMenuItem = new System.Windows.Forms.MenuItem();
            this.AddWordContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteMenuItem = new System.Windows.Forms.MenuItem();
            // 
            // AddWordTextBox
            // 
            this.AddWordTextBox.ContextMenu = this.AddWordContextMenu;
            this.AddWordTextBox.Location = new System.Drawing.Point(3, 3);
            this.AddWordTextBox.Size = new System.Drawing.Size(156, 21);
            // 
            // AddWordButton
            // 
            this.AddWordButton.Location = new System.Drawing.Point(165, 3);
            this.AddWordButton.Size = new System.Drawing.Size(72, 20);
            this.AddWordButton.Text = "追加(&A)";
            this.AddWordButton.Click += new System.EventHandler(this.AddWordButton_Click);
            // 
            // FilterListBox
            // 
            this.FilterListBox.ContextMenu = this.FilterListBoxContextMenu;
            this.FilterListBox.Location = new System.Drawing.Point(3, 29);
            this.FilterListBox.Size = new System.Drawing.Size(234, 212);
            // 
            // FilterListBoxContextMenu
            // 
            this.FilterListBoxContextMenu.MenuItems.Add(this.DeleteMenuItem);
            this.FilterListBoxContextMenu.Popup += new System.EventHandler(this.FilterListBoxContextMenu_Popup);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Text = "削除(&D)";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(165, 245);
            this.DeleteButton.Size = new System.Drawing.Size(72, 20);
            this.DeleteButton.Text = "削除(&D)";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
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
            // AddWordContextMenu
            // 
            this.AddWordContextMenu.MenuItems.Add(this.CutMenuItem);
            this.AddWordContextMenu.MenuItems.Add(this.CopyMenuItem);
            this.AddWordContextMenu.MenuItems.Add(this.PasteMenuItem);
            // 
            // CutMenuItem
            // 
            this.CutMenuItem.Text = "切り取り(&T)";
            this.CutMenuItem.Click += new System.EventHandler(this.CutMenuItem_Click);
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Text = "コピー(&C)";
            this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // PasteMenuItem
            // 
            this.PasteMenuItem.Text = "貼り付け(&P)";
            this.PasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
            // 
            // FilterSettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.FilterListBox);
            this.Controls.Add(this.AddWordButton);
            this.Controls.Add(this.AddWordTextBox);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Text = "フィルター設定";
            this.Resize += new System.EventHandler(this.FilterSettingForm_Resize);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FilterSettingForm_Closing);
            this.Load += new System.EventHandler(this.FilterSettingForm_Load);

        }
        #endregion

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する
        /// </summary>
        private void FixWindowSize()
        {
            // 水平モードの場合
            if (this.Size.Width > this.Size.Height)
            {
                // 横長のウィンドウ
                FixWindowSizeHorizon();
            }
            else
            {
                // 縦長のウィンドウ
                FixWindowSizeVertical();
            }
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（垂直）
        /// </summary>
        private void FixWindowSizeVertical()
        {
            this.AddWordButton.Location = new System.Drawing.Point(165, 3);
            this.DeleteButton.Location = new System.Drawing.Point(165, 245);
            this.AddWordTextBox.Location = new System.Drawing.Point(3, 3);
            this.AddWordTextBox.Size = new System.Drawing.Size(156, 21);
            this.FilterListBox.Location = new System.Drawing.Point(3, 29);
            this.FilterListBox.Size = new System.Drawing.Size(234, 212);
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.AddWordButton.Location = new System.Drawing.Point(245, 3);
            this.DeleteButton.Location = new System.Drawing.Point(245, 165);
            this.AddWordTextBox.Location = new System.Drawing.Point(3, 3);
            this.AddWordTextBox.Size = new System.Drawing.Size(236, 21);
            this.FilterListBox.Location = new System.Drawing.Point(3, 29);
            this.FilterListBox.Size = new System.Drawing.Size(314, 128);
        }

        private void FilterSettingForm_Load(object sender, System.EventArgs e)
        {
            FixWindowSize();
            foreach (string Word in UserSetting.FilterWords)
            {
                FilterListBox.Items.Add(Word);
            }
        }

        private void FilterSettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ArrayList AlfilterWord = new ArrayList();
            IEnumerator FilterEnum = FilterListBox.Items.GetEnumerator();
            while (FilterEnum.MoveNext())
            {
                AlfilterWord.Add(((string)FilterEnum.Current).Trim());
            }
            UserSetting.FilterWords = (string[])AlfilterWord.ToArray(typeof(string));
            UserSetting.SaveSetting();
        }

        private void AddWordButton_Click(object sender, System.EventArgs e)
        {
            if (!AddWordTextBox.Text.Trim().Equals(""))
            {
                FilterListBox.Items.Add(AddWordTextBox.Text.Trim());
                AddWordTextBox.Text = "";
            }
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            if (FilterListBox.SelectedIndex != -1)
            {
                FilterListBox.Items.RemoveAt(FilterListBox.SelectedIndex);
            }
        }

        private void DeleteMenuItem_Click(object sender, System.EventArgs e)
        {
            if (FilterListBox.SelectedIndex != -1)
            {
                FilterListBox.Items.RemoveAt(FilterListBox.SelectedIndex);
            }
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void FilterListBoxContextMenu_Popup(object sender, System.EventArgs e)
        {
            if (FilterListBox.SelectedIndex == -1)
            {
                DeleteMenuItem.Enabled = false;
            }
            else
            {
                DeleteMenuItem.Enabled = true;
            }
        }

        private void FilterSettingForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

        private void CutMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(AddWordTextBox);
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(AddWordTextBox);
        }

        private void PasteMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(AddWordTextBox);
        }
    }
}
