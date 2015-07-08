namespace NQuandl.Client.Interfaces
{
    public interface IHandleResponse<in TQuery, out TResult> where TQuery : IDefineResponse<TResult>
    {
        TResult Handle(TQuery query);
    }
}