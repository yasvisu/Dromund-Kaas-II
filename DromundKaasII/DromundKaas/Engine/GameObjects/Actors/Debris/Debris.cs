using System.Collections.Generic;

using DromundKaasII.Engine.GameObjects.Skills;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Debris
{
    /// <summary>
    /// Catch-all class for all debris.
    /// </summary>
    public abstract class Debris : Actor
    {
        /// <summary>
        /// Initializes new Debris.
        /// </summary>
        /// <param name="MapPosition">The Debris's position.</param>
        /// <param name="SkillChain">The skills the Debris can pick from.</param>
        /// <param name="Stats">The Debris's stats.</param>
        public Debris(Vector2 MapPosition, Dictionary<string, Skill> SkillChain, Statblock Stats)
            : base(MapPosition, SkillChain, Stats)
        { }
    }
}
