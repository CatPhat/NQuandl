using System.Collections.Generic;
using NQuandl.Client.Api;

namespace NQuandl.Client._OLD.Responses
{
    public class DeserializedJsonResponseV1<TEntity> : IDefineResponse<DeserializedJsonResponseV1<TEntity>>
    {
        public JsonResponseV1 DeserializedJson { get; set; }
        public IEnumerable<TEntity> Entities { get; set; }
    }
}