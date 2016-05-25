using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class DeleteEntities<TEntity> : IDefineCommand where TEntity : DbEntity
    {
        public DeleteEntities(Expression<Func<TEntity, object>> whereColumn,
            string whereStringValue)
        {
           
            WhereColumn = whereColumn;
            WhereStringValue = whereStringValue;
        }

        public DeleteEntities(Expression<Func<TEntity, object>> whereColumn,
            int whereIntValue)
        {
            WhereColumn = whereColumn;
            WhereIntValue = whereIntValue;
        }
        
        public Expression<Func<TEntity, object>> WhereColumn { get; }
        public string WhereStringValue { get; }
        public int? WhereIntValue { get; }
    }

    public class HandleDeleteEntities<TEntity> : IHandleCommand<DeleteEntities<TEntity>> where TEntity : DbEntity
    {
        private readonly IDbContext _dbContext;
        private readonly IEntityMetadataCache<TEntity> _metadata;

        public HandleDeleteEntities([NotNull] IDbContext dbContext, [NotNull] IEntityMetadataCache<TEntity> metadata)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            _dbContext = dbContext;
            _metadata = metadata;
        }

        public async Task Handle(DeleteEntities<TEntity> command)
        {
            DeleteCommand deleteCommand;
            var tableName = _metadata.GetTableName();
            var whereColumn = _metadata.GetColumnName(command.WhereColumn);
            if (command.WhereIntValue.HasValue)
            {
                deleteCommand = new DeleteCommand(tableName, whereColumn, command.WhereIntValue.Value);
            }
            else if (!string.IsNullOrEmpty(command.WhereStringValue))
            {
                deleteCommand = new DeleteCommand(tableName, whereColumn, command.WhereStringValue);
            }
            else
            {
                throw new Exception("missing where value");
            }

            await _dbContext.DeleteRowsAsync(deleteCommand);
        }
    }
}