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
                "20-27001001-7","20.27001001.7", "20 27001001 7",
                "2070010012", "20-7001001-2","20.7001001.2","20 7001001 2"
            };
        [Benchmark]
        public void CodeOldToString()
        {
            foreach (var cuitString in cuitArray)
            {
                var cuit = new CUIT(cuitString).OldToString();
                var cuit1 = new CUIT(cuitString).OldToString("hyphen");
                var cuit2 = new CUIT(cuitString).OldToString("dot");
                var cuit3 = new CUIT(cuitString).OldToString("space");
            }
        }
        [Benchmark]
        public void CodeToStringWithStringCreate()
        {
            foreach (var cuitString in cuitArray)
            {
                var cuit = new CUIT(cuitString).ToStringWithStringCreate();
                var cuit1 = new CUIT(cuitString).ToStringWithStringCreate("hyphen");
                var cuit2 = new CUIT(cuitString).ToStringWithStringCreate("dot");
                var cuit3 = new CUIT(cuitString).ToStringWithStringCreate("space");
            }
        }
    }
}
