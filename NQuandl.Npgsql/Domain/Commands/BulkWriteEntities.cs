using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class BulkWriteEntities<TEntity> : IDefineCommand where TEntity : DbEntity
    {
        public IEnumerable<TEntity> EntitiesEnumerable { get; private set; }
        public IObservable<TEntity> EntitiesObservable { get; private set; }

        public BulkWriteEntities(IObservable<TEntity> entitiesObservable)
        {
            EntitiesObservable = entitiesObservable;
        }

        public BulkWriteEntities(IEnumerable<TEntity> entitiesEnumerable)
        {
            EntitiesEnumerable = entitiesEnumerable;
        }
    }

    public class HandleBulkWriteEntities<TEntity> : IHandleCommand<BulkWriteEntities<TEntity>> where TEntity : DbEntity
    {
        private readonly IEntitySqlMapper<TEntity> _sqlMapper;
        private readonly IEntityObjectMapper<TEntity> _objectMapper;
        private readonly IDb _db;

        public HandleBulkWriteEntities([NotNull] IEntitySqlMapper<TEntity> sqlMapper,
            [NotNull] IEntityObjectMapper<TEntity> objectMapper, [NotNull] IDb db)
        {
            if (sqlMapper == null)
                throw new ArgumentNullException(nameof(sqlMapper));
            if (objectMapper == null)
                throw new ArgumentNullException(nameof(objectMapper));
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            _sqlMapper = sqlMapper;
            _objectMapper = objectMapper;
            _db = db;
        }

        public async Task Handle(BulkWriteEntities<TEntity> command)
        {
            IObservable<List<DbImportData>> dbDatas;
            if (command.EntitiesEnumerable != null && command.EntitiesEnumerable.Any())
            {
                dbDatas = _objectMapper.GetDbImportDatasObservable(command.EntitiesEnumerable);
            }
            else
            {
                dbDatas = _objectMapper.GetDbImportDatasObservable(command.EntitiesObservable);
            }

            var sqlStatement = _sqlMapper.GetBulkInsertSql();

            using (var connection = _db.CreateConnection())
            using (var importer = connection.BeginBinaryImport(sqlStatement))
            {
                await dbDatas.ForEachAsync(importData =>
                {
                    importer.StartRow();
                    foreach (var bulkImportData in importData.OrderBy(x => x.ColumnIndex))
                    {
                        importer.Write(bulkImportData, bulkImportData.DbType);
                    }
                });
                importer.Close();
            }
        }
    }
}
