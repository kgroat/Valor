namespace ValorTests
{
    using System.Drawing;

    using NUnit.Framework;

    using Valor;

    [TestFixture]
    public class GraphicsHelperTests
    {
        [Test]
        public void RandomColor_ReturnsValidColor()
        {
            var col = GraphicsHelper.RandomColor();
            Assert.IsNotNull(col);
        }

        [Test]
        public void Clamp_ReturnsSameNumberIfWithinRange()
        {
            var input = GraphicsHelper.Rand.Next();
            var output = GraphicsHelper.Clamp(input - 1, input, input + 1);
            Assert.AreEqual(input, output);
        }

        [Test]
        public void Clamp_ReturnsMinIfBelowRange()
        {
            var input = GraphicsHelper.Rand.Next();
            var output = GraphicsHelper.Clamp(input + 1, input, input + 2);
            Assert.AreEqual(input + 1, output);
        }

        [Test]
        public void Clamp_ReturnsMaxIfAboveRange()
        {
            var input = GraphicsHelper.Rand.Next();
            var output = GraphicsHelper.Clamp(input - 2, input, input - 1);
            Assert.AreEqual(input - 1, output);
        }

        [Test]
        public void CreateOpposite_ReturnsComplementaryColor()
        {
            var input = GraphicsHelper.RandomColor();
            var output = GraphicsHelper.CreateOpposite(input);
            Assert.IsTrue(input.R == 255 - output.R && input.G == 255 - output.G && input.B == 255 - output.B);
        }

        [Test]
        public void Rotate_ReturnsTernaryComplement()
        {
            var input = GraphicsHelper.RandomColor();
            var output = GraphicsHelper.Rotate(input);
            Assert.IsTrue(input.R == output.B && input.G == output.R && input.B == output.G);
        }

        [Test]
        public void Step_ReturnsColorOneGreater()
        {
            var input = Color.FromArgb(255, 0, 0);
            var output = GraphicsHelper.Step(input);
            Assert.IsTrue(output.R == 255 && output.G == 1 && output.B == 0);
        }

        [Test]
        public void Step_WithNegativeOneSize_ReturnsColorOneLess()
        {
            var input = Color.FromArgb(255, 0, 0);
            var output = GraphicsHelper.Step(input, -1);
            Assert.IsTrue(output.R == 255 && output.G == 0 && output.B == 1);
        }

        [Test]
        public void Step_WithNonRotableColor_ReturnsExactSameColor()
        {
            var input = Color.FromArgb(128, 128, 128);
            var output = GraphicsHelper.Step(input);
            Assert.IsTrue(output.R == 128 && output.G == 128 && output.B == 128);
        }
    }
}
