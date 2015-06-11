using DromundKaasII.GameObjects.Actors;
using DromundKaasII.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine
{
    public class GameState
    {
        public Vector2[] PlayerPositions;
        public Actor[] Actors { get; set; }
        public Tile[,] Map { get; set; }
    }
}
