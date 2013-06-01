namespace ValorTests
{
    using System;

    using NUnit.Framework;

    using Valor.Physics.Vector;

    [TestFixture]
    public class RayTests
    {
        [Test]
        public void Constructor2D_CreatesValidRay()
        {
            const float x = 5, y = 6, dx = 4, dy = 3;
            var ray = new Ray(x, y, dx, dy);
            var dlen = (float)Math.Sqrt(dx * dx + dy * dy);
            Assert.IsTrue(ray.Start.X == x && ray.Start.Y == y && ray.Start.Z == 0 && ray.Direction.X == dx / dlen && ray.Direction.Y == dy / dlen && ray.Direction.Z == 0);
        }

        [Test]
        public void Constructor3D_CreatesValidRay()
        {
            const float x = 5, y = 6, z = 7, dx = 4, dy = 3, dz = 2;
            var ray = new Ray(x, y, z, dx, dy, dz);
            var dlen = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
            Assert.IsTrue(ray.Start.X == x && ray.Start.Y == y && ray.Start.Z == z && ray.Direction.X == dx / dlen && ray.Direction.Y == dy / dlen && ray.Direction.Z == dz / dlen);
        }

        [Test]
        public void Equals_ReturnsTrueForSameRay()
        {
            const float x = 5, y = 6, z = 7, dx = 4, dy = 3, dz = 2;
            var ray1 = new Ray(x, y, z, dx, dy, dz);
            var ray2 = new Ray(x, y, z, dx, dy, dz);
            Assert.IsTrue(ray1.Equals(ray2));
        }

        [Test]
        public void Equals_ReturnsFalseForDifferentRay()
        {
            const float x = 5, y = 6, z = 7, dx = 4, dy = 3, dz = 2;
            var ray1 = new Ray(x, y, z, dx, dy, dz);
            var ray2 = new Ray(z, y, x, dx, dy, dz);
            Assert.IsFalse(ray1.Equals(ray2));
        }
    }
}
