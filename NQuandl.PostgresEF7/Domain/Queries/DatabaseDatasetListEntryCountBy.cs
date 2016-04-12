using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.PostgresEF7.Api.Transactions;

namespace NQuandl.PostgresEF7.Domain.Queries
{
    public class DatabaseDatasetListEntryCountBy : IDefineQuery<Task<int>>
    {
        public DatabaseDatasetListEntryCountBy(string databaseCode)
        {
            DatabaseCode = databaseCode;
        }

        public string DatabaseCode { get; }
    }

    public class HandleDatasetListEntryCountBy : IHandleQuery<DatabaseDatasetListEntryCountBy, Task<int>>
    {
        private readonly IExecuteQueries _queries;

        public HandleDatasetListEntryCountBy([NotNull] IExecuteQueries queries)
        {
            if (queries == null)
                throw new ArgumentNullException(nameof(queries));
            _queries = queries;
        }

        public async Task<int> Handle(DatabaseDatasetListEntryCountBy query)
        {
            var result = await _queries.Execute(new DatabaseDatasetListEntriesBy(query.DatabaseCode));
            var count = result.Count();
            return count;
        }
    }
}