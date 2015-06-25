using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine.GameObjects.Actors
{
    public class Statblock
    {
        int health;
        int mana;
        int focus;

        // Combat
        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = Math.Max(value, 0);
                this.health = Math.Min(this.MaxHealth, value);
            }
        }
        public int Mana
        {
            get
            {
                return this.mana;
            }
            set
            {
                this.mana = Math.Max(value, 0);
                this.mana = Math.Min(this.MaxMana, value);
            }
        }
        public int Focus
        {
            get
            {
                return this.focus;
            }
            set
            {
                this.focus = Math.Max(value, 0);
                this.focus = Math.Min(this.MaxFocus, value);
            }
        }

        // Experience
        public int Experience { get; set; }
        public int Level { get; set; }

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

        // Movement
        public int TraversalPower { get; set; }

        public void Add(Statblock target)
        {
            this.Health += target.Health;
            this.Mana += target.Mana;
            this.Focus += target.Focus;
        }

        public void Remove(Statblock target)
        {
            this.Health -= target.Health;
            this.Mana -= target.Mana;
            this.Focus -= target.Focus;
        }
    }
}
