using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Domain.Persistence.Api.Entities;
using NQuandl.Domain.Persistence.Api.Transactions;
using NQuandl.Domain.Persistence.Domain.Entities;

namespace NQuandl.Domain.Persistence.Domain.Commands
{
    public class CreateDatabaseDatasetListEntry : IDefineCommand
    {
        public CreateDatabaseDatasetListEntry(DatabaseDatasetListEntry entry)
        {
            Entry = entry;
        }

        public DatabaseDatasetListEntry Entry { get; }
    }

    public class HandleCreateDatabaseDatasetListEntry : IHandleCommand<CreateDatabaseDatasetListEntry>
    {
        private readonly IWriteEntities _entities;

        public HandleCreateDatabaseDatasetListEntry([NotNull] IWriteEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public async Task Handle(CreateDatabaseDatasetListEntry command)
        {
            _entities.Create(command.Entry);
            await _entities.SaveChangesAsync();
        }
    }
}