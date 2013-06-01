using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Valor
{
    using Microsoft.Xna.Framework.Input;

    using Windows.UI.ViewManagement;

    using global::Valor;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Valor : Game
    {
        public static Valor Engine { get; private set; }

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        BasicEffect _primitiveBatch;
        private GameMode _gameMode;

        public GameMode GameMode
        {
            get
            {
                return this._gameMode;
            }
            set
            {
                value.Init(this.GraphicsDevice);
                this._gameMode = value;
            }
        }

        public int Width
        {
            get
            {
                return this.GraphicsDevice.Viewport.Width;
            }
        }

        public int Height
        {
            get
            {
                return this.GraphicsDevice.Viewport.Height;
            }
        }

        public Valor()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Engine = this;
            this._gameMode = new MainMenuMode(this.Content);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            this.GameMode.Init(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputHelper.Keyboard = Keyboard.GetState(PlayerIndex.One);
            InputHelper.Mouse = Mouse.GetState();
            InputHelper.GamePad = GamePad.GetState(PlayerIndex.One);

            if (InputHelper.Keyboard[Keys.Escape] == KeyState.Down)
            {
                this.Exit();
            }

            _gameMode.Step(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 64, 144));
            this._gameMode.Render(this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);
            base.Draw(gameTime);
        }
    }
}
