using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Services;

namespace TSListCreator.Tests.Mocks
{
    class ImageDataServiceMock : Fake<ImageDataService>
    {
        public ImageDataServiceMock()
        {
            CallsTo(x =>
                    x.GetImagePixHeight())
                .Returns(500);
            CallsTo(x =>
                    x.GetImageHeight())
                .Returns(1000);
        }
    }
}
