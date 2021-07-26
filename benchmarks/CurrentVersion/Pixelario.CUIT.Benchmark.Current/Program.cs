using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pixelario.CUIT.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<CodeToBench>();
        }

    }
    [MemoryDiagnoser]
    public class CodeToBench
    {
        [Benchmark]
        public void Code()
        {

            var listaDeCUITs = new List<CUIT>();
            for (int i = 0; i < 10000; i++)
            {
                listaDeCUITs.Add(CreateRandomCUIT());
            }
            int cuitsValidos = 0;
            var listaDeCUITsUnicos = listaDeCUITs.GroupBy(c => c).Select(c => c.Key).ToList();
            foreach (var cuit in listaDeCUITsUnicos)
            {
                if (cuit.IsValid())
                {
                    cuitsValidos++;
                }
            }
            Console.WriteLine(string.Format("Fueron generados {0} CUITs aleatorios, solo {1} fueron distintos y solo {2} fueron válidos.",
                listaDeCUITs.Count,
                listaDeCUITsUnicos.Count,
                cuitsValidos));
        }
        private static Random _random = new Random();
        private static TipoDeCUIT[] arrayTiposDeCuit = new TipoDeCUIT[] {
                TipoDeCUIT._20,
                TipoDeCUIT._23,
                TipoDeCUIT._24,
                TipoDeCUIT._27,
                TipoDeCUIT._30,
                TipoDeCUIT._33,
                TipoDeCUIT._34
            };

        public static CUIT CreateRandomCUIT()
        {
            var tipo = arrayTiposDeCuit[_random.Next(arrayTiposDeCuit.Length - 1)];
            var numeroDeDocumento = 27000000 + _random.Next(100);
            var verificador = _random.Next(9);
            return new CUIT(
                tipoDeCUIT: tipo,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
        }

    }
}
