using FluentAssertions;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CUITIsValid
    {
        public static TheoryData<TestCUITIsValid> CUITsTheoryData => new TheoryData<TestCUITIsValid>()
        {
            TestCUITIsValid.ValidCUITConParametros,
            TestCUITIsValid.ValidCUITConString,
            TestCUITIsValid.ValidCUITCortoConParametros,
            TestCUITIsValid.ValidCUITCortoConString,
            TestCUITIsValid.ValidCUITConLong,
            TestCUITIsValid.NoValidCUITConLong,
            TestCUITIsValid.NoValidCUITConString,
            TestCUITIsValid.NoValidCUITConParametros
        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITIsValid testCUIT)
        {
            testCUIT.CUIT.IsValid().Should().Be(testCUIT.ResultadoCorrecto);
        }
    }
}
