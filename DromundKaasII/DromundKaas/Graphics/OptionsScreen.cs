using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    public class OptionsScreen : GameScreen
    {
        private List<Button> buttons;

        public override void LoadContent()
        {
            base.LoadContent();
            this.buttons = new List<Button>();
            this.buttons.Add(new Button() { Click = () => { }, Location = new Vector2(5, 5), Texture = content.Load<Texture2D>("button-sample") });
            this.buttons.Add(new Button() { Click = () => { }, Location = new Vector2(60, 60), Texture = content.Load<Texture2D>("button-sample"), IsActive = true });
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in buttons)
            {
                spriteBatch.Draw(button.Texture, button.Location, (button.IsActive ? Color.Red : Color.White));
            }
        }
    }
}