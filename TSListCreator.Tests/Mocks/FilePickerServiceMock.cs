using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using FakeItEasy;

namespace TSListCreator.Tests.Mocks;

internal class FilePickerServiceMock: Fake<ITopLevelService>
{
    public FilePickerServiceMock()
    {
        CallsTo<Task>(x =>
                x.GetImage())
            .Returns(
                Task.FromResult(
                    new Bitmap(AssetLoader.Open(new Uri("avares://TSListCreator.Tests/Images/list.png")))
                )
            );
    }
}