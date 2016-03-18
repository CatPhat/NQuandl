using System.Threading.Tasks;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Api.Quandl
{
    public interface IHttpClient
    {
        Task<HttpClientResponse> GetAsync(string requestUri);
    }
}