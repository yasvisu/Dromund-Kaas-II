using System;

namespace DromundKaasII.Input
{
    /// <summary>
    /// The types of game inputs.
    /// </summary>
    [Flags]
    public enum GameInputs
    {
        /// <summary>
        /// Equivalent to no input.
        /// </summary>
        None = 0x0,

        // Movement

        /// <summary>
        /// Go up.
        /// </summary>
        Up = 0x1,

        /// <summary>
        /// Go down.
        /// </summary>
        Down = 0x2,

        /// <summary>
        /// Go left.
        /// </summary>
        Left = 0x4,

        /// <summary>
        /// Go right.
        /// </summary>
        Right = 0x8,

        // Actions

        /// <summary>
        /// Use Action 1.
        /// </summary>
        A1 = 0x10,

        /// <summary>
        /// Use Action 2.
        /// </summary>
        A2 = 0x20,

        /// <summary>
        /// Use Action 3.
        /// </summary>
        A3 = 0x40,

        /// <summary>
        /// Use Action 4.
        /// </summary>
        A4 = 0x80,

        /// <summary>
        /// Use Action 5.
        /// </summary>
        A5 = 0x100,

        /// <summary>
        /// Interact with.
        /// </summary>
        Interact = 0x200,

        // Menus

        /// <summary>
        /// Pause current.
        /// </summary>
        Pause = 0x400,

        /// <summary>
        /// Quit game.
        /// </summary>
        Quit = 0x800
    }
}