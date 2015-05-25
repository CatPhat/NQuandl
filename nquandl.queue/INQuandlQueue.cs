using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Queue
{
    public interface INQuandlQueue
    {
        TransformBlock<IQuandlRequest, IQuandlRequest> Queue { get; }
        BroadcastBlock<string> BroadcastBlock { get; }
        Task<string> GetStringAsync(IQuandlRequest request);
        Task<IEnumerable<string>> GetStringsAsync(IEnumerable<IQuandlRequest> requests);

        Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            QueryParametersV1 options = null)
            where TEntity : QuandlEntity, new();

        Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            IDeserializedEntityRequest<TEntity> request)
            where TEntity : QuandlEntity;

        Task<IEnumerable<DeserializedEntityResponse<TEntity>>> GetAsync<TEntity>(
            List<QueueRequest<TEntity>> requests) where TEntity : QuandlEntity, new();
    }
}