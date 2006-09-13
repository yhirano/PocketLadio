using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using PocketLadio.Utility;

namespace PocketLadio.Stations.RssPodcast
{
    /// <summary>
    /// チャンネルの詳細情報表示フォーム
    /// </summary>
    public class ChannelPropertyForm : System.Windows.Forms.Form
    {
        private Label TitleCaptionLabel;
        private Label DescriptionCaptionLabel;
        private Label LinkCaptionLabel;
        private Label DateCaptionLabel;
        private Label TitleLabel;
        private Label DescriptionLabel;
        private Label LinkLabel;
        private Label DateLabel;
        private Button AccessButton;
        private MenuItem OkMenuItem;
        private Button PlayButton;
        private MainMenu MainMenu;
        private Label AuthorLabel;
        private Label AuthorCaptionLabel;
        private Label LengthLabel;
        private Label LengthCaptionLabel;
        private Label TypeLabel;
        private Label TypeCaptionLabel;

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
            this.DescriptionCaptionLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.LinkCaptionLabel = new System.Windows.Forms.Label();
            this.LinkLabel = new System.Windows.Forms.Label();
            this.DateCaptionLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.AccessButton = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.OkMenuItem = new System.Windows.Forms.MenuItem();
            this.PlayButton = new System.Windows.Forms.Button();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.AuthorCaptionLabel = new System.Windows.Forms.Label();
            this.LengthLabel = new System.Windows.Forms.Label();
            this.LengthCaptionLabel = new System.Windows.Forms.Label();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.TypeCaptionLabel = new System.Windows.Forms.Label();
            // 
            // TitleCaptionLabel
            // 
            this.TitleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.TitleCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.TitleCaptionLabel.Text = "番組名";
            // 
            // TitleLabel
            // 
            this.TitleLabel.Location = new System.Drawing.Point(3, 19);
            this.TitleLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // DescriptionCaptionLabel
            // 
            this.DescriptionCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.DescriptionCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.DescriptionCaptionLabel.Text = "詳細";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.Location = new System.Drawing.Point(3, 51);
            this.DescriptionLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // LinkCaptionLabel
            // 
            this.LinkCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.LinkCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.LinkCaptionLabel.Text = "リンク";
            // 
            // LinkLabel
            // 
            this.LinkLabel.Location = new System.Drawing.Point(3, 109);
            this.LinkLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // DateCaptionLabel
            // 
            this.DateCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.DateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.DateCaptionLabel.Text = "配信時刻";
            // 
            // DateLabel
            // 
            this.DateLabel.Location = new System.Drawing.Point(3, 199);
            this.DateLabel.Size = new System.Drawing.Size(234, 16);
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
            // AuthorLabel
            // 
            this.AuthorLabel.Location = new System.Drawing.Point(3, 167);
            this.AuthorLabel.Size = new System.Drawing.Size(88, 16);
            // 
            // AuthorCaptionLabel
            // 
            this.AuthorCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.AuthorCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.AuthorCaptionLabel.Text = "著者";
            // 
            // LengthLabel
            // 
            this.LengthLabel.Location = new System.Drawing.Point(3, 231);
            this.LengthLabel.Size = new System.Drawing.Size(88, 16);
            // 
            // LengthCaptionLabel
            // 
            this.LengthCaptionLabel.Location = new System.Drawing.Point(3, 215);
            this.LengthCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.LengthCaptionLabel.Text = "長さ";
            // 
            // TypeLabel
            // 
            this.TypeLabel.Location = new System.Drawing.Point(97, 231);
            this.TypeLabel.Size = new System.Drawing.Size(140, 16);
            // 
            // TypeCaptionLabel
            // 
            this.TypeCaptionLabel.Location = new System.Drawing.Point(97, 215);
            this.TypeCaptionLabel.Size = new System.Drawing.Size(34, 16);
            this.TypeCaptionLabel.Text = "タイプ";
            // 
            // ChannelPropertyForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.LengthLabel);
            this.Controls.Add(this.LengthCaptionLabel);
            this.Controls.Add(this.TypeLabel);
            this.Controls.Add(this.TypeCaptionLabel);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.AuthorCaptionLabel);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.AccessButton);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.DateCaptionLabel);
            this.Controls.Add(this.LinkLabel);
            this.Controls.Add(this.LinkCaptionLabel);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.DescriptionCaptionLabel);
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
            this.TitleCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.TitleLabel.Location = new System.Drawing.Point(3, 19);
            this.TitleLabel.Size = new System.Drawing.Size(234, 16);
            this.DescriptionCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.DescriptionCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.DescriptionLabel.Location = new System.Drawing.Point(3, 51);
            this.DescriptionLabel.Size = new System.Drawing.Size(234, 16);
            this.LinkCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.LinkCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.LinkLabel.Location = new System.Drawing.Point(3, 109);
            this.LinkLabel.Size = new System.Drawing.Size(234, 16);
            this.DateCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.DateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.DateLabel.Location = new System.Drawing.Point(3, 199);
            this.DateLabel.Size = new System.Drawing.Size(234, 16);
            this.AccessButton.Location = new System.Drawing.Point(165, 128);
            this.AccessButton.Size = new System.Drawing.Size(72, 20);
            this.PlayButton.Location = new System.Drawing.Point(165, 70);
            this.PlayButton.Size = new System.Drawing.Size(72, 20);
            this.AuthorLabel.Location = new System.Drawing.Point(3, 167);
            this.AuthorLabel.Size = new System.Drawing.Size(88, 16);
            this.AuthorCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.AuthorCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.LengthLabel.Location = new System.Drawing.Point(3, 231);
            this.LengthLabel.Size = new System.Drawing.Size(88, 16);
            this.LengthCaptionLabel.Location = new System.Drawing.Point(3, 215);
            this.LengthCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.TypeLabel.Location = new System.Drawing.Point(97, 231);
            this.TypeLabel.Size = new System.Drawing.Size(140, 16);
            this.TypeCaptionLabel.Location = new System.Drawing.Point(97, 215);
            this.TypeCaptionLabel.Size = new System.Drawing.Size(34, 16);
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.TitleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.TitleCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.TitleCaptionLabel.Text = "番組名";
            this.TitleLabel.Location = new System.Drawing.Point(3, 19);
            this.TitleLabel.Size = new System.Drawing.Size(314, 16);
            this.DescriptionCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.DescriptionCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.DescriptionLabel.Location = new System.Drawing.Point(3, 51);
            this.DescriptionLabel.Size = new System.Drawing.Size(314, 16);
            this.LinkCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.LinkCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.LinkLabel.Location = new System.Drawing.Point(3, 109);
            this.LinkLabel.Size = new System.Drawing.Size(236, 16);
            this.DateCaptionLabel.Location = new System.Drawing.Point(3, 141);
            this.DateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.DateLabel.Location = new System.Drawing.Point(73, 141);
            this.DateLabel.Size = new System.Drawing.Size(244, 16);
            this.AccessButton.Location = new System.Drawing.Point(245, 109);
            this.AccessButton.Size = new System.Drawing.Size(72, 20);
            this.PlayButton.Location = new System.Drawing.Point(245, 70);
            this.PlayButton.Size = new System.Drawing.Size(72, 20);
            this.AuthorLabel.Location = new System.Drawing.Point(41, 125);
            this.AuthorLabel.Size = new System.Drawing.Size(88, 16);
            this.AuthorCaptionLabel.Location = new System.Drawing.Point(3, 125);
            this.AuthorCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.LengthLabel.Location = new System.Drawing.Point(41, 157);
            this.LengthLabel.Size = new System.Drawing.Size(88, 16);
            this.LengthCaptionLabel.Location = new System.Drawing.Point(3, 157);
            this.LengthCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.TypeLabel.Location = new System.Drawing.Point(175, 157);
            this.TypeLabel.Size = new System.Drawing.Size(142, 16);
            this.TypeCaptionLabel.Location = new System.Drawing.Point(135, 157);
            this.TypeCaptionLabel.Size = new System.Drawing.Size(34, 16);
        }

        private void ChannelPropertyForm_Load(object sender, System.EventArgs e)
        {
            FixWindowSize();
            TitleLabel.Text = Channel.Title.Trim();
            DescriptionLabel.Text = Channel.Description.Trim();
            LinkLabel.Text = ((Channel.Link != null) ? Channel.Link.ToString().Trim() : "");
            AuthorLabel.Text = Channel.Author.Trim();
            DateLabel.Text = Channel.Date.Trim();
            LengthLabel.Text = Channel.Length.Trim();
            TypeLabel.Text = Channel.Type.Trim();
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
                if (LinkLabel.Text.Trim().Length != 0)
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
