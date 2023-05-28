using System.Security.Cryptography;
using Map;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Raycasting;

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

    public void DrawVerticalLine(int Start, int i, int End, 
        ref int column, byte[] Image, Image texture, double yy,
        uint xx, Color shade)
    {
        double counter = 0;
        for (int w = Start;
             w >= End
             && w >= 0;
             w--)
        {
            
            // g = w + (int)((h * 2) * Position.Z * _Size);
            if ( w < _Height && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
            {
                Color color = texture.GetPixel(xx, (uint)(texture.Size.Y - counter - 1))*shade;

                if (color.A != 0)
                {
                    column += 1;
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

    private void DrawWall(Carte Map, (double X, double Y, double Z) Position, (double X, double Y) Coord, 
        int halfres, Image[] Textures, byte[] Image, double cos, double cos2,int i,
        ref int column, int Vert, Box Case)
    {
        var tmp = Coord;
        double x = tmp.X;
        double y = tmp.Y;
        if (Case is Empty)
            return;

        double n = Double.Abs((x - Position.X) / cos);
        double h = (halfres / ((n * cos2) + 0.000001)); // - ((h * 2) * Map[(int)x, (int)y]._posZ * _Size)
        Image texture = Textures[Case._TextureId];
        var val = (byte)(Math.Min((lum + ((h / halfres) * (1-lum))) * 255, 255));

        var shade = new Color(val, val,val);

        uint xx = Case.GetTextureX((x,y),texture);

        double yy = (1 / ((h * 2 + 0.01))) * texture.Size.Y;
        //double counter = 0;
        int End = (int)(halfres + h
                        - ((h * 2) * Case._posZ * _Size)
                        - ((h * 2) * Case._Height * _Size)
                        + ((h * 2) * Position.Z * _Size));
        
        int Start = (int)(halfres + h
                          - (int)((h * 2) * Case._posZ * _Size)
                          + (int)((h * 2) * Position.Z * _Size));

        DrawVerticalLine(Start+Vert, i, End+Vert,
            ref column, Image, texture, yy,
            xx, shade);
    }
    
    private void DrawSprite(Carte Map, (double X, double Y, double Z) Position, (double X, double Y) Coord, 
        int halfres, Image[] Textures, byte[] Image, double cos, double cos2,int i,
        RaycastSprite entity, ref int column, int Vert)
    {
        var tmp = Coord;
        double x = tmp.X;
        double y = tmp.Y;

        double n = Double.Abs((x - Position.X) / cos);
        double h = (halfres / ((n * cos2) + 0.000001)); // - ((h * 2) * Map[(int)x, (int)y]._posZ * _Size)
        Image texture = entity.GetTexture(x, y);
        if (texture != null)
        {

            var val = (byte)(Math.Min((lum + ((h / halfres) * (1 - lum))) * 255, 255));
            var shade = new Color(val, val, val);
            uint xx = entity.GetTextureX(x, y);

            double yy = (1 / ((h * 2 + 0.01))) * texture.Size.Y;
            double counter = 0;
            int End = (int)(halfres + h
                            - ((h * 2) * entity.Z * _Size)
                            - ((h * 2) * entity._Size.Z * _Size)
                            + ((h * 2) * Position.Z * _Size));

            int Start = (int)(halfres + h
                              - (int)((h * 2) * entity.Z * _Size)
                              + (int)((h * 2) * Position.Z * _Size));
            
            DrawVerticalLine(Start+Vert, i, End+Vert,
                ref column, Image, texture, yy,
                xx, shade);
            
        }
    }
    
    
    
    
    
    

    private void DrawColumn(Carte Map, 
        (double X, double Y, double Z) Position, 
        double Angle, 
        int Vert,
        Image[] Textures, 
        byte[] Image, 
        int i, 
        int halfres, 
        double mod, 
        double FOV,
        RaycastSprite[] Entities, 
        DrawDistance Dist)
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
        
        Stack<Box> drawlist = new Stack<Box>();
        Stack<(double x, double y)> spritelist = new Stack<(double x, double y)>();
        int column = 0;

        while ( distance < Dist._MaxStep 
                && x >= 0 && x < Map.GetLength(0)
                && y >= 0 && y < Map.GetLength(1)
                && (Map[(int)x, (int)y,0].IsColliding((x, y)) == false ||
                    Map[(int)x, (int)y,0]._IsTransparent)
                && column < _Height-1)
        {

            if (distance % Dist._Step == 0)
            {
                foreach (var etape in Dist._Scale)
                {
                    if (distance >= etape.stepnumb)
                    {
                        cos3 *= etape.mult;
                        sin2 *= etape.mult;
                    }
                }
            }

            Box[] CurrentWall = Map.GetCollision(x, y);

            foreach (Box Case in CurrentWall)
            {

                bool drawed = false;

                // Wall

                if (drawlist.Count == 0
                    && (Case._IsTransparent || Position.Z > 0))
                {
                    drawlist.Push(Case);
                    DrawWall(Map, Position, (x, y),
                        halfres, Textures, Image, cos, cos2, i, ref column, Vert, Case);
                }
                else if (drawlist.Contains(Case) == false
                         && (Case._IsTransparent || Position.Z > 0))
                {
                    drawlist.Push(Case);
                    DrawWall(Map, Position, (x, y),
                        halfres, Textures, Image, cos, cos2, i, ref column, Vert, Case);
                }
                
                // Top and Down
                
                if (Case is Empty == false && Case._TopDownId != 0)
                {
                    double n = Double.Abs((x - Position.X) / cos);
                    double h2 = (halfres / ((n * cos2) + 0.000001));
                    Image texture = Textures[Case._TopDownId];
                    var val = (byte)(Math.Min((lum + ((h2 / halfres) * (1-lum))) * 255, 255));

                    var shade = new Color(val, val,val);

                    uint xx = (uint)((x % 1) * (texture.Size.X - 1));
                    uint yy = (uint)((y % 1) * (texture.Size.Y - 1));
                    int w = (int)(halfres
                                  + h2
                                  - ((h2 * 2) * Case._Height * _Size)
                                  - ((h2 * 2) * Case._posZ * _Size)
                                  + ((h2 * 2) * Position.Z * _Size))+Vert;

                    if (Case._posZ > Position.Z)
                        w = (int)(halfres
                                  + h2
                                  - (h2 * 2 * Case._posZ * _Size)
                                  + (h2 * 2 * Position.Z * _Size))+Vert;

                    if (w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                    {
                        column += 1;
                        Color color = texture.GetPixel(xx, (uint)yy)*shade;

                        Image[(((w * _Width * 4) + i * 4))] = color.R;
                        Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                        Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                        Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                    }
                }
            }
            
            // Entities

            if (Map[(int)x, (int)y, 0]._ContainsEntity
                && spritelist.Count != Entities.Length)
            {

                foreach (var entity in Entities)
                {
                    if (spritelist.Contains((entity.X, entity.Y)) == false)
                    {
                        var dist = 0;
                        if (entity.GetCollision(x, y))
                        {
                            DrawSprite(Map, Position, (x, y),
                                halfres, Textures, Image, cos, cos2, i, entity, ref column, Vert);

                            spritelist.Push((entity.X, entity.Y));

                        }
                    }
                }

            }
            
            // Floor
            
            if (Map[(int)x, (int)y,0]._FloorId != 0)
            {
                double n = Double.Abs((x - Position.X) / cos);
                double h2 = (halfres / ((n * cos2) + 0.000001));
                Image texture = Textures[Map[(int)x, (int)y,0]._FloorId];

                uint xx = (uint)((x % 1) * (texture.Size.X - 1));
                uint yy = (uint)((y % 1) * (texture.Size.Y - 1));
                int w = (int)(halfres + h2 + ((h2 * 2)*Position.Z*_Size))+Vert;
                var val = (byte)(Math.Min((lum + ((h2 / halfres) * (1-lum))) * 255, 255));

                var shade = new Color(val, val,val);
                
                if ( w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                {
                    column += 1;
                    Color color = texture.GetPixel(xx, (uint)yy)*shade;
                        
                    Image[(((w * _Width * 4) + i * 4))] = color.R;
                    Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                    Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                    Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                }
                if (Map[(int)x, (int)y,0]._CeilingId != 0)
                {
                    texture = Textures[Map[(int)x, (int)y,0]._CeilingId];
                    w = (int)(halfres
                              + h2
                              - ((h2 * 2) * _Size)
                              + ((h2 * 2)*Position.Z*_Size)
                        )+Vert;
                    
                    if ( w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                    {
                        column += 1;
                        Color color = texture.GetPixel(xx, (uint)yy)*shade;
                        
                        Image[(((w * _Width * 4) + i * 4))] = color.R;
                        Image[(((w * _Width * 4) + i * 4)) + 1] = color.G;
                        Image[(((w * _Width * 4) + i * 4)) + 2] = color.B;
                        Image[(((w * _Width * 4) + i * 4)) + 3] = 255;
                    }
                }

            }
            else if (Map[(int)x, (int)y,0]._CeilingId != 0 && Map[(int)x, (int)y,0]._Height != 1)
            {
                double n = Double.Abs((x - Position.X) / cos);
                double h2 = (halfres / ((n * cos2) + 0.000001));
                Image texture = Textures[Map[(int)x, (int)y,0]._CeilingId];
                var val = (byte)(Math.Min((lum + ((h2 / halfres) * (1-lum))) * 255, 255));

                var shade = new Color(val, val,val);

                uint xx = (uint)((x % 1) * (texture.Size.X - 1));
                uint yy = (uint)((y % 1) * (texture.Size.Y - 1));
                int w = (int)(halfres
                              + h2
                              - ((h2 * 2) * _Size)
                              + ((h2 * 2)*Position.Z*_Size)
                    )+Vert;
                
                if ( w < _Height && w >= 0 && Image[(((w * _Width * 4) + i * 4)) + 3] == 0)
                {
                    column += 1;
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
            

        if (x >= 0 && x < Map.GetLength(0) && y >= 0 && y < Map.GetLength(1) && column < _Height-1)
        {
            foreach (Box Case in Map.GetCollision(x, y))
            {
                DrawWall(Map, Position, (x, y),
                    halfres, Textures, Image, cos, cos2, i, ref column, Vert, Case);
            }
        }
    }
    
    
    
    
    
    
    public void Draw(Carte Map, 
        Image[] Textures, 
        byte[] Image, 
        RaycastSprite[] Entities,
        Camera Cam,
        RenderWindow _window)
    {
        
        double FOV = 60;
        int halfres = ((int)_Height) / 2;
        double mod = _Height / FOV;
        double FOVSTEP = FOV / (_Width);

        Parallel.For(0, _Width, i =>
        {
            DrawColumn(Map, Cam._Position, Cam._Angle, Cam._VerticalPos,
                Textures, Image, (int)i, halfres, mod, FOV, Entities, Cam._DrawDistance);
        });
        
        Image image2 = new Image((uint)_Width, (uint)_Height, Image);

        Texture texture = new Texture(image2);
        Sprite sprite = new Sprite(texture);
        /*sprite.Scale = new Vector2f((float)_window.Size.X/(float)_Width,
            (float)_window.Size.Y/(float)_Height);*/
        sprite.Draw(_window,RenderStates.Default);
        
    }
}