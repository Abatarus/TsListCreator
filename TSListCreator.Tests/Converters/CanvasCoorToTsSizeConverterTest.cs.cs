using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Converters;
using TSListCreator.Services;
using TSListCreator.Tests.Mocks;

namespace TSListCreator.Tests.Converters
{
    public class CanvasCoorToTsSizeConverterTest
    {
        [Fact]
        public void ConvertBack_ValueIsHalf_ShouldReturnAsInTableTop()
        {
            ConverterServiceContainer serviceContainer =
                new ConverterServiceContainer(
                    new SettingsServiceMock().FakedObject,
                    new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsSizeConverter();

            double value = serviceContainer.ImageDataService.GetImageHeight() / 2;
            double expected = 9;

            double result = (double)converter.ConvertBack(value, null, "Height", CultureInfo.CurrentCulture);
            result /= 1000;
            Assert.Equal(expected, result, 1);
        }
        [Fact]
        public void Convert_ValueIsHalf_ShouldReturnAsInTableTop()
        {
            ConverterServiceContainer serviceContainer =
                new ConverterServiceContainer(
                    new SettingsServiceMock().FakedObject,
                    new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsSizeConverter();

            double value = 9000;
            double expected = serviceContainer.ImageDataService.GetImageHeight() / 1000 / 2;

            double result = (double)converter.Convert(value, null, "Height", CultureInfo.CurrentCulture);
            result /= 1000;
            Assert.Equal(expected, result, 1);
        }
    }
}
