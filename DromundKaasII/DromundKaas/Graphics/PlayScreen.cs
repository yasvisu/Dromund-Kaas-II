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
            if(input.IsPressed(GameInputs.Up))
            {
                engine.GameState.Player.PlayerInputOptions = GameInputs.Up;
            }
            else if (input.IsPressed(GameInputs.Down))
            {
                engine.GameState.Player.PlayerInputOptions = GameInputs.Down;
            }
            else if (input.IsPressed(GameInputs.Left))
            {
                engine.GameState.Player.PlayerInputOptions = GameInputs.Left;
            }
            else if (input.IsPressed(GameInputs.Right))
            {
                engine.GameState.Player.PlayerInputOptions = GameInputs.Right;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < engine.GameState.Map.GetLength(0); i++)
            {
                for (int j = 0; j < engine.GameState.Map.GetLength(1); j++)
                {
                    spriteBatch.Draw(mytile, new Vector2(j * 64, i * 64));
                }
            }

            foreach (var a in engine.GameState.Actors)
            {
                spriteBatch.Draw(mychar, new Vector2(a.MapPosition.X * 64, a.MapPosition.Y * 64));
            }
        }
    }
}
