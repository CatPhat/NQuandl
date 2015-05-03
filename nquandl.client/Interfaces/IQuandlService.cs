using System.Threading.Tasks;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Interfaces
{
    public interface IQuandlService
    {
        Task<T> GetAsync<T>(IQuandlRequest request) where T : QuandlResponse;
        Task<string> GetStringAsync(IQuandlRequest request);
    }
}
