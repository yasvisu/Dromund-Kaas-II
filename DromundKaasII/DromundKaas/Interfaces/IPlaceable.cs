using Microsoft.Xna.Framework;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Exposes the position of a placeable element.
    /// </summary>
    public interface IPlaceable
    {
        /// <summary>
        /// The position of the actor.
        /// </summary>
        Vector2 MapPosition { get; set; }
    }
}
