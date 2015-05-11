using System.Threading.Tasks;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Interfaces
{
    public interface IQuandlJsonService
    {
        Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            RequestOptionsV1 options = null)
            where TEntity : QuandlEntity, new();

        Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            IDeserializedEntityRequest<TEntity> request)
            where TEntity : QuandlEntity;

        Task<DeserializedJsonResponse<TResponse>> GetAsync<TResponse>(IQuandlJsonRequest<TResponse> request)
            where TResponse : JsonResponse;

        Task<string> GetStringAsync(IQuandlRequest request);
    }
}