using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace Pixelario.CUIT.Benchmark.New.Current
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<NewFromStringToBench>();
        }
    }
    [MemoryDiagnoser]
    public class NewFromStringToBench
    {
        [Benchmark]
        public void Code()
        {
            var cuits = new string[] { "20270010017",
                "20-27001001-17","20.27001001.17", "20 27001001 17",
                "2070010012", "20-7001001-2","20.7001001.2","20 7001001 2"
            };
            foreach (var cuit in cuits)
            {
                var item = new CUIT(cuit);
            }
        }
    }
}
