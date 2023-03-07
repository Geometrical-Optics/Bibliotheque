// See https://aka.ms/new-console-template for more information

using ProjetS2;
using ProjetS2.Map;



Box[,] board = new Box[8,6];


Box[,] Board = new Box[board.GetLength(0),board.GetLength(1)];
    for (int x = 0; x < board.GetLength(0); x++)
    {
        for (int y = 0; y < board.GetLength(1); y++)
        {
            if (x == 0 || x == board.GetLength(0) - 1 || y == 0 || y == board.GetLength(1) - 1 || (x == 2 && y != 1)) //(x == 2 && y != 1)
                Board[x, y] = new Full((x, y), 5, 4, 0, 8, 8);
            else
                Board[x, y] = new Empty((x, y), 0, 0, 8);
        }
    }
    

Game oui = new Game(Board);
/*
NPC oui1 = new PatrouilleurX(1, (4, 4), Board);
oui.Add(oui1);
oui.PrintBoard((1,1));
oui.UpdateAll((1,1));
Console.WriteLine();
oui.PrintBoard((1,1));
oui.UpdateAll((1,1));
Console.WriteLine();
oui.PrintBoard((1,1));
oui.UpdateAll((1,1));
Console.WriteLine();
oui.PrintBoard((1,1));
oui.UpdateAll((1,1));
Console.WriteLine();
oui.PrintBoard((1,1));
*/
/*
NPC oui2 = new AgressifAstar(1, (3,3), Board);
oui.Add(oui2);
oui.PrintBoard((1,1));
Console.WriteLine();
oui.UpdateAll((1,1));
oui.PrintBoard((1,1));
Console.WriteLine();
oui.UpdateAll((1,1));
oui.PrintBoard((1,1));
*/
/*
Console.WriteLine();
NPC oui3 = new Fugitif(1, (1,2), Board);
oui.Add(oui3);
oui.PrintBoard((1,1));
Console.WriteLine();
oui.UpdateAll((1,1));
oui.PrintBoard((1,1));
*/

NPC oui3 = new AgressifAstar(1, (3,3), Board);
oui.Add(oui3);
oui.PrintBoard((1,1));
Console.WriteLine();
oui.UpdateAll((1,1));
oui.PrintBoard((1,1));
Console.WriteLine();
oui.UpdateAll((1,1));
oui.PrintBoard((1,1));
Console.WriteLine();
oui.UpdateAll((1,1));
oui.PrintBoard((1,1));
