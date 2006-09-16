#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using PocketLadio.Utility;

#endregion

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// チャンネルの詳細情報表示フォーム
    /// </summary>
    public class ChannelPropertyForm : System.Windows.Forms.Form
    {
        private Label titleCaptionLabel;
        private Label categoryCaptionLabel;
        private Label clusterUrlCaptionLabel;
        private Label playingCaptionLabel;
        private Label listenerCaptionLabel;
        private Label bitRateCaptionLabel;
        private Label titleLabel;
        private Label categoryLabel;
        private Label clusterUrlLabel;
        private Label playingLabel;
        private Label listenerLabel;
        private Label bitRateLabel;
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
            this.titleCaptionLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.categoryCaptionLabel = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.clusterUrlCaptionLabel = new System.Windows.Forms.Label();
            this.clusterUrlLabel = new System.Windows.Forms.Label();
            this.playingCaptionLabel = new System.Windows.Forms.Label();
            this.playingLabel = new System.Windows.Forms.Label();
            this.listenerCaptionLabel = new System.Windows.Forms.Label();
            this.listenerLabel = new System.Windows.Forms.Label();
            this.bitRateCaptionLabel = new System.Windows.Forms.Label();
            this.bitRateLabel = new System.Windows.Forms.Label();
            this.accessButton = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.playButton = new System.Windows.Forms.Button();
            // 
            // titleCaptionLabel
            // 
            this.titleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.titleCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.titleCaptionLabel.Text = "Title";
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(3, 19);
            this.titleLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // categoryCaptionLabel
            // 
            this.categoryCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.categoryCaptionLabel.Size = new System.Drawing.Size(62, 16);
            this.categoryCaptionLabel.Text = "Category";
            // 
            // categoryLabel
            // 
            this.categoryLabel.Location = new System.Drawing.Point(3, 51);
            this.categoryLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // clusterUrlCaptionLabel
            // 
            this.clusterUrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.clusterUrlCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.clusterUrlCaptionLabel.Text = "Cluster URL";
            // 
            // clusterUrlLabel
            // 
            this.clusterUrlLabel.Location = new System.Drawing.Point(3, 109);
            this.clusterUrlLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // playingCaptionLabel
            // 
            this.playingCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.playingCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.playingCaptionLabel.Text = "Playing Now";
            // 
            // playingLabel
            // 
            this.playingLabel.Location = new System.Drawing.Point(3, 167);
            this.playingLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // listenerCaptionLabel
            // 
            this.listenerCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.listenerCaptionLabel.Size = new System.Drawing.Size(120, 16);
            this.listenerCaptionLabel.Text = "Listener";
            // 
            // listenerLabel
            // 
            this.listenerLabel.Location = new System.Drawing.Point(3, 199);
            this.listenerLabel.Size = new System.Drawing.Size(120, 16);
            // 
            // bitRateCaptionLabel
            // 
            this.bitRateCaptionLabel.Location = new System.Drawing.Point(129, 183);
            this.bitRateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.bitRateCaptionLabel.Text = "Bit rate";
            // 
            // bitRateLabel
            // 
            this.bitRateLabel.Location = new System.Drawing.Point(129, 199);
            this.bitRateLabel.Size = new System.Drawing.Size(108, 16);
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
            this.Controls.Add(this.bitRateLabel);
            this.Controls.Add(this.bitRateCaptionLabel);
            this.Controls.Add(this.listenerLabel);
            this.Controls.Add(this.listenerCaptionLabel);
            this.Controls.Add(this.playingLabel);
            this.Controls.Add(this.playingCaptionLabel);
            this.Controls.Add(this.clusterUrlLabel);
            this.Controls.Add(this.clusterUrlCaptionLabel);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.categoryCaptionLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titleCaptionLabel);
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
            this.titleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.titleCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.titleLabel.Location = new System.Drawing.Point(3, 19);
            this.titleLabel.Size = new System.Drawing.Size(234, 16);
            this.categoryCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.categoryCaptionLabel.Size = new System.Drawing.Size(62, 16);
            this.categoryLabel.Location = new System.Drawing.Point(3, 51);
            this.categoryLabel.Size = new System.Drawing.Size(234, 16);
            this.clusterUrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.clusterUrlCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.clusterUrlCaptionLabel.Text = "Cluster URL";
            this.clusterUrlLabel.Location = new System.Drawing.Point(3, 109);
            this.clusterUrlLabel.Size = new System.Drawing.Size(234, 16);
            this.playingCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.playingCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.playingCaptionLabel.Text = "Playing Now";
            this.playingLabel.Location = new System.Drawing.Point(3, 167);
            this.playingLabel.Size = new System.Drawing.Size(234, 16);
            this.listenerCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.listenerCaptionLabel.Size = new System.Drawing.Size(120, 16);
            this.listenerLabel.Location = new System.Drawing.Point(3, 199);
            this.listenerLabel.Size = new System.Drawing.Size(120, 16);
            this.bitRateCaptionLabel.Location = new System.Drawing.Point(129, 183);
            this.bitRateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.bitRateLabel.Location = new System.Drawing.Point(129, 199);
            this.bitRateLabel.Size = new System.Drawing.Size(108, 16);
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
            this.titleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.titleCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.titleLabel.Location = new System.Drawing.Point(3, 19);
            this.titleLabel.Size = new System.Drawing.Size(314, 16);
            this.categoryCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.categoryCaptionLabel.Size = new System.Drawing.Size(62, 16);
            this.categoryLabel.Location = new System.Drawing.Point(3, 51);
            this.categoryLabel.Size = new System.Drawing.Size(314, 16);
            this.clusterUrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.clusterUrlCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.clusterUrlCaptionLabel.Text = "Cluster URL";
            this.clusterUrlLabel.Location = new System.Drawing.Point(3, 109);
            this.clusterUrlLabel.Size = new System.Drawing.Size(236, 16);
            this.playingCaptionLabel.Location = new System.Drawing.Point(3, 132);
            this.playingCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.playingLabel.Location = new System.Drawing.Point(86, 132);
            this.playingLabel.Size = new System.Drawing.Size(231, 16);
            this.listenerCaptionLabel.Location = new System.Drawing.Point(3, 148);
            this.listenerCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.listenerLabel.Location = new System.Drawing.Point(86, 148);
            this.listenerLabel.Size = new System.Drawing.Size(120, 16);
            this.bitRateCaptionLabel.Location = new System.Drawing.Point(3, 164);
            this.bitRateCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.bitRateLabel.Location = new System.Drawing.Point(86, 164);
            this.bitRateLabel.Size = new System.Drawing.Size(108, 16);
            this.playButton.Location = new System.Drawing.Point(245, 70);
            this.playButton.Size = new System.Drawing.Size(72, 20);
        }

        private void ChannelPropertyForm_Load(object sender, System.EventArgs e)
        {
            FixWindowSize();
            titleLabel.Text = channel.Title.Trim();
            categoryLabel.Text = channel.Category.Trim();
            clusterUrlLabel.Text = ((channel.GetWebsiteUrl() != null) ? channel.GetWebsiteUrl().ToString().Trim() : "");
            playingLabel.Text = channel.Playing.Trim();
            listenerLabel.Text = channel.Listener.Trim();
            if (channel.BitRate.Length != 0)
            {
                bitRateLabel.Text = channel.BitRate.Trim() + " Kbps";
            }
            else
            {
                bitRateLabel.Text = "Unknown";
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
                if (clusterUrlLabel.Text.Trim().Length != 0)
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
