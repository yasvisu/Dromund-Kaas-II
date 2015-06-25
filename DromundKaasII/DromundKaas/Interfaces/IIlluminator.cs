using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Interfaces
{
    public interface IIlluminator : IPlaceable
    {
        Color IlluminationColor { get; }
        float IlluminationRange { get; }
        bool HasIlluminated { get; }
    }
}
