using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Skills;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Players.Tier1
{
    class Builder : Tier1
    {
        public Builder(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition,SkillChain)
        {

        }

        public Builder(Primal candidate)
            : base(candidate)
        {
            candidate.Stats.Dexterity += 1;
            candidate.Stats.Constitution += 2;
            candidate.Stats.Wisdom += 2;
        }
    }
}
