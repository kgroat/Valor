namespace ValorTests
{
    using NUnit.Framework;

    using Valor;

    [TestFixture]
    public class GraphicsBindingContextTests
    {
        const double Ri = 12.5, Pi = 13.5;

        private ValorEngine engine;

        private ValorForm form;

        private GraphicsBindingContext context;

        [SetUp]
        public void Setup()
        {
            this.engine = new ValorEngine();
            this.form = new ValorForm();
            this.form.Engine = this.engine;
            this.context = new GraphicsBindingContext(this.form, Ri, Pi);
        }

        [Test]
        public void Constructor_GeneratesProperlyPopulatedContext()
        {
            this.context = new GraphicsBindingContext(this.form, Ri, Pi);
            Assert.IsTrue(this.context.Form == this.form && this.context.RenderInterval == Ri && this.context.PhysicsInterval == Pi);
        }

        [Test]
        public void SetRenderInterval_ChangesRenderInterval()
        {
            const double NewRi = 2.2;
            this.context.RenderInterval = NewRi;
            Assert.AreEqual(NewRi, this.context.RenderInterval);
        }

        [Test]
        public void SetPhysicsInterval_ChangesPhysicsInterval()
        {
            const double NewPi = 3.2;
            this.context.PhysicsInterval = NewPi;
            Assert.AreEqual(NewPi, this.context.PhysicsInterval);
        }

        [Test]
        public void Start_ReturnsTrueOnlyFirstTime()
        {
            var ret1 = this.context.Start();
            var ret2 = this.context.Start();
            Assert.IsTrue(ret1 && !ret2);
        }

        [Test]
        public void Stop_ReturnsTrueOnlyFirstTimeAfterStartCalled()
        {
            var ret1 = this.context.Stop();
            var ret2 = this.context.Start();
            var ret3 = this.context.Stop();
            var ret4 = this.context.Stop();
            Assert.IsTrue(!ret1 && ret2 && ret3 && !ret4);
        }
    }
}
