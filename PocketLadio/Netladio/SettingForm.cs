using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PocketLadio.Netladio
{
    /// <summary>
    /// ねとらじの設定フォーム
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage NetladioTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HeadlineCsvUrlTextBox;
        private System.Windows.Forms.TextBox HeadlineXmlUrlTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton HeadlineGetTypeCvsRadioButton;
        private System.Windows.Forms.RadioButton HeadlineGetTypeXmlRadioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuItem OkMenuItem;
        private System.Windows.Forms.MainMenu mainMenu;
        private Label label3;
        private System.Windows.Forms.TextBox HeadlineViewTypeTextBox;

        /// <summary>
        /// 設定
        /// </summary>
        private UserSetting Setting;


        public SettingForm(UserSetting Setting)
        {
            this.Setting = Setting;

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
            this.OkMenuItem = new System.Windows.Forms.MenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NetladioTabPage = new System.Windows.Forms.TabPage();
            this.HeadlineViewTypeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.HeadlineGetTypeXmlRadioButton = new System.Windows.Forms.RadioButton();
            this.HeadlineGetTypeCvsRadioButton = new System.Windows.Forms.RadioButton();
            this.HeadlineXmlUrlTextBox = new System.Windows.Forms.TextBox();
            this.HeadlineCsvUrlTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.OkMenuItem);
            // 
            // OkMenuItem
            // 
            this.OkMenuItem.Text = "&OK";
            this.OkMenuItem.Click += new System.EventHandler(this.OkMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NetladioTabPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 268);
            // 
            // NetladioTabPage
            // 
            this.NetladioTabPage.Controls.Add(this.HeadlineViewTypeTextBox);
            this.NetladioTabPage.Controls.Add(this.label4);
            this.NetladioTabPage.Controls.Add(this.panel1);
            this.NetladioTabPage.Controls.Add(this.HeadlineXmlUrlTextBox);
            this.NetladioTabPage.Controls.Add(this.HeadlineCsvUrlTextBox);
            this.NetladioTabPage.Controls.Add(this.label2);
            this.NetladioTabPage.Controls.Add(this.label1);
            this.NetladioTabPage.Location = new System.Drawing.Point(0, 0);
            this.NetladioTabPage.Size = new System.Drawing.Size(240, 245);
            this.NetladioTabPage.Text = "ねとらじ設定";
            // 
            // HeadlineViewTypeTextBox
            // 
            this.HeadlineViewTypeTextBox.Location = new System.Drawing.Point(3, 162);
            this.HeadlineViewTypeTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 139);
            this.label4.Size = new System.Drawing.Size(135, 20);
            this.label4.Text = "ヘッドラインの表示方法";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.HeadlineGetTypeXmlRadioButton);
            this.panel1.Controls.Add(this.HeadlineGetTypeCvsRadioButton);
            this.panel1.Location = new System.Drawing.Point(0, 91);
            this.panel1.Size = new System.Drawing.Size(240, 45);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Size = new System.Drawing.Size(135, 20);
            this.label3.Text = "ヘッドラインの取得方法";
            // 
            // HeadlineGetTypeXmlRadioButton
            // 
            this.HeadlineGetTypeXmlRadioButton.Enabled = false;
            this.HeadlineGetTypeXmlRadioButton.Location = new System.Drawing.Point(57, 23);
            this.HeadlineGetTypeXmlRadioButton.Size = new System.Drawing.Size(48, 20);
            this.HeadlineGetTypeXmlRadioButton.Text = "XML";
            // 
            // HeadlineGetTypeCvsRadioButton
            // 
            this.HeadlineGetTypeCvsRadioButton.Checked = true;
            this.HeadlineGetTypeCvsRadioButton.Location = new System.Drawing.Point(3, 22);
            this.HeadlineGetTypeCvsRadioButton.Size = new System.Drawing.Size(48, 20);
            this.HeadlineGetTypeCvsRadioButton.Text = "CVS";
            // 
            // HeadlineXmlUrlTextBox
            // 
            this.HeadlineXmlUrlTextBox.Location = new System.Drawing.Point(3, 64);
            this.HeadlineXmlUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // HeadlineCsvUrlTextBox
            // 
            this.HeadlineCsvUrlTextBox.Location = new System.Drawing.Point(3, 23);
            this.HeadlineCsvUrlTextBox.Size = new System.Drawing.Size(234, 21);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.Text = "ヘッドラインのURL XML";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.Text = "ヘッドラインのURL CSV";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu;
            this.Text = "設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // 設定の読み込み
            HeadlineCsvUrlTextBox.Text = Setting.HeadlineCsvUrl;
            HeadlineXmlUrlTextBox.Text = Setting.HeadlineXmlUrl;
            if (Setting.HeadlineGetType == UserSetting.HeadlineGetTypeEnum.Cvs)
            {
                HeadlineGetTypeCvsRadioButton.Checked = true;
                HeadlineGetTypeXmlRadioButton.Checked = false;
            }
            else if (Setting.HeadlineGetType == UserSetting.HeadlineGetTypeEnum.Xml)
            {
                HeadlineGetTypeCvsRadioButton.Checked = false;
                HeadlineGetTypeXmlRadioButton.Checked = true;
            }
            HeadlineViewTypeTextBox.Text = Setting.HeadlineViewType;
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
            Setting.HeadlineCsvUrl = HeadlineCsvUrlTextBox.Text.Trim();
            Setting.HeadlineXmlUrl = HeadlineXmlUrlTextBox.Text.Trim();
            if (HeadlineGetTypeCvsRadioButton.Checked)
            {
                Setting.HeadlineGetType = UserSetting.HeadlineGetTypeEnum.Cvs;
            }
            else if (HeadlineGetTypeXmlRadioButton.Checked)
            {
                Setting.HeadlineGetType = UserSetting.HeadlineGetTypeEnum.Xml;
            }
            Setting.HeadlineViewType = HeadlineViewTypeTextBox.Text.Trim();
            Setting.SaveSetting();
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
