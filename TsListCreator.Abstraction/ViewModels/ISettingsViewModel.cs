using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsListCreator.Shared.ViewModels;

public interface ISettingsViewModel
{
    public double BoundWidth { get; set; }
    public double BoundHeight { get; set; }
    public uint Background { get; set; }

    public uint FontColor { get; set; }
}