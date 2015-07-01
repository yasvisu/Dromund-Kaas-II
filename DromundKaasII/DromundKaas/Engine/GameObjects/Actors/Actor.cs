using System;
using System.Collections.Generic;
using System.Linq;

using DromundKaasII.Engine.GameObjects.Skills;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors
{
    /// <summary>
    /// Base actor class with all suitable variables.
    /// </summary>
    public abstract class Actor : IActor
    {
        /// <summary>
        /// A set of the current status effects of the actor.
        /// </summary>
        protected HashSet<StatusEffects> currentStatusEffects;


        /// <summary>
        /// Initialize new Actor.
        /// </summary>
        /// <param name="MapPosition">The Actor's position.</param>
        protected Actor(Vector2 MapPosition)
            : this(MapPosition, null, null)
        { }

        /// <summary>
        /// Initialize new Actor.
        /// </summary>
        /// <param name="MapPosition">The Actor's position.</param>
        /// <param name="SkillChain">The skills the Actor can pick from.</param>
        protected Actor(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : this(MapPosition, SkillChain, null)
        { }

        /// <summary>
        /// Initialize new Actor.
        /// </summary>
        /// <param name="MapPosition">The Actor's position.</param>
        /// <param name="SkillChain">The skills the Actor can pick from.</param>
        /// <param name="Stats">The stats of the Actor.</param>
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

            this.Skills = new Skill[5];
        }

        /// <summary>
        /// The position of the Actor.
        /// </summary>
        public Vector2 MapPosition { get; set; }

        /// <summary>
        /// The desired action of the Actor.
        /// </summary>
        public GameInputs DesiredAction { get; set; }

        /// <summary>
        /// The direction of the Actor.
        /// </summary>
        public Directions Direction { get; set; }

        /// <summary>
        /// A collection of the StatusEffects of the Actor.
        /// </summary>
        public IEnumerable<StatusEffects> Status
        {
            get
            {
                return this.currentStatusEffects;
            }
        }

        /// <summary>
        /// The Actor's regeneration.
        /// </summary>
        public Statsheet Regen { get; set; }

        /// <summary>
        /// The Actor's stats.
        /// </summary>
        public Statblock Stats { get; set; }


        /// <summary>
        /// A Dictionary of StatusEffects and their durations in rounds.
        /// </summary>
        public Dictionary<StatusEffects, uint> ActorStatusEffects { get; private set; }

        /// <summary>
        /// The Actor's skills.
        /// </summary>
        public Skill[] Skills { get; protected set; }


        /// <summary>
        /// Set a desired action based on the GameState.
        /// </summary>
        /// <param name="G">The GameState to act on.</param>
        public virtual void Act(GameState G)
        {
            this.ApplyRegeneration();
        }

        /// <summary>
        /// Inflict a StatusEffect.
        /// </summary>
        /// <param name="StatusEffect">The status effect to inflict.</param>
        public virtual void Inflict(StatusEffects StatusEffect)
        {
            this.Inflict(StatusEffect, 1);
        }

        /// <summary>
        /// Inflict a StatusEffect.
        /// </summary>
        /// <param name="StatusEffect">The status effect to inflict.</param>
        /// <param name="durationInCycles">The duration to inflict.</param>
        public virtual void Inflict(StatusEffects StatusEffect, uint durationInCycles)
        {
            this.ActorStatusEffects[StatusEffect] += durationInCycles;
        }


        /// <summary>
        /// Process all status effects of the actor.
        /// </summary>
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

        /// <summary>
        /// Refresh the Actor's regeneration.
        /// </summary>
        protected virtual void RefreshRegeneration()
        {
            this.Regen = new Statsheet();
            this.Regen.Mana++;
            this.Regen.Focus++;
        }

        /// <summary>
        /// Remove all expired status effects.
        /// </summary>
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

        /// <summary>
        /// Apply all status effects.
        /// </summary>
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

        /// <summary>
        /// React to Fear.
        /// </summary>
        protected virtual void Fear()
        {
            this.Regen.Health--;
            this.Regen.Mana--;
            this.Regen.Focus--;
        }

        /// <summary>
        /// Apply regeneration.
        /// </summary>
        protected virtual void ApplyRegeneration()
        {
            this.Stats.Health += this.Regen.Health;
            this.Stats.Mana += this.Regen.Mana;
            this.Stats.Focus += this.Regen.Focus;
        }

        /// <summary>
        /// Calculate distance to other actor.
        /// </summary>
        /// <param name="other">The actor to calculate distance to.</param>
        /// <returns>The distance between the two actors.</returns>
        public double DistanceTo(Actor other)
        {
            return Math.Sqrt((other.MapPosition.X - this.MapPosition.X) * (other.MapPosition.X - this.MapPosition.X) + (other.MapPosition.Y - this.MapPosition.Y) * (other.MapPosition.Y - this.MapPosition.Y));
        }

        /// <summary>
        /// Spend the skill.
        /// </summary>
        /// <param name="toEnact">The skill to spend.</param>
        public virtual void SpendSkill(Skill toEnact)
        {
            this.Stats.Focus -= toEnact.FocusCost;
            this.Stats.Mana -= toEnact.ManaCost;
        }

        /// <summary>
        /// React to the skill.
        /// </summary>
        /// <param name="toEnact">The skill to react to.</param>
        public virtual void ReactToSkill(Skill toEnact)
        {
            toEnact.Affect(this, toEnact.Effect);
        }
    }
}
