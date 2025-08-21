using Avalonia;
using Newtonsoft.Json.Linq;
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
            ConverterServiceContainer serviceContainer =
                new ConverterServiceContainer(
                    new SettingsServiceMock().FakedObject,
                    new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsPosConverter();

            var height = 0.0;
            double expected = -1.9 ;

            double result = (double)converter.Convert(height, null, "Height", CultureInfo.CurrentCulture);

            Assert.Equal(expected, result, 1);
        }

        [Fact]
        public void Convert_ValueIsMax_ShouldReturnAsInTableTop()
        {
            ConverterServiceContainer serviceContainer =
                new ConverterServiceContainer(
                    new SettingsServiceMock().FakedObject,
                    new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsPosConverter();

            var height = serviceContainer.ImageDataService.GetImageHeight();
            double expected = 1.9;

            double result = (double)converter.Convert(height, null, "Height", CultureInfo.CurrentCulture);

            Assert.Equal(expected, result, 1);
        }

        [Fact]
        public void Convert_ValueIsHalf_ShouldReturnAsInTableTop()
        {
            ConverterServiceContainer serviceContainer =
                new ConverterServiceContainer(
                    new SettingsServiceMock().FakedObject,
                    new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsPosConverter();

            var height = serviceContainer.ImageDataService.GetImageHeight() / 2;
            double expected = 0;

            double result = (double)converter.Convert(height, null, "Height", CultureInfo.CurrentCulture);

            Assert.Equal(expected, result, 1);
        }

        [Fact]
        public void ConvertBack_ValueIsZero_ShouldReturnAsInTableTop()
        {
            ConverterServiceContainer serviceContainer =
                new ConverterServiceContainer(
                    new SettingsServiceMock().FakedObject,
                    new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsPosConverter();

            double value = 0;
            double expected = serviceContainer.ImageDataService.GetImageHeight() / 2;

            double result = (double)converter.ConvertBack(value, null, "Height", CultureInfo.CurrentCulture);

            Assert.Equal(expected, result, 5);
        }

        [Fact]
        public void ConvertBack_ValueIsMax_ShouldReturnAsInTableTop()
        {
            ConverterServiceContainer serviceContainer =
                new ConverterServiceContainer(
                    new SettingsServiceMock().FakedObject,
                    new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsPosConverter();

            double value = serviceContainer.SettingsService.BoundHeight / 2;
            double expected = serviceContainer.ImageDataService.GetImageHeight();

            var result = (double)converter.ConvertBack(value, null, "Height", CultureInfo.CurrentCulture);

            Assert.Equal(expected, result, 5);
        }

        [Fact]
        public void ConvertBack_ValueIsMin_ShouldReturnAsInTableTop()
        {
            ConverterServiceContainer serviceContainer =
                new ConverterServiceContainer(
                    new SettingsServiceMock().FakedObject,
                    new ImageDataServiceMock().FakedObject);
            var converter = new CanvasCoorToTsPosConverter();

            double value = -serviceContainer.SettingsService.BoundHeight / 2;
            double expected = 0;

            double result = (double)converter.ConvertBack(value, null, "Height", CultureInfo.CurrentCulture);

            Assert.Equal(expected, result, 5);
        }
    }
}
