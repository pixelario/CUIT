using System;
using System.Collections.Generic;
using System.Linq;
using Pixelario.CUIT.Extensions;
namespace Pixelario.CUIT.Demos.PrimerDemo
{
    class Program
    {
        private static Random _random = new Random();
        private static  TipoDeCUIT[] arrayTiposDeCuit = new TipoDeCUIT[] { 
                TipoDeCUIT._20,
                TipoDeCUIT._23,
                TipoDeCUIT._24,
                TipoDeCUIT._27,
                TipoDeCUIT._30,
                TipoDeCUIT._33,
                TipoDeCUIT._34
            };
        static void Main(string[] args)
        {
            var listaDeCUITs = new List<CUIT>();
            for(int i =0; i<10000; i++)
            {
                listaDeCUITs.Add(CreateRandomCUIT());                
            }
            var listaDeCUITsUnicos = listaDeCUITs.GroupBy(c => c).Select(c=>c.Key).ToList();
            Console.WriteLine(string.Format("Fueron generados {0} CUITs aleatorios.",
                listaDeCUITs.Count));
             Console.WriteLine(string.Format("{0} son distintos.",
                listaDeCUITsUnicos.Count));
            Console.WriteLine(string.Format("{0} son válidos.",
               listaDeCUITsUnicos.OnlyValid().Count()));
            Console.WriteLine(string.Format("{0} son válidos y tienen número de documento entre 27000000 y 27000009.",
                listaDeCUITsUnicos.OnlyValid().RangeDocumento(27000000,9).Count()));

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
