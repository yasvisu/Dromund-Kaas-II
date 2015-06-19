using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        // Use this to map types to textures.
        Dictionary<Type, Texture2D> TypeTextures;

        public override void Begin()
        {
            this.engine = new Engine.Engine();
            engineTask = new AsyncTimer(engine.UpdateGameState, int.MaxValue, (ulong)engine.GameState.GameDifficulty).StartAsync();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            mychar = this.content.Load<Texture2D>("Actors/placeholderChar");
            mytile = this.content.Load<Texture2D>("Tiles/placeholderTile");


            input = new InputManager();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
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
        }
    }
}
