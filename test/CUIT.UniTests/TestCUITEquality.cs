namespace Pixelario.CUIT.UniTests
{
    public class TestCUITEquality
    {
        public bool ResultadoCorrecto { get; private set; }
        public CUIT CUIT1 { get; private set; }
        public CUIT CUIT2 { get; private set; }

        public static readonly TestCUITEquality EqualsCUITLargosConParametros = new TestCUITEquality(
            resultadoCorrecto: true,
            cuit1: new CUIT(tipoDeCUIT: TipoDeCUIT._20,
                numeroDeDocumento: 27001001,
                verificador: 7),
            cuit2: new CUIT(tipoDeCUIT: TipoDeCUIT._20,
                numeroDeDocumento: 27001001,
                verificador: 7));
        public static readonly TestCUITEquality NotEqualsCUITLargosConParametros = new TestCUITEquality(
            resultadoCorrecto: false,
            cuit1: new CUIT(tipoDeCUIT: TipoDeCUIT._20,
                numeroDeDocumento: 27001001,
                verificador: 7),
            cuit2: new CUIT(tipoDeCUIT: TipoDeCUIT._20,
                numeroDeDocumento: 28001001,
                verificador: 7));
        public static readonly TestCUITEquality EqualsCUITCortoConParametros = new TestCUITEquality(
            resultadoCorrecto: true,
            cuit1: new CUIT(tipoDeCUIT: TipoDeCUIT._20,
                numeroDeDocumento: 7001001,
                verificador: 7),
            cuit2: new CUIT(tipoDeCUIT: TipoDeCUIT._20,
                numeroDeDocumento: 7001001,
                verificador: 7));
        public static readonly TestCUITEquality NotEqualsCUITCortosConParametros = new TestCUITEquality(
            resultadoCorrecto: false,
            cuit1: new CUIT(tipoDeCUIT: TipoDeCUIT._20,
                numeroDeDocumento: 7001001,
                verificador: 7),
            cuit2: new CUIT(tipoDeCUIT: TipoDeCUIT._20,
                numeroDeDocumento: 8001001,
                verificador: 7));

        public static readonly TestCUITEquality EqualsCUITLargosConStrings = new TestCUITEquality(
            resultadoCorrecto: true,
            cuit1: new CUIT("20270010017"),
            cuit2: new CUIT("20270010017"));
        public static readonly TestCUITEquality NotEqualsCUITLargosConStrings = new TestCUITEquality(
            resultadoCorrecto: false,
            cuit1: new CUIT("20270010017"),
            cuit2: new CUIT("20280010017"));

        public static readonly TestCUITEquality EqualsCUITCortoConStrings = new TestCUITEquality(
            resultadoCorrecto: true,
            cuit1: new CUIT("2070010017"),
            cuit2: new CUIT("2070010017"));
        public static readonly TestCUITEquality NotEqualsCUITCortoConStrings = new TestCUITEquality(
            resultadoCorrecto: false,
            cuit1: new CUIT("2070010017"),
            cuit2: new CUIT("2080010017"));

        protected TestCUITEquality(bool resultadoCorrecto, CUIT cuit1, CUIT cuit2)
        {
            this.ResultadoCorrecto = resultadoCorrecto;
            this.CUIT1 = cuit1;
            this.CUIT2 = cuit2;
        }
    }
}
