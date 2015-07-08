using System.Threading.Tasks;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Api
{
    public interface IQuandlJsonClient
    {
        Task<JsonResponseV1<TEntity>> GetAsync<TEntity>(RequestParametersV1 requestParameters)
            where TEntity : QuandlEntity;
    }
}