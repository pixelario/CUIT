using System;

namespace Pixelario.CUIT.UniTests
{
    public class TestCUITArgumentException
    {
        public CUIT CUIT { get; set; }
        public Action Action { get; set; }

        public static readonly TestCUITArgumentException BadArgumentCUITConParametros = new TestCUITArgumentException(
            tipoDeCUIT: TipoDeCUIT._23,
            numeroDeDocumento: 27001001,
            verificador: 70);
        public static readonly TestCUITArgumentException BadArgumentCUITCortoConParametros = new TestCUITArgumentException(
            tipoDeCUIT: TipoDeCUIT._23,
            numeroDeDocumento: 001001,
            verificador: 2);
        public static readonly TestCUITArgumentException BadArgumentCUITCortoConLong = new TestCUITArgumentException(
            2070010012);
        public static readonly TestCUITArgumentException BadArgumentCUITConMinLong = new TestCUITArgumentException(
            0);
        public static readonly TestCUITArgumentException BadArgumentCUITConMaxLong = new TestCUITArgumentException(
            99999999999);
        public static readonly TestCUITArgumentException BadArgumentCUITNegative = new TestCUITArgumentException(
            -1);
        protected TestCUITArgumentException(TipoDeCUIT tipoDeCUIT, int numeroDeDocumento, 
            byte verificador)
            
        {
            this.Action = () =>
            {
                this.CUIT = new CUIT(tipoDeCUIT: tipoDeCUIT, numeroDeDocumento: numeroDeDocumento,
                  verificador: verificador);
            };
        }
        protected TestCUITArgumentException(long cuit)
        {
            this.Action = () =>
            {
                this.CUIT = CUIT.Parse(cuit);
            };            
        }

    }
}
