using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Skills;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.NPCs
{
    public abstract class Npc : Actor
    {
        public Npc(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition, SkillChain)
        {

        }
    }
}
