using System;
using System.Collections.Generic;

namespace Pixelario.CUIT.Extensions
{
    public static partial class EnumerableMethods
    {
        public static IEnumerable<CUIT> RangeDocumento(this IEnumerable<CUIT> input,
            int start, int count)
        {
            var end = start + count;
            if (input == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var cuit in input)
            {

                if (cuit.Componentes.NumeroDeDocumento >= start && 
                    cuit.Componentes.NumeroDeDocumento <= end)
                {
                    yield return cuit;
                }
            }
        }
    }
}
