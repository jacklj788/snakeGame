using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Snake[] bodyParts = new Snake[10];
        Apple apple = new Apple();
        Texture2D snakeBody, appleTexture;
        KeyboardState kb;

        Rectangle[] bodyZones = new Rectangle[10];
        Rectangle appleZone = new Rectangle(); 

        //bool movingRight = false, movingLeft = false, movingUp = false, movingDown = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 700;
            // forces the game to update 1/2th of a second. So twice a second. 
            // Turned out to be a bad idea. It limits how often a user can input commands, which makes it feel clunky and laggy
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 2f);
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
            // Head
            bodyParts[0] = new Snake(400, 240, true, true);
            // starting body parts
            bodyParts[1] = new Snake(350, 240, true, false);
            bodyParts[2] = new Snake(300, 240, true, false);
            // these are hidden off to the side of the screen. Inatalised ready to be moved in.
            for (int i = 3; i < 10; i++)
            {
                // Inactive
                bodyParts[i] = new Snake(-500, -500, false, false);
            }

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
            snakeBody = Content.Load<Texture2D>("Head");
            appleTexture = Content.Load<Texture2D>("Apple");


            // TODO: use this.Content to load your game content here
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
            kb = Keyboard.GetState();
            // You win if you get to length 10. 
            if (Snake.length < 10)
            { 
                if (kb.IsKeyDown(Keys.Right) || bodyParts[0].movingRight)
                {
                    if (bodyZones[0].Intersects(appleZone))
                    {
                        // Activate the next body part
                        bodyParts[Snake.length].setStateTrue();

                        bodyParts[Snake.length].setLocationRight(bodyParts[Snake.length - 1].getLocation());

                        apple.respawn();
                        Snake.length = Snake.length + 1;
                    }

                    bodyParts[0].moveRight();
                }

                if (kb.IsKeyDown(Keys.Left) || bodyParts[0].movingLeft)
                {
                    if (bodyZones[0].Intersects(appleZone))
                    {
                        // Activate the next body part
                        bodyParts[Snake.length].setStateTrue();

                        bodyParts[Snake.length].setLocationLeft(bodyParts[Snake.length - 1].getLocation());

                        apple.respawn();
                        Snake.length = Snake.length + 1;
                    } 
                    bodyParts[0].moveLeft();
                }

                if (kb.IsKeyDown(Keys.Up) || bodyParts[0].movingUp)
                {
                    if (bodyZones[0].Intersects(appleZone))
                    {
                        // Activate the next body part
                        bodyParts[Snake.length].setStateTrue();

                        bodyParts[Snake.length].setLocationUp(bodyParts[Snake.length - 1].getLocation());

                        apple.respawn();
                        Snake.length = Snake.length + 1;
                    }
                    // moves the body part [0] up
                    bodyParts[0].moveUp();
                }

                // Need to make it so if you're going up you can't go down, only left or right.. etc..
                if (kb.IsKeyDown(Keys.Down) || bodyParts[0].movingDown)
                {
                    if (bodyZones[0].Intersects(appleZone))
                    {
                        // Activate the next body part
                        bodyParts[Snake.length].setStateTrue();

                        bodyParts[Snake.length].setLocationDown(bodyParts[Snake.length - 1].getLocation());

                        apple.respawn();
                        Snake.length = Snake.length + 1;
                    }

                    // moves the body part [0] down
                    bodyParts[0].moveDown();
                }

                // Logic for bodies, not head
                for (int i = 1; i < 10; i++)
                {
                    if (bodyParts[i].getState())
                    {
                        if (bodyParts[i-1].movingRight)
                        {
                            if (bodyParts[i].getLocation().Y == bodyParts[i - 1].getLocation().Y)
                            {
                                bodyParts[i].moveRight();
                            }
                            else if (bodyParts[i].getLocation().Y < bodyParts[i - 1].getLocation().Y)
                                bodyParts[i].moveDown();
                            else
                                bodyParts[i].moveUp();
                        }
                        else if (bodyParts[i - 1].movingLeft)
                        {
                            if (bodyParts[i].getLocation().Y == bodyParts[i - 1].getLocation().Y)
                            {
                                bodyParts[i].moveLeft();
                            }
                            else if (bodyParts[i].getLocation().Y < bodyParts[i - 1].getLocation().Y)
                                bodyParts[i].moveDown();
                            else
                                bodyParts[i].moveUp();
                        }
                        else if (bodyParts[i - 1].movingUp)
                        {
                            if (bodyParts[i].getLocation().X == bodyParts[i - 1].getLocation().X)
                            {
                                bodyParts[i].moveUp();
                            }
                            else if (bodyParts[i].getLocation().X < bodyParts[i - 1].getLocation().X)
                                bodyParts[i].moveRight();
                            else
                                bodyParts[i].moveLeft();
                        }
                        else if (bodyParts[i - 1].movingDown)
                        {
                            if (bodyParts[i].getLocation().X == bodyParts[i - 1].getLocation().X)
                            {
                                bodyParts[i].moveDown();
                            }
                            else if (bodyParts[i].getLocation().X < bodyParts[i - 1].getLocation().X)
                                bodyParts[i].moveRight();
                            else
                                bodyParts[i].moveLeft();
                        }
                    }
                }
            }
            // Places a rectangle at the same position of each body part
            for (int i = 0; i < 10; i++)
            {
                bodyZones[i] = new Rectangle((int)bodyParts[i].getLocation().X, (int)bodyParts[i].getLocation().Y, 50, 50);
            }
            // and the apple.
            appleZone = new Rectangle((int)apple.getLocation().X, (int)apple.getLocation().Y, 50, 50);

            base.Update(gameTime);

        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            for (int i = 0; i < 10; i++)
            {
                if(bodyParts[i].getState())
                    spriteBatch.Draw(snakeBody, bodyParts[i].getLocation(), Color.White);
            }
            spriteBatch.Draw(appleTexture, apple.getLocation(), Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
