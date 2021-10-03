using FluentAssertions;
using System;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CUITArgumentException
    {
        public static TheoryData<TestCUITArgumentException> CUITsTheoryData => new TheoryData<TestCUITArgumentException>()
        {
            
            TestCUITArgumentException.BadArgumentCUITConParametros,            
            TestCUITArgumentException.BadArgumentCUITCortoConParametros,
            TestCUITArgumentException.BadArgumentCUITConMinLong,
            TestCUITArgumentException.BadArgumentCUITConMaxLong,
            TestCUITArgumentException.BadArgumentCUITCortoConLong,
            TestCUITArgumentException.BadArgumentCUITNegative

        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITArgumentException testCUIT)
        {
            Assert.Throws<ArgumentOutOfRangeException>(testCUIT.Action);
        }
    }
}
