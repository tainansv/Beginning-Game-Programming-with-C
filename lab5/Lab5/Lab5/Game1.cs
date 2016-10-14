using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab5
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public const int WindowWidth = 800;
        public const int WindowHeight = 600;

        SpriteBatch spriteBatch;
        Texture2D teddy0;
        Texture2D teddy1;
        Texture2D teddy2;
        Rectangle rect0;
        Rectangle rect1;
        Rectangle rect2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load sprites
            teddy0 = Content.Load<Texture2D>("teddybear0");
            teddy1 = Content.Load<Texture2D>("teddybear1");
            teddy2 = Content.Load<Texture2D>("teddybear2");

            // draw rectangles
            rect0.Width = teddy0.Width;
            rect0.Height = teddy0.Height;
            rect0.X = 30;
            rect0.Y = 30;

            rect1.Width = teddy1.Width;
            rect1.Height = teddy1.Height;
            rect1.X = 60;
            rect1.Y = 60;

            rect2.Width = teddy2.Width;
            rect2.Height = teddy2.Height;
            rect2.X = 90;
            rect2.Y = 90;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(teddy0, rect0, Color.White);
            spriteBatch.Draw(teddy1, rect1, Color.White);
            spriteBatch.Draw(teddy2, rect2, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
