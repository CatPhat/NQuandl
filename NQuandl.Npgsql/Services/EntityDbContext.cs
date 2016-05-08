using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Services
{
    public class EntityDbContext<TEntity> : IWriteEntities<TEntity>, IReadEntities<TEntity> where TEntity : DbEntity
    {
        private readonly ISqlMapper _sqlMapper;
        private readonly IEntityObjectMapper<TEntity> _objectMapper;
        private readonly IDb _db;

        public EntityDbContext([NotNull] ISqlMapper sqlMapper, [NotNull] IEntityObjectMapper<TEntity> objectMapper,
            [NotNull] IDb db)
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

        public Task BulkWriteEntities(IObservable<TEntity> entities)
        {
   
            var insertData = _objectMapper.GetBulkInsertCommand(entities);
            _db.BulkWriteData(, insertData.DbDatasObservable)
        }

        public Task WriteEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(EntitiesReaderQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public IObservable<TEntity> GetObservable(EntitiesReaderQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }
    }
}