using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Actors;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Skills
{
    public delegate void ActorAffectorDelegate(Actor target);

    public class Skill
    {
        // add appropriate constructors and properties for the fields
        // ...

        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int FocusCost { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public ActorAffectorDelegate Affect { get; set; }

        public SkillTargetPermissions TargetPermissions { get; set; }
    }
}
