using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor
{
    using Llamanim.Anim;

    public class TestMode : GameMode
    {
        public static readonly double FLIP_RATE = Double.Parse(ConfigurationManager.AppSettings["flipRate"]);
        public static readonly int CYCLE_RATE = Int32.Parse(ConfigurationManager.AppSettings["cycleRate"]);

        private double count = 0;

        public IRenderable Image { get; set; }

        public override void Render(System.Drawing.Graphics g, float width, float height)
        {
            g.FillRectangle(Brushes.Black, 0, 0, width, height);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.Image.Render(g, new Point(1, 1));
            var imageWidth = this.Image.GetBounds().Width;
            var imageHeight = this.Image.GetBounds().Height;
            
            g.DrawString("Try playing around in app.config...", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 1));
            g.DrawString("filename = image file to load", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 3.2f));
            g.DrawString("cycleMin & cycleMax = just play with this and see (0 - 255, int)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 4.4f));
            g.DrawString("    (try cycleMin=0 and cycleMax=255)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 5.6f));
            g.DrawString("cycleRate = the speed at which the crazy shit happens (int)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 6.8f));
            g.DrawString("flipRate = seconds between image swap (double)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 8f));
            g.DrawString("scale = self explanatory (float)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 9.2f));
            g.DrawString("FontFile = the Font file to attempt to use", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 10.4f));
            g.DrawString("Font = the system Font to use if FontFile can't be opened", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 11.6f));
            g.DrawString("music = the audio file to try to play (must be .mp3)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 12.8f));
            g.DrawString("vfps = visual frames per second (double)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 14f));
            g.DrawString("cfps = calculation frames per second (double)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 15.2f));
            g.DrawString("hardCodedColors = whether to use replacement & lighting colors", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 16.4f));
            g.DrawString("redReplacementColor = hard-coded color to replace red", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 17.6f));
            g.DrawString("greenReplacementColor = hard-coded color to replace green", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 18.8f));
            g.DrawString("lightingColor = hard-coded color for light (use cycleRate=0)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 20f));
            g.DrawString("darknessColor = hard-coded color for darkness (cycleRate=0)", new Font(ValorEngine.Font.FontFamily, 1f), Brushes.White, new PointF(imageWidth + 1, 21.2f));
        }

        public override void Step(double time)
        {
            count += time;
            //Image.C3 = GraphicsHelper.Step(Image.C3, CYCLE_RATE);
        }
    }
}
