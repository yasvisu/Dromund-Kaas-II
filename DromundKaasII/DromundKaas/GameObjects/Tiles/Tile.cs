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
        public double TraversalCost { get; set; }
        public Actor Occupant { get; set; }
        public TileTypeOptions TileType { get; set; }
    }
}
