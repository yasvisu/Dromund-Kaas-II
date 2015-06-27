using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DromundKaasII.Engine;
using DromundKaasII.Engine.GameObjects.Actors.Debris;
using DromundKaasII.Engine.GameObjects.Actors.NPCs;
using DromundKaasII.Engine.GameObjects.Actors.Players;
using DromundKaasII.Engine.GameObjects.Tiles;
using DromundKaasII.Graphics.Exceptions;
using DromundKaasII.Graphics.UI;
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

        HudScreen Hud;

        // Use this to map types to textures.
        Dictionary<Type, Texture2D> TypeTextures2D;
        Dictionary<StatusEffects, Texture2D> StatusEffectTextures2D;

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

            this.Hud.Player = this.engine.Player;
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

            this.Hud = new HudScreen();
            this.TypeTextures2D = new Dictionary<Type, Texture2D>();
            this.StatusEffectTextures2D = new Dictionary<StatusEffects, Texture2D>();

            mytile = this.content.Load<Texture2D>("Tiles/placeholderTile");
            ground = this.content.Load<Texture2D>("Tiles/default/ground");
            tree = this.content.Load<Texture2D>("Tiles/default/tree");
            hole = this.content.Load<Texture2D>("Tiles/default/hole");
            water = this.content.Load<Texture2D>("Tiles/default/water");
            wall = this.content.Load<Texture2D>("Tiles/default/wall");

            // Load Primal texture (horizontal)
            mychar = this.content.Load<Texture2D>("Actors/Primal/ChovecheHorizontal");

            this.TypeTextures2D[typeof(Primal)] = this.content.Load<Texture2D>("Actors/Primal/ChovecheHorizontal");
            this.TypeTextures2D[typeof(Campfire)] = this.content.Load<Texture2D>("Actors/Debris/campfire");
            this.TypeTextures2D[typeof(ZombieFriend)] = this.content.Load<Texture2D>("Actors/placeholderChar");

            foreach (StatusEffects se in Enum.GetValues(typeof(StatusEffects)))
            {
                this.StatusEffectTextures2D[se] = this.content.Load<Texture2D>("StatusEffects/" + se.ToString());
            }

            this.Hud.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.engineTask.IsFaulted)
            {
                throw new EngineFailureException("Engine failed!", this.engineTask.Exception);
            }

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

            Hud.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 playerOffset = -engine.Player.MapPosition;
            Vector2 pixelOffset = new Vector2((ScreenManager.Instance.Dimensions.X - 64) / 2,
                                                (ScreenManager.Instance.Dimensions.Y - 64) / 2);

            for (int i = 0; i < engine.Map.GetLength(0); i++)
            {
                for (int j = 0; j < engine.Map.GetLength(1); j++)
                {
                    var currentTile = engine.Map[i, j];
                    Rectangle originRect = new Rectangle(0, 0, 64, 64);

                    Vector2 destination = new Vector2((j + playerOffset.X) * 64, (i + playerOffset.Y) * 64) + pixelOffset;
                    Texture2D ToDraw = mytile;
                    switch (currentTile.TileType)
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
                    spriteBatch.Draw(ToDraw, destination, currentTile.Illumination);
                    if (currentTile.Occupant != null)
                    {
                        originRect.Offset(new Point(64 * (int)currentTile.Occupant.Direction, 0));
                        Texture2D temp = TypeTextures2D[currentTile.Occupant.GetType()];
                        
                        spriteBatch.Draw(temp, destination, originRect, currentTile.Illumination);
                        foreach(StatusEffects status in currentTile.Occupant.Status.ToList())
                        {
                            temp = StatusEffectTextures2D[status];
                            spriteBatch.Draw(temp, destination, currentTile.Illumination);
                        }
                    }
                }
            }

            this.Hud.Draw(spriteBatch);
        }
    }
}
