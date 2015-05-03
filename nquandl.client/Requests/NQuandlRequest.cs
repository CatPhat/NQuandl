using System;
using NQuandl.Client.Entities;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Requests
{
    public interface IGetNQuandlRequest
    {
        NQuandlRequest<TEntity, TResponse> Get<TEntity, TResponse>()
            where TEntity : QuandlEntity
            where TResponse : QuandlResponse;

    }

    public interface INQuandlRequest<out TEntity, TResponse> 
        where TEntity : QuandlEntity
        where TResponse : QuandlResponse
    {
        OptionalRequestParameters Options { get; set; }
        RequestParameters Parameters { get; }
        IMapData<TEntity> Mapper { get; }
        IReturn<TResponse> QuandlRequest { get; }
    }

    public abstract class NQuandlRequest<TEntity, TResponse> : INQuandlRequest<TEntity, TResponse>
        where TEntity : QuandlEntity
        where TResponse : QuandlResponse
    {
        public OptionalRequestParameters Options { get; set; }
        public RequestParameters Parameters
        {
            get
            {
                return new RequestParameters
                {
                    QuandlCode = ((TEntity) Activator.CreateInstance(typeof (TEntity))).QuandlCode,
                    Options = Options
                };
            }
        }

        public abstract IMapData<TEntity> Mapper { get; }
        public abstract IReturn<TResponse> QuandlRequest { get; }
    }
}