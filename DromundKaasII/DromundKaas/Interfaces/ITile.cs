using DromundKaasII.GameObjects.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.GameObjects.Tiles;

namespace DromundKaasII.Interfaces
{
    public interface ITile
    {
        double TraversalCost { get; }
        IActor Occupant { get; }
        TileTypeOptions TileType { get; }
    }
}
