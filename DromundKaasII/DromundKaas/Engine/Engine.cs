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
using DromundKaasII.GameObjects.Tiles;
namespace DromundKaasII.Engine
{
    public class Engine : IEngine
    {
        protected uint cycleCounter;
        protected TimeSpan elapsedTime;

        public Engine()
        {
            this.GameState = new GameState(7, 7);
            this.IsRunning = true;
            this.cycleCounter = 0;
            this.elapsedTime = new TimeSpan();
            this.SkillManager = new SkillManager();


            for (int i = 0; i < this.GameState.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.GameState.Map.GetLength(1); j++)
                {
                    Tile temp = new Tile(100, TileTypeOptions.Ground);
                    this.GameState.Map[i, j] = temp;
                }
            }

            // Sample water tile!
            this.GameState.Map[1, 1] = new Tile(200, TileTypeOptions.Water);
            this.GameState.Map[3, 1] = new Tile(1000, TileTypeOptions.Hole);
            this.GameState.Map[1, 3] = new Tile(700, TileTypeOptions.Wall);
            this.GameState.Map[3, 3] = new Tile(500, TileTypeOptions.Tree);

            Player p = new Primal(new Vector2(2, 2));
            this.GameState.Actors.Add(p);
            this.GameState.Player = p;
            this.GameState.Map[(int)p.MapPosition.Y, (int)p.MapPosition.X].Occupant = p;
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

        public void UpdateGameState()
        {
            cycleCounter++;
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
                    case GameInputs.Up:
                        MoveActor(a, new Vector2(a.MapPosition.X, a.MapPosition.Y - 1));
                        break;
                    case GameInputs.Down:
                        MoveActor(a, new Vector2(a.MapPosition.X, a.MapPosition.Y + 1));
                        break;
                    case GameInputs.Left:
                        MoveActor(a, new Vector2(a.MapPosition.X - 1, a.MapPosition.Y));
                        break;
                    case GameInputs.Right:
                        MoveActor(a, new Vector2(a.MapPosition.X + 1, a.MapPosition.Y));
                        break;
                    default:
                        break;
                }
                a.DesiredAction = GameInputs.None;
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
            if (target.X < 0 || target.X >= GameState.MapWidth || target.Y < 0 || target.Y >= GameState.MapHeight)
            {
                return;
            }
            if (parent.Stats.TraversalPower >= GameState.Map[(int)target.Y, (int)target.X].TraversalCost &&
                GameState.Map[(int)target.Y, (int)target.X].Occupant == null)
            {
                GameState.Map[(int)parent.MapPosition.Y, (int)parent.MapPosition.X].Occupant = null;
                parent.MapPosition = target;
                GameState.Map[(int)target.Y, (int)target.X].Occupant = parent;
            }

        }
    }
}
