using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.GameObjects.Enums
{
    /// <summary>
    /// Options for tile drawing.
    /// </summary>
    [Flags]
    public enum TileDrawOptions
    {
        /// <summary>
        /// Water-type tile.
        /// </summary>
        Water = 0x1,

        /// <summary>
        /// Wall-type tile.
        /// </summary>
        Wall = 0x2,

        /// <summary>
        /// Tree-type tile.
        /// </summary>
        Tree = 0x4,

        /// <summary>
        /// Ground-type tile.
        /// </summary>
        Ground = 0x8,

        /// <summary>
        /// Hole-type tile.
        /// </summary>
        Hole = 0x10
    }
}
