using Map;

namespace IA;

public class PatrouilleurX : NPC
{

    public int Health;
    
    public PatrouilleurX(int id, (double X, double Y) coordinates, Box[,] board, int speed) : base(id, coordinates, board, speed)
    {
        Symbol = "X";
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
                if ((int)Coordinates.X != Board.GetLength(0) && !Board[(int)(Coordinates.X + 0.1*Speed), (int)Coordinates.Y].IsColliding((Coordinates.X + 0.1*Speed, Coordinates.Y)))
                {
                    Coordinates = (Coordinates.X + 0.1*Speed, Coordinates.Y);
                }
                else
                {
                    if ((int)Coordinates.X != 0 && !Board[(int)(Coordinates.X - 0.1*Speed), (int)Coordinates.Y].IsColliding((Coordinates.X - 0.1*Speed, Coordinates.Y)))
                    {
                        Coordinates = (Coordinates.X - 0.1*Speed, Coordinates.Y);
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