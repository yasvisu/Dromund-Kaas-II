using System;
using System.Collections.Generic;
using System.Linq;
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    /// <summary>
    /// Main screen for the game.
    /// </summary>
    public class PlayScreen : GameScreen
    {
        Task engineTask;
        IEngine engine;

        Texture2D ground, tree, hole, water, wall;

        HudScreen Hud;

        /// <summary>
        /// Use this to map types to textures.
        /// </summary>
        Dictionary<Type, Texture2D> TypeTextures2D;
        Dictionary<StatusEffects, Texture2D> StatusEffectTextures2D;

        /// <summary>
        /// Exposes the EngineOptions of the Engine.
        /// </summary>
        public IEngineOptions EngineOptions
        {
            get
            {
                return this.engine;
            }
        }

        /// <summary>
        /// Initialize this screen.
        /// </summary>
        public void Initialize()
        {
            this.engine = new Engine.Engine();
            this.engineTask = Task.Factory.StartNew(() =>
                {
                    while (this.engine.IsRunning)
                    {
                        Thread.Sleep((int)Math.Max((int)this.engine.GameSpeed - (DateTime.Now - this.engine.LastCalled).TotalMilliseconds, 0));
                        if (!this.engine.IsPaused)
                        {
                            this.engine.Update();
                        }
                    }
                });

            this.Hud.Player = this.engine.Player;
        }

        /// <summary>
        /// Run the PlayScreen and the Engine.
        /// </summary>
        public override void Run()
        {
            base.Run();
            if (this.engine == null)
            {
                this.Initialize();
            }
            this.engine.IsPaused = false;
        }

        /// <summary>
        /// Pause the PlayScreen and the Engine.
        /// </summary>
        public override void Pause()
        {
            this.engine.IsPaused = true;
        }

        /// <summary>
        /// Load all content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            this.Hud = new HudScreen();
            this.TypeTextures2D = new Dictionary<Type, Texture2D>();
            this.StatusEffectTextures2D = new Dictionary<StatusEffects, Texture2D>();

            this.ground = this.content.Load<Texture2D>("Tiles/default/ground");
            this.tree = this.content.Load<Texture2D>("Tiles/default/tree");
            this.hole = this.content.Load<Texture2D>("Tiles/default/hole");
            this.water = this.content.Load<Texture2D>("Tiles/default/water");
            this.wall = this.content.Load<Texture2D>("Tiles/default/wall");

            // Load Primal texture (horizontal).
            this.TypeTextures2D[typeof(Primal)] = this.content.Load<Texture2D>("Actors/Primal/ChovecheHorizontal");

            // Load Campfire texture.
            this.TypeTextures2D[typeof(Campfire)] = this.content.Load<Texture2D>("Actors/Debris/campfire");

            // Load ZombieFriend texture (horizontal).
            this.TypeTextures2D[typeof(ZombieFriend)] = this.content.Load<Texture2D>("Actors/NPCs/ZombieFriendHorizontal");


            // Load textures for all status effects.
            foreach (StatusEffects se in Enum.GetValues(typeof(StatusEffects)))
            {
                this.StatusEffectTextures2D[se] = this.content.Load<Texture2D>("StatusEffects/" + se.ToString());
            }

            this.Hud.LoadContent();
        }

        /// <summary>
        /// Unload all content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Update this element.
        /// </summary>
        /// <param name="gameTime">The GameTime to update to.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.engineTask.IsFaulted)
            {
                throw new EngineFailureException("Engine failed!", this.engineTask.Exception);
            }

            this.Background.Position = Vector2.Zero + (-this.engine.Player.MapPosition * 64) / 10;

            if (this.input.IsPressed(GameInputs.Up))
            {
                this.engine.Player.DesiredAction = GameInputs.Up;
            }
            else if (this.input.IsPressed(GameInputs.Down))
            {
                this.engine.Player.DesiredAction = GameInputs.Down;
            }
            else if (this.input.IsPressed(GameInputs.Left))
            {
                this.engine.Player.DesiredAction = GameInputs.Left;
            }
            else if (this.input.IsPressed(GameInputs.Right))
            {
                this.engine.Player.DesiredAction = GameInputs.Right;
            }
            else if (this.input.IsPressed(GameInputs.A1))
            {
                this.engine.Player.DesiredAction = GameInputs.A1;
            }
            else if (this.input.IsPressed(GameInputs.A2))
            {
                this.engine.Player.DesiredAction = GameInputs.A2;
            }
            else if (this.input.IsPressed(GameInputs.A3))
            {
                this.engine.Player.DesiredAction = GameInputs.A3;
            }
            else if (input.IsPressed(GameInputs.A4))
            {
                this.engine.Player.DesiredAction = GameInputs.A4;
            }
            else if (this.input.IsPressed(GameInputs.A5))
            {
                this.engine.Player.DesiredAction = GameInputs.A5;
            }

            this.Hud.Update(gameTime);
        }

        /// <summary>
        /// Draw this element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 playerOffset = -this.engine.Player.MapPosition;
            Vector2 pixelOffset = new Vector2((ScreenManager.Instance.Dimensions.X - 64) / 2,
                                                (ScreenManager.Instance.Dimensions.Y - 64) / 2);

            for (int i = 0; i < this.engine.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.engine.Map.GetLength(1); j++)
                {
                    var currentTile = this.engine.Map[i, j];
                    Rectangle originRect = new Rectangle(0, 0, 64, 64);

                    Vector2 destination = new Vector2((j + playerOffset.X) * 64, (i + playerOffset.Y) * 64) + pixelOffset;
                    Texture2D ToDraw = this.ground;
                    switch (currentTile.TileType)
                    {
                        case TileTypeOptions.Ground:
                            ToDraw = this.ground;
                            break;
                        case TileTypeOptions.Water:
                            ToDraw = this.water;
                            break;
                        case TileTypeOptions.Tree:
                            ToDraw = this.tree;
                            break;
                        case TileTypeOptions.Hole:
                            ToDraw = this.hole;
                            break;
                        case TileTypeOptions.Wall:
                            ToDraw = this.wall;
                            break;
                    }
                    spriteBatch.Draw(ToDraw, destination, currentTile.Illumination);
                    if (currentTile.Occupant != null)
                    {
                        originRect.Offset(new Point(64 * (int)currentTile.Occupant.Direction, 0));
                        Texture2D temp = this.TypeTextures2D[currentTile.Occupant.GetType()];

                        spriteBatch.Draw(temp, destination, originRect, currentTile.Illumination);
                        foreach (StatusEffects status in currentTile.Occupant.Status.ToList())
                        {
                            temp = this.StatusEffectTextures2D[status];
                            spriteBatch.Draw(temp, destination, currentTile.Illumination);
                        }
                    }
                }
            }

            this.Hud.Draw(spriteBatch);
        }
    }
}
