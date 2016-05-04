using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class EntityWriter<TEntity> : IWriteEntities<TEntity> where TEntity : DbEntity
    {
        private readonly IExecuteRawSql _db;
        private readonly IEntityMetadata<TEntity> _metadata;
        private readonly IEntitySqlMapper<TEntity> _sql;


        public EntityWriter([NotNull] IEntitySqlMapper<TEntity> sql, [NotNull] IEntityMetadata<TEntity> metadata,
            [NotNull] IExecuteRawSql db
            )
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _sql = sql;
            _metadata = metadata;
            _db = db;
        }

        public async Task BulkWriteEntities(IObservable<TEntity> entities)
        {
            using (var importer = _db.GetBulkImporter(_sql.BulkInsertSql()))
            {
                await entities.ForEachAsync(entity =>
                {
                    importer.StartRow();
                    WriteByObjectParametersIfNotNull(importer, entity);
                });
                importer.Close();
            }
        }

        private void WriteByObjectParametersIfNotNull(NpgsqlBinaryImporter importer, TEntity entityWithData)
        {
            foreach (
                var keyValue in
                    _metadata.GetProperyNameDbMetadata().OrderBy(x => x.Value.ColumnIndex))
            {
                var data = _metadata.GetEntityValueByPropertyName(entityWithData, keyValue.Key);
                if (data == null)
                    continue;

                importer.Write(data, keyValue.Value.DbType);
            }
        }
    }
}