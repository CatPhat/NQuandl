using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NQuandl.Domain.Persistence.Api.Entities;

namespace NQuandl.Domain.Persistence.Api.Transactions
{
    public abstract class BaseEntityQuery<TEntity> where TEntity : Entity
    {
        public IEnumerable<Expression<Func<TEntity, object>>> EagerLoad { get; set; }
    }
}