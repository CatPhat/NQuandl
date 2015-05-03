using System.Collections.Generic;
using NQuandl.Client.Entities;

namespace NQuandl.Client.Responses
{
    public class NQuandlResponse<TEntity> where TEntity : QuandlEntity
    {
        public IEnumerable<TEntity> Entities;
        public QuandlResponse Response;
    }
}