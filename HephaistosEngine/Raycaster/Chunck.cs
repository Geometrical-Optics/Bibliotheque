namespace Map;

public class Chunck
{
    public List<Box>[,] _Value { get; private set; }
    public int _Height { get; private set; }
    public int _Width { get; private set; }
    public int _Depth { get; private set; }
    private int _ChunkSize;
    

    public List<Box> this[int x, int y]
    {
        get
        {
            return _Value[x, y];
        }
        set
        {
            _Value[x, y] = value;
        }
    }
    
    public Box this[int x, int y, int z]
    {
        get
        {
            return _Value[x, y][z];
        }
        set
        {
            _Value[x, y][z] = value;
        }
    }

    public Chunck(int ChunkSize, int Depth)
    {
        _Height = ChunkSize;
        _Width = ChunkSize;
        _ChunkSize = ChunkSize;
        _Depth = Depth;

        _Value = new List<Box>[ChunkSize,ChunkSize];
        for (int i = 0; i < ChunkSize; i++)
        {
            for (int j = 0; j < ChunkSize; j++)
            {
                _Value[i, j] = new List<Box>() { new Empty((i, j), 1, 1, 2) };
            }
        }
    }

    public void Save(string path, BinaryWriter sw)
    {

        foreach (var list in _Value)
        {
            sw.Write(list.Count);
            foreach (var box in list)
            {
                box.Save(path, sw);
            }
        }
        
    }

    public void Read(string path, int size, BinaryReader sr, int Depth)
    {
        _ChunkSize = size;
        _Width = size;
        _Height = size;
        _Value = new List<Box>[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int tmpdepth = sr.ReadInt32();
                _Value[i, j] = new List<Box>();
                
                for (int k = 0; k < tmpdepth; k++)
                {

                    int type = sr.ReadInt32();

                    if (type == 1)
                    {
                        _Value[i, j].Add(Full.Read(path, sr));
                    }
                    else if (type == 2)
                    {
                        _Value[i, j].Add(Cylinder.Read(path, sr));
                    }
                    else if (type == 3)
                    {
                        _Value[i, j].Add(Triangle.Read(path, sr));
                    }
                    else if (type == 4)
                    {
                        _Value[i, j].Add(Rect.Read(path, sr));
                    }
                    else
                    {
                        _Value[i, j].Add(Empty.Read(path, sr));
                    }
                }
            }
        }
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < _Value.GetLength(0); i++)
        {
            for (int j = 0; j < _Value.GetLength(1); j++)
            {
                for (int k = 0; k < _Value.GetLength(2); k++)
                {

                    if (_Value[i, j][k] is Empty)
                        result += '-';
                    else
                        result += '#';
                }
            }

            result += '\n';
        }

        return result;
    }
}