namespace Raycasting;

public class DrawDistance
{
    public static DrawDistance DrawDistance_640 = new DrawDistance(375, 1875, 
        new (uint, uint)[]
            {
                (3750,192),
                (3072,128),
                (2560,48),
                (1875,4),
                (0,2)
            });
    
    public static DrawDistance DrawDistance_320 = new DrawDistance(375, 1875, 
        new (uint, uint)[]
        {
            //(3750,256),
            //(3072,128),
            //(2560,64),
            //(1875,4),
            //(1500,3),
            (0,2)
        });
    
    public static DrawDistance DrawDistance_640_NoVerticalAngle = new DrawDistance(375, 1875, 
        new (uint, uint)[]
        {
            (3750,192),
            (3072,128),
            (2560,48),
            (1875,4),
            (0,2)
        });
    
    public static DrawDistance DrawDistance_320_NoVerticalAngle = new DrawDistance(375, 1875, 
        new (uint, uint)[]
        {
            (3750,256),
            (3072,128),
            (2560,64),
            (1875,6),
            (1000,5),
            (0,4)
        });
    
    public (uint stepnumb, uint mult)[] _Scale { get; set; }
    public uint _Step;
    public uint _MaxStep;

    public DrawDistance(uint Step, uint MaxStep)
    {
        _Step = Step;
        _MaxStep = MaxStep;
        _Scale = new (uint stepnumb, uint mult)[_MaxStep/_Step];
        uint mult = 1;

        for (uint i = 0; i < _Scale.Length; i++)
        {
            _Scale[i] = (_Step * i, mult);
            mult *= 2;
        }
    }
    
    public DrawDistance(uint Step, uint MaxStep, (uint,uint)[] Scale)
    {
        _Step = Step;
        _MaxStep = MaxStep;
        _Scale = Scale;
    }
}