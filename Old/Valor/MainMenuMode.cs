using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Llamanim.Anim;
using Valor.Physics.Particles;
using Valor.Physics.Vector;

namespace Valor
{
    public class MainMenuMode : GameMode
    {
        private float particlesToAdd;

        public ParticleEngine Particles { get; set; }

        public IList<Ship> Ships { get; set; } 

        public MainMenuMode()
        {
            Particles = new ParticleEngine();
            var starField = Particles.Particles;
            for (int i = 0; i < ValorEngine.Width * 2.5f; i++)
            {
                starField.Add(CreateParticleAtSquareZ((float)(GraphicsHelper.Rand.NextDouble() * ValorEngine.Width), (float)(GraphicsHelper.Rand.NextDouble() * ValorEngine.Height)));
            }
            Ships = new List<Ship>();
            Ships.Add(new Ship(new RotatableImage(){ToRender = ValorEngine.Packages["main"].LoadScript("Ship")}));
            Ships[0].PositionOnScreen = new Vector(ValorEngine.Width / 2, ValorEngine.Height / 2);
            ((SourceImage)((AnimableSection)(Ships[0].ToRender.ToRender)).ToRender).Scale = .5f;
        }

        public override void Render(Graphics g, float width, float height)
        {
            g.FillRectangle(Brushes.Black, 0, 0, width, height);
            foreach (var particle in Particles.Particles)
            {
                particle.Render(g, new Point(0, 0));
            }
            g.InterpolationMode = InterpolationMode.High;
            foreach (var ship in Ships)
            {
                ship.Render(g, ship.PositionOnScreen);
            }
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
        }

        public override void Step(float time)
        {
            particlesToAdd += time * 60 * ValorEngine.Height / 800;
            var x = ValorEngine.Width;
            for (int i = 1; i <= particlesToAdd; i++)
            {
                var y = ValorEngine.Height * (float)GraphicsHelper.Rand.NextDouble();
                var particle = CreateParticleAt(x, y);
                Particles.Particles.Add(particle);
            }
            Particles.Step(time);
            particlesToAdd %= 1;
            foreach (var ship in Ships)
            {
                MoveShip(ship);
            }
        }

        private void MoveShip(Ship ship)
        {
            var r = (float)(GraphicsHelper.Rand.NextDouble()-.5f)*300;
            ship.Theta += (r-ship.Theta)/900;
        }

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
            var particle = new LineParticle(new Pen(c), new Vector(x, y, z), new Vector(-vel, 0, 0), null);
            particle.Destruction = new GenericDestruction(f => particle.Position.X <= -2 * particle.Position.Z);
            return particle;
        }
    }
}
