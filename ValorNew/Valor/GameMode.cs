using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class GameMode
    {
        protected GraphicsDevice GraphicsDevice { get; private set; }

        protected ContentManager Content { get; private set; }

        protected GameMode(ContentManager content)
        {
            this.Content = content;
        }

        public virtual void Init(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
        }

        public abstract void Render(int width, int height);

        public abstract void Step(GameTime time);
    }
}
