using Map;

namespace IA;

public class PatrouilleurDiag : NPC
{
    public int Health;
    public bool avance = true;
    
    public PatrouilleurDiag(int health, (double X, double Y) coordinates, Carte board, float speed) : base(health, coordinates, board, speed)
    {
        Symbol = "D";
        Health = health;
    }

    
    public bool CanBlowUp((double X, double Y) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < 1.2 && Math.Abs(Coordinates.Y - coordinates_player.Y) < 1.2)
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
    
    public void BlowUpNPC(NPC npc)
    {
        if (npc.Health - Health > 0)
            npc.Health -= Health;
        else
            npc.Health = 0;
    }
    
    
    public void Update((double X, double Y) coordinates_player, Player _player)
    {
        if (avance)
        {
            if (CanBlowUp(coordinates_player))
            {
                BlowUp(_player);
            }
            
            if ((int)Coordinates.X != Board.GetLength(0) && (int)Coordinates.Y != Board.GetLength(1) && !Board[(int)(Coordinates.X + 0.1*Speed), (int)(Coordinates.Y + 0.1*Speed)].IsColliding((Coordinates.X + 0.1*Speed, Coordinates.Y + 0.1*Speed)))
            {
                Coordinates = (Coordinates.X + 0.1*Speed, Coordinates.Y + 0.1*Speed);
            }
            else
            {
                avance = false;
            }
        }
        else
        {
            
            if (CanBlowUp(coordinates_player))
            {
                BlowUp(_player);
            }
            
            if ((int)Coordinates.X != 0 && (int)Coordinates.Y != 0 && !Board[(int)(Coordinates.X - 0.1*Speed), (int)(Coordinates.Y - 0.1*Speed)].IsColliding((Coordinates.X - 0.1*Speed, Coordinates.Y - 0.1*Speed)))
            {
                Coordinates = (Coordinates.X - 0.1*Speed, Coordinates.Y - 0.1*Speed);
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
            
            if (CanBlowUp(npc.Coordinates))
            {
                BlowUpNPC(npc);
                Console.WriteLine($"Your health is now: {npc.Health}");
            }
            
            if ((int)Coordinates.X != Board.GetLength(0) && (int)Coordinates.Y != Board.GetLength(1) && !Board[(int)(Coordinates.X + 0.1*Speed), (int)(Coordinates.Y + 0.1*Speed)].IsColliding((Coordinates.X + 0.1*Speed, Coordinates.Y + 0.1*Speed)))
            {
                Coordinates = (Coordinates.X + 0.1*Speed, Coordinates.Y + 0.1*Speed);
            }
            else
            {
                avance = false;
            }
        }
        else
        {
            if (CanBlowUp(npc.Coordinates))
            {
                BlowUpNPC(npc);
                Console.WriteLine($"Your health is now: {npc.Health}");
            }
            
            if ((int)Coordinates.X != 0 && (int)Coordinates.Y != 0 && !Board[(int)(Coordinates.X - 0.1*Speed), (int)(Coordinates.Y - 0.1*Speed)].IsColliding((Coordinates.X - 0.1*Speed, Coordinates.Y - 0.1*Speed)))
            {
                Coordinates = (Coordinates.X - 0.1*Speed, Coordinates.Y - 0.1*Speed);
            }
            else
            {
                avance = true;
            }
        }
    }
}