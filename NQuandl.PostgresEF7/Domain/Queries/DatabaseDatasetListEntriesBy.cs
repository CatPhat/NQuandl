using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.PostgresEF7.Api.Entities;
using NQuandl.PostgresEF7.Api.Transactions;
using NQuandl.PostgresEF7.Domain.Entities;

namespace NQuandl.PostgresEF7.Domain.Queries
{
    public class DatabaseDatasetListEntriesBy : BaseEntityQuery<DatabaseDatasetListEntry>, IDefineQuery<Task<IQueryable<DatabaseDatasetListEntry>>>
    {
        public string DatabaseCode { get; set; }

        public DatabaseDatasetListEntriesBy(string databaseCode)
        {
            DatabaseCode = databaseCode;
        }
    }

    public class HandleDatabaseDatasetListEntriesBy : IHandleQuery<DatabaseDatasetListEntriesBy, Task<IQueryable<DatabaseDatasetListEntry>>>
    {
        private readonly IReadEntities _entities;
        public HandleDatabaseDatasetListEntriesBy([NotNull] IReadEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public Task<IQueryable<DatabaseDatasetListEntry>> Handle(DatabaseDatasetListEntriesBy query)
        {
            var entities = _entities.Query<DatabaseDatasetListEntry>().Where(x => x.DatabaseCode == query.DatabaseCode);
            return Task.FromResult(entities);
        }
    }
}
