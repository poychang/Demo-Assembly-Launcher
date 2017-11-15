namespace DynamicExecuteAssembly
{
    /// <summary>預設執行結果</summary>
    /// <typeparam name="T">回傳類別</typeparam>
    public class DefaultExecuteResult<T> : IExecuteResult<T>
    {
        /// <summary>是否執行成功</summary>
        public bool IsSuccess { get; set; }

        /// <summary>執行結果</summary>
        public T Data { get; set; }

        /// <summary>執行訊息</summary>
        public string Message { get; set; }
    }
}
