using TsListCreator.Shared.Utils;

namespace TsListCreator.Model.Utils;

public class TsImage: DataModel
{
    public TsImage(IImageSource source)
    {
        Source = source;
    }
    private IImageSource _source;

    public IImageSource Source
    {
        get => _source;
        set => SetField(ref _source, value);
    }

    private double _width;
    public double Width
    {
        get => _width;
        set => SetField(ref _width, value);
    }

    private double _height;
    public double Height { 
        get => _height;
        set => SetField(ref _height, value);
    }
}