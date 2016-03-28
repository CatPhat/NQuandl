namespace NQuandl.Api.Transactions
{
    public interface IHandleQuandlRequest<in TQuery, out TResult> where TQuery : IDefineQuandlRequest<TResult>
    {
        TResult Handle(TQuery query);
    }
}