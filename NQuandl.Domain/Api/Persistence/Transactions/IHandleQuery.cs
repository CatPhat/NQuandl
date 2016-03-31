namespace NQuandl.Api.Persistence.Transactions
{
    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IDefineQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}