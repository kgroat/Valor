namespace Valor.Physics.Vector
{
    public class Ray
    {

        public Vector Start { get; protected set; }

        public Vector Direction { get; protected set; }

        public Ray(Vector start, Vector dir)
        {
            this.Start = start;
            this.Direction = dir.Normalize();
        }

        public Ray(float startX, float startY, float dirX, float dirY)
            : this(new Vector(startX, startY), new Vector(dirX, dirY)) { }

        public Ray(float startX, float startY, float startZ, float dirX, float dirY, float dirZ)
            : this(new Vector(startX, startY, startZ), new Vector(dirX, dirY, dirZ)) { }
        
        public override int GetHashCode()
        {
            return this.Start.GetHashCode() + this.Direction.GetHashCode() * 199933;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Ray;
            return other != null && this.GetHashCode() == other.GetHashCode();
        }
    }
}
