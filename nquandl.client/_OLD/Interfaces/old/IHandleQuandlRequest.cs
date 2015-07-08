namespace NQuandl.Client._OLD.Interfaces.old
{
    public interface IHandleQuandlRequest<in TQuery, out TResult> where TQuery : IDefineQuandlRequest<TResult>
    {
        TResult Handle(TQuery query);
    }
}