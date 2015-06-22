using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using DromundKaasII.GameObjects.Skills;
using DromundKaasII.Input;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// IPlayer interface for all players.
    /// </summary>
    public interface IPlayer : IActor
    {
        /// <summary>
        /// The score accumulated so far in the game.
        /// </summary>
        int Score { get; }

        /// <summary>
        /// A collection of the skills the player has.
        /// </summary>
        Skill[] PlayerSkills { get; }

        /// <summary>
        /// The current desired action, based on input.
        /// </summary>
        GameInputs DesiredAction { get; set; }
    }
}
