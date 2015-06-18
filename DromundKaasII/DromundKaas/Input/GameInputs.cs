using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Input
{
    [Flags]
    public enum GameInputs
    {

        None = 0x0,

        // Movement

        Up = 0x1,
        Down = 0x2,
        Left = 0x4,
        Right = 0x8,

        // Actions

        A1 = 0x10,
        A2 = 0x20,
        A3 = 0x40,
        A4 = 0x80,
        A5 = 0x100,

        Interact = 0x200,

        // Menus

        Pause = 0x400,

        Quit = 0x800
    }
}