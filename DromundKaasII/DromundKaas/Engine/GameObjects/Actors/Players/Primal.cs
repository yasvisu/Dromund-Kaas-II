using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DromundKaasII.Engine.GameObjects.Skills;

namespace DromundKaasII.Engine.GameObjects.Actors.Players
{


    public class Primal : Player 
    {

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

        }
    }
}
