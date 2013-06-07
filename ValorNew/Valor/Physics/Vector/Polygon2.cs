using Microsoft.Xna.Framework;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Valor.Physics.Vector
{
    public class Polygon2
    {
        private Vector2[] points;

        public IEnumerable<Vector2> Points
        {
            get { return points; }
        }

        public RectangleF Bounds { get; private set; }

        public Polygon2(Vector2[] pts)
        {
            this.points = pts;
            this.Bounds = this.RecalcBounds();
        }

        public Polygon2(IEnumerable<Vector2> pts) : this(pts.ToArray())
        { }

        public byte[][] getFill(float offsetX, float offsetY, int width, int height)
        {
            var output = new byte[width][];
            offsetX %= 1;
            offsetY %= 1;
            for(float x = offsetX; x < width; x++)
            {
                output[(int)x] = new byte[height];
                for (float y = offsetY; y < width; y++)
                {
                }
            }
            return output;
        }

        private RectangleF RecalcBounds()
        {
            float minX, minY, maxX, maxY;
            minX = maxX = this.points[0].X;
            minY = maxY = this.points[0].Y;
            foreach(var point in this.Points)
            {
                if(point.X < minX)
                {
                    minX = point.X;
                }
                else if(point.X > maxX)
                {
                    maxX = point.X;
                }

                if(point.Y < minY)
                {
                    minY = point.Y;
                }
                else if(point.Y > maxY)
                {
                    maxY = point.Y;
                }
            }
            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }
    }
}
