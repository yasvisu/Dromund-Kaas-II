using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine;
using DromundKaasII.Engine.GameObjects;

namespace DromundKaasII.Interfaces
{
    public interface IEngine : IEngineOptions
    {
        bool IsRunning { get; set; }
        bool IsPaused { get; set; }

        IPlayer Player { get; }
        Queue<ActorStateEvent> TranspiredEvents { get; }
        IEnumerable<IActor> Actors { get; }
        ITile[,] Map { get; }
        int MapHeight { get; }
        int MapWidth { get; }

        void Update();
    }
}
