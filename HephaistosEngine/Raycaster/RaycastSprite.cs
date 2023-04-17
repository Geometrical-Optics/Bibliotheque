using SFML.Graphics;

namespace Raycaster;

public class RaycastSprite
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public (float X, float Y, float Z) _Size { get; set; }
    public Image _Texture { get; }  
    public (float X, float Y, float Z) _TexturePosition { get; set; }

    public (float X, float Y, float Z) _Position
    {
        get { return (X, Y, Z); }
        set {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }
    }

    public RaycastSprite((float X, float Y, float Z) Position, (float X, float Y, float Z) Size, Image Texture, (float X, float Y, float Z) TexturePosition)
    {
        X = Position.X;
        Y = Position.Y;
        Z = Position.Z;

        _Size = Size;
        _Texture = Texture;
        _TexturePosition = TexturePosition;
    }
}