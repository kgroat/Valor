namespace ValorTests
{
    using System;

    using NUnit.Framework;

    using Valor.Physics.Vector;

    [TestFixture]
    public class LineTests
    {
        public Line Line1 { get; set; }

        public Line Line2 { get; set; }

        public Line Line3 { get; set; }

        public Line Line4 { get; set; }

        [SetUp]
        public void Setup()
        {
            Line1 = new Line(0, 0, 4, 3);
            Line2 = new Line(new Vector(4, 0), new Vector(0, 3));
            Line3 = new Line(1, 1, 5, 4);
            Line4 = new Line(9, 5, 5, 8);
        }

        [Test]
        public void X_IsDifferenceInComponents()
        {
            Assert.IsTrue(Line1.X == Line1.End.X - Line1.Start.X && Line2.X == Line2.End.X - Line2.Start.X);
        }

        [Test]
        public void Y_IsDifferenceInComponents()
        {
            Assert.IsTrue(Line1.Y == Line1.End.Y - Line1.Start.Y && Line2.Y == Line2.End.Y - Line2.Start.Y);
        }

        [Test]
        public void Length_IsLengthOfDifferenceOfComponents()
        {
            var x = Line1.End.X - Line1.Start.X;
            var y = Line1.End.Y - Line1.Start.Y;
            var length = Line1.Length;
            Assert.IsTrue(length == Math.Sqrt(x*x + y*y));
        }

        [Test]
        public void Intersects_ReturnsTrueForIntersectingLines()
        {
            Assert.IsTrue(Line1.Intersects(Line2) && Line2.Intersects(Line1));
        }

        [Test]
        public void Intersects_ReturnsFalseForParallelLines()
        {
            Assert.IsFalse(Line1.Intersects(Line3) || Line3.Intersects(Line1));
        }

        [Test]
        public void Intersects_ReturnsFalseForLinesTooFarApart()
        {
            Assert.IsFalse(Line1.Intersects(Line4) || Line4.Intersects(Line1));
        }

        [Test]
        public void PointOfIntersection_ReturnsVectorAtWhichLinesIntersect()
        {
            var intersection1 = Line1.PointOfIntersection(Line2);
            var intersection2 = Line2.PointOfIntersection(Line1);
            Assert.IsTrue(intersection1 == intersection2 && intersection1.X == 2 && intersection1.Y == 1.5f);
        }

        [Test]
        public void PointAtSection_ReturnsPointAtFractionOfLength()
        {
            const float fraction = 0.25f;
            var intersection = Line3.PointAtSection(0.25f);
            Assert.IsTrue(intersection.X == Line3.Start.X + fraction * Line3.X && intersection.Y == Line3.Start.Y + fraction * Line3.Y);
        }

        [Test]
        public void SplitAt_ReturnsLineSegmentsSplitByFraction()
        {
            const float fraction = 0.25f;
            var split = Line3.SplitAt(0.25f);
            Assert.IsTrue(split.Item1.X == fraction * Line3.X && split.Item2.X == (1 - fraction) * Line3.X && split.Item1.Y == fraction * Line3.Y && split.Item2.Y == (1 - fraction) * Line3.Y);
        }

        [Test]
        public void AdditionWithVector_WorksInBothDirections()
        {
            var vec = new Vector(3.2f, 5.3f);
            var sum1 = Line1 + vec;
            var sum2 = vec + Line1;
            Assert.IsTrue(sum1 == sum2);
        }

        [Test]
        public void SubtractionWithVector_WorksInBothDirections()
        {
            var vec = new Vector(3.5f, 2.3f);
            var diff1 = Line1 - vec;
            var diff2 = vec - Line1;
            Assert.IsTrue(diff1 == -diff2);
        }

        [Test]
        public void DotWithVector_WorksInBothDirections()
        {
            var vec = new Vector(4.5f, 2.7f);
            var prod1 = Line1 * vec;
            var prod2 = vec * Line1;
            Assert.IsTrue(prod1 == prod2);
        }

        [Test]
        public void Negation_ReturnsInverseVector()
        {
            var vec = (Vector)Line1;
            var inv = -Line1;
            Assert.IsTrue(inv == -vec);
        }

        [Test]
        public void Multiplication_MultipliesBothComponents()
        {
            const float mult = 2.1f;
            var product = this.Line1 * mult;
            Assert.IsTrue(product.X == this.Line1.X * mult && product.Y == this.Line1.Y * mult);
        }

        [Test]
        public void Division_DividesBothComponents()
        {
            const float div = 2.1f;
            var quotient = this.Line1 / div;
            Assert.IsTrue(quotient.X == this.Line1.X / div && quotient.Y == this.Line1.Y / div);
        }

        [Test]
        public void Multiplication_WorksTheSameForwardsAsBackwards()
        {
            const float mult = 2.1f;
            var product1 = this.Line1 * mult;
            var product2 = mult * this.Line1;
            Assert.IsTrue(product1 == product2);
        }

        [Test]
        public void Division_WorksTheSameForwardsAsBackwards()
        {
            const float div = 2.1f;
            var quotient1 = this.Line1 / div;
            var quotient2 = div / this.Line1;
            Assert.IsTrue(quotient1 == quotient2);
        }
    }
}
