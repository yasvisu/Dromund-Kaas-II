using System.Collections.Concurrent;
using System.Collections.Generic;
using DromundKaasII.Engine;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Exposes the control functions of the engine.
    /// </summary>
    public interface IEngine : IEngineOptions
    {
        /// <summary>
        /// Whether the engine is running or set for exit.
        /// </summary>
        bool IsRunning { get; set; }

        /// <summary>
        /// Whether the engine is momentarily paused.
        /// </summary>
        bool IsPaused { get; set; }

        /// <summary>
        /// The player.
        /// </summary>
        IPlayer Player { get; }

        /// <summary>
        /// The events that have occurred so far.
        /// </summary>
        ConcurrentQueue<ActorStateEvent> TranspiredEvents { get; }

        /// <summary>
        /// A collection of actors that are in the game state.
        /// </summary>
        IEnumerable<IActor> Actors { get; }

        IEnumerable<IIlluminator> Illuminators { get; }

        /// <summary>
        /// The matrix of tiles which are the game map.
        /// </summary>
        ITile[,] Map { get; }

        /// <summary>
        /// The height of the game map.
        /// </summary>
        int MapHeight { get; }

        /// <summary>
        /// The width of the game map.
        /// </summary>
        int MapWidth { get; }

        Color FogOfWar { get; }

        /// <summary>
        /// Updates the engine game state.
        /// </summary>
        void Update();
    }
}
