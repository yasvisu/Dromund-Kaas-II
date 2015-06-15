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
using DromundKaasII.GameObjects.Actors;
using DromundKaasII.GameObjects.Enums;
namespace DromundKaasII.Engine
{
    public class Engine : IEngine
    {

        
        private uint cycleCounter;
        private TimeSpan elapsedTime;

        public Engine()
        {
            this.GameState = new GameState(10, 10);
            this.IsRunning = true;
            cycleCounter = 0;
            elapsedTime = new TimeSpan();
        }

        public GameState GameState { get; set; }



        public bool IsRunning { get; set; }

        public uint CycleCounter
        {
            get
            {
                return this.cycleCounter;
            }
        }

        public void Step(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime.TotalMilliseconds < ((int)this.GameState.GameSpeed) * cycleCounter)
            {
                return;
            }
            elapsedTime = new TimeSpan();
            cycleCounter++;

            UpdateGameState();
        }

        public void UpdateGameState()
        {
            Stack<Actor> GarbageCan = new Stack<Actor>();
            foreach (Actor a in this.GameState.Actors)
            {
                a.RemoveExpiredStatusEffects();
                a.Act(this.GameState);
                if (a.Health <= 0)
                {
                    GarbageCan.Push(a);
                }
            }
            while (GarbageCan.Count > 0)
            {
                var frag = GarbageCan.Pop();
                this.GameState.TranspiredEvents.Enqueue(new ActorStateEvent(ActorEvents.Death, frag));
                this.GameState.Actors.Remove(frag);
            }
        }
    }
}
