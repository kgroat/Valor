using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Llamanim.Anim;
using Valor.Physics.Vector;

namespace Valor
{
    public class Ship
    {
        public RotatableImage ToRender { get; set; }

        public float Theta
        {
            get { return ToRender.Theta; }
            set { ToRender.Theta = value; }
        }

        public Vector PositionOnScreen { get; set; }

        public Vector PositionInSpace { get; set; }

        public Vector Velocity { get; set; }

        public Ship(RotatableImage toRender)
        {
            this.ToRender = toRender;
        }

        public Vector GetRelativePosition()
        {
            return ValorEngine.CenterOfViewInSpace - PositionInSpace;
        }

        public void Render(Graphics g, Vector pos)
        {
            ToRender.Render(g, pos);
        }
    }
}
