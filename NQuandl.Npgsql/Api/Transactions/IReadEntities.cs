using System;
using System.Threading.Tasks;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IReadEntities<TEntity> where TEntity : DbEntity
    {
        Task<TEntity> GetAsync(EntitiesReaderQuery<TEntity> query);
        IObservable<TEntity> GetObservable(EntitiesReaderQuery<TEntity> query);
    }
}