using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixelario.CUIT;
namespace Pixelario.CUIT.UniTests
{
    public class TestCUITToString 
    {
        public static readonly TestCUITToString CUITNormalEnString = new TestCUITToString("20270010010", null,"20270010010");
        public static readonly TestCUITToString CUITCortoEnString = new TestCUITToString("20070010010", null, "2070010010");
        public static readonly TestCUITToString CUITNormalConGuionEnString = new TestCUITToString("20270010010", null, "20-27001001-0");
        public static readonly TestCUITToString CUITCortoConGuionEnString = new TestCUITToString("20070010010", null, "20-7001001-0");
        public static readonly TestCUITToString CUITNormalConPuntoEnString = new TestCUITToString("20270010010", null, "20.27001001.0");
        public static readonly TestCUITToString CUITCortoConPuntoEnString = new TestCUITToString("20070010010", null, "20.7001001.0");
        public static readonly TestCUITToString CUITNormalConEspacioEnString = new TestCUITToString("20270010010", null, "20 27001001 0");
        public static readonly TestCUITToString CUITCortoConEspacioEnString = new TestCUITToString("20070010010", null, "20 7001001 0");
        public static readonly TestCUITToString CUITNormalEnParametros = new TestCUITToString("20270010010", null, TipoDeCUIT._20, 27001001, 0);
        public static readonly TestCUITToString CUITCortoEnParametros = new TestCUITToString("20070010010", null, TipoDeCUIT._20, 7001001, 0);
        public static readonly TestCUITToString CUITNormalEnStringConGuion = new TestCUITToString("20-27001001-0", "guion", "20-27001001-0");
        public static readonly TestCUITToString CUITCortoEnStringConGuion = new TestCUITToString("20-07001001-0", "guion", "20-7001001-0");
        public static readonly TestCUITToString CUITNormalEnStringConPuntos = new TestCUITToString("20.27001001.0", "punto", "20-27001001-0");
        public static readonly TestCUITToString CUITCortoEnStringConPuntos = new TestCUITToString("20.07001001.0", "punto", "20-7001001-0");
        public static readonly TestCUITToString CUITNormalEnStringConEspacios = new TestCUITToString("20 27001001 0", "espacio", "20-27001001-0");
        public static readonly TestCUITToString CUITCortoEnStringConEspacios = new TestCUITToString("20 07001001 0", "espacio", "20-7001001-0");

        protected TestCUITToString(string resultadoCorrecto, string delimitador, string cuit) 
        {
            this.CUIT = CUIT.Parse(cuit);
            this.ResultadoCorrecto = resultadoCorrecto;
            this.Delimitador = delimitador;
        }
        protected TestCUITToString(string resultadoCorrecto, 
            string delimitador, TipoDeCUIT tipoDeCUIT, int numeroDeDocumento, byte verificador)
            
        {
            this.CUIT = new CUIT(tipoDeCUIT: tipoDeCUIT, numeroDeDocumento: numeroDeDocumento,
                  verificador: verificador);
            this.ResultadoCorrecto = resultadoCorrecto;
            this.Delimitador = delimitador;

        }

        public string ResultadoCorrecto { get; private set; }
        public string Delimitador { get; private set; }
        public CUIT CUIT { get; set; }
    }
}
