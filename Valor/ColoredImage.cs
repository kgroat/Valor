using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor
{
    public class ColoredImage
    {
        protected Color c1, c2, c3;

        protected Bitmap original, current;

        public ColoredImage(Bitmap original, Color c1, Color c2) : this(original, c1, c2, Color.White) { }

        public ColoredImage(Bitmap original, Color c1, Color c2, Color c3)
        {
            this.original = new Bitmap(original);
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
            this.RepaintCurrent();
        }

        public Color C1
        {
            get
            {
                return c1;
            }
            set
            {
                this.c1 = value;
                this.RepaintCurrent();
            }
        }

        public Color C2
        {
            get
            {
                return this.c2;
            }
            set
            {
                this.c2 = value;
                this.RepaintCurrent();
            }
        }

        public Color C3
        {
            get
            {
                return this.c3;
            }
            set
            {
                this.c3 = value;
                this.RepaintCurrent();
            }
        }

        public Bitmap Original
        {
            get
            {
                return this.original;
            }
            set
            {
                this.original = value;
                this.RepaintCurrent();
            }
        }

        public Bitmap Current
        {
            get
            {
                return this.current;
            }
        }

        protected void RepaintCurrent()
        {
            if(this.current == null || this.current.Width != this.original.Width || this.current.Height != this.original.Height)
            {
                this.current = new Bitmap(original);
            }
            for (int x = 0; x < this.original.Width; x++)
            {
                for (int y = 0; y < this.original.Height; y++)
                {
                    var oCol = this.original.GetPixel(x, y);
                    var a = oCol.A;
                    var br = oCol.B;
                    var c1v = oCol.R - br;
                    var c2v = oCol.G - br;
                    var r = (c1v * c1.R + c2v * c2.R + br * c3.R + (255 - br) * (255 - c3.R)) / 255;
                    var g = (c1v * c1.G + c2v * c2.G + br * c3.G + (255 - br) * (255 - c3.G)) / 255;
                    var b = (c1v * c1.B + c2v * c2.B + br * c3.B + (255 - br) * (255 - c3.B)) / 255;
                    GraphicsHelper.Clamp(0, a, 255);
                    this.current.SetPixel(x, y, Color.FromArgb(a, GraphicsHelper.Clamp(0, r, 255), GraphicsHelper.Clamp(0, g, 255), GraphicsHelper.Clamp(0, b, 255)));
                }
            }
        }

        public static implicit operator Bitmap(ColoredImage self)
        {
            return self.Current;
        }
    }
}
