using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor.Physics.Particles
{
    using Vector;

    class LineParticle : Particle
    {
        public Pen Pen { get; set; }

        public LineParticle(Pen pen, Vector position, Vector velocity, Destruction destruction) : base(position, velocity, destruction)
        {
            this.Pen = pen;
        }

        public override void Render(Graphics g, Point p)
        {
            const float margin = 1;
            var v = new Vector(p.X, p.Y);
            var vel = this.Velocity / 60;
            if (vel.Length < margin)
            {
                vel = vel.Normalize() * margin;
            }
            g.DrawLine(this.Pen, new Line(Position + v, Position - vel + v));
        }
    }
}
