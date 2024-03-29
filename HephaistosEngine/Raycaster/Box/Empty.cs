﻿using SFML.Graphics;

namespace Map;

public class Empty : Box
{
    public (int X, int Y) _Coordinates { get; private set; }
    public (float X, float Y) _TexturePos { get; set; }

    public (double X, double Y)[] _Vertex { get; private set; }
    public float _Height { get; private set; }
    public int _TextureId { get; set; }
    public float _Size { get; private set; }
    public int _FloorId { get; set; }
    public int _CeilingId { get; private set; }
    public int _TopDownId { get; private set; }
    public bool _IsTransparent { get; private set; }
    
    public bool _ContainsEntity { get; set; }
    public float _posZ { get; private set; }
    public int _distance { get; set;  }


    public Empty((int X, int Y) Coordinates, float Height, int floor, int ceil)
    {
        _Coordinates = Coordinates;
        _Height = 0;
        _TextureId = 0;
        _Vertex = new (double X, double Y)[1]{Coordinates};
        _Size = 0;
        _FloorId = floor;
        _CeilingId = ceil;
        _IsTransparent = true;
        _posZ = 0;
        _TopDownId = 0;
        _ContainsEntity = false;
        _TexturePos = (0, 0);
        _distance = 0;
    }

    public bool IsColliding((double X, double Y) Coordinates)
    {
        return false;
    }
    
    public bool Collide((double X, double Y, float Z) Coordinates)
    {
        return false;
    }

    public void Save(string path, BinaryWriter sw)
    {
        sw.Write(0);
        sw.Write(_distance);
        sw.Write(_Coordinates.X);
        sw.Write(_Coordinates.Y);
        sw.Write(_FloorId);
        sw.Write(_CeilingId);
    }

    public static Box Read(string path, BinaryReader sr)
    {
        int x;
        int y;
        int floor;
        int ceil;

        var tempo = sr.ReadInt32();
        x = sr.ReadInt32();
        y = sr.ReadInt32();
        floor = sr.ReadInt32();
        ceil = sr.ReadInt32();

        var tmp = new Empty((x, y), 0, floor, ceil);
        tmp._distance = tempo;

        return tmp;
    }
    
    public uint GetTextureX((double X, double Y) Coordinates, Image Texture)
    {
        return 0;
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