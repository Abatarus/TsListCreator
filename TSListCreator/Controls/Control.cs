using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TSListCreator.Interfaces;

public abstract class Control : INotifyPropertyChanged, IJsonInput
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


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public abstract string GetJsonString();
}
