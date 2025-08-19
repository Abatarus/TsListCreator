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
        public void AlignmentList_MustConatinsDescriptions(string input)
        {
            var dictService = new DictService();

            Assert.Contains(input, dictService.AlignmentList);
        }
    }
}
