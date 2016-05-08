using System;
using System.Linq.Expressions;

namespace NQuandl.Npgsql.Api.Entities
{
    public class EntitiesReaderQuery<TEntity>
    {
        public EntitiesReaderQuery()
        {
            
        }

        public EntitiesReaderQuery(Expression<Func<TEntity, object>> where, string query)
        {
            QueryByString = query;
            WhereColumn = where;
        }

        public EntitiesReaderQuery(Expression<Func<TEntity, object>> where, int query)
        {
            QueryByInt = query;
            WhereColumn = where;
        }

        public Expression<Func<TEntity, object>> WhereColumn { get; }
        public Expression<Func<TEntity, object>> OrderByColumn { get; set; }
        public string QueryByString { get; }
        public int? QueryByInt { get;  }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }

    public class ReaderQuery
    {
    
        public string TableName { get; set; }
        public string WhereColumn { get; set; }
        public string OrderByColumn { get; set;}
        public string QueryByString { get; set; }
        public int? QueryByInt { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string[] ColumnNames { get; set; }
    }
}