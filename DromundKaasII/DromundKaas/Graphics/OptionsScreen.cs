using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class OptionsScreen : GameScreen
    {
        private const float buttonTransparency = 0.50f;

        private List<Button> buttons;
        private int activeIndex;
        private bool buttonToggleActive;
        private Color TintColor;

        private int buttonTimeout;

        Texture2D fadeout;

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

            this.fadeout = content.Load<Texture2D>("fadeout");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // React to button clicks
            if (this.buttonToggleActive)
            {
                if (input.IsPressed(GameInputs.Up))
                {
                    this.buttonToggleActive = false;
                    this.ActiveIndex--;
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(this.buttonTimeout);
                        this.buttonToggleActive = true;
                    });
                }
                else if (input.IsPressed(GameInputs.Down))
                {
                    this.buttonToggleActive = false;
                    this.ActiveIndex++;
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(this.buttonTimeout);
                        this.buttonToggleActive = true;
                    });
                }
                else if (input.IsPressed(GameInputs.Interact))
                {
                    if (buttons[activeIndex].IsActive)
                    {
                        buttons[activeIndex].IsActive = false;
                        Console.Beep();
                        buttons[activeIndex].Click();
                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(this.buttonTimeout);
                            buttons[activeIndex].IsActive = true;
                        });
                    }
                }
            }

            // Refresh button text
            buttons[0].Text = string.Format("Toggle Speed: {0}", EngineOptions.GameSpeed.ToString());
            buttons[1].Text = string.Format("Toggle Difficulty: {0}", EngineOptions.GameDifficulty.ToString());

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Point penPoint = (ScreenManager.Instance.Dimensions * new Vector2(0.10f, 0.10f)).ToPoint();
            Point sizePoint = (ScreenManager.Instance.Dimensions * new Vector2(0.80f, 0.20f)).ToPoint();
            for (int i = 0; i < buttons.Count; i++)
            {
                Color tempTint = (i == activeIndex ? this.TintColor : Color.White);
                spriteBatch.Draw(fadeout, new Rectangle(penPoint, sizePoint), tempTint * buttonTransparency);

                Vector2 textPosition = penPoint.ToVector2() + (sizePoint.ToVector2() - ScreenManager.Instance.TitleFont.MeasureString(buttons[i].Text)) / 2;

                spriteBatch.DrawString(ScreenManager.Instance.TitleFont, buttons[i].Text,
                    textPosition,
                    tempTint);


                penPoint.Y += (int)(0.30 * ScreenManager.Instance.Dimensions.Y);
            }
        }

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