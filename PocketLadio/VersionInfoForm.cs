#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

#endregion

namespace PocketLadio
{
    /// <summary>
    /// バージョン情報のフォーム
    /// </summary>
    public class VersionInfoForm : System.Windows.Forms.Form
    {
        private MenuItem OkMenuItem;
        private Label ApplicationNameLabel;
        private Label VersionNumberlabel;
        private Label CopyrightLabel;
        /// <summary>
        /// フォームのメイン メニューです。
        /// </summary>
        private System.Windows.Forms.MainMenu MainMenu;

        public VersionInfoForm()
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
            this.ApplicationNameLabel = new System.Windows.Forms.Label();
            this.VersionNumberlabel = new System.Windows.Forms.Label();
            this.CopyrightLabel = new System.Windows.Forms.Label();
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
            // ApplicationNameLabel
            // 
            this.ApplicationNameLabel.Location = new System.Drawing.Point(3, 9);
            this.ApplicationNameLabel.Size = new System.Drawing.Size(234, 20);
            this.ApplicationNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VersionNumberlabel
            // 
            this.VersionNumberlabel.Location = new System.Drawing.Point(3, 29);
            this.VersionNumberlabel.Size = new System.Drawing.Size(234, 20);
            this.VersionNumberlabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.Location = new System.Drawing.Point(3, 49);
            this.CopyrightLabel.Size = new System.Drawing.Size(234, 20);
            this.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VersionInfoForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.CopyrightLabel);
            this.Controls.Add(this.VersionNumberlabel);
            this.Controls.Add(this.ApplicationNameLabel);
            this.Menu = this.MainMenu;
            this.Text = "バージョン情報";
            this.Load += new System.EventHandler(this.VersionInfoForm_Load);
            this.MaximizeBox = false;

        }

        #endregion

        private void VersionInfoForm_Load(object sender, EventArgs e)
        {
            ApplicationNameLabel.Text = Controller.ApplicationName;
            VersionNumberlabel.Text = "Version " + Controller.VersionNumber;
            CopyrightLabel.Text = Controller.Copyright;
        }

        private void OkMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
