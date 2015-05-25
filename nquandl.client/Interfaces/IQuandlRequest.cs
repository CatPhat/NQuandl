using NQuandl.Client.URIs;

namespace NQuandl.Client.Interfaces
{
    public interface IQuandlRequest
    {
        IQuandlUri Uri { get; }
    }
}