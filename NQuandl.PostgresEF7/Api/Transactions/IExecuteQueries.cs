namespace NQuandl.PostgresEF7.Api.Transactions
{
    public interface IExecuteQueries
    {
        TResult Execute<TResult>(IDefineQuery<TResult> query);
    }
}