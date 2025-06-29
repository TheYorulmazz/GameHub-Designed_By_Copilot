using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace GameHub
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAllGames;
       // private System.Windows.Forms.Label lblFavGames;
        private System.Windows.Forms.FlowLayoutPanel pnlAllGames;
       // private System.Windows.Forms.FlowLayoutPanel pnlFavGames;
        private RgbPanel pnlGameDetail;
        private NeonButton btnPlay;
        private PrivateFontCollection pfc = new PrivateFontCollection();

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblAllGames = new Label();
            pnlAllGames = new FlowLayoutPanel();
            pnlGameDetail = new RgbPanel();
            btnPlay = new NeonButton();
            button1 = new Button();
            pnlGameDetail.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial Narrow", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Cyan;
            lblTitle.Location = new Point(12, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(712, 99);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ATLANTIS LAUNCHER";
            lblTitle.Click += lblTitle_Click;
            // 
            // lblAllGames
            // 
            lblAllGames.Font = new Font("Arial Black", 14F, FontStyle.Bold);
            lblAllGames.ForeColor = Color.Violet;
            lblAllGames.Location = new Point(26, 123);
            lblAllGames.Name = "lblAllGames";
            lblAllGames.Size = new Size(172, 39);
            lblAllGames.TabIndex = 1;
            lblAllGames.Text = "OYUNLAR";
            // 
            // pnlAllGames
            // 
            pnlAllGames.AutoScroll = true;
            pnlAllGames.BackColor = Color.Transparent;
            pnlAllGames.Location = new Point(26, 179);
            pnlAllGames.Name = "pnlAllGames";
            pnlAllGames.Size = new Size(614, 503);
            pnlAllGames.TabIndex = 2;
            // 
            // pnlGameDetail
            // 
            pnlGameDetail.Controls.Add(btnPlay);
            pnlGameDetail.Location = new Point(730, 69);
            pnlGameDetail.Name = "pnlGameDetail";
            pnlGameDetail.Size = new Size(350, 613);
            pnlGameDetail.TabIndex = 6;
            pnlGameDetail.Paint += pnlGameDetail_Paint;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(20, 20, 40);
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Font = new Font("Arial Black", 18F, FontStyle.Bold);
            btnPlay.ForeColor = Color.Cyan;
            btnPlay.Location = new Point(45, 542);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(250, 50);
            btnPlay.TabIndex = 7;
            btnPlay.Text = "▶ OYNA";
            btnPlay.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = Color.Cyan;
            button1.Location = new Point(646, 622);
            button1.Name = "button1";
            button1.Size = new Size(60, 60);
            button1.TabIndex = 7;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = false;
            button1.Visible = false;
            // 
            // Form1
            // 
            BackColor = Color.FromArgb(20, 10, 40);
            ClientSize = new Size(1108, 703);
            Controls.Add(button1);
            Controls.Add(lblTitle);
            Controls.Add(lblAllGames);
            Controls.Add(pnlAllGames);
            Controls.Add(pnlGameDetail);
            ForeColor = Color.FromArgb(20, 10, 40);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Form1";
            Load += Form1_Load;
            pnlGameDetail.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public void ApplyOrbitronFont()
        {
            string fontPath = System.IO.Path.Combine(Application.StartupPath, "fonts", "Orbitron-Bold.ttf");
            if (!System.IO.File.Exists(fontPath))
                MessageBox.Show("Font dosyası bulunamadı: " + fontPath);

            pfc.AddFontFile(fontPath);

            lblTitle.Font = new Font(pfc.Families[0], 28F, FontStyle.Bold);
            lblAllGames.Font = new Font(pfc.Families[0], 16F, FontStyle.Bold);
            //lblFavGames.Font = new Font(pfc.Families[0], 16F, FontStyle.Bold);
            //btnAddFav.Font = new Font(pfc.Families[0], 18F, FontStyle.Bold);
            btnPlay.Font = new Font(pfc.Families[0], 18F, FontStyle.Bold);
        }

        public void LoadCustomFont()
        {
            string fontPath = System.IO.Path.Combine(Application.StartupPath, "fonts", "Orbitron-Bold.ttf");
            if (!System.IO.File.Exists(fontPath))
                MessageBox.Show("Font dosyası bulunamadı: " + fontPath);

            pfc.AddFontFile(fontPath);
            this.Font = new Font(pfc.Families[0], 18, FontStyle.Bold);
        }
        private Button button1;
       // private Button btnAddFav;
    }
}
