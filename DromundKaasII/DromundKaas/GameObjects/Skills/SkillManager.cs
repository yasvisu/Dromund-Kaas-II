using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.GameObjects.Skills
{
    public class SkillManager
    {
        public SkillManager()
        {
            this.Skills = new Dictionary<string, Skill>();
            LoadSkills();
        }

        public Dictionary<string, Skill> Skills { get; private set; }

        void LoadSkills()
        {
            // Load all skills here
            // ...
            Skills["Fireball"] = new Skill()
            {
                Name = "Fireball",
                ManaCost = 100,
                FocusCost = 0,
                Range = 5,

                TargetPermissions = SkillTargetPermissions.All
            };

            // Ice bolt...
            // Electric shock...
        }
    }
}
