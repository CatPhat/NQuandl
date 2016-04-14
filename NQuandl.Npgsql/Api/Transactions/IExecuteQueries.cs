namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IExecuteQueries
    {
        TResult Execute<TResult>(IDefineQuery<TResult> query);
    }
}