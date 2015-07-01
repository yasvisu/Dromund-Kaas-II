using System;

using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine
{
    /// <summary>
    /// Static Calculator encompassing common calculation methods and extensions to household classes.
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
        /// <param name="stats">The target to sum the coefficients of.</param>
        /// <returns>The sum of the stat coefficients.</returns>
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

        /// <summary>
        /// Casts a GameInput into a Direction.
        /// </summary>
        /// <param name="input">The GameInput to cast.</param>
        /// <returns>The Direction corresponding to the GameInput.</returns>
        public static Directions ToDirection(this GameInputs input)
        {
            switch(input)
            {
                case GameInputs.Up:
                    return Directions.North;
                case GameInputs.Down:
                    return Directions.South;
                case GameInputs.Left:
                    return Directions.West;
                case GameInputs.Right:
                    return Directions.East;
                default:
                    throw new ArgumentException("Input could not be parsed into Directions.");
            }
        }

        /// <summary>
        /// Casts a Direction into a GameInput.
        /// </summary>
        /// <param name="input">The Direction to cast.</param>
        /// <returns>The GameInput corresponding to the Direction.</returns>
        public static GameInputs ToGameInput(this Directions input)
        {
            switch (input)
            {
                case Directions.North:
                    return GameInputs.Up;
                case Directions.South:
                    return GameInputs.Down;
                case Directions.West:
                    return GameInputs.Left;
                case Directions.East:
                    return GameInputs.Right;
                default:
                    throw new ArgumentException("Input could not be parsed into GameInputs");
            }
        }
    }
}
