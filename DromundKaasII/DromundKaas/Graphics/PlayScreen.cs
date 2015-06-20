using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DromundKaasII.GameObjects.Tiles;
using DromundKaasII.Input;
using DromundKaasII.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    public class PlayScreen : GameScreen
    {
        Texture2D image;
        string path;
        Engine.Engine engine;
        Task engineTask;
        InputManager input;

        Texture2D mytex;
        Texture2D mychar;
        Texture2D mytile;
        Texture2D ground, tree, hole, water, wall;

        // Use this to map types to textures.
        Dictionary<Type, Texture2D> TypeTextures;

        public override void Begin()
        {
            this.engine = new Engine.Engine();
            engineTask = new AsyncTimer(engine.UpdateGameState, int.MaxValue, (ulong)engine.GameState.GameSpeed).StartAsync();
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

            input = new InputManager();
            input.InputMode = InputModes.Keyboard;
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            input.UpdateInput();
            if (input.IsPressed(GameInputs.Up))
            {
                engine.GameState.Player.DesiredAction = GameInputs.Up;
            }
            else if (input.IsPressed(GameInputs.Down))
            {
                engine.GameState.Player.DesiredAction = GameInputs.Down;
            }
            else if (input.IsPressed(GameInputs.Left))
            {
                engine.GameState.Player.DesiredAction = GameInputs.Left;
            }
            else if (input.IsPressed(GameInputs.Right))
            {
                engine.GameState.Player.DesiredAction = GameInputs.Right;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 playerOffset = -engine.GameState.Player.MapPosition;
            Vector2 pixelOffset = new Vector2((640 - 64) / 2, (480 - 64) / 2);

            for (int i = 0; i < engine.GameState.Map.GetLength(0); i++)
            {
                for (int j = 0; j < engine.GameState.Map.GetLength(1); j++)
                {
                    Vector2 destination = new Vector2((j + playerOffset.X) * 64, (i + playerOffset.Y) * 64) + pixelOffset;
                    Texture2D ToDraw = mytile;
                    switch (engine.GameState.Map[i, j].TileType)
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
                    if (engine.GameState.Map[i, j].Occupant != null)
                    {
                        spriteBatch.Draw(mychar, destination);
                    }
                }
            }
        }
    }
}
