using System.Threading.Tasks;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Api
{
    public interface IQuandlJsonClient
    {
        Task<JsonResponseV1<TEntity>> GetAsync<TEntity>(RequestParameters requestParameters)
            where TEntity : QuandlEntity;

        Task<string> GetStringAsync<TEntity>(RequestParameters requestParameters) where TEntity : QuandlEntity;
    }
}