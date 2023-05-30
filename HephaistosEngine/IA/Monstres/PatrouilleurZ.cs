using Map;

namespace IA;

public class PatrouilleurY : NPC
{
    public int Health;
    public bool avance = true;
    public PatrouilleurY(int health, (double X, double Y) coordinates, Carte board, float speed) : base(health, coordinates, board, speed)
    {
        Symbol = "Y";
        Health = health;
    }

    
    public bool CanAttack((double X, double Y) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < 1)
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
            
            if ((int)Coordinates.Y != Board.GetLength(1) && !Board[(int)Coordinates.X, (int)(Coordinates.Y + 0.1*Speed)]
                    .IsColliding((Coordinates.X, Coordinates.Y + 0.1*Speed)))
            {
                Coordinates = (Coordinates.X, Coordinates.Y + 0.1*Speed);
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
            
            if ((int)Coordinates.Y != 0 && !Board[(int)Coordinates.X, (int)(Coordinates.Y - 0.1*Speed)]
                    .IsColliding((Coordinates.X, Coordinates.Y - 0.1*Speed)))
            {
                Coordinates = (Coordinates.X, Coordinates.Y - 0.1*Speed);
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
            
            if ((int)Coordinates.Y != Board.GetLength(1) && !Board[(int)Coordinates.X, (int)(Coordinates.Y + 0.1*Speed)]
                    .IsColliding((Coordinates.X, Coordinates.Y + 0.1*Speed)))
            {
                Coordinates = (Coordinates.X, Coordinates.Y + 0.1*Speed);
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
            
            if ((int)Coordinates.Y != 0 && !Board[(int)Coordinates.X, (int)(Coordinates.Y - 0.1*Speed)]
                    .IsColliding((Coordinates.X, Coordinates.Y - 0.1*Speed)))
            {
                Coordinates = (Coordinates.X, Coordinates.Y - 0.1*Speed);
            }
            else
            {
                avance = true;
            }
        }
    }
}