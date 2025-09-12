using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsListCreator.Shared.Services;

public interface IUIDispatcher
{
    void Post(Action action);
}