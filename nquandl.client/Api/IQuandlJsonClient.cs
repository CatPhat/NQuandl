using System.Threading.Tasks;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Api
{
    public interface IQuandlJsonClient
    {
        Task<JsonDatasetResponse<TEntity>> GetAsync<TEntity>(OptionalDataRequestParameters requestParameters = null)
            where TEntity : QuandlEntity;
    }
}