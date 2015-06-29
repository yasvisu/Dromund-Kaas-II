using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using DromundKaasII.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    /// <summary>
    /// Credits screen to display all the credits for this game.
    /// </summary>
    public class CreditsScreen : GameScreen
    {
        string creditsText;

        Vector2 initialPosition;
        Vector2 creditsPosition;
        Vector2 bgOffset;

        float finalThresholdY;

        private static readonly Vector2 idleScroll = new Vector2(0, -0.5f);
        private static readonly Vector2 activeScroll = new Vector2(0, 3);

        private static readonly Vector2 bgScroll = new Vector2(0, -0.5f);
        private static readonly Vector2 bgIdleScroll = bgScroll * new Vector2(idleScroll.X,Math.Abs(idleScroll.Y));
        private static readonly Vector2 bgActiveScroll = bgScroll * activeScroll;

        bool scroll;
        bool done;

        /// <summary>
        /// Run the CreditsScreen.
        /// </summary>
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

            this.creditsPosition = this.initialPosition;
            this.bgOffset = this.Background.Position;
        }

        /// <summary>
        /// Load all content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            this.creditsText = File.ReadAllText("Content/Credits.txt");

            Vector2 textSize = ScreenManager.Instance.TextFont.MeasureString(this.creditsText);
            this.initialPosition = (ScreenManager.Instance.Dimensions - new Vector2(textSize.X, 0)) / 2;
            this.finalThresholdY = ScreenManager.Instance.Dimensions.Y / 2 - textSize.Y;
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
            if (this.input.IsPressed(GameInputs.Up))
            {
                if (this.creditsPosition.Y < this.initialPosition.Y)
                {
                    this.creditsPosition += CreditsScreen.activeScroll;
                    this.bgOffset -= CreditsScreen.bgActiveScroll;
                }
            }
            else if (this.input.IsPressed(GameInputs.Down))
            {
                if (this.creditsPosition.Y > this.finalThresholdY)
                {
                    this.creditsPosition -= CreditsScreen.activeScroll;
                    this.bgOffset += CreditsScreen.bgActiveScroll;
                }
            }
            else if (this.scroll == true)
            {
                this.creditsPosition += CreditsScreen.idleScroll;
                this.bgOffset += CreditsScreen.bgIdleScroll;
            }

            if (!this.done && this.creditsPosition.Y < this.finalThresholdY)
            {
                this.scroll = false;
                this.done = true;
            }
        }

        /// <summary>
        /// Draw this element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            var temp = this.Background.Position;
            this.Background.Position = this.bgOffset;

            base.Draw(spriteBatch);

            this.Background.Position = temp;
            spriteBatch.DrawString(ScreenManager.Instance.TextFont, this.creditsText, this.creditsPosition, Color.Yellow);
        }
    }
}
