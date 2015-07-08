//using NQuandl.Client.Entities.Base;
//using NQuandl.Client.Responses;

//namespace NQuandl.Client.Interfaces
//{
//    public abstract class NQuandlRequest<TEntity, TResponse> : INQuandlRequest<TEntity, TResponse>
//        where TEntity : QuandlEntity
//        where TResponse : QuandlResponse
//    {
//        public OptionalRequestParameters Options { get; set; }
//        public RequestParameters Parameters
//        {
//            get
//            {
//                return new RequestParameters
//                {
//                    QuandlCode = ((TEntity)Activator.CreateInstance(typeof(TEntity))).QuandlCode,
//                    Options = Options
//                };
//            }
//        }

//        public abstract IMapData<TEntity> Mapper { get; }
//        public abstract IReturn<TResponse> QuandlRequest { get; }
//    }


//    public interface IQuandlJsonRequest<TResponse> : IQuandlRequest where TResponse : JsonResponse
//    {
//    }

//    public interface IDeserializedEntityRequest<TEntity> : IQuandlRequest
//        where TEntity : QuandlEntity
//    {
//        IMapData<TEntity> Mapper { get; set; }
//    }

//    public interface IDeserializedEntityRequestHandler<out TEntity> : IQuandlRequest
//      where TEntity : QuandlEntity
//    {
//        IMapData<TEntity> Handle { get; }
//    }
//}