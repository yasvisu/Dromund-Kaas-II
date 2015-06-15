using DromundKaasII.GameObjects.Actors;
using DromundKaasII.GameObjects.Enums;
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
        public GameSpeed GameSpeed;

        public GameState(int X, int Y)
        {
            this.GameSpeed = GameSpeed.Fast;
            this.Actors = new List<Actor>();
            this.Map = new Tile[X, Y];
        }

        //public Player
        public List<Actor> Actors { get; set; }
        public Tile[,] Map { get; set; }
    }
}
