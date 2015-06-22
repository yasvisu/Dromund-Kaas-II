using DromundKaasII.Engine.GameObjects.Enums;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Exposes the options of the engine.
    /// </summary>
    public interface IEngineOptions
    {
        /// <summary>
        /// The speed at which the engine state updates.
        /// </summary>
        GameSpeedOptions GameSpeed { get; set; }

        /// <summary>
        /// The difficulty of the gameplay.
        /// </summary>
        GameDifficultyOptions GameDifficulty { get; set; }
    }
}
