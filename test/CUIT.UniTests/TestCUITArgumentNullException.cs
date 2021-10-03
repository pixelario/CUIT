using System;

namespace Pixelario.CUIT.UniTests
{
    public class TestCUITArgumentNullException
    {
        public CUIT CUIT { get; set; }
        public Action Action { get; set; }

        public static readonly TestCUITArgumentNullException BadArgumentCUITStringNull = 
            new TestCUITArgumentNullException(null);


        protected TestCUITArgumentNullException(string cuit)
            
        {
            this.Action = () =>
            {
                this.CUIT = CUIT.Parse(cuit);
            };
        }
    }
}
