using System;
using System.Collections.Generic;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics.UI
{
    /// <summary>
    /// A Heads-Up-Display containing various StatusBars.
    /// </summary>
    public class HudScreen : GameScreen
    {
        /// <summary>
        /// The default bar transparency of the HUD bars.
        /// </summary>
        public const float BAR_TRANSPARENCY = 0.9f;

        Texture2D whiteFadeout;
        StatusBar health, mana, focus, experience;
        StatusBar[] bars;

        /// <summary>
        /// Delay between blinks.
        /// </summary>
        private static readonly TimeSpan blinkDelayMillis = new TimeSpan(0, 0, 0, 0, 400);

        /// <summary>
        /// The Player owner of this HUD.
        /// </summary>
        public IPlayer Player { get; set; }

        /// <summary>
        /// Load all content.
        /// </summary>
        public new void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            this.input = ScreenManager.Instance.Input;
            whiteFadeout = content.Load<Texture2D>("w_Fadeout");

            health = new StatusBar(new Rectangle(0, 0, (int)ScreenManager.Instance.Dimensions.X, (int)(ScreenManager.Instance.Dimensions.Y * 0.05f)), 1, Color.Red);

            mana = new StatusBar(new Rectangle((int)(ScreenManager.Instance.Dimensions.X * 0.95f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.95f)), 1, Color.CadetBlue * BAR_TRANSPARENCY);

            focus = new StatusBar(new Rectangle(0, (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.95f)), 1, Color.Green * BAR_TRANSPARENCY);

            experience = new StatusBar(new Rectangle((int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.90f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f)), 1, Color.Indigo * BAR_TRANSPARENCY);

            bars = new List<StatusBar> { health, mana, focus, experience }.ToArray();
        }

        /// <summary>
        /// Unload all content.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        /// <summary>
        /// Update this element.
        /// </summary>
        /// <param name="gameTime">The GameTime to update to.</param>
        public override void Update(GameTime gameTime)
        {
            //if(expBarBlink!=null  && expBarBlink.IsCompleted)
            //{
            //    expBarBlink.Dispose();
            //    expBarBlink = null;
            //}

            this.health.FillRatio = (float)this.Player.Stats.Health / this.Player.Stats.MaxHealth;
            this.mana.FillRatio = (float)this.Player.Stats.Mana / this.Player.Stats.MaxMana;
            this.focus.FillRatio = (float)this.Player.Stats.Focus / this.Player.Stats.MaxFocus;
            this.experience.FillRatio = (float)this.Player.Stats.Experience / ((this.Player.Stats.Level + 1) * 100);

            this.health.Bounds = new Rectangle(this.health.Bounds.X, this.health.Bounds.Y, (int)(this.health.MaxWidth * this.health.FillRatio), this.health.Bounds.Height);
            this.mana.Bounds = new Rectangle(this.mana.Bounds.X, this.mana.Bounds.Y, this.mana.MaxWidth, (int)(this.mana.MaxHeight * this.mana.FillRatio));
            this.focus.Bounds = new Rectangle(this.focus.Bounds.X, this.focus.Bounds.Y, this.focus.Bounds.Width, (int)(this.focus.MaxHeight * this.focus.FillRatio));
            this.experience.Bounds = new Rectangle(this.experience.Bounds.X, this.experience.Bounds.Y, (int)(this.experience.MaxWidth * this.experience.FillRatio), this.experience.Bounds.Height);

            //if(this.Player.LevelUp && expBarBlink==null)
            //{
            //    expBarBlink = Task.Factory.StartNew(() =>
            //    {
            //        while (this.Player.LevelUp)
            //        {
            //            Thread.Sleep(blinkDelayMillis);
            //            this.experience.Color = Color.Indigo * BAR_TRANSPARENCY;
            //            Thread.Sleep(blinkDelayMillis);
            //            this.experience.Color = Color.Yellow * BAR_TRANSPARENCY;
            //        }
            //    });
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bar in bars)
            {
                spriteBatch.Draw(whiteFadeout, bar.Bounds, bar.Color);
                spriteBatch.DrawString(ScreenManager.Instance.TextFont, bar.FillRatio.ToString(), new Vector2(bar.Bounds.X, bar.Bounds.Y), Color.White);
            }
        }
    }
}
