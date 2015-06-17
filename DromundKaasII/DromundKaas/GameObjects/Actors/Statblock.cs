﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.GameObjects.Actors
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
                this.focus = Math.Min(this.MaxFocus, value);
            }
        }

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

        public Statblock operator +(Statblock s)
        {
            return new Statblock()
            {
                Strength = this.Strength + s.Strength,
                Dexterity = this.Dexterity + s.Dexterity,
                Constitution = this.Constitution + s.Constitution,
                Intelligence = this.Intelligence + s.Intelligence,

                Wisdom = this.Wisdom + s.Wisdom,
                Charisma = this.Charisma + s.Charisma,
                Psychic = this.Psychic + s.Psychic,
            };
        }
    }
}
