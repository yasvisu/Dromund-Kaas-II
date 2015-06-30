using System;

using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine.GameObjects.Actors
{
    /// <summary>
    /// A statsheet with maximum value constraints.
    /// </summary>
    public class Statblock : Statsheet, IStatblock
    {
        /// <summary>
        /// The default primary stat of statblocks.
        /// </summary>
        public const float DEFAULT_PRIMARY_STAT = 8f;

        /// <summary>
        /// The default traversal power of statblocks.
        /// </summary>
        public const int DEFAULT_TRAVERSAL_POWER = 100;

        private int health;
        private int mana;
        private int focus;

        /// <summary>
        /// Initializes a new Statblock.
        /// </summary>
        /// <param name="defaultPrimaryStat">The default primary stat to use.</param>
        /// <param name="traversalPower">The default traversal power to use.</param>
        public Statblock(float defaultPrimaryStat = DEFAULT_PRIMARY_STAT, int traversalPower = DEFAULT_TRAVERSAL_POWER)
            : this(defaultPrimaryStat, defaultPrimaryStat, defaultPrimaryStat, defaultPrimaryStat, traversalPower)
        { }

        /// <summary>
        /// Initializes a new Statblock.
        /// </summary>
        /// <param name="strength">The Strength stat.</param>
        /// <param name="dexterity">The Dexterity stat.</param>
        /// <param name="constitution">The Constitution stat.</param>
        /// <param name="intelligence">The Intelligence stat.</param>
        /// <param name="traversalPower">The TraversalPower stat.</param>
        public Statblock(float strength, float dexterity, float constitution, float intelligence, int traversalPower)
        {
            this.Strength = strength;
            this.Dexterity = dexterity;
            this.Constitution = constitution;
            this.Intelligence = intelligence;
            this.TraversalPower = traversalPower;

            this.Health = this.MaxHealth;
            this.Mana = this.MaxMana;
            this.Focus = this.MaxFocus;
        }

        /// <summary>
        /// Statblock health. Cannot be higher than the maximum.
        /// </summary>
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

        /// <summary>
        /// Statblock mana. Cannot be higher than the maximum.
        /// </summary>
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

        /// <summary>
        /// Statblock focus. Cannot be higher than the maximum.
        /// </summary>
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


        /// <summary>
        /// Maximum health of the statblock.
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return (int)(this.Constitution * 10 + Level * 10);
            }
        }

        /// <summary>
        /// Maximum mana of the statblock.
        /// </summary>
        public int MaxMana
        {
            get
            {
                return (int)(this.Intelligence * 10 + Level * 10);
            }
        }

        /// <summary>
        /// Maximum focus of the statblock.
        /// </summary>
        public int MaxFocus
        {
            get
            {
                return (int)(this.Dexterity * 10 + Level * 10);
            }
        }
    }
}
