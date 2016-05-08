using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
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

        public ReaderQuery GetReaderQuery(EntitiesReaderQuery<TEntity> query)
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

        public InsertDataCommand GetInsertCommand(TEntity entity)
        {
            return new InsertDataCommand
            {
                TableName = _metadata.GetTableName(),
                DbDatas = GetDbDatas(entity)
            };
        }

        public BulkInsertCommand GetBulkInsertCommand(IObservable<TEntity> entities)
        {
            var columnNamesWithIndices = _metadata.GetPropertyInfos()
                .Select(propertyInfo => new ColumnNameWithIndex
            {
                ColumnName = _metadata.GetColumnName(propertyInfo.Name),
                ColumnIndex = _metadata.GetColumnIndex(propertyInfo.Name)
            });
            return new BulkInsertCommand
            {
                ColumnNameWithIndices = columnNamesWithIndices,
                DbDatasObservable = GetDbDatasObservable(entities)
            };
        }

        private TEntity CreateEntity(IDataRecord record)
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

        private IEnumerable<DbData> GetDbDatas(TEntity entity)
        {
            return from propertyInfo in _metadata.GetPropertyInfos()
                let data = GetEntityValueByPropertyName(entity, propertyInfo.Name)
                let propertyName = propertyInfo.Name
                select new DbData
                {
                    Data = data,
                    DbType = _metadata.GetNpgsqlDbType(propertyName),
                    ColumnName = _metadata.GetColumnName(propertyName),
                    ColumnIndex = _metadata.GetColumnIndex(propertyName),
                    IsNullable = _metadata.GetIsNullable(propertyName),
                    IsStoreGenerated = _metadata.GetIsStoreGenerated(propertyName)
                };
        }

        private IObservable<List<DbData>> GetDbDatasObservable(IObservable<TEntity> entities)
        {
            return Observable.Create<List<DbData>>(observer =>
                entities.Subscribe(entity => observer.OnNext(GetDbDatas(entity).OrderBy(x => x.ColumnIndex).ToList()),
                    onCompleted: observer.OnCompleted,
                    onError: ex => { throw new Exception(ex.Message); }));
        }

        private object GetEntityValueByPropertyName(TEntity entityWithData, string propertyName)
        {
            return _metadata.GetPropertyInfo(propertyName).GetValue(entityWithData, new object[] {});
        }
        
    }
}