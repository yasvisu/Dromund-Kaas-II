using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics.UI
{
    public class HudScreen : GameScreen
    {

        Texture2D whiteFadeout;

        StatusBar health, mana, focus, experience;
        StatusBar[] bars;

        public IPlayer Player { get; set; }

        public new void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            this.input = ScreenManager.Instance.Input;
            whiteFadeout = content.Load<Texture2D>("w_Fadeout");

            health = new StatusBar(new Rectangle(0, 0, (int)ScreenManager.Instance.Dimensions.X, (int)(ScreenManager.Instance.Dimensions.Y * 0.05f)), 1, Color.Red);

            mana = new StatusBar(new Rectangle((int)(ScreenManager.Instance.Dimensions.X * 0.95f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.95f)), 1, Color.CadetBlue * 0.9f);

            focus = new StatusBar(new Rectangle(0, (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.95f)), 1, Color.Green * 0.9f);

            experience = new StatusBar(new Rectangle((int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.90f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f)), 1, Color.Indigo * 0.9f);

            bars = new List<StatusBar> { health, mana, focus, experience }.ToArray();
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            this.health.FillRatio = (float)this.Player.Stats.Health / this.Player.Stats.MaxHealth;
            this.mana.FillRatio = (float)this.Player.Stats.Mana / this.Player.Stats.MaxMana;
            this.focus.FillRatio = (float)this.Player.Stats.Focus / this.Player.Stats.MaxFocus;

            this.health.Bounds = new Rectangle(this.health.Bounds.X, this.health.Bounds.Y, (int)(this.health.MaxWidth * this.health.FillRatio), this.health.Bounds.Height);
            this.mana.Bounds = new Rectangle(this.mana.Bounds.X, this.mana.Bounds.Y, this.mana.MaxWidth , (int)(this.mana.Bounds.Height* this.mana.FillRatio));
            this.focus.Bounds = new Rectangle(this.focus.Bounds.X, this.focus.Bounds.Y, this.focus.Bounds.Width, (int)(this.focus.Bounds.Height* this.focus.FillRatio));

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bar in bars)
            {
                spriteBatch.Draw(whiteFadeout, bar.Bounds, bar.Color);
            }
        }
    }
}
