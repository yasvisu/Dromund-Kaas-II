using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    public class CreditsScreen : GameScreen
    {

        string text;
        Texture2D textTexture;

        Vector2 initialPosition;
        Vector2 creditsPosition;
        float finalThresholdY;

        Vector2 idleScroll;
        Vector2 activeScroll;

        bool scroll;
        bool done;

        public override void Run()
        {
            this.IsSwitchReady = false;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                this.IsSwitchReady = true;
                this.scroll = true;
            });
            ScreenManager.Instance.PlayCredits = false;

            this.scroll = true;
            this.done = false;

            this.creditsPosition = initialPosition;

        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.text = File.ReadAllText("Content/Credits.txt");

            Vector2 textSize = ScreenManager.Instance.TextFont.MeasureString(text);
            initialPosition = (ScreenManager.Instance.Dimensions - new Vector2(textSize.X, 0)) / 2;
            this.finalThresholdY = ScreenManager.Instance.Dimensions.Y / 2 - textSize.Y;

            idleScroll = new Vector2(0, -0.5f);
            activeScroll = new Vector2(0, 3);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (input.IsPressed(GameInputs.Up))
            {
                if (this.creditsPosition.Y < initialPosition.Y)
                {
                    this.creditsPosition += activeScroll;
                }
            }
            else if (input.IsPressed(GameInputs.Down))
            {
                if (this.creditsPosition.Y > this.finalThresholdY)
                {
                    this.creditsPosition -= activeScroll;
                }
            }
            else if (scroll == true)
            {
                this.creditsPosition += idleScroll;
            }

            if (!done && this.creditsPosition.Y < this.finalThresholdY)
            {
                scroll = false;
                done = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(ScreenManager.Instance.TextFont, text, creditsPosition, Color.Yellow);
        }
    }
}
