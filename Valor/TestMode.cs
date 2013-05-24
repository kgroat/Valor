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
        }

        public override void Step(float time)
        {
            count += time;
        }
    }
}
