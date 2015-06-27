using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine
{
    [Flags]
    public enum StatusEffects
    {
        Bleeding = 0x1,
        Burning = 0x2,
        Frozen = 0x4,
        Stunned = 0x8,
        Confused = 0x10,
        Weakened = 0x20,
        Fear = 0x40,
        Regeneration = 0x80
    }
}
