namespace NQuandl.Client.Api.Transactions
{
    public interface IExecuteQuandlRequests
    {
        TResult Execute<TResult>(IDefineQuandlRequest<TResult> quandlRequest);
    }
}