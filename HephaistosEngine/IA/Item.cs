namespace IA.Item;

public delegate void Upgrade();

public abstract class Item
{
    public string Name { get; protected set; }
    
    public abstract Upgrade UpgradeMe();
}