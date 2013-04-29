namespace ValorTests
{
    using System;

    using NUnit.Framework;

    using Valor.Physics.Vector;

    [TestFixture]
    public class VectorTests
    {
        public Vector Vector1 { get; set; }
        public Vector Vector2 { get; set; }

        [SetUp]
        public void Setup()
        {
            this.Vector1 = new Vector(2.5f, 3.6f);
            this.Vector2 = new Vector(1.3f, 4.2f);
        }

        [Test]
        public void EmptyConstructor_ReturnsZeroVector()
        {
            var zero = new Vector();
            Assert.IsTrue(zero.X == 0 && zero.Y == 0 && zero.Length == 0);
        }

        [Test]
        public void Negation_NegatesBothComponents()
        {
            var negation = -this.Vector1;
            Assert.IsTrue(negation.X == -this.Vector1.X && negation.Y == -this.Vector1.Y);
        }

        [Test]
        public void Addition_AddsBothComponents()
        {
            var sum = this.Vector1 + this.Vector2;
            Assert.IsTrue(sum.X == this.Vector1.X + this.Vector2.X && sum.Y == this.Vector1.Y + this.Vector2.Y);
        }

        [Test]
        public void Subtraction_SubtractsBothComponents()
        {
            var difference = this.Vector1 - this.Vector2;
            Assert.IsTrue(difference.X == this.Vector1.X - this.Vector2.X && difference.Y == this.Vector1.Y - this.Vector2.Y);
        }

        [Test]
        public void Multiplication_MultipliesBothComponents()
        {
            const float mult = 2.1f;
            var product = this.Vector1 * mult;
            Assert.IsTrue(product.X == this.Vector1.X * mult && product.Y == this.Vector1.Y * mult);
        }

        [Test]
        public void Division_DividesBothComponents()
        {
            const float div = 2.1f;
            var quotient = this.Vector1 / div;
            Assert.IsTrue(quotient.X == this.Vector1.X / div && quotient.Y == this.Vector1.Y / div);
        }

        [Test]
        public void Multiplication_WorksTheSameForwardsAsBackwards()
        {
            const float mult = 2.1f;
            var product1 = this.Vector1 * mult;
            var product2 = mult * this.Vector1;
            Assert.IsTrue(product1 == product2);
        }

        [Test]
        public void Division_WorksTheSameForwardsAsBackwards()
        {
            const float div = 2.1f;
            var quotient1 = this.Vector1 / div;
            var quotient2 = div / this.Vector1;
            Assert.IsTrue(quotient1 == quotient2);
        }

        [Test]
        public void Equivalence_ReturnsTrueForTheSameVector()
        {
            Assert.IsTrue(this.Vector1 == this.Vector1);
        }

        [Test]
        public void Equivalence_ReturnsFalseForDifferentVectors()
        {
            Assert.IsFalse(this.Vector1 == this.Vector2);
        }

        [Test]
        public void Equivalence_WorksForNullValues()
        {
            Assert.IsFalse(null == this.Vector2);
        }

        [Test]
        public void Inequivalence_ReturnsFalseForTheSameVector()
        {
            Assert.IsFalse(this.Vector1 != this.Vector1);
        }

        [Test]
        public void Inequivalence_ReturnsTrueForDifferentVectors()
        {
            Assert.IsTrue(this.Vector1 != this.Vector2);
        }

        [Test]
        public void Inequivalence_WorksForNullValues()
        {
            Assert.IsTrue(null != this.Vector1);
        }

        [Test]
        public void CrossProduct_IsOppositeWhenOrderIsReversed()
        {
            var cross1 = this.Vector1.Cross(this.Vector2);
            var cross2 = this.Vector2.Cross(this.Vector1);
            Assert.IsTrue(cross1 == -cross2);
        }

        [Test]
        public void DotProduct_IsTheSameEvenWhenOrderIsReversed()
        {
            var dot1 = this.Vector1 * this.Vector2;
            var dot2 = this.Vector2 * this.Vector1;
            Assert.IsTrue(dot1 == dot2);
        }

        [Test]
        public void Normalize_ReturnsVectorOfLengthOne()
        {
            var normalize = this.Vector1.Normalize();
            Assert.IsTrue(Math.Abs(normalize.Length - 1) < .00001f);
        }

        [Test]
        public void GetHash_ReturnsVectorOfLengthOne()
        {
            var normalize = this.Vector1.Normalize();
            Assert.IsTrue(Math.Abs(normalize.Length - 1) < .00001f);
        }

        [Test]
        public void ToPoint_ReturnsPointfWithSameComponents()
        {
            var point = this.Vector1.ToPoint();
            Assert.IsTrue(point.X == this.Vector1.X && point.Y == this.Vector1.Y);
        }

        [Test]
        public void ToSize_ReturnsSizeWithSameDimensions()
        {
            var size = this.Vector1.ToSize();
            Assert.IsTrue(size.Width == this.Vector1.X && size.Height == this.Vector1.Y);
        }
    }
}
