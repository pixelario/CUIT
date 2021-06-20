using System;
using System.Collections.Generic;
using System.Linq;
namespace Pixelario.CUIT.Demos.PrimerDemo
{
    class Program
    {
        private static Random _random = new Random();
        private static  TiposDeCUIT[] arrayTiposDeCuit = new TiposDeCUIT[] { 
                TiposDeCUIT._20,
                TiposDeCUIT._23,
                TiposDeCUIT._24,
                TiposDeCUIT._27,
                TiposDeCUIT._30,
                TiposDeCUIT._33,
                TiposDeCUIT._34
            };
        static void Main(string[] args)
        {
            var listaDeCUITs = new List<CUIT>();
            for(int i =0; i<10000; i++)
            {
                listaDeCUITs.Add(CreateRandomCUIT());                
            }
            int cuitsValidos = 0;
            var listaDeCUITsUnicos = listaDeCUITs.GroupBy(c => c).Select(c=>c.Key).ToList();
            foreach (var cuit in listaDeCUITsUnicos)
            {
                if (cuit.IsValid())
                {
                    cuitsValidos++;
                }
            }
            Console.WriteLine(string.Format("Se generaron {0} CUITs aleatorios, solo {1} fueron distintos y solo {2} fueron validos.",
                listaDeCUITs.Count,
                listaDeCUITsUnicos.Count,
                cuitsValidos));

        }

        public static CUIT CreateRandomCUIT()
        {
            var tipo = arrayTiposDeCuit[_random.Next(arrayTiposDeCuit.Length - 1)];
            var numeroDeDocumento = 27000000 + _random.Next(100);
            var verificador = _random.Next(9);
            return new CUIT(
                tipoDeCUIT: tipo, 
                numeroDeDocumento: numeroDeDocumento, 
                verificador: verificador) ;
        }

    }
}
