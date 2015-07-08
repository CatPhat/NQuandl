using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client._OLD.Requests;

namespace NQuandl.Client._OLD.Interfaces.old
{
    public interface IQuandlJsonService
    {
        Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            QueryParametersV1 options = null)
            where TEntity : QuandlEntity, new();
    }
}