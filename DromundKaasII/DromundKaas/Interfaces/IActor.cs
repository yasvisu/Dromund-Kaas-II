using DromundKaasII.Engine.GameObjects.Actors;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Exposes the position and direction of an actor.
    /// </summary>
    public interface IActor
    {
        /// <summary>
        /// The position of the actor.
        /// </summary>
        Vector2 MapPosition { get; set; }

        /// <summary>
        /// The direction of the actor.
        /// </summary>
        Directions Direction { get; set; }
    }
}
