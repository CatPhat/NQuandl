using System.Threading.Tasks;
using NQuandl.Client.Domain;

namespace NQuandl.Client.Api
{
    public interface IQuandlRestClient
    {
        Task<string> DoGetRequestAsync(QuandlRestClientRequestParameters parameters);
    }
}