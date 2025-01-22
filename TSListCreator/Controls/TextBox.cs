using System.ComponentModel;
using TSListCreator.Interfaces;

namespace TSListCreator.Controls;

class TextBox : INotifyPropertyChanged, ILuaInput
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public string GetLuaString()
    {
        return "";
    }
}
