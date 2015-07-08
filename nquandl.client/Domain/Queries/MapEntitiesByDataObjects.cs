using System.Collections.Generic;
using System.Linq;
using NQuandl.Client.Api;
using NQuandl.Client.Entities.Base;

namespace NQuandl.Client.Domain.Queries
{
    public class MapToEntitiesByDataObjects<TEntity> : IDefineQuery<IEnumerable<TEntity>>
        where TEntity : QuandlEntity
    {
        public MapToEntitiesByDataObjects(object[][] dataObjects)
        {
            DataObjects = dataObjects;
        }

        public object[][] DataObjects { get; private set; }
    }

    public class HandleMapToEntitiesByDataObjects<TEntity> :
        IHandleQuery<MapToEntitiesByDataObjects<TEntity>, IEnumerable<TEntity>> where TEntity : QuandlEntity
    {
        private readonly IMapObjectToEntity<TEntity> _mapper;

        public HandleMapToEntitiesByDataObjects(IMapObjectToEntity<TEntity> mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<TEntity> Handle(MapToEntitiesByDataObjects<TEntity> query)
        {
            return query.DataObjects.Select(_mapper.MapEntity);
        }
    }
}