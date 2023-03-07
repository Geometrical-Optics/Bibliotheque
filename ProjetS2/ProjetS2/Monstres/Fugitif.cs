using System.Runtime.Intrinsics.X86;
using System.Text;
using ProjetS2.Map;

namespace ProjetS2;

public class Fugitif : NPC
{
    public Fugitif(int id, (double X, double Y) coordinates, Box[,] board) : base(id, coordinates, board)
    {
        Symbol = "F";
    }

    public double Distance((double X, double Y) coor, (double X, double Y) coordinates_player)
    {
        return Math.Sqrt(Math.Pow(coor.X - coordinates_player.X, 2) + Math.Pow(coor.Y - coordinates_player.Y, 2));
    }

    public bool testBlacklist((double, double) coor, Queue<(double, double)> Stack)
    {
        bool inside = false;
        foreach (var elt in Stack)
        {
            if (elt == coor)
            {
                inside = true;
            }
        }

        return inside;
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

    public (double, double) PathFinding((double X, double Y) coordinates_player)
    {
        Queue<(double, double)> Path = new Queue<(double, double)>();
        Queue<(double, double)> Blacklist = new Queue<(double, double)>();
        
            if (Board[(int)aux(coordinates_player).X, (int)aux(coordinates_player).Y].IsColliding(aux(coordinates_player)))
                Blacklist.Enqueue(Coordinates);
            if (!testBlacklist(aux(coordinates_player), Blacklist))
            {
                Path.Enqueue(aux(coordinates_player));
                Coordinates = aux(coordinates_player);
            }
            else 
                Path.Enqueue(Coordinates);
            return Path.Dequeue();
    }


    public (double, double) max((double X, double Y) coor1, (double X, double Y) coor2, (double X, double Y) coor3,
        (double X, double Y) coordinates_player)
    {
        if (valid(coor1) && valid(coor2) && valid(coor3))
        {
            if (Distance(coor1, coordinates_player) >= Distance(coor2, coordinates_player))
            { 
                if (Distance(coor1, coordinates_player) >= Distance(coor3, coordinates_player)) 
                    return coor1;
                else if (Distance(coor2, coordinates_player) >= Distance(coor3, coordinates_player))
                    return coor2;
                else
                    return coor3;
            }
            else
            {
                if (Distance(coor2, coordinates_player) >= Distance(coor3, coordinates_player))
                    return coor2;
                else
                    return coor3;
            }
        }
        else if (valid(coor1) && valid(coor2))
        {
            if (Distance(coor1, coordinates_player) >= Distance(coor2, coordinates_player))
                return coor1;
            else
                return coor2;
        }
        else if (valid(coor1) && valid(coor3))
        {
            if (Distance(coor1, coordinates_player) >= Distance(coor3, coordinates_player))
                return coor1;
            else
                return coor3;
        }
        else if (valid(coor2) && valid(coor3))
        {
            if (Distance(coor2, coordinates_player) >= Distance(coor3, coordinates_player))
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
            decx = (coordinates_player.X - Coordinates.X > 0) ? -1 : 1;

        if (Coordinates.Y != coordinates_player.Y)
            decy = (coordinates_player.Y - Coordinates.Y > 0) ? -1 : 1;

        if (coordinates_player.X == Coordinates.X)
            decx = (Coordinates.X > 0) ? -1 : 1;

        if (Coordinates.Y == coordinates_player.Y)
            decy = (Coordinates.Y > 0) ? -1 : 1;

        return max((Coordinates.X + decx, Coordinates.Y + decy), (Coordinates.X + decx, Coordinates.Y),
            (Coordinates.X, Coordinates.Y + decy), coordinates_player);
    }
    
    public void Update((double X, double Y)coordinates_player)
    {
        Coordinates = PathFinding(coordinates_player);
    }
}
