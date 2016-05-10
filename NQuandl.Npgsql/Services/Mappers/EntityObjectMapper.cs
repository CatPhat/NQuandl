using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using JetBrains.Annotations;
using NpgsqlTypes;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Extensions;
using NQuandl.Npgsql.Services.Metadata;


namespace NQuandl.Npgsql.Services.Mappers
{
    public class EntityObjectMapper<TEntity> : IEntityObjectMapper<TEntity> where TEntity : DbEntity
    {
        private readonly IEntityMetadataCache<TEntity> _metadata;

        public EntityObjectMapper([NotNull] IEntityMetadataCache<TEntity> metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            _metadata = metadata;
        }

        public IObservable<TEntity> GetEntityObservable(IObservable<IDataRecord> records)
        {
            return Observable.Create<TEntity>(
                obs => records.Subscribe(
                    record => obs.OnNext(CreateEntity(record)), onCompleted: obs.OnCompleted, onError:
                        exception => { throw new Exception(exception.Message); }));
        }


        public DataRecordsObservableBy GetDataRecordsObservableQuery<TQuery>(TQuery query) where TQuery : BaseEntitiesQuery<TEntity>
        {
            DataRecordsObservableBy recordsQuery;
            var tableName = _metadata.GetTableName();
            var whereColumn = _metadata.GetPropertyName(query.WhereColumn);
            var columnNames = GetOrderedColumnsStrings();
            if (query.QueryByInt.HasValue)
            {
                recordsQuery = new DataRecordsObservableBy(tableName, whereColumn, columnNames, query.QueryByInt.Value);
            }
            else if (string.IsNullOrEmpty(query.QueryByString))
            {
                recordsQuery = new DataRecordsObservableBy(tableName, whereColumn, columnNames, query.QueryByString);
            }
            else
            {
                recordsQuery = new DataRecordsObservableBy(tableName, columnNames);
            }

            recordsQuery.Limit = query.Limit;
            recordsQuery.Offset = query.Offset;

            return recordsQuery;
        }

        private string[] GetOrderedColumnsStrings()
        {
            return _metadata.ToColumnNameWithIndices().Select(x => x.ColumnName).ToArray();
        }

     

        public ReaderQuery GetReaderQuery<TQuery>(TQuery query) where TQuery : BaseEntitiesQuery<TEntity>
        {
            var whereColumnPropertyName = _metadata.GetPropertyName(query.WhereColumn);
            var orderByPropertyName = _metadata.GetPropertyName(query.OrderByColumn);
            var properties = _metadata.GetPropertyInfos();
            var columnNames = properties.Select(propertyInfo => _metadata.GetColumnName(propertyInfo.Name)).ToArray();
            return new ReaderQuery
            {
                TableName = _metadata.GetTableName(),
                WhereColumn = _metadata.GetColumnName(whereColumnPropertyName),
                OrderByColumn = _metadata.GetColumnName(orderByPropertyName),
                Limit = query.Limit,
                Offset = query.Offset,
                QueryByInt = query.QueryByInt,
                QueryByString = query.QueryByString,
                ColumnNames = columnNames
            };
        }
        
        

        public TEntity CreateEntity(IDataRecord record)
        {
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity), new object[] { });
            var properties = _metadata.GetPropertyInfos();

            foreach (var propertyInfo in properties)
            {
                var columnIndex = _metadata.GetColumnIndex(propertyInfo.Name);
                var recordValue = record[columnIndex];
                if (recordValue != DBNull.Value)
                {
                    propertyInfo.SetValue(entity, recordValue);
                }
            }
            return entity;
        }

        public IEnumerable<DbImportData> GetDbImportDatas(TEntity entity)
        {
            return from propertyInfo in _metadata.GetPropertyInfos()
                let data = GetEntityPropertyValue(entity, propertyInfo.Name)
                let propertyName = propertyInfo.Name
                select new DbImportData
                {
                    Data = data,
                    DbType = _metadata.GetNpgsqlDbType(propertyName),
                    ColumnName = _metadata.GetColumnName(propertyName),
                    ColumnIndex = _metadata.GetColumnIndex(propertyName),
                    IsNullable = _metadata.GetIsNullable(propertyName),
                    IsStoreGenerated = _metadata.GetIsStoreGenerated(propertyName)
                };
        }

      

        public IObservable<List<DbImportData>> GetDbImportDatasObservable(IEnumerable<TEntity> entities)
        {
            return GetDbImportDatasObservable(entities.ToObservable());
        }

        public IObservable<List<DbImportData>> GetDbImportDatasObservable(IObservable<TEntity> entities)
        {
            return Observable.Create<List<DbImportData>>(observer =>
                entities.Subscribe(entity => observer.OnNext(GetDbImportDatas(entity).OrderBy(x => x.ColumnIndex).ToList()),
                    onCompleted: observer.OnCompleted,
                    onError: ex => { throw new Exception(ex.Message); }));
        }

        public object GetEntityPropertyValue(TEntity entityWithData, string propertyName)
        {
            return _metadata.GetPropertyInfo(propertyName).GetValue(entityWithData, new object[] {});
        }
        
    }
}