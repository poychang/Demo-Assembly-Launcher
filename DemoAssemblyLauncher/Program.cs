using System;
using DynamicExecuteAssembly;
using Newtonsoft.Json;

namespace DemoAssemblyLauncher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var assemblyLauncher = new AssemblyLauncher();
            var result1 = assemblyLauncher.Execute<BizLagicObjcet, object>(new BizLagicObjcet(), "GetString", "");
            Console.WriteLine(JsonConvert.SerializeObject(result1));

            var result2 = assemblyLauncher.Execute<BizLagicObjcet, object>(new BizLagicObjcet(), "GetStringWithParamter", "'Poy'");
            Console.WriteLine(JsonConvert.SerializeObject(result2));

            try
            {
                var result3 = assemblyLauncher.Execute<BizLagicObjcet, object>(new BizLagicObjcet(), "GetStringWith2Paramter", "'Poy', 'Chang'");
                Console.WriteLine(JsonConvert.SerializeObject(result3));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
