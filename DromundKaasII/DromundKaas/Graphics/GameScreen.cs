using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    public class GameScreen
    {
        protected ContentManager content;
        protected InputManager input;

        public Image Background { get; set; }

        public bool IsSwitchReady { get; set; }

        public virtual void Run()
        {
            this.IsSwitchReady = false;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                this.IsSwitchReady = true;
            });
        }

        public virtual void Pause()
        {

        }

        public virtual void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            this.input = ScreenManager.Instance.Input;
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Background != null)
            {
                Background.Draw(spriteBatch);
            }
        }
    }
}
