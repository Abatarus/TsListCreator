using Avalonia;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Controls;
using TSListCreator.Converters;
using TSListCreator.Services;
using TSListCreator.Tests.Mocks;

namespace TSListCreator.Tests.Converters
{
    public class CanvasCoorToTsPosConverterTest
    {
        [Fact]
        public void Convert_ValueIsZero_ShouldReturnAsInTableTop()
        {
            SingletonServiceContainer serviceContainer =
                new SingletonServiceContainer(new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsPosConverter();

            var height = 0.0;
            double expected = -1.9 ;

            double result = (double)converter.Convert(height, null, "Height", CultureInfo.CurrentCulture);

            Assert.Equal(expected, result, 1);
        }
    }
}
