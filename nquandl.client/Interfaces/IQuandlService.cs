using System.Threading.Tasks;

namespace NQuandl.Client
{
    public interface IQuandlService
    {
        Task<T> GetAsync<T>(BaseQuandlRequest<T> request) where T : QuandlResponse;
        Task<string> GetStringAsync<T>(BaseQuandlRequest<T> request) where T : QuandlResponse;
    }
}
