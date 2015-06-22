using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Tiles
{
    public class Tile : ITile
    {
        public Tile(double TraversalCost, TileTypeOptions TileType)
        {
            this.TraversalCost = TraversalCost;
            this.TileType = TileType;
        }

        public double TraversalCost { get; set; }
        public IActor Occupant { get; set; }
        public TileTypeOptions TileType { get; set; }
    }
}
