using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor
{
    public class TestMode : GameMode
    {
        public static readonly double FLIP_RATE = Double.Parse(ConfigurationManager.AppSettings["flipRate"]);
        public static readonly int CYCLE_RATE = Int32.Parse(ConfigurationManager.AppSettings["cycleRate"]);

        private double count = 0;

        public ColoredImage Image { get; set; }

        public override void Render(System.Drawing.Graphics g, float width, float height)
        {
            g.FillRectangle(Brushes.Black, 0, 0, width, height);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            if ((int)(count / FLIP_RATE) % 2 == 0)
            {
                g.DrawImage(this.Image.Current, 1, 1);
                g.DrawString("Colorshifted", new Font(ValorEngine.Font.FontFamily, 8), Brushes.White, new PointF(1, this.Image.Current.Height));
            }
            else
            {
                g.DrawImage(this.Image.Original, 1, 1);
                g.DrawString("Original", new Font(ValorEngine.Font.FontFamily, 8), Brushes.White, new PointF(1, this.Image.Original.Height));
            }
            g.DrawString("Try playing around in app.config...", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 1));
            g.DrawString("filename = image file to load", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 3.2f));
            g.DrawString("cycleMin & cycleMax = just play with this and see (0 - 255, int)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 4.4f));
            g.DrawString("    (try cycleMin=0 and cycleMax=255)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 5.6f));
            g.DrawString("cycleRate = the speed at which the crazy shit happens (int)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 6.8f));
            g.DrawString("flipRate = seconds between image swap (double)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 8f));
            g.DrawString("scale = self explanatory (float)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 9.2f));
            g.DrawString("FontFile = the Font file to attempt to use", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 10.4f));
            g.DrawString("Font = the system Font to use if FontFile can't be opened", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 11.6f));
            g.DrawString("music = the audio file to try to play (must be .mp3)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 12.8f));
            g.DrawString("vfps = visual frames per second (double)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 14f));
            g.DrawString("cfps = calculation frames per second (double)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(this.Image.Current.Width + 1, 15.2f));
        }

        public override void Step(double time)
        {
            count += time;
            Image.C3 = GraphicsHelper.Step(Image.C3, CYCLE_RATE);
        }
    }
}
