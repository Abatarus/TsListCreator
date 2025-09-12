using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TsListCreator.Shared.ViewModels.Controls
{
    public interface IControlViewModel
    {
        string Name { get; set; }
        bool IsHighlighted { get; set; }
        ICommand Delete { get; set; }
    }
}
