using FluentAssertions;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CUITIsValid
    {
        public static TheoryData<TestCUITIsValid> CUITsTheoryData => new TheoryData<TestCUITIsValid>()
        {
            TestCUITIsValid.ValidCUITConParametros,
            TestCUITIsValid.InValidCUITConParametros,
            TestCUITIsValid.ValidCUITConString,
            TestCUITIsValid.InValidCUITConString,
            TestCUITIsValid.ValidCUITCortoConParametros,
            TestCUITIsValid.InValidCUITCortoConParametros,
            TestCUITIsValid.ValidCUITCortoConString,
            TestCUITIsValid.InValidCUITCortoConString
        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITIsValid testCUIT)
        {
            testCUIT.IsValid().Should().Be(testCUIT.ResultadoCorrecto);
        }
    }
}
