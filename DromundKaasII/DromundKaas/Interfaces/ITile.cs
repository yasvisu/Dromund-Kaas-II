using DromundKaasII.Engine.GameObjects.Tiles;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Exposes the type and occupant of a tile.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// The current occupant of the tile.
        /// </summary>
        IActor Occupant { get; }

        /// <summary>
        /// The type of the tile.
        /// </summary>
        TileTypeOptions TileType { get; }

        /// <summary>
        /// The current illumination of the tile.
        /// </summary>
        Color Illumination { get; set; }
    }
}
