using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Engine.GameObjects.Skills;
using DromundKaasII.Input;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Exposes player score, skills, and desired action.
    /// </summary>
    public interface IPlayer : IActor
    {
        /// <summary>
        /// The score accumulated so far in the game.
        /// </summary>
        int Score { get; }

        bool LevelUp { get; }


        /// <summary>
        /// The player stats.
        /// </summary>
        Statblock Stats { get; }

        /// <summary>
        /// A collection of the skills the player has.
        /// </summary>
        Skill[] Skills { get; }

        /// <summary>
        /// The current desired action, based on input.
        /// </summary>
        GameInputs DesiredAction { get; set; }
    }
}
