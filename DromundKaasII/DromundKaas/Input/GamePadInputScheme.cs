using Microsoft.Xna.Framework.Input;

namespace DromundKaasII.Input
{
    /// <summary>
    /// Input scheme of a GamePad.
    /// </summary>
    public class GamePadInputScheme
    {
        #region Defaults
        // Movement
        public const Buttons default_Up = Buttons.DPadUp;
        public const Buttons default_Down = Buttons.DPadDown;
        public const Buttons default_Left = Buttons.DPadLeft;
        public const Buttons default_Right = Buttons.DPadRight;

        // Actions
        public const Buttons default_A1 = Buttons.A;
        public const Buttons default_A2 = Buttons.B;
        public const Buttons default_A3 = Buttons.X;
        public const Buttons default_A4 = Buttons.Y;
        public const Buttons default_A5 = Buttons.LeftShoulder;

        public const Buttons default_Interact = Buttons.Start;

        // Menus
        public const Buttons default_Pause = Buttons.Back;

        public const Buttons default_Quit = Buttons.Back | Buttons.Start;
        #endregion

        /// <summary>
        /// Default constructor setting all the keys to their default values.
        /// </summary>
        public GamePadInputScheme()
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
        public Buttons Up { get; set; }
        public Buttons Down { get; set; }
        public Buttons Left { get; set; }
        public Buttons Right { get; set; }

        // Actions
        public Buttons A1 { get; set; }
        public Buttons A2 { get; set; }
        public Buttons A3 { get; set; }
        public Buttons A4 { get; set; }
        public Buttons A5 { get; set; }

        public Buttons Interact { get; set; }

        // Menus
        public Buttons Pause { get; set; }

        public Buttons Quit { get; set; }

    }
}
