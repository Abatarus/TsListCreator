using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsListCreator.Shared.ViewModels.Controls
{
    public interface ICounterViewModel : IControlViewModel
    {
        public int Value { get; set; }
        public bool HideBg { get; set; }
    }
}
