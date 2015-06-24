using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Actors;

namespace DromundKaasII.Engine.GameObjects.Skills
{
    public class SkillManager
    {
        public SkillManager()
        {
            this.Skills = new Dictionary<string, Skill>();
            this.LoadSkills();
        }

        public Dictionary<string, Skill> Skills { get; private set; }

        void LoadSkills()
        {
            // Load all skills here
            this.Skills["None"] = new Skill()
            {
                Name = "None",
                TargetPermissions = SkillTargetPermissions.None,
            };

            this.Skills["Hit"] = new Skill()
            {
                Name = "Hit",
                ManaCost = 0,
                FocusCost = 5,
                Range = 1,
                TargetPermissions = SkillTargetPermissions.All,
                Affect = Damage,
                SkillType = SkillTypes.Physical,
                Effect = new Statblock() { Health = 50 }
            };
            this.Skills["Throw Rock"] = new Skill()
            {
                Name = "Throw Rock",
                ManaCost = 0,
                FocusCost = 10,
                Range = 5,
                TargetPermissions = SkillTargetPermissions.All,
                Affect = Damage,
                SkillType = SkillTypes.Physical,
                Effect = new Statblock() { Health = 20 }
            };
            this.Skills["Huddle"] = new Skill()
            {
                Name = "Huddle",
                ManaCost = 0,
                FocusCost = 5,
                Range = 0,
                TargetPermissions = SkillTargetPermissions.All,
                Affect = Augment,
                SkillType = SkillTypes.Physical,
                Effect = new Statblock() { Health = 30 }
            };
            this.Skills["Start Fire"] = new Skill()
            {
                Name = "Start Fire",
                ManaCost = 10,
                FocusCost = 10,
                Range = 3,
                TargetPermissions = SkillTargetPermissions.All,
                Affect = Augment,
                SkillType = SkillTypes.Summon
            };



            // Ice bolt...
            // Electric shock...
        }

        void Augment(Actor target, Statblock effect)
        {
            target.Stats += effect;
        }
        void Damage(Actor target, Statblock effect)
        {
            target.Stats -= effect;
        }
    }
}
