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

    public Raycaster((uint Width, uint Height) Scale, (int X, int Y) Position, float Size)
    {
        _Width = Scale.Width;
        _Height = Scale.Height;

        _Position = Position;
        _Size = Size;
    }

    public double Dist((double x, double y) Pos1, (double x, double y) Pos2)
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
        int halfres, int Cube_Height, Image[] Textures, byte[] Image, double cos, double cos2,int i)
    {
        var tmp = Coord;
        double x = tmp.X;
        double y = tmp.Y;

        double n = Double.Abs((x - Position.X) / cos);
        double h = (halfres / ((n * cos2) + 0.000001)); // - ((h * 2) * Map[(int)x, (int)y]._posZ * _Size)
        Image texture = Textures[Map[(int)x, (int)y]._TextureId];

        uint xx = (uint)((x % 1) * (texture.Size.X - 1));

        if (x % 1 < 0.02 || x % 1 > 0.98)
        {
            xx = (uint)((y % 1) * (texture.Size.Y - 1));
        }

        double yy = (1 / ((h * 2 + 0.01))) * texture.Size.Y;
        double counter = 0;
        int size = (int)(halfres + h
                         - ((h * 2) * Map[(int)x, (int)y]._posZ * _Size)
                         - ((h * 2) * Map[(int)x, (int)y]._Height * _Size)
                         + ((h * 2) * Position.Z * _Size));
        
        int Start = (int)(halfres + h
                          - (int)((h * 2) * Map[(int)x, (int)y]._posZ * _Size)
                          + (int)((h * 2) * Position.Z * _Size));
        
        for (int w = Start-1;
             w >= size
             && w >= 0;
             w--)
        {
            
            // g = w + (int)((h * 2) * Position.Z * _Size);
            if ( w < _Height && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
            {
                Color color = texture.GetPixel(xx, (uint)(texture.Size.Y - counter - 1));

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
        Image texture = entity._Texture;

        uint xx = (uint)(((entity.X + (entity._Size.X/2) - x + entity._TexturePosition.X)%1)* (texture.Size.X - 1));

        if (((entity.X + (entity._Size.X/2) - x + entity._TexturePosition.X)%1)< 0.02 
            || ((entity.X + (entity._Size.X/2) - x + entity._TexturePosition.X)%1) > 0.98)
        {
            xx = (uint)(((entity.Y + (entity._Size.Y/2) - y + entity._TexturePosition.Y)%1) * (texture.Size.Y - 1));
        }

        double yy = (1 / ((h * 2 + 0.01))) * texture.Size.Y;
        double counter = 0;
        int size = (int)(halfres + h
                         - ((h * 2) * entity.Z * _Size)
                         - ((h * 2) * entity._Size.Z * _Size)
                         + ((h * 2) * Position.Z * _Size));
        
        int Start = (int)(halfres + h
                          - (int)((h * 2) * entity.Z * _Size)
                          + (int)((h * 2) * Position.Z * _Size));
        
        for (int w = Start-1;
             w >= size
             && w >= 0;
             w--)
        {
            if ( w < _Height && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
            {
                Color color = texture.GetPixel(xx, (uint)(texture.Size.Y - counter - 1));

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
                counter += yy*(w-_Height-1);
                counter %= texture.Size.Y;
                
                w = (int)_Height-1;
            }


            counter += yy;
            counter = counter % texture.Size.Y;
        }
    }

    private void DrawColumn(Carte Map, (double X, double Y, double Z) Position, int Cube_Height, double Angle, 
        Image[] Textures, byte[] Image, int i, int halfres, double mod, double FOV,
        RaycastSprite[] Entities)
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

        while ( distance < 4096 && x >= 0 && x < Map.GetLength(0)
                && y >= 0 && y < Map.GetLength(1)
                && (Map[(int)x, (int)y].IsColliding((x, y)) == false ||
                    Map[(int)x, (int)y]._IsTransparent))
        {

            if (distance % 256 == 0)
            {
                
                if (distance >= 3072)
                {
                    cos3 *= 64;
                    sin2 *= 64;
                }
                else if (distance > 2560)
                {
                    cos3 *= 32;
                    sin2 *= 32;
                }
                
                else
                {
                    cos3 *= 2;
                    sin2 *= 2;
                }
            }

            if (Map[(int)x, (int)y].IsColliding((x, y))
                && Map[(int)x, (int)y].IsColliding((Position.X, Position.Y)) == false
                && drawlist.Count == 0
                && (Map[(int)x, (int)y]._IsTransparent || Position.Z > 0))
            {
                drawlist.Push((x, y));
                DrawWall( Map, Position, (x,y),
                    halfres, Cube_Height, Textures, Image, cos, cos2,i);
            }
            else if (Map[(int)x, (int)y].IsColliding((x, y))
                     && Map[(int)x, (int)y].IsColliding((Position.X, Position.Y)) == false
                     && ((int)drawlist.Peek().x != (int)x
                         || (int)drawlist.Peek().y != (int)y)
                     && (Map[(int)x, (int)y]._IsTransparent || Position.Z > 0))
            {
                drawlist.Push((x, y));
                DrawWall(Map, Position, (x, y),
                    halfres, Cube_Height, Textures, Image, cos, cos2, i);
            }
            else if (spritelist.Count != Entities.Length) //if (Map[(int)x, (int)y]._ContainsEntity)
            {

                foreach (var entity in Entities)
                {
                    if (spritelist.Contains((entity.X, entity.Y)) == false)
                    {
                        var dist = 0;
                        if (x > entity.X - (entity._Size.X / 2) 
                            && x < entity.X + (entity._Size.X/2)
                            && y > entity.Y - (entity._Size.Y / 2) 
                            && y < entity.Y + (entity._Size.Y/2)) 
                        {
                            DrawSprite(Map, Position, (x, y),
                                halfres, Cube_Height, Textures, Image, cos, cos2, i, entity, dist);
                            
                            spritelist.Push((entity.X, entity.Y));

                        }
                    }
                }
                
            }

            if (Map[(int)x, (int)y].IsColliding((x, y)))
            {
                double n = Double.Abs((x - Position.X) / cos);
                double h2 = (halfres / ((n * cos2) + 0.000001));
                Image texture = Textures[Map[(int)x, (int)y]._TopDownId];

                uint xx = (uint)((x % 1) * (texture.Size.X - 1));
                uint yy = (uint)((y % 1) * (texture.Size.Y - 1));
                int w = (int)(halfres 
                              + h2 
                              - ((h2 * 2)* Map[(int)x, (int)y]._Height*_Size)
                              - ((h2 * 2)*Map[(int)x, (int)y]._posZ*_Size) 
                              + ((h2 * 2)*Position.Z*_Size));
                    
                if (Map[(int)x, (int)y]._posZ > Position.Z)
                    w = (int)(halfres 
                              + h2 
                              - (h2 * 2 * Map[(int)x, (int)y]._posZ * _Size)
                              + (h2 * 2 * Position.Z * _Size));
                    
                if (w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                {
                    Color color = texture.GetPixel(xx, (uint)yy);
                        
                    Image[(((w * _Width * 4) + i * 4))] = color.R;
                    Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                    Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                    Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
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
                
                if ( w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                {
                    Color color = texture.GetPixel(xx, (uint)yy);
                        
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
                        Color color = texture.GetPixel(xx, (uint)yy);
                        
                        Image[(((w * _Width * 4) + i * 4))] = color.R;
                        Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                        Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                        Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                    }
                }

            }
            else if (Map[(int)x, (int)y]._CeilingId != 0)
            {
                double n = Double.Abs((x - Position.X) / cos);
                double h2 = (halfres / ((n * cos2) + 0.000001));
                Image texture = Textures[Map[(int)x, (int)y]._CeilingId];

                uint xx = (uint)((x % 1) * (texture.Size.X - 1));
                uint yy = (uint)((y % 1) * (texture.Size.Y - 1));
                int w = (int)(halfres
                              + h2
                              - ((h2 * 2) * _Size)
                              + ((h2 * 2)*Position.Z*_Size)
                    );
                
                if ( w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                {
                    Color color = texture.GetPixel(xx, (uint)yy);
                        
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
            DrawWall( Map, Position, (x,y),
                halfres, Cube_Height, Textures, Image, cos, cos2,i);
        }
    }
    public void Draw(Carte Map, (double X, double Y, double Z) Position, int Cube_Height, double rot, 
        Image[] Textures, byte[] Image, 
        RaycastSprite[] Entities, 
        RenderWindow _window)
    {
        
        double FOV = 60;
        int halfres = ((int)_Height) / 2;
        double mod = _Height / FOV;
        double FOVSTEP = FOV / (_Width);

        Parallel.For(0, _Width, i =>
        {
            DrawColumn(Map, Position, Cube_Height, rot,
                Textures, Image, (int)i, halfres, mod, FOV, Entities);
        });
        
        Image image2 = new Image((uint)_Width, (uint)_Height, Image);
        //Image test = new Image(image);

        Texture texture = new Texture(image2);
        Sprite sprite = new Sprite(texture);
        sprite.Draw(_window,RenderStates.Default);
        
    }
    
    
}