using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine.GameObjects.Actors
{
    public class Statblock : Statsheet, IStatblock
    {
        public const float DEFAULT_PRIMARY_STAT = 8f;
        public const int DEFAULT_TRAVERSAL_POWER = 100;

        int health;
        int mana;
        int focus;

        public Statblock(float defaultPrimaryStat = DEFAULT_PRIMARY_STAT, int traversalPower = DEFAULT_TRAVERSAL_POWER)
            : this(defaultPrimaryStat, defaultPrimaryStat, defaultPrimaryStat, defaultPrimaryStat, traversalPower)
        { }

        public Statblock(float strength, float dexterity, float constitution, float intelligence, int traversalPower)
        {
            this.Strength = strength;
            this.Dexterity = dexterity;
            this.Constitution = constitution;
            this.Intelligence = intelligence;
            this.TraversalPower = traversalPower;
        }

        // Combat
        public override int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = Math.Max(value, 0);
                this.health = Math.Min(this.MaxHealth, this.health);
            }
        }
        public override int Mana
        {
            get
            {
                return this.mana;
            }
            set
            {
                this.mana = Math.Max(value, 0);
                this.mana = Math.Min(this.MaxMana, this.mana);
            }
        }
        public override int Focus
        {
            get
            {
                return this.focus;
            }
            set
            {
                this.focus = Math.Max(value, 0);
                this.focus = Math.Min(this.MaxFocus, this.focus);
            }
        }

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
    }
}
