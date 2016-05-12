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
        private readonly IDb _db;
        private readonly IEntityMetadataCache<TEntity> _metadata;

        public HandleBulkWriteEntities([NotNull] IEntityMetadataCache<TEntity> metadata, [NotNull] IDb db)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));

            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _metadata = metadata;
            _db = db;
        }

        public async Task Handle(BulkWriteEntities<TEntity> command)
        {
            IObservable<IEnumerable<DbInsertData>> dbDatas;
            if (command.EntitiesEnumerable != null && command.EntitiesEnumerable.Any())
            {
                dbDatas = GetDbImportDatasObservable(command.EntitiesEnumerable.ToObservable());
            }
            else
            {
                dbDatas = GetDbImportDatasObservable(command.EntitiesObservable);
            }

            var bulkWriteCommand = new BulkWriteCommand
            {
                DatasObservable = dbDatas,
                TableName = _metadata.GetTableName()
            };
            await _db.BulkWriteAsync(bulkWriteCommand);
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