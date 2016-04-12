using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NQuandl.PostgresEF7.Api.Transactions
{
    public abstract class BaseEnumerableQuery<T>
    {
        public IDictionary<Expression<Func<T, object>>, OrderByDirection> OrderBy { get; set; }
    }
}