namespace ProjetS2.Map;

public class Full : Box
{
    public (int X, int Y) _Coordinates { get; private set; }
    public (int X, int Y)[] _Vertex { get; private set; }
    public int _Height { get; private set; }
    public int _TextureId { get; private set; }
    public int _Size { get; private set; }
    public int _FloorId { get; private set; }
    public int _CeilingId { get; private set; }
    public bool _IsTransparent { get; private set; }

    public Full((int X, int Y) Coordinates, int Height, int TextureId, int floor, int ceil, int max_height)
    {
        _Coordinates = Coordinates;
        _Height = Height;
        _TextureId = TextureId;
        _Vertex = new (int X, int Y)[1]{Coordinates};
        _Size = 1;
        _FloorId = floor;
        _CeilingId = ceil;

        if (Height == max_height)
            _IsTransparent = false;
        else
            _IsTransparent = true;
    }

    public bool IsColliding((double X, double Y) Coordinates)
    {
        if ((int)Coordinates.X == _Coordinates.X && (int)Coordinates.Y == _Coordinates.Y)
            return true;

        return false;
    } 
    
}
