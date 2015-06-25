using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Interfaces
{
    public interface IStatsheet
    {
        // Combat
        int Health { get; set; }
        int Mana { get; set; }
        int Focus { get; set; }

        // Experience
        int Experience { get; set; }
        int Level { get; set; }

        float Strength { get; set; }
        float Dexterity { get; set; }
        float Constitution { get; set; }
        float Intelligence { get; set; }

        // Secondary stats
        float Wisdom { get; set; }
        float Charisma { get; set; }
        float Psychic { get; set; }

        // Movement
        int TraversalPower { get; set; }

        // Sight
        float IlluminationRange { get; set; }

        void Add(IStatsheet target);

        void Remove(IStatsheet target);
    }
}
