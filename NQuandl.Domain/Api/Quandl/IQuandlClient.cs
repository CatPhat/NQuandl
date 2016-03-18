using System.Threading.Tasks;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Api.Quandl
{
    public interface IQuandlClient
    {
        Task<ResultStringWithQuandlResponseInfo> GetStringAsync(QuandlClientRequestParameters parameters);
        Task<ResultStreamWithQuandlResponseInfo> GetStreamAsync(QuandlClientRequestParameters parameters);

        Task<TResult> GetAsync<TResult>(
            QuandlClientRequestParameters parameters)
            where TResult : ResultWithQuandlResponseInfo;
    }
}