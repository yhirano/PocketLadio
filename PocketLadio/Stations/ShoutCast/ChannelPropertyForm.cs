using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using PocketLadio.Utility;

namespace PocketLadio.Stations.ShoutCast
{
    /// <summary>
    /// チャンネルの詳細情報表示フォーム
    /// </summary>
    public class ChannelPropertyForm : System.Windows.Forms.Form
    {
        private Label TitleCaptionLabel;
        private Label CategoryCaptionLabel;
        private Label ClusterUrlCaptionLabel;
        private Label PlayingCaptionLabel;
        private Label ListenerCaptionLabel;
        private Label BitRateCaptionLabel;
        private Label TitleLabel;
        private Label CategoryLabel;
        private Label ClusterUrlLabel;
        private Label PlayingLabel;
        private Label ListenerLabel;
        private Label BitRateLabel;
        private Button AccessButton;
        private MenuItem OkMenuItem;
        private Button PlayButton;
        private MainMenu MainMenu;

        /// <summary>
        /// チャンネル
        /// </summary>
        private Channel Channel;

        public ChannelPropertyForm(Channel channel)
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            this.Channel = channel;
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
            this.TitleCaptionLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.CategoryCaptionLabel = new System.Windows.Forms.Label();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.ClusterUrlCaptionLabel = new System.Windows.Forms.Label();
            this.ClusterUrlLabel = new System.Windows.Forms.Label();
            this.PlayingCaptionLabel = new System.Windows.Forms.Label();
            this.PlayingLabel = new System.Windows.Forms.Label();
            this.ListenerCaptionLabel = new System.Windows.Forms.Label();
            this.ListenerLabel = new System.Windows.Forms.Label();
            this.BitRateCaptionLabel = new System.Windows.Forms.Label();
            this.BitRateLabel = new System.Windows.Forms.Label();
            this.AccessButton = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.OkMenuItem = new System.Windows.Forms.MenuItem();
            this.PlayButton = new System.Windows.Forms.Button();
            // 
            // TitleCaptionLabel
            // 
            this.TitleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.TitleCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.TitleCaptionLabel.Text = "Title";
            // 
            // TitleLabel
            // 
            this.TitleLabel.Location = new System.Drawing.Point(3, 19);
            this.TitleLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // CategoryCaptionLabel
            // 
            this.CategoryCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.CategoryCaptionLabel.Size = new System.Drawing.Size(62, 16);
            this.CategoryCaptionLabel.Text = "Category";
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.Location = new System.Drawing.Point(3, 51);
            this.CategoryLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // ClusterUrlCaptionLabel
            // 
            this.ClusterUrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.ClusterUrlCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.ClusterUrlCaptionLabel.Text = "Cluster URL";
            // 
            // ClusterUrlLabel
            // 
            this.ClusterUrlLabel.Location = new System.Drawing.Point(3, 109);
            this.ClusterUrlLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // PlayingCaptionLabel
            // 
            this.PlayingCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.PlayingCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.PlayingCaptionLabel.Text = "Playing Now";
            // 
            // PlayingLabel
            // 
            this.PlayingLabel.Location = new System.Drawing.Point(3, 167);
            this.PlayingLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // ListenerCaptionLabel
            // 
            this.ListenerCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.ListenerCaptionLabel.Size = new System.Drawing.Size(120, 16);
            this.ListenerCaptionLabel.Text = "Listener";
            // 
            // ListenerLabel
            // 
            this.ListenerLabel.Location = new System.Drawing.Point(3, 199);
            this.ListenerLabel.Size = new System.Drawing.Size(120, 16);
            // 
            // BitRateCaptionLabel
            // 
            this.BitRateCaptionLabel.Location = new System.Drawing.Point(129, 183);
            this.BitRateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.BitRateCaptionLabel.Text = "Bit rate";
            // 
            // BitRateLabel
            // 
            this.BitRateLabel.Location = new System.Drawing.Point(129, 199);
            this.BitRateLabel.Size = new System.Drawing.Size(108, 16);
            // 
            // AccessButton
            // 
            this.AccessButton.Location = new System.Drawing.Point(165, 128);
            this.AccessButton.Size = new System.Drawing.Size(72, 20);
            this.AccessButton.Text = "&Access";
            this.AccessButton.Click += new System.EventHandler(this.AccessButton_Click);
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
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(165, 70);
            this.PlayButton.Size = new System.Drawing.Size(72, 20);
            this.PlayButton.Text = "&Play";
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // ChannelPropertyForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.AccessButton);
            this.Controls.Add(this.BitRateLabel);
            this.Controls.Add(this.BitRateCaptionLabel);
            this.Controls.Add(this.ListenerLabel);
            this.Controls.Add(this.ListenerCaptionLabel);
            this.Controls.Add(this.PlayingLabel);
            this.Controls.Add(this.PlayingCaptionLabel);
            this.Controls.Add(this.ClusterUrlLabel);
            this.Controls.Add(this.ClusterUrlCaptionLabel);
            this.Controls.Add(this.CategoryLabel);
            this.Controls.Add(this.CategoryCaptionLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.TitleCaptionLabel);
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
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
            this.TitleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.TitleCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.TitleLabel.Location = new System.Drawing.Point(3, 19);
            this.TitleLabel.Size = new System.Drawing.Size(234, 16);
            this.CategoryCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.CategoryCaptionLabel.Size = new System.Drawing.Size(62, 16);
            this.CategoryLabel.Location = new System.Drawing.Point(3, 51);
            this.CategoryLabel.Size = new System.Drawing.Size(234, 16);
            this.ClusterUrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.ClusterUrlCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.ClusterUrlCaptionLabel.Text = "Cluster URL";
            this.ClusterUrlLabel.Location = new System.Drawing.Point(3, 109);
            this.ClusterUrlLabel.Size = new System.Drawing.Size(234, 16);
            this.PlayingCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.PlayingCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.PlayingCaptionLabel.Text = "Playing Now";
            this.PlayingLabel.Location = new System.Drawing.Point(3, 167);
            this.PlayingLabel.Size = new System.Drawing.Size(234, 16);
            this.ListenerCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.ListenerCaptionLabel.Size = new System.Drawing.Size(120, 16);
            this.ListenerLabel.Location = new System.Drawing.Point(3, 199);
            this.ListenerLabel.Size = new System.Drawing.Size(120, 16);
            this.BitRateCaptionLabel.Location = new System.Drawing.Point(129, 183);
            this.BitRateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.BitRateLabel.Location = new System.Drawing.Point(129, 199);
            this.BitRateLabel.Size = new System.Drawing.Size(108, 16);
            this.AccessButton.Location = new System.Drawing.Point(165, 128);
            this.AccessButton.Size = new System.Drawing.Size(72, 20);
            this.PlayButton.Location = new System.Drawing.Point(165, 70);
            this.PlayButton.Size = new System.Drawing.Size(72, 20);
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.TitleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.TitleCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.TitleLabel.Location = new System.Drawing.Point(3, 19);
            this.TitleLabel.Size = new System.Drawing.Size(314, 16);
            this.CategoryCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.CategoryCaptionLabel.Size = new System.Drawing.Size(62, 16);
            this.CategoryLabel.Location = new System.Drawing.Point(3, 51);
            this.CategoryLabel.Size = new System.Drawing.Size(314, 16);
            this.ClusterUrlCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.ClusterUrlCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.ClusterUrlCaptionLabel.Text = "Cluster URL";
            this.ClusterUrlLabel.Location = new System.Drawing.Point(3, 109);
            this.ClusterUrlLabel.Size = new System.Drawing.Size(236, 16);
            this.PlayingCaptionLabel.Location = new System.Drawing.Point(3, 132);
            this.PlayingCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.PlayingLabel.Location = new System.Drawing.Point(86, 132);
            this.PlayingLabel.Size = new System.Drawing.Size(231, 16);
            this.ListenerCaptionLabel.Location = new System.Drawing.Point(3, 148);
            this.ListenerCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.ListenerLabel.Location = new System.Drawing.Point(86, 148);
            this.ListenerLabel.Size = new System.Drawing.Size(120, 16);
            this.BitRateCaptionLabel.Location = new System.Drawing.Point(3, 164);
            this.BitRateCaptionLabel.Size = new System.Drawing.Size(77, 16);
            this.BitRateLabel.Location = new System.Drawing.Point(86, 164);
            this.BitRateLabel.Size = new System.Drawing.Size(108, 16);
            this.PlayButton.Location = new System.Drawing.Point(245, 70);
            this.PlayButton.Size = new System.Drawing.Size(72, 20);
        }

        private void ChannelPropertyForm_Load(object sender, System.EventArgs e)
        {
            FixWindowSize();
            TitleLabel.Text = Channel.Title.Trim();
            CategoryLabel.Text = Channel.Category.Trim();
            ClusterUrlLabel.Text = ((Channel.GetWebsiteUrl() != null) ? Channel.GetWebsiteUrl().ToString().Trim() : "");
            PlayingLabel.Text = Channel.Playing.Trim();
            ListenerLabel.Text = Channel.Listener.Trim();
            if (Channel.BitRate.Length != 0)
            {
                BitRateLabel.Text = Channel.BitRate.Trim() + " Kbps";
            }
            else
            {
                BitRateLabel.Text = "Unknown";
            }
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                PocketLadioUtility.PlayStreaming(Channel.GetPlayUrl());
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
                if (ClusterUrlLabel.Text.Trim().Length != 0)
                {
                    PocketLadioUtility.AccessWebsite(Channel.GetWebsiteUrl());
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
