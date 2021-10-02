using FluentAssertions;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CUITIsValid
    {
        public static TheoryData<TestCUITIsValid> CUITsTheoryData => new TheoryData<TestCUITIsValid>()
        {
            TestCUITIsValid.ValidCUITConParametros,
            // TestCUITIsValid.InValidCUITConParametros,
            TestCUITIsValid.ValidCUITConString,
            //TestCUITIsValid.InValidCUITConString,
            TestCUITIsValid.ValidCUITCortoConParametros,
            //TestCUITIsValid.InValidCUITCortoConParametros,
            TestCUITIsValid.ValidCUITCortoConString,
            //TestCUITIsValid.InValidCUITCortoConString,
            TestCUITIsValid.ValidCUITConLong,
            //TestCUITIsValid.InValidCUITConMinLong,
            //TestCUITIsValid.InValidCUITConMaxLong,
            //TestCUITIsValid.ValidCUITCortoConLong,
            //TestCUITIsValid.InValidCUITCortoConLong
        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITIsValid testCUIT)
        {
            testCUIT.CUIT.IsValid().Should().Be(testCUIT.ResultadoCorrecto);
        }
    }
}
