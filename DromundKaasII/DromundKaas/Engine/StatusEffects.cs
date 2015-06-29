using System;

namespace DromundKaasII.Engine
{
    /// <summary>
    /// The kinds of status effects.
    /// </summary>
    [Flags]
    public enum StatusEffects
    {
        /// <summary>
        /// Bleeding effect.
        /// </summary>
        Bleeding = 0x1,

        /// <summary>
        /// Burning effect.
        /// </summary>
        Burning = 0x2,

        /// <summary>
        /// Frozen effect.
        /// </summary>
        Frozen = 0x4,

        /// <summary>
        /// Stunned effect.
        /// </summary>
        Stunned = 0x8,

        /// <summary>
        /// Confused effect.
        /// </summary>
        Confused = 0x10,

        /// <summary>
        /// Weakened effect.
        /// </summary>
        Weakened = 0x20,

        /// <summary>
        /// Fear effect.
        /// </summary>
        Fear = 0x40,

        /// <summary>
        /// Regeneration effect.
        /// </summary>
        Regeneration = 0x80
    }
}
