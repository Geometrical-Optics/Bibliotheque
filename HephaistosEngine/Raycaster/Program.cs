using System;
using Map;
using Raycasting;
using SFML.Graphics;
using SFML.System;

Random rd = new Random();

// Width % ChunkSize = 0 et Height % ChunkSize = 0
// Width et Height multiples de ChunkSize

//Carte map2 = new Carte((Width,Height), ChunkSize);
//map2.Load("Demo_Mini_Jeu.hep");

Image[] materials = new Image[] {
    //new Texture("C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/LAB_1A.png").CopyToImage(),
    //new Texture("C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/SLIME_1A.png").CopyToImage(),
    //new Texture("C:/Users/yvanf/OneDrive/Documents/Raycaster/Raycaster/Raycaster/Ressources/Doom/TILE_2A.png").CopyToImage()


};

Carte map2 = new Carte((7,14,2), 7);

/*
map2[1, 1] = new List<Box>()
{
    new Full((1, 1), 1f, 0, 0, 0,0, 0)
};
map2[1, 2] = new List<Box>()
{
    new Full((1, 2), 1f, 0, 0, 0,0, 0)
};


map2[2, 1] = new List<Box>()
{
    new Full((2, 1), 1f, 0, 0, 0,0, 0)
};
map2[2, 2] = new List<Box>()
{
    new Full((2, 2), 1f, 0, 0, 0,0, 0)
};






map2[5, 1] = new List<Box>()
{
    new Full((5, 1), 1f, 0, 0, 0,0, 0)
};
map2[5, 2] = new List<Box>()
{
    new Full((5, 2), 1f, 0, 0, 0,0, 0)
};


map2[4, 1] = new List<Box>()
{
    new Full((4, 1), 1f, 0, 0, 0,0, 0)
};
map2[4, 2] = new List<Box>()
{
    new Full((4, 2), 1f, 0, 0, 0,0, 0)
};

map2[3, 1] = new List<Box>()
{
    new Full((3, 1), 0.5f, 0, 0, 2,2, 0)
};


map2[3, 2] = new List<Box>()
{
    new Rect((3,2),0.45f,0,0,2,2,0,new []{(0.5,0.5),(0.5,-0.3)}),
    new Rect((3,2),0.4f,0,0,2,2,0,new []{(0.5,0.5),(0.5,-0.1)}),
    new Rect((3,2),0.35f,0,0,2,2,0,new []{(0.5,0.5),(0.5,0.1)}),
    new Rect((3,2),0.3f,0,0,2,2,0,new []{(0.5,0.5),(0.5,0.3)}),
    new Rect((3,2),0.25f,0,0,2,2,0,new []{(0.5,0.5),(0.5,0.5)})

};

map2[3, 2, 0]._distance = 1500;


map2[1, 3] = new List<Box>()
{
    new Full((1, 3), 0.25f, 0, 0, 2,2, 0)
};
map2[2, 3] = new List<Box>()
{
    new Full((2, 3), 0.25f, 0, 0, 2,2, 0)
};
map2[3, 3] = new List<Box>()
{
    new Full((3, 3), 0.25f, 0, 0, 2,2, 0)
};
map2[4, 3] = new List<Box>()
{
    new Full((4, 3), 0.25f, 0, 0, 2,2, 0)
};
map2[5, 3] = new List<Box>()
{
    new Full((5, 3), 0.25f, 0, 0, 2,2, 0)
};

map2.SuperSave("","Labo", materials);*/

materials = map2.SuperLoad("", "Labo");

map2[3, 4] = new List<Box>()
{
    new Triangle((3,4),0.25f,0,1,2,2,0,
        new (double X, double Y)[] {(3,4),(4,5)})
};
map2[2, 4] = new List<Box>()
{
    new Triangle((2,4),0.25f,0,1,2,2,0,
        new (double X, double Y)[] {(3,5),(2,4)})
};


map2[2, 5] = new List<Box>()
{
    new Triangle((2,5),0.25f,0,1,2,2,0,
        new (double X, double Y)[] {(2,5),(3,6)})
};
map2[1, 5] = new List<Box>()
{
    new Triangle((1,5),0.25f,0,1,2,2,0,
        new (double X, double Y)[] {(2,6),(1,5)})
};

map2[2, 6] = new List<Box>()
{
    new Triangle((2,6),0.25f,0,1,2,2,0,
        new (double X, double Y)[] {(2,7),(3,6)})
};
map2[1, 6] = new List<Box>()
{
    new Triangle((1,6),0.25f,0,1,2,2,0,
        new (double X, double Y)[] {(2,6),(1,7)})
};

map2[3, 7] = new List<Box>()
{
    new Triangle((3,7),0.25f,0,1,2,2,0,
        new (double X, double Y)[] {(3,8),(4,7)})
};
map2[2, 7] = new List<Box>()
{
    new Triangle((2,7),0.25f,0,1,2,2,0,
        new (double X, double Y)[] {(3,7),(2,8)})
};

map2[3, 8] = new List<Box>()
{
    new Full((3,8), 0.25f, 0, 0, 2,2, 0)
};


