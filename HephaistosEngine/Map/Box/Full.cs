﻿using System.Net.Mime;

namespace Map;

public class Cylinder : Box
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

    public Cylinder((int X, int Y) Coordinates, float Height, int TextureId, int floor, 
        int ceil, int topid, float pos_Z, float size)
    {
        _Coordinates = Coordinates;
        _Height = Height;
        _TextureId = TextureId;
        _Vertex = new (double X, double Y)[1]{Coordinates};
        _Size = size;
        _FloorId = floor;
        _CeilingId = ceil;
        _TopDownId = topid;

        if (Height == 1 && pos_Z == 0)
            _IsTransparent = false;
        else
            _IsTransparent = true;

        _posZ = pos_Z;
        _ContainsEntity = false;
    }

    public bool IsColliding((double X, double Y) Coordinates)
    {
        if (Math.Sqrt((Coordinates.X-_Coordinates.X-0.5)*(Coordinates.X-_Coordinates.X-0.5) 
                      + (Coordinates.Y-_Coordinates.Y-0.5)*(Coordinates.Y-_Coordinates.Y-0.5)) < _Size)
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
    
    public void Save(string path, BinaryWriter sw)
    {
        sw.Write(2);
        sw.Write(_Coordinates.X);
        sw.Write(_Coordinates.Y);
        sw.Write((double)_posZ);
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
        int text;
        int floor;
        int ceil;
        int top;
        double height;
        double size;
        
        x = sr.ReadInt32();
        y = sr.ReadInt32();
        z = sr.ReadDouble();
        text = sr.ReadInt32();
        floor = sr.ReadInt32();
        ceil = sr.ReadInt32();
        top = sr.ReadInt32();
        height = sr.ReadDouble();
        size = sr.ReadDouble();

        return new Cylinder((x, y), (float)height, text, floor, ceil, top, (float)z, (float)size);
    }
}