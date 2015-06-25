using System.Collections.Concurrent;
using System.Collections.Generic;
using DromundKaasII.Engine.GameObjects;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Engine.GameObjects.Tiles;
using DromundKaasII.Engine.Interfaces;
using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine
{
    /// <summary>
    /// Represents a gamestate with all the appropriate variables.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Default constructor, setting a Fast game speed.
        /// </summary>
        /// <param name="X">The width of the game map.</param>
        /// <param name="Y">The height of the game map.</param>
        public GameState(int X, int Y)
        {
            this.GameSpeed = GameSpeedOptions.Fast;
            this.Actors = new List<Actor>();
            this.Illuminators = new List<IIlluminator>();
            this.Map = new Tile[X, Y];
            this.TranspiredEvents = new ConcurrentQueue<ActorStateEvent>();
        }

        /// <summary>
        /// The main player of this GameState.
        /// </summary>
        public IPlayer Player { get; set; }

        /// <summary>
        /// A collection of events that have occurred.
        /// </summary>
        public ConcurrentQueue<ActorStateEvent> TranspiredEvents { get; private set; }

        /// <summary>
        /// The speed option of the GameState.
        /// </summary>
        public GameSpeedOptions GameSpeed { get; set; }

        /// <summary>
        /// The difficulty option of the GameState.
        /// </summary>
        public GameDifficultyOptions GameDifficulty { get; set; }

        /// <summary>
        /// A list of the actors in the GameState.
        /// </summary>
        public List<Actor> Actors { get; set; }

        public List<IIlluminator> Illuminators { get; set; }

        /// <summary>
        /// The map of the GameState.
        /// </summary>
        public IPathable[,] Map { get; set; }

        /// <summary>
        /// The height of the map of the GameState.
        /// </summary>
        public int MapHeight
        {
            get
            {
                return this.Map.GetLength(0);
            }
        }

        /// <summary>
        /// The width of the map of the GameState.
        /// </summary>
        public int MapWidth
        {
            get
            {
                return this.Map.GetLength(1);
            }
        }
    }
}
