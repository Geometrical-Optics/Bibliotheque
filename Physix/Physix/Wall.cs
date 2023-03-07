namespace Physix;

public class Wall:PhysixObject
{
    public Wall(string name, int imagePath, float x, float y, float z, float mass, float size, float velocityx = 0, float velocityy = 0, float velocityz = 0) : base(name, imagePath, x, y, z, mass, size, velocityx, velocityy, velocityz)
    {
    }
}