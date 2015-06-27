using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DromundKaasII.Engine.Exceptions;
using DromundKaasII.Engine.GameObjects;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Engine.GameObjects.Actors.Debris;
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
            this.GameState = new GameState(7, 7);
            this.GameState.GameSpeed = GameSpeedOptions.Fast;
            this.IsRunning = true;
            this.cycleCounter = 0;
            this.elapsedTime = new TimeSpan();
            this.SkillManager = new SkillManager();
            this.actorFactory = new ActorFactory(this.GameState);

            for (int i = 0; i < this.GameState.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.GameState.Map.GetLength(1); j++)
                {
                    Tile temp = new Tile(100, TileTypeOptions.Ground, this.GameState.FogOfWar);
                    this.GameState.Map[i, j] = temp;
                }
            }

            // Sample water tile!
            this.GameState.Map[1, 1] = new Tile(200, TileTypeOptions.Water, this.GameState.FogOfWar);
            this.GameState.Map[3, 1] = new Tile(1000, TileTypeOptions.Hole, this.GameState.FogOfWar);
            this.GameState.Map[1, 3] = new Tile(700, TileTypeOptions.Wall, this.GameState.FogOfWar);
            this.GameState.Map[3, 3] = new Tile(500, TileTypeOptions.Tree, this.GameState.FogOfWar);

            Player p = new Primal(new Vector2(2, 2), SkillManager.Skills);
            actorFactory.CreatePlayer(p);

            ZombieFriend z = new ZombieFriend(new Vector2(5, 5), SkillManager.Skills);
            actorFactory.CreateActor(z);

            Campfire c = new Campfire(new Vector2(6, 6), new Statblock(8));
            actorFactory.CreateActor(c);
        }

        protected GameState GameState { get; set; }

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
                return this.GameState.Player;
            }
        }
        public ConcurrentQueue<ActorStateEvent> TranspiredEvents
        {
            get
            {
                return this.GameState.TranspiredEvents;
            }
        }
        public GameSpeedOptions GameSpeed
        {
            get
            {
                return this.GameState.GameSpeed;
            }
            set
            {
                this.GameState.GameSpeed = value;
            }
        }
        public GameDifficultyOptions GameDifficulty
        {
            get
            {
                return this.GameState.GameDifficulty;
            }
            set
            {
                this.GameState.GameDifficulty = value;
            }
        }
        public IEnumerable<IActor> Actors
        {
            get
            {
                return this.GameState.Actors;
            }
        }
        public IEnumerable<IIlluminator> Illuminators
        {
            get
            {
                return this.GameState.Illuminators;
            }
        }
        public ITile[,] Map
        {
            get
            {
                return this.GameState.Map;
            }
        }
        public int MapHeight
        {
            get
            {
                return this.GameState.MapHeight;
            }
        }
        public int MapWidth
        {
            get
            {
                return this.GameState.MapWidth;
            }
        }
        public Color FogOfWar
        {
            get
            {
                return this.GameState.FogOfWar;
            }
        }

        public void Update()
        {
            cycleCounter++;

            PruneAllActors();

            SpawnNewActors();

            ProcessAllActors();

            ProcessAllIlluminators();

            ActAllActors();
        }

        #endregion


        #region State Changers


        private void PruneAllActors()
        {
            // Prune dead actors
            Stack<Actor> NpcGarbageCan = new Stack<Actor>();

            foreach (Actor a in this.GameState.Actors)
            {
                if (a.Stats.Health <= 0)
                {
                    if (!(a is Player))
                    {
                        this.TranspiredEvents.Enqueue(new ActorStateEvent(ActorEvents.Death, a));
                        NpcGarbageCan.Push(a);

                        AwardExp(this.Player, a);

                        if (a is IIlluminator)
                        {
                            RemoveIllumination(a as IIlluminator);
                        }
                    }
                }
            }

            while (NpcGarbageCan.Count > 0)
            {
                actorFactory.RemoveActor(NpcGarbageCan.Pop());

            }
        }

        private void SpawnNewActors()
        {
            while (this.GameState.SpawnQueue.Count > 0)
            {
                Actor candidate;
                bool success = this.GameState.SpawnQueue.TryDequeue(out candidate);
                if (success && candidate != null)
                {
                    this.actorFactory.CreateActor(candidate);
                }
            }
        }

        private void ProcessAllActors()
        {
            foreach (Actor a in this.GameState.Actors)
            {
                a.RemoveExpiredStatusEffects();
                // Tell all actors to think of a next move
                a.Act(this.GameState);
            }
        }

        private void ProcessAllIlluminators()
        {
            foreach (var illum in this.Illuminators)
            {
                if (true||!illum.HasIlluminated)
                {
                    IlluminateMap(illum);
                }
            }
        }

        private void ActAllActors()
        {
            // Move / act all actors based on their desired move
            foreach (Actor a in this.GameState.Actors)
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


        private void AwardExp(IPlayer player, IActor a)
        {
            // player.Stats.Experience += Calculator.CalculateExperience(a, player);
            player.Stats.Experience += 100;
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

            if (toEnact == null || toEnact.Name == "None")
            {
                return;
            }


            Vector2 skillEffectLocation = GetGroundTarget(parent.MapPosition, DirectionToUnitVector(parent.Direction), toEnact.Range);

            if (skillEffectLocation.X < 0 || skillEffectLocation.X >= this.GameState.MapWidth ||
                skillEffectLocation.Y < 0 || skillEffectLocation.Y >= this.GameState.MapHeight)
            {
                return;
            }


            if (toEnact.FocusCost > Player.Stats.Focus || toEnact.ManaCost > Player.Stats.Mana)
            {
                return;
            }

            parent.SpendSkill(toEnact);

            if (toEnact.SkillType == SkillTypes.Summon)
            {
                bool summoningResult = HandleSummonSkill(toEnact, skillEffectLocation);
                if (!summoningResult)
                {
                    return;
                }
            }
            else
            {
                Actor target = this.GameState.Map[(int)skillEffectLocation.Y, (int)skillEffectLocation.X].Occupant as Actor;
                if (target != null)
                {
                    target.ReactToSkill(toEnact);
                }
            }
        }

        private Vector2 DirectionToUnitVector(Directions D)
        {
            switch (D)
            {
                case Directions.North:
                    return UpV;
                case Directions.South:
                    return DownV;
                case Directions.East:
                    return RightV;
                case Directions.West:
                    return LeftV;
                default:
                    return Vector2.Zero;
            }
        }

        private Vector2 GetGroundTarget(Vector2 Current, Vector2 Movement, int Range)
        {
            return Current + Movement * Range;
        }

        private bool HandleSummonSkill(Skill toEnact, Vector2 spawnLocation)
        {
            if (toEnact == null)
            {
                throw new ArgumentNullException("Skill cannot be null.");
            }

            if (toEnact.SkillType != SkillTypes.Summon)
            {
                throw new InvalidSkillTypeException("Cannot handle non-summon skill.");
            }

            if (this.GameState.Map[(int)spawnLocation.X, (int)spawnLocation.Y].Occupant != null)
            {
                return false;
            }

            // Works for campfires.
            if (toEnact.Name == "Start Fire")
            {
                var fire = new Campfire(spawnLocation, toEnact.Effect as Statblock);
                this.GameState.SpawnQueue.Enqueue(fire);
            }

            return true;
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
                if (parent is IIlluminator)
                {
                    RemoveIllumination(parent as IIlluminator);
                }

                GameState.Map[(int)parent.MapPosition.Y, (int)parent.MapPosition.X].Occupant = null;
                parent.MapPosition = target;
                GameState.Map[(int)target.Y, (int)target.X].Occupant = parent;

                if (parent is IIlluminator)
                {
                    IlluminateMap(parent as IIlluminator);
                }
            }
        }



        // Illumination

        private void IlluminateMap(IIlluminator I)
        {
            if (I.HasIlluminated)
            {
                return;
            }
            I.HasIlluminated = true;

            Vector2 topLeft = new Vector2(I.MapPosition.X - I.IlluminationRange, I.MapPosition.Y - I.IlluminationRange);
            Vector2 bottomRight = new Vector2(I.MapPosition.X + I.IlluminationRange, I.MapPosition.Y + I.IlluminationRange);

            Rectangle IllumRect = new Rectangle(topLeft.ToPoint(), bottomRight.ToPoint());


            for (int i = IllumRect.Top; i <= IllumRect.Bottom; i++)
            {
                for (int j = IllumRect.Left; j <= IllumRect.Right; j++)
                {
                    if (i >= 0 && i < this.MapHeight &&
                        j >= 0 && j < this.MapWidth &&
                        Distance(new Vector2(j, i), I.MapPosition) <= I.IlluminationRange)
                    {
                        if (this.GameState.Map[i, j].Illumination == this.GameState.FogOfWar)
                        {
                            this.GameState.Map[i, j].Illumination = I.IlluminationColor;
                        }
                    }
                }
            }
        }

        private double Distance(Vector2 a, Vector2 b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        private void RemoveIllumination(IIlluminator I)
        {
            if (!I.HasIlluminated)
            {
                return;
            }

            Vector2 topLeft = new Vector2(I.MapPosition.X - I.IlluminationRange, I.MapPosition.Y - I.IlluminationRange);
            Vector2 bottomRight = new Vector2(I.MapPosition.X + I.IlluminationRange, I.MapPosition.Y + I.IlluminationRange);

            Rectangle IllumRect = new Rectangle(topLeft.ToPoint(), bottomRight.ToPoint());


            for (int i = IllumRect.Top; i <= IllumRect.Bottom; i++)
            {
                for (int j = IllumRect.Left; j <= IllumRect.Right; j++)
                {
                    if (i >= 0 && i < this.MapHeight &&
                        j >= 0 && j < this.MapWidth &&
                        Distance(new Vector2(j, i), I.MapPosition) <= I.IlluminationRange)
                    {
                        var tempColor = this.GameState.Map[i, j].Illumination;
                        if (tempColor == I.IlluminationColor)
                        {
                            this.GameState.Map[i, j].Illumination = this.GameState.FogOfWar;
                        }
                    }
                }
            }
            I.HasIlluminated = false;
        }





        #endregion
    }
}
