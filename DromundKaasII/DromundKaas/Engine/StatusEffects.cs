using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine
{
    [Flags]
    public enum StatusEffects
    {
        Bleeding=0x1,
        Burning=0x2,
        Frozen=0x4,
        Stun=0x6,
        Confusion=0x8,
        Weakened=0x10,
        Fear=0x20
    }
}
