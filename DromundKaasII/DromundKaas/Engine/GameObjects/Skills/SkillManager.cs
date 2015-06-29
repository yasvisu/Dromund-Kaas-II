using System.Collections.Generic;

using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine.GameObjects.Skills
{
    /// <summary>
    /// A screen manager to contain and seed all skills.
    /// </summary>
    public class SkillManager
    {
        /// <summary>
        /// Initializes a new SkillManager.
        /// </summary>
        public SkillManager()
        {
            this.Skills = new Dictionary<string, Skill>();
            this.LoadSkills();
        }

        /// <summary>
        /// A dictionary of the skills in this SkillManager.
        /// </summary>
        public Dictionary<string, Skill> Skills { get; private set; }

        /// <summary>
        /// Seeds all skills.
        /// </summary>
        protected virtual void LoadSkills()
        {
            // Load all skills here
            this.Skills["None"] = new Skill("None", 0, 0, 0, SkillTypes.Physical, null, Augment, SkillTargetOptions.Directional, SkillTargetPermissions.None);

            this.Skills["Hit"] = new Skill(
                "Hit",
                0, 5,
                1,
                SkillTypes.Physical,
                new Statsheet() { Health = 50 },
                Damage,
                SkillTargetOptions.Directional,
                SkillTargetPermissions.All);

            this.Skills["Throw Rock"] = new Skill(
                "Throw Rock",
                0, 10,
                5,
                SkillTypes.Physical,
                new Statsheet() { Health = 20 },
                Damage,
                SkillTargetOptions.Directional,
                SkillTargetPermissions.All);

            this.Skills["Huddle"] = new Skill(
                "Huddle",
                0, 5,
                0,
                SkillTypes.Physical,
                new Statsheet() { Health = 30 },
                Augment,
                SkillTargetOptions.Directional,
                SkillTargetPermissions.All);

            this.Skills["Start Fire"] = new Skill(
                "Start Fire",
                10, 10,
                1,
                SkillTypes.Summon,
                new Statblock(8, 0),
                Augment,
                SkillTargetOptions.Directional,
                SkillTargetPermissions.Ground);

            // Ice bolt...
            // Electric shock...
        }

        /// <summary>
        /// Augment target with effect.
        /// </summary>
        /// <param name="target">Target to augment.</param>
        /// <param name="effect">Effect to use.</param>
        void Augment(Actor target, IStatsheet effect)
        {
            target.Stats.Add(effect);
        }

        /// <summary>
        /// Damage target with effect.
        /// </summary>
        /// <param name="target">Target to damage.</param>
        /// <param name="effect">Effect to use.</param>
        void Damage(Actor target, IStatsheet effect)
        {
            target.Stats.Remove(effect);
        }
    }
}
