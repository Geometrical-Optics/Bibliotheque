using IA.Item;

namespace IA;

public class Player
{
    public int Health { get; set; }
        public List<Item.Item> Bag { get;}

        public Player()
        {
            Health = 100;
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

        public void ClearInventory()
        {
            Bag.Clear();
        }
        public void PrintBag()
        {
            Bag.ForEach(item => Console.WriteLine(item.ToString()));
        }

        public override string ToString()
        {
            return $"Health: {Health}\n";
        }

        public void PrintStat()
        {
            Console.Write($"Health: {Health}\n");
            PrintBag();
        }
}