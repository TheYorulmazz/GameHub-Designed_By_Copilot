using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

namespace GameHub
{
    public partial class Form1 : Form
    {
        private Button btnAddAllGames;
        private Panel customScrollBar;

        public Form1()
        {
            InitializeComponent();
            

            // Sadece tek artý butonu
            btnAddAllGames = new Button
            {
                Text = "+",
                Font = new Font("Arial", 18F, FontStyle.Bold),
                ForeColor = Color.Cyan,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(60, 60),
                Location = new Point(pnlAllGames.Right + 10, pnlAllGames.Top)
            };
            btnAddAllGames.FlatAppearance.BorderColor = Color.Cyan;
            btnAddAllGames.FlatAppearance.BorderSize = 1;
            btnAddAllGames.Click += (s, e) => AddGameToPanel(pnlAllGames);
            Controls.Add(btnAddAllGames);

            // Gaming scroll bar paneli
            customScrollBar = new Panel
            {
                Width = 8,
                Height = 60,
                BackColor = Color.Cyan,
                Location = new Point(pnlAllGames.Right - 8, pnlAllGames.Top),
                Visible = false
            };
            Controls.Add(customScrollBar);

            // Scroll eventini baðla
            pnlAllGames.Scroll += PnlAllGames_Scroll;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyOrbitronFont();
            btnPlay.BringToFront();

            // Kutucuk ve margin ayarlarý
            int kutucukWidth = 90;
            int kutucukMargin = 8;
            int kutucukCountPerRow = 4;
            int satirSayisi = 3; // 3 satýr gösterilecek
            int panelWidth = (kutucukWidth * kutucukCountPerRow) + (kutucukMargin * (kutucukCountPerRow + 1));
            int panelHeight = (110 * satirSayisi) + (kutucukMargin * (satirSayisi + 1));

            pnlAllGames.FlowDirection = FlowDirection.LeftToRight;
            pnlAllGames.WrapContents = true;
            pnlAllGames.AutoScroll = false; // Scrollbar çýkmasýn
            pnlAllGames.Padding = new Padding(kutucukMargin, kutucukMargin, kutucukMargin, kutucukMargin);
            pnlAllGames.Margin = new Padding(0);
            pnlAllGames.Width = panelWidth;
            pnlAllGames.Height = panelHeight;

            // Scrollbar konumunu ayarla
            customScrollBar.Location = new Point(pnlAllGames.Right - 8, pnlAllGames.Top);
            customScrollBar.Height = Math.Max(40, pnlAllGames.Height / 3);
            customScrollBar.Visible = pnlAllGames.VerticalScroll.Visible;

            // Favori Oyunlar paneli
            //pnlFavGames.FlowDirection = FlowDirection.LeftToRight;
            //pnlFavGames.WrapContents = true;
            //pnlFavGames.AutoScroll = true;
            //pnlFavGames.Padding = new Padding(0);
            //pnlFavGames.Margin = new Padding(0);
            //pnlFavGames.Width = panelWidth;
            //pnlFavGames.Height = panelHeight;

            //// Favori paneli aþaðýya kaydýr
            //pnlFavGames.Top = pnlAllGames.Top + pnlAllGames.Height + 40;
        }

