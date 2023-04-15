using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Raycaster;

public class Game
{
    private const uint _width = 640;
    private const uint _height = 480;

    //private TextureManager _texturemanager;
    private VideoMode _mode = new VideoMode(_width,_height);
    private RenderWindow _window;
    private Raycaster _renderengine = new Raycaster((_width,_height), (0,0), 1);

    private double angle = 0;
    private double x = 4;
    private double y = 4;
    private float z = 0;
    private Image[] _textures;
    private int _cubeheight;

    public Game(string title, Image[] textures, int Height)
    {
        _window = new RenderWindow(_mode,title);
        _window.SetVerticalSyncEnabled(true);
        _window.Closed += (sender, args) => { _window.Close(); };
        _window.SetActive(false);
        _textures = textures;
        _cubeheight = Height;
        _renderengine = new Raycaster((_width,_height), (0,0), Height);
    }
    
    public void Run(Carte map, RaycastSprite[] sprites)
    {
        bool running = true;
        byte[] image = new byte[_width*_height*4];
        Clock clock = new Clock();
        double lastTime = 0;
        double fps = 0;
        Sprite tempomachinegun = new Sprite(new Texture(
                "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Others/machinegun.png"));

        tempomachinegun.Position = new Vector2f(0, 0);
        tempomachinegun.Scale = new Vector2f(_width, _height);
        tempomachinegun.Origin = new Vector2f(0, 0);
        double tmpangle = 0;

        while (_window.IsOpen)
        {
            tmpangle += 0.1;
            tmpangle %= Math.PI * 2;
            sprites[0].X = (float)(4.5 + Math.Cos(tmpangle) * 2);
            sprites[0].Y = (float)(4.5 + Math.Sin(tmpangle) * 2);
            sprites[0].Z = (float)(0.25 + (Math.Sin(tmpangle) / 4));
            
            _window.DispatchEvents();
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                angle -= 0.1;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                angle += 0.1;

            angle %= Math.PI * 2;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) 
                && (map[(int)(x + Math.Cos(angle) * 0.5), (int)(y + Math.Sin(angle) * 0.5)] is Empty
                    || map[(int)(x + Math.Cos(angle) * 0.5), (int)(y + Math.Sin(angle) * 0.5)].Collide(
                        ((int)(x + Math.Cos(angle) * 0.5), (int)(y + Math.Sin(angle) * 0.5), z)) == false))
            {
                x += Math.Cos(angle)*0.1;
                y += Math.Sin(angle)*0.1;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down)
                && (map[(int)(x - Math.Cos(angle) * 0.5),(int)(y - Math.Sin(angle) * 0.5)] is Empty
                || map[(int)(x - Math.Cos(angle) * 0.5), (int)(y - Math.Sin(angle) * 0.5)].Collide(
                    ((int)(x - Math.Cos(angle) * 0.5), (int)(y - Math.Sin(angle) * 0.5), z)) == false))
            {
                x -= Math.Cos(angle)*0.1;
                y -= Math.Sin(angle)*0.1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                z += 0.01f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl) && z - 0.01f >= 0 && map[(int)(x + Math.Cos(angle) * 0.5), (int)(y + Math.Sin(angle) * 0.5)].Collide(
                    ((int)(x + Math.Cos(angle) * 0.5), (int)(y + Math.Sin(angle) * 0.5), z)) == false)
            {
                z -= 0.01f;
            }

            _window.Clear();
           _renderengine.Draw(map, (x, y, z), _cubeheight, angle, _textures, image, sprites, _window);
           _window.Display();
            Array.Clear(image);
        }
    }
}