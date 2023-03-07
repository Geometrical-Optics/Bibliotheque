namespace ProjetS2.Map;

    public interface Box
    {
        (int X, int Y) _Coordinates { get; }
        
        (int X, int Y)[] _Vertex { get; }
        
        int _Height { get; }
        
        int _TextureId { get; }
        
        int _FloorId { get; }
        
        int _CeilingId { get; }
        
        int _Size { get; }
        
        bool _IsTransparent { get; }

        bool IsColliding((double X, double Y) Coordinates); 
        
    }
