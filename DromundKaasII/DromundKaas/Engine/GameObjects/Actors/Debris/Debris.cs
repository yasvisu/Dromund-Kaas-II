using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Skills;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Debris
{
    public abstract class Debris : Actor
    {
        public Debris(Vector2 MapPosition, Dictionary<string, Skill> SkillChain, Statblock Stats)
            : base(MapPosition, SkillChain, Stats)
        {
        }
    }
}
