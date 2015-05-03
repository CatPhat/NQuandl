using System.Collections.Generic;
using System.Linq;
using NQuandl.Client.Entities;

namespace NQuandl.Client.CompositionRoot
{
    public class NQuandlResponseProcessor
    {
        private readonly IMapProcessor _mapper;

        public NQuandlResponseProcessor()
        {
            _mapper = Bootstapper.Container.GetInstance<IMapProcessor>();
        }

        public IEnumerable<TEntity> MapEntities<TEntity>(object[][] objects) where TEntity : QuandlEntity
        {
            return objects.Select(_mapper.Process<TEntity>);
        }
    }
}