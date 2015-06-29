using System;

using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Interfaces;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine
{
    /// <summary>
    /// Static Calculator encompassing common calculation methods.
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// Calculate the experience one actor would give another upon death.
        /// </summary>
        /// <param name="source">The source of the experience.</param>
        /// <param name="target">The target of the experience.</param>
        /// <returns></returns>
        public static int CalculateExperience(Actor source, Actor target)
        {
            return (int)((Calculator.SumStatCoefficients(source) / Calculator.SumStatCoefficients(target)) * source.Stats.Level * source.Stats.Level / target.Stats.Level);
        }

        /// <summary>
        /// Sum the stat coefficients of an actor.
        /// </summary>
        /// <param name="target">The target to sum the coefficients of.</param>
        /// <returns></returns>
        public static float SumStatCoefficients(Actor target)
        {
            return Calculator.SumStats(target) / 10;
        }

        /// <summary>
        /// Sum the stat coefficients of a statsheet.
        /// </summary>
        /// <param name="target">The target to sum the coefficients of.</param>
        /// <returns></returns>
        public static float SumStatCoefficients(IStatsheet stats)
        {
            return Calculator.SumStats(stats) / 10;
        }

        /// <summary>
        /// Sum the stats of an actor.
        /// </summary>
        /// <param name="target">The target to sum the stats of.</param>
        /// <returns></returns>
        public static float SumStats(Actor target)
        {
            return Calculator.SumStats(target.Stats);
        }

        /// <summary>
        /// Sum the stats of a statsheet.
        /// </summary>
        /// <param name="stats">The statsheet to sum the stats of.</param>
        /// <returns></returns>
        public static float SumStats(IStatsheet stats)
        {
            return stats.Charisma + stats.Constitution + stats.Dexterity + stats.Intelligence +
                stats.Psychic + stats.Strength + stats.Wisdom;
        }

        /// <summary>
        /// Calculate the distance between two points.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>The distance between the two points.</returns>
        public static double Distance(Vector2 a, Vector2 b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }
    }
}
