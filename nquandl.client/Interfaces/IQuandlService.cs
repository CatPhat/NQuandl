using System.Threading.Tasks;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Interfaces
{
    public interface IQuandlService
    {
        Task<TResponse> GetAsync<TResponse>(IReturn<TResponse> request) where TResponse : QuandlResponse;
        Task<string> GetStringAsync(string url);
    }
}
