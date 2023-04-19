namespace IA.Item;

public class AssaultWeapon : Item
{
    public int Damage { get; private set; }

    public AssaultWeapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }

    public void UpgradeWeapons()
    {
        Damage *= 3;
    }

    public override Upgrade UpgradeMe()
    {
        return UpgradeWeapons;
    }
}