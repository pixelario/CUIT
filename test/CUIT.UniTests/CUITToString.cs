using FluentAssertions;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CUITToString
    {
        public static TheoryData<TestCUITToString> CUITsTheoryData => new TheoryData<TestCUITToString>()
        {
            TestCUITToString.CUITNormalEnString,
            TestCUITToString.CUITCortoEnString,
            TestCUITToString.CUITNormalConGuionEnString,
            TestCUITToString.CUITCortoConGuionEnString,
            TestCUITToString.CUITNormalConPuntoEnString,
            TestCUITToString.CUITCortoConPuntoEnString,
            TestCUITToString.CUITNormalConEspacioEnString,
            TestCUITToString.CUITCortoConEspacioEnString,
            TestCUITToString.CUITNormalEnParametros,
            TestCUITToString.CUITCortoEnParametros,
            TestCUITToString.CUITNormalEnStringConGuion,
            TestCUITToString.CUITCortoEnStringConGuion,
            TestCUITToString.CUITNormalEnStringConPuntos,
            TestCUITToString.CUITCortoEnStringConPuntos,
            TestCUITToString.CUITNormalEnStringConEspacios,
            TestCUITToString.CUITCortoEnStringConEspacios
        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITToString testCUIT)
        {
            string result;
            if (string.IsNullOrEmpty(testCUIT.Delimitador))
            {
                result = testCUIT.CUIT.ToString();
            }
            else
            {
                result = testCUIT.CUIT.ToString(testCUIT.Delimitador);
            }
            result.Should().Be(testCUIT.ResultadoCorrecto);
        }
    
    
    }
}
