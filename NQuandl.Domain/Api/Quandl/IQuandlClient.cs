using System.Threading.Tasks;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Api.Quandl
{
    public interface IQuandlClient
    {
        Task<ResultStringWithQuandlResponseInfo> GetStringAsync(string uri);
        Task<ResultStreamWithQuandlResponseInfo> GetStreamAsync(string uri);

        Task<TResult> GetAsync<TResult>(string uri) where TResult : ResultWithQuandlResponseInfo;
    }
}