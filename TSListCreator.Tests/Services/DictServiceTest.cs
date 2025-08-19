using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Services;

namespace TSListCreator.Tests.Services
{
    public class DictServiceTest
    {
        [Theory]
        [InlineData("Лево")]
        [InlineData("Право")]
        public void AlignmentList_MustContainsDescriptions(string input)
        {

            Assert.Contains(input, DictService.AlignmentList);
        }
    }
}
