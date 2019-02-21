using System;
using DemoBizLogic;
using DynamicExecuteAssembly;
using Newtonsoft.Json;

namespace DemoAssemblyLauncher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var assemblyLauncher = new AssemblyLauncher();

            var result1 = assemblyLauncher.Execute<BizLogicObjcet, object>(new BizLogicObjcet(), "GetString");
            Console.WriteLine(JsonConvert.SerializeObject(result1));

            var result2 = assemblyLauncher.Execute<BizLogicObjcet, object>(
                new BizLogicObjcet(),
                "GetStringWithParamter",
                JsonConvert.SerializeObject(new { Name = "Hello World" })
                );
            Console.WriteLine(JsonConvert.SerializeObject(result2));

            try
            {
                var result3 = assemblyLauncher.Execute<BizLogicObjcet, object>(
                    new BizLogicObjcet(),
                    "GetStringWith2Paramter",
                    string.Concat(
                        JsonConvert.SerializeObject(new { Name = "Hello World1" }),
                        JsonConvert.SerializeObject(new { Name = "Hello World2" }))
                        );
                Console.WriteLine(JsonConvert.SerializeObject(result3));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
