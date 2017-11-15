using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DynamicExecuteAssembly
{
    public class AssemblyLauncher
    {
        /// <summary>執行指定組件方法</summary>
        /// <typeparam name="T1">組件類別</typeparam>
        /// <typeparam name="T2">回傳類別</typeparam>
        /// <param name="assemblyInstance">組件執行個體</param>
        /// <param name="methodName">組件方法的名稱</param>
        /// <param name="parameterByJson">組件方法的參數，請使用 JSON 格式</param>
        /// <returns></returns>
        public IExecuteResult<T2> Execute<T1, T2>(T1 assemblyInstance, string methodName, string parameterByJson)
        {
            var method = GetMethod<T1>(methodName);
            var parameters = method.GetParameters();

            // 組件方法無參數的執行方法
            if (!parameters.Any())
            {
                var executeResult = method.Invoke(assemblyInstance, new object[0]) as IExecuteResult<T2>;
                if (executeResult == null)
                    throw new Exception(
                        $"組件方法 {methodName} 未依照設計規範回傳繼承自 IExecuteResult<T> 介面的物件，可將該組件方法的回傳型別改為 DefaultExecuteResult");
                return executeResult;
            }

            // 組件方法有參數的執行方法
            var parameterType = parameters[0].ParameterType;
            if (parameters.Count() > 1)
                throw new Exception($"組件方法 {methodName as object} 未依照設計規範，所執行的組件方法只能傳入一個參數");
            try
            {
                var parameter = JsonConvert.DeserializeObject(parameterByJson, parameterType);
                var executeResult1 = method.Invoke(assemblyInstance, new[] { parameter }) as IExecuteResult<T2>;
                if (executeResult1 == null)
                    throw new Exception(
                        $"組件方法 {methodName} 未依照設計規範回傳繼承自 IExecuteResult<T> 介面的物件，可將該組件方法的回傳型別改為 DefaultExecuteResult");
                return executeResult1;
            }
            catch (Exception ex)
            {
                throw new Exception($"JSON 參數反序列化錯誤，請檢查組件方法 {methodName} 所需參數型別應為: {parameterType}", ex);
            }
        }

        /// <summary>取得組件方法</summary>
        /// <typeparam name="T">組件類別</typeparam>
        /// <param name="methodName">組件方法的名稱</param>
        /// <returns></returns>
        private static MethodInfo GetMethod<T>(string methodName)
        {
            var method = typeof(T).GetMethod(methodName);
            if (method == null)
                throw new Exception($"沒有 '{methodName}' 這個組件方法");
            return method;
        }
    }
}
