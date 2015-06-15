using DromundKaasII.GameObjects;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DromundKaasII.Engine
{
    public class Engine : IEngine
    {

        ConcurrentQueue<ActorStateEvent> transpiredEvents;
        private int cycleCounter;
        private TimeSpan elapsedTime;

        public Engine()
        {
            this.GameState = new GameState(10, 10);
            this.transpiredEvents = new ConcurrentQueue<ActorStateEvent>();
            this.IsRunning = true;
            cycleCounter = 0;
            elapsedTime = new TimeSpan();
        }

        public GameState GameState { get; set; }

        public IEnumerable<ActorStateEvent> TranspiredEvents
        {
            get { return this.transpiredEvents; }
        }

        public bool IsRunning { get; set; }

        public int CycleCounter
        {
            get
            {
                return this.cycleCounter;
            }
        }

        public void Step(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime.TotalMilliseconds< ((int)this.GameState.GameSpeed) * cycleCounter)
            {
                return;
            }
            elapsedTime = new TimeSpan();
            cycleCounter++;
        }

    }
}
