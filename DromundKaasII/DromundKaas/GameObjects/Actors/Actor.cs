using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.GameObjects.Enums;
using DromundKaasII.Engine;
using DromundKaasII.Input;

namespace DromundKaasII.GameObjects.Actors
{
    public abstract class Actor : IActor
    {
        protected Actor(Vector2 MapPosition)
        {
            this.MapPosition = MapPosition;
            this.StatusEffects = new Dictionary<StatusEffects, TimeSpan>();
        }

        protected Actor(Vector2 MapPosition, Statblock Stats)
            : this(MapPosition)
        {
            this.Stats = Stats;
        }


        public Vector2 MapPosition { get; set; }

        public Actor Target { get; protected set; }
        public Vector2 GroundTarget { get; protected set; }
        public GameInputs DesiredAction { get; set; }

        public Statblock Stats { get; set; }

        public Dictionary<StatusEffects, TimeSpan> StatusEffects { get; set; }

        public virtual void Act(GameState G)
        {
            throw new NotImplementedException("Not implemented.");
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
    }
}
