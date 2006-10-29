#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

#endregion

namespace PocketLadio.Stations.Netladio
{
    /// <summary>
    /// チャンネルの詳細情報表示フォーム
    /// </summary>
    public class ChannelPropertyForm : System.Windows.Forms.Form
    {
        private Label namCaptionLabel;
        private Label gnlCaptionLabel;
        private Label urlCaptionLabel;
        private Label timsCaptionLabel;
        private Label clnCaptionLabel;
        private Label bitCaptionLabel;
        private Label namLabel;
        private Label gnlLabel;
        private Label urlLabel;
        private Label timsLabel;
        private Label clnLabel;
        private Label bitLabel;
        private Button accessButton;
        private MenuItem okMenuItem;
        private Button playButton;
        private MainMenu mainMenu;

        /// <summary>
        /// チャンネル
        /// </summary>
        private Channel channel;

        public ChannelPropertyForm(Channel channel)
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            this.channel = channel;
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
            this.namCaptionLabel = new System.Windows.Forms.Label();
            this.namLabel = new System.Windows.Forms.Label();
            this.gnlCaptionLabel = new System.Windows.Forms.Label();
            this.gnlLabel = new System.Windows.Forms.Label();
            this.urlCaptionLabel = new System.Windows.Forms.Label();
            this.urlLabel = new System.Windows.Forms.Label();
            this.timsCaptionLabel = new System.Windows.Forms.Label();
            this.timsLabel = new System.Windows.Forms.Label();
            this.clnCaptionLabel = new System.Windows.Forms.Label();
            this.clnLabel = new System.Windows.Forms.Label();
            this.bitCaptionLabel = new System.Windows.Forms.Label();
            this.bitLabel = new System.Windows.Forms.Label();
            this.accessButton = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.playButton = new System.Windows.Forms.Button();
            // 
            // namCaptionLabel
            // 
            this.namCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.namCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.namCaptionLabel.Text = "番組名";
            // 
            // namLabel
            // 
            this.namLabel.Location = new System.Drawing.Point(3, 19);
            this.namLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // gnlCaptionLabel
            // 
            this.gnlCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.gnlCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.gnlCaptionLabel.Text = "ジャンル";
            // 
            // gnlLabel
            // 
            this.gnlLabel.Location = new System.Drawing.Point(3, 51);
            this.gnlLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // urlCaptionLabel
            // 
            this.urlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.urlCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.urlCaptionLabel.Text = "URL";
            // 
            // urlLabel
            // 
            this.urlLabel.Location = new System.Drawing.Point(3, 109);
            this.urlLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // timsCaptionLabel
            // 
            this.timsCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.timsCaptionLabel.Size = new System.Drawing.Size(88, 16);
            this.timsCaptionLabel.Text = "放送開始時刻";
            // 
            // timsLabel
            // 
            this.timsLabel.Location = new System.Drawing.Point(3, 167);
            this.timsLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // clnCaptionLabel
            // 
            this.clnCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.clnCaptionLabel.Size = new System.Drawing.Size(120, 16);
            this.clnCaptionLabel.Text = "リスナ数（現在/述べ）";
            // 
            // clnLabel
            // 
            this.clnLabel.Location = new System.Drawing.Point(3, 199);
            this.clnLabel.Size = new System.Drawing.Size(120, 16);
            // 
            // bitCaptionLabel
            // 
            this.bitCaptionLabel.Location = new System.Drawing.Point(129, 183);
            this.bitCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.bitCaptionLabel.Text = "ビットレート";
            // 
            // bitLabel
            // 
            this.bitLabel.Location = new System.Drawing.Point(129, 199);
            this.bitLabel.Size = new System.Drawing.Size(108, 16);
            // 
            // accessButton
            // 
            this.accessButton.Location = new System.Drawing.Point(165, 128);
            this.accessButton.Size = new System.Drawing.Size(72, 20);
            this.accessButton.Text = "&Access";
            this.accessButton.Click += new System.EventHandler(this.AccessButton_Click);
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
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(165, 70);
            this.playButton.Size = new System.Drawing.Size(72, 20);
            this.playButton.Text = "&Play";
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // channelPropertyForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.accessButton);
            this.Controls.Add(this.bitLabel);
            this.Controls.Add(this.bitCaptionLabel);
            this.Controls.Add(this.clnLabel);
            this.Controls.Add(this.clnCaptionLabel);
            this.Controls.Add(this.timsLabel);
            this.Controls.Add(this.timsCaptionLabel);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.urlCaptionLabel);
            this.Controls.Add(this.gnlLabel);
            this.Controls.Add(this.gnlCaptionLabel);
            this.Controls.Add(this.namLabel);
            this.Controls.Add(this.namCaptionLabel);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "番組の詳細";
            this.Resize += new System.EventHandler(this.ChannelPropertyForm_Resize);
            this.Load += new System.EventHandler(this.ChannelPropertyForm_Load);

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
            this.namCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.namCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.namLabel.Location = new System.Drawing.Point(3, 19);
            this.namLabel.Size = new System.Drawing.Size(234, 16);
            this.gnlCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.gnlCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.gnlLabel.Location = new System.Drawing.Point(3, 51);
            this.gnlLabel.Size = new System.Drawing.Size(234, 16);
            this.urlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.urlCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.urlLabel.Location = new System.Drawing.Point(3, 109);
            this.urlLabel.Size = new System.Drawing.Size(234, 16);
            this.timsCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.timsCaptionLabel.Size = new System.Drawing.Size(88, 16);
            this.timsLabel.Location = new System.Drawing.Point(3, 167);
            this.timsLabel.Size = new System.Drawing.Size(234, 16);
            this.clnCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.clnCaptionLabel.Size = new System.Drawing.Size(120, 16);
            this.clnLabel.Location = new System.Drawing.Point(3, 199);
            this.clnLabel.Size = new System.Drawing.Size(120, 16);
            this.bitCaptionLabel.Location = new System.Drawing.Point(129, 183);
            this.bitCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.bitLabel.Location = new System.Drawing.Point(129, 199);
            this.bitLabel.Size = new System.Drawing.Size(108, 16);
            this.accessButton.Location = new System.Drawing.Point(165, 128);
            this.accessButton.Size = new System.Drawing.Size(72, 20);
            this.playButton.Location = new System.Drawing.Point(165, 70);
            this.playButton.Size = new System.Drawing.Size(72, 20);
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.namCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.namCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.namLabel.Location = new System.Drawing.Point(3, 19);
            this.namLabel.Size = new System.Drawing.Size(314, 16);
            this.gnlCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.gnlCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.gnlLabel.Location = new System.Drawing.Point(3, 51);
            this.gnlLabel.Size = new System.Drawing.Size(314, 16);
            this.urlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.urlCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.urlLabel.Location = new System.Drawing.Point(3, 109);
            this.urlLabel.Size = new System.Drawing.Size(236, 16);
            this.timsCaptionLabel.Location = new System.Drawing.Point(3, 132);
            this.timsCaptionLabel.Size = new System.Drawing.Size(88, 16);
            this.timsLabel.Location = new System.Drawing.Point(129, 132);
            this.timsLabel.Size = new System.Drawing.Size(188, 16);
            this.clnCaptionLabel.Location = new System.Drawing.Point(3, 148);
            this.clnCaptionLabel.Size = new System.Drawing.Size(120, 16);
            this.clnLabel.Location = new System.Drawing.Point(129, 148);
            this.clnLabel.Size = new System.Drawing.Size(120, 16);
            this.bitCaptionLabel.Location = new System.Drawing.Point(3, 164);
            this.bitCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.bitLabel.Location = new System.Drawing.Point(129, 164);
            this.bitLabel.Size = new System.Drawing.Size(108, 16);
            this.accessButton.Location = new System.Drawing.Point(245, 109);
            this.accessButton.Size = new System.Drawing.Size(72, 20);
            this.playButton.Location = new System.Drawing.Point(245, 70);
            this.playButton.Size = new System.Drawing.Size(72, 20);
        }

        private void ChannelPropertyForm_Load(object sender, System.EventArgs e)
        {
            FixWindowSize();
            namLabel.Text = channel.Nam.Trim();
            gnlLabel.Text = channel.Gnl.Trim();
            urlLabel.Text = ((channel.GetWebsiteUrl() != null) ? channel.GetWebsiteUrl().ToString().Trim() : "");
            timsLabel.Text = channel.Tims.ToString().Trim();
            if (channel.Cln >= 0)
            {
                clnLabel.Text = channel.Cln.ToString();
            }
            if (channel.Clns >= 0)
            {
                clnLabel.Text += " / " + channel.Clns.ToString();
            }
            if (channel.Bit > 0)
            {
                bitLabel.Text = channel.Bit.ToString() + " Kbps";
            }
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                PocketLadioUtility.PlayStreaming(channel.GetPlayUrl());
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("メディアプレイヤーが見つかりません", "警告");
            }
        }

        private void AccessButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (urlLabel.Text.Trim().Length != 0)
                {
                    PocketLadioUtility.AccessWebsite(channel.GetWebsiteUrl());
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("ブラウザが見つかりません", "警告");
            }
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ChannelPropertyForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

    }
}
