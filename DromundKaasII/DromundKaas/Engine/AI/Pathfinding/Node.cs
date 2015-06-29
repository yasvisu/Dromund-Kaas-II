﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine.AI.Pathfinding
{
    /// <summary>
    /// Helps Path Finding class to determine each Node (each step)
    /// 
    /// A Path finding class used to calclulate the enemy NPC's Movements
    /// the A* algorithm is used: https://en.wikipedia.org/wiki/A*_search_algorithm
    /// </summary>
    public class Node
    {
        private int x;

        private int y;

        // private bool isEmpty, isWall, isGoal, isZombieFriend;

        /// <summary>
        /// Basic constructor.
        /// Takes coordinates x, y, and box type.
        /// </summary>
        public Node(int x, int y, string type)
        {
            this.X = x;
            this.Y = y;
            this.IsEmpty = false;
            this.IsWall = false;
            this.IsZombieFriend = false;
            this.IsGoal = false;
            if (type.ToLower().Equals("empty"))
            {
                this.IsEmpty = true;
            }
            if (type.ToLower().Equals("wall"))
            {
                this.IsWall = true;
            }
            if (type.ToLower().Equals("goal"))
            {
                this.IsGoal = true;
            }
            if (type.ToLower().Equals("zombie"))
            {
                this.IsZombieFriend = true;
            }
        }

        public bool IsEmpty { get; set; }

        public bool IsZombieFriend { get; set; }

        public bool IsGoal { get; set; }

        public bool IsWall { get; set; }

        /// <summary>
        /// Get the Y coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Get the X coordinate
        /// </summary>
        public int X { get; set; }


        //public void SetType(string type)
        //{
        //    if (type.ToLower().Equals("empty"))
        //    {
        //        this.IsWall = false;
        //        this.IsEmpty = true;
        //        this.IsZombieFriend = false;
        //        this.IsGoal = false;
        //    }
        //    else if (type.ToLower().Equals("wall"))
        //    {
        //        this.IsWall = true;
        //        this.IsEmpty = false;
        //        this.IsZombieFriend = false;
        //        this.IsGoal = false;
        //    }
        //    else if (type.ToLower().Equals("zombie"))
        //    {
        //        this.IsWall = false;
        //        this.IsEmpty = false;
        //        this.IsZombieFriend = true;
        //        this.IsGoal = false;
        //    }
        //    else if (type.ToLower().Equals("goal"))
        //    {
        //        this.IsWall = false;
        //        this.IsEmpty = false;
        //        this.IsZombieFriend = false;
        //        this.IsGoal = true;
        //    }
    }
}
