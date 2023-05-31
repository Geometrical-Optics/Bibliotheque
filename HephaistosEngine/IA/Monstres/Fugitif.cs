using System.Text;
using Map;
using System;
using System.Collections.Generic;
using IA.Item;

namespace IA;

public class Fugitif : NPC
{
    private bool Vu;
    private HealingWeapon Weapon;

    public bool stop;


    public Fugitif(int health, (double X, double Y, double Z) coordinates, Carte board, float speed) : base(health, coordinates, board, speed)
    {
        Health = health;
        Symbol = "F";
        Vu = false;
        Weapon = new HealingWeapon("Stick", 5);
        stop = false;
    }
    

    public double Distance((double X, double Y, double Z) coor, (double X, double Y, double Z) coordinates_player)
    {
        return Math.Sqrt(Math.Pow(coor.X - coordinates_player.X, 2) + Math.Pow(coor.Y - coordinates_player.Y, 2));
    }

    public bool testBlacklist((double, double, double) coor, Queue<(double, double, double)> Stack)
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
    
    public bool valid((double X, double Y, double Z) coor)
    { 
        if ( Board.IsColliding(( coor.X, coor.Y , (float)coor.Z)) == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public (double, double, double) PathFinding((double X, double Y, double Z) coordinates_player)
    {
        Queue<(double, double, double)> Path = new Queue<(double, double, double)>();
        Queue<(double, double, double)> Blacklist = new Queue<(double, double, double)>();
        
            
            if (Board.IsColliding(( coordinates_player.X, coordinates_player.Y , (float)coordinates_player.Z)) == false)
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


    public (double, double, double) max((double X, double Y, double Z) coor1, (double X, double Y, double Z) coor2, (double X, double Y, double Z) coor3,
        (double X, double Y, double Z) coordinates_player)
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

    public (double X, double Y, double Z) aux((double X, double Y, double Z) coordinates_player)
    {
        double decx = 0, decy = 0;

        if (Coordinates == coordinates_player)
            return coordinates_player;

        if (coordinates_player.X != Coordinates.X)
            decx = (coordinates_player.X - Coordinates.X > 0) ? -0.1*Speed : 0.1*Speed;

        if (Coordinates.Y != coordinates_player.Y)
            decy = (coordinates_player.Y - Coordinates.Y > 0) ? -0.1*Speed : 0.1*Speed;

        if (coordinates_player.X == Coordinates.X)
            decx = (Coordinates.X > 0) ? -0.1*Speed : 0.1*Speed;

        if (Coordinates.Y == coordinates_player.Y)
            decy = (Coordinates.Y > 0) ? -0.1*Speed : 0.1*Speed;

        return max((Coordinates.X + decx, Coordinates.Y + decy, Coordinates.Z), (Coordinates.X + decx, Coordinates.Y, Coordinates.Z),
            (Coordinates.X, Coordinates.Y + decy, Coordinates.Z), coordinates_player);
    }
    
    public bool HaveToHeal((double X, double Y, double Z) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < 0.5 && Math.Abs(Coordinates.Y - coordinates_player.Y) < 0.5)
            return true;
        else
            return false;
    }

    public bool CanMove((double X, double Y, double Z) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < Board.GetLength(0)/2 && Math.Abs(Coordinates.Y - coordinates_player.Y) < Board.GetLength(1)/2)
            return true;
        else
            return false;
    }
    
    public bool CanMoveNPC((double X, double Y, double Z) coordinates)
    {
        if (Math.Abs(Coordinates.X - coordinates.X) < Board.GetLength(0)/3 && Math.Abs(Coordinates.Y - coordinates.Y) < Board.GetLength(1)/3)
            return true;
        else
            return false;
    }

    public void Heal((double X, double Y, double Z) coordinates_player, Player _player)
    {
        if (_player.Health < 1000)
        {
            _player.Health += Weapon.Care;
        }
    }
    
    public void HealNPC(NPC npc)
    {
        if (npc.Health < 1000)
            npc.Health += Weapon.Care;
    }

    public void Update((double X, double Y, double Z) coordinates_player, Player _player)
    {
        if (Health > 0 )
        {
            if (_player.IsAlive())
            {
                if (stop == false)
                {
                    Vu = CanMove(coordinates_player);
                    if (Vu & Coordinates != coordinates_player)
                    {
                        var coor = aux(coordinates_player);
                        
                        if (valid(coor))
                        {
                            Coordinates = coor;
                        }
                        else
                        {
                            stop = true;
                        }
                    }
                }

                Vu = false;
                if (HaveToHeal(coordinates_player))
                {
                    if (_player.Health == 100)
                    {
                        Console.WriteLine("You can't get more care !");
                    }
                    else
                    {
                        Heal(coordinates_player, _player);
                        Console.WriteLine($"Your health is now: {_player.Health}");
                    }
                }

                
            }

            Health -= 1;
        }
    }

    public void UpdateNPC(NPC npc)
    {
        if (Health > 0 )
        {
            if (npc.Health > 0)
            {
                Vu = CanMoveNPC(npc.Coordinates);
                if (Vu & Coordinates != npc.Coordinates)
                {
                    (double, double, double) oui = aux(npc.Coordinates);
                    Coordinates = oui;
                }

                Vu = false;
                if (HaveToHeal(npc.Coordinates))
                {
                    if (npc.Health == 100)
                    {
                        Console.WriteLine("You can't get more care !");
                    }
                    else
                    {
                        HealNPC(npc);
                        Console.WriteLine($"Your health is now: {npc.Health}");
                    }
                }

            }

            Health -= 1;
        }
    }
}
