using System.Collections.Generic;
using DromundKaasII.Engine;
using DromundKaasII.Engine.GameObjects.Actors;
using Microsoft.Xna.Framework;

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

        IEnumerable<StatusEffects> Status { get; }

    }
}
