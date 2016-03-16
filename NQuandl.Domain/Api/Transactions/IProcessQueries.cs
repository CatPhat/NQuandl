namespace NQuandl.Api.Transactions
{
    public interface IProcessQueries
    {
        TResult Execute<TResult>(IDefineQuery<TResult> query);
    }
}