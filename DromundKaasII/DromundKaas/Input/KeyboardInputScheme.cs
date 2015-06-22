using Microsoft.Xna.Framework.Input;

namespace DromundKaasII.Input
{
    /// <summary>
    /// Input scheme of a Keyboard.
    /// </summary>
    public class KeyboardInputScheme
    {
        #region Defaults
        // Movement
        public const Keys default_Up = Keys.W;
        public const Keys default_Down = Keys.S;
        public const Keys default_Left = Keys.A;
        public const Keys default_Right = Keys.D;

        // Actions
        public const Keys default_A1 = Keys.D1;
        public const Keys default_A2 = Keys.D2;
        public const Keys default_A3 = Keys.D3;
        public const Keys default_A4 = Keys.D4;
        public const Keys default_A5 = Keys.D5;

        public const Keys default_Interact = Keys.F;

        // Menus
        public const Keys default_Pause = Keys.Space;

        public const Keys default_Quit = Keys.Escape;
        #endregion

        /// <summary>
        /// Default constructor setting all the keys to their default values.
        /// </summary>
        public KeyboardInputScheme()
        {
            this.Up = default_Up;
            this.Down = default_Down;
            this.Left = default_Left;
            this.Right = default_Right;

            this.A1 = default_A1;
            this.A2 = default_A2;
            this.A3 = default_A3;
            this.A4 = default_A4;
            this.A5 = default_A5;

            this.Interact = default_Interact;

            this.Pause = default_Pause;

            this.Quit = default_Quit;
        }

        // Movement
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Left { get; set; }
        public Keys Right { get; set; }

        // Actions
        public Keys A1 { get; set; }
        public Keys A2 { get; set; }
        public Keys A3 { get; set; }
        public Keys A4 { get; set; }
        public Keys A5 { get; set; }

        public Keys Interact { get; set; }

        // Menus
        public Keys Pause { get; set; }

        public Keys Quit { get; set; }

    }
}
