using System;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Domain.Persistence.Api.Entities;
using NQuandl.Domain.Persistence.Api.Transactions;
using NQuandl.Domain.Persistence.Domain.Entities;

namespace NQuandl.Domain.Persistence.Domain.Queries
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
