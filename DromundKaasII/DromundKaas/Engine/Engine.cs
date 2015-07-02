using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using DromundKaasII.Engine.Exceptions;
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
    /// <summary>
    /// Engine for all processing within the game.
    /// </summary>
    public class Engine : IEngine
    {
        /// <summary>
        /// The cycle counter of the engine.
        /// </summary>
        protected uint cycleCounter;

        /// <summary>
        /// The factory the Engine uses.
        /// </summary>
        protected ActorFactory actorFactory;

        #region Vector Constants
        /// <summary>
        /// Unit vector Up.
        /// </summary>
        protected static readonly Vector2 UpV = new Vector2(0, -1);

        /// <summary>
        /// Unit vector Down.
        /// </summary>
        protected static readonly Vector2 DownV = new Vector2(0, 1);

        /// <summary>
        /// Unit vector Left.
        /// </summary>
        protected static readonly Vector2 LeftV = new Vector2(-1, 0);

        /// <summary>
        /// Unit vector Right.
        /// </summary>
        protected static readonly Vector2 RightV = new Vector2(1, 0);

        /// <summary>
        /// Unit vector Up-Left diagonal.
        /// </summary>
        protected static readonly Vector2 UpLeftV = UpV + LeftV;

        /// <summary>
        /// Unit vector Up-Right diagonal.
        /// </summary>
        protected static readonly Vector2 UpRightV = UpV + RightV;

        /// <summary>
        /// Unit vector Down-Left diagonal.
        /// </summary>
        protected static readonly Vector2 DownLeftV = DownV + LeftV;

        /// <summary>
        /// Unit vector Down-Right diagonal.
        /// </summary>
        protected static readonly Vector2 DownRightV = DownV + RightV;
        #endregion

        /// <summary>
        /// Initialize new Engine with default values.
        /// </summary>
        public Engine()
        {
            this.GameState = new GameState(33, 33);
            this.GameState.GameSpeed = GameSpeedOptions.Fast;
            this.GameState.GameDifficulty = GameDifficultyOptions.Easy;
            this.IsRunning = true;
            this.cycleCounter = 0;
            this.SkillManager = new SkillManager();
            this.actorFactory = new ActorFactory(this.GameState);

            Random ra = new Random();
            int n;
            for (int i = 0; i < this.GameState.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.GameState.Map.GetLength(1); j++)
                {
                    n = ra.Next(100);
                    Tile temp;
                    if (n <= 1)
                    {
                        temp = new Tile(1000, TileTypeOptions.Hole, this.GameState.FogOfWar);
                    }
                    else if (n <= 2)
                    {
                        temp = new Tile(700, TileTypeOptions.Wall, this.GameState.FogOfWar);
                    }
                    else if (n <= 6)
                    {
                        temp = new Tile(200, TileTypeOptions.Water, this.GameState.FogOfWar);
                    }
                    else if (n <= 10)
                    {
                        temp = new Tile(500, TileTypeOptions.Tree, this.GameState.FogOfWar);
                    }
                    else
                    {
                        temp = new Tile(100, TileTypeOptions.Ground, this.GameState.FogOfWar);
                    }
                    this.GameState.Map[i, j] = temp;
                }
            }

            this.GameState.Map[(int)this.GameState.MapWidth / 2, (int)this.GameState.MapHeight / 2] = new Tile(100, TileTypeOptions.Ground, this.GameState.FogOfWar);

            Player p = new Primal(new Vector2((int)this.GameState.MapWidth / 2, (int)this.GameState.MapHeight / 2), SkillManager.Skills);
            this.actorFactory.CreatePlayer(p);

            //ZombieFriend z = new ZombieFriend(new Vector2(5, 5), SkillManager.Skills);
            //this.actorFactory.CreateActor(z);
        }

        /// <summary>
        /// The GameState the Engine uses.
        /// </summary>
        protected GameState GameState { get; set; }

        #region Public Interface
        /// <summary>
        /// Whether the Engine is running.
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// The time stamp of the last Update call.
        /// </summary>
        public DateTime LastCalled { get; private set; }

        /// <summary>
        /// Whether the Engine is paused.
        /// </summary>
        public bool IsPaused { get; set; }

        /// <summary>
        /// The CycleCounter of the Engine.
        /// </summary>
        public uint CycleCounter
        {
            get
            {
                return this.cycleCounter;
            }
        }

        /// <summary>
        /// The SkillManager the Engine uses.
        /// </summary>
        public SkillManager SkillManager { get; private set; }

        /// <summary>
        /// The Player of the Engine's GameState.
        /// </summary>
        public IPlayer Player
        {
            get
            {
                return this.GameState.Player;
            }
        }

        /// <summary>
        /// The transpired events since last Engine Update.
        /// </summary>
        public ConcurrentQueue<ActorStateEvent> TranspiredEvents
        {
            get
            {
                return this.GameState.TranspiredEvents;
            }
        }

        /// <summary>
        /// The GameSpeed of the Engine.
        /// </summary>
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

        /// <summary>
        /// The GameDifficulty of the Engine.
        /// </summary>
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

        /// <summary>
        /// A collection of the Actors in the Engine.
        /// </summary>
        public IEnumerable<IActor> Actors
        {
            get
            {
                return this.GameState.Actors;
            }
        }

        /// <summary>
        /// A collection of the Illuminators of the Engine.
        /// </summary>
        public IEnumerable<IIlluminator> Illuminators
        {
            get
            {
                return this.GameState.Illuminators;
            }
        }

        /// <summary>
        /// The Tile Map of the Engine.
        /// </summary>
        public ITile[,] Map
        {
            get
            {
                return this.GameState.Map;
            }
        }

        /// <summary>
        /// The height of the Engine's Map.
        /// </summary>
        public int MapHeight
        {
            get
            {
                return this.GameState.MapHeight;
            }
        }

        /// <summary>
        /// The width of the Engine's Map.
        /// </summary>
        public int MapWidth
        {
            get
            {
                return this.GameState.MapWidth;
            }
        }

        /// <summary>
        /// The default Fog of War color.
        /// </summary>
        public Color FogOfWar
        {
            get
            {
                return this.GameState.FogOfWar;
            }
        }

        /// <summary>
        /// Update the Engine.
        /// </summary>
        public void Update()
        {
            this.LastCalled = DateTime.Now;
            this.cycleCounter++;

            this.PruneAllActors();

            this.SpawnNewActors();

            this.ProcessAllActors();

            this.ProcessAllIlluminators();

            this.ActAllActors();
        }

        #endregion


        #region State Changers

        /// <summary>
        /// Prune all dead actors.
        /// </summary>
        private void PruneAllActors()
        {
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

        /// <summary>
        /// Spawn all new actors.
        /// </summary>
        private void SpawnNewActors()
        {
            if (this.CycleCounter % ((int)this.GameDifficulty + 1) == 0)
            {
                this.SpawnZombie();
            }

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

        /// <summary>
        /// Spawns a zombie on the fringes of the map.
        /// </summary>
        private void SpawnZombie()
        {
            Random ra = new Random();

            Directions d = (Directions)ra.Next(4);

            int x = 0, y = 0;
            char toSwitch = 'x';
            switch (d)
            {
                case Directions.East:
                case Directions.West:
                    if (d == Directions.East)
                    {
                        x = this.GameState.MapWidth - 1;
                    }
                    else
                    {
                        x = 0;
                    }
                    toSwitch = 'y';
                    y = ra.Next(this.GameState.MapHeight);
                    break;

                case Directions.South:
                case Directions.North:
                    if (d == Directions.South)
                    {
                        y = this.GameState.MapHeight - 1;
                    }
                    else
                    {
                        y = 0;
                    }
                    toSwitch = 'x';
                    x = ra.Next(this.GameState.MapWidth);
                    break;
                default:
                    break;
            }
            int counter = 0;
            while (this.GameState.Map[y, x].Occupant != null && counter++ < 100)
            {
                switch (toSwitch)
                {
                    case 'x':
                        x = ra.Next(this.GameState.MapWidth);
                        break;
                    case 'y':
                        y = ra.Next(this.GameState.MapHeight);
                        break;
                }
            }
            try
            {
                this.actorFactory.CreateActor(new ZombieFriend(new Vector2(x, y), this.SkillManager.Skills));
            }
            catch (SpawnOccupiedException se)
            { }
        }

        /// <summary>
        /// Tell all actors to think of a move.
        /// </summary>
        private void ProcessAllActors()
        {
            foreach (Actor a in this.GameState.Actors)
            {
                a.ProcessStatusEffects();
                a.Act(this.GameState);
            }
        }

        /// <summary>
        /// Illuminate the map with all illuminators, sorted by range.
        /// </summary>
        private void ProcessAllIlluminators()
        {
            this.GameState.Illuminators = this.GameState.Illuminators.OrderBy((x) => x.IlluminationRange).ToList();

            foreach (var illum in this.Illuminators)
            {
                this.IlluminateMap(illum);
            }
        }

        /// <summary>
        /// Act all actors according to their state.
        /// </summary>
        private void ActAllActors()
        {
            foreach (Actor a in this.GameState.Actors)
            {
                switch (a.DesiredAction)
                {
                    case GameInputs.Up:
                    case GameInputs.Down:
                    case GameInputs.Left:
                    case GameInputs.Right:
                        this.MoveActor(a, a.MapPosition + this.DirectionToUnitVector(a.Direction));
                        break;
                    case GameInputs.A1:
                    case GameInputs.A2:
                    case GameInputs.A3:
                    case GameInputs.A4:
                    case GameInputs.A5:
                        this.EnactSkill(a);
                        break;
                    default:
                        break;
                }
                a.DesiredAction = GameInputs.None;

                if (this.IsAloneInTheDark(a) && !(a is Debris))
                {
                    a.Inflict(StatusEffects.Fear);
                }
            }
        }

        /// <summary>
        /// Award experience to the player.
        /// </summary>
        /// <param name="player">The player to award experience to.</param>
        /// <param name="a">The actor to award experience from.</param>
        private void AwardExp(IPlayer player, IActor a)
        {
            // player.Stats.Experience += Calculator.CalculateExperience(a, player);
            player.Stats.Experience += 100;
        }

        /// <summary>
        /// Enact actor skill.
        /// </summary>
        /// <param name="parent">The parent whose skill to enact.</param>
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


            Vector2 skillEffectLocation = Engine.GetGroundTarget(parent.MapPosition, DirectionToUnitVector(parent.Direction), toEnact.Range);

            if (skillEffectLocation.X < 0 || skillEffectLocation.X >= this.GameState.MapWidth ||
                skillEffectLocation.Y < 0 || skillEffectLocation.Y >= this.GameState.MapHeight)
            {
                return;
            }


            if (toEnact.FocusCost > parent.Stats.Focus || toEnact.ManaCost > parent.Stats.Mana)
            {
                return;
            }

            parent.SpendSkill(toEnact);

            if (toEnact.SkillType == SkillTypes.Summon)
            {
                bool summoningResult = this.HandleSummonSkill(toEnact, skillEffectLocation);
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

        /// <summary>
        /// Get Unit Vector from Direction
        /// </summary>
        /// <param name="D">The Direction to switch.</param>
        /// <returns>The Unit Vector corresponding to the direction.</returns>
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

        /// <summary>
        /// Get the ground target, based on a movement vector and range.
        /// </summary>
        /// <param name="Current">The current location.</param>
        /// <param name="Movement">The movement vector.</param>
        /// <param name="Range">The range to move.</param>
        /// <returns>The new ground target.</returns>
        private static Vector2 GetGroundTarget(Vector2 Current, Vector2 Movement, int Range)
        {
            return Current + Movement * Range;
        }

        /// <summary>
        /// Handle summoning skill.
        /// </summary>
        /// <param name="toEnact">The skill to handle.</param>
        /// <param name="spawnLocation">The location to handle.</param>
        /// <returns>Whether the skill was handled successfully.</returns>
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
                var fireStats = new Statblock();
                fireStats.CopyValues(toEnact.Effect);

                var fire = new Campfire(spawnLocation, fireStats);
                this.GameState.SpawnQueue.Enqueue(fire);
            }

            return true;
        }

        /// <summary>
        /// Move actor to target coordinates.
        /// </summary>
        /// <param name="parent">The parent to move.</param>
        /// <param name="target">The target coordinates to move to.</param>
        private void MoveActor(Actor parent, Vector2 target)
        {
            // Check whether Actor can move on tile, then move Actor.
            if (target.X < 0 || target.X >= this.GameState.MapWidth || target.Y < 0 || target.Y >= this.GameState.MapHeight)
            {
                return;
            }
            if (parent.Stats.TraversalPower >= this.GameState.Map[(int)target.Y, (int)target.X].TraversalCost &&
                this.GameState.Map[(int)target.Y, (int)target.X].Occupant == null)
            {
                if (parent is IIlluminator)
                {
                    this.RemoveIllumination(parent as IIlluminator);
                }

                this.GameState.Map[(int)parent.MapPosition.Y, (int)parent.MapPosition.X].Occupant = null;
                parent.MapPosition = target;
                this.GameState.Map[(int)target.Y, (int)target.X].Occupant = parent;

                if (parent is IIlluminator)
                {
                    this.IlluminateMap(parent as IIlluminator);
                }
            }
        }


        // Illumination

        /// <summary>
        /// Illuminate the map.
        /// </summary>
        /// <param name="I">The illuminator to illuminate from.</param>
        private void IlluminateMap(IIlluminator I)
        {
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
                        Calculator.Distance(new Vector2(j, i), I.MapPosition) <= I.IlluminationRange)
                    {
                        var currentTileIllumination = this.GameState.Map[i, j].Illumination;
                        if (currentTileIllumination == this.GameState.FogOfWar ||
                            new[] { currentTileIllumination.R, currentTileIllumination.G, currentTileIllumination.B }.Max() <
                            new[] { I.IlluminationColor.R, I.IlluminationColor.G, I.IlluminationColor.B }.Max())
                        {
                            this.GameState.Map[i, j].Illumination = I.IlluminationColor;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove illumination from the map.
        /// </summary>
        /// <param name="I">The illuminator whose illumination to remove.</param>
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
                        Calculator.Distance(new Vector2(j, i), I.MapPosition) <= I.IlluminationRange)
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

        /// <summary>
        /// Checks whether an actor is sitting in FogOfWar, or in its own illumination.
        /// </summary>
        /// <param name="a">The actor to check.</param>
        /// <returns>Whether the actor is sitting in FogOfWar / its own illumination.</returns>
        private bool IsAloneInTheDark(IActor a)
        {
            var currentTileIllumination = this.GameState.Map[(int)a.MapPosition.Y, (int)a.MapPosition.X].Illumination;

            bool result = currentTileIllumination == this.FogOfWar;

            if (a is IIlluminator)
            {
                result = result || currentTileIllumination == (a as IIlluminator).IlluminationColor;
            }

            return result;
        }

        #endregion
    }
}
