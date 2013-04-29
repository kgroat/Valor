namespace Valor.Physics.Vector
{
    public class Ray
    {

        public Vector Start { get; protected set; }

        public Vector Direction { get; protected set; }

        public Ray(Vector start, Vector dir)
        {
            this.Start = start;
            this.Direction = dir;
        }

        public Ray(float startX, float startY, float dirX, float dirY)
        {
            this.Start = new Vector(startX, startY);
            this.Direction = new Vector(dirX, dirY).Normalize();
        }

        public override int GetHashCode()
        {
            return this.Start.GetHashCode() + this.Direction.GetHashCode() * 199933;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Ray;
            return other != null && (this.Start == other.Start && this.Direction == other.Direction);
        }
    }
}
