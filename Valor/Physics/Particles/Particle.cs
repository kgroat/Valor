namespace Valor.Physics.Particles
{
    using Valor.Physics.Vector;

    public abstract class Particle
    {
        public virtual Vector Position { get; set; }

        public virtual Vector Velocity { get; set; }
    }
}
