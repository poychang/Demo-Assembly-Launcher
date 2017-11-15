using DynamicExecuteAssembly;

namespace DemoAssemblyLauncher
{
    public class BizLagicObjcet
    {
        public IExecuteResult<object> GetString()
        {
            return new DefaultExecuteResult<object>()
            {
                Data = "This is string from BizLogic'a GetString",
                IsSuccess = true,
                Message = "Good Job"
            };
        }

        public IExecuteResult<object> GetStringWithParamter(string str)
        {
            return new DefaultExecuteResult<object>()
            {
                Data = $"This is string from BizLogic'a GetString with {str}",
                IsSuccess = true,
                Message = "Good Job"
            };
        }

        public IExecuteResult<object> GetStringWith2Paramter(string str1, string str2)
        {
            return new DefaultExecuteResult<object>()
            {
                Data = $"This is string from BizLogic'a GetString with {str1} and {str2}",
                IsSuccess = true,
                Message = "Good Job"
            };
        }
    }
}
