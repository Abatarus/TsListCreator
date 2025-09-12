using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsListCreator.Shared.Utils;

public interface ITsColor
{
    double R { get; }
    double G { get; }
    double B { get; }

    uint Value { get; set; }
}