using Map;

namespace IA;


public abstract class NPC 
{
    public string Symbol;
    public int Id;
    public Box[,] Board;
    public (double X, double Y) Coordinates;
    public int Speed;

    public NPC(int id, (double X, double Y) coordinates, Box[,] board, int speed)
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