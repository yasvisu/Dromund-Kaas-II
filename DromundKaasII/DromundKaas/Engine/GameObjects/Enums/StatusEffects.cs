using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine.GameObjects.Enums
{
    [Flags]
    public enum StatusEffects
    {
        Bleeding=0x1,
        Burning=0x2,
        Frozen=0x4,
        Stunned=0x6,
        Confused=0x8,
        Weakened=0x10
    }
}
