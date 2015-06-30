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
            this.content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            this.input = ScreenManager.Instance.Input;
            this.whiteFadeout = this.content.Load<Texture2D>("w_Fadeout");

            this.health = new StatusBar(new Rectangle(0, 0, (int)ScreenManager.Instance.Dimensions.X, (int)(ScreenManager.Instance.Dimensions.Y * 0.05f)), 1, Color.Red);

            this.mana = new StatusBar(new Rectangle((int)(ScreenManager.Instance.Dimensions.X * 0.95f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.95f)), 1, Color.CadetBlue * BAR_TRANSPARENCY);

            this.focus = new StatusBar(new Rectangle(0, (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.95f)), 1, Color.Green * BAR_TRANSPARENCY);

            this.experience = new StatusBar(new Rectangle((int)(ScreenManager.Instance.Dimensions.X * 0.05f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f), (int)(ScreenManager.Instance.Dimensions.X * 0.90f), (int)(ScreenManager.Instance.Dimensions.Y * 0.05f)), 1, Color.Indigo * BAR_TRANSPARENCY);

            this.bars = new List<StatusBar> { this.health, this.mana, this.focus, this.experience }.ToArray();
        }

        /// <summary>
        /// Unload all content.
        /// </summary>
        public override void UnloadContent()
        {
            this.content.Unload();
        }

        /// <summary>
        /// Update this element.
        /// </summary>
        /// <param name="gameTime">The GameTime to update to.</param>
        public override void Update(GameTime gameTime)
        {
            this.health.FillRatio = (float)this.Player.Stats.Health / this.Player.Stats.MaxHealth;
            this.mana.FillRatio = (float)this.Player.Stats.Mana / this.Player.Stats.MaxMana;
            this.focus.FillRatio = (float)this.Player.Stats.Focus / this.Player.Stats.MaxFocus;
            this.experience.FillRatio = (float)this.Player.Stats.Experience / ((this.Player.Stats.Level + 1) * 100);

            this.health.Bounds = new Rectangle(this.health.Bounds.X, this.health.Bounds.Y, (int)(this.health.MaxWidth * this.health.FillRatio), this.health.Bounds.Height);
            this.mana.Bounds = new Rectangle(this.mana.Bounds.X, this.mana.Bounds.Y, this.mana.MaxWidth, (int)(this.mana.MaxHeight * this.mana.FillRatio));
            this.focus.Bounds = new Rectangle(this.focus.Bounds.X, this.focus.Bounds.Y, this.focus.Bounds.Width, (int)(this.focus.MaxHeight * this.focus.FillRatio));
            this.experience.Bounds = new Rectangle(this.experience.Bounds.X, this.experience.Bounds.Y, (int)(this.experience.MaxWidth * this.experience.FillRatio), this.experience.Bounds.Height);
        }

        /// <summary>
        /// Draw this element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bar in this.bars)
            {
                spriteBatch.Draw(this.whiteFadeout, bar.Bounds, bar.Color);
                spriteBatch.DrawString(ScreenManager.Instance.TextFont, bar.FillRatio.ToString(), new Vector2(bar.Bounds.X, bar.Bounds.Y), Color.White);
            }
        }
    }
}
