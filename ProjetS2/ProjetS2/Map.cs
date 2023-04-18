namespace ProjetS2;

public class Map
{
    public bool Is_colliding;
    public int Longueur;
    public int Largeur;

    public Map(bool is_colliding, int longueur, int largeur)
    {
        Is_colliding = is_colliding;
        Longueur = longueur;
        Largeur = largeur;
    }
}