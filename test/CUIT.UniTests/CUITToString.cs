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
            TestCUITToString.CUITCortoEnStringConEspacios,
            TestCUITToString.CUITNormalEnStringConChar,
            TestCUITToString.CUITCortoEnStringConChar
        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCUITToString testCUIT)
        {
            string result;
            if (string.IsNullOrEmpty(testCUIT.Delimitador) && testCUIT.DelimitadorChar == default(char))
            {
                result = testCUIT.CUIT.ToString();
            }
            else if (testCUIT.DelimitadorChar == default(char))
            {
                result = testCUIT.CUIT.ToString(testCUIT.Delimitador);
            }
            else 
            {
                result = testCUIT.CUIT.ToString(testCUIT.DelimitadorChar);
            }
            result.Should().Be(testCUIT.ResultadoCorrecto);
        }
    
    
    }
}
