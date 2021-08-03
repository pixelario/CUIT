using System;
using System.Collections.Generic;
using System.Text;

namespace Pixelario.CUIT.Extensions
{
    public static partial class EnumerableMethods
    {
        public static IEnumerable<CUIT> OnlyValid(this IEnumerable<CUIT> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }
            foreach(var cuit in input)
            {
                if(cuit.IsValid())
                {
                    yield return cuit;
                }               
            }
        }
    }
}
