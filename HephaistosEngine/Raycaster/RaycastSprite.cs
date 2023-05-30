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
    public int _Identifier { get; set; }

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
        (float X, float Y) TexturePosition, int id)
    {
        X = 1;
        Y = 1;
        Z = 1;
        _Identifier = id;

        _Size = Size;
        _Texture = Texture;
        _TexturePosition = TexturePosition;
        _Angle = 0;
        _TextureList = new[] { Texture, Texture, Texture, Texture, Texture, Texture };
    }
    
    public RaycastSprite((float X, float Y, float Z) Size, 
        int Texture, int id)
    {
        X = 1;
        Y = 1;
        Z = 1;
        _Identifier = id;

        _Size = Size;
        _Texture = Texture;
        _TexturePosition = (0,0);
        _Angle = 0;
        _TextureList = new[] { Texture, Texture, Texture, Texture, Texture, Texture };
    }
    
    public void MoveEntity(Carte Map, (float X, float Y, float Z) Coord, RaycastSprite[] Entities)
    {
        X = Coord.X;
        Y = Coord.Y;
        Z = Coord.Z;
        
        foreach (var entity in Entities)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (entity.X + i >= 0 && entity.Y + j >= 0 && entity.X + i < Map._Width && entity.Y + j < Map._Height)
                        Map[(int)entity.X + i, (int)entity.Y + j, 0]._ContainsEntity = true;
                }
            }
        }
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
            xx = (uint)((((Y + (_Size.Y / 2) - y + ((_Size.Y)*_TexturePosition.X)) / (_Size.Y)) % 1) * (materials[_Texture].Size.X - 1));
        }
        

        return xx;

    }
}