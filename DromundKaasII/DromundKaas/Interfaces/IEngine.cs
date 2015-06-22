using DromundKaasII.Engine;
using DromundKaasII.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Interfaces
{
    public interface IEngine
    {
        GameState GameState { get; }

        bool IsRunning { get; set; }
        bool IsPaused { get; set; }

        void Update();
    }
}
