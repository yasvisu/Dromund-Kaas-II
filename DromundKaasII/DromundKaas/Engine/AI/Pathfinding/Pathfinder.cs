using System;
using System.Collections.Generic;
using System.Linq;

namespace DromundKaasII.Engine.AI.Pathfinding
{

    public class Pathfinder
    {

        private double movementCost = 1.0;
        private IEnumerable<Node> iEnumerableInput;
        private Dictionary<Node, Double> gmap;
        private Dictionary<Node, Double> fmap;
        private Dictionary<Node, Node> navmap;
        private static int heuristicMode;

        public Pathfinder(IEnumerable<Node> input)
        {
            iEnumerableInput = input;
        }


        public string Pathfind(Node start, Node goal, int mode)
        {
            heuristicMode = mode;
            return astar(start, goal);
        }

        private string astar(Node start, Node goal)
        {
            HashSet<Node> closedset = new HashSet<Node>(); //set of evaluated nodes
            HashSet<Node> openset = new HashSet<Node>(); //set of nodes to be evaluated
            openset.Add(start); //first node to be evaluated
            gmap = populateHashMap(iEnumerableInput); //initialize HashMap of visited Nodes
            navmap = new Dictionary<Node, Node>();
            fmap = populateHashMap(iEnumerableInput);


            //Heuristic: F=G+H
            //F = total cost
            //G = path cost
            //H = heuristic cost
            //put appropriate G for start
            gmap.Add(start, 0.0);
            fmap.Add(start, gmap[start] + ManhattanDistance(start, goal)); //heuristic cost of 0

            Node current;

            while (!openset.Any())
            {
                current = GetCheapest(openset);
                if (SameNode(current, goal))
                {
                    return constructPath(navmap, goal);
                }

                openset.Remove(current);
                closedset.Add(current);

                HashSet<Node> neighbors = GetNeighbors(current, heuristicMode);
                foreach (Node neighbor in neighbors)
                {
                    if (closedset.Contains(neighbor))
                        continue;
                    double temp_g = gmap[current] + movementCost;
                    if (!openset.Contains(neighbor) || temp_g < gmap[neighbor])
                    {
                        navmap.Add(neighbor, current);
                        gmap.Add(neighbor, temp_g);
                        fmap.Add(neighbor, gmap[neighbor] + heuristicCost(neighbor, goal, heuristicMode));
                        if (!openset.Contains(neighbor))
                            openset.Add(neighbor);
                    }
                }

            }
            return "failed";
        }

        public void setMovementCost(double cost)
        {
            movementCost = cost;
        }

        private double heuristicCost(Node current, Node goal, int mode)
        {
            switch (mode)
            {
                case 1:
                    return DiagonalDistance(current, goal);
                default:
                    return ManhattanDistance(current, goal);
            }
        }

        private double ManhattanDistance(Node current, Node goal)
        {
            return movementCost*(Math.Abs(current.getX() - goal.getX()) + Math.Abs(current.getY() - goal.getY()));
        }

        private double DiagonalDistance(Node current, Node goal)
        {
            return movementCost*(Math.Abs(current.getX() - goal.getX()) + Math.Abs(current.getY() - goal.getY())) +
                   (Math.Sqrt(2)*movementCost - 2*movementCost)*
                   Math.Min(Math.Abs(current.getX() - goal.getX()), Math.Abs(current.getY() - goal.getY()));
        }

        private Dictionary<Node, Double> populateHashMap(IEnumerable<Node> input)
        {
            Dictionary<Node, Double> hashmap = new Dictionary<Node, double>();
            var iterator = input.GetEnumerator();
            while (iterator.MoveNext())
            {
                hashmap.Add(iterator.Current, -1.0);
            }
            return hashmap;
        }

        private Node GetCheapest(HashSet<Node> openset)
        {
            var iterator = openset.GetEnumerator();
            Node thisNode = iterator.Current;
            Node nextNode;
            while (iterator.MoveNext())
            {
                nextNode = iterator.Current;

                if (fmap[nextNode] < fmap[thisNode])
                    thisNode = nextNode;
            }
            return thisNode;
        }

        private HashSet<Node> GetNeighbors(Node current, int mode)
        {
            HashSet<Node> result = new HashSet<Node>();
            //List<int> keySet = new List<int>(gmap.Keys);
            HashSet<Node> keySet = (HashSet<Node>) gmap.Select(x => x.Key);
            var iterator = keySet.GetEnumerator();
            while (iterator.MoveNext())
            {
                Node thisNode = iterator.Current;
                //Manhattan movement
                if (thisNode.getX() + 1 == current.getX() && thisNode.getY() == current.getY())
                    result.Add(thisNode);
                else if (thisNode.getX() - 1 == current.getX() && thisNode.getY() == current.getY())
                    result.Add(thisNode);
                else if (thisNode.getX() == current.getX() && thisNode.getY() + 1 == current.getY())
                    result.Add(thisNode);
                else if (thisNode.getX() == current.getX() && thisNode.getY() - 1 == current.getY())
                    result.Add(thisNode);
                //Diagonal movement
                else if (mode == 1)
                {
                    if (thisNode.getX() + 1 == current.getX() && thisNode.getY() + 1 == current.getY())
                        result.Add(thisNode);
                    else if (thisNode.getX() + 1 == current.getX() && thisNode.getY() - 1 == current.getY())
                        result.Add(thisNode);
                    else if (thisNode.getX() - 1 == current.getX() && thisNode.getY() + 1 == current.getY())
                        result.Add(thisNode);
                    else if (thisNode.getX() - 1 == current.getX() && thisNode.getY() - 1 == current.getY())
                        result.Add(thisNode);
                }
            }
            return result;
        }

        private bool SameNode(Node thisNode, Node otherNode)
        {
            return thisNode.getX() == otherNode.getX() && thisNode.getY() == otherNode.getY();
        }

        private String constructPath(Dictionary<Node, Node> hashmap, Node last)
        {
            string t;
            if (hashmap.ContainsKey(last))
            {
                t = constructPath(hashmap, hashmap[last]);
                return (t + last);
            }
            else
            {
                return toNodeString(last);
            }
        }

        private string toNodeString(Node a)
        {
            return "(" + a.getX() + "," + a.getY() + ")";
        }
    }

}