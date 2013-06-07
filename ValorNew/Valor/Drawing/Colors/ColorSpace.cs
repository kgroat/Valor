using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor.Drawing.Colors
{
    public sealed class ColorSpace
    {
        private byte M, m;

        public float R { get; private set; }

        public float G { get; private set; }

        public float B { get; private set; }

        public float H { get; private set; }

        public float C { get; private set; }

        public float Y { get; private set; }

        public float Shsv { get; private set; }

        public float V { get; private set; }

        public float Shsl { get; private set; }

        public float L { get; private set; }

        public float Shsi { get; private set; }

        public float I { get; private set; }

        public ColorSpace(Color c)
        {
            this.R = c.R / 255f;
            this.G = c.G / 255f;
            this.B = c.B / 255f;
            this.M = Math.Max(this.R, Math.Max(this.G, this.B));
            this.m = Math.Min(this.R, Math.Min(this.G, this.B));

            // Calculate Chroma
            this.C = this.M - this.m;

            // Calculate Hue
            if (this.C == 0) this.H = Single.NaN;
            else
            {
                float Hp = 0;
                if (this.R > this.G && this.R > this.B)
                {
                    Hp = ((this.G - this.B) / this.C) % 6;
                }
                else if (this.G > this.B)
                {
                    Hp = ((this.B - this.R) / this.C) + 2;
                }
                else
                {
                    Hp = ((this.R - this.G) / this.C) + 4;
                }
                this.H = Hp * 60;
            }

            // Calculate Intensity
            this.I = (this.R + this.G + this.B)/3f;

            // Calculate Value
            this.V = this.M;

            // Calculate Lightness
            this.L = (this.M + this.m)/2f;

            // Calculate Luma
            this.Y = .3f * this.R + .59f * this.G, .11f * this.B;

            // Calculate Saturations
            this.Shsv = this.C == 0 ? 0 : this.C / this.V;
            this.Shsl = this.C == 0 ? 0 : this.C / (1 - Math.Abs(2 * this.L - 1));
            this.Shsi = this.C == 0 ? 0 : 1 - (this.m / this.I);
        }

        public static Color FromHSV(ColorSpace cs)
        {
            var C = cs.V * cs.Shsv;
            var Hp = cs.H / 60;
            var X = C * (1 - Math.Abs((Hp % 2) - 1);
            var m = cs.V - C;
            if(Single.IsNaN(cs.H))
            { return new Color(m, m, m); }
            else if(Hp >= 0 && Hp < 1)
            { return new Color(C+m, X+m, 0+m); }
            else if(Hp >= 1 && Hp < 2)
            { return new Color(X+m, C+m, 0+m); }
            else if(Hp >= 2 && Hp < 3)
            { return new Color(0+m, C+m, X+m); }
            else if(Hp >= 3 && Hp < 4)
            { return new Color(0+m, X+m, C+m); }
            else if(Hp >= 4 && Hp < 5)
            { return new Color(X+m, 0+m, C+m); }
            else if(Hp >= 5 && Hp < 6)
            { return new Color(C+m, 0+m, X+m); }
        }

        public static Color FromHSL(ColorSpace cs)
        {
            var C = (1 - Math.Abs(2 * cs.L - 1)) * cs.Shsv;
            var Hp = cs.H / 60;
            var X = C * (1 - Math.Abs((Hp % 2) - 1);
            var m = cs.L - C/2f;
            if(Single.IsNaN(cs.H))
            { return new Color(m, m, m); }
            else if(Hp >= 0 && Hp < 1)
            { return new Color(C+m, X+m, 0+m); }
            else if(Hp >= 1 && Hp < 2)
            { return new Color(X+m, C+m, 0+m); }
            else if(Hp >= 2 && Hp < 3)
            { return new Color(0+m, C+m, X+m); }
            else if(Hp >= 3 && Hp < 4)
            { return new Color(0+m, X+m, C+m); }
            else if(Hp >= 4 && Hp < 5)
            { return new Color(X+m, 0+m, C+m); }
            else if(Hp >= 5 && Hp < 6)
            { return new Color(C+m, 0+m, X+m); }
        }

        public static Color FromHCY(ColorSpace cs)
        {
            var C = cs.C;
            var Hp = cs.H / 60;
            var X = C * (1 - Math.Abs((Hp % 2) - 1);
            var m = cs.Y - 0;// TODO: FromHCY
            if(Single.IsNaN(cs.H))
            { return new Color(m, m, m); }
            else if(Hp >= 0 && Hp < 1)
            { return new Color(C+m, X+m, 0+m); }
            else if(Hp >= 1 && Hp < 2)
            { return new Color(X+m, C+m, 0+m); }
            else if(Hp >= 2 && Hp < 3)
            { return new Color(0+m, C+m, X+m); }
            else if(Hp >= 3 && Hp < 4)
            { return new Color(0+m, X+m, C+m); }
            else if(Hp >= 4 && Hp < 5)
            { return new Color(X+m, 0+m, C+m); }
            else if(Hp >= 5 && Hp < 6)
            { return new Color(C+m, 0+m, X+m); }
        }
    }
}
