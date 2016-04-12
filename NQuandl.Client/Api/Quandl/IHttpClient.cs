using System.Threading.Tasks;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Api.Quandl
{
    public interface IHttpClient
    {
        Task<HttpClientResponse> GetAsync(string requestUri);
    }
}