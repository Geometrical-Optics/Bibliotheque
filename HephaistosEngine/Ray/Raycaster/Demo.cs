using System.Diagnostics;
using System.Runtime.CompilerServices;
using Map;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Raycasting;

public class Demo
{
    private const uint _width = 640/2;
    private const uint _height = 480/2;

    //private TextureManager _texturemanager;
    private VideoMode _mode = new VideoMode(_width,_height);
    private RenderWindow _window;
    
    
    private Raycaster _renderengine; // = new Raycaster((320,240), (100,20), 1);
    
    

    private double angle = 0;
    private double x = 3.5;
    private double y = 1.5;
    private float z = 0.51f;
    
    private Image[] _textures;

    public Demo(string title, Image[] textures)
    {
        
        
        _window = new RenderWindow(_mode,title); // Utiliser Styles.Fullscreen si vous souhaitez démarrer en plein écran
        _window.SetVerticalSyncEnabled(true);
        _window.Closed += (sender, args) => { _window.Close(); };
        _window.SetActive(false);


        _textures = textures;
        _renderengine = new Raycaster((_width,_height), (0,0));
    }
    
    public void Run(Carte map, RaycastSprite[] sprites)
    {
        uint score = 0;
        DateTime last = DateTime.Now;
        bool running = true;
        Random rd = new Random();
        byte[] image = new byte[_width*_height*4];
        Clock clock = new Clock();
        double lastTime = 0;
        double fps = 0;
        
        
        var camtest = new Camera(DrawDistance.DrawDistance_320_NoVerticalAngle);
        
        
        //var music = new Music(
        //    "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/doom.ogg");
        Sprite tempomachinegun = new Sprite(new Texture(
                "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/gun.png"));

        Sprite cr = new Sprite(new Texture(
            "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/cr.png"));

        Sprite fire = new Sprite(new Texture(
            "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/explosion.png"));

        
        tempomachinegun.Origin = new Vector2f(0, 0);
        tempomachinegun.Scale = new Vector2f(0.5f,0.5f);
        tempomachinegun.Position = new Vector2f(100,0);
        fire.Position = new Vector2f(150, 100);
        fire.Scale = new Vector2f(0.2f,0.2f);
        //tempomachinegun.Color = Color.Cyan;
        double tmpangle = 0;

        tempomachinegun.Origin = new Vector2f(0, 0);
        cr.Position = new Vector2f(160 - (cr.Texture.Size.X / 2), 120);

        bool space = true;
        /*Music tmpm = new Music(
            "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/fire.ogg");

        music.Loop = true;
        music.Play();*/
        while (_window.IsOpen)
        {
            //sprites[0]._Angle = angle;
            //sprites[0].FaceTo(x,y);
            camtest._Position = (x, y, z);
            camtest._Angle = angle;
            _window.DispatchEvents();
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                angle -= 0.1;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                angle += 0.1;

            angle %= Math.PI * 2;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) 
                && map.IsColliding((x + Math.Cos(angle) * 0.5,y + Math.Sin(angle) * 0.5,z)) == false)
                //&& (map[(int)(x + Math.Cos(angle) * 0.5), (int)(y + Math.Sin(angle) * 0.5),0] is Empty
                //    || map[(int)(x + Math.Cos(angle) * 0.5), (int)(y + Math.Sin(angle) * 0.5),0].Collide(
                //        ((int)(x + Math.Cos(angle) * 0.5), (int)(y + Math.Sin(angle) * 0.5), z)) == false))
            {
                x += Math.Cos(angle)*0.1;
                y += Math.Sin(angle)*0.1;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down)
                && map.IsColliding((x - Math.Cos(angle) * 0.5,y - Math.Sin(angle) * 0.5,z)) == false)
                //&& (map[(int)(x - Math.Cos(angle) * 0.5),(int)(y - Math.Sin(angle) * 0.5),0] is Empty
                //|| map[(int)(x - Math.Cos(angle) * 0.5), (int)(y - Math.Sin(angle) * 0.5),0].Collide(
                //    ((int)(x - Math.Cos(angle) * 0.5), (int)(y - Math.Sin(angle) * 0.5), z)) == false))
            {
                x -= Math.Cos(angle)*0.1;
                y -= Math.Sin(angle)*0.1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)
                && map.IsColliding((x,y,z+0.01f)) == false)
            {
                z += 0.01f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl) 
                && map.IsColliding((x,y,z-0.01f)) == false
                && z - 0.01 > 0 )
            {
                z -= 0.01f;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.U) && _renderengine.lum > 0.05)
                _renderengine.lum -= 0.05f;
            else if (_renderengine.lum < 1 && Keyboard.IsKeyPressed(Keyboard.Key.U) == false)
                _renderengine.lum += 0.05f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
                camtest._VerticalPos += 10;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                camtest._VerticalPos -= 10;
            
            if (camtest._VerticalPos > 30 || camtest._VerticalPos < -30)
                camtest._DrawDistance = DrawDistance.DrawDistance_320;
            else 
                camtest._DrawDistance = DrawDistance.DrawDistance_320_NoVerticalAngle;
                
            
            _window.Clear();
            
            
            _renderengine.Draw(map,
               _textures,
               image,
               sprites,
               camtest,
               _window);

            _window.Display();
            Array.Clear(image);
        }
    }
}