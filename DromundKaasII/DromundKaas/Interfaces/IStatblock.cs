#pragma warning disable 1591

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Exposes a Statblock's maximum values.
    /// </summary>
    public interface IStatblock : IStatsheet
    {
        // Primary stats
        int MaxHealth { get; }
        int MaxMana { get; }
        int MaxFocus { get; }
    }
}

#pragma warning restore 1591