map2[1, 8] = new List<Box>()
{
    new Full((1, 8), 0.25f, 0, 0, 2,2, 0)
};
map2[2, 8] = new List<Box>()
{
    new Full((2, 8), 0.25f, 0, 0, 2,2, 0)
};
map2[3, 8] = new List<Box>()
{
    new Full((3, 8), 0.25f, 0, 0, 2,2, 0)
};
map2[4, 8] = new List<Box>()
{
    new Full((4, 8), 0.25f, 0, 0, 2,2, 0)
};
map2[5, 8] = new List<Box>()
{
    new Full((5, 8), 0.25f, 0, 0, 2,2, 0)
};


map2[1, 9] = new List<Box>()
{
    new Full((1, 9), 1, 0, 0, 2,2, 0)
};
map2[2, 9] = new List<Box>()
{
    new Full((2, 9), 1, 0, 0, 2,2, 0)
};

map2[3, 9] = new List<Box>()
{
    new Full((3, 9), 0.25f, 0, 0, 0,2, 0.75f),
    new Rect((3,9),0.2f,0,0,2,2,0,new []{(0.5,0.5),(0.5,-0.25)}),
    new Rect((3,9),0.15f,0,0,2,2,0,new []{(0.5,0.5),(0.5,0)}),
    new Rect((3,9),0.1f,0,0,2,2,0,new []{(0.5,0.5),(0.5,0.25)}),
    new Rect((3,9),0.05f,0,0,2,2,0,new []{(0.5,0.5),(0.5,0.5)})

};

map2[3, 9, 0]._TexturePos = (0, 0.5f);

map2[3, 9, 0]._distance = 800;
map2[3, 2, 0]._distance = 800;


map2[4, 9] = new List<Box>()
{
    new Full((4, 9), 1, 0, 0, 2,2, 0)
};
map2[5, 9] = new List<Box>()
{
    new Full((5, 9), 1, 0, 0, 2,2, 0)
};

map2[5, 10, 0]._FloorId = 2;
map2[4, 10, 0]._FloorId = 2;
map2[3, 10, 0]._FloorId = 2;
map2[2, 10, 0]._FloorId = 2;
map2[1, 10, 0]._FloorId = 2;

map2[5, 11, 0]._FloorId = 2;
map2[4, 11, 0]._FloorId = 2;
map2[3, 11, 0]._FloorId = 2;
map2[2, 11, 0]._FloorId = 2;
map2[1, 11, 0]._FloorId = 2;

map2[5, 12, 0]._FloorId = 2;
map2[4, 12, 0]._FloorId = 2;
map2[3, 12, 0]._FloorId = 2;
map2[2, 12, 0]._FloorId = 2;
map2[1, 12, 0]._FloorId = 2;

map2[3, 0, 0]._TextureId = 4;

//map2[1, 2,0]._TextureId = 3;

RaycastSprite[] test = new RaycastSprite[]
{
    new RaycastSprite((1f,1f,0.5f),6,1),
    new RaycastSprite((0.5f,0.5f,0.5f),6,2)
};

test[0]._Angle = Math.PI / 4;
test[1]._Angle = Math.PI / 3;

test[0].MoveEntity(map2,(5,11,0),test);
test[1].MoveEntity(map2,(5,12.5f,0),test);

map2[4, 6] = new List<Box>()
{
    new Cylinder((4,6),0.1f,0,1,2,2,0,0.35f),
    new Cylinder((4,6),0.8f,0,1,2,2,0.1f,0.25f),
    new Cylinder((4,6),0.1f,0,1,2,2,0.9f,0.35f),
};



Demo game = new Demo("Raycasting Test", materials);
game.Run(map2,test);

//materials = map2.SuperLoad("", "Demo_Mini_Jeu");
//map2.SuperSave("","Demo_Mini_Jeu",materials);

/*CarteSimplifie tmp = new CarteSimplifie((10, 10));
tmp.Load("test");*/

/*RaycastSprite[] SpritesList = new RaycastSprite[]
{
    
    new RaycastSprite((2.55f,5.6f,0f),(0.5f,0.5f,0.5f),
        6,map2)

};

map2.MoveEntity(SpritesList[0],(2.55f,5.6f));


SpritesList[0]._TexturePosition = (0.5f, 0.5f);
SpritesList[0]._TextureList[4] = 1;
SpritesList[0]._TextureList[5] = 3;*/

/*
tmp[1, 2] = new List<Box>()
{
    new Full((1, 2), 0.5f, 3, 0, 1,2, 0),
    new Cylinder((1,2),0.5f,1,2,5,2,0.5f,0.5f)
};

tmp.Save("test");*/
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
        new (double X, double Y)[] {(5,2),(6,3)}),
    new Cylinder((5,2),0.5f,1,2,5,3,0.5f,0.5f)
};
map2[5, 2, 0]._TexturePos = (0.5f, 0.5f);
map2.Save("Demo_Mini_Jeu.hep");*/

/*map2[5, 2] = new List<Box>()
{
    new Triangle((5,2),0.5f,3,1,1,1,0,
        new (double X, double Y)[] {(5,2),(6,3)}),
    new Cylinder((5,2),0.5f,1,2,5,3,0.1f,0.5f)
};*/