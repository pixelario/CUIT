using FluentAssertions;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CUITEquality
    {
        public static TheoryData<TestCUITEquality> CUITsTheoryData => new TheoryData<TestCUITEquality>()
        {
            TestCUITEquality.EqualsCUITLargosConParametros,
            TestCUITEquality.NotEqualsCUITLargosConParametros,
            TestCUITEquality.EqualsCUITCortoConParametros,
            TestCUITEquality.NotEqualsCUITCortosConParametros,
            TestCUITEquality.EqualsCUITLargosConStrings,
            TestCUITEquality.NotEqualsCUITLargosConStrings,
            TestCUITEquality.EqualsCUITCortoConStrings,
            TestCUITEquality.NotEqualsCUITCortoConStrings
        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITEquality testCUIT)
        {
            testCUIT.CUIT1.Equals(testCUIT.CUIT2).Should().Be(testCUIT.ResultadoCorrecto);
            (testCUIT.CUIT1 == testCUIT.CUIT2).Should().Be(testCUIT.ResultadoCorrecto);
            (testCUIT.CUIT1 != testCUIT.CUIT2).Should().Be(!testCUIT.ResultadoCorrecto);
        }
    }
}
