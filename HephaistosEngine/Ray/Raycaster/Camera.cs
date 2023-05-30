namespace Raycasting;

public class Camera
{
    public (double X, double Y, double Z) _Position { get; set; }
    public double _Angle { get; set; }
    public int _VerticalPos { get; set; }
    public DrawDistance _DrawDistance { get; set; }

    public Camera(DrawDistance Distance)
    {
        _Position = (2, 2, 0);
        _Angle = 0;
        _VerticalPos = 0;
        _DrawDistance = Distance;
    }
}