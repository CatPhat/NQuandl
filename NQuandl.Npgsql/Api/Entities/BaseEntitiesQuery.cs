using System;
using System.Linq.Expressions;

namespace NQuandl.Npgsql.Api.Entities
{
    public abstract class BaseEntitiesQuery<TEntity> where TEntity : DbEntity
    {
        public Expression<Func<TEntity, object>> WhereColumn { get; protected set; }
        public string QueryByString { get; protected set; }
        public int? QueryByInt { get; protected set; }

        public Expression<Func<TEntity, object>> OrderByColumn { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}