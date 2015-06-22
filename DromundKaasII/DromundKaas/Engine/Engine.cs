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
using DromundKaasII.GameObjects.Actors.NPCs;
namespace DromundKaasII.Engine
{
    public class Engine : IEngine
    {
        protected uint cycleCounter;
        protected TimeSpan elapsedTime;
        protected ActorFactory actorFactory;

        #region Vector Constants
        protected static readonly Vector2 UpV = new Vector2(0, -1);
        protected static readonly Vector2 DownV = new Vector2(0, 1);
        protected static readonly Vector2 LeftV = new Vector2(-1, 0);
        protected static readonly Vector2 RightV = new Vector2(1, 0);

        protected static readonly Vector2 UpLeftV = UpV + LeftV;
        protected static readonly Vector2 UpRightV = UpV + RightV;
        protected static readonly Vector2 DownLeftV = DownV + LeftV;
        protected static readonly Vector2 DownRightV = DownV + RightV;
        #endregion

        public Engine()
        {
            this.gameState = new GameState(7, 7);
            this.gameState.GameSpeed = GameSpeedOptions.Fast;
            this.IsRunning = true;
            this.cycleCounter = 0;
            this.elapsedTime = new TimeSpan();
            this.SkillManager = new SkillManager();
            actorFactory = new ActorFactory(gameState);

            for (int i = 0; i < this.gameState.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.gameState.Map.GetLength(1); j++)
                {
                    Tile temp = new Tile(100, TileTypeOptions.Ground);
                    this.gameState.Map[i, j] = temp;
                }
            }

            // Sample water tile!
            this.gameState.Map[1, 1] = new Tile(200, TileTypeOptions.Water);
            this.gameState.Map[3, 1] = new Tile(1000, TileTypeOptions.Hole);
            this.gameState.Map[1, 3] = new Tile(700, TileTypeOptions.Wall);
            this.gameState.Map[3, 3] = new Tile(500, TileTypeOptions.Tree);

            Player p = new Primal(new Vector2(2, 2));
            actorFactory.CreatePlayer(p);

            ZombieFriend z = new ZombieFriend(new Vector2(5, 5));
            actorFactory.CreateNpc(z);
        }

        protected GameState gameState { get; set; }

        #region Public Interface
        public bool IsRunning { get; set; }

        public bool IsPaused { get; set; }

        public uint CycleCounter
        {
            get
            {
                return this.cycleCounter;
            }
        }

        public SkillManager SkillManager { get; private set; }

        public IPlayer Player
        {
            get
            {
                return this.gameState.Player;
            }
        }
        public Queue<ActorStateEvent> TranspiredEvents
        {
            get
            {
                return this.gameState.TranspiredEvents;
            }
        }
        public GameSpeedOptions GameSpeed
        {
            get
            {
                return this.gameState.GameSpeed;
            }
            set
            {
                this.gameState.GameSpeed = value;
            }
        }
        public GameDifficultyOptions GameDifficulty
        {
            get
            {
                return this.gameState.GameDifficulty;
            }
            set
            {
                this.gameState.GameDifficulty = value;
            }
        }
        public IEnumerable<IActor> Actors
        {
            get
            {
                return this.gameState.Actors;
            }
        }
        public IPathable[,] Map
        {
            get
            {
                return this.gameState.Map;
            }
        }
        public int MapHeight
        {
            get
            {
                return this.gameState.MapHeight;
            }
        }
        public int MapWidth
        {
            get
            {
                return this.gameState.MapWidth;
            }
        }

        public void Update()
        {
            cycleCounter++;
            // Tell all actors to think of a next move
            foreach (Actor a in this.gameState.Actors)
            {
                a.RemoveExpiredStatusEffects();
                a.Act(this.gameState);
            }

            // Move / act all actors based on their desired move
            foreach (Actor a in this.gameState.Actors)
            {
                switch (a.DesiredAction)
                {
                    case GameInputs.Up:
                        MoveActor(a, a.MapPosition + UpV);
                        break;
                    case GameInputs.Down:
                        MoveActor(a, a.MapPosition + DownV);
                        break;
                    case GameInputs.Left:
                        MoveActor(a, a.MapPosition + LeftV);
                        break;
                    case GameInputs.Right:
                        MoveActor(a, a.MapPosition + RightV);
                        break;
                    default:
                        break;
                }
                a.DesiredAction = GameInputs.None;
            }


            // Prune dead actors
            foreach (Actor a in this.gameState.Actors)
            {
                if (a.Stats.Health <= 0)
                {
                    if (a is Npc)
                    {
                        actorFactory.RemoveNpc(a as Npc);
                    }
                }
            }
        }

        #endregion

        #region State Changers
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
            if (target.X < 0 || target.X >= gameState.MapWidth || target.Y < 0 || target.Y >= gameState.MapHeight)
            {
                return;
            }
            if (parent.Stats.TraversalPower >= gameState.Map[(int)target.Y, (int)target.X].TraversalCost &&
                gameState.Map[(int)target.Y, (int)target.X].Occupant == null)
            {
                gameState.Map[(int)parent.MapPosition.Y, (int)parent.MapPosition.X].Occupant = null;
                parent.MapPosition = target;
                gameState.Map[(int)target.Y, (int)target.X].Occupant = parent;
            }

        }
        #endregion
    }
}
