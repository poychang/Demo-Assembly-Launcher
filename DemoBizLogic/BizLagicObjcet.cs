using DynamicExecuteAssembly;

namespace DemoBizLogic
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

        public IExecuteResult<object> GetStringWithParamter(BizLagicModel data)
        {
            return new DefaultExecuteResult<object>()
            {
                Data = $"This is string from BizLogic'a GetString with {data.Name}",
                IsSuccess = true,
                Message = "Good Job"
            };
        }

        public IExecuteResult<object> GetStringWith2Paramter(BizLagicModel data1, BizLagicModel data2)
        {
            return new DefaultExecuteResult<object>()
            {
                Data = $"This is string from BizLogic'a GetString with {data1.Name} and {data2.Name}",
                IsSuccess = true,
                Message = "Good Job"
            };
        }
    }

    public class BizLagicModel
    {
        public string Name { get; set; }
    }
}
