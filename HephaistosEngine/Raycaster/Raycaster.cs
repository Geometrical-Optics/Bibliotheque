using System.Security.Cryptography;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Raycaster;

public class Raycaster
{
    private uint _Width;
    private uint _Height;
    private (int X, int Y) _Position;
    private float _Size;
    public float lum = 0.75f;

    public Raycaster((uint Width, uint Height) Scale, (int X, int Y) Position, float Size)
    {
        _Width = Scale.Width;
        _Height = Scale.Height;

        _Position = Position;
        _Size = Size;
    }

    public static double Dist((double x, double y) Pos1, (double x, double y) Pos2)
    {
        return Math.Sqrt((Pos1.x - Pos2.x)*(Pos1.x-Pos2.x) + (Pos1.y-Pos2.y)*(Pos1.y-Pos2.y));
    }
    
    public double Modulo(double a, double b)
    {
        return a - (int)((int)a / b) * b;
    }

    public double abs(double a)
    {
        if (a < 0)
            return -a;
        return a;
    }

    private void DrawWall(Carte Map, (double X, double Y, double Z) Position, (double X, double Y) Coord, 
        int halfres, int Cube_Height, Image[] Textures, byte[] Image, double cos, double cos2,int i, int j)
    {
        var tmp = Coord;
        double x = tmp.X;
        double y = tmp.Y;
        if (Map[(int)x, (int)y, j] is Empty)
            return;

        double n = Double.Abs((x - Position.X) / cos);
        double h = (halfres / ((n * cos2) + 0.000001)); // - ((h * 2) * Map[(int)x, (int)y]._posZ * _Size)
        Image texture = Textures[Map[(int)x, (int)y, j]._TextureId];
        var val = (byte)(Math.Min((lum + ((h / halfres) * (1-lum))) * 255, 255));

        var shade = new Color(val, val,val);

        uint xx = (uint)((x % 1) * (texture.Size.X - 1));

        if (x % 1 < 0.02 || x % 1 > 0.98)
        {
            xx = (uint)((y % 1) * (texture.Size.X - 1));
        }

        if (Map[(int)x, (int)y, j] is Rect)
            xx = (uint)(((Rect)Map[(int)x, (int)y, j]).GetTextureX(x, y)*texture.Size.X);
        else if (Map[(int)x, (int)y, j] is Cylinder)
            xx = (uint)((x % 1) * (texture.Size.X - 1));

        double yy = (1 / ((h * 2 + 0.01))) * texture.Size.Y;
        double counter = 0;
        int size = (int)(halfres + h
                         - ((h * 2) * Map[(int)x, (int)y, j]._posZ * _Size)
                         - ((h * 2) * Map[(int)x, (int)y, j]._Height * _Size)
                         + ((h * 2) * Position.Z * _Size));
        
        int Start = (int)(halfres + h
                          - (int)((h * 2) * Map[(int)x, (int)y, j]._posZ * _Size)
                          + (int)((h * 2) * Position.Z * _Size));
        
        for (int w = Start;
             w >= size
             && w >= 0;
             w--)
        {
            
            // g = w + (int)((h * 2) * Position.Z * _Size);
            if ( w < _Height && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
            {
                Color color = texture.GetPixel(xx, (uint)(texture.Size.Y - counter - 1))*shade;

                Image[(((w * _Width * 4) + i * 4))] = color.R;
                Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
            }
            else if (w >= _Height)
            {
                counter += yy*(w-_Height-1);
                counter %= texture.Size.Y;
                
                w = (int)_Height-1;
            }


            counter += yy;
            counter = counter % texture.Size.Y;
        }
    }
    
    private void DrawSprite(Carte Map, (double X, double Y, double Z) Position, (double X, double Y) Coord, 
        int halfres, int Cube_Height, Image[] Textures, byte[] Image, double cos, double cos2,int i,
        RaycastSprite entity,
        double dist)
    {
        var tmp = Coord;
        double x = tmp.X;
        double y = tmp.Y;

        double n = Double.Abs((x - Position.X) / cos);
        double h = (halfres / ((n * cos2) + 0.000001)); // - ((h * 2) * Map[(int)x, (int)y]._posZ * _Size)
        Image texture = entity.GetTexture( x, y);
        if (texture != null)
        {

            var val = (byte)(Math.Min((lum + ((h / halfres) * (1-lum))) * 255, 255));
            var shade = new Color(val, val,val);
            uint xx = entity.GetTextureX(x, y);

            double yy = (1 / ((h * 2 + 0.01))) * texture.Size.Y;
            double counter = 0;
            int size = (int)(halfres + h
                             - ((h * 2) * entity.Z * _Size)
                             - ((h * 2) * entity._Size.Z * _Size)
                             + ((h * 2) * Position.Z * _Size));

            int Start = (int)(halfres + h
                              - (int)((h * 2) * entity.Z * _Size)
                              + (int)((h * 2) * Position.Z * _Size));

            for (int w = Start - 1;
                 w >= size
                 && w >= 0;
                 w--)
            {
                if (w < _Height && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                {
                    Color color = texture.GetPixel(xx, (uint)(texture.Size.Y - counter - 1))*shade;

                    if (color.A != 0)
                    {
                        Image[(((w * _Width * 4) + i * 4))] = color.R;
                        Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                        Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                        Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                    }
                }
                else if (w >= _Height)
                {
                    counter += yy * (w - _Height - 1);
                    counter %= texture.Size.Y;

                    w = (int)_Height - 1;
                }


                counter += yy;
                counter = counter % texture.Size.Y;
            }
        }
    }

    private void DrawColumn(Carte Map, (double X, double Y, double Z) Position, int Cube_Height, double Angle, 
        Image[] Textures, byte[] Image, int i, int halfres, double mod, double FOV,
        RaycastSprite[] Entities, int step, (int stepnumb, int mult)[] scale, int renderdist)
    {
        double tempo = (Math.PI * (((i / mod) - (FOV/2))/180)); // double cos = Math.Cos(rot_i);
        double rot_i = Angle + tempo;// + Math.Atan2(_Width/2 - (i - 0.5), dist); //+ tempo;

        double sin = Math.Sin(rot_i);
        double cos = Math.Cos(rot_i);
        double cos2 = Math.Cos(tempo);

        double x = Position.X;
        double y = Position.Y;

        double cos3 = cos * 0.00024414062;
        double sin2 = sin * 0.00024414062;

        double distance = 0;
        
        Stack<(double x, double y)> drawlist = new Stack<(double x, double y)>();
        Stack<(double x, double y)> spritelist = new Stack<(double x, double y)>();

        while ( distance < renderdist && x >= 0 && x < Map.GetLength(0)
                && y >= 0 && y < Map.GetLength(1)
                && (Map[(int)x, (int)y].IsColliding((x, y)) == false ||
                    Map[(int)x, (int)y]._IsTransparent))
        {

            if (distance % step == 0)
            {
                foreach (var etape in scale)
                {
                    if (distance >= etape.stepnumb)
                    {
                        cos3 *= etape.mult;
                        sin2 *= etape.mult;
                    }
                }
            }

            bool drawed = false;
            
            if (drawlist.Count == 0
                && Map[(int)x, (int)y].IsColliding((x, y))
                && Map[(int)x, (int)y].IsColliding((Position.X, Position.Y)) == false
                && (Map[(int)x, (int)y]._IsTransparent || Position.Z > 0))
            {
                drawlist.Push((x, y));
                for (int j = 0; j < Map._Depth; j++)
                {
                    DrawWall(Map, Position, (x, y),
                        halfres, Cube_Height, Textures, Image, cos, cos2, i, j);
                    drawed = true;
                }
            }
            else if (Map[(int)x, (int)y].IsColliding((x, y))
                     && Map[(int)x, (int)y].IsColliding((Position.X, Position.Y)) == false
                     && ((int)drawlist.Peek().x != (int)x
                         || (int)drawlist.Peek().y != (int)y)
                     && (Map[(int)x, (int)y]._IsTransparent || Position.Z > 0))
            {
                drawlist.Push((x, y));
                for (int j = 0; j < Map._Depth; j++)
                {
                    DrawWall(Map, Position, (x, y),
                        halfres, Cube_Height, Textures, Image, cos, cos2, i, j);
                }
            }

            else if (spritelist.Count != Entities.Length) 
            {

                foreach (var entity in Entities)
                {
                    if (spritelist.Contains((entity.X, entity.Y)) == false)
                    {
                        var dist = 0;
                        if (entity.GetCollision(x,y)) 
                        {
                            DrawSprite(Map, Position, (x, y),
                                halfres, Cube_Height, Textures, Image, cos, cos2, i, entity, dist);
                            
                            spritelist.Push((entity.X, entity.Y));

                        }
                    }
                }
                
            }

            for (int j = 0; j < Map._Depth; j++)
            {
                if (Map[(int)x, (int)y,j].IsColliding((x, y)))
                {
                    if (Map[(int)x, (int)y, j] is Empty)
                        continue;

                    double n = Double.Abs((x - Position.X) / cos);
                    double h2 = (halfres / ((n * cos2) + 0.000001));
                    Image texture = Textures[Map[(int)x, (int)y, j]._TopDownId];
                    //Console.Write(Map[(int)x, (int)y,j]._TopDownId);
                    var val = (byte)(Math.Min((lum + ((h2 / halfres) * (1-lum))) * 255, 255));

                    var shade = new Color(val, val,val);

                    uint xx = (uint)((x % 1) * (texture.Size.X - 1));
                    uint yy = (uint)((y % 1) * (texture.Size.Y - 1));
                    int w = (int)(halfres
                                  + h2
                                  - ((h2 * 2) * Map[(int)x, (int)y, j]._Height * _Size)
                                  - ((h2 * 2) * Map[(int)x, (int)y, j]._posZ * _Size)
                                  + ((h2 * 2) * Position.Z * _Size));

                    if (Map[(int)x, (int)y, j]._posZ > Position.Z)
                        w = (int)(halfres
                                  + h2
                                  - (h2 * 2 * Map[(int)x, (int)y, j]._posZ * _Size)
                                  + (h2 * 2 * Position.Z * _Size));

                    if (w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                    {
                        Color color = texture.GetPixel(xx, (uint)yy)*shade;

                        Image[(((w * _Width * 4) + i * 4))] = color.R;
                        Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                        Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                        Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                    }
                }
            }

            if (Map[(int)x, (int)y]._FloorId != 0)
            {
                double n = Double.Abs((x - Position.X) / cos);
                double h2 = (halfres / ((n * cos2) + 0.000001));
                Image texture = Textures[Map[(int)x, (int)y]._FloorId];

                uint xx = (uint)((x % 1) * (texture.Size.X - 1));
                uint yy = (uint)((y % 1) * (texture.Size.Y - 1));
                int w = (int)(halfres + h2 + ((h2 * 2)*Position.Z*_Size));
                var val = (byte)(Math.Min((lum + ((h2 / halfres) * (1-lum))) * 255, 255));

                var shade = new Color(val, val,val);
                
                if ( w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                {
                    Color color = texture.GetPixel(xx, (uint)yy)*shade;
                        
                    Image[(((w * _Width * 4) + i * 4))] = color.R;
                    Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                    Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                    Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                }
                if (Map[(int)x, (int)y]._CeilingId != 0)
                {
                    texture = Textures[Map[(int)x, (int)y]._CeilingId];
                    w = (int)(halfres
                              + h2
                              - ((h2 * 2) * _Size)
                              + ((h2 * 2)*Position.Z*_Size)
                        );
                    
                    if ( w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                    {
                        Color color = texture.GetPixel(xx, (uint)yy)*shade;
                        
                        Image[(((w * _Width * 4) + i * 4))] = color.R;
                        Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                        Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                        Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                    }
                }

            }
            else if (Map[(int)x, (int)y]._CeilingId != 0 && Map[(int)x, (int)y]._Height != 1)
            {
                double n = Double.Abs((x - Position.X) / cos);
                double h2 = (halfres / ((n * cos2) + 0.000001));
                Image texture = Textures[Map[(int)x, (int)y]._CeilingId];
                var val = (byte)(Math.Min((lum + ((h2 / halfres) * (1-lum))) * 255, 255));

                var shade = new Color(val, val,val);

                uint xx = (uint)((x % 1) * (texture.Size.X - 1));
                uint yy = (uint)((y % 1) * (texture.Size.Y - 1));
                int w = (int)(halfres
                              + h2
                              - ((h2 * 2) * _Size)
                              + ((h2 * 2)*Position.Z*_Size)
                    );
                
                if ( w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                {
                    Color color = texture.GetPixel(xx, (uint)yy)*shade;
                        
                    Image[(((w * _Width * 4) + i * 4))] = color.R;
                    Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                    Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                    Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                }
            }

            x = x + cos3;
            y = y + sin2;
            distance += 1;
        }
            

        if (x >= 0 && x < Map.GetLength(0)
                   && y >= 0 && y < Map.GetLength(1)
                   && Map[(int)x, (int)y].IsColliding((x, y)))
        {
            for (int j = 0; j < Map._Depth; j++)
            {
                DrawWall(Map, Position, (x, y),
                    halfres, Cube_Height, Textures, Image, cos, cos2, i,j);
            }
        }
    }
    public void Draw(Carte Map, (double X, double Y, double Z) Position, int Cube_Height, double rot, 
        Image[] Textures, byte[] Image, 
        RaycastSprite[] Entities, 
        RenderWindow _window, int step, (int stepnumb, int mult)[] scale, int renderdist)
    {
        
        double FOV = 60;
        int halfres = ((int)_Height) / 2;
        double mod = _Height / FOV;
        double FOVSTEP = FOV / (_Width); //  {(3072,64),(2560,32),(0,2)}

        Parallel.For(0, _Width, i =>
        {
            DrawColumn(Map, Position, Cube_Height, rot,
                Textures, Image, (int)i, halfres, mod, FOV, Entities, step, scale, renderdist);
        });
        
        Image image2 = new Image((uint)_Width, (uint)_Height, Image);

        Texture texture = new Texture(image2);
        Sprite sprite = new Sprite(texture);
        sprite.Draw(_window,RenderStates.Default);
        
    }
    
    
}