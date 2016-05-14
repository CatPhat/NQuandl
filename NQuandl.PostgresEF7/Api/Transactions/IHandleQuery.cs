namespace NQuandl.PostgresEF7.Api.Transactions
{
    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IDefineQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}