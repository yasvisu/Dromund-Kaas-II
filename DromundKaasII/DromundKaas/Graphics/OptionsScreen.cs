using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DromundKaasII.Graphics.UI;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    public class OptionsScreen : GameScreen
    {
        private List<Button> buttons;
        private int activeIndex;
        private bool buttonToggleActive;
        private Color TintColor;

        private int buttonTimeout;

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
            this.buttons.Add(new Button(() => { this.TintColor = Color.Red; }, new Vector2(5, 5), content.Load<Texture2D>("button-sample"), true));
            this.buttons.Add(new Button(() => { this.TintColor = Color.Green; }, new Vector2(60, 60), content.Load<Texture2D>("button-sample"), true));
            this.TintColor = Color.Red;
            this.buttonToggleActive = true;
            this.buttonTimeout = 150;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                spriteBatch.Draw(buttons[i].Texture, buttons[i].Location, (i == activeIndex ? this.TintColor : Color.White));
            }
        }
    }
}