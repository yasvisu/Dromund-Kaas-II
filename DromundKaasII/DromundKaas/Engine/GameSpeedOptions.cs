using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine
{
    /// <summary>
    /// Game Speed options, for the rate at which the Engine is updated.
    /// </summary>
    public enum GameSpeedOptions
    {
        /// <summary>
        /// Slow game speed, at 1500ms per Engine step.
        /// </summary>
        Slow = 1500,

        /// <summary>
        /// Normal game speed, at 1000ms per Engine step.
        /// </summary>
        Normal = 1000,

        /// <summary>
        /// Fast game speed, at 500ms per Engine step.
        /// </summary>
        Fast = 500
    }
}
