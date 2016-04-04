using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Persistence.Entities;
using NQuandl.Api.Persistence.Transactions;
using NQuandl.Domain.Persistence.Entities;

namespace NQuandl.Domain.Persistence.Commands
{
    public class CreateDatabase : BaseEntityCommand, IDefineCommand
    {
        public CreateDatabase([NotNull] Database database)
        {
            if (database == null)
                throw new ArgumentNullException(nameof(database));
            Database = database;
        }

        public Database Database { get; }
    }

    public class HandleCreateDatabase : IHandleCommand<CreateDatabase>
    {
        private readonly IWriteEntities _entities;

        public HandleCreateDatabase([NotNull] IWriteEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public async Task Handle(CreateDatabase command)
        {
            _entities.Create(command.Database);
            await _entities.SaveChangesAsync();
        }
    }
}