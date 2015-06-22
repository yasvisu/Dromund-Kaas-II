using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Actors;

namespace DromundKaasII.Engine
{
    public static class Calculator
    {
        public static int CalculateExperience(Actor source, Actor target)
        {
            return (int)((SumStatCoefficients(source) / SumStatCoefficients(target)) * source.Stats.Level * source.Stats.Level / target.Stats.Level);
        }

        public static float SumStatCoefficients(Actor target)
        {
            return SumStats(target) / 10;
        }

        public static float SumStatCoefficients(Statblock stats)
        {
            return SumStats(stats) / 10;
        }

        public static float SumStats(Actor target)
        {
            return SumStats(target.Stats);
        }

        public static float SumStats(Statblock stats)
        {
            return stats.Charisma + stats.Constitution + stats.Dexterity + stats.Intelligence +
                stats.Psychic + stats.Strength + stats.Wisdom;
        }
    }
}
