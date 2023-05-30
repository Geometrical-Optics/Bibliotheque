using SFML.Graphics;

namespace Map;

public interface Box
{
    (int X, int Y) _Coordinates { get; }
    (float X, float Y) _TexturePos { get; set; }
    (double X, double Y)[] _Vertex { get; }
    float _Height { get; }
    int _TextureId { get; set; }
    int _FloorId { get; set; }
    int _CeilingId { get; }
    int _TopDownId { get; }
    float _Size { get; }
    bool _IsTransparent { get; }
    
    bool _ContainsEntity { get; set; }
    float _posZ { get; }
    int _distance { get; set;  }

    bool IsColliding((double X, double Y) Coordinates);
    bool Collide((double X, double Y, float Z) Coordinates);
    uint GetTextureX((double X, double Y) Coordinates, Image Texture);

    void Save(string path, BinaryWriter sw);

    bool Same(Box tmp);
    //static void Read(string path, BinaryReader sr);
}