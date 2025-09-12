using FakeItEasy;
using System;
using System.Collections.Generic;

namespace TSListCreator.Tests.Mocks;

class ImageDataServiceMock : Fake<IImageDataService>
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