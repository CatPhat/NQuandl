using System.Threading.Tasks;
using NQuandl.Domain.RequestParameters;
using NQuandl.Domain.Responses;

namespace NQuandl.Api
{
    public interface IQuandlClient
    {
        Task<RawHttpContent> GetFullResponseAsync(QuandlClientRequestParameters parameters);
    }
}