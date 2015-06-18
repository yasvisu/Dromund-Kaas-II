using DromundKaasII.GameObjects.Actors;
using DromundKaasII.GameObjects.Enums;
using DromundKaasII.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.GameObjects;

namespace DromundKaasII.Engine
{
    public class GameState
    {
        protected Queue<ActorStateEvent> transpiredEvents;

        public GameState(int X, int Y)
        {
            this.GameSpeed = GameSpeedOptions.Fast;
            this.Actors = new List<Actor>();
            this.Map = new Tile[X, Y];
            this.transpiredEvents = new Queue<ActorStateEvent>();
        }

        //public Player
        public Queue<ActorStateEvent> TranspiredEvents
        {
            get { return this.transpiredEvents; }
        }
        public GameSpeedOptions GameSpeed { get; set; }
        public GameDifficultyOptions GameDifficulty { get; set; }
        public List<Actor> Actors { get; set; }
        public Tile[,] Map { get; set; }
    }
}
