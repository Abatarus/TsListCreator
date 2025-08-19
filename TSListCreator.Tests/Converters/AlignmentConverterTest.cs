using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Controls;
using TSListCreator.Converters;
using Xunit.Sdk;

namespace TSListCreator.Tests.Converters
{
    public class AlignmentConverterTest
    {
        [Fact]
        public void Convert_InputLeft_ShouldReturnText()
        {
            AlignmentId alignment = AlignmentId.Left;
            var converter = new AlignmentConverter();
            
            string result = (string)converter.Convert(alignment, null, null, CultureInfo.CurrentCulture);

            Assert.Equal("Лево", result);
        }

        [Fact]
        public void ConvertBack_InputLeft_ShouldReturnCorrectAlignmentId()
        {
            string input = "Лево";
            var converter = new AlignmentConverter();

            AlignmentId result = (AlignmentId)converter.ConvertBack(input, null, null, CultureInfo.CurrentCulture);

            Assert.Equal(AlignmentId.Left, result);
        }
    }
}
