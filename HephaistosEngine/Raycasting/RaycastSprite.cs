using Map;
using SFML.Graphics;

namespace Raycasting;

public class RaycastSprite
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public (float X, float Y, float Z) _Size { get; set; }
    public int _Texture { get; }  
    public int[] _TextureList { get; set; }  
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

    public RaycastSprite((float X, float Y, float Z) Size, 
        int Texture,
        (float X, float Y) TexturePosition)
    {
        X = 0;
        Y = 0;
        Z = 0;

        _Size = Size;
        _Texture = Texture;
        _TexturePosition = TexturePosition;
        _Angle = 0;
        _TextureList = new[] { Texture, Texture, Texture, Texture, Texture, Texture };
    }
    
    public RaycastSprite((float X, float Y, float Z) Size, 
        int Texture)
    {
        X = 0;
        Y = 0;
        Z = 0;

        _Size = Size;
        _Texture = Texture;
        _TexturePosition = (0,0);
        _Angle = 0;
        _TextureList = new[] { Texture, Texture, Texture, Texture, Texture, Texture };
    }
    
    public void MoveEntity(Carte Map, (float X, float Y, float Z) Coord, RaycastSprite[] Entities)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (X + i >= 0 && X + i < Map._Width 
                                      && Y + j >= 0 && Y + j < Map._Height)
                    foreach (var value in Map[(int)(X + i), (int)(Y + j)])
                    {
                        if (Entities.Any(x => (int)(x.X)-(int)(X+i) <= -1 || (int)(x.X)-(int)(X+i) >= 1
                            && (int)(x.Y)-(int)(Y+i) <= -1 || (int)(x.Y)-(int)(Y+i) >= 1))
                        value._ContainsEntity = false;
                    }
            }
        }
        
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (Coord.X + i >= 0 && Coord.X + i < Map._Width
                                     && Coord.Y + j >= 0 && Coord.Y + j < Map._Height)
                    foreach (var value in Map[(int)(Coord.X + i), (int)(Coord.Y + j)])
                    {
                        value._ContainsEntity = true;
                    }
            }
        }

        X = Coord.X;
        Y = Coord.Y;
        Z = Coord.Z;
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
    
    public int GetTexture(double x, double y)
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
    
    public uint GetTextureX(double x, double y, Image[] materials)
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
        
        uint xx = (uint)((((X + (_Size.X/2) - x + ((_Size.X)*_TexturePosition.X)) / _Size.X)%1) * (materials[_Texture].Size.X - 1));
        
        if (((X + (_Size.X/2) - x)%1)< 0.02
            || ((X + (_Size.X/2) - x)%1) > _Size.X-0.02)
        {
            xx = (uint)((((Y + (_Size.Y / 2) - y + ((_Size.Y)*_TexturePosition.Y)) / (_Size.Y)) % 1) * (materials[_Texture].Size.X - 1));
        }
        

        return xx;

    }
}