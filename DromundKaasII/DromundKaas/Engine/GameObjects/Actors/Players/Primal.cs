using System.Collections.Generic;

using DromundKaasII.Engine.GameObjects.Skills;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Players
{
    /// <summary>
    /// Tier 0 Primal.
    /// </summary>
    public class Primal : Player 
    {
        /// <summary>
        /// Initializes a new Primal.
        /// </summary>
        /// <param name="MapPosition">The Primal's map position.</param>
        /// <param name="SkillChain">The skills the Primal can pick from.</param>
        public Primal(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition,SkillChain)
        {
            this.Stats = new Statblock(8);

            this.Stats.Health = this.Stats.MaxHealth;
            this.Stats.Mana = this.Stats.MaxMana;
            this.Stats.Focus = this.Stats.MaxFocus;

            this.Skills = new Skill[5];

            this.Skills[0] = SkillChain["Hit"];
            this.Skills[1] = SkillChain["Throw Rock"];
            this.Skills[2] = SkillChain["Huddle"];
            this.Skills[3] = SkillChain["Start Fire"];
            this.Skills[4] = SkillChain["None"];

            this.Stats.IlluminationRange = 2;
            this.IlluminationColor = Color.LightSteelBlue;

        }
    }
}