        private void PnlAllGames_Scroll(object sender, ScrollEventArgs e)
        {
            // Gaming scroll barý güncelle
            if (pnlAllGames.VerticalScroll.Visible)
            {
                customScrollBar.Visible = true;
                int scrollMax = pnlAllGames.VerticalScroll.Maximum - pnlAllGames.VerticalScroll.LargeChange + 1;
                int scrollVal = pnlAllGames.VerticalScroll.Value;
                int barHeight = Math.Max(40, pnlAllGames.Height * pnlAllGames.Height / Math.Max(1, pnlAllGames.DisplayRectangle.Height));
                int barY = pnlAllGames.Top + (scrollVal * (pnlAllGames.Height - barHeight)) / Math.Max(1, scrollMax);
                customScrollBar.Height = barHeight;
                customScrollBar.Top = barY;
            }
            else
            {
                customScrollBar.Visible = false;
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void AddGameToPanel(FlowLayoutPanel panel)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Uygulama veya Kýsayol (*.exe;*.url)|*.exe;*.url";
                ofd.Title = "Bir oyun (exe) veya internet kýsayolu (*.url) seçin";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string file = ofd.FileName;
                        Icon icon = null;
                        string displayName = Path.GetFileNameWithoutExtension(file);

                        if (Path.GetExtension(file).Equals(".url", StringComparison.OrdinalIgnoreCase))
                        {
                            string targetPath = GetUrlTargetPath(file);
                            if (!string.IsNullOrEmpty(targetPath) && File.Exists(targetPath))
                            {
                                icon = Icon.ExtractAssociatedIcon(targetPath);
                                displayName = Path.GetFileNameWithoutExtension(targetPath);
                            }
                            else
                            {
                                icon = SystemIcons.Information;
                                displayName = GetUrlTitle(file) ?? displayName;
                            }
                        }
                        else
                        {
                            icon = Icon.ExtractAssociatedIcon(file);
                        }

                        // Özelleþtirilmiþ kutucuk paneli
                        var gamePanel = new Panel
                        {
                            Width = 90,
                            Height = 110,
                            BackColor = Color.FromArgb(30, 30, 50),
                            Margin = new Padding(8),
                            Padding = new Padding(0)
                        };

                        var pb = new PictureBox
                        {
                            Image = icon.ToBitmap(),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Size = new Size(64, 64),
                            Location = new Point((gamePanel.Width - 64) / 2, 6)
                        };

                        var lbl = new Label
                        {
                            Text = displayName,
                            AutoSize = false,
                            TextAlign = ContentAlignment.TopCenter,
                            ForeColor = Color.White,
                            Font = new Font("Arial", 9, FontStyle.Bold),
                            Location = new Point(5, 74),
                            Width = gamePanel.Width - 10,
                            Height = 32,
                            MaximumSize = new Size(gamePanel.Width - 10, 32),
                            AutoEllipsis = true
                        };

                        // Çok satýrlý isim desteði
                        lbl.TextAlign = ContentAlignment.TopCenter;

                        gamePanel.Controls.Add(pb);
                        gamePanel.Controls.Add(lbl);
                        panel.Controls.Add(gamePanel);

                        // Dinamik yükseklik ayarý:
                        int kutucukHeight = 110;
                        int kutucukMargin = 8;
                        int kutucukCountPerRow = 4;
                        int toplamOyun = panel.Controls.Count;
                        int satirSayisi = (int)Math.Ceiling(toplamOyun / (double)kutucukCountPerRow);
                        panel.Height = (kutucukHeight * satirSayisi) + (kutucukMargin * (satirSayisi + 1));
                    }
                    catch
                    {
                        MessageBox.Show("Dosyanýn ikonu alýnamadý.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // .url dosyasýnýn hedefini okuyan yardýmcý fonksiyon
        private string GetUrlTargetPath(string urlFilePath)
        {
            try
            {
                foreach (var line in File.ReadAllLines(urlFilePath))
                {
                    if (line.StartsWith("URL=", StringComparison.OrdinalIgnoreCase))
                    {
                        string url = line.Substring(4);
                        if (url.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && File.Exists(url))
                            return url;
                        return null;
                    }
                }
            }
            catch { }
            return null;
        }

        // .url dosyasýndan baþlýk (isim) okuma
        private string GetUrlTitle(string urlFilePath)
        {
            try
            {
                foreach (var line in File.ReadAllLines(urlFilePath))
                {
                    if (line.StartsWith("Name=", StringComparison.OrdinalIgnoreCase))
                        return line.Substring(5);
                }
            }
            catch { }
            return null;
        }

        private void pnlGameDetail_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

public class RgbPanel : Panel
{
    private System.Windows.Forms.Timer timer;
    private float hue = 0;

    public RgbPanel()
    {
        this.DoubleBuffered = true;
        this.timer = new System.Windows.Forms.Timer();
        this.timer.Interval = 30;
        this.timer.Tick += (s, e) => { hue += 2; if (hue > 360) hue = 0; this.Invalidate(); };
        this.timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        int thickness = 4;
        int radius = 24; // Köþe yumuþaklýðý (isteðe göre artýrýlabilir)
        Rectangle rect = new Rectangle(thickness / 2, thickness / 2, this.Width - thickness, this.Height - thickness);

        using (GraphicsPath path = RoundedRect(rect, radius))
        using (Pen pen = new Pen(ColorFromHSV(hue, 1, 1), thickness))
        {
            pen.Alignment = PenAlignment.Inset;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawPath(pen, path);
        }
    }

    private GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        int diameter = radius * 2;
        GraphicsPath path = new GraphicsPath();
        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }

    private Color ColorFromHSV(double hue, double saturation, double value)
    {
        int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        double f = hue / 60 - Math.Floor(hue / 60);

        value = value * 255;
        int v = Convert.ToInt32(value);
        int p = Convert.ToInt32(value * (1 - saturation));
        int q = Convert.ToInt32(value * (1 - f * saturation));
        int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => Color.FromArgb(255, v, t, p),
            1 => Color.FromArgb(255, q, v, p),
            2 => Color.FromArgb(255, p, v, t),
            3 => Color.FromArgb(255, p, q, v),
            4 => Color.FromArgb(255, t, p, v),
            _ => Color.FromArgb(255, v, p, q),
        };
    }
}
