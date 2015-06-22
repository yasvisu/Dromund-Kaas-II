using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using DromundKaasII.Input;
using DromundKaasII.Interfaces;
using DromundKaasII.Engine.GameObjects;
using DromundKaasII.Engine.GameObjects.Enums;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Engine.GameObjects.Tiles;

namespace DromundKaasII.Engine
{
    public class GameState
    {

        public GameState(int X, int Y)
        {
            this.GameSpeed = GameSpeedOptions.Fast;
            this.Actors = new List<Actor>();
            this.Map = new Tile[X, Y];
            this.TranspiredEvents = new Queue<ActorStateEvent>();
        }

        public IPlayer Player { get; set; }
        public Queue<ActorStateEvent> TranspiredEvents { get; private set; }
        public GameSpeedOptions GameSpeed { get; set; }
        public GameDifficultyOptions GameDifficulty { get; set; }
        public List<Actor> Actors { get; set; }
        public Tile[,] Map { get; set; }
        public int MapHeight
        {
            get
            {
                return this.Map.GetLength(0);
            }
        }
        public int MapWidth
        {
            get
            {
                return this.Map.GetLength(1);
            }
        }
    }
}
