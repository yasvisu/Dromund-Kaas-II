using System;
using System.Collections.Generic;
using System.Linq;
using DromundKaasII.Engine.AI.Pathfinding;


namespace Pathfinding
{

    public class WumpusWorld
    {

        private static int size;
        private static Node startpoint;
        private static Node endpoint;
        private static int mode;
        private static String[][] nodeTypes;

        public static void Main()
        {
            LinkedList<Node> thegrid = new LinkedList<Node>();

            // get local variables from GUI
            size = 10;
            mode = 0; // Manhattan Heuristic

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    thegrid.AddLast(new Node(i, j, nodeTypes[i][j]));
                }
            }
            Pathfinder pathf = new Pathfinder(thegrid);
            string temp = pathf.Pathfind(startpoint, endpoint, mode);
            temp = temp.Substring(1);
            temp = temp.Substring(0, temp.Count());
            string[] result = temp.Split(')', '(');
            int finalcounter = result.Length;
            LinkedList<Node> victoryPath = new LinkedList<Node>();
            foreach (string a in result)
            {
                string[] b = a.Split(',');
                victoryPath.AddLast(new Node(int.Parse(b[0]), int.Parse(b[1]), "empty"));
            }

            // print this in GUI
        }
    }
}


