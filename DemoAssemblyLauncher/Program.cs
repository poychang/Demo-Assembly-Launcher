using DemoBizLogic;
using DynamicExecuteAssembly;
using System;
using System.Text.Json;

namespace DemoAssemblyLauncher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var assemblyLauncher = new AssemblyLauncher();

            var result1 = assemblyLauncher.Execute<BizLogicObjcet, object>(new BizLogicObjcet(), "GetString");
            Console.WriteLine(JsonSerializer.Serialize(result1));

            var result2 = assemblyLauncher.Execute<BizLogicObjcet, object>(
                new BizLogicObjcet(),
                "GetStringWithParamter",
                JsonSerializer.Serialize(new { Name = "Hello World" })
                );
            Console.WriteLine(JsonSerializer.Serialize(result2));

            try
            {
                var result3 = assemblyLauncher.Execute<BizLogicObjcet, object>(
                    new BizLogicObjcet(),
                    "GetStringWith2Paramter",
                    string.Concat(
                        JsonSerializer.Serialize(new { Name = "Hello World1" }),
                        JsonSerializer.Serialize(new { Name = "Hello World2" }))
                        );
                Console.WriteLine(JsonSerializer.Serialize(result3));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
