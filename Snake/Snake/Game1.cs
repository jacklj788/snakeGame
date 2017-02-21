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

        Snake[] bodyParts = new Snake[50];
        Apple apple = new Apple();
        Texture2D snakeBody, appleTexture;
        KeyboardState kb;

        Rectangle[] bodyZones = new Rectangle[50];
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
            //bodyParts[1] = new Snake(350, 240, false, false);
            //bodyParts[2] = new Snake(300, 240, false, false);
            // these are hidden off to the side of the screen. Inatalised ready to be moved in.
            for (int i = 1; i < bodyParts.Length; i++)
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

            if (bodyZones[0].Intersects(appleZone))
            {
                // Activate the next body part
                bodyParts[Snake.length].setStateTrue();
                // Determines where to spawn the next body part in from.
                if (bodyParts[Snake.length - 1].movingRight)
                    bodyParts[Snake.length].setLocationRight(bodyParts[Snake.length - 1].getLocation());
                else if (bodyParts[Snake.length - 1].movingLeft)
                    bodyParts[Snake.length].setLocationLeft(bodyParts[Snake.length - 1].getLocation());
                else if (bodyParts[Snake.length - 1].movingUp)
                    bodyParts[Snake.length].setLocationUp(bodyParts[Snake.length - 1].getLocation());
                else
                    bodyParts[Snake.length].setLocationDown(bodyParts[Snake.length - 1].getLocation());
                // Move the apple to a new spot
                apple.respawn();
                // The snake grew my 1, need to communicate that
                Snake.length = Snake.length + 1;
            }
            // Stop if you hit yourself. Excluse body[1] because it will overlap the head when it turns.
            for (int i = 2; i < bodyZones.Length; i++)
            {
                // If it hits itself OR the border of the screen.
                if (bodyZones[0].Intersects(bodyZones[i]) || bodyZones[0].Location.X > 650 || bodyZones[0].Location.X < 0 || bodyZones[0].Location.Y > 650 || bodyZones[0].Location.Y < 0)
                {
                    // The game pauses when the snake length is > 50, so this is basically a way of "killing" the snake.
                    Snake.length = 100;
                }
            }

            // Changed variable numbers to bodyParts.Length so i don't need to keep changing it.
            if (Snake.length < bodyParts.Length)
            { 
                // Player controls the direction of the head. They don't need to have to hold the key down, that's now how snake played
                // But the snake should also always be moving.
                if ((kb.IsKeyDown(Keys.Right) || bodyParts[0].movingRight) && !(bodyParts[0].movingLeft))
                {
                    bodyParts[0].moveRight();
                }
                // if they're going left, they can't also go right..
                if ((kb.IsKeyDown(Keys.Left) || bodyParts[0].movingLeft) && !(bodyParts[0].movingRight))
                {
                    bodyParts[0].moveLeft();
                }

                if ((kb.IsKeyDown(Keys.Up) || bodyParts[0].movingUp) && !(bodyParts[0].movingDown))
                {
                    // moves the body part [0] up
                    bodyParts[0].moveUp();
                }

                // if you're going up you can't go down, only left or right.. etc..
                if ((kb.IsKeyDown(Keys.Down) || bodyParts[0].movingDown) && !(bodyParts[0].movingUp))
                {
                    bodyParts[0].moveDown();
                }

                // Logic for bodies, not head
                for (int i = 1; i < bodyParts.Length; i++)
                {
                    // Are they active?
                    if (bodyParts[i].getState())
                    {
                        // Is it moving right?
                        if (bodyParts[i-1].movingRight)
                        {
                            // If the body is lower than the next piece, move it up
                            if (bodyParts[i].getLocation().Y > bodyParts[i - 1].getLocation().Y)
                            {
                                bodyParts[i].moveUp();
                            }
                            // is it higher? move it down?
                            else if (bodyParts[i].getLocation().Y < bodyParts[i - 1].getLocation().Y)
                                bodyParts[i].moveDown();
                            // Oh, it's on the correct Y axis? Perfect - move right.
                            else
                                bodyParts[i].moveRight();
                        }
                        else if (bodyParts[i - 1].movingLeft)
                        {
                            if (bodyParts[i].getLocation().Y > bodyParts[i - 1].getLocation().Y)
                            {
                                bodyParts[i].moveUp();
                            }
                            else if (bodyParts[i].getLocation().Y < bodyParts[i - 1].getLocation().Y)
                                bodyParts[i].moveDown();
                            else
                                bodyParts[i].moveLeft();
                        }
                        else if (bodyParts[i - 1].movingUp)
                        {
                            if (bodyParts[i].getLocation().X > bodyParts[i - 1].getLocation().X)
                            {
                                bodyParts[i].moveLeft();
                            }
                            else if (bodyParts[i].getLocation().X < bodyParts[i - 1].getLocation().X)
                                bodyParts[i].moveRight();
                            else
                                bodyParts[i].moveUp();
                        }
                        else
                        {
                            if (bodyParts[i].getLocation().X > bodyParts[i - 1].getLocation().X)
                            {
                                bodyParts[i].moveLeft();
                            }
                            else if (bodyParts[i].getLocation().X < bodyParts[i - 1].getLocation().X)
                                bodyParts[i].moveRight();
                            else
                                bodyParts[i].moveDown();
                        }
                    }
                }
            }
            // My way of fixing a bug. Until I can figure out why it's happening in the first place, this will have to do.
            // See the function itself for more details. 
            cureHeadAche();

            // Places a rectangle at the same position of each body part
            for (int i = 0; i < bodyParts.Length  - 1; i++)
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
            for (int i = 0; i < bodyParts.Length; i++)
            {
                    spriteBatch.Draw(snakeBody, bodyParts[i].getLocation(), Color.White);
            }
            spriteBatch.Draw(appleTexture, apple.getLocation(), Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void cureHeadAche()
        {
            // Need to actually be more than 1 part
            if (Snake.length >= 2)
            {
                // Determines if the head is more than 50 places away from the body. (It should never be).
                // However for some reason it is, which is creating a gap that gets larger and larger. It didn't use to be like this - new "clean" code that actually works introduced
                // this bug. 
                if (bodyParts[0].getLocation().X > (bodyParts[1].getLocation().X + 50))
                    bodyParts[0].backStepRight();
                else if (bodyParts[0].getLocation().X < (bodyParts[1].getLocation().X - 50))
                    bodyParts[0].backStepLeft();
                else if (bodyParts[0].getLocation().Y > (bodyParts[1].getLocation().Y + 50))
                    bodyParts[0].backStepUp();
                else if (bodyParts[0].getLocation().Y < (bodyParts[1].getLocation().Y - 50))
                    bodyParts[0].backStepDown();
            }
        }
    }
}
