using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor.Drawing
{
    using Microsoft.Xna.Framework;
    using Physics.Vector;

    public abstract class Graphics
    {
        private FillMode fill;

        protected Matrix xform;

        public Graphics()
        {
            xform = Matrix.Identity;
        }

        public abstract void DrawPolygon(FillMode fill, Polygon2 polygon);

        public abstract void DrawLine(FillMode fill, float startX, float startY, float endX, float endY, float width = 1);

        public abstract void DrawRay(FillMode fill, float startX, float startY, float dirX, float dirY, float width = 1);

        public abstract void DrawRectangle(FillMode fill, float locX, float locY, float width, float height);

        public abstract void DrawCircle(FillMode fill, float locX, float locY, float radius);

        public abstract void DrawEllipse(FillMode fill, float locX, float locY, float width, float height);

        public void DrawLine(FillMode fill, Vector2 start, Vector2 end)
        {
            this.DrawLine(fill, start.X, start.Y, end.X, end.Y);
        }

        public void DrawRectangle(FillMode fill, Vector2 loc, Vector2 size)
        {
            this.DrawRectangle(fill, loc.X, loc.Y, size.X, size.Y);
        }

        public void DrawCircle(FillMode fill, Vector2 loc, float radius)
        {
            this.DrawCircle(fill, loc.X, loc.Y, radius);
        }

        public void DrawEllipse(FillMode fill, Vector2 loc, Vector2 size)
        {
            this.DrawEllipse(fill, loc.X, loc.Y, size.X, size.Y);
        }

        public void DrawRay(FillMode fill, Vector2 loc, Vector2 dir)
        {
            this.DrawRay(fill, loc.X, loc.Y, dir.X, dir.Y);
        }
    }
}
