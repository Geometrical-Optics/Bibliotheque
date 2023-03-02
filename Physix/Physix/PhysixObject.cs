using System.Numerics;

namespace Physix;

public class PhysixObject
{
    public string Name;

    public int Id;
    
    public float X { get; set; } // position X
    public float Y { get; set; } // position Y
    public float Z { get; set; } // position Z

    public float VelocityX { get; set; } // pour tout mouvement horizontal
    public float VelocityY { get; set; } // pour tout mouvement vertical

    public float VelocityZ { get; set; }

    public float Mass { get; set; } // la Masse x)
    
    public float Size { get; set; } // La taille de l'objet de son centre vers son extremite
    
    // Taux d'elasticite si besoin (impossible a determiner sauf grace au createur qui devra tt faire et ca va changer les formules) #useless avec le raycasting

    public PhysixObject(string name, float x, float y,float z, float mass, float size, float velocityx = 0f, float velocityy = 0f, float velocityz = 0f )
    {
        Name = name;
        X = x;
        Y = y;
        Z = z;
        Mass = mass;
        Size = size;
        VelocityX = velocityx;
        VelocityY = velocityy;
        VelocityZ = velocityz;
    }
    
    

    public void ApplyForces(Vector3 forces, float delta)
    {

        Vector3 acceleration = forces / Mass;
        
        VelocityX += acceleration.X * delta;
        VelocityY += acceleration.Y * delta;
        VelocityZ += acceleration.Z * delta;
        
        
    }

    public void Update(float delta)
    {
        X += VelocityX * delta;
        Y += VelocityY * delta;
        Z = VelocityZ * delta;
    }


    public void Collision(PhysixObject andere)
    {
        float totMass = Mass + andere.Mass;
        
        
        
        float newVelocityX = ((Mass - andere.Mass) * VelocityX + 2*andere.Mass*andere.VelocityX)/totMass;
        float newVelocityY = ((Mass - andere.Mass) * VelocityY + 2*andere.Mass*andere.VelocityY)/totMass;
        float newVelocityZ = ((Mass - andere.Mass) * VelocityZ + 2*andere.Mass*andere.VelocityZ)/totMass;
        
        andere.VelocityX = ( (andere.Mass - Mass) * andere.VelocityX + 2 * Mass * VelocityX) / totMass;
        andere.VelocityY = ( (andere.Mass - Mass) * andere.VelocityY + 2 * Mass * VelocityY) / totMass;
        andere.VelocityZ = ( (andere.Mass - Mass) * andere.VelocityZ + 2 * Mass * VelocityZ) / totMass;

        VelocityX = newVelocityX;
        VelocityY = newVelocityY;
        VelocityZ = newVelocityZ;


    }


    public bool Iscolliding(PhysixObject andere)
    {
        Vector3 distance = new Vector3(andere.X - X, andere.Y - Y, andere.Z - Z);

        float radiusSum = Size + andere.Size;

        return distance.LengthSquared() <= radiusSum * radiusSum;
    }
    
    //maybe add Friction, gravityscale, ApplyImpulse
    // la friction serait a appliquer dans ApplyForces
}