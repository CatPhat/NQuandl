namespace NQuandl.Api.Persistence.Transactions
{
    public interface IExecuteQueries
    {
        TResult Execute<TResult>(IDefineQuery<TResult> query);
    }
}