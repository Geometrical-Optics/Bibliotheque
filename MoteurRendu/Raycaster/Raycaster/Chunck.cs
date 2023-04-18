namespace Raycaster;

public class Chunck
{
    public Box[,] _Value { get; private set; }
    public int _Height { get; private set; }
    public int _Width { get; private set; }
    private int _ChunkSize;

    public Box this[int x, int y]
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

    public Chunck(int ChunkSize)
    {
        _Height = ChunkSize;
        _Width = ChunkSize;
        _ChunkSize = ChunkSize;

        _Value = new Box[ChunkSize,ChunkSize];
        for (int i = 0; i < ChunkSize; i++)
        {
            for (int j = 0; j < ChunkSize; j++)
            {
                _Value[i,j] = new Empty((i,j), 1, 1, 2);
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
                if (_Value[i, j] is Empty)
                    result += '-';
                else
                    result += '#';
            }

            result += '\n';
        }

        return result;
    }
}