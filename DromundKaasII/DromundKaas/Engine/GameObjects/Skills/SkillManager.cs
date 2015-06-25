using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Interfaces;

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
            this.Skills["None"] = new Skill("None", 0, 0, 0, SkillTypes.Physical, null, Augment, SkillTargetPermissions.None);

            this.Skills["Hit"] = new Skill(
                "Hit",
                0, 5,
                1,
                SkillTypes.Physical,
                new Statsheet() { Health = 50 },
                Damage,
                SkillTargetPermissions.All);

            this.Skills["Throw Rock"] = new Skill(
                "Throw Rock",
                0, 10,
                5,
                SkillTypes.Physical,
                new Statsheet() { Health = 20 },
                Damage,
                SkillTargetPermissions.All);

            this.Skills["Huddle"] = new Skill(
                "Huddle",
                0, 5,
                0,
                SkillTypes.Physical,
                new Statsheet() { Health = 30 },
                Augment,
                SkillTargetPermissions.All);

            this.Skills["Start Fire"] = new Skill(
                "Start Fire",
                10, 10,
                3,
                SkillTypes.Summon,
                new Statblock(8, 0),
                Augment,
                SkillTargetPermissions.Ground);




            // Ice bolt...
            // Electric shock...
        }

        void Augment(Actor target, IStatsheet effect)
        {
            target.Stats.Add(effect);
        }
        void Damage(Actor target, IStatsheet effect)
        {
            target.Stats.Remove(effect);
        }
    }
}
