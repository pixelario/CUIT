using System;

namespace Pixelario.CUIT.Benchmark
{
    public class Lab
    {
        public static byte CalcularVerificadorEnLab(string cadena)
        {
            
            ReadOnlySpan<char> _cadenaSpan = cadena.AsSpan();
            return Convert.ToByte((
                (6 * byte.Parse(_cadenaSpan.Slice((0), 1))) +
                (7 * byte.Parse(_cadenaSpan.Slice((1), 1))) +
                (8 * byte.Parse(_cadenaSpan.Slice((2), 1))) +
                (9 * byte.Parse(_cadenaSpan.Slice((3), 1))) +
                (4 * byte.Parse(_cadenaSpan.Slice((4), 1))) +
                (5 * byte.Parse(_cadenaSpan.Slice((5), 1))) +
                (6 * byte.Parse(_cadenaSpan.Slice((6), 1))) +
                (7 * byte.Parse(_cadenaSpan.Slice((7), 1))) +
                (8 * byte.Parse(_cadenaSpan.Slice((8), 1))) +
                (9 * byte.Parse(_cadenaSpan.Slice((9), 1)))
                )% 11);            
        }
        public static byte CalcularVerificador(string cadena)
        {
            ReadOnlySpan<char> code = "6789456789".AsSpan();
            ReadOnlySpan<char> _cadenaSpan = cadena.AsSpan();
            int x = 0;
            int suma = 0;
            while (x < 10)
            {
                int producto1 = int.Parse(code.Slice((x), 1));
                int producto2 = int.Parse(_cadenaSpan.Slice((x), 1));
                int producto = producto1 * producto2;
                suma += producto;
                x++;
            }
            return Convert.ToByte(suma % 11);
        }
    }
}
