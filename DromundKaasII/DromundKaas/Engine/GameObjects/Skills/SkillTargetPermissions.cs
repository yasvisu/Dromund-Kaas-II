using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine.GameObjects.Skills
{
    [Flags]
    public enum SkillTargetPermissions
    {
        None = 0x0,
        Ground = 0x1,
        Actor = 0x2,
        All = 0x3
    }
}
