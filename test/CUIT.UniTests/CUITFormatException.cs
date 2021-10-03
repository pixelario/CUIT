using FluentAssertions;
using System;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CUITFormatException
    {
        public static TheoryData<TestCUITFormatException> CUITsTheoryData => new TheoryData<TestCUITFormatException>()
        {
            
            TestCUITFormatException.BadFormatCUITConString,            
            TestCUITFormatException.BadFormatCUITCortoConString,
            TestCUITFormatException.BadFormatCUITAlfaNumeric,
            TestCUITFormatException.BadFormatCUITEmptyString,
            TestCUITFormatException.BadFormatCUITWhitString
        };       
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITFormatException testCUIT)
        {
            Assert.Throws<FormatException>(testCUIT.Action);
        }
    }
}
