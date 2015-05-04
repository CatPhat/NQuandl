using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Interfaces
{
    public interface IReturn
    {
        string Uri { get; }
    }

    public interface IReturn<TResponse> : IReturn where TResponse : QuandlResponse 
    {
       
    }
}