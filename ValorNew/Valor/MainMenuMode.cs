using System.Collections.Generic;
using Valor.Physics.Particles;
using Valor.Physics.Vector;

namespace Valor
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using SharpDX.Direct2D1;

    public class MainMenuMode : GameMode
    {
        private float particlesToAdd;
        SpriteBatch _spriteBatch;

        public ParticleEngine Particles { get; set; }

        //public IList<Ship> Ships { get; set; } 

        public MainMenuMode(ContentManager content) : base(content)
        {
            Particles = new ParticleEngine();
            ////Ships = new List<Ship>();
            ////Ships.Add(new Ship(new RotatableImage(){ToRender = ValorEngine.Packages["main"].LoadScript("Ship")}));
            ////Ships[0].PositionOnScreen = new Vector(ValorEngine.Width / 2, ValorEngine.Height / 2);
            ////((SourceImage)((AnimableSection)(Ships[0].ToRender.ToRender)).ToRender).Scale = .5f;
        }

        public override void Init(GraphicsDevice graphicsDevice)
        {
            base.Init(graphicsDevice);
            var starField = Particles.Particles;
            for (int i = 0; i < Valor.Engine.Width * 2.5f; i++)
            {
                starField.Add(CreateParticleAtSquareZ((float)(GraphicsHelper.Rand.NextDouble() * Valor.Engine.Width), (float)(GraphicsHelper.Rand.NextDouble() * Valor.Engine.Height)));
            }
            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
        }

        public override void Render(int width, int height)
        {
            GraphicsDevice.Clear(Color.Black);
            this._spriteBatch.Begin();
            this._spriteBatch.Draw(new Texture2D(this.GraphicsDevice, 1, 1), new Rectangle(0, 0, width, height), Color.Black);
            foreach (var particle in Particles.Particles)
            {
                particle.Render(new Point(0, 0));
            }
            this._spriteBatch.End();
            ////g.InterpolationMode = InterpolationMode.Anisotropic;
            ////foreach (var ship in Ships)
            ////{
            ////    ship.Render(g, ship.PositionOnScreen);
            ////}
            ////g.InterpolationMode = InterpolationMode.NearestNeighbor;
        }

        public override void Step(GameTime time)
        {
            var ms = (float)time.ElapsedGameTime.TotalSeconds;
            particlesToAdd += ms * 60 * Valor.Engine.Height / 800;
            var x = Valor.Engine.Width;
            for (int i = 1; i <= particlesToAdd; i++)
            {
                var y = Valor.Engine.Height * (float)GraphicsHelper.Rand.NextDouble();
                var particle = CreateParticleAt(x, y);
                Particles.Particles.Add(particle);
            }
            Particles.Step(time);
            particlesToAdd %= 1;
            ////foreach (var ship in Ships)
            ////{
            ////    MoveShip(ship);
            ////}
        }

        ////private void MoveShip(Ship ship)
        ////{
        ////    var r = (float)(GraphicsHelper.Rand.NextDouble()-.5f)*300;
        ////    ship.Theta += (r-ship.Theta)/900;
        ////}

        public Particle CreateParticleAtSquareZ(float x, float y)
        {
            var r = GraphicsHelper.Rand.NextDouble();
            r = 1-(r*r*r);
            var z = (float)r / 10 + .000001f;
            return CreateParticleAt(x, y, z);
        }

        public Particle CreateParticleAt(float x, float y)
        {
            var r = GraphicsHelper.Rand.NextDouble();
            var z = (float)r / 10 + .000001f;
            return CreateParticleAt(x, y, z);
        }

        public Particle CreateParticleAt(float x, float y, float z)
        {
            var c = GraphicsHelper.RandomColor();
            var vel = 1 / z;
            var particle = new LineParticle(c, this.GraphicsDevice, new Vector(x, y, z), new Vector(-vel, 0, 0), null);
            particle.Destruction = new GenericDestruction(f => particle.Position.X <= -2 * particle.Position.Z);
            return particle;
        }
    }
}
