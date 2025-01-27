using Avalonia.Media.Imaging;
using TSListCreator.Utils;

namespace TSListCreator.ViewModels;

public class MainViewModel : DataModel
{
    private Bitmap? _image = null;
    public Bitmap? Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }

    public bool CanInteract
    {
        get => Image != null;
    }
    public MainViewModel() {
    }
}
