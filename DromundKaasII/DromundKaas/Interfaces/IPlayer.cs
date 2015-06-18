using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Interfaces
{
    using DromundKaasII.GameObjects.Skills;

    public interface IPlayer
    {
        int Score { get; }

        new Skill[] PlayerSkills { get; }
    }
}
