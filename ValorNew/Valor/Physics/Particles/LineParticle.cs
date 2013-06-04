using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor.Physics.Particles
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Vector;

    class LineParticle : Particle
    {
        private BasicEffect line { get; set; }

        public Color Color { get; set; }

        public LineParticle(Color color, GraphicsDevice graphicsDevice, Vector position, Vector velocity, Destruction destruction)
            : base(graphicsDevice, position, velocity, destruction)
        {
            this.Color = color;
            this.line = new BasicEffect(graphicsDevice);
            this.line.VertexColorEnabled = true;
            this.line.Projection = Matrix.CreateOrthographicOffCenter
                (0, graphicsDevice.Viewport.Width,     // left, right
                 graphicsDevice.Viewport.Height, 0,    // bottom, top
                 0, 1);
        }

        public override void Render(Point p)
        {
            const float Margin = 1;
            var g = this.line;
            var v = new Vector(p.X, p.Y);
            var vel = this.Velocity / 60;
            if (vel.Length < Margin)
            {
                vel = vel.Normalize() * Margin;
            }
            var vertices = new VertexPositionColor[2];
            vertices[0].Position = (this.Position + v + new Vector(0, 0, -2)).ToVector3();
            vertices[0].Color = this.Color;
            vertices[1].Position = (this.Position - vel + v + new Vector(0, 0, -2)).ToVector3();
            vertices[1].Color = this.Color;

            line.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);

            ////g.DrawLine(this.Pen, new Line(Position + v, Position - vel + v));
        }
    }
}
