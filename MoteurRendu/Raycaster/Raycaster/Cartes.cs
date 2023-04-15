namespace Raycaster;

public class Carte
{
    public Chunck[,] _Carte { get; private set; }
    public int _Height { get; private set; }
    public int _Width { get; private set; }
    private int _ChunkSize;

    public Box this[int x, int y]
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

    public Carte((int Width, int Height) Size, int ChunkSize)
    {
        _Height = Size.Height;
        _Width = Size.Width;
        _ChunkSize = ChunkSize;

        _Carte = new Chunck[_Width / _ChunkSize, _Height / _ChunkSize];
        for (int i = 0; i < (int)(_Width / _ChunkSize); i++)
        {
            for (int j = 0; j < (int)(_Height / _ChunkSize); j++)
            {
                _Carte[i, j] = new Chunck(_ChunkSize);
            }
        }

        for (int i = 0; i < _Width; i++)
        {
            this[i, _Height - 1] = new Full((i, _Height - 1), 1, 0, 1, 2, 0, 0);
            this[i, 0] = new Full((i, 0), 1, 0, 1, 2, 0, 0);
        }

        for (int i = 0; i < _Height; i++)
        {
            this[_Width-1, i] = new Full((_Width-1,i), 1, 0, 1, 2, 0, 0);
            this[0, i] = new Full((0, i), 1, 0, 1, 2, 0, 0);
        }
    }

    public int GetLength(int i)
    {
        if (i == 0)
            return _Width;
        
        return _Height;
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