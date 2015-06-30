using System;

using DromundKaasII.Graphics;
using DromundKaasII.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII
{
    /// <summary>
    /// The main class of the game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;
        
        /// <summary>
        /// Initializes the Game.
        /// </summary>
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.input = new InputManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it queries for any required services and loads any non-graphic
        /// related content.
        /// </summary>
        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            this.graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            this.graphics.ApplyChanges();  
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenManager.Instance.LoadContent(Content, input);
        }

        /// <summary>
        /// UnloadContent will be called once per game.
        /// </summary>
        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(input.GamePadConnected)
            {
                if (input.InputMode != InputModes.GamePad)
                {
                    Console.Beep();
                    this.input.InputMode = InputModes.GamePad;
                }
            }
            else if(input.InputMode!=InputModes.Keyboard)
            {
                Console.Beep();
                this.input.InputMode = InputModes.Keyboard;
            }

            if (input.IsPressed(GameInputs.Quit))
                this.Exit();

            ScreenManager.Instance.Update(gameTime);
            this.input.UpdateInput();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();

            ScreenManager.Instance.Draw(spriteBatch);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
