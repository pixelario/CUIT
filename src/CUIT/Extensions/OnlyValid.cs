using System;
using System.Collections.Generic;
using System.Text;

namespace Pixelario.CUIT.Extensions
{
    public static partial class EnumerableMethods
    {
        /// <summary>
        /// Crea una IEnumerable<CUIT> con los <see cref="CUIT">CUIT</see> válidos de <paramref name="input"/>
        /// </summary>
        /// <returns>IEnumerable<CUIT> con <see cref="CUIT">CUIT</see>s válidos</returns>
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
