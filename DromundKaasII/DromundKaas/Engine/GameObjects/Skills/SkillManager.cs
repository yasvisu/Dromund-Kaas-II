using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                TargetPermissions = SkillTargetPermissions.All
            };
            this.Skills["Throw Rock"] = new Skill()
            {
                Name = "Throw Rock",
                ManaCost = 0,
                FocusCost = 10,
                Range = 5,
                TargetPermissions = SkillTargetPermissions.All
            };
            this.Skills["Huddle"] = new Skill()
            {
                Name = "Huddle",
                ManaCost = 0,
                FocusCost = 5,
                Range = 0,
                TargetPermissions = SkillTargetPermissions.All
            };
            this.Skills["Start Fire"] = new Skill()
            {
                Name = "Start Fire",
                ManaCost = 10,
                FocusCost = 10,
                Range = 3,
                TargetPermissions = SkillTargetPermissions.All
            };



            // Ice bolt...
            // Electric shock...
        }
    }
}
