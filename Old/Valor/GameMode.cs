using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor
{
    public abstract class GameMode
    {
        public abstract void Render(Graphics g, float width, float height);

        public abstract void Step(float time);
    }
}
