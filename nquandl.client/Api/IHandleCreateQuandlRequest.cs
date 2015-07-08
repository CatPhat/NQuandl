namespace NQuandl.Client.Api
{
    public interface IHandleCreateQuandlRequest<in TQuery, out TResult> where TQuery : IDefineCreateQuandlRequest<TResult>
    {
        TResult Handle(TQuery query);
    }
}