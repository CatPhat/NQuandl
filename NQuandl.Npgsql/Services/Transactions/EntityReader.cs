using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Mappers;

namespace NQuandl.Npgsql.Services.Transactions
{
    public interface IReadEntities
    {
        Task<TEntity> GetAsync<TEntity>();
        TEntity Get<TEntity>();
        IObservable<TEntity> GetObservable<TEntity>(EntitiesReaderQuery<TEntity> query);
        IEnumerable<TEntity> GetEnumerable<TEntity>(EntitiesReaderQuery<TEntity> query);
    }

    public interface IReadEntities<TEntity> where TEntity : DbEntity
    {
        Task<TEntity> GetAsync();
        TEntity Get();
        IObservable<TEntity> GetObservable(EntitiesReaderQuery<TEntity> query);
        IEnumerable<TEntity> GetEnumerable(EntitiesReaderQuery<TEntity> query);
    }

    public class EntitiesReaderQuery<TEntity>
    {
        public Expression<Func<TEntity, object>> Column { get; set; }
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public string QueryByString { get; set; }
        public int? QueryByInt { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }

    public class EntityReader<TEntity> : IReadEntities<TEntity> where TEntity : DbEntity
    {
        private readonly IProvideConnection _connection;
        private readonly IEntityMetadata<TEntity> _metadata;
        private readonly IEntitySqlMapper<TEntity> _sql;


        public EntityReader([NotNull] IEntitySqlMapper<TEntity> sql, [NotNull] IEntityMetadata<TEntity> metadata,
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

        public Task<TEntity> GetAsync()
        {
            throw new NotImplementedException();
        }

        public TEntity Get()
        {
            throw new NotImplementedException();
        }

        public IObservable<TEntity> GetObservable(EntitiesReaderQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetEnumerable(EntitiesReaderQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }
    }
}