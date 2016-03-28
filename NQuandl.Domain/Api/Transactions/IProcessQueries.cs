namespace NQuandl.Api.Transactions
{
    public interface IProcessQueries
    {
        TResult Execute<TResult>(IDefineQuandlRequest<TResult> quandlRequest);
    }
}