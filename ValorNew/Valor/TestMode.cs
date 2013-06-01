namespace Valor
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class TestMode : GameMode
    {
        public static readonly double FLIP_RATE = 60;
        public static readonly int CYCLE_RATE = 60;

        private readonly Texture2D tex;

        private SpriteBatch spriteBatch;

        public TestMode(ContentManager content) : base(content)
        {
            tex = content.Load<Texture2D>("Mandelbrot Render 16");
        }

        public override void Init(GraphicsDevice graphicsDevice)
        {
            base.Init(graphicsDevice);
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
        }

        public override void Render(int width, int height)
        {
            var g = this.spriteBatch;
            g.Begin();
            g.Draw(tex, new Rectangle(0, 0, width, height), Color.White);
            g.End();
        }

        public override void Step(GameTime time)
        {
            //Image.C3 = GraphicsHelper.Step(Image.C3, CYCLE_RATE);
        }
    }
}
