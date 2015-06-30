using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine.GameObjects.Actors
{
    /// <summary>
    /// A Statistics sheet of statistics coefficients.
    /// </summary>
    public class Statsheet : IStatsheet
    {
        /// <summary>
        /// Health stat.
        /// </summary>
        public virtual int Health { get; set; }

        /// <summary>
        /// Mana stat.
        /// </summary>
        public virtual int Mana { get; set; }

        /// <summary>
        /// Focus stat.
        /// </summary>
        public virtual int Focus { get; set; }

        // Experience
        /// <summary>
        /// Amount of experience.
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Amount of levels.
        /// </summary>
        public int Level { get; set; }

        // Primary stats
        /// <summary>
        /// Strength stat.
        /// </summary>
        public float Strength { get; set; }

        /// <summary>
        /// Dexterity stat.
        /// </summary>
        public float Dexterity { get; set; }

        /// <summary>
        /// Constitution stat.
        /// </summary>
        public float Constitution { get; set; }

        /// <summary>
        /// Intelligence stat.
        /// </summary>
        public float Intelligence { get; set; }

        // Secondary stats
        /// <summary>
        /// Wisdom stat.
        /// </summary>
        public float Wisdom { get; set; }

        /// <summary>
        /// Charisma stat.
        /// </summary>
        public float Charisma { get; set; }

        /// <summary>
        /// Psychic stat.
        /// </summary>
        public float Psychic { get; set; }

        // Movement
        /// <summary>
        /// Traversal power stat.
        /// </summary>
        public int TraversalPower { get; set; }

        // Sight
        /// <summary>
        /// Range to illuminate in.
        /// </summary>
        public float IlluminationRange { get; set; }

        /// <summary>
        /// Adds a statsheet to this one. 
        /// For safety, currently works only on combat stats (health, mana, focus).
        /// </summary>
        /// <param name="target">The statsheet to process.</param>
        public void Add(IStatsheet target)
        {
            this.Health += target.Health;
            this.Mana += target.Mana;
            this.Focus += target.Focus;
        }

        /// <summary>
        /// Removes a statsheet from this one. 
        /// For safety, currently works only on combat stats (health, mana, focus).
        /// </summary>
        /// <param name="target">The statsheet to process.</param>
        public void Remove(IStatsheet target)
        {
            this.Health -= target.Health;
            this.Mana -= target.Mana;
            this.Focus -= target.Focus;
        }

        /// <summary>
        /// Copies all values from another statsheet.
        /// </summary>
        /// <param name="target">The statsheet to process.</param>
        public void CopyValues(IStatsheet target)
        {
            this.Level = target.Level;
            this.Experience = target.Experience;

            this.Strength = target.Strength;
            this.Dexterity = target.Dexterity;
            this.Constitution = target.Constitution;
            this.Charisma = target.Charisma;
            this.Intelligence = target.Intelligence;

            this.Psychic = target.Psychic;
            this.Wisdom = target.Wisdom;

            this.Health = target.Health;
            this.Mana = target.Mana;
            this.Focus = target.Focus;

            this.TraversalPower = target.TraversalPower;

            this.IlluminationRange = target.IlluminationRange;
        }
    }
}
