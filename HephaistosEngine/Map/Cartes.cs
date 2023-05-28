using System.Text;
using Raycasting;

namespace Map;

public class Carte
{
    public Chunck[,] _Carte { get; private set; }
    public int _Height { get; private set; }
    public int _Width { get; private set; }
    public int _Depth { get; private set; }
    private int _ChunkSize;
    

    public List<Box> this[int x, int y]
    {
        get
        {
            return _Carte[x / _ChunkSize, y / _ChunkSize][x % _ChunkSize, y % _ChunkSize];
        }
        set
        {
            _Carte[(int)(x / _ChunkSize), (int)(y / _ChunkSize)][x % _ChunkSize, y % _ChunkSize] = value;
        }
    }
    
    public Box this[int x, int y, int z]
    {
        get
        {
            return _Carte[x / _ChunkSize, y / _ChunkSize][x % _ChunkSize, y % _ChunkSize, z];
        }
        set
        {
            _Carte[(int)(x / _ChunkSize), (int)(y / _ChunkSize)][x % _ChunkSize, y % _ChunkSize, z] = value;
        }
    }

    public Carte((int Width, int Height) Size, int ChunkSize)
    {
        _Height = Size.Height;
        _Width = Size.Width;
        _Depth = 1;
        _ChunkSize = ChunkSize;

        _Carte = new Chunck[_Width / _ChunkSize, _Height / _ChunkSize];
        for (int i = 0; i < (int)(_Width / _ChunkSize); i++)
        {
            for (int j = 0; j < (int)(_Height / _ChunkSize); j++)
            {
                _Carte[i, j] = new Chunck(_ChunkSize,_Depth);
            }
        }

        for (int i = 0; i < _Width; i++)
        {
            this[i, _Height - 1,0] = new Full((i, _Height - 1), 1, 0, 0, 0, 0, 0);
            this[i, 0,0] = new Full((i, 0), 1, 0, 0, 0, 0, 0);
        }

        for (int i = 0; i < _Height; i++)
        {
            this[_Width-1, i,0] = new Full((_Width-1,i), 1, 0, 0, 0, 0, 0);
            this[0, i,0] = new Full((0, i), 1, 0, 0, 0, 0, 0);
        }
    }

    public Box[] GetCollision(double x, double y)
    {
        List<Box> result = new List<Box>();
        foreach (var wall in this[(int)x,(int)y])
        {
            if (wall.IsColliding((x, y)))
            {
                result.Add(wall);
            }
        }

        return result.ToArray();
    }

    public int GetLength(int i)
    {
        if (i == 0)
            return _Width;
        
        return _Height;
    }

    public void Save(string path)
    {
        using (BinaryWriter sw = new BinaryWriter(File.Open(path, FileMode.Create), Encoding.UTF8, false))
            {
                sw.Write(_ChunkSize);
                sw.Write(_Width);
                sw.Write(_Height);
                sw.Write(_Depth);

                foreach (var chunk in _Carte)
                {
                    chunk.Save(path, sw);
                }
            }

    }

    public void Load(string path)
    {

        if (File.Exists(path))
        {
            using (var stream = File.Open(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {

                    _ChunkSize = reader.ReadInt32();
                    _Width = reader.ReadInt32();
                    _Height = reader.ReadInt32();
                    _Depth = reader.ReadInt32();

                    _Carte = new Chunck[_Width / _ChunkSize, _Height / _ChunkSize];
                    for (int i = 0; i < (int)(_Width / _ChunkSize); i++)
                    {
                        for (int j = 0; j < (int)(_Height / _ChunkSize); j++)
                        {
                            _Carte[i, j] = new Chunck(_ChunkSize, _Depth);
                            _Carte[i,j].Read(path,_ChunkSize,reader, _Depth);
                        }
                    }
                }
            }
        }

    }

    public void MoveEntity(RaycastSprite Entity, (float X, float Y) Coord)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (Entity.X + i >= 0 && Entity.X + i < _Width 
                                      && Entity.Y + j >= 0 && Entity.Y + j < _Height)
                    foreach (var value in this[(int)(Entity.X + i), (int)(Entity.Y + j)])
                    {
                        value._ContainsEntity = false;
                    }
            }
        }
        
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (Coord.X + i >= 0 && Coord.X + i < _Width
                                     && Coord.Y + j >= 0 && Coord.Y + j < _Height)
                    foreach (var value in this[(int)(Coord.X + i), (int)(Coord.Y + j)])
                    {
                        value._ContainsEntity = true;
                    }
            }
        }

        Entity.X = Coord.X;
        Entity.Y = Coord.Y;
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                if (this[i, j] is Empty)
                    result += '-';
                else
                    result += '#';
            }

            result += '\n';
        }

        return result;
    }
}