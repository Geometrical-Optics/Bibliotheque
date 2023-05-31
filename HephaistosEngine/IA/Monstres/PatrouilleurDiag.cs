using Map;

namespace IA;

public class PatrouilleurDiag : NPC
{
    public int Health;
    public bool avance = true;
    
    public PatrouilleurDiag(int health, (double X, double Y, double Z) coordinates, Carte board, float speed) : base(health, coordinates, board, speed)
    {
        Symbol = "D";
        Health = health;
    }

    
    public bool CanBlowUp((double X, double Y, double Z) coordinates_player)
    {
        if (Math.Abs(Coordinates.X - coordinates_player.X) < 1.2 && Math.Abs(Coordinates.Y - coordinates_player.Y) < 1.2)
            return true;
        else
            return false;
    }
    
    public void BlowUp(Player _player)
    {
        if (_player.Health - Health > 0)
        {
            _player.Health -= Health;
            Health = 0;
        }
        else
        {
            _player.Health = 0;
            Health = 0;
        }
        
    }
    
    public void BlowUpNPC(NPC npc)
    {
        if (npc.Health - Health > 0)
            npc.Health -= Health;
        else
            npc.Health = 0;
    }
    
    
    public void Update((double X, double Y, double Z) coordinates_player, Player _player)
    {
        if (avance)
        {
            if (CanBlowUp(coordinates_player))
            {
                BlowUp(_player);
            }
            
            
            if (Board.IsColliding(( (Coordinates.X + 0.1*Speed), Coordinates.Y + 0.1*Speed, (float)Coordinates.Z)) == false)
            {
                Coordinates = (Coordinates.X + 0.1*Speed, Coordinates.Y + 0.1*Speed, Coordinates.Z);
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
            
            
            if (Board.IsColliding(( (Coordinates.X - 0.1*Speed), Coordinates.Y - 0.1*Speed, (float)Coordinates.Z)) == false)
            {
                Coordinates = (Coordinates.X - 0.1*Speed, Coordinates.Y - 0.1*Speed, Coordinates.Z);
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
            
            if (Board.IsColliding(( (Coordinates.X + 0.1*Speed), Coordinates.Y + 0.1*Speed, (float)Coordinates.Z)) == false)
            {
                Coordinates = (Coordinates.X + 0.1*Speed, Coordinates.Y + 0.1*Speed, Coordinates.Z);
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
            
            if (Board.IsColliding(( (Coordinates.X - 0.1*Speed), Coordinates.Y - 0.1*Speed, (float)Coordinates.Z)) == false)
            {
                Coordinates = (Coordinates.X - 0.1*Speed, Coordinates.Y - 0.1*Speed, Coordinates.Z);
            }
            else
            {
                avance = true;
            }
        }
    }
}