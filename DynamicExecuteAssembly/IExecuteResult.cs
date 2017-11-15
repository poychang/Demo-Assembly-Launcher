namespace DynamicExecuteAssembly
{
    /// <summary>執行結果介面</summary>
    /// <typeparam name="T">回傳類別</typeparam>
    public interface IExecuteResult<T>
    {
        /// <summary>是否執行成功</summary>
        bool IsSuccess { get; set; }

        /// <summary>執行結果</summary>
        T Data { get; set; }

        /// <summary>執行訊息</summary>
        string Message { get; set; }
    }
}
