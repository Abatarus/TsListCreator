using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsListCreator.Shared.Enums;

public enum AlignmentId
{
    [Description("Автоматически")] Automatic,
    [Description("Лево")] Left,
    [Description("Центру")] Center,
    [Description("Право")] Right,
    [Description("Ширине")] Justified
}