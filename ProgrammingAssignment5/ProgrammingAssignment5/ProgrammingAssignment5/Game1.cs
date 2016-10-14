using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeddyMineExplosion;
using System.Collections.Generic;
using System;

namespace ProgrammingAssignment5
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

        // mines support
        Texture2D mine;
        List<Mine> mines = new List<Mine>();

        // bears support
        Texture2D teddy;
        List<TeddyBear> bears = new List<TeddyBear>();
        Random rand = new Random();
        int spawTimer;

        // explosions suport
        Texture2D explosion;       
        List<Explosion> explosions = new List<Explosion>();

        // click processing
        bool clickStarted = false;
        bool buttonReleased = true;

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

            // Load the sprites
            teddy = Content.Load<Texture2D>("teddybear");
            mine = Content.Load<Texture2D>("mine");
            explosion = Content.Load<Texture2D>("explosion");

            // Set spaw timer
            spawTimer = rand.Next(1000, 3000);
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

            // Get state of the mouse
            MouseState mouse = Mouse.GetState();

            // Check the click
            if (mouse.LeftButton == ButtonState.Pressed && buttonReleased)
            {
                clickStarted = true;
                buttonReleased = false;
            }
            else if(mouse.RightButton == ButtonState.Released)
            {
                buttonReleased = true;

                // if click finished, add mine
                if (clickStarted)
                {
                    clickStarted = false;
                    Mine newMine = new Mine(mine, mouse.X, mouse.Y);
                    mines.Add(newMine);
                }
            }

            // Update spaw timer
            spawTimer -= gameTime.ElapsedGameTime.Milliseconds;

            // If timer is finished, set a new timer and spaw a new teddy
            if (spawTimer <= 0)
            {
                spawTimer = rand.Next(1000, 3000);
                
                Vector2 velocity = new Vector2(rand.Next(-5,5)/10f,rand.Next(-5,5)/10f);
                TeddyBear newTeddy = new TeddyBear(teddy, velocity, WindowWidth, WindowHeight);
                bears.Add(newTeddy);
            }

            // Update bears
            foreach(TeddyBear bear in bears)
            {
                bear.Update(gameTime);
            }

            // check for collisions
            foreach (Mine mine in mines)
            {
                foreach (TeddyBear teddy in bears)
                {
                    if (teddy.CollisionRectangle.Intersects(mine.CollisionRectangle))
                    {
                        teddy.Active = false;
                        mine.Active = false;

                        Explosion newExplosion = new Explosion(explosion, mine.CollisionRectangle.Center.X, mine.CollisionRectangle.Center.Y);
                        explosions.Add(newExplosion);
                    }
                }
            }

            // Update explosions
            foreach (Explosion exp in explosions)
            {
                exp.Update(gameTime);
            }

            // Remove inactive bears
            for(int i=0; i < bears.Count; i++)
            {
                if (!bears[i].Active) { bears.RemoveAt(i); }
            }

            // Remove inactive mines
            for(int i = 0; i < mines.Count; i++)
            {
                if (!mines[i].Active) { mines.RemoveAt(i); }
            }

            // Remove inactive explosions
            for(int i = 0; i < explosions.Count; i++)
            {
                if (!explosions[i].Playing) { explosions.RemoveAt(i); }
            }

            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // draw each mine
            for(int i=0; i<mines.Count; i++)
            {
                mines[i].Draw(spriteBatch);
            }

            // draw each bear
            for (int i = 0; i < bears.Count; i++)
            {
                bears[i].Draw(spriteBatch);
            }

            // draw each explosion
            foreach(Explosion exp in explosions)
            {
                exp.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
