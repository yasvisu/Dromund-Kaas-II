using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DromundKaasII.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    public class OptionsScreen : GameScreen
    {
        private List<Button> buttons;
        private int activeIndex;
        private bool buttonsActive;

        private const int buttonTimeout = 150;

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
            this.buttons.Add(new Button() { Click = () => { }, Location = new Vector2(5, 5), Texture = content.Load<Texture2D>("button-sample") });
            this.buttons.Add(new Button() { Click = () => { }, Location = new Vector2(60, 60), Texture = content.Load<Texture2D>("button-sample"), IsActive = true });
            this.buttonsActive = true;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.buttonsActive)
            {
                if (input.IsPressed(Input.GameInputs.Up))
                {
                    this.buttonsActive = false;
                    this.ActiveIndex--;
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(buttonTimeout);
                        this.buttonsActive = true;
                    });
                }
                else if (input.IsPressed(Input.GameInputs.Down))
                {
                    this.buttonsActive = false;
                    this.ActiveIndex++;
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(buttonTimeout);
                        this.buttonsActive = true;
                    });
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                spriteBatch.Draw(buttons[i].Texture, buttons[i].Location, (i == activeIndex ? Color.Red : Color.White));
            }
        }
    }
}