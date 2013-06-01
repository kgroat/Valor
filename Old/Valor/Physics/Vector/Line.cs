namespace Valor.Physics.Vector
{
    using System;

    public class Line
    {
        public float X
        {
            get
            {
                return this.End.X - this.Start.X;
            }
        }

        public float Y
        {
            get
            {
                return this.End.Y - this.Start.Y;
            }
        }

        public float Z
        {
            get
            {
                return this.End.Z - this.Start.Z;
            }
        }

        public float Length
        {
            get
            {
                return this.AsVector().Length;
            }
        }

        public Vector Start { get; protected set; }

        public Vector End { get; protected set; }

        public Line(Vector start, Vector end)
        {
            this.Start = start;
            this.End = end;
        }

        public Line(float startX, float startY, float endX, float endY)
        {
            this.Start = new Vector(startX, startY);
            this.End = new Vector(endX, endY);
        }

        public virtual bool Intersects2D(Line intersector)
        {
            bool intersection = false;
            var p = new Vector(-intersector.Y, intersector.X);
            var h = ((-this.Start + intersector.Start) * p) / (this * p);
            intersection = h >= 0 && h <= 1;
            if (intersection)
            {
                p = new Vector(-this.Y, this.X);
                h = ((-intersector.Start + this.Start) * p) / (intersector * p);
                intersection = h >= 0 && h <= 1;
            }
            return intersection;
        }

        public virtual Vector PointOfIntersection2D(Line intersector)
        {
            var p = new Vector(-intersector.Y, intersector.X);
            var h = ((-this.Start + intersector.Start) * p) / (this * p);
            return this.Start + this * h;
        }

        public virtual Vector PointAtSection(float t)
        {
            var u = 1 - t;
            return (this.Start * u) + (this.End * t);
        }

        public virtual Tuple<Line, Line> SplitAt(float t)
        {
            var mid = this.PointAtSection(t);
            return new Tuple<Line, Line>(new Line(this.Start, mid), new Line(mid, this.End));
        }

        public virtual Vector AsVector()
        {
            return this.End - this.Start;
        }

        public override int GetHashCode()
        {
            return this.Start.GetHashCode() + this.End.GetHashCode() * 199933;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Line;
            return other != null && this.GetHashCode() == other.GetHashCode();
        }

        public static Vector operator +(Line addend1, Vector addend2)
        {
            return addend1.AsVector().Add(addend2);
        }

        public static Vector operator -(Line minuend, Vector subtrahend)
        {
            return minuend.AsVector().Subtract(subtrahend);
        }

        public static Vector operator *(Line multiplicand, float multiplier)
        {
            return multiplicand.AsVector().Multiply(multiplier);
        }

        public static Vector operator /(Line dividend, float divisor)
        {
            return dividend.AsVector().Divide(divisor);
        }

        public static Vector operator *(float multiplier, Line multiplicand)
        {
            return multiplicand.AsVector().Multiply(multiplier);
        }

        public static Vector operator /(float divisor, Line dividend)
        {
            return dividend.AsVector().Divide(divisor);
        }

        public static float operator *(Line multiplicand, Vector multiplier)
        {
            return multiplicand.AsVector().Dot(multiplier);
        }

        public static Line operator -(Line origin)
        {
            return new Line(origin.Start, -origin.End);
        }

        public static bool operator ==(Line origin, Vector other)
        {
            return origin != null && origin.AsVector().Equals(other);
        }

        public static bool operator !=(Line origin, Vector other)
        {
            return origin != null && !origin.AsVector().Equals(other);
        }

        public static implicit operator Vector(Line origin)
        {
            return origin.AsVector();
        }
    }
}
