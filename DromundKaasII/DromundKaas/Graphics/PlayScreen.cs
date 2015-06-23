﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DromundKaasII.Engine.GameObjects.Tiles;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;
using DromundKaasII.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    public class PlayScreen : GameScreen
    {
        Texture2D image;
        string path;
        Task engineTask;
        IEngine engine;

        Texture2D mytex;
        Texture2D mychar;
        Texture2D mytile;
        Texture2D ground, tree, hole, water, wall;

        // Use this to map types to textures.
        Dictionary<Type, Texture2D> TypeTextures;

        public IEngineOptions EngineOptions
        {
            get
            {
                return this.engine;
            }
        }

        public void Initialize()
        {
            this.engine = new Engine.Engine();
            engineTask = Task.Factory.StartNew(() =>
                {
                    while (this.engine.IsRunning)
                    {
                        Thread.Sleep((int)this.engine.GameSpeed);
                        if (!this.engine.IsPaused)
                        {
                            this.engine.Update();
                        }
                    }
                });
        }

        public override void Run()
        {
            base.Run();
            if (engine == null)
            {
                this.Initialize();
            }
            engine.IsPaused = false;
        }

        public override void Pause()
        {
            engine.IsPaused = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            mychar = this.content.Load<Texture2D>("Actors/placeholderChar");
            mytile = this.content.Load<Texture2D>("Tiles/placeholderTile");
            ground = this.content.Load<Texture2D>("Tiles/default/ground");
            tree = this.content.Load<Texture2D>("Tiles/default/tree");
            hole = this.content.Load<Texture2D>("Tiles/default/hole");
            water = this.content.Load<Texture2D>("Tiles/default/water");
            wall = this.content.Load<Texture2D>("Tiles/default/wall");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.Background.Position = Vector2.Zero + (-engine.Player.MapPosition * 64) / 10;

            if (input.IsPressed(GameInputs.Up))
            {
                engine.Player.DesiredAction = GameInputs.Up;
            }
            else if (input.IsPressed(GameInputs.Down))
            {
                engine.Player.DesiredAction = GameInputs.Down;
            }
            else if (input.IsPressed(GameInputs.Left))
            {
                engine.Player.DesiredAction = GameInputs.Left;
            }
            else if (input.IsPressed(GameInputs.Right))
            {
                engine.Player.DesiredAction = GameInputs.Right;
            }
            else if (input.IsPressed(GameInputs.A1))
            {
                engine.Player.DesiredAction = GameInputs.A1;
            }
            else if (input.IsPressed(GameInputs.A2))
            {
                engine.Player.DesiredAction = GameInputs.A2;
            }
            else if (input.IsPressed(GameInputs.A3))
            {
                engine.Player.DesiredAction = GameInputs.A3;
            }
            else if (input.IsPressed(GameInputs.A4))
            {
                engine.Player.DesiredAction = GameInputs.A4;
            }
            else if (input.IsPressed(GameInputs.A5))
            {
                engine.Player.DesiredAction = GameInputs.A5;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 playerOffset = -engine.Player.MapPosition;
            Vector2 pixelOffset = new Vector2((ScreenManager.Instance.Dimensions.X - 64) / 2, (ScreenManager.Instance.Dimensions.Y - 64) / 2);

            for (int i = 0; i < engine.Map.GetLength(0); i++)
            {
                for (int j = 0; j < engine.Map.GetLength(1); j++)
                {
                    Vector2 destination = new Vector2((j + playerOffset.X) * 64, (i + playerOffset.Y) * 64) + pixelOffset;
                    Texture2D ToDraw = mytile;
                    switch (engine.Map[i, j].TileType)
                    {
                        case TileTypeOptions.Ground:
                            ToDraw = ground;
                            break;
                        case TileTypeOptions.Water:
                            ToDraw = water;
                            break;
                        case TileTypeOptions.Tree:
                            ToDraw = tree;
                            break;
                        case TileTypeOptions.Hole:
                            ToDraw = hole;
                            break;
                        case TileTypeOptions.Wall:
                            ToDraw = wall;
                            break;
                    }
                    spriteBatch.Draw(ToDraw, destination);
                    if (engine.Map[i, j].Occupant != null)
                    {
                        spriteBatch.Draw(mychar, destination);
                    }
                }
            }
        }
    }
}
