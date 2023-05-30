
using System;
using System.Collections.Generic;
using Map;

namespace IA;

public delegate void Pattern();

public delegate void Upgrade();

public class IAManager
{
    public Carte Board;
    public List<NPC> _Monstres;
    public Player _player;

    public IAManager(Carte board)
    {
        Board = board;
        _Monstres = new List<NPC>();
        _player = new Player();
    }

    public void Add(NPC monstre)
    {
        monstre.Id = _Monstres.Count;
        _Monstres.Add(monstre);
    }

    public void Remove(int id) => _Monstres.RemoveAll(x => x.Id == id);

    public void RemoveAll(List<NPC> monstres)
    {
        monstres.Clear();
    }

    public void UpdateAll((double X, double Y) coordinates_player)
    {
        foreach (NPC npc in _Monstres)
        {
            switch (npc)
            {
                case AgressifAstar n:
                    n.Update(coordinates_player, _player);
                    break;
                case Fugitif n:
                    n.Update(coordinates_player, _player);
                    break;
                case PatrouilleurX n:
                    n.Update(coordinates_player, _player);
                    break;
                case PatrouilleurY n:
                    n.Update(coordinates_player, _player);
                    break;
                case PatrouilleurDiag n:
                    n.Update(coordinates_player, _player);
                    break;
            }
        }
    }
    
    public void UpdateAllNPC()
    {
        
        for (int i = 0; i < _Monstres.Count; i++)
        {
            if (i % 2 == 0 || i == 0)
            {
                switch (_Monstres[i])
                {
                    case AgressifAstar n:
                        n.UpdateNPC(_Monstres[i + 1]);
                        break;
                    case Fugitif n:
                        n.UpdateNPC(_Monstres[i + 1]);
                        break;
                    case PatrouilleurX n:
                        n.UpdateNPC(_Monstres[i + 1]);
                        break;
                    case PatrouilleurY n:
                        n.UpdateNPC(_Monstres[i + 1]);
                        break;
                    case PatrouilleurDiag n:
                        n.UpdateNPC(_Monstres[i + 1]);
                        break;
                }
            }
            else
            {
                switch (_Monstres[i])
                {
                    case AgressifAstar n:
                        n.UpdateNPC(_Monstres[i - 1]);
                        break;
                    case Fugitif n:
                        n.UpdateNPC(_Monstres[i - 1]);
                        break;
                    case PatrouilleurX n:
                        n.UpdateNPC(_Monstres[i - 1]);
                        break;
                    case PatrouilleurY n:
                        n.UpdateNPC(_Monstres[i - 1]);
                        break;
                    case PatrouilleurDiag n:
                        n.UpdateNPC(_Monstres[i - 1]);
                        break;
                }
            }
        }
        /*
        if (_Monstres.Count > 1)
        {
            switch (_Monstres[0])
            {
                case AgressifAstar n:
                    n.UpdateNPC(_Monstres[1]);
                    break;
                case Fugitif n:
                    n.UpdateNPC(_Monstres[1]);
                    break;
                case PatrouilleurX n:
                    n.UpdateNPC(_Monstres[1]);
                    break;
                case PatrouilleurY n:
                    n.UpdateNPC(_Monstres[1]);
                    break;
                case PatrouilleurDiag n:
                    n.UpdateNPC(_Monstres[1]);
                    break;
            }

            switch (_Monstres[1])
            {
                case AgressifAstar n:
                    n.UpdateNPC(_Monstres[0]);
                    break;
                case Fugitif n:
                    n.UpdateNPC(_Monstres[0]);
                    break;
                case PatrouilleurX n:
                    n.UpdateNPC(_Monstres[0]);
                    break;
                case PatrouilleurY n:
                    n.UpdateNPC(_Monstres[0]);
                    break;
                case PatrouilleurDiag n:
                    n.UpdateNPC(_Monstres[0]);
                    break;
            }
        }
        */
    }
}
