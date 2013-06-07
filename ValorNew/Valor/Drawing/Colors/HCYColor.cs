using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor.Drawing.Colors
{
    public class HCYColor : ColorSpace
    {
        public float H { get; set; }

        public float C { get; set; }

        public float Y { get; set; }

        public HSVColor(Color c)
        {
            int M = Math.Max(c.R, Math.Max(c.G, c.B));
            int m = Math.Min(c.R, Math.Min(c.G, c.B));
            this.C = M - m;
            if (this.C == 0) this.H = 0;
            else
            {
                float Hp = 0;
                if (M == c.R)
                {
                    Hp = ((c.G - c.B) / c) % 6;
                }
                else if(M == c.G)
                {
                    Hp = ((c.B - c.R) / c) + 2;
                }
                else if (M == c.B)
                {
                    Hp = ((c.R - c.G) / c) + 4;
                }
                this.H = Hp * 60;
            }
            this.Y = .3f * c.R + .59f * c.G, .11f * c.B;
        }
    
        public Color ToRGB()
        {
 	        throw new NotImplementedException("HCY cannot yet convert to RGB");
        }
    }
}
