using System;
using System.Collections.Generic;
using Map;

namespace IA;

public delegate void Pattern();

public delegate void Upgrade();

public class IAManager
{
    public Box[,] Board;
    public List<NPC> _Monstres;


    public IAManager(Box[,] board)
    {
        Board = board;
        _Monstres = new List<NPC>();
    }
    
    public void Add(NPC monstre)
    {
        monstre.Id = _Monstres.Count;
        _Monstres.Add(monstre);
    }

    public void Remove(int id) => _Monstres.RemoveAll(x => x.Id == id);

    public void Show()
    {
        _Monstres.ForEach(monstre => Console.WriteLine(monstre.ToString()));
    }

    public void RemoveAll(List<NPC> monstres)
    {
        monstres.Clear();
    }

    public void UpdateAll((double X, double Y)coordinates_player)
    {
        foreach (NPC npc in _Monstres)
        {
            switch (npc)
            {
                case AgressifAstar n:
                    n.Update(coordinates_player);
                    break;
                case Fugitif n:
                    n.Update(coordinates_player);
                    break;
                case PatrouilleurX n:
                    n.Update();
                    break;
                case PatrouilleurY n:
                    n.Update();
                    break;
                case PatrouilleurDiag n:
                    n.Update();
                    break;
            }
        }
    }

    public virtual void PrintBoard((double X, double Y)coordinates_player)
    {
        foreach (NPC monstre in _Monstres)
        {
            ConsoleColor bg = Console.BackgroundColor;
            ConsoleColor fg = Console.ForegroundColor;
            for (int y = 0; y < Board.GetLength(1); y++)
            {
                Console.Write("|");
                for (int x = 0; x < Board.GetLength(0); x++)
                {
                    switch (Board[x,y])
                    {
                        case Full:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            break;
                        default:
                            Console.BackgroundColor = bg;
                            break;
                    }
                
                    if ((int)monstre.Coordinates.X == x && (int)monstre.Coordinates.Y == y)
                        Console.Write(monstre.ToString());
                    else if ((int)coordinates_player.X == x && (int)coordinates_player.Y == y)
                        Console.Write("P");
                    else
                        Console.Write(" ");
                }
                Console.BackgroundColor = bg;
                Console.ForegroundColor = fg;
                Console.WriteLine("|");
            }
        }
    }
}