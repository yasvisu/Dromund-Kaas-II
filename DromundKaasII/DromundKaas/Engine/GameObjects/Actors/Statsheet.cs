using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine.GameObjects.Actors
{
    public class Statsheet : IStatsheet
    {
        public virtual int Health { get; set; }
        public virtual int Mana { get; set; }
        public virtual int Focus { get; set; }

        // Experience
        public int Experience { get; set; }
        public int Level { get; set; }

        // Primary stats
        public float Strength { get; set; }
        public float Dexterity { get; set; }
        public float Constitution { get; set; }
        public float Intelligence { get; set; }

        // Secondary stats
        public float Wisdom { get; set; }
        public float Charisma { get; set; }
        public float Psychic { get; set; }

        // Movement
        public int TraversalPower { get; set; }

        // Sight
        public float IlluminationRange { get; set; }

        public void Add(IStatsheet target)
        {
            this.Health += target.Health;
            this.Mana += target.Mana;
            this.Focus += target.Focus;
        }

        public void Remove(IStatsheet target)
        {
            this.Health -= target.Health;
            this.Mana -= target.Mana;
            this.Focus -= target.Focus;
        }
    }
}
