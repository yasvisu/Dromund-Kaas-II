using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine.Interfaces
{
    public interface IPathable : ITile
    {
        double TraversalCost { get; }
        IActor Occupant { get; set; }
    }
}
