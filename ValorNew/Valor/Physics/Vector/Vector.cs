using System.Drawing;

namespace Valor.Physics.Vector
{
    using System;

    using Microsoft.Xna.Framework;

    public class Vector
    {
        private const float Epsilon = 0.00001f;

        protected float PLength;

        public float X { get; protected set; }

        public float Y { get; protected set; }

        public float Z { get; protected set; }

        public float Angle
        {
            get { return (float)Math.Atan2(Y, X); }
        }

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

        public Vector(Point pointF)
            : this(pointF.X, pointF.Y) { }

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

        public virtual Point ToPoint()
        {
            return new Point((int)this.X, (int)this.Y);
        }

        public virtual Vector3 ToVector3()
        {
            return new Vector3(this.X, this.Y, this.Z);
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

        internal Vector Clamp(Vector minValues, Vector maxValues, out int bx, out int by, out int bz)
        {
            float nx = X, ny = Y, nz = Z;
            bx = by = bz = 0;
            if (X < minValues.X)
            {
                nx = minValues.X;
                bx = -1;
            }
            else if (X > maxValues.X)
            {
                nx = maxValues.X;
                bx = 1;
            }
            if (Y < minValues.Y)
            {
                ny = minValues.Y;
                by = -1;
            }
            else if (Y > maxValues.Y)
            {
                ny = maxValues.Y;
                by = 1;
            }
            if (Z < minValues.Z)
            {
                nz = minValues.Z;
                bz = -1;
            }
            else if (Z > maxValues.Z)
            {
                nz = maxValues.Z;
                bz = 1;
            }
            return new Vector(nx, ny, nz);
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

        public static Vector operator +(Vector addend1, Point addend2)
        {
            return new Vector(addend1.X + addend2.X, addend1.Y + addend2.Y);
        }

        public static Vector operator -(Vector minuend, Point subtrahend)
        {
            return new Vector(minuend.X - subtrahend.X, minuend.Y - subtrahend.Y);
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

        public static implicit operator Point(Vector original)
        {
            return new Point((int)original.X, (int)original.Y);
        }
    }
}
