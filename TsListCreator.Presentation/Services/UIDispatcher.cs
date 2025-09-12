using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
using TsListCreator.Shared.Services;

namespace TsListCreator.Presentation.Services
{
    public class UIDispatcher: IUIDispatcher
    {
        public void Post(Action action)
        {
            Dispatcher.UIThread.Post(action);
        }
    }
}