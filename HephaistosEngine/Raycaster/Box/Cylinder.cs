using System.Net.Mime;
using SFML.Graphics;

namespace Map;

public class Cylinder : Box
{
    public (int X, int Y) _Coordinates { get; private set; }
    public (float X, float Y) _TexturePos { get; set; }

    public (double X, double Y)[] _Vertex { get; private set; }
    public float _Height { get; private set; }
    public int _TextureId { get; set; }
    public float _Size { get; private set; }
    public float _Size2 { get; private set; }
    public int _FloorId { get; set; }
    public int _CeilingId { get; private set; }
    
    public int _TopDownId { get; private set; }

    public bool _IsTransparent { get; private set; }
    public bool _ContainsEntity { get; set; }
    public float _posZ { get; private set; }
    public int _distance { get; set;  }

    public Cylinder((int X, int Y) Coordinates, float Height, int TextureId, int floor, 
        int ceil, int topid, float pos_Z, float size)
    {
        _Coordinates = Coordinates;
        _Height = Height;
        _TextureId = TextureId;
        _Vertex = new (double X, double Y)[1]{Coordinates};
        _Size = size*size;
        _Size2 = size;
        _FloorId = floor;
        _CeilingId = ceil;
        _TopDownId = topid;
        _TexturePos = (0, 0);
        _distance = 0;

        if (Height == 1 && pos_Z == 0)
            _IsTransparent = false;
        else
            _IsTransparent = true;

        _posZ = pos_Z;
        _ContainsEntity = false;
    }

    public bool IsColliding((double X, double Y) Coordinates)
    {
        if (((Coordinates.X-_Coordinates.X-0.5)*(Coordinates.X-_Coordinates.X-0.5) 
                      + (Coordinates.Y-_Coordinates.Y-0.5)*(Coordinates.Y-_Coordinates.Y-0.5)) < _Size)
            return true;

        return false;
    } 
    
    public bool Collide((double X, double Y, float Z) Coordinates)
    {
        if (IsColliding((Coordinates.X,Coordinates.Y))
                        && Coordinates.Z >= _posZ
                        && Coordinates.Z <= _posZ+_Height)
            return true;

        return false;
    }
    
    public void Save(string path, BinaryWriter sw)
    {
        sw.Write(2);
        sw.Write(_distance);
        sw.Write(_Coordinates.X);
        sw.Write(_Coordinates.Y);
        sw.Write((double)_posZ);
        
        sw.Write((double)_TexturePos.X);
        sw.Write((double)_TexturePos.Y);
        
        sw.Write(_TextureId);
        sw.Write(_FloorId);
        sw.Write(_CeilingId);
        sw.Write(_TopDownId);
        sw.Write((double)_Height);
        sw.Write((double)_Size);
    }
    
    public static Box Read(string path, BinaryReader sr)
    {
        int x;
        int y;
        double z;
        
        double x1;
        double y1;
        
        int text;
        int floor;
        int ceil;
        int top;
        double height;
        double size;

        var tmp2 = sr.ReadInt32();
        
        x = sr.ReadInt32();
        y = sr.ReadInt32();
        z = sr.ReadDouble();
        
        x1 = sr.ReadDouble();
        y1 = sr.ReadDouble();
        
        text = sr.ReadInt32();
        floor = sr.ReadInt32();
        ceil = sr.ReadInt32();
        top = sr.ReadInt32();
        height = sr.ReadDouble();
        size = sr.ReadDouble();

        var tmp = new Cylinder((x, y), (float)height, text, floor, ceil, top, (float)z, (float)Math.Sqrt(size));

        tmp._TexturePos = ((float)x1, (float)y1);
        tmp._distance = tmp2;

        return tmp;
    }
    
    public uint GetTextureX((double X, double Y) Coordinates, Image Texture)
    {
        uint xx = (uint)(((Coordinates.X+_TexturePos.X) % 1) * (Texture.Size.X - 1));

        if ((Coordinates.X % 1) < _Size2/2 || (Coordinates.X % 1)+_Size2/2 > 1)
        {
            xx = (uint)(((Coordinates.Y+_TexturePos.X) % 1) * (Texture.Size.X - 1));
        }

        return xx;
    }

    public bool Same(Box tmp)
    {
        if (tmp._Coordinates == _Coordinates
            && tmp._Height == _Height
            && tmp._posZ == _posZ
            && tmp._Vertex == _Vertex)
            return true;

        return false;
    }

    public override bool Equals(object obj)
    {
        if (obj is Box)
            return Same((Box)(obj));
        return false;
    }
}