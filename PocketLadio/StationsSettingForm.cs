using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using PocketLadio.Util;

namespace PocketLadio
{
    /// <summary>
    /// StationsSettingForm の概要の説明です。
    /// </summary>
    public class StationsSettingForm : System.Windows.Forms.Form
    {
        private MenuItem OkMenuItem;
        private Label StationListLabel;
        private Label AddStationLabel;
        private ListBox StationListBox;
        private Button DeleteButton;
        private Button AddButton;
        private ComboBox StationKindComboBox;
        private TextBox StationNameTextBox;
        private ContextMenu StationListBoxContextMenu;
        private MenuItem SettingMenuItem;
        private MenuItem DeleteMenuItem;
        private ContextMenu StationNameContextMenu;
        private MenuItem CutStationNameMenuItem;
        private MenuItem CopyStationNameMenuItem;
        private MenuItem PasteStationNameMenuItem;

        /// <summary>
        /// 放送局のリスト
        /// </summary>
        private ArrayList AlStationList = new ArrayList();

        /// <summary>
        /// フォームのメイン メニューです。
        /// </summary>
        private System.Windows.Forms.MainMenu MainMenu;

        public StationsSettingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
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
            this.StationListLabel = new System.Windows.Forms.Label();
            this.AddStationLabel = new System.Windows.Forms.Label();
            this.StationListBox = new System.Windows.Forms.ListBox();
            this.StationListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.SettingMenuItem = new System.Windows.Forms.MenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.MenuItem();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.StationKindComboBox = new System.Windows.Forms.ComboBox();
            this.StationNameTextBox = new System.Windows.Forms.TextBox();
            this.StationNameContextMenu = new System.Windows.Forms.ContextMenu();
            this.CutStationNameMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyStationNameMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteStationNameMenuItem = new System.Windows.Forms.MenuItem();
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
            // StationListLabel
            // 
            this.StationListLabel.Location = new System.Drawing.Point(3, 77);
            this.StationListLabel.Size = new System.Drawing.Size(84, 20);
            this.StationListLabel.Text = "放送局一覧";
            // 
            // AddStationLabel
            // 
            this.AddStationLabel.Location = new System.Drawing.Point(3, 4);
            this.AddStationLabel.Size = new System.Drawing.Size(84, 20);
            this.AddStationLabel.Text = "放送局の追加";
            // 
            // StationListBox
            // 
            this.StationListBox.ContextMenu = this.StationListBoxContextMenu;
            this.StationListBox.Location = new System.Drawing.Point(3, 99);
            this.StationListBox.Size = new System.Drawing.Size(234, 142);
            // 
            // StationListBoxContextMenu
            // 
            this.StationListBoxContextMenu.MenuItems.Add(this.SettingMenuItem);
            this.StationListBoxContextMenu.MenuItems.Add(this.DeleteMenuItem);
            // 
            // SettingMenuItem
            // 
            this.SettingMenuItem.Text = "設定(&S)";
            this.SettingMenuItem.Click += new System.EventHandler(this.SettingMenuItem_Click);
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
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(165, 54);
            this.AddButton.Size = new System.Drawing.Size(72, 20);
            this.AddButton.Text = "追加(&A)";
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // StationKindComboBox
            // 
            this.StationKindComboBox.Items.Add("ねとらじ");
            this.StationKindComboBox.Items.Add("Podcast");
            this.StationKindComboBox.Location = new System.Drawing.Point(137, 27);
            this.StationKindComboBox.Size = new System.Drawing.Size(100, 22);
            // 
            // StationNameTextBox
            // 
            this.StationNameTextBox.ContextMenu = this.StationNameContextMenu;
            this.StationNameTextBox.Location = new System.Drawing.Point(3, 27);
            this.StationNameTextBox.Size = new System.Drawing.Size(128, 21);
            // 
            // StationNameContextMenu
            // 
            this.StationNameContextMenu.MenuItems.Add(this.CutStationNameMenuItem);
            this.StationNameContextMenu.MenuItems.Add(this.CopyStationNameMenuItem);
            this.StationNameContextMenu.MenuItems.Add(this.PasteStationNameMenuItem);
            // 
            // CutStationNameMenuItem
            // 
            this.CutStationNameMenuItem.Text = "切り取り(&T)";
            this.CutStationNameMenuItem.Click += new System.EventHandler(this.CutStationNameMenuItem_Click);
            // 
            // CopyStationNameMenuItem
            // 
            this.CopyStationNameMenuItem.Text = "コピー(&C)";
            this.CopyStationNameMenuItem.Click += new System.EventHandler(this.CopyStationNameMenuItem_Click);
            // 
            // PasteStationNameMenuItem
            // 
            this.PasteStationNameMenuItem.Text = "貼り付け(&P)";
            this.PasteStationNameMenuItem.Click += new System.EventHandler(this.PasteStationNameMenuItem_Click);
            // 
            // StationsSettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.StationListLabel);
            this.Controls.Add(this.AddStationLabel);
            this.Controls.Add(this.StationListBox);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.StationKindComboBox);
            this.Controls.Add(this.StationNameTextBox);
            this.Menu = this.MainMenu;
            this.Text = "放送局の追加と削除";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.StationsSettingForm_Closing);
            this.Load += new System.EventHandler(this.StationsSettingForm_Load);

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
            this.StationListLabel.Location = new System.Drawing.Point(3, 77);
            this.StationListLabel.Size = new System.Drawing.Size(84, 20);
            this.AddStationLabel.Location = new System.Drawing.Point(3, 4);
            this.AddStationLabel.Size = new System.Drawing.Size(84, 20);
            this.StationListBox.ContextMenu = this.StationListBoxContextMenu;
            this.StationListBox.Location = new System.Drawing.Point(3, 99);
            this.StationListBox.Size = new System.Drawing.Size(234, 142);
            this.DeleteButton.Location = new System.Drawing.Point(165, 245);
            this.DeleteButton.Size = new System.Drawing.Size(72, 20);
            this.AddButton.Location = new System.Drawing.Point(165, 54);
            this.AddButton.Size = new System.Drawing.Size(72, 20);
            this.StationKindComboBox.Location = new System.Drawing.Point(137, 27);
            this.StationKindComboBox.Size = new System.Drawing.Size(100, 22);
            this.StationNameTextBox.Location = new System.Drawing.Point(3, 27);
            this.StationNameTextBox.Size = new System.Drawing.Size(128, 21);
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.StationListLabel.Location = new System.Drawing.Point(3, 77);
            this.StationListLabel.Size = new System.Drawing.Size(84, 20);
            this.AddStationLabel.Location = new System.Drawing.Point(3, 4);
            this.AddStationLabel.Size = new System.Drawing.Size(84, 20);
            this.StationListBox.Location = new System.Drawing.Point(3, 99);
            this.StationListBox.Size = new System.Drawing.Size(314, 58);
            this.DeleteButton.Location = new System.Drawing.Point(245, 165);
            this.DeleteButton.Size = new System.Drawing.Size(72, 20);
            this.AddButton.Location = new System.Drawing.Point(245, 55);
            this.AddButton.Size = new System.Drawing.Size(72, 20);
            this.StationKindComboBox.Location = new System.Drawing.Point(217, 27);
            this.StationKindComboBox.Size = new System.Drawing.Size(100, 22);
            this.StationNameTextBox.Location = new System.Drawing.Point(3, 27);
            this.StationNameTextBox.Size = new System.Drawing.Size(208, 21);
        }

        private void StationsSettingForm_Load(object sender, EventArgs e)
        {
            // フォーム内の中身のサイズを適正に変更する
            FixWindowSize();

            // 放送局情報の読み込み
            foreach (Station Station in StationList.GetStationList())
            {
                AlStationList.Add(Station);
                StationListBox.Items.Add(Station.GetDisplayName());
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (StationListBox.SelectedIndex != -1)
            {
                // 設定ファイルの削除
                ((Station)AlStationList[StationListBox.SelectedIndex]).GetHeadline().DeleteUserSettingFile();

                AlStationList.RemoveAt(StationListBox.SelectedIndex);
                StationListBox.Items.RemoveAt(StationListBox.SelectedIndex);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (StationNameTextBox.Text.Trim() != "" && StationKindComboBox.Text.Trim() != "")
            {
                if (StationKindComboBox.Text.Trim().Equals("ねとらじ"))
                {
                    Station Station = new Station(DateTime.Now.ToString("yyyyMMddHHmmssff"), StationNameTextBox.Text.Trim(), Station.StationKindEnum.Netladio);
                    AlStationList.Add(Station);
                    StationListBox.Items.Add(Station.GetDisplayName());
                    StationNameTextBox.Text = "";
                }
                else if (StationKindComboBox.Text.Trim().Equals("Podcast"))
                {
                    Station Station = new Station(DateTime.Now.ToString("yyyyMMddHHmmssff"), StationNameTextBox.Text.Trim(), Station.StationKindEnum.RssPodcast);
                    AlStationList.Add(Station);
                    StationListBox.Items.Add(Station.GetDisplayName());
                    StationNameTextBox.Text = "";

                    // 設定画面を呼び出す
                    Station.GetHeadline().ShowSettingForm();
                }
            }
        }

        private void SettingMenuItem_Click(object sender, EventArgs e)
        {
            if (StationListBox.SelectedIndex != -1)
            {
                ((Station)AlStationList[StationListBox.SelectedIndex]).GetHeadline().ShowSettingForm();
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (StationListBox.SelectedIndex != -1)
            {
                // 設定ファイルの削除
                ((Station)AlStationList[StationListBox.SelectedIndex]).GetHeadline().DeleteUserSettingFile();

                AlStationList.RemoveAt(StationListBox.SelectedIndex);
                StationListBox.Items.RemoveAt(StationListBox.SelectedIndex);
            }
        }

        private void CutStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(StationNameTextBox);
        }

        private void CopyStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(StationNameTextBox);
        }

        private void PasteStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(StationNameTextBox);
        }

        private void OkMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StationsSettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
            StationList.SetStationList((Station[])AlStationList.ToArray(typeof(Station)));
            UserSetting.SaveSetting();
        }
    }
}
