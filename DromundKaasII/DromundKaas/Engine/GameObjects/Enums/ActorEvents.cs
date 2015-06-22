using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine.GameObjects.Enums
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
        /// Raised when an Actor casts a spell.
        /// </summary>
        SpellCast
    }
}
