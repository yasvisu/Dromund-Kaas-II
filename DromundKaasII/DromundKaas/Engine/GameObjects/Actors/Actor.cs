using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine;
using DromundKaasII.Input;
using System.Collections;
using DromundKaasII.Engine.GameObjects.Skills;

namespace DromundKaasII.Engine.GameObjects.Actors
{
    public abstract class Actor : IActor
    {
        protected Actor(Vector2 MapPosition)
            : this(MapPosition, null, null)
        { }

        protected Actor(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : this(MapPosition, SkillChain, null)
        { }

        protected Actor(Vector2 MapPosition, Dictionary<string, Skill> SkillChain, Statblock Stats)
        {
            this.MapPosition = MapPosition;
            this.StatusEffects = new Dictionary<StatusEffects, TimeSpan>();
            this.DesiredAction = GameInputs.None;
            this.Stats = Stats;
        }

        public Vector2 MapPosition { get; set; }

        public Actor Target { get; protected set; }
        public Vector2 GroundTarget { get; protected set; }
        public GameInputs DesiredAction { get; set; }

        public Directions Direction { get; set; }

        public Statblock Stats { get; set; }

        public Dictionary<StatusEffects, TimeSpan> StatusEffects { get; set; }

        public Skill[] Skills { get; protected set; }

        public virtual void Act(GameState G)
        {
            switch (this.DesiredAction)
            {
                case GameInputs.Up:
                    this.Direction = Directions.North;
                    break;
                case GameInputs.Down:
                    this.Direction = Directions.South;
                    break;
                case GameInputs.Left:
                    this.Direction = Directions.West;
                    break;
                case GameInputs.Right:
                    this.Direction = Directions.East;
                    break;
                default: 
                    break;
            }
        }

        public virtual void RemoveExpiredStatusEffects()
        {
            Stack<StatusEffects> GarbageCan = new Stack<StatusEffects>();

            foreach (var kvp in this.StatusEffects)
            {
                if (kvp.Value.TotalMilliseconds <= 0)
                {
                    GarbageCan.Push(kvp.Key);
                }
            }
            while (GarbageCan.Count > 0)
            {
                this.StatusEffects.Remove(GarbageCan.Pop());
            }
        }

        public double DistanceTo(Actor other)
        {
            return Math.Sqrt((other.MapPosition.X - this.MapPosition.X) * (other.MapPosition.X - this.MapPosition.X) + (other.MapPosition.Y - this.MapPosition.Y) * (other.MapPosition.Y - this.MapPosition.Y));
        }

        public void SpendSkill(Skill toEnact)
        {
            this.Stats.Focus -= toEnact.FocusCost;
            this.Stats.Mana -= toEnact.ManaCost;
        }

        public void ReactToSkill(Skill toEnact)
        {
            toEnact.Affect(this, toEnact.Effect);
        }
    }
}
