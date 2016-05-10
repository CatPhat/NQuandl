using System;
using System.Linq.Expressions;
using NQuandl.Npgsql.Api.Entities;

namespace NQuandl.Npgsql.Api.Transactions
{
    public abstract class BaseEntitiesQuery<TEntity> where TEntity : DbEntity
    {
        protected BaseEntitiesQuery() {}

        protected BaseEntitiesQuery(Expression<Func<TEntity, object>> whereColumn, string query)
        {
            QueryByString = query;
            WhereColumn = whereColumn;
        }

        protected BaseEntitiesQuery(Expression<Func<TEntity, object>> whereColumn, int query)
        {
            QueryByInt = query;
            WhereColumn = whereColumn;
        }

        public Expression<Func<TEntity, object>> WhereColumn { get; protected set; }
        public string QueryByString { get; protected set; }
        public int? QueryByInt { get; protected set; }

        public Expression<Func<TEntity, object>> OrderByColumn { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}