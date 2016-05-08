using System;
using System.Data;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;

namespace NQuandl.Npgsql.Api.Mappers
{
    public interface IEntityObjectMapper<TEntity> where TEntity : DbEntity {
        InsertDataCommand GetInsertCommand(TEntity entity);
        BulkInsertCommand GetBulkInsertCommand(IObservable<TEntity> entities);
        ReaderQuery GetReaderQuery(EntitiesReaderQuery<TEntity> query);
        IObservable<TEntity> GetEntityObservable(IObservable<IDataRecord> records);
    }
}