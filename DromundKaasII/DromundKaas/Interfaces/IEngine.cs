using DromundKaasII.Engine;
using DromundKaasII.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.GameObjects.Enums;

namespace DromundKaasII.Interfaces
{
    public interface IEngine : IEngineOptions
    {
        bool IsRunning { get; set; }
        bool IsPaused { get; set; }

        IPlayer Player { get; }
        Queue<ActorStateEvent> TranspiredEvents { get; }
        IEnumerable<IActor> Actors { get; }
        IPathable[,] Map { get; }
        int MapHeight { get; }
        int MapWidth { get; }

        void Update();
    }
}
