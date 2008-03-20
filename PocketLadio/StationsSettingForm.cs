#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// StationsSettingForm の概要の説明です。
    /// </summary>
    public class StationsSettingForm : System.Windows.Forms.Form
    {
        private MenuItem okMenuItem;
        private Label stationListLabel;
        private Label addStationLabel;
        private ListBox stationListBox;
        private Button deleteButton;
        private Button addButton;
        private ComboBox stationKindComboBox;
        private TextBox stationNameTextBox;
        private ContextMenu stationListBoxContextMenu;
        private MenuItem settingMenuItem;
        private MenuItem deleteMenuItem;
        private ContextMenu stationNameContextMenu;
        private MenuItem cutStationNameMenuItem;
        private MenuItem copyStationNameMenuItem;
        private MenuItem pasteStationNameMenuItem;

        /// <summary>
        /// 放送局のリスト
        /// </summary>
        private ArrayList alStationList = new ArrayList();

        /// <summary>
        /// フォームのメインメニュー
        /// </summary>
        private System.Windows.Forms.MainMenu mainMenu;
        private Button upButton;
        private Button downButton;
        private Panel updownButtonPanel;

        /// <summary>
        /// アンカーコントロールのリスト
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.stationListLabel = new System.Windows.Forms.Label();
            this.addStationLabel = new System.Windows.Forms.Label();
            this.stationListBox = new System.Windows.Forms.ListBox();
            this.stationListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.settingMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.stationKindComboBox = new System.Windows.Forms.ComboBox();
            this.stationNameTextBox = new System.Windows.Forms.TextBox();
            this.stationNameContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutStationNameMenuItem = new System.Windows.Forms.MenuItem();
            this.copyStationNameMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteStationNameMenuItem = new System.Windows.Forms.MenuItem();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.updownButtonPanel = new System.Windows.Forms.Panel();
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
            // stationListLabel
            // 
            this.stationListLabel.Location = new System.Drawing.Point(3, 77);
            this.stationListLabel.Size = new System.Drawing.Size(84, 20);
            this.stationListLabel.Text = "放送局一覧";
            // 
            // addStationLabel
            // 
            this.addStationLabel.Location = new System.Drawing.Point(3, 4);
            this.addStationLabel.Size = new System.Drawing.Size(84, 20);
            this.addStationLabel.Text = "放送局の追加";
            // 
            // stationListBox
            // 
            this.stationListBox.ContextMenu = this.stationListBoxContextMenu;
            this.stationListBox.Location = new System.Drawing.Point(3, 99);
            this.stationListBox.Size = new System.Drawing.Size(210, 142);
            this.stationListBox.SelectedIndexChanged += new System.EventHandler(this.stationListBox_SelectedIndexChanged);
            // 
            // stationListBoxContextMenu
            // 
            this.stationListBoxContextMenu.MenuItems.Add(this.settingMenuItem);
            this.stationListBoxContextMenu.MenuItems.Add(this.deleteMenuItem);
            // 
            // settingMenuItem
            // 
            this.settingMenuItem.Text = "設定(&S)";
            this.settingMenuItem.Click += new System.EventHandler(this.SettingMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Text = "削除(&D)";
            this.deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(165, 245);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.deleteButton.Text = "削除(&D)";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(165, 54);
            this.addButton.Size = new System.Drawing.Size(72, 20);
            this.addButton.Text = "追加(&A)";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // stationKindComboBox
            // 
            this.stationKindComboBox.Items.Add("ねとらじ");
            this.stationKindComboBox.Items.Add("Podcast");
            this.stationKindComboBox.Items.Add("SHOUTcast");
            this.stationKindComboBox.Items.Add("Icecast");
            this.stationKindComboBox.Location = new System.Drawing.Point(137, 27);
            this.stationKindComboBox.Size = new System.Drawing.Size(100, 22);
            // 
            // stationNameTextBox
            // 
            this.stationNameTextBox.ContextMenu = this.stationNameContextMenu;
            this.stationNameTextBox.Location = new System.Drawing.Point(3, 27);
            this.stationNameTextBox.Size = new System.Drawing.Size(128, 21);
            this.stationNameTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StationNameTextBox_KeyUp);
            this.stationNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StationNameTextBox_KeyPress);
            this.stationNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StationNameTextBox_KeyDown);
            // 
            // stationNameContextMenu
            // 
            this.stationNameContextMenu.MenuItems.Add(this.cutStationNameMenuItem);
            this.stationNameContextMenu.MenuItems.Add(this.copyStationNameMenuItem);
            this.stationNameContextMenu.MenuItems.Add(this.pasteStationNameMenuItem);
            // 
            // cutStationNameMenuItem
            // 
            this.cutStationNameMenuItem.Text = "切り取り(&T)";
            this.cutStationNameMenuItem.Click += new System.EventHandler(this.CutStationNameMenuItem_Click);
            // 
            // copyStationNameMenuItem
            // 
            this.copyStationNameMenuItem.Text = "コピー(&C)";
            this.copyStationNameMenuItem.Click += new System.EventHandler(this.CopyStationNameMenuItem_Click);
            // 
            // pasteStationNameMenuItem
            // 
            this.pasteStationNameMenuItem.Text = "貼り付け(&P)";
            this.pasteStationNameMenuItem.Click += new System.EventHandler(this.PasteStationNameMenuItem_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(3, 13);
            this.upButton.Size = new System.Drawing.Size(20, 40);
            this.upButton.Text = "↑";
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(3, 88);
            this.downButton.Size = new System.Drawing.Size(20, 40);
            this.downButton.Text = "↓";
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // updownButtonPanel
            // 
            this.updownButtonPanel.Controls.Add(this.upButton);
            this.updownButtonPanel.Controls.Add(this.downButton);
            this.updownButtonPanel.Location = new System.Drawing.Point(214, 100);
            this.updownButtonPanel.Size = new System.Drawing.Size(26, 141);
            // 
            // StationsSettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.updownButtonPanel);
            this.Controls.Add(this.stationListLabel);
            this.Controls.Add(this.addStationLabel);
            this.Controls.Add(this.stationListBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.stationKindComboBox);
            this.Controls.Add(this.stationNameTextBox);
            this.Menu = this.mainMenu;
            this.Text = "放送局の追加と削除";
            this.Resize += new System.EventHandler(this.StationsSettingForm_Resize);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.StationsSettingForm_Closing);
            this.Load += new System.EventHandler(this.StationsSettingForm_Load);

        }

        #endregion

        /// <summary>
        /// コントロールにアンカーをセットする
        /// </summary>
        private void SetAnchorControl()
        {
            anchorControlList.Add(new AnchorLayout(addStationLabel, AnchorStyles.Top | AnchorStyles.Left, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(stationNameTextBox, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(stationKindComboBox, AnchorStyles.Top | AnchorStyles.Right, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(addButton, AnchorStyles.Top | AnchorStyles.Right, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(stationListLabel, AnchorStyles.Top | AnchorStyles.Left, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(stationListBox, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(updownButtonPanel, AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(upButton, AnchorStyles.Top | AnchorStyles.Left, updownButtonPanel.Width, updownButtonPanel.Height));
            anchorControlList.Add(new AnchorLayout(downButton, AnchorStyles.Bottom | AnchorStyles.Left, updownButtonPanel.Width, updownButtonPanel.Height));
            anchorControlList.Add(new AnchorLayout(deleteButton, AnchorStyles.Right | AnchorStyles.Bottom, PocketLadioInfo.FormBaseWidth, PocketLadioInfo.FormBaseHight));
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する
        /// </summary>
        private void FixWindowSize()
        {
            foreach (AnchorLayout anchorLayout in anchorControlList)
            {
                anchorLayout.LayoutControl();
            }
        }

        /// <summary>
        /// 放送局を作成する。
        /// </summary>
        /// <param name="stationKind">放送局の種類</param>
        private void CreateStation(string stationKind)
        {
            Station station = null;
            switch (stationKind)
            {
                case "ねとらじ":
                    station = StationList.CreateStation(stationNameTextBox.Text.Trim(), StationKinds.Netladio);
                    break;
                case "Podcast":
                    station = StationList.CreateStation(stationNameTextBox.Text.Trim(), StationKinds.RssPodcast);

                    // 設定画面を呼び出す
                    station.Headline.ShowSettingForm();
                    break;
                case "SHOUTcast":
                    station = StationList.CreateStation(stationNameTextBox.Text.Trim(), StationKinds.ShoutCast);

                    // 設定画面を呼び出す
                    station.Headline.ShowSettingForm();
                    break;
                case "Icecast":
                    station = StationList.CreateStation(stationNameTextBox.Text.Trim(), StationKinds.Icecast);

                    // 設定画面を呼び出す
                    station.Headline.ShowSettingForm();
                    break;
                default:
                    // ここに到達することはあり得ない
                    Trace.Assert(false, "想定外の動作のため、終了します");
                    break;
            }

            // リストに追加
            alStationList.Add(station);
            stationListBox.Items.Add(station.DisplayName);
            stationNameTextBox.Text = string.Empty;
        }

        /// <summary>
        ///  スワップする
        /// </summary>
        /// <param name="list">リスト</param>
        /// <param name="x">スワップ位置1</param>
        /// <param name="y">スワップ位置2</param>
        private static void Swap(IList list, int x, int y)
        {
            object tmp;

            tmp = list[x];
            list[x] = list[y];
            list[y] = tmp;
        }

        /// <summary>
        /// UPボタン、DOWNボタン、DELETEボタンの有効無効を切り替える
        /// </summary>
        private void ButtonsEnable()
        {
            if (stationListBox.SelectedIndex == -1)
            {
                upButton.Enabled = false;
                downButton.Enabled = false;
                deleteButton.Enabled = false;
            }
            else
            {
                if (stationListBox.SelectedIndex != 0)
                {
                    upButton.Enabled = true;
                }
                else
                {
                    upButton.Enabled = false;
                }
                if (stationListBox.SelectedIndex < stationListBox.Items.Count - 1)
                {
                    downButton.Enabled = true;
                }
                else
                {
                    downButton.Enabled = false;
                }
                deleteButton.Enabled = true;
            }
        }

        private void StationsSettingForm_Load(object sender, EventArgs e)
        {
            SetAnchorControl();
            FixWindowSize();
            ButtonsEnable();

            // 放送局情報の読み込み
            foreach (Station station in StationList.GetStationList())
            {
                alStationList.Add(station);
                stationListBox.Items.Add(station.DisplayName);
            }
        }

        private void stationListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonsEnable();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (stationListBox.SelectedIndex != -1)
            {
                // 設定ファイルの削除
                ((Station)alStationList[stationListBox.SelectedIndex]).Headline.DeleteUserSettingFile();

                alStationList.RemoveAt(stationListBox.SelectedIndex);
                stationListBox.Items.RemoveAt(stationListBox.SelectedIndex);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (stationNameTextBox.Text.Trim().Length != 0 && stationKindComboBox.Text.Trim().Length != 0)
            {
                CreateStation(stationKindComboBox.Text.Trim());
            }
        }

        private void SettingMenuItem_Click(object sender, EventArgs e)
        {
            if (stationListBox.SelectedIndex != -1)
            {
                ((Station)alStationList[stationListBox.SelectedIndex]).Headline.ShowSettingForm();
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (stationListBox.SelectedIndex > 0)
            {
                int selectIndex = stationListBox.SelectedIndex;
                Swap(alStationList, selectIndex, selectIndex - 1);
                stationListBox.Items.RemoveAt(selectIndex);
                stationListBox.Items.Insert(selectIndex - 1, ((Station)alStationList[selectIndex - 1]).DisplayName);
                stationListBox.SelectedIndex = selectIndex - 1;
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (stationListBox.SelectedIndex != -1 && stationListBox.SelectedIndex < stationListBox.Items.Count - 1)
            {
                int selectIndex = stationListBox.SelectedIndex;
                Swap(alStationList, selectIndex, selectIndex + 1);
                stationListBox.Items.RemoveAt(selectIndex);
                stationListBox.Items.Insert(selectIndex + 1, ((Station)alStationList[selectIndex + 1]).DisplayName);
                stationListBox.SelectedIndex = selectIndex + 1;
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (stationListBox.SelectedIndex != -1)
            {
                // 設定ファイルの削除
                ((Station)alStationList[stationListBox.SelectedIndex]).Headline.DeleteUserSettingFile();

                alStationList.RemoveAt(stationListBox.SelectedIndex);
                stationListBox.Items.RemoveAt(stationListBox.SelectedIndex);
            }
        }

        private void CutStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(stationNameTextBox);
        }

        private void CopyStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(stationNameTextBox);
        }

        private void PasteStationNameMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(stationNameTextBox);
        }

        private void OkMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StationsSettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 放送局を追加し忘れていると思われる場合
            if (stationNameTextBox.Text.Trim().Length != 0 && stationKindComboBox.Text.Trim().Length != 0)
            {
                // 追加するかを聞く
                DialogResult result = MessageBox.Show(
                    stationNameTextBox.Text.Trim() + "を追加しますか？\n（" + stationNameTextBox.Text.Trim() + "はまだ追加されていません）",
                    "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    CreateStation(stationKindComboBox.Text.Trim());
                }
            }

            // 設定の書き込み
            StationList.SetStationList((Station[])alStationList.ToArray(typeof(Station)));
            try
            {
                UserSetting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが書き込めませんでした", "設定ファイル書き込みエラー");
            }

        }

        private void StationsSettingForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

        private void StationNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力ボタンを押したとき
            if (e.KeyCode == Keys.Enter)
            {
                AddButton_Click(sender, e);
            }
            // 切り取りショートカット
            else if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(stationNameTextBox);
            }
            // 貼り付けショートカット
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(stationNameTextBox);
            }
        }

        private void StationNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 入力ボタンを押したときの音を消すため
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void StationNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // コピーショートカット
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(stationNameTextBox);
            }
        }
    }
}
