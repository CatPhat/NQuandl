using System.Threading.Tasks;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Api
{
    public interface IQuandlJsonClient
    {
        Task<JsonDataResponse> GetAsync<TEntity>(DataRequestParameters requestParameters)
            where TEntity : QuandlEntity;

        Task<string> GetStringAsync<TEntity>(DataRequestParameters requestParameters) where TEntity : QuandlEntity;
    }
}