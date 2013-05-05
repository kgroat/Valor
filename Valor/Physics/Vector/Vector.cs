namespace Valor.Physics.Vector
{
    using System;

    public class Vector
    {
        private const float Epsilon = 0.00001f;

        protected float PLength;

        public float X { get; protected set; }

        public float Y { get; protected set; }

        public float Z { get; protected set; }

        public float Length
        {
            get { return this.PLength; }
        }

        public Vector()
        {
            this.X = this.Y = this.Z = this.PLength = 0;
        }

        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
            this.PLength = (float)Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public Vector(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.PLength = (float)Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
        }

        public virtual Vector Cross(Vector other)
        {
            var u = this;
            var v = other;
            return new Vector(
                this.Y * other.Z - this.Z * other.Y,
                this.Z * other.X - this.X * other.Z,
                this.X * other.Y - this.Y * other.X);
        }

        public virtual Vector Add(Vector addend)
        {
            return new Vector(this.X + addend.X, this.Y + addend.Y);
        }

        public virtual Vector Subtract(Vector subtrahend)
        {
            return new Vector(this.X - subtrahend.X, this.Y - subtrahend.Y);
        }

        public virtual float Dot(Vector other)
        {
            return this.X * other.X + this.Y * other.Y + this.Z * other.Z;
        }

        public virtual Vector Multiply(float multiplicand)
        {
            return new Vector(this.X * multiplicand, this.Y * multiplicand);
        }

        public virtual Vector Divide(float dividend)
        {
            return new Vector(this.X / dividend, this.Y / dividend);
        }

        public virtual System.Drawing.PointF ToPoint()
        {
            return new System.Drawing.PointF(this.X, this.Y);
        }

        public virtual System.Drawing.SizeF ToSize()
        {
            return new System.Drawing.SizeF(this.X, this.Y);
        }

        public virtual Vector Normalize()
        {
            var ret = this;
            if (this.PLength != 0)
            {
                ret = this / this.PLength;
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return (int)(this.X + this.Y * 199933);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Vector;
            if (other != null)
            {
                return this.GetHashCode() == other.GetHashCode();
            }
            return false;
        }

        public static Vector operator +(Vector addend1, Vector addend2)
        {
            return addend1.Add(addend2);
        }

        public static Vector operator -(Vector minuend, Vector subtrahend)
        {
            return minuend.Subtract(subtrahend);
        }

        public static Vector operator *(Vector multiplicand, float multiplier)
        {
            return multiplicand.Multiply(multiplier);
        }

        public static Vector operator /(Vector dividend, float divisor)
        {
            return dividend.Divide(divisor);
        }

        public static Vector operator *(float multiplicand, Vector multiplier)
        {
            return multiplier.Multiply(multiplicand);
        }

        public static Vector operator /(float dividend, Vector divisor)
        {
            return divisor.Divide(dividend);
        }

        public static float operator *(Vector multiplicand, Vector multiplier)
        {
            return multiplicand.Dot(multiplier);
        }

        public static Vector operator -(Vector origin)
        {
            return new Vector(-origin.X, -origin.Y);
        }

        public static bool operator ==(Vector origin, Vector other)
        {
            if ((object)origin == null) return (object)other == null;
            return origin.Equals(other);
        }

        public static bool operator !=(Vector origin, Vector other)
        {
            if ((object)origin == null) return (object)other != null;
            return !origin.Equals(other);
        }
    }
}
