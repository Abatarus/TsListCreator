using Avalonia.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Controls;
using TSListCreator.Converters;

namespace TSListCreator.Tests.Converters
{
    public class StringToDoubleConverterTest
    {
        [Fact]
        public void ConvertBack_InputCorrectString_ShouldReturnDouble()
        {
            var converter = new StringToDoubleConverter();
            var value = "1.1";
            var expected = 1.1;

            double result = (double)converter.ConvertBack(value, null, null, CultureInfo.CurrentCulture);

            Assert.Equal(expected, result, 5);
        }

        [Fact]
        public void ConvertBack_InputIncorrectString_ShouldReturnError()
        {
            var converter = new StringToDoubleConverter();
            var value = "1.1dsa";
            var expected = 0.0;

            var result = (double)converter.ConvertBack(value, null, null, CultureInfo.CurrentCulture);
            Assert.Equal(expected, result, 5);
        }


        [Fact]
        public void Convert_InputDouble_ShouldReturnString()
        {
            var converter = new StringToDoubleConverter();
            var value = 1.1;
            var expected = "1.1";


            string result = (string)converter.Convert(value, null, null, CultureInfo.CurrentCulture);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_InputNotDouble_ShouldReturnError()
        {
            var converter = new StringToDoubleConverter();
            var value = "ds";
            var expected = "0";

            var result = (string)converter.Convert(value, null, null, CultureInfo.CurrentCulture);
            
            Assert.Equal(expected, result);
        }
    }
}
