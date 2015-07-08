using System.Threading.Tasks;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Interfaces
{
    public interface IQuandlJsonService
    {
        Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            QueryParametersV1 options = null)
            where TEntity : QuandlEntity, new();
    }
}