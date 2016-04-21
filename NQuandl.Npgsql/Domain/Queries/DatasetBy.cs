using System;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DatasetBy : IDefineQuery<Task<Dataset>>
    {
        public DatasetBy(string requestUri)
        {
            RequestUri = requestUri;
        }

        public string RequestUri { get; }
    }

    public class HandleDatasetBy : IHandleQuery<DatasetBy, Task<Dataset>>
    {
        private readonly IMapDataRecordToEntity<Dataset> _mapper;
        private readonly IExecuteRawSql _sql;

        public HandleDatasetBy([NotNull] IExecuteRawSql sql, [NotNull] IMapDataRecordToEntity<Dataset> mapper)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _sql = sql;
            _mapper = mapper;
        }

        public async Task<Dataset> Handle(DatasetBy query)
        {
            var queryString = new StringBuilder();
            queryString.Append($"select {_mapper.GetColumnNames()} " +
                               $"from {_mapper.GetTableName()} " +
                               $"where {_mapper.GetColumnNameByPropertyName(x => x.Code)} " +
                               $"= '{query.RequestUri}'");

            var result = _sql.ExecuteQueryAsync(queryString.ToString());
            var task = result.FirstOrDefaultAsync();
            return _mapper.ToEntity(await task);
        }
    }
}