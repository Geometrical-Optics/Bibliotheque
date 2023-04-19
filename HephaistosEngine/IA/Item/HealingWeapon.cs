namespace IA.Item;

public class HealingWeapon : Item
{
    public int Care { get; private set; }

    public HealingWeapon(string name, int care)
    {
        Name = name;
        Care = care;
    }

    public void UpgradeWeapons()
    {
        Care *= 3;
    }

    public override Upgrade UpgradeMe()
    {
        return UpgradeWeapons;
    }
}