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
using DromundKaasII.GameObjects.Skills;
using DromundKaasII.Input;
using DromundKaasII.GameObjects.Actors.Players;
namespace DromundKaasII.Engine
{
    public class Engine : IEngine
    {
        protected uint cycleCounter;
        protected TimeSpan elapsedTime;

        public Engine()
        {
            this.GameState = new GameState(10, 10);
            this.IsRunning = true;
            this.cycleCounter = 0;
            this.elapsedTime = new TimeSpan();
            this.SkillManager = new SkillManager();
            this.GameState.Actors.Add(new Primal(new Vector2(3,3)));
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

        public SkillManager SkillManager { get; private set; }

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
            // Tell all actors to think of a next move
            foreach (Actor a in this.GameState.Actors)
            {
                a.RemoveExpiredStatusEffects();
                a.Act(this.GameState);
            }

            // Move / act all actors based on their desired move
            foreach (Actor a in this.GameState.Actors)
            {
                switch (a.DesiredAction)
                {
                    case ActionTypeOptions.Move:
                        MoveActor(a, a.GroundTarget);
                        break;
                    case ActionTypeOptions.SkillActor:
                        EnactSkill(a, a.Target);
                        break;
                    case ActionTypeOptions.SkillGround:
                        EnactSkill(a, a.GroundTarget);
                        break;
                    default:
                        break;
                }
            }


            // Prune dead actors
            Stack<Actor> GarbageCan = new Stack<Actor>();
            foreach (Actor a in this.GameState.Actors)
            {
                if (a.Stats.Health <= 0)
                {
                    GarbageCan.Push(a);
                }
            }
            while (GarbageCan.Count > 0)
            {
                Actor temp = GarbageCan.Pop();
                GameState.TranspiredEvents.Enqueue(new ActorStateEvent(ActorEvents.Death, temp));
                this.GameState.Actors.Remove(temp);
            }
        }

        private void EnactSkill(Actor parent, Vector2 target)
        {
            // Check whether Skill can target ground, then enact Skill.
            throw new NotImplementedException();
        }

        private void EnactSkill(Actor parent, Actor target)
        {
            // Check whether Actor is within range of other Actor (by Skill), then enact skill.
            throw new NotImplementedException();
        }

        private void MoveActor(Actor parent, Vector2 target)
        {
            // Check whether Actor can move on tile, then move Actor.
            if (parent.Stats.TraversalPower >= GameState.Map[(int)target.Y, (int)target.X].TraversalCost && GameState.Map[(int)target.Y, (int)target.X] != null)
            {
                GameState.Map[(int)parent.MapPosition.Y, (int)parent.MapPosition.X] = null;
                parent.MapPosition = target;
                GameState.Map[(int)target.Y, (int)target.X].Occupant = parent;
            }
        }
    }
}
