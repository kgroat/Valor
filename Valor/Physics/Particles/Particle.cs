using System;
using System.Collections.Generic;
using System.Drawing;

namespace Valor.Physics.Particles
{
    using Valor.Physics.Vector;

    public abstract class Particle
    {
        public virtual Vector Position { get; set; }

        public virtual Vector Velocity { get; set; }

        public virtual Destruction Destruction { get; set; }

        public bool IsDestryed
        {
            get { return Destruction.IsDestroyed; }
        }

        protected Particle(Vector position, Vector velocity, Destruction destruction)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Destruction = destruction;
        }

        public abstract void Render(Graphics g, Point p);

        public bool Step(float time)
        {
            return Destruction.TryDestroy(time);
        }
    }

    public class ZComparer : IComparer<Particle>
    {
        public int Compare(Particle x, Particle y)
        {
            return Math.Sign(y.Position.Z - x.Position.Z);
        }
    }

    public abstract class Destruction
    {
        public bool IsDestroyed { get; set; }

        public bool TryDestroy(float time)
        {
            return IsDestroyed = DestructionFunction(time);
        }

        public abstract bool DestructionFunction(float time);
    }

    public class TimeDestruction : Destruction
    {
        public float Lifetime { get; set; }

        public float CurrentTime { get; set; }

        public override bool DestructionFunction(float time)
        {
            CurrentTime += time;
            return CurrentTime >= Lifetime;
        }
    }

    public class GenericDestruction : Destruction
    {
        public Func<float, bool> Function { get; set; }

        public GenericDestruction() { }

        public GenericDestruction(Func<float, bool> function)
        {
            this.Function = function;
        }

        public override bool DestructionFunction(float time)
        {
            return this.Function(time);
        }
    }
}
