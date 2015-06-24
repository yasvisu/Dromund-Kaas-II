using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Actors;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Skills
{
    public delegate void ActorAffectorDelegate(Actor target, Statblock effect);

    public class Skill
    {
        // add appropriate constructors and properties for the fields
        // ...

        public SkillTypes SkillType { get; set; }
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int FocusCost { get; set; }
        public int Range { get; set; }
        public Statblock Effect { get; set; }
        public ActorAffectorDelegate Affect { get; set; }

        public SkillTargetPermissions TargetPermissions { get; set; }
    }
}
