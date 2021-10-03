using System;

namespace Pixelario.CUIT.UniTests
{
    public class TestCUITFormatException
    {
        public CUIT CUIT { get; set; }
        public Action Action { get; set; }

        public static readonly TestCUITFormatException BadFormatCUITConString = new TestCUITFormatException(
            "232700100170");
        public static readonly TestCUITFormatException BadFormatCUITCortoConString = new TestCUITFormatException(
            "230010012");
        public static readonly TestCUITFormatException BadFormatCUITAlfaNumeric= new TestCUITFormatException(
            "2a2r{_10017%");
        public static readonly TestCUITFormatException BadFormatCUITEmptyString = new TestCUITFormatException(
            "");
        public static readonly TestCUITFormatException BadFormatCUITWhitString = new TestCUITFormatException(
            "           ");
        protected TestCUITFormatException(string cuit)            
        {
            this.Action = () =>
            {
                this.CUIT = CUIT.Parse(cuit: cuit);
            };
        }
    }
}
