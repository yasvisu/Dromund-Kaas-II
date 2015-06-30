using System;

public class Node
{
    private int x;
    private int y;
    private bool isEmpty, isWall, isRobot, isGoal;

    public Node(int x, int y, string type)
    {
        this.x = x;
        this.y = y;
        isEmpty = false;
        isWall = false;
        isRobot = false;
        isGoal = false;
        if (type.Equals("empty"))
        {
            isEmpty = true;
        }
        else if (type.Equals("wall"))
        {
            isWall = true;
        }
        else if (type.Equals("robot"))
        {
            isRobot = true;
        }
        else if (type.Equals("goal"))
        {
            isGoal = true;
        }

    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }


    public bool IsEmpty()
    {
        return isEmpty;
    }
    public bool IsWall()
    {
        return isWall;
    }

    public bool IsRobot()
    {
        return isRobot;
    }

    public bool IsGoal()
    {
        return isGoal;
    }

    public void setType(string type)
    {
        if (type.Equals("empty"))
        {
            isWall = false;
            isEmpty = true;
            isRobot = false;
            isGoal = false;
        }
        else if (type.Equals("wall"))
        {
            isWall = true;
            isEmpty = false;
            isRobot = false;
            isGoal = false;
        }
        else if (type.Equals("robot"))
        {
            isWall = false;
            isEmpty = false;
            isRobot = true;
            isGoal = false;
        }
        else if (type.Equals("goal"))
        {
            isWall = false;
            isEmpty = false;
            isRobot = false;
            isGoal = true;
        }
    }
}


