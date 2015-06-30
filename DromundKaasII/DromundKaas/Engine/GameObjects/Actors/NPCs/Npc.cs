using System.Collections.Generic;

using DromundKaasII.Engine.GameObjects.Skills;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.NPCs
{
    /// <summary>
    /// Catch-all class for all Npc's.
    /// </summary>
    public abstract class Npc : Actor
    {
        /// <summary>
        /// Initializes a new Npc.
        /// </summary>
        /// <param name="MapPosition">The Npc's position.</param>
        /// <param name="SkillChain">The skills the Npc can pick from.</param>
        public Npc(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition, SkillChain)
        { }
    }
}
