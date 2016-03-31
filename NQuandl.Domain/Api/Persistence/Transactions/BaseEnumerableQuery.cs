using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NQuandl.Api.Transactions;

namespace NQuandl.Api.Persistence.Transactions
{
    public abstract class BaseEnumerableQuery<T>
    {
        public IDictionary<Expression<Func<T, object>>, OrderByDirection> OrderBy { get; set; }
    }
}