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

        Snake[] bodyParts = new Snake[5];
        Texture2D snakeBody;
        KeyboardState kb;

        bool movingRight = true, movingLeft = false, movingUp = false, movingDown = false;

        //bool movingRight = false, movingLeft = false, movingUp = false, movingDown = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            // forces the game to update 1/2th of a second. So twice a second. 
            // Why? Snake doesn't need high FPS, in fact it's a game you could probably make in a windows form. This way I can achieve that "move .. move .. move .." feel that snake has.
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
            // first body will appear in the middle of the screen.
            bodyParts[0] = new Snake(400, 240, true, true);
            // these are hidden off to the side of the screen. Inatalised ready to be moved in.
            bodyParts[1] = new Snake(350, 240, false, false);



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

            if (kb.IsKeyDown(Keys.Right) || movingRight)
            {
                bodyParts[0].moveRight();
                movingRight = true;
                movingLeft = false;
                movingDown = false;
                movingUp = false;

                float a = bodyParts[0].getLocation().Y;
                // 200 > 20
                if (bodyParts[1].getLocation().Y > a)
                    bodyParts[1].moveUp();
                else if (bodyParts[1].getLocation().Y < a)
                    bodyParts[1].moveDown();
                else
                    bodyParts[1].moveRight();
            }

            if (kb.IsKeyDown(Keys.Left) || movingLeft)
            {
                bodyParts[0].moveLeft();
                movingRight = false;
                movingLeft = true;
                movingDown = false;
                movingUp = false;

                float a = bodyParts[0].getLocation().Y;
                // 200 > 20
                if (bodyParts[1].getLocation().Y > a)
                    bodyParts[1].moveUp();
                else if (bodyParts[1].getLocation().Y < a)
                    bodyParts[1].moveDown();
                else
                    bodyParts[1].moveLeft();
            }

            if (kb.IsKeyDown(Keys.Up) || movingUp)
            {
                movingUp = true;
                movingRight = false;
                movingLeft = false;
                movingDown = false;
                // moves the body part [0] up
                bodyParts[0].moveUp();
                // a = the X axis of the head
                float a = bodyParts[0].getLocation().X;
                if (bodyParts[1].getLocation().X < a)
                {
                    // move the body [1] across to the location of [0] - 50 on the X Axis
                    bodyParts[1].moveRight();
                }
                else if (bodyParts[1].getLocation().X > a)
                {
                    bodyParts[1].moveLeft();
                }
                else
                {
                    // Otherwise they're both on the same X axis, so start to make it follow the Y axis.
                    bodyParts[1].moveUp();
                }

            }

            // Need to make it so if you're going up you can't go down, only left or right.. etc..
            if (kb.IsKeyDown(Keys.Down) || movingDown)
            {
                movingRight = false;
                movingLeft = false;
                movingDown = true;
                movingUp = false;

                // moves the body part [0] down
                bodyParts[0].moveDown();
                // a = the X axis of the head
                float a = bodyParts[0].getLocation().X;

                if (bodyParts[1].getLocation().X < a)
                {
                    // move the body [1] down to the location of [0]
                    bodyParts[1].moveRight();
                }
                else if (bodyParts[1].getLocation().X > a)
                    bodyParts[1].moveLeft();
                else
                {
                    // Otherwise they're both on the same X axis, so start to make it follow the Y axis.
                    bodyParts[1].moveDown();
                }
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
            spriteBatch.Draw(snakeBody, bodyParts[0].getLocation(), Color.White);
            spriteBatch.Draw(snakeBody, bodyParts[1].getLocation(), Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
