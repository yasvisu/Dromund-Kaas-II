using DromundKaasII.Engine.Interfaces;
using DromundKaasII.Interfaces;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Tiles
{
    /// <summary>
    /// Basic Tile class.
    /// </summary>
    public class Tile : IPathable
    {
        /// <summary>
        /// Initializes a new Tile.
        /// </summary>
        /// <param name="TraversalCost">The traversal cost of the tile.</param>
        /// <param name="TileType">The type of the tile.</param>
        /// <param name="Illumination">The current illumination color of the tile.</param>
        public Tile(double TraversalCost, TileTypeOptions TileType, Color Illumination)
        {
            this.TraversalCost = TraversalCost;
            this.TileType = TileType;
            this.Illumination = Illumination;
        }

        /// <summary>
        /// The traversal cost of the tile.
        /// </summary>
        public double TraversalCost { get; set; }

        /// <summary>
        /// The current occupant of the tile.
        /// </summary>
        public IActor Occupant { get; set; }

        /// <summary>
        /// The type of the tile.
        /// </summary>
        public TileTypeOptions TileType { get; set; }

        /// <summary>
        /// The current illumination color of the tile.
        /// </summary>
        public Color Illumination { get; set; }

        /// <summary>
        /// The tile's map position.
        /// </summary>
        public Vector2 MapPosition { get; set; }
    }
}
