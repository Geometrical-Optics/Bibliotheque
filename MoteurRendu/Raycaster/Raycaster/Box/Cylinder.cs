namespace Raycaster;

public class Full : Box
{
    public (int X, int Y) _Coordinates { get; private set; }
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

    public Full((int X, int Y) Coordinates, float Height, int TextureId, int floor, 
        int ceil, int topid, float pos_Z)
    {
        _Coordinates = Coordinates;
        _Height = Height;
        _TextureId = TextureId;
        _Vertex = new (double X, double Y)[1]{Coordinates};
        _Size = 1;
        _FloorId = floor;
        _CeilingId = ceil;
        _TopDownId = topid;
        _ContainsEntity = false;

        if (Height == 1 && pos_Z == 0)
            _IsTransparent = false;
        else
            _IsTransparent = true;

        _posZ = pos_Z;
    }

    public bool IsColliding((double X, double Y) Coordinates)
    {
        if ((int)Coordinates.X == _Coordinates.X && (int)Coordinates.Y == _Coordinates.Y)
            return true;

        return false;
    } 
    
    public bool Collide((double X, double Y, float Z) Coordinates)
    {
        if (((int)Coordinates.X == _Coordinates.X && (int)Coordinates.Y == _Coordinates.Y)
            && (Coordinates.Z >= _posZ-0.75 && Coordinates.Z < _posZ+_Height ))
            return true;

        return false;
    }
}