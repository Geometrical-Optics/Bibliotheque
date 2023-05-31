using Map;

namespace IA;


public abstract class NPC 
{
    public string Symbol;
    public int Id;
    public Carte Board;
    public (double X, double Y, double Z) Coordinates;
    public float Speed;
    public int Health;

    public NPC(int health, (double X, double Y, double Z) coordinates, Carte board, float speed)
    {
        Health = health;
        Coordinates = coordinates;
        Board = board;
        Speed = speed;
    }
    
    public override string ToString()
    {
        return Symbol;
    }
}