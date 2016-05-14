namespace NQuandl.Client.Api.Transactions
{
    public interface IDefineQuandlRequest<TResult>
    {
        string Uri { get; }
    }
}