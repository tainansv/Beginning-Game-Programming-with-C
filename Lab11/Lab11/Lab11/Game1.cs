using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ExplodingTeddies;
using System;

namespace Lab11
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
        TeddyBear teddy;
        Explosion explosion;
        Random rand = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

            IsMouseVisible = true;
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

            // create teddy
            Vector2 velocity = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble())/5;
            teddy = new TeddyBear(Content, WindowWidth, WindowHeight, "teddybear", WindowWidth / 2, WindowHeight / 2, velocity);

            // create explosion
            explosion = new Explosion(Content, "explosion");
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

            // update objects
            teddy.Update(gameTime);
            explosion.Update(gameTime);


            // explode teddy with click
            if(Mouse.GetState().LeftButton==ButtonState.Pressed &&
                teddy.DrawRectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y)){

                teddy.Active = false;
                explosion.Play(teddy.DrawRectangle.Center.X,teddy.DrawRectangle.Center.Y);
            }

            // create new teddy with rick click after explode it
            if (teddy.Active == false && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Vector2 velocity = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble()) / 5;
                teddy = new TeddyBear(Content, WindowWidth, WindowHeight, "teddybear", WindowWidth / 2, WindowHeight / 2, velocity);
            }

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

            teddy.Draw(spriteBatch);
            explosion.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
