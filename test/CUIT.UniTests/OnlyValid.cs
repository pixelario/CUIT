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
    public class OnlyValid
    {        
        [Fact]
        public void ThrowsExceptionGivenEmptyInput()
        {
            var input = new List<CUIT>() { 
                CUIT.Parse("20270010017"),
                CUIT.Parse("23270010017")
            };
            Assert.Equal(1, input.OnlyValid().Count());
        }

    }
}
