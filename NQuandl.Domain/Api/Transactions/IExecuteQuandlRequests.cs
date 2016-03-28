namespace NQuandl.Api.Transactions
{
    public interface IExecuteQuandlRequests
    {
        TResult Execute<TResult>(IDefineQuandlRequest<TResult> quandlRequest);
    }
}