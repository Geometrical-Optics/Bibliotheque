using Map;

namespace IA;


public abstract class NPC 
{
    public string Symbol;
    public int Id;
    public Carte Board;
    public (double X, double Y) Coordinates;
    public float Speed;

    public NPC(int id, (double X, double Y) coordinates, Carte board, float speed)
    {
        
        Id = id;
        Coordinates = coordinates;
        Board = board;
        Speed = speed;
    }
    
    public override string ToString()
    {
        return Symbol;
    }
}