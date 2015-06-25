using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Skills
{
    public delegate void ActorAffectorDelegate(Actor target, IStatsheet effect);

    public class Skill
    {
        // add appropriate constructors and properties for the fields
        // ...

        public Skill(string name, int manaCost, int focusCost, int range, SkillTypes skillType, IStatsheet effect, ActorAffectorDelegate affect, SkillTargetPermissions targetPermissions)
        {
            this.Name = name;
            this.ManaCost = manaCost;
            this.FocusCost = focusCost;
            this.Range = range;
            this.SkillType = skillType;
            this.Effect = effect;
            this.Affect = affect;
            this.TargetPermissions = targetPermissions;
        }

        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int FocusCost { get; set; }
        public int Range { get; set; }

        public SkillTypes SkillType { get; set; }

        public IStatsheet Effect { get; set; }
        public ActorAffectorDelegate Affect { get; set; }

        public SkillTargetPermissions TargetPermissions { get; set; }
    }
}
