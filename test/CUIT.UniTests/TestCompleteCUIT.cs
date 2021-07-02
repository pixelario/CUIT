namespace Pixelario.CUIT.UniTests
{
    public class TestCompleteCUIT
    {
        public CUIT ResultadoCorrecto { get; private set; }
        public TipoDeCUIT TipoDeCUIT { get; private set; }
        public int NumeroDeDocumento { get; private set; }

        public static readonly TestCompleteCUIT ValidCUIT = new TestCompleteCUIT(
            resultadoCorrecto: new CUIT(TipoDeCUIT._20, 27001001,7),
            tipoDeCUIT: TipoDeCUIT._20,
            numeroDeDocumento: 27001001);
        public static readonly TestCompleteCUIT ValidCUITCorto = new TestCompleteCUIT(
           resultadoCorrecto: new CUIT(TipoDeCUIT._20,7001001,2),
           tipoDeCUIT: TipoDeCUIT._20,
           numeroDeDocumento: 7001001);

       
        protected TestCompleteCUIT(CUIT resultadoCorrecto, 
            TipoDeCUIT tipoDeCUIT, 
            int numeroDeDocumento)
        {
            this.ResultadoCorrecto = resultadoCorrecto;
            this.TipoDeCUIT = tipoDeCUIT;
            this.NumeroDeDocumento = numeroDeDocumento;
        }        
    }
}
