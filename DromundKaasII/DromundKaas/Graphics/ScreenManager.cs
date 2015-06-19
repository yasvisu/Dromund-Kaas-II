using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    public class ScreenManager
    {
        private static ScreenManager instance;
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }


        GameScreen[] Screens;

        SplashScreen Splash;
        PlayScreen Play;
        OptionsScreen Options;
        CreditsScreen Credits;

        GameScreen currentScreen;
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;


        public ScreenManager()
        {
            Dimensions = new Vector2(640, 480);
            currentScreen = new SplashScreen();

            Screens = new GameScreen[4];

            Splash = new SplashScreen();
            Play = new PlayScreen();
            Options = new OptionsScreen();
            Credits = new CreditsScreen();

            Screens[0] = Splash;
            Screens[1] = Play;
            Screens[2] = Options;
            Screens[3] = Credits;

            currentScreen = Splash;
        }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            for(int i=0; i<Screens.Length; i++)
            {
                Screens[i].LoadContent();
            }
        }

        public void UnloadContent()
        {
            for (int i = 0; i < Screens.Length; i++)
            {
                Screens[i].UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            if(gameTime.TotalGameTime.TotalSeconds>3 && currentScreen is SplashScreen)
            {
                this.SwitchScreen(Play);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        private void SwitchScreen(GameScreen G)
        {
            currentScreen = G;
            G.Begin();
        }
    }
}