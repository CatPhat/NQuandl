using System.Threading.Tasks;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Api
{
    public interface IQuandlClient
    {
        Task<RawHttpContent> GetFullResponseAsync(QuandlClientRequestParameters parameters);
    }
}