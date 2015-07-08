using System.Collections.Generic;
using System.Linq;
using NQuandl.Client.Api;
using NQuandl.Client.Entities.Base;

namespace NQuandl.Client.Domain.Queries
{
    public class MapEntitiesByDataObjects<TEntity> : IDefineQuery<IEnumerable<TEntity>>
        where TEntity : QuandlEntity
    {
        public MapEntitiesByDataObjects(object[][] dataObjects)
        {
            DataObjects = dataObjects;
        }

        public object[][] DataObjects { get; private set; }
    }

    public class HandleMapEntitiesByDataObjects<TEntity> :
        IHandleQuery<MapEntitiesByDataObjects<TEntity>, IEnumerable<TEntity>> where TEntity : QuandlEntity
    {
        private readonly IMapObjectToEntity<TEntity> _mapper;

        public HandleMapEntitiesByDataObjects(IMapObjectToEntity<TEntity> mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<TEntity> Handle(MapEntitiesByDataObjects<TEntity> query)
        {
            return query.DataObjects.Select(_mapper.MapEntity);
        }
    }
}