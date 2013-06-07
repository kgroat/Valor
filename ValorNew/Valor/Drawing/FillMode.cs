using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor.Drawing
{
    public interface FillMode
    {
        Color GetColorAt(float x, float y);
    }
}
