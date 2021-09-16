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
                listaDeCUITs.Add(CreateCUIT(i));
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

        public static CUIT CreateCUIT(int seed)
        {
            var tipo = TipoDeCUIT._20;
            var numeroDeDocumento = 27000000 + seed;
            var verificador = 1;
            return new CUIT(
                tipoDeCUIT: tipo,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
        }

    }
}
