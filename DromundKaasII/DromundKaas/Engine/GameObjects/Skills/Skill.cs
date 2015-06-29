using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine.GameObjects.Skills
{
    /// <summary>
    /// Delegate for an actor affector function.
    /// </summary>
    /// <param name="target">The target to affect.</param>
    /// <param name="effect">The effect to enforce.</param>
    public delegate void ActorAffectorDelegate(Actor target, IStatsheet effect);

    /// <summary>
    /// Basic skill containing all appropriate data.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Initializes a new Skill.
        /// </summary>
        /// <param name="name">The name of the skill.</param>
        /// <param name="manaCost">The mana cost of the skill.</param>
        /// <param name="focusCost">The focus cost of the skill.</param>
        /// <param name="range">The range of the skill.</param>
        /// <param name="skillType">The type of the skill.</param>
        /// <param name="effect">The stat effect of the skill.</param>
        /// <param name="affect">The affector delegate of the skill.</param>
        /// <param name="targetOptions">The target options of the skill.</param>
        /// <param name="targetPermissions">The target permissions of the skill.</param>
        public Skill(string name, int manaCost, int focusCost, int range, SkillTypes skillType, IStatsheet effect, ActorAffectorDelegate affect, SkillTargetOptions targetOptions, SkillTargetPermissions targetPermissions)
        {
            this.Name = name;
            this.ManaCost = manaCost;
            this.FocusCost = focusCost;
            this.Range = range;
            this.SkillType = skillType;
            this.Effect = effect;
            this.Affect = affect;
            this.TargetOptions = targetOptions;
            this.TargetPermissions = targetPermissions;
        }

        /// <summary>
        /// The name of the skill.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The mana cost of the skill.
        /// </summary>
        public int ManaCost { get; set; }

        /// <summary>
        /// The focus cost of the skill.
        /// </summary>
        public int FocusCost { get; set; }

        /// <summary>
        /// The range of the skill.
        /// </summary>
        public int Range { get; set; }


        /// <summary>
        /// The type of the skill.
        /// </summary>
        public SkillTypes SkillType { get; set; }


        /// <summary>
        /// The stat effect of the skill.
        /// </summary>
        public IStatsheet Effect { get; set; }

        /// <summary>
        /// The affector delegate of the skill.
        /// </summary>
        public ActorAffectorDelegate Affect { get; set; }


        /// <summary>
        /// The target options of the skill.
        /// </summary>
        public SkillTargetOptions TargetOptions { get; set; }

        /// <summary>
        /// The target permissions of the skill.
        /// </summary>
        public SkillTargetPermissions TargetPermissions { get; set; }
    }
}
