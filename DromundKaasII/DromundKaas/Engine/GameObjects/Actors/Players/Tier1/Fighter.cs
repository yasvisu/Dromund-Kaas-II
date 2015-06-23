using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Skills;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Players.Tier1
{
    class Fighter : Tier1
    {
        public Fighter(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition,SkillChain)
        {

        }

        public Fighter(Primal candidate)
            : base(candidate)
        {
            candidate.Stats.Strength += 2;
            candidate.Stats.Constitution += 2;
            candidate.Stats.Charisma += 1;
        }
    }
}
