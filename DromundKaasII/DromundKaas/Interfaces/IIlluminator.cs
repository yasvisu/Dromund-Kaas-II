using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Interface exposing an illuminating element's properties.
    /// </summary>
    public interface IIlluminator : IPlaceable
    {
        /// <summary>
        /// The color of the illuminator's light.
        /// </summary>
        Color IlluminationColor { get; }

        /// <summary>
        /// The range the illuminator illuminates.
        /// </summary>
        float IlluminationRange { get; }

        /// <summary>
        /// Whether the illuminator has arbitrarily illuminated.
        /// </summary>
        bool HasIlluminated { get; set; }
    }
}
