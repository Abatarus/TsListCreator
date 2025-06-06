using TSListCreator.Interfaces;
using TSListCreator.Utils;

public abstract class Control : DataModel, IJsonInput
{
    private double _posX = 0.0;
    public double PosX
    {
        get => _posX;
        set => SetField(ref _posX, value); 
    }

    private double _posY = 0.0;
    public double PosY
    {
        get => _posY;
        set => SetField(ref _posY, value); 
    }

    public abstract string GetJsonString();
}
