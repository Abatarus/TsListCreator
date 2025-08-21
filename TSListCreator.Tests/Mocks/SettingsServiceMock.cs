using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using TSListCreator.Interfaces;

namespace TSListCreator.Tests.Mocks
{
    class SettingsServiceMock: Fake<ISettingsService>
    {
        public SettingsServiceMock()
        {
            CallsTo(x =>
                    x.BoundHeight)
                .Returns(3.742);
        }
    }
}
