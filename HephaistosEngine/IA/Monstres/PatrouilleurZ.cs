using Map;

namespace IA;

public class PatrouilleurY : NPC
{
    public int Health;
    
    public PatrouilleurY(int id, (double X, double Y) coordinates, Carte board, float speed) : base(id, coordinates, board, speed)
    {
        Symbol = "Y";
        Health = 30;
    }

    
    public bool CanAttack((double X, double Y) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < 1 || Math.Abs(Coordinates.Y - coordinates_player.Y) < 1)
            return true;
        else
            return false;
    }
    
    public void Update((double X, double Y) coordinates_player, Player _player)
    {
        if (_player.IsAlive())
        {
            if (CanAttack(coordinates_player))
            {
                _player.Health -= 10;
                Console.WriteLine($"Your health is now: {_player.Health}");
            }
            else
            {
                if ((int)Coordinates.Y != Board.GetLength(1) && !Board[(int)Coordinates.X, (int)(Coordinates.Y + 0.1*Speed)]
                        .IsColliding((Coordinates.X, Coordinates.Y + 0.1*Speed)))
                {
                    Coordinates = (Coordinates.X, Coordinates.Y + 0.1*Speed);
                }
                else
                {
                    if ((int)Coordinates.Y != 0 && !Board[(int)Coordinates.X, (int)(Coordinates.Y - 0.1*Speed)]
                            .IsColliding((Coordinates.X, Coordinates.Y - 0.1*Speed)))
                    {
                        Coordinates = (Coordinates.X, Coordinates.Y - 0.1*Speed);
                    }
                }
            }
            if (_player.IsAlive() == false)
            {
                Console.WriteLine("You lost !");
            }
        }
    }
}