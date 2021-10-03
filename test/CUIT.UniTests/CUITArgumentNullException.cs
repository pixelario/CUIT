using FluentAssertions;
using System;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CUITArgumentNullException
    {
        public static TheoryData<TestCUITArgumentNullException> CUITsTheoryData => new TheoryData<TestCUITArgumentNullException>()
        {
            
            TestCUITArgumentNullException.BadArgumentCUITStringNull
        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITArgumentNullException testCUIT)
        {
            Assert.Throws<ArgumentNullException>(testCUIT.Action);
        }
    }
}
