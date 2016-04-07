namespace NQuandl.Domain.Persistence.Api.Transactions
{
    public interface IExecuteQueries
    {
        TResult Execute<TResult>(IDefineQuery<TResult> query);
    }
}