using ProjetS2.Map;

namespace ProjetS2;

public class PatrouilleurY : NPC
{
    public PatrouilleurY(int id, (double X, double Y) coordinates, Box[,] board) : base(id, coordinates, board)
    {
        Symbol = "Y";
    }

    public void Update()
    {
        if ((int)Coordinates.Y != Board.GetLength(1) && !Board[(int)Coordinates.X, (int)Coordinates.Y + 1].IsColliding((Coordinates.X, Coordinates.Y + 1)))
        {
            Coordinates = (Coordinates.X, Coordinates.Y + 1);
        }
        else
        {
            if ((int)Coordinates.Y != 0 && !Board[(int)Coordinates.X, (int)Coordinates.Y - 1].IsColliding((Coordinates.X, Coordinates.Y - 1)))
            {
                Coordinates = (Coordinates.X, Coordinates.Y - 1);
            }
        }
    }
}