using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NQuandl.Domain.Persistence.Api.Transactions
{
    public abstract class BaseEnumerableQuery<T>
    {
        public IDictionary<Expression<Func<T, object>>, OrderByDirection> OrderBy { get; set; }
    }
}