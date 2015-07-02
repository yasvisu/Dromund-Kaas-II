using System.Collections.Generic;

using DromundKaasII.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    /// <summary>
    /// Manager of various screens, their initialization, and their switching.
    /// </summary>
    public class ScreenManager
    {
        /// <summary>
        /// The singleton instance of this class.
        /// </summary>
        private static ScreenManager instance;

        GameScreen[] Screens;

        SplashScreen Splash;
        PlayScreen Play;
        OptionsScreen Options;
        CreditsScreen Credits;

        GameScreen currentScreen;
        Stack<GameScreen> stackedScreens;

        /// <summary>
        /// The graphics device of the ScreenManager.
        /// </summary>
        public GraphicsDevice GraphicsDevice;

        /// <summary>
        /// The sprite batch of the ScreenManager.
        /// </summary>
        public SpriteBatch SpriteBatch;


        /// <summary>
        /// Initializes a default ScreenManager singleton instance.
        /// </summary>
        public ScreenManager()
        {
            this.Dimensions = new Vector2(640, 480);
            this.currentScreen = new SplashScreen();
            this.stackedScreens = new Stack<GameScreen>();

            this.Screens = new GameScreen[4];

            this.Splash = new SplashScreen();
            this.Play = new PlayScreen();
            this.Options = new OptionsScreen();
            this.Credits = new CreditsScreen();

            this.Screens[0] = this.Splash;
            this.Screens[1] = this.Play;
            this.Screens[2] = this.Options;
            this.Screens[3] = this.Credits;

            this.currentScreen = this.Splash;
            this.Splash.Run();
        }

        /// <summary>
        /// The singleton instance of the ScreenManager class.
        /// </summary>
        public static ScreenManager Instance
        {
            get
            {
                if (ScreenManager.instance == null)
                {
                    ScreenManager.instance = new ScreenManager();
                }
                return ScreenManager.instance;
            }
        }

        /// <summary>
        /// Font used for titles.
        /// </summary>
        public SpriteFont TitleFont { get; private set; }

        /// <summary>
        /// Font used for texts.
        /// </summary>
        public SpriteFont TextFont { get; private set; }


        /// <summary>
        /// The dimensions of the ScreenManager.
        /// </summary>
        public Vector2 Dimensions { private set; get; }

        /// <summary>
        /// The content manager of the ScreenManager.
        /// </summary>
        public ContentManager Content { private set; get; }

        /// <summary>
        /// The input manager of the ScreenManager.
        /// </summary>
        public InputManager Input { private set; get; }


        /// <summary>
        /// Whether to play credits.
        /// </summary>
        public bool PlayCredits { get; set; }

        /// <summary>
        /// Load all content.
        /// </summary>
        public void LoadContent(ContentManager Content, InputManager Input)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            this.Input = Input;

            this.TitleFont = this.Content.Load<SpriteFont>("Fonts/TitleFont");
            this.TextFont = this.Content.Load<SpriteFont>("Fonts/TextFont");

            // starry background from http://amzwall.com/starry-background-image/
            Image Background = new Image()
            {
                Path = "starry_background",
            };
            Background.LoadContent();

            for (int i = 0; i < this.Screens.Length; i++)
            {
                this.Screens[i].LoadContent();
                this.Screens[i].Background = Background;
            }
        }

        /// <summary>
        /// Unload all content.
        /// </summary>
        public void UnloadContent()
        {
            for (int i = 0; i < this.Screens.Length; i++)
            {
                this.Screens[i].UnloadContent();
            }
        }

        /// <summary>
        /// Update this element.
        /// </summary>
        /// <param name="gameTime">The GameTime to update to.</param>
        public void Update(GameTime gameTime)
        {
            this.CheckForSwitch(gameTime);

            this.currentScreen.Update(gameTime);
        }

        /// <summary>
        /// Draw this element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            this.currentScreen.Draw(spriteBatch);
        }

        /// <summary>
        /// Checks whether switch is applicable and applies it.
        /// </summary>
        /// <param name="gameTime">The GameTime to check.</param>
        private void CheckForSwitch(GameTime gameTime)
        {
            if (this.currentScreen.IsSwitchReady)
            {
                if (this.currentScreen is SplashScreen && (gameTime.TotalGameTime.TotalSeconds > 5 || this.Input.IsPressed(GameInputs.Pause)))
                {
                    this.SwitchScreen(this.Play);
                    this.Options.EngineOptions = this.Play.EngineOptions;
                }
                else if ((this.currentScreen is PlayScreen || this.currentScreen is CreditsScreen) && this.Input.IsPressed(GameInputs.Pause))
                {
                    this.SwitchScreen(this.Options, true);
                }
                else if (this.currentScreen is OptionsScreen && this.Input.IsPressed(GameInputs.Pause))
                {
                    this.SwitchScreen(this.Play);
                }
                else if (!(this.currentScreen is CreditsScreen) && this.PlayCredits)
                {
                    this.SwitchScreen(this.Credits);
                }
            }
        }

        /// <summary>
        /// Switch to a different screen.
        /// </summary>
        /// <param name="G">The screen to switch to.</param>
        private void SwitchScreen(GameScreen G)
        {
            this.SwitchScreen(G, false);
        }

        /// <summary>
        /// Switch to a different screen.
        /// </summary>
        /// <param name="G">The screen to switch to.</param>
        /// <param name="stackScreen">Whether to push that screen on the screen stack.</param>
        private void SwitchScreen(GameScreen G, bool stackScreen)
        {
            if (stackScreen)
            {
                this.stackedScreens.Push(currentScreen);
                this.currentScreen.Pause();
            }
            this.currentScreen = G;
            this.currentScreen.Run();
        }
    }
}