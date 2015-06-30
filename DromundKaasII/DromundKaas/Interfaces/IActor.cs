using System.Collections.Generic;

using DromundKaasII.Engine;
using DromundKaasII.Engine.GameObjects.Actors;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Exposes the position and direction of an actor.
    /// </summary>
    public interface IActor : IPlaceable
    {
        /// <summary>
        /// The direction of the actor.
        /// </summary>
        Directions Direction { get; set; }

        /// <summary>
        /// A collection of the StatusEffects affecting the Actor.
        /// </summary>
        IEnumerable<StatusEffects> Status { get; }
    }
}
