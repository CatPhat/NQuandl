using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.PostgresEF7.Api.Entities;
using NQuandl.PostgresEF7.Api.Transactions;
using NQuandl.PostgresEF7.Domain.Entities;

namespace NQuandl.PostgresEF7.Domain.Queries
{
    public class DatabaseDatasetListEntryBy : IDefineQuery<Task<DatabaseDatasetListEntry>>
    {
        public DatabaseDatasetListEntryBy(string quandlCode)
        {
            QuandlCode = quandlCode;
        }

        public string QuandlCode { get; }
    }

    public class HandleDatabaseDatasetListEntryBy :
        IHandleQuery<DatabaseDatasetListEntryBy, Task<DatabaseDatasetListEntry>>
    {
        private readonly IReadEntities _entities;

        public HandleDatabaseDatasetListEntryBy([NotNull] IReadEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public Task<DatabaseDatasetListEntry> Handle(DatabaseDatasetListEntryBy query)
        {
            var entity =
                _entities.Query<DatabaseDatasetListEntry>()
                    .FirstOrDefault(
                        x => x.QuandlCode == query.QuandlCode);

            return Task.FromResult(entity);
        }
    }
}