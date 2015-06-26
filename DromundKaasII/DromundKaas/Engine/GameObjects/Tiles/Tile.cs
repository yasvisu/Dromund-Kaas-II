using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.Interfaces;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Tiles
{
    public class Tile : IPathable
    {
        public Tile(double TraversalCost, TileTypeOptions TileType, Color Illumination)
        {
            this.TraversalCost = TraversalCost;
            this.TileType = TileType;
            this.Illumination = Illumination;
        }

        public double TraversalCost { get; set; }
        public IActor Occupant { get; set; }
        public TileTypeOptions TileType { get; set; }
        public Color Illumination { get; set; }
    }
}
