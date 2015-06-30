using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine.Interfaces
{
    /// <summary>
    /// Exposes the occupant and traversal cost of a pathable element.
    /// </summary>
    public interface IPathable : ITile, IPlaceable
    {
        /// <summary>
        /// The traversal cost / power needed to traverse this element.
        /// </summary>
        double TraversalCost { get; }

        /// <summary>
        /// The current occupant of this element.
        /// </summary>
        new IActor Occupant { get; set; }
    }
}
