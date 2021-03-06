﻿using System;
using System.Collections.Generic;
using System.Linq;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.AI.Pathfinding
{
    /// <summary>
    /// Class containing pathfinding and orientation functionalities.
    /// </summary>
    public class Pathfinder
    {
        GameState gameState;
        private double movementCost = 1.0;
        private Dictionary<IPathable, Double> gmap;
        private Dictionary<IPathable, Double> fmap;
        private Dictionary<IPathable, IPathable> navmap;
        private static int heuristicMode;

        /// <summary>
        /// Initializes a new Pathfinder.
        /// </summary>
        /// <param name="G">The GameState to use in complex pathfinding.</param>
        public Pathfinder(GameState G)
        {
            this.gameState = G;
        }

        /// <summary>
        /// The cost of a movement.
        /// </summary>
        public double MovementCost
        {
            get
            {
                return this.movementCost;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("MovementCost", "Value cannot be negative!");
                }
                this.movementCost = value;
            }
        }

        /// <summary>
        /// Returns the direction of the target, relative to the parent.
        /// </summary>
        /// <param name="parent">The origin position.</param>
        /// <param name="target">The target position.</param>
        /// <returns>The direction in which the target is.</returns>
        public Directions Orient(Vector2 parent, Vector2 target)
        {
            Directions result;

            float dx = target.X - parent.X;
            float dy = target.Y - parent.Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (dx > 0)
                {
                    result = Directions.East;
                }
                else
                {
                    result = Directions.West;
                }
            }
            else
            {
                if (dy >= 0)
                {
                    result = Directions.South;
                }
                else
                {
                    result = Directions.North;
                }
            }

            return result;
        }

        /// <summary>
        /// Pathfinds between two pathables.
        /// </summary>
        /// <param name="start">The start pathable.</param>
        /// <param name="goal">The goal pathable.</param>
        /// <param name="mode">The heuristic mode (Manhattan or Diagonal movement)</param>
        /// <returns>The path to the target, if there is any.</returns>
        public LinkedList<IPathable> Pathfind(IPathable start, IPathable goal, int mode)
        {
            heuristicMode = mode;
            return astar(start, goal);
        }

        private LinkedList<IPathable> astar(IPathable start, IPathable goal)
        {
            HashSet<IPathable> closedset = new HashSet<IPathable>(); //set of evaluated IPathables
            HashSet<IPathable> openset = new HashSet<IPathable>(); //set of IPathables to be evaluated
            openset.Add(start); //first IPathable to be evaluated
            this.gmap = Pathfinder.populateHashMap(this.gameState); //initialize HashMap of visited IPathables
            this.navmap = new Dictionary<IPathable, IPathable>();
            this.fmap = Pathfinder.populateHashMap(this.gameState);


            //Heuristic: F=G+H
            //F = total cost
            //G = path cost
            //H = heuristic cost
            //put appropriate G for start
            gmap[start]= 0.0;
            fmap[start]= gmap[start] + ManhattanDistance(start, goal); //heuristic cost of 0

            IPathable current;

            while (openset.Any())
            {
                current = GetCheapest(openset);
                if (current == goal)
                {
                    return constructPath(navmap, goal);
                }

                openset.Remove(current);
                closedset.Add(current);

                HashSet<IPathable> neighbors = GetNeighbors(current, heuristicMode);
                foreach (IPathable neighbor in neighbors)
                {
                    if (closedset.Contains(neighbor))
                        continue;

                    double temp_g = gmap[current] + movementCost;
                    if (!openset.Contains(neighbor) || temp_g < gmap[neighbor])
                    {
                        navmap.Add(neighbor, current);
                        gmap[neighbor]= temp_g;
                        fmap[neighbor]= gmap[neighbor] + heuristicCost(neighbor, goal, heuristicMode);
                        if (!openset.Contains(neighbor))
                            openset.Add(neighbor);
                    }
                }
            }
            return null;
        }

        private double heuristicCost(IPathable current, IPathable goal, int mode)
        {
            switch (mode)
            {
                case 1:
                    return DiagonalDistance(current, goal);
                default:
                    return ManhattanDistance(current, goal);
            }
        }

        #region Heuristics

        private double ManhattanDistance(IPathable current, IPathable goal)
        {
            return movementCost * (Math.Abs(current.MapPosition.X - goal.MapPosition.X) + Math.Abs(current.MapPosition.Y - goal.MapPosition.Y));
        }

        private double DiagonalDistance(IPathable current, IPathable goal)
        {
            return
                movementCost * (Math.Abs(current.MapPosition.X - goal.MapPosition.X) + Math.Abs(current.MapPosition.Y - goal.MapPosition.Y)) +
                (Math.Sqrt(2) * movementCost - 2 * movementCost) * Math.Min(Math.Abs(current.MapPosition.X - goal.MapPosition.X), Math.Abs(current.MapPosition.Y - goal.MapPosition.Y));
        }

        #endregion

        private static Dictionary<IPathable, Double> populateHashMap(GameState G)
        {
            Dictionary<IPathable, Double> result = new Dictionary<IPathable, double>();
            foreach (IPathable p in G.Map)
            {
                result.Add(p, -1.0);
            }
            return result;
        }

        private IPathable GetCheapest(HashSet<IPathable> openset)
        {
            // return openset.Aggregate((x, y) => fmap[x] < fmap[y] ? x : y);

            var iterator = openset.GetEnumerator();
            IPathable max = iterator.Current;
            double maxValue = fmap[max];

            while (iterator.MoveNext())
            {
                IPathable current = iterator.Current;
                double currentValue = fmap[current];

                if (currentValue < maxValue)
                {
                    max = current;
                    maxValue = currentValue;
                }
            }
            return max;
        }

        private HashSet<IPathable> GetNeighbors(IPathable current, int mode)
        {
            HashSet<IPathable> result = new HashSet<IPathable>();

            Point master = current.MapPosition.ToPoint();

            List<Point> toCheck = new List<Point>()
            {
                master + new Point(0, -1),
                master + new Point(0, 1),
                master + new Point(-1, 0),
                master + new Point(1, 0)
            };

            if (mode == 1)
            {
                toCheck.AddRange(new List<Point>()
                    {
                        master + new Point(-1, -1),
                        master + new Point(-1, 1),
                        master + new Point(1, -1),
                        master + new Point(1, 1)
                    });
            }

            foreach (Point p in toCheck)
            {
                if (IsValidPoint(p))
                {
                    result.Add(this.gameState.Map[p.Y, p.X]);
                }
            }
            return result;
        }

        private bool IsValidPoint(Point p)
        {
            return p.X >= 0 && p.X < this.gameState.MapWidth && p.Y >= 0 && p.Y < this.gameState.MapHeight;
        }

        private LinkedList<IPathable> constructPath(Dictionary<IPathable, IPathable> map, IPathable last)
        {
            LinkedList<IPathable> result = new LinkedList<IPathable>();

            IPathable current = last;
            result.AddFirst(current);

            while (map.ContainsKey(current))
            {
                current = map[current];
                result.AddFirst(current);
            }
            return result;
        }
    }
}