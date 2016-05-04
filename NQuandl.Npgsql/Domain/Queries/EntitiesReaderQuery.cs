using System;
using System.Linq.Expressions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class EntitiesReaderQuery<TEntity>
    {
        public EntitiesReaderQuery()
        {
            
        }

        public EntitiesReaderQuery(Expression<Func<TEntity, object>> where, string query)
        {
            QueryByString = query;
            Column = where;
        }

        public EntitiesReaderQuery(Expression<Func<TEntity, object>> where, int query)
        {
            QueryByInt = query;
            Column = where;
        }

        public Expression<Func<TEntity, object>> Column { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public string QueryByString { get; }
        public int? QueryByInt { get;  }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}