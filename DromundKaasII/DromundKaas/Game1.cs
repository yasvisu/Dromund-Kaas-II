﻿using DromundKaasII.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DromundKaasII.Input;
using DromundKaasII.Tools;

namespace DromundKaasII
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Engine.Engine engine;
        Task engineTask;
        InputManager input;

        // Use this to map types to textures.
        Dictionary<Type, Texture2D> TypeTextures;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.engine = new Engine.Engine();
            engineTask = new AsyncTimer(engine.UpdateGameState, int.MaxValue, (ulong)engine.GameState.GameDifficulty).StartAsync();
            input = new InputManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();  
            base.Initialize();
        }


        Texture2D mytex;
        Texture2D mychar;
        Texture2D mytile;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mytex = this.Content.Load<Texture2D>("Splash/dksplash");
            mychar = this.Content.Load<Texture2D>("Actors/placeholderChar");
            mytile = this.Content.Load<Texture2D>("Tiles/placeholderTile");
            //ScreenManager.Instance.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            //ScreenManager.Instance.UnloadContent();
            //engineTask.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (input.IsPressed(GameInputs.Quit))
                Exit();

            //ScreenManager.Instance.Update(gameTime);
            input.UpdateInput();
            /*
            switch (engine.CycleCounter % 4)
            {
                case 0:
                    engine.GameState.Player.PlayerInputOptions = GameInputs.Down;
                    break;
                case 1:
                    engine.GameState.Player.PlayerInputOptions = GameInputs.Up;
                    break;
                case 2:
                    engine.GameState.Player.PlayerInputOptions = GameInputs.Left;
                    break;
                case 3:
                    engine.GameState.Player.PlayerInputOptions = GameInputs.Right;
                    break;
            }
            */
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin();
            //ScreenManager.Instance.Draw(spriteBatch);
            //spriteBatch.End();
            //spriteBatch.Begin();
            //spriteBatch.Draw(mytex, new Vector2(0, 0));
            for (int i = 0; i < engine.GameState.Map.GetLength(0); i++)
            {
                for (int j = 0; j < engine.GameState.Map.GetLength(1); j++)
                {
                    spriteBatch.Draw(mytile, new Vector2(i * 64, j * 64));
                }
            }

            foreach (var a in engine.GameState.Actors)
            {
                spriteBatch.Draw(mychar, new Vector2(a.MapPosition.X * 64, a.MapPosition.Y * 64));
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
