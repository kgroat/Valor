using System;
using System.Collections.Generic;

namespace Valor.Physics.Particles
{
    using Microsoft.Xna.Framework;

    using Physics.Vector;

    public class ParticleEngine
    {
        private readonly List<Particle> particles;

        private Vector minValues;

        private Vector maxValues;

        public IList<Particle> Particles
        {
            get
            {
                particles.Sort(new ZComparer());
                return particles;
            }
        } 

        public float MinX
        {
            get { return minValues.X; } 
            set { minValues = new Vector(value, minValues.Y, minValues.Z); }
        }

        public float MinY
        {
            get { return minValues.Y; }
            set { minValues = new Vector(minValues.X, value, minValues.Z); }
        }

        public float MinZ
        {
            get { return minValues.Z; }
            set { minValues = new Vector(minValues.X, minValues.Y, value); }
        }

        public float MaxX
        {
            get { return maxValues.X; }
            set { maxValues = new Vector(value, maxValues.Y, maxValues.Z); }
        }

        public float MaxY
        {
            get { return maxValues.Y; }
            set { maxValues = new Vector(maxValues.X, value, maxValues.Z); }
        }

        public float MaxZ
        {
            get { return maxValues.Z; }
            set { maxValues = new Vector(maxValues.X, maxValues.Y, value); }
        }

        public Vector Gravity { get; set; }

        public BounceBehavior BounceBehavior { get; set; }

        public ParticleEngine()
            : this(new Vector(Single.NegativeInfinity, Single.NegativeInfinity, Single.NegativeInfinity),
                   new Vector(Single.PositiveInfinity, Single.PositiveInfinity, Single.PositiveInfinity),
                   new Vector(0, 0, 0)) { }

        public ParticleEngine(Vector min, Vector max, Vector gravity)
        {
            particles = new List<Particle>();
            minValues = min;
            maxValues = max;
            Gravity = gravity;
            BounceBehavior = new BounceBehavior();
        }

        public void Step(GameTime time)
        {
            var ms = (float)time.ElapsedGameTime.TotalSeconds;
            foreach (var particle in particles)
            {
                particle.Position += particle.Velocity * ms;
                particle.Velocity += Gravity * ms;
                int bx, by, bz;
                particle.Position = particle.Position.Clamp(minValues, maxValues, out bx, out by, out bz);
                BounceBehavior.Bounce(particle, bx, by, bz);
            }
            for (int i = 0; i < particles.Count; i++)
            {
                if (!particles[i].Step(time)) continue;
                particles.RemoveAt(i);
                i--;
            }
        }
    }

    public class BounceBehavior
    {
        public enum BounceType
        {
            Reverse, ReverseWithDrag, Stop
        }

        public BounceType MinX { get; set; }

        public BounceType MinY { get; set; }

        public BounceType MinZ { get; set; }

        public BounceType MaxX { get; set; }

        public BounceType MaxY { get; set; }

        public BounceType MaxZ { get; set; }

        public BounceBehavior()
        {
            MinX = MinY = MinZ = MaxX = MaxY = MaxZ = BounceType.ReverseWithDrag;
        }

        public void Bounce(Particle p, int bx, int by, int bz)
        {
            float dx = p.Velocity.X, dy = p.Velocity.Z, dz = p.Velocity.Z;
            if (bx < 0)
            {
                switch (MinX)
                {
                    case BounceType.Reverse:
                        dx = -dx;
                        break;
                    case BounceType.ReverseWithDrag:
                        dx = -dx*.9f;
                        break;
                    case BounceType.Stop:
                        dx = 0;
                        break;
                }
            }
            else if (bx > 0)
            {
                switch (MaxX)
                {
                    case BounceType.Reverse:
                        dx = -dx;
                        break;
                    case BounceType.ReverseWithDrag:
                        dx = -dx * .9f;
                        break;
                    case BounceType.Stop:
                        dx = 0;
                        break;
                }
            }

            if (by < 0)
            {
                switch (MinY)
                {
                    case BounceType.Reverse:
                        dy = -dy;
                        break;
                    case BounceType.ReverseWithDrag:
                        dy = -dy * .9f;
                        break;
                    case BounceType.Stop:
                        dy = 0;
                        break;
                }
            }
            else if (by > 0)
            {
                switch (MaxY)
                {
                    case BounceType.Reverse:
                        dy = -dy;
                        break;
                    case BounceType.ReverseWithDrag:
                        dy = -dy * .9f;
                        break;
                    case BounceType.Stop:
                        dy = 0;
                        break;
                }
            }

            if (bz < 0)
            {
                switch (MinZ)
                {
                    case BounceType.Reverse:
                        dz = -dz;
                        break;
                    case BounceType.ReverseWithDrag:
                        dz = -dz * .9f;
                        break;
                    case BounceType.Stop:
                        dz = 0;
                        break;
                }
            }
            else if (bz > 0)
            {
                switch (MaxZ)
                {
                    case BounceType.Reverse:
                        dz = -dz;
                        break;
                    case BounceType.ReverseWithDrag:
                        dz = -dz * .9f;
                        break;
                    case BounceType.Stop:
                        dz = 0;
                        break;
                }
            }

            p.Velocity = new Vector(dx, dy, dz);
        }
    }
}
