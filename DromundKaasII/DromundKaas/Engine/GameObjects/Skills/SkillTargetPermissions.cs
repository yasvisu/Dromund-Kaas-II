using System;

namespace DromundKaasII.Engine.GameObjects.Skills
{
    /// <summary>
    /// Skill target permissions.
    /// </summary>
    [Flags]
    public enum SkillTargetPermissions
    {
        /// <summary>
        /// No target.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Ground targets.
        /// </summary>
        Ground = 0x1,

        /// <summary>
        /// Actor targets.
        /// </summary>
        Actor = 0x2,

        /// <summary>
        /// All taragets.
        /// </summary>
        All = 0x3
    }
}
