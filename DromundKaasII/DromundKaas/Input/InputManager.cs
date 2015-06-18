using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Input
{
    public class InputManager
    {
        public InputManager()
        {
            this.InputMode = InputModes.Keyboard;
            this.KeyboardScheme = new KeyboardInputScheme();
            this.GamePadScheme = new GamePadInputScheme();
            this.CurrentKeyboard = Keyboard.GetState();
            this.CurrentPlayerIndex = PlayerIndex.One;
            this.CurrentGamePad = GamePad.GetState(CurrentPlayerIndex);
        }

        public InputModes InputMode { get; set; }

        public KeyboardInputScheme KeyboardScheme { get; set; }
        public KeyboardState CurrentKeyboard { get; set; }

        public GamePadInputScheme GamePadScheme { get; set; }
        public GamePadState CurrentGamePad { get; set; }
        public PlayerIndex CurrentPlayerIndex { get; set; }

        public void UpdateInput()
        {
            if(InputMode==InputModes.Keyboard)
            {
                CurrentKeyboard = Keyboard.GetState();
            }
            else if(InputMode==InputModes.GamePad)
            {
                CurrentGamePad = GamePad.GetState(CurrentPlayerIndex);
            }
        }
    }
}
