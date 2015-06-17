using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.GameObjects.Actors
{
    public class Statblock
    {
        // Combat
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Focus { get; set; }

        // Experience
        public int Experience { get; set; }
        public byte Level { get; set; }

        // Primary stats
        public int MaxHealth
        {
            get
            {
                return (int)(this.Constitution * 10 + Level * 10);
            }
        }
        public int MaxMana
        {
            get
            {
                return (int)(this.Intelligence * 10 + Level * 10);
            }
        }
        public int MaxFocus
        {
            get
            {
                return (int)(this.Dexterity * 10 + Level * 10);
            }
        }

        public float Strength { get; set; }
        public float Dexterity { get; set; }
        public float Constitution { get; set; }
        public float Intelligence { get; set; }

        // Secondary stats
        public float Wisdom { get; set; }
        public float Charisma { get; set; }
        public float Psychic { get; set; }

    }
}
