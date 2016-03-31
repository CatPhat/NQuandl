namespace NQuandl.Api.Transactions
{
    public interface IDefineQuandlRequest<TResult>
    {
        string Uri { get; }
    }
}