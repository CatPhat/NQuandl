using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Domain.RequestParameters;

namespace NQuandl.Api
{
    public interface IQuandlClient
    {
        Task<HttpResponseMessage> GetFullResponseAsync(QuandlClientRequestParameters parameters);
    }
}