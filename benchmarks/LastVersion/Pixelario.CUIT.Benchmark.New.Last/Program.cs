using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace Pixelario.CUIT.Benchmark.New.Last
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
            var listado = new List<CUIT>();
            foreach (var cuit in CreateRandomCUITs())
            {
                listado.Add(new CUIT(cuit));
            }
        }
        private static Random _random = new Random();
        private static string[] arrayTiposDeCuit = new string[] {
                "20",
                "23",
                "24",
                "27",
                "30",
                "33",
                "34"
            };

        public static List<string> CreateRandomCUITs()
        {
            var cuits = new List<string>();

            for (int i = 0; i < 250; i++)
            {
                var tipo = arrayTiposDeCuit[_random.Next(arrayTiposDeCuit.Length - 1)];
                var numeroDeDocumento = 27000000 + _random.Next(100);
                var verificador = _random.Next(9);
                cuits.Add(string.Format("{0}{1}{2}", tipo, numeroDeDocumento.ToString(), verificador.ToString()));
            }
            for (int i = 0; i < 250; i++)
            {
                var tipo = arrayTiposDeCuit[_random.Next(arrayTiposDeCuit.Length - 1)];
                var numeroDeDocumento = 27000000 + _random.Next(100);
                var verificador = _random.Next(9);
                cuits.Add(string.Format("{0}-{1}-{2}", tipo, numeroDeDocumento.ToString(), verificador.ToString()));
            }
            for (int i = 0; i < 250; i++)
            {
                var tipo = arrayTiposDeCuit[_random.Next(arrayTiposDeCuit.Length - 1)];
                var numeroDeDocumento = 27000000 + _random.Next(100);
                var verificador = _random.Next(9);
                cuits.Add(string.Format("{0}.{1}.{2}", tipo, numeroDeDocumento.ToString(), verificador.ToString()));
            }
            for (int i = 0; i < 250; i++)
            {
                var tipo = arrayTiposDeCuit[_random.Next(arrayTiposDeCuit.Length - 1)];
                var numeroDeDocumento = 27000000 + _random.Next(100);
                var verificador = _random.Next(9);
                cuits.Add(string.Format("{0} {1} {2}", tipo, numeroDeDocumento.ToString(), verificador.ToString()));
            }
            return cuits;
        }
    }

}
