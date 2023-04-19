// See https://aka.ms/new-console-template for more information



using Physix;
using IA;
using Raycaster;
using Map;
using AudioEngine;
using System;
using SFML.Graphics;
using SFML.System;
using Game = Raycaster.Game;

Carte map = new Carte((12,9), 3);

//map[8, 8] = new Cylinder((15,15), 0.35f, 0, 1, 5,3,0.65f, 0.5f);

map[6, 6] = new Cylinder((6,6), 1, 0, 1, 5,5,0, 0.5f);

map[2, 6] = new Cylinder((2,6), 0.25f, 0, 1, 5,5,0.5f, 0.5f);

map[6, 2] = new Cylinder((6,2), 0.5f, 0, 1, 5,5,0, 0.5f);

map[2, 2] = new Full((2, 2), 1, 3, 2, 5,2, 0);

map[3, 1] = new Full((3, 1), 1, 3, 2, 5,2, 0);
map[3, 2] = new Triangle((3, 2), 1, 3, 1, 5, 2, 0,
    new (double X, double Y)[] { (3,2),(4,3) });
map[2, 3] = new Triangle((2, 3), 1, 3, 1, 5, 2, 0,
    new (double X, double Y)[] { (2,3),(3,4) });
map[1, 3] = new Full((1, 3), 1, 3, 2, 5,2, 0);

map[4, 4] = new Full((4, 4), 0.1f, 3, 2, 5,2, 0);
map[6, 4] = new Full((6, 4), 0.3f, 3, 2, 5,2, 0);
map[8, 4] = new Full((8, 4), 1, 3, 2, 5,2, 0);



//map[7, 2] = new Full((7, 2), 1, 3, 2, 5,2, 0);
map[4, 4] = new Empty((4,4), 1, 3, 2);;


Image[] materials = new[] { new Texture(
        "/home/kenny/Documents/ProjetS/Bibliotheque/HephaistosEngine/Raycaster/Ressources/Doom/SUPPORT_5D.png").CopyToImage(),
    new Texture(
        "/home/kenny/Documents/ProjetS/Bibliotheque/HephaistosEngine/Raycaster/Ressources/Doom/SLIME_1A.png").CopyToImage(),
    new Texture(
        "/home/kenny/Documents/ProjetS/Bibliotheque/HephaistosEngine/Raycaster/Ressources/Doom/TILE_2A.png").CopyToImage(),
    new Texture(
        "/home/kenny/Documents/ProjetS/Bibliotheque/HephaistosEngine/Raycaster/Ressources/Doom/SUPPORT_5D.png").CopyToImage(),
    new Texture(
    "/home/kenny/Documents/ProjetS/Bibliotheque/HephaistosEngine/Raycaster/Ressources/Doom/BRICK_1A.png").CopyToImage(),
    new Texture(
        "/home/kenny/Documents/ProjetS/Bibliotheque/HephaistosEngine/Raycaster/Ressources/Doom/TILE_2A.png").CopyToImage()
}; // machinegun.png
(double, double, double, double, double,Image)[] materials2 = new (double, double, double, double, double,Image)[]
{(4.5,4.5,0, 0.5, 1, (new Texture(
    "/home/kenny/Documents/ProjetS/Bibliotheque/HephaistosEngine/Raycaster/Ressources/cat.jpg")).CopyToImage())};

RaycastSprite[] SpritesList = new RaycastSprite[]
{
    new RaycastSprite((4.5f,4.5f,0),(0.25f,1,0.5f),
        (new Texture("/home/kenny/Documents/ProjetS/Bibliotheque/HephaistosEngine/Raycaster/Ressources/monstre.png")).CopyToImage(),
        (0,0,0))
};

bool running = true;
Game game = new Game("Raycasting Test", materials, 2);
game.Run(map,SpritesList);

Console.WriteLine("Hello");