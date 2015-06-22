using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DromundKaasII.Graphics
{
    public class ScreenManager
    {
        private static ScreenManager instance;


        GameScreen[] Screens;

        SplashScreen Splash;
        PlayScreen Play;
        OptionsScreen Options;
        CreditsScreen Credits;

        GameScreen currentScreen;
        Stack<GameScreen> stackedScreens;

        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;



        public ScreenManager()
        {
            Dimensions = new Vector2(640, 480);
            currentScreen = new SplashScreen();
            stackedScreens = new Stack<GameScreen>();

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
            Splash.Run();
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

        public SpriteFont TitleFont { get; private set; }
        public SpriteFont TextFont { get; private set; }

        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }
        public InputManager Input { private set; get; }

        public bool PlayCredits { get; set; }

        public void LoadContent(ContentManager Content, InputManager Input)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            this.Input = Input;

            TitleFont = Content.Load<SpriteFont>("Fonts/TitleFont");
            TextFont = Content.Load<SpriteFont>("Fonts/TextFont");

            // starry background from http://amzwall.com/starry-background-image/
            Image Background = new Image()
            {
                Path = "starry_background",
            };
            Background.LoadContent();

            for (int i = 0; i < Screens.Length; i++)
            {
                Screens[i].LoadContent();
                Screens[i].Background = Background;
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
            CheckForSwitch(gameTime);

            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        private void CheckForSwitch(GameTime gameTime)
        {
            if (currentScreen.IsSwitchReady)
            {
                if (currentScreen is SplashScreen && (gameTime.TotalGameTime.TotalSeconds > 3 || Input.IsPressed(GameInputs.Pause)))
                {
                    this.SwitchScreen(Play);
                    Options.EngineOptions = Play.EngineOptions;
                }
                else if ((currentScreen is PlayScreen || currentScreen is CreditsScreen) && Input.IsPressed(GameInputs.Pause))
                {
                    this.SwitchScreen(Options, true);
                }
                else if (currentScreen is OptionsScreen && Input.IsPressed(GameInputs.Pause))
                {
                    this.SwitchScreen(Play);
                }
                else if (!(currentScreen is CreditsScreen) && PlayCredits)
                {
                    this.SwitchScreen(Credits);
                }
            }
        }

        private void SwitchScreen(GameScreen G)
        {
            this.SwitchScreen(G, false);
        }

        private void SwitchScreen(GameScreen G, bool stackScreen)
        {
            if (stackScreen)
            {
                stackedScreens.Push(currentScreen);
                currentScreen.Pause();
            }
            currentScreen = G;
            currentScreen.Run();
        }
    }
}