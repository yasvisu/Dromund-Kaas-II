using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Interfaces
{
    public interface IPlaceable
    {
        /// <summary>
        /// The position of the actor.
        /// </summary>
        Vector2 MapPosition { get; set; }
    }
}
