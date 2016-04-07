using System;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Domain.Persistence.Api.Entities;
using NQuandl.Domain.Persistence.Api.Transactions;
using NQuandl.Domain.Persistence.Domain.Entities;

namespace NQuandl.Domain.Persistence.Domain.Queries
{
    public class DatabaseBy : IDefineQuery<Task<Database>>
    {
        public DatabaseBy(string databaseCode)
        {
            DatabaseCode = databaseCode;
        }

        public string DatabaseCode { get; }
    }

    public class HandleDatabaseBy : IHandleQuery<DatabaseBy, Task<Database>>
    {
        private readonly IReadEntities _entities;

        public HandleDatabaseBy([NotNull] IReadEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public Task<Database> Handle(DatabaseBy query)
        {
            var entity = _entities.Query<Database>().FirstOrDefault(x => x.DatabaseCode == query.DatabaseCode);
            return Task.FromResult(entity);
        }
    }
}