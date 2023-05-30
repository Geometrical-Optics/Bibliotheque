using System.Text;
using Raycasting;
using SFML.Graphics;

namespace Map;

public class CarteSimplifie
{
    public List<Box>[,] _Carte { get; private set; }
    public int _Height { get; private set; }
    public int _Width { get; private set; }
    public int _Depth { get; private set; }
    public int _ZSize { get; private set; }

    
    public List<Box> this[int x, int y]
    {
        get
        {
            return _Carte[x,y];
        }
        set
        {
            _Carte[x,y] = value;
        }
    }
    
    public Box this[int x, int y, int z]
    {
        get
        {
            return _Carte[x,y][z];
        }
        set
        {
            _Carte[x,y][z] = value;
        }
    }

    public CarteSimplifie((int Width, int Height, int Depth) Size)
    {
        _Height = Size.Height;
        _Width = Size.Width;
        _Depth = 1;
        _ZSize = Size.Depth;
        

        _Carte = new List<Box>[_Width,_Height];

        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                _Carte[i, j] = new List<Box>() { new Empty((i, j), 1, 1, 2) };
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
                sw.Write(_Width);
                sw.Write(_Height);
                sw.Write(_Depth);

                foreach (var chunk in _Carte)
                {
                    for (int i = 0; i < (int)(_Width); i++)
                    {
                        for (int j = 0; j < (int)(_Height); j++)
                        {
                            sw.Write(this[i,j].Count);
                            foreach (var VARIABLE in this[i,j])
                            {
                                VARIABLE.Save(path,sw);
                            }
                        }
                    }
                }
            }
    }
    
    public void SuperSave(string path, string name, Image[] materials)
    {
        string way = path + name + '/';
        
        if (!Directory.Exists(way))
        {
            Directory.CreateDirectory(way);
        }
        
        Save(way+'/'+name+".hep");
    
        for (int i = 0; i < materials.Length; i++)
        {
            string tmp = "img_";
            int w = i;
            while (w >= 10)
            {
                tmp += "9";
                w /= 10;
            }
            tmp += w;
            
            
            materials[i].SaveToFile(way + tmp + ".png");
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

                    _Width = reader.ReadInt32();
                    _Height = reader.ReadInt32();
                    _Depth = reader.ReadInt32();

                    _Carte = new List<Box>[_Width,_Height];
                    for (int i = 0; i < (int)(_Width); i++)
                    {
                        for (int j = 0; j < (int)(_Height); j++)
                        {
                            _Carte[i, j] = new List<Box>();
                            int tmpdepth = reader.ReadInt32();

                            for (int k = 0; k < tmpdepth; k++)
                            {
                                int type = reader.ReadInt32();

                                if (type == 1)
                                {
                                    _Carte[i, j].Add(Full.Read(path, reader));
                                }
                                else if (type == 2)
                                {
                                    _Carte[i, j].Add(Cylinder.Read(path, reader));
                                }
                                else if (type == 3)
                                {
                                    _Carte[i, j].Add(Triangle.Read(path, reader));
                                }
                                else if (type == 4)
                                {
                                    _Carte[i, j].Add(Rect.Read(path, reader));
                                }
                                else
                                {
                                    _Carte[i, j].Add(Empty.Read(path, reader));
                                }
                            }
                        }
                    }
                }
            }
        }

    }
    
    public Image[] SuperLoad(string path, string name)
    {
        Load(path+name+'/'+name+".hep");
        string[] filePaths = Directory.GetFiles(path+name+'/',"*.png");
        filePaths = filePaths.OrderBy(x => x).ToArray();

        Image[] result = new Image[filePaths.Length];
        for (int i = 0; i < filePaths.Length; i++)
        {
            result[i] = new Texture(filePaths[i]).CopyToImage();
        }

        return result;
    }

    public Carte ToNormalMap(int ChunkSize)
    {
        Carte result = new Carte((_Width, _Height, _ZSize), ChunkSize);

        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                result[i, j] = this[i, j];
            }
        }

        return result;
    }
    
    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                if (this[i, j,0] is Empty)
                    result += '-';
                else
                    result += '#';
            }

            result += '\n';
        }

        return result;
    }
}