using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Api
{
    public interface IQuandlRestClient
    {
        Task<HttpResponseMessage> GetFullResponseAsync(QuandlRestClientRequestParameters parameters);
    }
}