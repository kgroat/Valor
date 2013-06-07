using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor.Drawing
{
    public class ColorFill : FillMode
    {
        public Color Color { get; private set; }

        public ColorFill(Color col)
        {
            this.Color = col;
        }

        public Color GetColorAt(float x, float y)
        {
            return this.Color;
        }
    }
}
