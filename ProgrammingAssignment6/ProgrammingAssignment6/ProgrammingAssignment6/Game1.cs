using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaCards;

namespace ProgrammingAssignment6
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        // max valid blockjuck score for a hand
        const int MaxHandValue = 21;

        // deck and hands
        Deck deck;
        List<Card> dealerHand = new List<Card>();
        List<Card> playerHand = new List<Card>();

        // hand placement
        const int TopCardOffset = 100;
        const int HorizontalCardOffset = 150;
        const int VerticalCardSpacing = 125;

        // messages
        SpriteFont messageFont;
        const string ScoreMessagePrefix = "Score: ";
        Message playerScoreMessage;
        Message dealerScoreMessage;
        Message winnerMessage;
		List<Message> messages = new List<Message>();

        // message placement
        const int ScoreMessageTopOffset = 25;
        const int HorizontalMessageOffset = HorizontalCardOffset;
        Vector2 winnerMessageLocation = new Vector2(WindowWidth / 2,
            WindowHeight / 2);

        // menu buttons
        Texture2D quitButtonSprite;
        List<MenuButton> menuButtons = new List<MenuButton>();

        // menu button placement
        const int TopMenuButtonOffset = TopCardOffset;
        const int QuitMenuButtonOffset = WindowHeight - TopCardOffset;
        const int HorizontalMenuButtonOffset = WindowWidth / 2;
        const int VeryicalMenuButtonSpacing = 125;

        // use to detect hand over when player and dealer didn't hit
        bool playerHit = false;
        bool dealerHit = false;

        // game state tracking
        static GameState currentState = GameState.WaitingForPlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set resolution and show mouse
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

            // create and shuffle deck
            deck = new Deck(Content, WindowHeight / 2, WindowWidth / 2);
            deck.Shuffle();

            // first player card
            Card card1Player = deck.TakeTopCard();
            card1Player.FlipOver();
            card1Player.X = HorizontalCardOffset;
            card1Player.Y = TopCardOffset;
            playerHand.Add(card1Player);

            // first dealer card
            Card card1Dealer = deck.TakeTopCard();
            card1Dealer.X = WindowWidth - HorizontalCardOffset;
            card1Dealer.Y = TopCardOffset;
            dealerHand.Add(card1Dealer);

            // second player card
            Card card2Player = deck.TakeTopCard();
            card2Player.FlipOver();
            card2Player.X = HorizontalCardOffset;
            card2Player.Y = TopCardOffset+VerticalCardSpacing;
            playerHand.Add(card2Player);

            // second dealer card
            Card card2Dealer = deck.TakeTopCard();
            card2Dealer.FlipOver();
            card2Dealer.X = WindowWidth - HorizontalCardOffset;
            card2Dealer.Y = TopCardOffset + VerticalCardSpacing;
            dealerHand.Add(card2Dealer);

            // load sprite font, create message for player score and add to list
            messageFont = Content.Load<SpriteFont>(@"fonts\Arial24");
            playerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(playerHand).ToString(),
                messageFont,
                new Vector2(HorizontalMessageOffset, ScoreMessageTopOffset));
            messages.Add(playerScoreMessage);

            // load quit button sprite for later use
			quitButtonSprite = Content.Load<Texture2D>(@"graphics\quitbutton");

            // create hit button and add to list
            Vector2 centerHit = new Vector2(HorizontalMenuButtonOffset, VeryicalMenuButtonSpacing);
            MenuButton hitButton = new MenuButton(Content.Load<Texture2D>(@"graphics/hitbutton"), centerHit, GameState.PlayerHitting);
            menuButtons.Add(hitButton);

            // create stand button and add to list
            Vector2 centerStand = new Vector2(HorizontalMenuButtonOffset, VeryicalMenuButtonSpacing+VeryicalMenuButtonSpacing);
            MenuButton standButton = new MenuButton(Content.Load<Texture2D>(@"graphics/standbutton"), centerStand, GameState.WaitingForDealer);
            menuButtons.Add(standButton);

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

            
            // update menu buttons as appropriate
            if (currentState == GameState.WaitingForPlayer ||
                currentState == GameState.DisplayingHandResults)
            {
                foreach(MenuButton btn in menuButtons)
                {
                    btn.Update(Mouse.GetState());
                }
            }


            // game state-specific processing
            
            switch (currentState)
            {
                case GameState.PlayerHitting:
                    Card cardPlayer = deck.TakeTopCard();
                    cardPlayer.FlipOver();
                    cardPlayer.X = HorizontalCardOffset;
                    cardPlayer.Y = playerHand.Count * TopCardOffset + VerticalCardSpacing;
                    playerHand.Add(cardPlayer);
                    playerScoreMessage.Text = ScoreMessagePrefix + GetBlockjuckScore(playerHand).ToString();
                    currentState = GameState.WaitingForDealer;
                    playerHit = true;
                    break;

                case GameState.WaitingForDealer:
                    if (GetBlockjuckScore(dealerHand) <= 16)
                    {
                        currentState = GameState.DealerHitting;
                    }
                    else
                    {
                        currentState = GameState.CheckingHandOver;
                    }
                    break;

                case GameState.DealerHitting:
                    Card cardDealer = deck.TakeTopCard();
                    cardDealer.X = WindowWidth - HorizontalCardOffset;
                    cardDealer.Y = dealerHand.Count * TopCardOffset + VerticalCardSpacing;
                    cardDealer.FlipOver();
                    dealerHand.Add(cardDealer);
                    currentState = GameState.CheckingHandOver;
                    dealerHit = true;
                    break;

                case GameState.CheckingHandOver:
                    if (GetBlockjuckScore(playerHand) >= MaxHandValue ||
                        GetBlockjuckScore(dealerHand) >= MaxHandValue ||
                        (!dealerHit && !playerHit))
                    {
                        string winnerText;
                        if (GetBlockjuckScore(playerHand) == GetBlockjuckScore(dealerHand))
                        {
                            winnerText = "Tie!";
                        }
                        else if (GetBlockjuckScore(playerHand) <= MaxHandValue && GetBlockjuckScore(dealerHand) > MaxHandValue ||
                            GetBlockjuckScore(playerHand) <= MaxHandValue && GetBlockjuckScore(playerHand) > GetBlockjuckScore(dealerHand))
                        {
                            winnerText = "Player Won!";
                        }
                        else
                        {
                            winnerText = "Dealer Won!";
                        }

                        winnerMessage = new Message(winnerText, messageFont, winnerMessageLocation);
                        messages.Add(winnerMessage);

                        dealerHand[0].FlipOver();

                        dealerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(dealerHand).ToString(), 
                            messageFont, new Vector2(WindowWidth - HorizontalMessageOffset, ScoreMessageTopOffset));
                        messages.Add(dealerScoreMessage);

                        menuButtons.Clear();
                        Vector2 centerQuit = new Vector2(HorizontalMenuButtonOffset, WindowHeight - VeryicalMenuButtonSpacing);
                        MenuButton quitButton = new MenuButton(Content.Load<Texture2D>(@"graphics/quitbutton"), centerQuit, GameState.Exiting);
                        menuButtons.Add(quitButton);

                        currentState = GameState.DisplayingHandResults;

                    }
                    else
                    {
                        currentState = GameState.WaitingForPlayer;
                        dealerHit = false;
                        playerHit = false;
                    }
                    break;

                case GameState.Exiting:
                    
                    this.Exit();
                    break;

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Goldenrod);
						
            spriteBatch.Begin();

            // draw hands
            for(int i = 0; i < playerHand.Count; i++)
            {
                playerHand[i].Draw(spriteBatch);
            }

            for(int i = 0; i < dealerHand.Count; i++)
            {
                dealerHand[i].Draw(spriteBatch);
            }

            // draw messages
            for(int i = 0; i < messages.Count; i++)
            {
                messages[i].Draw(spriteBatch);
            }

            // draw menu buttons
            for(int i = 0; i < menuButtons.Count; i++)
            {
                menuButtons[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Calculates the Blockjuck score for the given hand
        /// </summary>
        /// <param name="hand">the hand</param>
        /// <returns>the Blockjuck score for the hand</returns>
        private int GetBlockjuckScore(List<Card> hand)
        {
            // add up score excluding Aces
            int numAces = 0;
            int score = 0;
            foreach (Card card in hand)
            {
                if (card.Rank != Rank.Ace)
                {
                    score += GetBlockjuckCardValue(card);
                }
                else
                {
                    numAces++;
                }
            }

            // if more than one ace, only one should ever be counted as 11
            if (numAces > 1)
            {
                // make all but the first ace count as 1
                score += numAces - 1;
                numAces = 1;
            }

            // if there's an Ace, score it the best way possible
            if (numAces > 0)
            {
                if (score + 11 <= MaxHandValue)
                {
                    // counting Ace as 11 doesn't bust
                    score += 11;
                }
                else
                {
                    // count Ace as 1
                    score++;
                }
            }

            return score;
        }

        /// <summary>
        /// Gets the Blockjuck value for the given card
        /// </summary>
        /// <param name="card">the card</param>
        /// <returns>the Blockjuck value for the card</returns>
        private int GetBlockjuckCardValue(Card card)
        {
            switch (card.Rank)
            {
                case Rank.Ace:
                    return 11;
                case Rank.King:
                case Rank.Queen:
                case Rank.Jack:
                case Rank.Ten:
                    return 10;
                case Rank.Nine:
                    return 9;
                case Rank.Eight:
                    return 8;
                case Rank.Seven:
                    return 7;
                case Rank.Six:
                    return 6;
                case Rank.Five:
                    return 5;
                case Rank.Four:
                    return 4;
                case Rank.Three:
                    return 3;
                case Rank.Two:
                    return 2;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="newState">the new game state</param>
        public static void ChangeState(GameState newState)
        {
            currentState = newState;
        }

       

    }
}


