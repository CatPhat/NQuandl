using System.Threading.Tasks;
using NQuandl.Client.Domain.Responses;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Requests;

namespace NQuandl.Client.Api
{
    public interface IQuandlJsonClient
    {
        Task<JsonResponseV1<TEntity>> GetAsync<TEntity>(QueryParametersV1 queryParameters)
            where TEntity : QuandlEntity;
    }
}