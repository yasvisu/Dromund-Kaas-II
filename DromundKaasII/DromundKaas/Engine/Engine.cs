using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DromundKaasII.Engine.Exceptions;
using DromundKaasII.Engine.GameObjects;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Engine.GameObjects.Actors.NPCs;
using DromundKaasII.Engine.GameObjects.Actors.Players;
using DromundKaasII.Engine.GameObjects.Skills;
using DromundKaasII.Engine.GameObjects.Tiles;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

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
            this.actorFactory = new ActorFactory(this.gameState);

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
        public ConcurrentQueue<ActorStateEvent> TranspiredEvents
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
        public ITile[,] Map
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

            ProcessAllActors();

            ActAllActors();

            PruneAllActors();
        }

        #endregion



        #region State Changers

        private void ProcessAllActors()
        {
            foreach (Actor a in this.gameState.Actors)
            {
                a.RemoveExpiredStatusEffects();
                // Tell all actors to think of a next move
                a.Act(this.gameState);
            }
        }

        private void ActAllActors()
        {
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
                    case GameInputs.A1:
                    case GameInputs.A2:
                    case GameInputs.A3:
                    case GameInputs.A4:
                    case GameInputs.A5:
                        EnactSkill(a);
                        break;
                    default:
                        break;
                }
                a.DesiredAction = GameInputs.None;
            }
        }

        private void PruneAllActors()
        {
            // Prune dead actors
            Stack<Npc> NpcGarbageCan = new Stack<Npc>();

            foreach (Actor a in this.gameState.Actors)
            {
                if (a.Stats.Health <= 0)
                {
                    if (a is Npc)
                    {
                        this.TranspiredEvents.Enqueue(new ActorStateEvent(ActorEvents.Death, a));
                        NpcGarbageCan.Push(a as Npc);
                    }
                }
            }

            while (NpcGarbageCan.Count > 0)
            {
                actorFactory.RemoveNpc(NpcGarbageCan.Pop());
            }
        }

        private void EnactSkill(Actor parent)
        {
            Skill toEnact;
            switch (parent.DesiredAction)
            {
                case GameInputs.A1:
                    toEnact = parent.Skills[0];
                    break;
                case GameInputs.A2:
                    toEnact = parent.Skills[1];
                    break;
                case GameInputs.A3:
                    toEnact = parent.Skills[2];
                    break;
                case GameInputs.A4:
                    toEnact = parent.Skills[3];
                    break;
                case GameInputs.A5:
                    toEnact = parent.Skills[4];
                    break;
                default:
                    throw new UnsupportedKeyException("Invalid key corresponding to skill.");
            }
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
