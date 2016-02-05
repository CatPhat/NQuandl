namespace NQuandl.Client.Api
{
    public interface IDefineQuandlQuery<TQuandlResult>
    {
        string ApiVersion { get; }
    }
}