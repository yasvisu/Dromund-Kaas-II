using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Interfaces
{
    /// <summary>
    /// Interface exposing a Skill's name.
    /// </summary>
    public interface ISkill
    {
        /// <summary>
        /// The name of the skill.
        /// </summary>
        string Name { get; }
    }
}
