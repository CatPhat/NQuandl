using System.Threading.Tasks;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Api
{
    public interface IQuandlClient
    {
        Task<string> GetAsync(QuandlClientRequestParameters requestParameters);
    }
}