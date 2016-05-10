using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class WriteEntity<TEntity> : IDefineCommand where TEntity : DbEntity
    {
        public WriteEntity(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; }
    }

    public class HandleWriteEntity<TEntity> : IHandleCommand<WriteEntity<TEntity>> where TEntity : DbEntity
    {
        private readonly IDb _db;
        private readonly IEntityObjectMapper<TEntity> _objectMapper;
        private readonly IEntitySqlMapper<TEntity> _sqlMapper;

        public HandleWriteEntity([NotNull] IEntitySqlMapper<TEntity> sqlMapper,
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

        public async Task Handle(WriteEntity<TEntity> command)
        {
            var dbImportDatas = _objectMapper.GetDbImportDatas(command.Entity).ToList();

            var sqlStatement = _sqlMapper.GetInsertSql(dbImportDatas);

            var parameters = dbImportDatas.Select(dbData => new NpgsqlParameter(dbData.ColumnName, dbData.DbType)
            {
                Value = dbData.Data,
                IsNullable = dbData.IsNullable
            }).ToArray();

            using (var connection = _db.CreateConnection())
            using (var cmd = new NpgsqlCommand(sqlStatement, connection))
            {
                await cmd.Connection.OpenAsync();
                cmd.Parameters.AddRange(parameters);
                cmd.Prepare();
                await cmd.ExecuteNonQueryAsync();
                cmd.Connection.Close();
            }
        }
    }
}