namespace Raycaster;

public class Empty : Box
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

    public Empty((int X, int Y) Coordinates, float Height, int floor, int ceil)
    {
        _Coordinates = Coordinates;
        _Height = Height;
        _TextureId = 0;
        _Vertex = new (double X, double Y)[1]{Coordinates};
        _Size = 0;
        _FloorId = floor;
        _CeilingId = ceil;
        _IsTransparent = true;
        _posZ = 0;
        _TopDownId = 0;
        _ContainsEntity = false;
    }

    public bool IsColliding((double X, double Y) Coordinates)
    {
        return false;
    }
    
    public bool Collide((double X, double Y, float Z) Coordinates)
    {
        return false;
    }
}