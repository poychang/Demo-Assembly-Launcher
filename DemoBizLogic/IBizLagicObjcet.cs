using DynamicExecuteAssembly;

namespace DemoBizLogic
{
    public interface IBizLagicObjcet
    {
        IExecuteResult<object> GetString();

        IExecuteResult<object> GetStringWithParamter(BizLagicModel data);

        IExecuteResult<object> GetStringWith2Paramter(BizLagicModel data1, BizLagicModel data2);
    }
}
