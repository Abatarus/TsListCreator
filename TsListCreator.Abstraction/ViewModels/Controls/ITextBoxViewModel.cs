using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Shared.ViewModels.Controls
{
    public interface ITextBoxViewModel : IControlViewModel
    {
        public AlignmentId Alignment { get; set; }
        public string? Value { get; set; }
        public string? Label { get; set; }
        public double FontSize { get; set; }
    }
}
