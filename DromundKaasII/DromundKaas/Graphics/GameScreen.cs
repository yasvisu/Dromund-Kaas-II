using System.Threading;
using System.Threading.Tasks;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    /// <summary>
    /// Base class for any sort of game screen.
    /// </summary>
    public class GameScreen
    {
        /// <summary>
        /// The content manager of the game screen.
        /// </summary>
        protected ContentManager content;

        /// <summary>
        /// The input manager of the game screen.
        /// </summary>
        protected InputManager input;

        /// <summary>
        /// The background image.
        /// </summary>
        public Image Background { get; set; }

        /// <summary>
        /// Whether the screen is ready to be switched.
        /// </summary>
        public bool IsSwitchReady { get; set; }

        /// <summary>
        /// Run this element.
        /// </summary>
        public virtual void Run()
        {
            this.IsSwitchReady = false;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                this.IsSwitchReady = true;
            });
        }

        /// <summary>
        /// Pause this element.
        /// </summary>
        public virtual void Pause()
        { }

        /// <summary>
        /// Load all content.
        /// </summary>
        public virtual void LoadContent()
        {
            this.content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            this.input = ScreenManager.Instance.Input;
        }

        /// <summary>
        /// Unload all content.
        /// </summary>
        public virtual void UnloadContent()
        {
            this.content.Unload();
        }

        /// <summary>
        /// Update this element.
        /// </summary>
        /// <param name="gameTime">The GameTime to update to.</param>
        public virtual void Update(GameTime gameTime)
        { }

        /// <summary>
        /// Draw this element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.Background != null)
            {
                this.Background.Draw(spriteBatch);
            }
        }
    }
}
