using ProjetS2.Map;

namespace ProjetS2;

public class PatrouilleurDiag : NPC
{
    public PatrouilleurDiag(int id, (double X, double Y) coordinates, Box[,] board) : base(id, coordinates, board)
    {
        Symbol = "D";
    }

    public void Update()
    {
        if ((int)Coordinates.X != Board.GetLength(0) && (int)Coordinates.Y != Board.GetLength(1) && !Board[(int)Coordinates.X + 1, (int)Coordinates.Y + 1].IsColliding((Coordinates.X + 1, Coordinates.Y + 1)))
        {
            Coordinates = (Coordinates.X + 1, Coordinates.Y + 1);
        }
        else
        {
            if ((int)Coordinates.X != 0 && (int)Coordinates.Y != 0 && !Board[(int)Coordinates.X - 1, (int)Coordinates.Y - 1].IsColliding((Coordinates.X -1, Coordinates.Y - 1)))
            {
                Coordinates = (Coordinates.X - 1, Coordinates.Y - 1);
            }
        }
    }
}