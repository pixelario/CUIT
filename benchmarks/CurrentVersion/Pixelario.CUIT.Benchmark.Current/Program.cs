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

            //string[] cuitArray = new string[] { //"20270010017",
            //    //"20-27001001-7","20.27001001.7", "20 27001001 7",
            //    "2070010012", "20-7001001-2","20.7001001.2","20 7001001 2"
            //};
            //foreach (var cuitString in cuitArray)
            //{
            //    var cuit = new CUIT(cuitString).ToStringWithStringCreate();
            //}
            BenchmarkRunner.Run<CodeToBench>();
        }

    }
    [MemoryDiagnoser]
    public class CodeToBench
    {
        public string[] cuitArray = new string[] { "20270010017",                
                "20070010012"
            };
        [Benchmark]
        public void CodeOld()
        {
            foreach (var cuitString in cuitArray)
            {
                var verificador = Lab.CalcularVerificador(cuitString);                       
            }
        }
        [Benchmark]
        public void CodeNew()
        {
            foreach (var cuitString in cuitArray)
            {
                var verificador = Lab.CalcularVerificadorEnLab(cuitString);
            }
        }
    }
}
