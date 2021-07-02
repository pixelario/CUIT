using FluentAssertions;
using Xunit;
namespace Pixelario.CUIT.UniTests
{
    public class CompleteCUIT
    {
        public static TheoryData<TestCompleteCUIT> CUITsTheoryData => new TheoryData<TestCompleteCUIT>()
        {
            TestCompleteCUIT.ValidCUIT,
            TestCompleteCUIT.ValidCUITCorto
        };
        
        [Theory]
        [MemberData(nameof(CUITsTheoryData))]
        public void ReturnsFormatedCUIT(TestCompleteCUIT testCUIT)
        {
            CUIT.Complete(
                tipoDeCUIT: testCUIT.TipoDeCUIT,
                numeroDeDocumento: testCUIT.NumeroDeDocumento)
                .Should().Be(testCUIT.ResultadoCorrecto);
        }
    }
}
