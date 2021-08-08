using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Pixelario.CUIT.Extensions;

namespace Pixelario.CUIT.UniTests
{
    public class RangeDocumento
    {        
        [Fact]
        public void GetBetween27001001And27001999()
        {
            var input = new List<CUIT>() { 
                new CUIT("20270010017"),
                new CUIT("27230010012"),
                new CUIT("23270019990"),
                new CUIT("23270020004")
            };
            Assert.Equal(2, input.RangeDocumento(27001001, 998).Count());
        }

    }
}
