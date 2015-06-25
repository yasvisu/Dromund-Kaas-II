using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Interfaces
{
    public interface IStatblock : IStatsheet
    {
        // Primary stats
        int MaxHealth { get; }
        int MaxMana { get; }
        int MaxFocus { get; }
    }
}
