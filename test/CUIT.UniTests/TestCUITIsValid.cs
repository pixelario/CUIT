namespace Pixelario.CUIT.UniTests
{
    public class TestCUITIsValid 
    {
        public bool ResultadoCorrecto { get; private set; }
        public CUIT CUIT { get; set; }
        public static readonly TestCUITIsValid ValidCUITConParametros = new TestCUITIsValid(
            resultadoCorrecto: true,
            tipoDeCUIT: TipoDeCUIT._20,
            numeroDeDocumento: 27001001,
            verificador: 7);
        public static readonly TestCUITIsValid ValidCUITConString = new TestCUITIsValid(
            resultadoCorrecto: true, "20270010017");
        public static readonly TestCUITIsValid ValidCUITCortoConParametros = new TestCUITIsValid(
           resultadoCorrecto: true,
           tipoDeCUIT: TipoDeCUIT._20,
           numeroDeDocumento: 7001001,
           verificador: 2);
        public static readonly TestCUITIsValid ValidCUITCortoConString = new TestCUITIsValid(
            resultadoCorrecto: true, "2070010012");
        public static readonly TestCUITIsValid ValidCUITConLong = new TestCUITIsValid(
            resultadoCorrecto: true, 20270010017);
        public static readonly TestCUITIsValid ValidCUITCortoConLong = new TestCUITIsValid(
           resultadoCorrecto: true, 20070010012);
        public static readonly TestCUITIsValid NoValidCUITConParametros = new TestCUITIsValid(
            resultadoCorrecto: false,
            tipoDeCUIT: TipoDeCUIT._20,
            numeroDeDocumento: 27001001,
            verificador: 8);
        public static readonly TestCUITIsValid NoValidCUITConString = new TestCUITIsValid(
            resultadoCorrecto: false, "20270010018");
        public static readonly TestCUITIsValid NoValidCUITConLong = new TestCUITIsValid(
            resultadoCorrecto: false, 20270010018);
        protected TestCUITIsValid(bool resultadoCorrecto, string cuit)            
        {
            this.CUIT = CUIT.Parse(cuit: cuit);
            this.ResultadoCorrecto = resultadoCorrecto;
        }
        protected TestCUITIsValid(bool resultadoCorrecto, 
            TipoDeCUIT tipoDeCUIT, int numeroDeDocumento, byte verificador)
            
        {
            this.CUIT = new CUIT(tipoDeCUIT: tipoDeCUIT, numeroDeDocumento: numeroDeDocumento,
                  verificador: verificador);
            this.ResultadoCorrecto = resultadoCorrecto;
        }
        protected TestCUITIsValid(bool resultadoCorrecto, long cuit)
        {
            this.CUIT = CUIT.Parse(cuit);
            this.ResultadoCorrecto = resultadoCorrecto;
        }

    }
}
