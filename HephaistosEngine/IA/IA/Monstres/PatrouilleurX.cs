using Map;

namespace IA;

public class PatrouilleurX : NPC
{

    public PatrouilleurX(int id, (double X, double Y) coordinates, Box[,] board) : base(id, coordinates, board)
    {
        Symbol = "X";
    }

    public void Update()
    {
        if ((int)Coordinates.X != Board.GetLength(0) && !Board[(int)Coordinates.X + 1, (int)Coordinates.Y].IsColliding((Coordinates.X + 1, Coordinates.Y)))
        {
            Coordinates = (Coordinates.X + 1, Coordinates.Y);
        }
        else
        {
            if ((int)Coordinates.X != 0 && !Board[(int)Coordinates.X - 1, (int)Coordinates.Y].IsColliding((Coordinates.X -1, Coordinates.Y)))
            {
                Coordinates = (Coordinates.X - 1, Coordinates.Y);
            }
        }
    }
}