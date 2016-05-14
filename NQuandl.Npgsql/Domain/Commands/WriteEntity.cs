using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Services.Extensions;

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
        private readonly IDbContext _dbContext;
        private readonly IEntityMetadataCache<TEntity> _metadata;

        public HandleWriteEntity([NotNull] IDbContext dbContext, [NotNull] IEntityMetadataCache<TEntity> metadata)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));

            _dbContext = dbContext;
            _metadata = metadata;
        }

        public async Task Handle(WriteEntity<TEntity> command)
        {
            var dbImportDatas = _metadata.CreateInsertDatas(command.Entity);
            var writeCommand = new WriteCommand
            {
                Datas = dbImportDatas,
                TableName = _metadata.GetTableName()
            };
            await _dbContext.WriteAsync(writeCommand);
        }
    }
}