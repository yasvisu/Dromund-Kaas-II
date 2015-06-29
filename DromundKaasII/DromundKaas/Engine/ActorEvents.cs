namespace DromundKaasII.Engine
{
    /// <summary>
    /// Types of Actor events.
    /// </summary>
    public enum ActorEvents
    {
        /// <summary>
        /// Raised when Actor dies.
        /// </summary>
        Death,

        /// <summary>
        /// Raised when a new Actor is spawned.
        /// </summary>
        Spawn,

        /// <summary>
        /// Raised when an Actor is hit.
        /// </summary>
        Hit,

        /// <summary>
        /// Raised when an Actor uses a skill.
        /// </summary>
        SkillUse
    }
}
