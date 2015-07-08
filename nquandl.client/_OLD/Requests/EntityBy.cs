using System.Threading.Tasks;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;

namespace NQuandl.Client.Requests
{
    public class EntityBy<TEntity> : IDefineQuandlRequest<Task<TEntity>> where TEntity : QuandlEntity
    {
        public EntityBy(QuandlRequest request)
        {
            Request = request;
        }

        public QuandlRequest Request { get; private set; }
    }

    public class HandleEntityByRequest<TEntity> : IHandleQuandlRequest<EntityBy<TEntity>, Task<TEntity>> where TEntity : QuandlEntity
    {
        private readonly IQuandlService _quandlService;
        private readonly IMapJsonToEntity<TEntity> _mapper; 
        public HandleEntityByRequest(IQuandlService service, IMapJsonToEntity<TEntity> mapper)
        {
            _quandlService = service;
            _mapper = mapper;
        }

        public async Task<TEntity> Handle(EntityBy<TEntity> query)
        {
            var response = await _quandlService.GetStringAsync(query.Request);
            return new 
        }
    }
}