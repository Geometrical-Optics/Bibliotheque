using System.Numerics;

namespace Physix;

public class PhysixObject
{
    public string Name;
    public int ImageID;
    public int Id;


    public Vector3 Pos { get; set; }// Player's position
    // -> x =x / y = z / z = y
    // 0 <= z <= 1
    
    
    public Vector3 Velocity { get; set; } // Velocity's movement
    
    
    public float Mass { get; set; } // la Masse x)
    
    public float Size { get; set; } // La taille de l'objet de son centre vers son extremite
    

    public PhysixObject(string name, float x, float y,float z, float mass, float size, float velocityx = 0f, float velocityy = 0f, float velocityz = 0f )
    {
        Name = name;
        //ImageID = imagePath;

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
        bool posx = Pos.X == 0;
        bool posy = Pos.Y == 0;
        bool posz = Pos.Z == 0;
        
        Pos += Velocity * delta;
        if (Pos.Y < 0) Pos = new Vector3(Pos.X,-Pos.Y,Pos.Z);
        if (Pos.Y < 0.1) Pos = new Vector3(Pos.X,0,Pos.Z);

        Pos = new Vector3(posx ? 0 : Pos.X, posy ? 0 : Pos.Y, posz ? 0 : Pos.Z);

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