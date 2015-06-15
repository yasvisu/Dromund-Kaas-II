using DromundKaasII.GameObjects.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Interfaces
{
    public interface IPathable
    {
        double TraversalCost { get; }
        Actor Occupant { get; }
    }
}
