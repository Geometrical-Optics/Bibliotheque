using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System;
using System.Collections.Generic;
using Map;

namespace IA;

public class AgressifAstar : NPC
{
    public AgressifAstar(int id, (double X, double Y) coordinates, Box[,] board) : base(id, coordinates, board)
    {
        Symbol = "A";
    }

    public double Distance((double X, double Y) coor, (double X, double Y) coordinates_player)
    {
        return Math.Sqrt(Math.Pow(coor.X - coordinates_player.X, 2) + Math.Pow(coor.Y - coordinates_player.Y, 2));
    }

    public bool testBlacklist((double, double) coor, Queue<(double, double)> stack)
    {
        bool inside = false;
        foreach (var elt in stack)
        {
            if (elt == coor)
            {
                inside = true;
            }
        }

        return inside;
    }

    public (double, double) PathFinding((double X, double Y) coordinates_player)
    {
        Queue<(double, double)> Path = new Queue<(double, double)>();
        Queue<(double, double)> Blacklist = new Queue<(double, double)>();
        while (Coordinates != coordinates_player)
        {
            if (!Board[(int)aux(coordinates_player).X, (int)aux(coordinates_player).Y]
                .IsColliding(aux(coordinates_player)))
                Blacklist.Enqueue(Coordinates);
            if (!testBlacklist(aux(coordinates_player), Blacklist))
            {
                Path.Enqueue(aux(coordinates_player));
                Coordinates = aux(coordinates_player);
            }
            else
                Coordinates = Path.Dequeue();
        }

        return Path.Dequeue();
    }

    public bool valid((double X, double Y) coor)
    {
        if (coor.X >= 0 && coor.X < Board.GetLength(0) && coor.Y >= 0 && coor.Y < Board.GetLength(1) && !Board[(int)coor.X, (int)coor.Y].IsColliding((coor.X,coor.Y)) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public (double, double) min((double X, double Y) coor1, (double X, double Y) coor2, (double X, double Y) coor3,
        (double X, double Y) coordinates_player)
    {
        if (valid(coor1) && valid(coor2) && valid(coor3))
        {
            if (Distance(coor1, coordinates_player) <= Distance(coor2, coordinates_player))
            { 
                if (Distance(coor1, coordinates_player) <= Distance(coor3, coordinates_player)) 
                    return coor1;
                else if (Distance(coor2, coordinates_player) <= Distance(coor3, coordinates_player))
                    return coor2;
                else
                    return coor3;
            }
            else
            {
                if (Distance(coor2, coordinates_player) <= Distance(coor3, coordinates_player))
                    return coor2;
                else
                    return coor3;
            }
        }
        else if (valid(coor1) && valid(coor2))
        {
            if (Distance(coor1, coordinates_player) <= Distance(coor2, coordinates_player))
                return coor1;
            else
                return coor2;
        }
        else if (valid(coor1) && valid(coor3))
        {
            if (Distance(coor1, coordinates_player) <= Distance(coor3, coordinates_player))
                return coor1;
            else
                return coor3;
        }
        else if (valid(coor2) && valid(coor3))
        {
            if (Distance(coor2, coordinates_player) <= Distance(coor3, coordinates_player))
                return coor2;
            else
                return coor3;
        }
        else if (valid(coor1))
            return coor1;
        else if (valid(coor2))
            return coor2;
        else
            return coor3;
    }

    public (double X, double Y) aux((double X, double Y) coordinates_player)
    {
        int decx = 0, decy = 0;

        if (Coordinates == coordinates_player)
            return coordinates_player;

        if (coordinates_player.X != Coordinates.X)
            decx = (coordinates_player.X - Coordinates.X > 0) ? 1 : -1;

        if (Coordinates.Y != coordinates_player.Y)
            decy = (coordinates_player.Y - Coordinates.Y > 0) ? 1 : -1;

        if (decx == 0)
            return (Coordinates.X, Coordinates.Y + decy);
        if (decy == 0)
            return (Coordinates.X + decx, Coordinates.Y);

        return min((Coordinates.X + decx, Coordinates.Y + decy), (Coordinates.X + decx, Coordinates.Y),
            (Coordinates.X, Coordinates.Y + decy), coordinates_player);
    }

    public void Update((double X, double Y) coordinates_player)
    {
        (double, double) oui = PathFinding(coordinates_player);
        Coordinates = oui;
    }
}