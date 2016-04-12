using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NQuandl.PostgresEF7.Api.Entities;

namespace NQuandl.PostgresEF7.Api.Transactions
{
    public abstract class BaseEntitiesQuery<TEntity> where TEntity : Entity
    {
        public IEnumerable<Expression<Func<TEntity, object>>> EagerLoad { get; set; }
        public IDictionary<Expression<Func<TEntity, object>>, OrderByDirection> OrderBy { get; set; }
    }
}