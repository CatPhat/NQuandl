﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NQuandl.Api.Persistence.Entities;

namespace NQuandl.Api.Persistence.Transactions
{
    public abstract class BaseEntityQuery<TEntity> where TEntity : Entity
    {
        public IEnumerable<Expression<Func<TEntity, object>>> EagerLoad { get; set; }
    }
}