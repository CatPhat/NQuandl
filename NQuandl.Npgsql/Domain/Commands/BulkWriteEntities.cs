using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class BulkWriteEntities<TEntity> : IDefineCommand where TEntity : DbEntity
    {
        public BulkWriteEntities(IObservable<TEntity> entitiesObservable)
        {
            EntitiesObservable = entitiesObservable;
        }

        public BulkWriteEntities(IEnumerable<TEntity> entitiesEnumerable)
        {
            EntitiesEnumerable = entitiesEnumerable;
        }

        public IEnumerable<TEntity> EntitiesEnumerable { get; }
        public IObservable<TEntity> EntitiesObservable { get; }
    }

    public class HandleBulkWriteEntities<TEntity> : IHandleCommand<BulkWriteEntities<TEntity>> where TEntity : DbEntity
    {
        private readonly IDbContext _dbContext;
        private readonly IEntityMetadataCache<TEntity> _metadata;

        public HandleBulkWriteEntities([NotNull] IEntityMetadataCache<TEntity> metadata, [NotNull] IDbContext dbContext)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));

            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            _metadata = metadata;
            _dbContext = dbContext;
        }

        public async Task Handle(BulkWriteEntities<TEntity> command)
        {
            IEnumerable<IEnumerable<DbInsertData>> dbDatas;
            if (command.EntitiesEnumerable != null && command.EntitiesEnumerable.Any())
            {
                dbDatas = GetDbImportDatasEnumerable(command.EntitiesEnumerable.ToObservable());
            }
            else
            {
                dbDatas = GetDbImportDatasEnumerable(command.EntitiesObservable);
            }

            var bulkWriteCommand = new BulkWriteCommand
            {
                DatasObservable = dbDatas,
                TableName = _metadata.GetTableName()
            };
            await _dbContext.BulkWriteAsync(bulkWriteCommand);
        }

        private IEnumerable<IEnumerable<DbInsertData>> GetDbImportDatasEnumerable(
            IObservable<TEntity> entitiesObservable)
        {
            var insertDatas = new List<IEnumerable<DbInsertData>>();
            entitiesObservable.Subscribe(entity => insertDatas.Add(_metadata.CreateInsertDatas(entity)), onError: ex => {throw new Exception(ex.Message);});
            return insertDatas;

        }

        private IObservable<IEnumerable<DbInsertData>> GetDbImportDatasObservable(IObservable<TEntity> entities)
        {
            return Observable.Create<IEnumerable<DbInsertData>>(observer =>
                entities.Subscribe(
                    entity => observer.OnNext(_metadata.CreateInsertDatas(entity)),
                    onCompleted: observer.OnCompleted,
                    onError: ex => { throw new Exception(ex.Message); }));
        }
    }
}