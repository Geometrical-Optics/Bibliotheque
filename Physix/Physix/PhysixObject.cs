using System.Numerics;

namespace Physix;

public class PhysixObject
{
    public string Name;
    public int ImageID;
    public int Id;


    public Vector3 Pos { get; set; }// Player's position
    
    
    
    public Vector3 Velocity { get; set; } // Velocity's movement
    
    
    public float Mass { get; set; } // la Masse x)
    
    public float Size { get; set; } // La taille de l'objet de son centre vers son extremite
    

    public PhysixObject(string name,int imagePath, float x, float y,float z, float mass, float size, float velocityx = 0f, float velocityy = 0f, float velocityz = 0f )
    {
        Name = name;
        ImageID = imagePath;

        Pos = new Vector3(x, y, z);
        
        Mass = mass;
        Size = size;

        Velocity = new Vector3(velocityx, velocityy, velocityz);
        
    }
    
    

    public void ApplyForces(Vector3 forces, float delta)
    {

        Vector3 acceleration = forces / Mass;

        Velocity += acceleration * delta;
        
        
        
    }

    public void Update(float delta)
    {
        Pos += Velocity * delta;
        
    }


    public void Collision(PhysixObject andere)
    {
        float totMass = Mass + andere.Mass;

        Vector3 newvel = ((Mass - andere.Mass) * Velocity + 2 * andere.Mass * andere.Velocity) / totMass;
        
        andere.Velocity = ((andere.Mass - Mass) * andere.Velocity + 2 * Mass * Velocity) / totMass;

        Velocity = newvel;
        

    }


    public bool Iscolliding(PhysixObject andere)
    {
        Vector3 distance = andere.Velocity - Velocity;
        
        float radiusSum = Size + andere.Size;

        return distance.LengthSquared() <= radiusSum * radiusSum;
    }
    
    //maybe add Friction, gravityscale, ApplyImpulse
    // la friction serait a appliquer dans ApplyForces
}