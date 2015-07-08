using System.Threading.Tasks;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Api
{
    public interface IQuandlRestClient
    {
        Task<string> DoGetRequestAsync(QuandlRestClientRequestParameters parameters);
    }
}