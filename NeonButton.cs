using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;

public class NeonButton : Button
{
    private System.Windows.Forms.Timer timer;
    private float hue = 0;
    private PrivateFontCollection pfc = new PrivateFontCollection();

    public NeonButton()
    {
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.BackColor = Color.FromArgb(20, 20, 40);
        this.ForeColor = Color.Cyan;
        this.Font = new Font("Arial Black", 18F, FontStyle.Bold); // Sistem fontu
        this.DoubleBuffered = true;
        this.timer = new System.Windows.Forms.Timer();
        this.timer.Interval = 30;
        this.timer.Tick += (s, e) => { hue += 2; if (hue > 360) hue = 0; this.Invalidate(); };
        this.timer.Start();
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);

        int thickness = 4;
        using (Pen pen = new Pen(ColorFromHSV(hue, 1, 1), thickness))
        {
            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pevent.Graphics.DrawRectangle(pen, thickness / 2, thickness / 2, this.Width - thickness, this.Height - thickness);
        }
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