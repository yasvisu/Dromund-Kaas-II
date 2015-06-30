using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Skills;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Players.Tier1
{
    abstract class Tier1 : Player
    {
        protected Tier1(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition,SkillChain)
        {

        }

        protected Tier1(Primal candidate)
            : base(candidate.MapPosition)
        {
            this.Score = candidate.Score;
            this.Stats = candidate.Stats;
        }
    }
}
