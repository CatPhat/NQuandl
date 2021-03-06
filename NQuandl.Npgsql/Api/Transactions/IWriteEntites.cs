﻿using System;
using System.Threading.Tasks;
using NQuandl.Npgsql.Api.Entities;

namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IWriteEntities<in TEntity> where TEntity : DbEntity
    {
        Task BulkWriteEntities(IObservable<TEntity> entities);
        Task WriteEntity(TEntity entity);
    }

  
}