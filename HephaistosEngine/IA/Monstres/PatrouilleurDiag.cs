using Map;

namespace IA;

public class PatrouilleurDiag : NPC
{
    public int Health;
    
    public PatrouilleurDiag(int id, (double X, double Y) coordinates, Carte board, float speed) : base(id, coordinates, board, speed)
    {
        Symbol = "D";
        Health = 30;
    }

    
    public bool CanBlowUp((double X, double Y) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < 3 || Math.Abs(Coordinates.Y - coordinates_player.Y) < 3)
            return true;
        else
            return false;
    }
    
    public void BlowUp(Player _player)
    {
        if (_player.Health - Health > 0)
            _player.Health -= Health;
        else
            _player.Health = 0;
    }
    
    
    public void Update((double X, double Y) coordinates_player, Player _player)
    {
        if (_player.IsAlive())
        {
            if (CanBlowUp(coordinates_player))
            {
                BlowUp(_player);
                Console.WriteLine($"Your health is now: {_player.Health}");
            }
            else
            {
                if ((int)Coordinates.X != Board.GetLength(0) && (int)Coordinates.Y != Board.GetLength(1) && !Board[(int)(Coordinates.X + 0.1*Speed), (int)(Coordinates.Y + 0.1*Speed)].IsColliding((Coordinates.X + 0.1*Speed, Coordinates.Y + 0.1*Speed)))
                {
                    Coordinates = (Coordinates.X + 0.1*Speed, Coordinates.Y + 0.1*Speed);
                }
                else
                {
                    if ((int)Coordinates.X != 0 && (int)Coordinates.Y != 0 && !Board[(int)(Coordinates.X - 0.1*Speed), (int)(Coordinates.Y - 0.1*Speed)].IsColliding((Coordinates.X - 0.1*Speed, Coordinates.Y - 0.1*Speed)))
                    {
                        Coordinates = (Coordinates.X - 0.1*Speed, Coordinates.Y - 0.1*Speed);
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