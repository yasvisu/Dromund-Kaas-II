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
            this.ActorStatusEffects = new Dictionary<StatusEffects, uint>();
            this.DesiredAction = GameInputs.None;
            this.Stats = Stats;
        }

        public Vector2 MapPosition { get; set; }

        public Actor Target { get; protected set; }
        public Vector2 GroundTarget { get; protected set; }
        public GameInputs DesiredAction { get; set; }

        public Directions Direction { get; set; }


        public Statsheet Regen { get; set; }
        public Statblock Stats { get; set; }

        public Dictionary<StatusEffects, uint> ActorStatusEffects { get; set; }

        public Skill[] Skills { get; protected set; }

        public virtual void Act(GameState G)
        {
            this.Regen = new Statsheet();
            this.Regen.Mana++;
            this.Regen.Focus++;


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
            if (!this.ActorStatusEffects.ContainsKey(StatusEffect))
            {
                this.ActorStatusEffects[StatusEffect] = 1;
            }
            else
            {
                this.ActorStatusEffects[StatusEffect]++;
            }
        }

        public virtual void ProcessStatusEffects()
        {
            this.RefreshRegeneration();

            this.RemoveExpiredStatusEffects();

            foreach (var effect in this.ActorStatusEffects)
            {
                switch (effect.Key)
                {
                    case StatusEffects.Fear:
                        this.Fear();
                        this.ActorStatusEffects[StatusEffects.Fear]--;
                        break;
                    default:
                        throw new NotImplementedException("Status effect not implemented.");
                }
            }
        }

        protected virtual void RefreshRegeneration()
        {
            this.Regen = new Statsheet();
            this.Regen.Mana++;
            this.Regen.Health++;
        }

        protected virtual void RemoveExpiredStatusEffects()
        {
            Stack<StatusEffects> GarbageCan = new Stack<StatusEffects>();

            foreach (var kvp in this.ActorStatusEffects)
            {
                if (kvp.Value <= 0)
                {
                    GarbageCan.Push(kvp.Key);
                }
            }
            while (GarbageCan.Count > 0)
            {
                this.ActorStatusEffects.Remove(GarbageCan.Pop());
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
