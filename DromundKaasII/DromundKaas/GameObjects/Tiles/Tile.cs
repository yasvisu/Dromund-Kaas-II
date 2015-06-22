using DromundKaasII.GameObjects.Actors;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.GameObjects.Enums;

namespace DromundKaasII.GameObjects.Tiles
{
    public class Tile : IPathable
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
