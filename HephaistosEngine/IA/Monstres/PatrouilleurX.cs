using Map;

namespace IA;

public class PatrouilleurX : NPC
{

    public int Health;
    public bool avance = true;
    
    public PatrouilleurX(int health, (double X, double Y) coordinates,  Carte board, float speed) : base(health, coordinates, board, speed)
    {
        Symbol = "X";
        Health = health;
    }

    public bool CanAttack((double X, double Y) coordinates_player)
    {
        if (Math.Abs(Coordinates.Y - coordinates_player.Y) < 1.2)
            return true;
        else
            return false;
    }
    
    public void Update((double X, double Y) coordinates_player, Player _player)
    {
        if (avance)
        {
            
            if (CanAttack(coordinates_player))
            {
                _player.Health -= 10;
            }
            
            if ((int)Coordinates.X != Board.GetLength(0) &&
                !Board[(int)(Coordinates.X + 0.1 * Speed), (int)Coordinates.Y]
                    .IsColliding((Coordinates.X + 0.1 * Speed, Coordinates.Y)))
            {
                Coordinates = (Coordinates.X + 0.1 * Speed, Coordinates.Y);
            }
            else
            {
                avance = false;
            }
        }
        else
        {
            if (CanAttack(coordinates_player))
            {
                _player.Health -= 10;
            }
            
            if ((int)Coordinates.X != Board.GetLength(0) &&
                !Board[(int)(Coordinates.X - 0.1 * Speed), (int)Coordinates.Y]
                    .IsColliding((Coordinates.X - 0.1 * Speed, Coordinates.Y)))
            {
                Coordinates = (Coordinates.X - 0.1 * Speed, Coordinates.Y);
            }
            else
            {
                avance = true;
            }
        }
    }
    
    public void UpdateNPC(NPC npc)
    {
        if (avance)
        {
            
            if (CanAttack(npc.Coordinates))
            {
                npc.Health -= 10;
            }
            
            if ((int)Coordinates.X != Board.GetLength(0) &&
                !Board[(int)(Coordinates.X + 0.1 * Speed), (int)Coordinates.Y]
                    .IsColliding((Coordinates.X + 0.1 * Speed, Coordinates.Y)))
            {
                Coordinates = (Coordinates.X + 0.1 * Speed, Coordinates.Y);
            }
            else
            {
                avance = false;
            }
        }
        else
        {
            if (CanAttack(npc.Coordinates))
            {
                npc.Health -= 10;
            }
            
            if ((int)Coordinates.X != Board.GetLength(0) &&
                !Board[(int)(Coordinates.X - 0.1 * Speed), (int)Coordinates.Y]
                    .IsColliding((Coordinates.X - 0.1 * Speed, Coordinates.Y)))
            {
                Coordinates = (Coordinates.X - 0.1 * Speed, Coordinates.Y);
            }
            else
            {
                avance = true;
            }
        }
    }
}