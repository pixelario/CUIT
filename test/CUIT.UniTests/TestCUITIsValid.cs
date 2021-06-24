namespace Pixelario.CUIT.UniTests
{
    public class TestCUITIsValid : CUIT
    {
        public bool ResultadoCorrecto { get; private set; }

        public static readonly TestCUITIsValid ValidCUITConParametros = new TestCUITIsValid(
            resultadoCorrecto: true,
            tipoDeCUIT: TipoDeCUIT._20,
            numeroDeDocumento: 27001001,
            verificador: 7);
        public static readonly TestCUITIsValid InValidCUITConParametros = new TestCUITIsValid(
            resultadoCorrecto: false,
            tipoDeCUIT: TipoDeCUIT._23,
            numeroDeDocumento: 27001001,
            verificador: 7);
        public static readonly TestCUITIsValid ValidCUITConString = new TestCUITIsValid(
            resultadoCorrecto: true, "20270010017");
        public static readonly TestCUITIsValid InValidCUITConString = new TestCUITIsValid(
            resultadoCorrecto: false, "23270010017");
        public static readonly TestCUITIsValid ValidCUITCortoConParametros = new TestCUITIsValid(
           resultadoCorrecto: true,
           tipoDeCUIT: TipoDeCUIT._20,
           numeroDeDocumento: 7001001,
           verificador: 2);
        public static readonly TestCUITIsValid InValidCUITCortoConParametros = new TestCUITIsValid(
            resultadoCorrecto: false,
            tipoDeCUIT: TipoDeCUIT._23,
            numeroDeDocumento: 7001001,
            verificador: 2);
        public static readonly TestCUITIsValid ValidCUITCortoConString = new TestCUITIsValid(
            resultadoCorrecto: true, "2070010012");
        public static readonly TestCUITIsValid InValidCUITCortoConString = new TestCUITIsValid(
            resultadoCorrecto: false, "2370010012");

        public static readonly TestCUITIsValid ValidCUITConLong = new TestCUITIsValid(
            resultadoCorrecto: true, 20270010017);
        public static readonly TestCUITIsValid ValidCUITCortoConLong = new TestCUITIsValid(
           resultadoCorrecto: true, 20070010012);
        public static readonly TestCUITIsValid InValidCUITCortoConLong = new TestCUITIsValid(
           resultadoCorrecto: false, 2070010012);
        public static readonly TestCUITIsValid InValidCUITConMinLong = new TestCUITIsValid(
            resultadoCorrecto: false, 0);
        public static readonly TestCUITIsValid InValidCUITConMaxLong = new TestCUITIsValid(
            resultadoCorrecto: false, 99999999999);
        protected TestCUITIsValid(bool resultadoCorrecto, string cuit) : base(cuit: cuit)
        {
            this.ResultadoCorrecto = resultadoCorrecto;
        }
        protected TestCUITIsValid(bool resultadoCorrecto, TipoDeCUIT tipoDeCUIT, int numeroDeDocumento, int verificador)
            : base(tipoDeCUIT: tipoDeCUIT, numeroDeDocumento: numeroDeDocumento,
                  verificador: verificador)
        {
            this.ResultadoCorrecto = resultadoCorrecto;
        }
        protected TestCUITIsValid(bool resultadoCorrecto, long cuit) : base(cuit: cuit)
        {
            this.ResultadoCorrecto = resultadoCorrecto;
        }

    }
}
