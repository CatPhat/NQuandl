using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Mappers;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class EntityWriter<TEntity> : IWriteEntities<TEntity> where TEntity : DbEntity
    {
        private readonly IProvideConnection _connection;
        private readonly IEntityMetadata<TEntity> _metadata;
        private readonly IEntitySqlMapper<TEntity> _sql;


        public EntityWriter([NotNull] IEntitySqlMapper<TEntity> sql, [NotNull] IEntityMetadata<TEntity> metadata,
            [NotNull] IProvideConnection connection)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));


            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            _sql = sql;

            _metadata = metadata;
            _connection = connection;
        }

        public async Task BulkWriteEntities(IObservable<TEntity> entities)
        {
            using (var importer = GetBulkImporter(_sql.BulkInsertSql()))
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

        private NpgsqlBinaryImporter GetBulkImporter(string sqlStatement)
        {
            var connection = _connection.CreateConnection();
            return connection.BeginBinaryImport(sqlStatement);
        }
    }
}