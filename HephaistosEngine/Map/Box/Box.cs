namespace Map;

public interface Box
{
    (int X, int Y) _Coordinates { get; }
    (double X, double Y)[] _Vertex { get; }
    float _Height { get; }
    int _TextureId { get; }
    int _FloorId { get; }
    int _CeilingId { get; }
    int _TopDownId { get; }
    float _Size { get; }
    bool _IsTransparent { get; }
    
    bool _ContainsEntity { get; set; }
    float _posZ { get; }

    bool IsColliding((double X, double Y) Coordinates);
    bool Collide((double X, double Y, float Z) Coordinates);

    void Save(string path, BinaryWriter sw);
    //static void Read(string path, BinaryReader sr);
}