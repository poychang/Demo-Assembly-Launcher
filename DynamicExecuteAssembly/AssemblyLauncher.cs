using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DynamicExecuteAssembly
{
    public class AssemblyLauncher
    {
        /// <summary>執行指定組件方法</summary>
        /// <typeparam name="TInstance">組件型別</typeparam>
        /// <typeparam name="TResult">回傳型別</typeparam>
        /// <param name="assemblyInstance">組件執行個體</param>
        /// <param name="methodName">組件方法的名稱</param>
        /// <param name="parameterByJson">組件方法的參數，請使用 JSON 格式</param>
        /// <returns></returns>
        public IExecuteResult<TResult> Execute<TInstance, TResult>(TInstance assemblyInstance, string methodName, string parameterByJson = "")
        {
            return GetMethod<TInstance>(methodName).GetParameters().Any() ?
                ExecuteMethodWithParamter<TInstance, TResult>(assemblyInstance, methodName, parameterByJson) :
                ExecuteMethod<TInstance, TResult>(assemblyInstance, methodName);
        }

        /// <summary>取得組件方法</summary>
        /// <typeparam name="TInstance">組件型別</typeparam>
        /// <param name="methodName">組件方法的名稱</param>
        /// <returns></returns>
        private static MethodBase GetMethod<TInstance>(string methodName)
        {
            var method = typeof(TInstance).GetMethod(methodName);
            if (method == null)
                throw new Exception($"{typeof(TInstance)} 沒有 {methodName} 這個組件方法");
            return method;
        }

        /// <summary>組件方法無參數的執行方法</summary>
        /// <typeparam name="TInstance">組件型別</typeparam>
        /// <typeparam name="TResult">回傳型別</typeparam>
        /// <param name="assemblyInstance">組件執行個體</param>
        /// <param name="methodName">組件方法的名稱</param>
        /// <returns></returns>
        private static IExecuteResult<TResult> ExecuteMethod<TInstance, TResult>(TInstance assemblyInstance, string methodName)
        {
            var method = GetMethod<TInstance>(methodName);
            var executeResult = method.Invoke(assemblyInstance, new object[0]) as IExecuteResult<TResult>;
            if (executeResult == null)
                throw new Exception(
                    $"依照設計規範 {typeof(TInstance)} 的 {method.Name} 組件方法必須回傳繼承自 IExecuteResult<T> 介面的物件，可將該組件方法的回傳型別改為 DefaultExecuteResult");
            return executeResult;
        }

        /// <summary>組件方法有參數的執行方法</summary>
        /// <typeparam name="TInstance">組件型別</typeparam>
        /// <typeparam name="TResult">回傳型別</typeparam>
        /// <param name="assemblyInstance">組件執行個體</param>
        /// <param name="methodName">組件方法的名稱</param>
        /// <param name="parameterByJson">組件方法的參數，請使用 JSON 格式</param>
        /// <returns></returns>
        private static IExecuteResult<TResult> ExecuteMethodWithParamter<TInstance, TResult>(TInstance assemblyInstance, string methodName, string parameterByJson)
        {
            var method = GetMethod<TInstance>(methodName);
            var parameters = method.GetParameters();
            var parameterType = parameters[0].ParameterType;
            if (parameters.Count() > 1)
                throw new Exception($"依照設計規範 {typeof(TInstance)} 的 {method.Name} 組件方法只能傳入一個參數");

            object parameter;
            try
            {
                parameter = JsonConvert.DeserializeObject(parameterByJson, parameterType);
            }
            catch (Exception ex)
            {
                throw new Exception($"JSON 參數反序列化錯誤，{typeof(TInstance)} 的 {method.Name} 組件方法所需參數型別應為: {parameterType}", ex);
            }

            var executeResult = method.Invoke(assemblyInstance, new[] { parameter }) as IExecuteResult<TResult>;
            if (executeResult == null)
                throw new Exception(
                    $"依照設計規範 {typeof(TInstance)} 的 {method.Name} 組件方法必須回傳繼承自 IExecuteResult<T> 介面的物件，可將該組件方法的回傳型別改為 DefaultExecuteResult");
            return executeResult;
        }
    }
}
