using NQuandl.Client.Entities;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client.CompositionRoot
{
    internal sealed class RequestProcessor : IGetNQuandlRequest
    {
        public NQuandlRequest<TEntity, TResponse> Get<TEntity, TResponse>() 
            where TEntity : QuandlEntity
            where TResponse : QuandlResponse
        {
            var requestType = typeof (INQuandlRequest<,>).MakeGenericType(typeof (TEntity));
            return (NQuandlRequest<TEntity, TResponse>)Bootstapper.Container.GetInstance(requestType);
        }
    }
}