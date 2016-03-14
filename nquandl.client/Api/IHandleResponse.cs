namespace NQuandl.Api
{
    public interface IHandleResponse<in TQuery, out TResult> where TQuery : IDefineResponse<TResult>
    {
        TResult Handle(TQuery query);
    }
}