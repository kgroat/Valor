using System;
using System.Collections.Generic;
using System.Drawing;

namespace Valor.Physics.Particles
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Vector;

    public abstract class Particle
    {
        protected virtual GraphicsDevice GraphicsDevice { get; set; }

        public virtual Vector Position { get; set; }

        public virtual Vector Velocity { get; set; }

        public virtual Destruction Destruction { get; set; }

        public bool IsDestryed
        {
            get { return Destruction.IsDestroyed; }
        }

        protected Particle(GraphicsDevice graphicsDevice, Vector position, Vector velocity, Destruction destruction)
        {
            this.GraphicsDevice = graphicsDevice;
            this.Position = position;
            this.Velocity = velocity;
            this.Destruction = destruction;
        }

        public abstract void Render(Point p);

        public bool Step(GameTime time)
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

        public bool TryDestroy(GameTime time)
        {
            return IsDestroyed = DestructionFunction(time);
        }

        public abstract bool DestructionFunction(GameTime time);
    }

    public class TimeDestruction : Destruction
    {
        public TimeSpan Lifetime { get; set; }

        public TimeSpan CurrentTime { get; set; }

        public override bool DestructionFunction(GameTime time)
        {
            CurrentTime += time.ElapsedGameTime;
            return CurrentTime >= Lifetime;
        }
    }

    public class GenericDestruction : Destruction
    {
        public Func<GameTime, bool> Function { get; set; }

        public GenericDestruction() { }

        public GenericDestruction(Func<GameTime, bool> function)
        {
            this.Function = function;
        }

        public override bool DestructionFunction(GameTime time)
        {
            return this.Function(time);
        }
    }
}
