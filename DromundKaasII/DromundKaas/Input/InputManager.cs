using DromundKaasII.Engine.Exceptions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DromundKaasII.Input
{
    /// <summary>
    /// Manager for input device abstraction.
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// Default constructor for PlayerIndex.One and default Keyboard InputMode.
        /// </summary>
        public InputManager()
        {
            this.InputMode = InputModes.Keyboard;
            this.KeyboardScheme = new KeyboardInputScheme();
            this.GamePadScheme = new GamePadInputScheme();
            this.CurrentKeyboard = Keyboard.GetState();
            this.CurrentPlayerIndex = PlayerIndex.One;
            this.CurrentGamePad = GamePad.GetState(CurrentPlayerIndex);
        }

        /// <summary>
        /// The current device the manager is listening to.
        /// </summary>
        public InputModes InputMode { get; set; }

        /// <summary>
        /// The Keyboard key scheme the manager is listening to and mapping to GameInputs
        /// </summary>
        public KeyboardInputScheme KeyboardScheme { get; set; }

        /// <summary>
        /// The last retrieved state of the keyboard.
        /// </summary>
        public KeyboardState CurrentKeyboard { get; set; }


        /// <summary>
        /// The Keyboard key scheme the manager is listening to and mapping to GameInputs
        /// </summary>
        public GamePadInputScheme GamePadScheme { get; set; }

        /// <summary>
        /// The last retrieved state of the keyboard.
        /// </summary>
        public GamePadState CurrentGamePad { get; set; }

        /// <summary>
        /// The current PlayerIndex GamePad being listened to.
        /// </summary>
        public PlayerIndex CurrentPlayerIndex { get; set; }


        /// <summary>
        /// Whether the GamePad is connected.
        /// </summary>
        public bool GamePadConnected
        {
            get
            {
                return this.CurrentGamePad.IsConnected;
            }
        }

        /// <summary>
        /// Updates the states of the input devices the manager is checking.
        /// </summary>
        public void UpdateInput()
        {
            if (InputMode == InputModes.Keyboard)
            {
                CurrentKeyboard = Keyboard.GetState();
            }
            else if (InputMode == InputModes.GamePad)
            {
                CurrentGamePad = GamePad.GetState(CurrentPlayerIndex);
            }
        }

        /// <summary>
        /// Checks whether the button the GameInput corresponds to is being pressed on the current device.
        /// </summary>
        /// <param name="Input">The GameInput to map and check.</param>
        /// <returns></returns>
        public bool IsPressed(GameInputs Input)
        {
            switch (Input)
            {
                case GameInputs.Up:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.Up);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.Up);
                    }
                    break;
                case GameInputs.Down:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.Down);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.Down);
                    }
                    break;
                case GameInputs.Left:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.Left);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.Left);
                    }
                    break;
                case GameInputs.Right:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.Right);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.Right);
                    }
                    break;

                // Actions

                case GameInputs.A1:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.A1);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.A1);
                    }
                    break;
                case GameInputs.A2:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.A2);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.A2);
                    }
                    break;
                case GameInputs.A3:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.A3);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.A3);
                    }
                    break;
                case GameInputs.A4:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.A4);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.A4);
                    }
                    break;
                case GameInputs.A5:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.A5);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.A5);
                    }
                    break;

                case GameInputs.Interact:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.Interact);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.Interact);
                    }
                    break;

                // Menus

                case GameInputs.Pause:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.Pause);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.Pause);
                    }
                    break;

                case GameInputs.Quit:
                    if (InputMode == InputModes.Keyboard)
                    {
                        return CurrentKeyboard.IsKeyDown(KeyboardScheme.Quit);
                    }
                    else if (InputMode == InputModes.GamePad)
                    {
                        return CurrentGamePad.IsButtonDown(GamePadScheme.Quit);
                    }
                    break;
                default:
                    throw new UnsupportedKeyException(string.Format("{0} key is not supported!", Input));
            }
            return false;
        }
    }
}
