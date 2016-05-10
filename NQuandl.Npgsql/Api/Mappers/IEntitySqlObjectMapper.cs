using System;
using System.Collections.Generic;
using System.Data;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Queries;

namespace NQuandl.Npgsql.Api.Mappers
{
    public interface IEntityObjectMapper<TEntity> where TEntity : DbEntity {
        IObservable<TEntity> GetEntityObservable(IObservable<IDataRecord> records);
        ReaderQuery GetReaderQuery<TQuery>(TQuery query) where TQuery : BaseEntitiesQuery<TEntity>;
        TEntity CreateEntity(IDataRecord record);
        IEnumerable<DbImportData> GetDbImportDatas(TEntity entity);
        IObservable<List<DbImportData>> GetDbImportDatasObservable(IEnumerable<TEntity> entities);
        IObservable<List<DbImportData>> GetDbImportDatasObservable(IObservable<TEntity> entities);
        object GetEntityPropertyValue(TEntity entityWithData, string propertyName);
        DataRecordsObservableBy GetDataRecordsObservableQuery<TQuery>(TQuery query) where TQuery : BaseEntitiesQuery<TEntity>;
    }
}