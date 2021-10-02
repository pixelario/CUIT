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
                if (int.Parse(cuit.Componentes.NumeroDeDocumento) >= start &&
                    int.Parse(cuit.Componentes.NumeroDeDocumento) <= end)
                {
                    yield return cuit;
                }
            }
        }
    }
}
