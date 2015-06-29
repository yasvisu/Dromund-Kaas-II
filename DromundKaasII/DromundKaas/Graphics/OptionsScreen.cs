using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using DromundKaasII.Engine;
using DromundKaasII.Graphics.UI;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    /// <summary>
    /// Screen managing options.
    /// </summary>
    public class OptionsScreen : GameScreen
    {
        private const float buttonTransparency = 0.50f;

        private List<Button> buttons;
        private int activeIndex;
        private bool buttonToggleActive;
        private Color TintColor;

        private int buttonTimeout;

        Texture2D fadeout;

        /// <summary>
        /// The EngineOptions to modify.
        /// </summary>
        public IEngineOptions EngineOptions { get; set; }

        private int ActiveIndex
        {
            get
            {
                return this.activeIndex;
            }
            set
            {
                if (value < 0)
                {
                    this.activeIndex = this.buttons.Count - 1;
                }
                else if (value >= this.buttons.Count)
                {
                    this.activeIndex = 0;
                }
                else
                {
                    this.activeIndex = value;
                }
            }
        }

        /// <summary>
        /// Load all content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            this.buttons = new List<Button>();
            this.buttons.Add(new Button("Toggle Speed", this.CycleSpeed, new Vector2(5, 5),true));


            this.buttons.Add(new Button("Toggle Difficulty", this.CycleDifficulty, new Vector2(60, 60), true));

            this.buttons.Add(new Button("Credits", () =>
            {
                ScreenManager.Instance.PlayCredits = true;
            }, 
            new Vector2(60, 60), true));

            this.TintColor = Color.Red;
            this.buttonToggleActive = true;
            this.buttonTimeout = 150;

            this.fadeout = this.content.Load<Texture2D>("fadeout");
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

            // React to button clicks
            if (this.buttonToggleActive)
            {
                if (this.input.IsPressed(GameInputs.Up))
                {
                    this.buttonToggleActive = false;
                    this.ActiveIndex--;
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(this.buttonTimeout);
                        this.buttonToggleActive = true;
                    });
                }
                else if (this.input.IsPressed(GameInputs.Down))
                {
                    this.buttonToggleActive = false;
                    this.ActiveIndex++;
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(this.buttonTimeout);
                        this.buttonToggleActive = true;
                    });
                }
                else if (this.input.IsPressed(GameInputs.Interact))
                {
                    if (this.buttons[this.ActiveIndex].IsActive)
                    {
                        this.buttons[this.ActiveIndex].IsActive = false;
                        Console.Beep();
                        this.buttons[this.ActiveIndex].Click();
                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(this.buttonTimeout);
                            this.buttons[this.ActiveIndex].IsActive = true;
                        });
                    }
                }
            }

            // Refresh button text
            this.buttons[0].Text = string.Format("Toggle Speed: {0}", this.EngineOptions.GameSpeed.ToString());
            this.buttons[1].Text = string.Format("Toggle Difficulty: {0}", this.EngineOptions.GameDifficulty.ToString());

        }

        /// <summary>
        /// Draw this element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Point penPoint = (ScreenManager.Instance.Dimensions * new Vector2(0.10f, 0.10f)).ToPoint();
            Point sizePoint = (ScreenManager.Instance.Dimensions * new Vector2(0.80f, 0.20f)).ToPoint();
            for (int i = 0; i < this.buttons.Count; i++)
            {
                Color tempTint = (i == this.ActiveIndex ? this.TintColor : Color.White);
                spriteBatch.Draw(this.fadeout, new Rectangle(penPoint, sizePoint), tempTint * OptionsScreen.buttonTransparency);

                Vector2 textPosition = penPoint.ToVector2() + (sizePoint.ToVector2() - ScreenManager.Instance.TitleFont.MeasureString(buttons[i].Text)) / 2;

                spriteBatch.DrawString(ScreenManager.Instance.TitleFont, this.buttons[i].Text,
                    textPosition,
                    tempTint);


                penPoint.Y += (int)(0.30 * ScreenManager.Instance.Dimensions.Y);
            }
        }

        /// <summary>
        /// Cycle the speeds of the EngineOptions.
        /// </summary>
        private void CycleSpeed()
        {
            switch (this.EngineOptions.GameSpeed)
            {
                case GameSpeedOptions.Fast:
                    this.EngineOptions.GameSpeed = GameSpeedOptions.Slow;
                    break;
                case GameSpeedOptions.Normal:
                    this.EngineOptions.GameSpeed = GameSpeedOptions.Fast;
                    break;
                case GameSpeedOptions.Slow:
                    this.EngineOptions.GameSpeed = GameSpeedOptions.Normal;
                    break;
            }
        }

        /// <summary>
        /// Cycle the difficulties of the EngineOptions.
        /// </summary>
        private void CycleDifficulty()
        {
            switch (this.EngineOptions.GameDifficulty)
            {
                case GameDifficultyOptions.Hard:
                    this.EngineOptions.GameDifficulty = GameDifficultyOptions.Easy;
                    break;
                case GameDifficultyOptions.Medium:
                    this.EngineOptions.GameDifficulty = GameDifficultyOptions.Hard;
                    break;
                case GameDifficultyOptions.Easy:
                    this.EngineOptions.GameDifficulty = GameDifficultyOptions.Medium;
                    break;
            }
        }
    }
}