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
        protected HashSet<StatusEffects> currentStatusEffects;

        protected Actor(Vector2 MapPosition)
            : this(MapPosition, null, null)
        { }

        protected Actor(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : this(MapPosition, SkillChain, null)
        { }

        protected Actor(Vector2 MapPosition, Dictionary<string, Skill> SkillChain, Statblock Stats)
        {
            this.currentStatusEffects = new HashSet<StatusEffects>();

            this.MapPosition = MapPosition;
            this.ActorStatusEffects = new Dictionary<StatusEffects, uint>();
            foreach (StatusEffects se in Enum.GetValues(typeof(StatusEffects)))
            {
                this.ActorStatusEffects[se] = 0;
            }

            this.DesiredAction = GameInputs.None;
            this.Stats = Stats;
        }

        public Vector2 MapPosition { get; set; }

        public Actor Target { get; protected set; }
        public Vector2 GroundTarget { get; protected set; }
        public GameInputs DesiredAction { get; set; }

        public Directions Direction { get; set; }
        public IEnumerable<StatusEffects> Status
        {
            get
            {
                return this.currentStatusEffects;
            }
        }

        public Statsheet Regen { get; set; }
        public Statblock Stats { get; set; }

        public Dictionary<StatusEffects, uint> ActorStatusEffects { get; private set; }

        public Skill[] Skills { get; protected set; }

        public virtual void Act(GameState G)
        {
            this.ApplyRegeneration();

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

        public virtual void Inflict(StatusEffects StatusEffect)
        {
            this.Inflict(StatusEffect, 1);
        }

        public virtual void Inflict(StatusEffects StatusEffect, uint durationInCycles)
        {
            this.ActorStatusEffects[StatusEffect] += durationInCycles;
        }

        public virtual void ProcessStatusEffects()
        {
            this.RefreshRegeneration();

            this.RemoveExpiredStatusEffects();

            foreach (var key in this.ActorStatusEffects.Keys.ToList())
            {
                if (this.ActorStatusEffects[key] == 0)
                {
                    this.currentStatusEffects.Remove(key);
                }
                else if (this.ActorStatusEffects[key] >= 1)
                {
                    this.currentStatusEffects.Add(key);
                    this.ActorStatusEffects[key]--;
                }
            }

            this.ApplyStatusEffects();
        }

        protected virtual void RefreshRegeneration()
        {
            this.Regen = new Statsheet();
            this.Regen.Mana++;
            this.Regen.Focus++;
        }

        protected virtual void RemoveExpiredStatusEffects()
        {
            foreach (var kvp in this.ActorStatusEffects)
            {
                if (kvp.Value <= 0)
                {
                    this.currentStatusEffects.Remove(kvp.Key);
                }
            }
        }

        protected virtual void ApplyStatusEffects()
        {
            foreach (var effect in this.currentStatusEffects)
            {
                switch (effect)
                {
                    case StatusEffects.Fear:
                        this.Fear();
                        break;
                    default:
                        break;
                }
            }
        }

        protected virtual void Fear()
        {
            this.Regen.Health--;
            this.Regen.Mana--;
            this.Regen.Focus--;
        }

        protected virtual void ApplyRegeneration()
        {
            this.Stats.Health += this.Regen.Health;
            this.Stats.Mana += this.Regen.Mana;
            this.Stats.Focus += this.Regen.Focus;

            
        }

        public double DistanceTo(Actor other)
        {
            return Math.Sqrt((other.MapPosition.X - this.MapPosition.X) * (other.MapPosition.X - this.MapPosition.X) + (other.MapPosition.Y - this.MapPosition.Y) * (other.MapPosition.Y - this.MapPosition.Y));
        }

        public virtual void SpendSkill(Skill toEnact)
        {
            this.Stats.Focus -= toEnact.FocusCost;
            this.Stats.Mana -= toEnact.ManaCost;
        }

        public virtual void ReactToSkill(Skill toEnact)
        {
            toEnact.Affect(this, toEnact.Effect);
        }
    }
}
