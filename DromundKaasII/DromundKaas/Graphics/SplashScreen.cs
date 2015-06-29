using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    /// <summary>
    /// Simple splash screen.
    /// </summary>
    public class SplashScreen : GameScreen
    {
        Texture2D image;
        string path;

        /// <summary>
        /// Load all content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            this.path = "Splash/dksplash";
            this.image = this.content.Load<Texture2D>(this.path);
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
        }

        /// <summary>
        /// Draw this element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.image, Vector2.Zero, Color.White);
        }
    }
}
