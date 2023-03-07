namespace ProjetS2.Map;

public class Empty : Box
{
    public (int X, int Y) _Coordinates { get; private set; }
    public (int X, int Y)[] _Vertex { get; private set; }
    public int _Height { get; private set; }
    public int _TextureId { get; private set; }
    public int _Size { get; private set; }
    public int _FloorId { get; private set; }
    public int _CeilingId { get; private set; }
    public bool _IsTransparent { get; private set; }

    public Empty((int X, int Y) Coordinates, int Height, int floor, int ceil)
    {
        _Coordinates = Coordinates;
        _Height = Height;
        _TextureId = 0;
        _Vertex = new (int X, int Y)[1]{Coordinates};
        _Size = 0;
        _FloorId = floor;
        _CeilingId = ceil;
        _IsTransparent = true;
    }

    public bool IsColliding((double X, double Y) Coordinates)
    {
        return false;
    }
    
}