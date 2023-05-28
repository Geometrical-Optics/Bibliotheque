using System;
using Map;
using Raycasting;
using SFML.Graphics;
using SFML.System;

Random rd = new Random();

Image[] materials = new[] { new Texture(
        "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/TECH_0D.png").CopyToImage(),
    new Texture(
        "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/TILE_2A.png").CopyToImage(),
    new Texture(
        "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/TILE_2A.png").CopyToImage(),
    new Texture(
        "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/TECH_0D.png").CopyToImage(),
    new Texture(
        "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/BRICK_1A.png").CopyToImage(),
    new Texture(
        "C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/TILE_2A.png").CopyToImage()
};

RaycastSprite[] SpritesList = new RaycastSprite[]
{
    
    new RaycastSprite((2.5f,5.5f,0),(0.5f,0.5f,0.5f),
        (new Texture("C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/target.png")).CopyToImage(),
        (0,0))

};



Carte map2 = new Carte((5,5), 1);

/*map2[1, 2] = new List<Box>()
{
    new Full((1, 2), 0.5f, 3, 0, 1,2, 0),
    new Cylinder((1,2),0.5f,1,2,5,2,0.5f,0.5f)
};

map2[1, 4] = new List<Box>()
{
    new Full((1, 4), 0.2f, 3, 2, 5,1, 0.1f),
    new Cylinder((1,4),0.2f,1,2,5,3,0.5f,0.5f)
};

map2[1, 5] = new List<Box>()
{
    new Cylinder((1,5),0,1,2,5,3,0.5f,0.5f)
};

map2[1, 2, 0]._TexturePos = (0.5f, 0.5f);
map2[3, 2] = new List<Box>(){new Rect((3, 2), 0.25f, 3, 2, 5, 2, 0,
    new (double X, double Y)[] {(0.25,0.5),(0.25,0.5)})};

map2[5, 2] = new List<Box>()
{
    new Triangle((5,2),0.5f,3,1,1,1,0,
        new (double X, double Y)[] {(5,2),(6,3)})
};
map2[5, 2, 0]._TexturePos = (0.5f, 0.5f);*/

map2.Load("Demo_Mini_Jeu.hep");

//map2.MoveEntity(SpritesList[0],(2.5f,5.5f));
//SpritesList[0]._TexturePosition = (0.5f, 0.5f);

//map2.Save("Demo_Mini_Jeu.hep");



Demo game = new Demo("Raycasting Test", materials, 2);
game.Run(map2,SpritesList);
