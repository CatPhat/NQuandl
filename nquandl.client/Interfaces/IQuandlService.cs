using System.Threading.Tasks;
using NQuandl.Client.Models;

namespace NQuandl.Client
{
    public interface IQuandlService
    {
        Task<T> GetAsync<T>(IQuandlRequest<T> request) where T : QuandlResponse;
        Task<string> GetStringAsync<T>(IQuandlRequest<T> request) where T : QuandlResponse;
    }
}
