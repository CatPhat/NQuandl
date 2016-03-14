namespace NQuandl.Api
{
    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IDefineQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}