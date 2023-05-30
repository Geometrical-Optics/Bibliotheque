using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System;
using System.Collections.Generic;
using IA.Item;
using Map;

namespace IA;

public class AgressifAstar : NPC
{
    private bool Vu;
    private AssaultWeapon Weapon;

    public int Health;
    //private Player _player;


    public AgressifAstar(int health, (double X, double Y) coordinates, Carte board, float speed) : base(health, coordinates,
        board, speed)
    {
        Health = health;
        Symbol = "A";
        Vu = false;
        Weapon = new AssaultWeapon("Sword", 50);
        //_player = new Player();
    }

    public double Distance((double X, double Y) coor, (double X, double Y) coordinates_player)
    {
        return Math.Sqrt(Math.Pow(coor.X - coordinates_player.X, 2) + Math.Pow(coor.Y - coordinates_player.Y, 2));
    }

    public bool testBlacklist((double, double) coor, Queue<(double, double)> stack)
    {
        if (stack.Contains(coor))
            return true;
        else
            return false;
    }

    public (double, double) PathFinding((double X, double Y) coordinates_player)
    {
        Queue<(double, double)> Path = new Queue<(double, double)>();
        Queue<(double, double)> Blacklist = new Queue<(double, double)>();
        /*
        while (Path.Count < 3 && Coordinates != coordinates_player)
        {
            //if (!Board[(int)aux(coordinates_player).X, (int)aux(coordinates_player).Y]
            //        .IsColliding(aux(coordinates_player)))
            //    Blacklist.Enqueue(Coordinates);
            //if (!testBlacklist(aux(coordinates_player), Blacklist))
            if (valid(aux(coordinates_player)))
            {
                Path.Enqueue(aux(coordinates_player));
                Coordinates = aux(coordinates_player);
            }
            //else if (Path.Count != 0)
              //  Coordinates = Path.Dequeue();
        }
        */
        /*
        while (Coordinates != coordinates_player)
        {
            (double, double) coordinates = aux(coordinates_player);
            if (!Board[(int)aux(coordinates_player).X, (int)aux(coordinates_player).Y]
                    .IsColliding(aux(coordinates_player)))
                Blacklist.Enqueue(Coordinates);
            if (!testBlacklist(aux(coordinates_player), Blacklist))
            {
                Path.Enqueue(aux(coordinates_player));
                Coordinates = aux(coordinates_player);
            }
            else if (Path.Count != 0)
                Coordinates = Path.Dequeue();
        }
        */
        while (Path.Count < 15 && Coordinates != coordinates_player)
        {
            (double, double) coordinates = aux(coordinates_player);
            if (!valid(coordinates))
                Blacklist.Enqueue(Coordinates);
            if (!testBlacklist(coordinates, Blacklist))
            {
                Path.Enqueue(coordinates);
                Coordinates = coordinates;
            }
            else if (Path.Count != 0)
                Coordinates = Path.Dequeue();
        }
        
        return Path.Dequeue();
    }

    public bool valid((double X, double Y) coor)
    {
        //Console.WriteLine(coor.X >= 0 && coor.X < Board.GetLength(0) && coor.Y >= 0 && coor.Y < Board.GetLength(1) && Board[(int)coor.X, (int)coor.Y] is Empty);
        if (coor.X >= 0 && coor.X < Board.GetLength(0) && coor.Y >= 0 && coor.Y < Board.GetLength(1) && Board[(int)coor.X, (int)coor.Y] is Empty)
            //Board[(int)Math.Ceiling(coor.X), (int)Math.Ceiling(coor.Y)] is Empty)
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
        double decx = 0, decy = 0;

        if (Coordinates == coordinates_player)
            return coordinates_player;

        if (coordinates_player.X != Coordinates.X)
            decx = (coordinates_player.X - Coordinates.X > 0) ? 0.1 * Speed : -0.1 * Speed;

        if (Coordinates.Y != coordinates_player.Y)
            decy = (coordinates_player.Y - Coordinates.Y > 0) ? 0.1 * Speed : -0.1 * Speed;

        if (decx == 0)
            return (Coordinates.X, Coordinates.Y + decy);
        if (decy == 0)
            return (Coordinates.X + decx, Coordinates.Y);
        
        return min((Coordinates.X + decx, Coordinates.Y + decy), (Coordinates.X + decx, Coordinates.Y),
            (Coordinates.X, Coordinates.Y + decy), coordinates_player);
    }


    public bool CanAttack((double X, double Y) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < 0.8 && Math.Abs(Coordinates.Y - coordinates_player.Y) < 0.8)
            return true;
        else
            return false;
    }
    
    public bool CanAttackNPC((double X, double Y) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < 0.4 && Math.Abs(Coordinates.Y - coordinates_player.Y) < 0.4)
            return true;
        else
            return false;
    }

    public bool CanMove((double X, double Y) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < Board.GetLength(0)/3 &&
            Math.Abs(Coordinates.Y - coordinates_player.Y) < Board.GetLength(1)/3 )
            return true;
        else
            return false;
    }

    public void Attack((double X, double Y) coordinates_player, Player _player)
    {
        if (_player.Health - Weapon.Damage > 0)
            _player.Health -= Weapon.Damage;
        else
            _player.Health = 0;
    }
    
    public void AttackNPC(NPC npc)
    {
        if (npc.Health - Weapon.Damage > 0)
            npc.Health -= Weapon.Damage;
        else
            npc.Health = 0;
    }

    public void Update((double X, double Y) coordinates_player, Player _player)
    {
        //if (_player.IsAlive())
        //{
            Vu = CanMove(coordinates_player);
            if (Vu & Coordinates != coordinates_player)
            {
                (double X, double Y) oui = PathFinding(coordinates_player);
                Coordinates = oui;
            }

            Vu = false;
            if (CanAttack(coordinates_player))
            {
                Attack(coordinates_player, _player);
                Console.WriteLine($"Your health is now: {_player.Health}");
            }
        // }

    }

    public void UpdateNPC(NPC npc)
    {
        if (Coordinates != npc.Coordinates)
        {
            (double X, double Y) oui = PathFinding(npc.Coordinates);
            Coordinates = oui;
        }

        Vu = false;
        if (CanAttackNPC(npc.Coordinates))
        {
            AttackNPC(npc);
            Console.WriteLine($"Your health is now: {npc.Health}");
        }
    }
}