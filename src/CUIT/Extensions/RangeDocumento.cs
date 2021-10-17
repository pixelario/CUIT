using System;
using System.Collections.Generic;

namespace Pixelario.CUIT.Extensions
{
    public static partial class EnumerableMethods
    {
        /// <summary>
        /// Crea una IEnumerable<CUIT> con un total de <paramref name="count"/> <see cref="CUIT">CUIT</see>s de <paramref name="input"/>
        /// iniciando desde <paramref name="start"/>
        /// </summary>
        /// <param name="start">Valor del CUIT inicial</param>
        /// <param name="count">Cantidad de <see cref="CUIT">CUIT</see>s</param>
        /// <returns>IEnumerable<CUIT> con <see cref="CUIT">CUIT</see>s</returns>
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
