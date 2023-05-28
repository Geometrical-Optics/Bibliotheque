using SFML.Graphics;

namespace Map;

public class Rect : Box
{
    public (int X, int Y) _Coordinates { get; private set; }
    public (float X, float Y) _TexturePos { get; set; }

    public (double X, double Y)[] _Vertex { get; private set; }
    public float _Height { get; private set; }
    public int _TextureId { get; private set; }
    public float _Size { get; private set; }
    public int _FloorId { get; private set; }
    public int _CeilingId { get; private set; }
    
    public int _TopDownId { get; private set; }

    public bool _IsTransparent { get; private set; }
    public bool _ContainsEntity { get; set; }
    public float _posZ { get; private set; }

    public Rect((int X, int Y) Coordinates, float Height, int TextureId, int floor, 
        int ceil, int topid, float pos_Z, (double X, double Y)[] vertex)
    {
        _Coordinates = Coordinates;
        _Height = Height;
        _TextureId = TextureId;
        _Size = 1;
        _FloorId = floor;
        _CeilingId = ceil;
        _TopDownId = topid;
        _TexturePos = (0, 0);

        if (Height == 1 && pos_Z == 0)
            _IsTransparent = false;
        else
            _IsTransparent = true;

        _posZ = pos_Z;
        _Vertex = vertex;
        _ContainsEntity = false;
    }
    
    public double dist(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    }

    public bool IsColliding((double X, double Y) Coordinates)
    {
        if (Coordinates.X < _Coordinates.X+0.5f+_Vertex[1].X
             && Coordinates.X > _Coordinates.X+0.5f-_Vertex[0].X
             && Coordinates.Y < _Coordinates.Y+0.5f+_Vertex[1].Y
             && Coordinates.Y > _Coordinates.Y+0.5f-_Vertex[0].Y)
            return true;
        return false;
    } 
    
    public double GetTextureX(double x, double y)
    {
        int X = _Coordinates.X;
        int Y = _Coordinates.Y;
        
        double xx = ((x-X+ _TexturePos.X)%1);

        if (((x - X)%1) < _Coordinates.X+0.5f-_Vertex[0].X+0.02 
            || (((x - X)%1) > _Coordinates.X+0.5f+_Vertex[1].X-0.02))
        {
            xx = ((y - Y + _TexturePos.Y) % 1);
        }

        return xx;

    }
    
    public bool Collide((double X, double Y, float Z) Coordinates)
    {
        if (((int)Coordinates.X == _Coordinates.X && (int)Coordinates.Y == _Coordinates.Y)
            && (Coordinates.Z >= _posZ-0.75 && Coordinates.Z < _posZ+_Height ))
            return true;

        return false;
    }
    
    public void Save(string path, BinaryWriter sw)
    {
        sw.Write(4);
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
        
        sw.Write(_Vertex[0].X);
        sw.Write(_Vertex[0].Y);

        sw.Write(_Vertex[1].X);
        sw.Write(_Vertex[1].Y);

    }
    
    public static Box Read(string path, BinaryReader sr)
    {
        int x;
        int y;
        double z;

        double xt;
        double yt;
        
        int text;
        int floor;
        int ceil;
        int top;
        double height;
        
        double x1;
        double x2;
        double y1;
        double y2;
        
        x = sr.ReadInt32();
        y = sr.ReadInt32();
        z = sr.ReadDouble();
        
        xt = sr.ReadDouble();
        yt = sr.ReadDouble();
        
        text = sr.ReadInt32();
        floor = sr.ReadInt32();
        ceil = sr.ReadInt32();
        top = sr.ReadInt32();
        height = sr.ReadDouble();
        x1 = sr.ReadDouble();
        y1 = sr.ReadDouble();
        
        x2 = sr.ReadDouble();
        y2 = sr.ReadDouble();

        var tmp = new Rect((x,y),(float)height,text,floor,ceil,top,(float)z,new []{(x1,y1),(x2,y2)});
        tmp._TexturePos = ((float)xt, (float)yt);
        return tmp;
    }
    
    public uint GetTextureX((double X, double Y) Coordinates, Image Texture)
    {
        int X = _Coordinates.X;
        int Y = _Coordinates.Y;
        
        double xx = ((Coordinates.X-X)%1);

        if (((Coordinates.X - X)%1) < _Coordinates.X+0.5f-_Vertex[0].X+0.02 
            || (((Coordinates.X - X)%1) > _Coordinates.X+0.5f+_Vertex[1].X-0.02))
        {
            xx = ((Coordinates.Y - Y) % 1);
        }

        return (uint)(xx*Texture.Size.X);
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