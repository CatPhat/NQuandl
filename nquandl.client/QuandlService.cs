using System;
using System.Threading.Tasks;
using NQuandl.Client.Entities;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client
{
    public class QuandlService : IQuandlService
    {
        public async Task<T> GetAsync<T>(IQuandlRequest request) where T : QuandlResponse
        {
            string response = await GetStringAsync(request);
            return await response.DeserializeToObjectAsync<T>();
        }

        public async Task<string> GetStringAsync(IQuandlRequest request)
        {
            return await new WebClientHttpConsumer().DownloadStringAsync(request.Url);
        }

        public async Task<NQuandlResponse<TEntity>> GetAsync<TEntity>(OptionalRequestParameters options = null)
            where TEntity : QuandlEntity
        {
            var entity = (TEntity) Activator.CreateInstance(typeof (TEntity));
            var request = new QuandlEntityRequest<TEntity>(entity, options);
            QuandlResponseV1 response = await GetAsync<QuandlResponseV1>(request);
            var mapper = new NQuandlResponseProcessor();
            var nquandlResponse = new NQuandlResponse<TEntity>
            {
                Response = response,
                Entities = mapper.MapEntities<TEntity>(response.Data)
            };

            return nquandlResponse;
        }
    }
}