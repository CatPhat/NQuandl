using NQuandl.Client.Responses;

namespace NQuandl.Client.Interfaces
{
    public interface IReturn
    {
        string Url { get; }
    }

    public interface IReturn<TResponse> : IReturn where TResponse : QuandlResponse 
    {
       
    }
}