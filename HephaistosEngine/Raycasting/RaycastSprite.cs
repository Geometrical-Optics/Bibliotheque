using SFML.Graphics;

namespace Raycasting;

public class RaycastSprite
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public (float X, float Y, float Z) _Size { get; set; }
    public Image _Texture { get; }  
    public Image[] _TextureList { get; set; }  
    public (float X, float Y) _TexturePosition { get; set; }

    public double _Angle { get; set; } 

    public (float X, float Y, float Z) _Position
    {
        get { return (X, Y, Z); }
        set {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }
    }

    public RaycastSprite((float X, float Y, float Z) Position, (float X, float Y, float Z) Size, 
        Image Texture,
        (float X, float Y) TexturePosition)
    {
        X = Position.X;
        Y = Position.Y;
        Z = Position.Z;

        _Size = Size;
        _Texture = Texture;
        _TexturePosition = TexturePosition;
        _Angle = 0;
        _TextureList = new[] { Texture, Texture, Texture, Texture };
    }

    public bool GetCollision(double x, double y)
    {
        if (_Angle == 0)
        {
            if (x > X - (_Size.X / 2)
                && x < X + (_Size.X / 2)
                && y > Y - (_Size.Y / 2)
                && y < Y + (_Size.Y / 2))
                return true;
            return false;
        }
        else
        {
            var dx = x - X;
            var dy = y - Y;
            var angle = Math.Atan2(dy,dx)-_Angle;
            var dist = Raycaster.Dist((0, 0), (dx, dy));

            dx = Math.Cos(angle) * dist;
            dy = Math.Sin(angle) * dist;
            
            if (dx > - (_Size.X / 2)
                && dx < (_Size.X / 2)
                && dy > - (_Size.Y / 2)
                && dy < (_Size.Y / 2))
                return true;
            return false;
        }
    }
    
    public Image GetTexture(double x, double y)
    {
        if (_Angle != 0)
        {
            
            var dx = x - X;
            var dy = y - Y;
            var angle = Math.Atan2(dy,dx)-_Angle;
            var dist = Raycaster.Dist((0, 0), (dx, dy));

            x = X + Math.Cos(angle) * dist;
            y = Y + Math.Sin(angle) * dist;
            
        }
        
        if (((X + (_Size.X/2) - x)%1)< 0.02)
        {
            return _TextureList[0];
        }
        else if (((X + (_Size.X/2) - x)%1) > _Size.X-0.02)
        {
            return _TextureList[2];
        }
        else if (((Y + (_Size.Y/2) - y)%1) > _Size.Y-0.02)
        {
            return _TextureList[1];

        }

        return _TextureList[3];


    }

    public void FaceTo(double x, double y)
    {
        var dx = x - X;
        var dy = y - Y;
        var angle = Math.Atan2(dy,dx);

        _Angle = angle;
    }
    
    public uint GetTextureX(double x, double y)
    {
        if (_Angle != 0)
        {
            
            var dx = x - X;
            var dy = y - Y;
            var angle = Math.Atan2(dy,dx)-_Angle;
            var dist = Raycaster.Dist((0, 0), (dx, dy));

            /*if (dx < 0 && dy < 0)
                angle -= Math.PI;
            else if (dx < 0)
                angle += Math.PI;*/

            x = X + Math.Cos(angle) * dist;
            y = Y + Math.Sin(angle) * dist;
            
        }
        
        uint xx = (uint)((((X + (_Size.X/2) - x + ((_Size.X)*_TexturePosition.X)) / _Size.X)%1) * (_Texture.Size.X - 1));
        
        if (((X + (_Size.X/2) - x)%1)< 0.02
            || ((X + (_Size.X/2) - x)%1) > _Size.X-0.02)
        {
            xx = (uint)((((Y + (_Size.Y / 2) - y + ((_Size.Y)*_TexturePosition.Y)) / (_Size.Y)) % 1) * (_Texture.Size.X - 1));
        }
        

        return xx;

    }
}