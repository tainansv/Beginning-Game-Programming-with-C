using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ExplodingTeddies;

namespace Lab10
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        const int WindowWidth = 800;
        const int WindowHeight = 600;

        SpriteBatch spriteBatch;
        TeddyBear teddy0;
        TeddyBear teddy1;
        Explosion explosion;

        bool intersect = false;

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

            // create teddys
            teddy0 = new TeddyBear(Content, WindowWidth, WindowHeight, "teddybear0", 
                WindowWidth / 3, WindowHeight / 2, new Vector2(-1, 0));
            teddy1 = new TeddyBear(Content, WindowWidth, WindowHeight, "teddybear1", 
                WindowWidth - WindowWidth/ 3,  WindowHeight / 2, new Vector2(1, 0));

            // create explosion
            explosion = new Explosion(Content,"explosion");
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

            teddy0.Update(gameTime);
            teddy1.Update(gameTime);

            if(teddy0.Active && teddy1.Active &&
                teddy0.DrawRectangle.Intersects(teddy1.DrawRectangle))
            {
                teddy0.Active = false;
                teddy1.Active = false;
                explosion.Play(teddy0.DrawRectangle.X, teddy0.DrawRectangle.Y);
            }
            explosion.Update(gameTime);
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

            teddy0.Draw(spriteBatch);
            teddy1.Draw(spriteBatch);
            explosion.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
