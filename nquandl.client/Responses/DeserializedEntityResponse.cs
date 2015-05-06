using System.Collections.Generic;
using System.Linq;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;

namespace NQuandl.Client.Responses
{
    public class DeserializedEntityResponse<TEntity> : DeserializedJsonResponse<JsonResponseV1>
        where TEntity : QuandlEntity
    {
        private readonly IMapData<TEntity> _mapper;

        public DeserializedEntityResponse(string response, IMapData<TEntity> mapper)
            : base(response)
        {
            _mapper = mapper;
        }

        public IEnumerable<TEntity> Entities
        {
            get { return base.JsonResponse.Data.Select(_mapper.Map); }
        }
    }
}