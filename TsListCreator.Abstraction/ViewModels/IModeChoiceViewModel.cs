using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Shared.ViewModels;

public interface IModeChoiceViewModel
{
    public Mode Mode { get; set; }
    public bool Magnet { get; set; }
}