using IA.Item;

namespace IA;

public class Player
{
    public int Health { get; set; }
        public List<Item.Item> Bag { get;}

        public Player()
        {
            Health = 1000;
            Bag = new List<Item.Item>();
            AssaultWeapon w = new AssaultWeapon("Sword", 1);
            Bag.Add(w);
        }

        public bool IsAlive()
        {
            if (Health <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            return $"Health: {Health}\n";
        }
        
}